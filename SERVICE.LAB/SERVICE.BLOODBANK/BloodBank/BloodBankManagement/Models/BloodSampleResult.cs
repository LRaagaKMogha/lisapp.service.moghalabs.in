using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.Validators;
using ErrorOr;
using Microsoft.Identity.Client;

namespace BloodBankManagement.Models
{
    public class BloodSampleResult
    {
        public Int64 Identifier { get; set; }
        public Int64 BloodBankRegistrationId { get; set; }
        public Int64 ContainerId { get; set; }
        public Int64 TestId { get; set; }
        public Int64 ParentTestId { get; set; }
        public Int64 InventoryId { get; set; }
        public string TestName { get; set; } = "";
        public string Unit { get; set; }
        public string TestValue { get; set; }
        public string Comments { get; set; }
        public string BarCode { get; set; }
        public string Status { get; set; }
        public bool IsRejected { get; set; }
        public Int64? ParentRegistrationId { get; set; }
        public BloodBankRegistration BloodBankRegistration { get; set; }

        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public bool ReCheck { get; set; } = false;

        public bool SentToHSA { get; set; } = false;

        public bool? interfaceispicked { get; set; }
        public int? IsUploadAvail { get; set; }
        public Int64 GroupId { get; set; }
        public bool IsBilled {  get; set; }
        public BloodSampleResult()
        {

        }

        public BloodSampleResult(Int64 identifier, Int64 registrationId, Int64 containerId, Int64 testId, Int64 parentTestId, Int64 inventoryId, string testName, string unit, string testValue, string comments, string barCode, string status, bool isRejected, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, bool reCheck, bool? interfaceispicked, int? isUploadAvail, Int64 groupId, Int64? parentRegistrationId = null)
        {
            Identifier = identifier;
            BloodBankRegistrationId = registrationId;
            ContainerId = containerId;
            TestId = testId;
            ParentTestId = parentTestId;
            InventoryId = inventoryId;
            TestName = testName;
            Unit = unit;
            TestValue = testValue;
            Comments = comments;
            BarCode = barCode;
            Status = status;
            IsRejected = isRejected;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
            ReCheck = reCheck;
            if (isUploadAvail != null)
            {
                this.IsUploadAvail = isUploadAvail;
            }
            if (interfaceispicked != null)
                this.interfaceispicked = interfaceispicked;
            if (parentRegistrationId.GetValueOrDefault() != 0)
                ParentRegistrationId = parentRegistrationId;
            GroupId = groupId;
        }

        public static ErrorOr<BloodSampleResult> Create(Int64 registrationId, Int64 containerId, Int64 testId, Int64 parentTestId, Int64 inventoryId, string testName, string unit, string testValue, string comments, string barCode, string status, bool isRejected, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, bool reCheck, bool? interfaceispicked, int? isUploadAvail, Int64 groupId, Int64? identifier = null, Int64? parentRegistrationId = null)
        {

            return new BloodSampleResult(identifier ?? 0, registrationId, containerId, testId, parentTestId, inventoryId, testName, unit, testValue, comments, barCode, status, isRejected, modifiedBy, modifiedByUserName, lastModifiedDateTime, reCheck, interfaceispicked, isUploadAvail, groupId, parentRegistrationId ?? 0);
        }

        public static ErrorOr<BloodSampleResult> From(UpsertBloodSampleResultRequest request, HttpContext httpContext)
        {
            var response = Create(request.RegistrationId, request.ContainerId, request.TestId, request.ParentTestId, request.InventoryId, request.TestName, request.Unit, request.TestValue, request.Comments, request.BarCode, request.Status, request.IsRejected, request.ModifiedBy, request.ModifiedByUserName, request.LastModifiedDateTime, request.ReCheck, request.interfaceispicked, request.IsUploadAvail, request.groupId, request.Identifier, request.ParentRegistrationId);
            List<Error> errors = Shared.Helpers.ValidateInput<BloodSampleResult, BloodSampleResultValidator>(response.Value, httpContext);
            if (errors.Count > 0)
            {
                return errors;
            }
            return response;
        }
    }
}
