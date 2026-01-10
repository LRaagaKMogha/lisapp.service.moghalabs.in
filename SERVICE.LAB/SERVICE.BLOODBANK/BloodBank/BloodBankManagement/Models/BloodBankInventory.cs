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
    public class BloodBankInventory
    {
        public Int64 Identifier { get; set; }
        public string BatchId { get; set; } = "";
        public string DonationId { get; set; } = "";
        public string CalculatedDonationId { get; set; }
        public Int64 ProductCode { get; set; }
        public DateTime ExpirationDateAndTime { get; set; }
        public string AboOnLabel { get; set; }
        public string Volume { get; set; }
        public string AntiAGrading { get; set; }
        public string AntiBGrading { get; set; }
        public string AntiABGrading { get; set; }
        public string AboResult { get; set; }
        public string AboPerformedByUserName { get; set; }
        public DateTime? AboPerformedByDateTime { get; set; }
        public string Status { get; set; }
        public bool IsRejected { get; set; }
        public bool IsVisualInspectionPassed { get; set; }
        public string Comments { get; set; }
        public string Temprature { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public string Antibodies { get; set; }
        public Int64 ModifiedProductId { get; set; } = 0;
        public ICollection<BloodBankInventoryTransaction> Transactions { get; } = new List<BloodBankInventoryTransaction>();

        public BloodBankInventory()
        {

        }

        public BloodBankInventory(
            Int64 identifier,
            string batchId,
            string donationId,
            string calculatedDonationId,
            Int64 productCode,
            DateTime expirationDateAndTime,
            string aboOnLabel,
            string volume,
            string antiAGrading,
            string antiBGrading,
            string antiABGrading,
            string aboResult,
            string aboPerformedByUserName,
            DateTime? aboPerformedByDateTime,
            string status,
            bool isRejected,
            bool isVisualInspectionPassed,
            string comments,
            string temprature,
            Int64 modifiedBy,
            string modifiedByUserName,
            string antibodies,
            DateTime lastModifiedDateTime
            )
        {
            Identifier = identifier;
            BatchId = batchId;
            DonationId = donationId;
            CalculatedDonationId = calculatedDonationId;
            ProductCode = productCode;
            ExpirationDateAndTime = expirationDateAndTime;
            AboOnLabel = aboOnLabel;
            Volume = volume;
            AntiAGrading = antiAGrading;
            AntiBGrading = antiBGrading;
            AntiABGrading = antiABGrading;
            AboResult = aboResult;
            AboPerformedByUserName = aboPerformedByUserName;
            AboPerformedByDateTime = aboPerformedByDateTime;
            Status = status;
            IsRejected = isRejected;
            IsVisualInspectionPassed = isVisualInspectionPassed;
            Comments = comments;
            Temprature = temprature;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            Antibodies = antibodies;
            LastModifiedDateTime = lastModifiedDateTime;
        }


        public static ErrorOr<BloodBankInventory> Create(
         string batchId,
         string donationId,
         string calculatedDonationId,
         Int64 productCode,
         DateTime expirationDateAndTime,
         string aboOnLabel,
         string volume,
         string antiAGrading,
         string antiBGrading,
         string antiABGrading,
         string aboResult,
         string aboPerformedByUserName,
         DateTime? aboPerformedByDateTime,
         string status,
         bool isRejected,
         bool isVisualInspectionPassed,
         string comments,
         string temprature,
         Int64 modifiedBy,
         string modifiedByUserName,
         string antibodies,
         DateTime lastModifiedDateTime,
         Int64? identifier = null)
        {
            return new BloodBankInventory(identifier ?? 0, batchId, donationId, calculatedDonationId, productCode, expirationDateAndTime, aboOnLabel, volume, antiAGrading, antiBGrading, antiABGrading, aboResult, aboPerformedByUserName, aboPerformedByDateTime, status, isRejected, isVisualInspectionPassed, comments, temprature, modifiedBy, modifiedByUserName, antibodies, lastModifiedDateTime);
        }

        public static ErrorOr<BloodBankInventory> From(UpsertBloodBankInventoryRequest request, HttpContext httpContext)
        {
            var response = Create(request.BatchId, request.DonationId, request.CalculatedDonationId ?? request.DonationId, request.ProductCode, request.ExpirationDateAndTime, request.AboOnLabel, request.Volume, request.AntiAGrading ?? "", request.AntiBGrading ?? "",
            request.AntiABGrading ?? "", request.AboResult ?? "", request.AboPerformedByUserName, request.AboPerformedByDateTime, request.Status, request.IsRejected, request.IsVisualInspectionPassed, request.Comments, request.Temprature, request.ModifiedBy, request.ModifiedByUserName, request.Antibodies, request.LastModifiedDateTime, request.Identifier);
            List<Error> errors = Shared.Helpers.ValidateInput<BloodBankInventory, BloodBankInventoryValidator>(response.Value, httpContext);
            if (errors.Count > 0)
            {
                return errors;
            }
            return response;
        }

    }
}
