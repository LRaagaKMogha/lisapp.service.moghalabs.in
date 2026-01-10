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
    public class BloodSample
    {
        public Int64 Identifier { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 RegistrationId { get; set; }
        public Int64 SampleTypeId { get; set; }
        public Int64 UnitCount { get; set; }
        public bool IsActive { get; set; }
        public string TubeNo { get; set;} = "";
        public string BarCode { get; set; }
        public Int64? ParentRegistrationId { get; set;}
        public string Tests { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set;}
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public BloodSample()
        {

        }

        public BloodSample(Int64 patientId, Int64 registrationId, Int64 sampleTypeId, Int64 unitCount, string tubeNo, string barCode,  Int64 modifiedBy, string modifiedByUserName, Int64? parentRegistrationId, string tests)
        {
            PatientId = patientId;
            RegistrationId = registrationId;
            SampleTypeId = sampleTypeId;
            UnitCount = unitCount;
            TubeNo = tubeNo;
            BarCode = barCode;
            IsActive = true; 
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            ParentRegistrationId = parentRegistrationId;
            Tests = tests;
        }

        public static ErrorOr<BloodSample> From(SampleResponse request, HttpContext httpContext)
        {
            var response = new BloodSample(request.PatientId, request.RegistrationId, request.SampleTypeId, request.UnitCount, request.TubeNo, request.BarCode, request.ModifiedBy, request.ModifiedByUserName, request.ParentRegistrationId, request.Tests);
            List<Error> errors = Shared.Helpers.ValidateInput<BloodSample, BloodSampleValidator>(response, httpContext);
            if (errors.Count > 0)
            {
                return errors;
            }
            return response;
        }
    }
}