using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.Validators;
using ErrorOr;

namespace BloodBankManagement.Models
{
    public class BloodSampleInventory
    {
        public Int64 Identifier { get; set; }
        public Int64 RegistrationId { get; set; }
        public Int64 BloodSampleResultId { get; set; }
        public Int64 ProductId { get; set; }
        public Int64 InventoryId { get; set; }
        public bool IsMatched { get; set; }
        public bool IsComplete { get; set; }
        public string Comments { get; set; } = "";
        public string Status { get; set; }
        public Int64? IssuedToNurseId { get; set; }
        public Int64? ClinicId { get; set; }
        public Int64? PostIssuedClinicId { get; set; }
        
        public Int64? ReturnedByNurseId { get; set; }
        public Int64 CrossMatchingTestId { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public DateTime? TransfusionDateTime { get; set; }
        public string TransfusionVolume { get; set; } = "";
        public bool IsTransfusionReaction { get; set; }
        public string TransfusionComments { get; set; } = "";
        public DateTime? CompatibilityValidTill { get; set; }
        public DateTime? IssuedDateTime { get; set; }
        public BloodBankRegistration BloodBankRegistration { get; set;  }
        public BloodSampleInventory()
        {

        }

        public BloodSampleInventory(Int64 identifier, Int64 registrationId, Int64 bloodSampleResultId, Int64 productId, Int64 inventoryId, bool isMatched, bool isComplete, string comments, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, Int64 crossMatchingTestId, DateTime? compatibilityValidTill = null, DateTime? issuedDateTime = null)
        {
            this.Identifier = identifier;
            this.RegistrationId = registrationId;
            this.BloodSampleResultId = bloodSampleResultId;
            this.ProductId = productId;
            this.InventoryId = inventoryId;
            this.IsMatched = isMatched;
            this.IsComplete = isComplete;
            this.Comments = comments;
            this.Status = status;
            this.ModifiedBy = modifiedBy;
            this.ModifiedByUserName = modifiedByUserName;
            this.LastModifiedDateTime = lastModifiedDateTime;
            this.CrossMatchingTestId = crossMatchingTestId;
            this.CompatibilityValidTill = compatibilityValidTill;
            this.IssuedDateTime = issuedDateTime;
        }

        public static ErrorOr<BloodSampleInventory> Create(Int64 registrationId, Int64 bloodSampleResultId, Int64 productId, Int64 inventoryId, bool isMatched, bool isComplete, string comments, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, Int64 crossMatchingTestId, DateTime? compatibilityValidTill, DateTime? issuedDateTime, Int64? identifier = null)
        {

            return new BloodSampleInventory(identifier ?? 0, registrationId, bloodSampleResultId, productId, inventoryId, isMatched, isComplete, comments, status, modifiedBy, modifiedByUserName, lastModifiedDateTime, crossMatchingTestId, compatibilityValidTill, issuedDateTime);
        }

        public static ErrorOr<BloodSampleInventory> From(UpsertBloodSampleInventoryRequest request, HttpContext httpContext)
        {
            var response = Create(request.RegistrationId, request.BloodSampleResultId, request.ProductId, request.InventoryId, request.IsMatched, request.IsComplete, request.Comments, request.Status, request.ModifiedBy, request.ModifiedByUserName, request.LastModifiedDateTime, request.CrossMatchingTestId, request.CompatibilityValidTill, request.IssuedDateTime, request.Identifier);
            List<Error> errors = Shared.Helpers.ValidateInput<BloodSampleInventory, BloodSampleInventoryValidator>(response.Value, httpContext);
            if (errors.Count > 0)
            {
                return errors;
            }
            return response;
        }
    }
}