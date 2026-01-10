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
    public class Nurse
    {
        public const int MinNameLength = 2;
        public const int MaxNameLength = 100;

        public const int MinDescriptionLength = 2;
        public const int MaxDescriptionLength = 100;
        public Int64 Id { get; set; }
        public string Name { get; set; } = "";
        public string EmployeeId { get; set; } = "";
        public string Description { get; set; } = "";
        public Int64 ModifiedBy { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public Nurse()
        {

        }
        public Nurse(Int64 identifier, string name, string employeeId, string description, bool isActive, Int64 modifiedBy)
        {
            Id = identifier;
            Name = name;
            EmployeeId = employeeId;
            Description = description;
            IsActive = isActive;
            ModifiedBy = modifiedBy;
        }

        public static ErrorOr<Nurse> Create(string name, string employeeId, string description, bool isActive, Int64 modifiedBy, Int64? identifier = null)
        {
            return new Nurse(identifier ?? 0, name, employeeId, description, isActive, modifiedBy);
        }

        public static ErrorOr<Nurse> From(UpsertNurseRequest request, HttpContext httpContext)
        {
            var input = Create(request.Name, request.EmployeeId, request.Description, request.IsActive, request.ModifiedBy);
            List<Error> errors = Shared.Helpers.ValidateInput<Nurse, NurseValidator>(input.Value, httpContext);

            if (errors.Count > 0)
            {
                return errors;
            }
            return input;
        }

        public static ErrorOr<Nurse> From(Int64 id, UpsertNurseRequest request, HttpContext httpContext)
        {
            var input = Create(request.Name, request.EmployeeId, request.Description, request.IsActive, request.ModifiedBy, id);
            List<Error> errors = Shared.Helpers.ValidateInput<Nurse, NurseValidator>(input.Value, httpContext);

            if (errors.Count > 0)
            {
                return errors;
            }
            return input;
        }
    }
}