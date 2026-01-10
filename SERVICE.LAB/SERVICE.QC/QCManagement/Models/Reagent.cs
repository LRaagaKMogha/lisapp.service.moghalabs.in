using Azure;
using ErrorOr;
using QC.Validators;
using QCManagement.Contracts;
using QCManagement.Validators;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Models
{
    public class Reagent
    {
        public Int64 Identifier { get; private set; }
        public string Name { get; set;}
        public string LotNo { get; set;}
        public string SequenceNo { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? LotSetupOrInstallationDate { get; set;}
        public Int64 ManufacturerID { get;  set; }
        public Int64 DistributorID { get;  set; }
        public bool Status { get; set;  }
        public Int64 DepartmentId { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime LastModifiedDateTime { get; set; }   

        public Reagent()
        {

        }     
        public Reagent(Int64 identifier, string name, string lotNo, string sequenceNo, DateTime expirationDate, DateTime? lotSetupOrInstallationDate, Int64 manufacturerID, Int64 distributorID, bool status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, Int64 departmentId)
        {
            Identifier = identifier;
            Name = name;
            LotNo = lotNo;
            SequenceNo = sequenceNo;
            ExpirationDate = expirationDate;
            LotSetupOrInstallationDate = lotSetupOrInstallationDate;
            ManufacturerID = manufacturerID;
            DistributorID = distributorID;
            Status = status;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
            DepartmentId = departmentId;
        }

        public static ErrorOr<Reagent> From(ReagentRequest request, HttpContext httpContext)
        {
            var input = new Reagent(request.Identifier, request.Name, request.LotNo, request.SequenceNo, request.ExpirationDate, request.LotSetupOrInstallationDate, request.ManufacturerID, request.DistributorID, request.Status, request.ModifiedBy, request.ModifiedByUserName, request.LastModifiedDateTime, request.DepartmentId);
            var errors = Helpers.ValidateInput<Reagent, ReagentValidator>(input, httpContext);
            if (errors.Count > 0)
                return errors;
            return input;
        }
    }
}