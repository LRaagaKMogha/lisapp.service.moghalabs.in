using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MasterManagement.Contracts;
using MasterManagement.ServiceErrors;
using MasterManagement.Validators;

namespace MasterManagement.Models
{
    public class Product
    {
        public const int MinCodeLength = 2;
        public const int MaxCodeLength = 100;

        public const int MinDescriptionLength = 2;
        public const int MaxDescriptionLength = 100;
        public Int64 Id { get; set; }
        public string Code { get; set; } = "";
        public string Description { get; set; } = "";

        public Int64 CategoryId { get; set; }
        public DateTime EffectiveFromDate { get; set; }
        public DateTime? EffectiveToDate { get; set; }
        public int MinCount { get; set; } = 1;
        public int MaxCount { get; set; } = 3;
        public int ThresholdCount { get; set; } = 2;
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }

        public bool IsThawed { get; set; }
        public ICollection<ProductSpecialRequirement> ProductSpecialRequirements { get; set; } = new List<ProductSpecialRequirement>();
        public Product()
        {

        }
        public Product(Int64 identifier, string code, string description, Int64 categoryId, DateTime effectiveFromDate, DateTime effectiveToDate,
        int minCount, int maxCount, int thresholdCount, List<ProductSpecialRequirement> productSpecialRequirements, bool isActive, bool isThawed)
        {
            Id = identifier;
            Code = code;
            Description = description;
            IsActive = isActive;
            CategoryId = categoryId;
            EffectiveFromDate = effectiveFromDate;
            EffectiveToDate = effectiveToDate;
            MinCount = minCount;
            MaxCount = maxCount;
            ThresholdCount = thresholdCount;
            ProductSpecialRequirements = productSpecialRequirements;
            IsThawed = isThawed;
        }

        public static ErrorOr<Product> Create(string code, string description, Int64 categoryId, DateTime effectiveFromDate, DateTime effectiveToDate,
             int minCount, int maxCount, int thresholdCount, List<ProductSpecialRequirement> productSpecialRequirements, bool isActive, bool isThawed, Int64? identifier = null)
        {
            return new Product(identifier ?? 0, code, description, categoryId, effectiveFromDate, effectiveToDate,
            minCount, maxCount, thresholdCount, productSpecialRequirements, isActive, isThawed);
        }

        public static ErrorOr<Product> From(UpsertProductRequest request, HttpContext httpContext)
        {
            var productSpecialRequirements = request.SpecialRequirementIds.Select(x =>
            {
                return new ProductSpecialRequirement(x);
            }).ToList();
            var input = Create(request.Code, request.Description, request.CategoryId, request.EffectiveFromDate, request.EffectiveToDate,
             request.MinCount, request.MaxCount, request.ThresholdCount, productSpecialRequirements, request.IsActive, request.IsThawed);
            List<Error> errors = Shared.Helpers.ValidateInput<Product, ProductValidator>(input.Value, httpContext);

            if (errors.Count > 0)
            {
                return errors;
            }
            return input;
        }

        public static ErrorOr<Product> From(Int64 id, UpsertProductRequest request, HttpContext httpContext)
        {
            var productSpecialRequirements = request.SpecialRequirementIds.Select(x =>
            {
                return new ProductSpecialRequirement(id, x);
            }).ToList();
            var input = Create(request.Code, request.Description, request.CategoryId, request.EffectiveFromDate, request.EffectiveToDate,
             request.MinCount, request.MaxCount, request.ThresholdCount, productSpecialRequirements, request.IsActive, request.IsThawed, id);
            List<Error> errors = Shared.Helpers.ValidateInput<Product, ProductValidator>(input.Value, httpContext);

            if (errors.Count > 0)
            {
                return errors;
            }
            return input;

        }
    }
}