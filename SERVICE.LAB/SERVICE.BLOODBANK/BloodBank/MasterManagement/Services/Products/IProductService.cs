using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterManagement.Models;
using ErrorOr;

namespace MasterManagement.Services.Products
{
    public interface IProductService
    {
        Task<ErrorOr<Product>> CreateProduct(Product Product);
        Task<ErrorOr<List<Product>>> GetProducts();
        Task<ErrorOr<Product>> GetProduct(Int64 id);
        Task<ErrorOr<UpsertedProductResult>> UpsertProduct(Product Product);
        Task<ErrorOr<Deleted>> DeleteProduct(Int64 id);
    }
}