using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MasterManagement.Contracts;
using MasterManagement.ServiceErrors;
using MasterManagement.Validators;
using Shared;

namespace MasterManagement.Models
{
    public class Lookup
    {
        public const int MinNameLength = 1;
        public const int MaxNameLength = 100;

        public const int MinDescriptionLength = 1;
        public const int MaxDescriptionLength = 100;
        public Int64 Id { get; set; }
        public string Name { get; set; } = "";
        public string Code { get; set; } = "";
        public string Description { get; set; } = "";
        public string Type { get; set; } = "";
        public DateTime? LastModifiedDateTime { get; set; }
        public bool IsActive { get; set; } = true;
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ModifiedBy { get; set; }
        public ICollection<ProductSpecialRequirement> ProductSpecialRequirements { get; set; } = new List<ProductSpecialRequirement>();
        public Lookup()
        {

        }
        public Lookup(Int64 identifier, string name, string code, string description, string type, bool isActive)
        {
            Id = identifier;
            Name = name;
            Code = code;
            Description = description;
            Type = type;
            IsActive = isActive;
           

        }

        public static ErrorOr<Lookup> Create(string name, string code, string description, string type, bool isActive, Int64? identifier = null)
        {
            return new Lookup(identifier ?? 0, name, code, description, type, isActive);
        }

        public static ErrorOr<Lookup> From(UpsertLookupRequest request, HttpContext httpContext)
        {
            var input = Create(request.Name, request.Code, request.Description, request.Type, request.IsActive);
            List<Error> errors = Shared.Helpers.ValidateInput<Lookup, LookupValidator>(input.Value, httpContext);

            if (errors.Count > 0)
            {
                return errors;
            }
            return input;
        }

        public static ErrorOr<Lookup> From(Int64 id, UpsertLookupRequest request, HttpContext httpContext)
        {
            var input = Create(request.Name, request.Code, request.Description, request.Type, request.IsActive, id);
            List<Error> errors = Shared.Helpers.ValidateInput<Lookup, LookupValidator>(input.Value, httpContext);

            if (errors.Count > 0)
            {
                return errors;
            }
            return input;

        }
    }
}