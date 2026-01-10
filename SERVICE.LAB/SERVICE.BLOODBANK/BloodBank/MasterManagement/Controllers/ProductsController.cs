using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ErrorOr;
using MasterManagement.Contracts;
using MasterManagement.Models;
using MasterManagement.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Audit;

namespace MasterManagement.Controllers
{
    [CustomAuthorize("LIMSFRONTOFFICE,LIMSSAMPLEMNTC,LIMSPATIENTRESULTS,LIMSPATIENTREPORTS,LIMSMISREPORTS,LIMSMasters,LIMSUserMgmt,BloodBankMgmt,BloodBankMasters,QCMgmt,MicroQCMgmt")]
    public class ProductsController : ApiController
    {
        private readonly IProductService _ProductService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuditService _auditService;

        public ProductsController(IProductService ProductService, IHttpContextAccessor _httpContextAccessor, IAuditService auditService)
        {
            _ProductService = ProductService;
            httpContextAccessor = _httpContextAccessor;
            _auditService = auditService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProduct(UpsertProductRequest request)
        {
            var requestToProductResult = Models.Product.From(request, httpContextAccessor.HttpContext!);

            if (requestToProductResult.IsError)
            {
                return Problem(requestToProductResult.Errors);
            }

            var Product = requestToProductResult.Value;
            ErrorOr<Models.Product> createProductResult;
            using (var auditScope = new AuditScope<UpsertProductRequest>(request, _auditService, "", new string[] { "Create Product" }))
            {
                createProductResult = await _ProductService.CreateProduct(Product);
                auditScope.IsRollBack = createProductResult.IsError;
            }
                

            return createProductResult.Match(
                created => CreatedAtGetProduct(createProductResult.Value),
                errors => Problem(errors));
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.Product>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            ErrorOr<List<Models.Product>> response = await _ProductService.GetProducts();
            return response.Match(
                Products => base.Ok(new ServiceResponse<List<Contracts.Product>>("", "200", Products.Select(x => MapProductResponse(x)).ToList())),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProduct(Int64 id)
        {
            ErrorOr<Models.Product> getProductResult = await _ProductService.GetProduct(id);

            return getProductResult.Match(
                Product => base.Ok(new ServiceResponse<Contracts.Product>("", "200", MapProductResponse(Product))),
                errors => Problem(errors));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertProduct(Int64 id, UpsertProductRequest request)
        {
            ErrorOr<Models.Product> requestToProductResult = Models.Product.From(id, request, httpContextAccessor.HttpContext!);

            if (requestToProductResult.IsError)
            {
                return Problem(requestToProductResult.Errors);
            }

            var Product = requestToProductResult.Value;
            ErrorOr<UpsertedProductResult> upsertProductResult;
            using (var auditScope = new AuditScope<UpsertProductRequest>(id.ToString(), request, _auditService, "", new string[] { "Update Product" }))
            {
                upsertProductResult = await _ProductService.UpsertProduct(Product);
                auditScope.IsRollBack = upsertProductResult.IsError;
            }

            return upsertProductResult.Match(
                upserted => CreatedAtGetProduct(Product),
                errors => Problem(errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Int64 id)
        {
            ErrorOr<Deleted> deleteProductResult = await _ProductService.DeleteProduct(id);

            return deleteProductResult.Match(
                deleted => NoContent(),
                errors => Problem(errors));
        }

        private CreatedAtActionResult CreatedAtGetProduct(Models.Product Product)
        {
            return base.CreatedAtAction(
                actionName: nameof(GetProduct),
                routeValues: new { id = Product.Id },
                value: new ServiceResponse<Contracts.Product>("", "201", MapProductResponse(Product)));
        }

        private static Contracts.Product MapProductResponse(Models.Product Product)
        {
            var specialRequirementIds = Product.ProductSpecialRequirements.Select(x => x.SpecialRequirementId).ToList();
            return new Contracts.Product(
                Product.Id,
                Product.Description,
                Product.Code,
                Product.CategoryId,
                Product.EffectiveFromDate,
                Product.EffectiveToDate,
                Product.MinCount,
                Product.MaxCount,
                Product.ThresholdCount,
                specialRequirementIds,
                Product.IsActive,
                Product.IsThawed,
                Product.LastModifiedDateTime
            );
        }
    }
}