using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterManagement.Models;
using ErrorOr;
using MasterManagement.Helpers;
using AutoMapper;
using MasterManagement.ServiceErrors;
using Microsoft.EntityFrameworkCore;

namespace MasterManagement.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly MasterDataContext dataContext;
        public ProductService(MasterDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<ErrorOr<Product>> CreateProduct(Product Product)
        {
            if (dataContext.Products.Any(x => x.Code.ToLower() == Product.Code.ToLower()))
                return Errors.Product.ProductExists;
            dataContext.Products.Add(Product);
            await this.dataContext.SaveChangesAsync();
            return Product;
        }

        public async Task<ErrorOr<Deleted>> DeleteProduct(Int64 id)
        {
            var ProductToUpdate = await dataContext.Products.FindAsync(id);
            if (ProductToUpdate != null)
            {
                ProductToUpdate.IsActive = false;
                await dataContext.SaveChangesAsync();
            }
            return Result.Deleted;
        }

        public async Task<ErrorOr<Product>> GetProduct(Int64 id)
        {
            var data = await dataContext.Products.Include(x => x.ProductSpecialRequirements).FirstOrDefaultAsync(x => x.Id == id);
            if (data != null) return data;
            return Errors.Product.NotFound;
        }

        public async Task<ErrorOr<List<Product>>> GetProducts()
        {
            return await dataContext.Products.Include(x => x.ProductSpecialRequirements).ToListAsync();
        }

        public async Task<ErrorOr<UpsertedProductResult>> UpsertProduct(Product Product)
        {
            var isNewlyCreated = false;
            var currentProduct = dataContext.Products.Include(x => x.ProductSpecialRequirements).First(x => x.Id == Product.Id);
            if (currentProduct != null)
            {
                currentProduct.Code = Product.Code;
                currentProduct.Description = Product.Description;
                currentProduct.IsActive = Product.IsActive;
                currentProduct.CategoryId = Product.CategoryId;
                currentProduct.EffectiveFromDate = Product.EffectiveFromDate;
                currentProduct.EffectiveToDate = Product.EffectiveToDate;
                currentProduct.MinCount = Product.MinCount;
                currentProduct.MaxCount = Product.MaxCount;
                currentProduct.ThresholdCount = Product.ThresholdCount;
                currentProduct.LastModifiedDateTime  = DateTime.Now;
                currentProduct.ProductSpecialRequirements = Product.ProductSpecialRequirements;
                currentProduct.IsThawed = Product.IsThawed;
                dataContext.Products.Update(currentProduct);
            }
            else
            {
                isNewlyCreated = true;
                await dataContext.Products.AddAsync(Product);
            }
            await this.dataContext.SaveChangesAsync();
            return new UpsertedProductResult(isNewlyCreated);
        }
    }
}