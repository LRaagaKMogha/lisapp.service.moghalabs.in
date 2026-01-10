using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using DEV.Model.Integration;
using ErrorOr;
using Shared;

namespace BloodBankManagement.Services.Integration
{
    public interface IIntegrationService
    {
        public Task<ErrorOr<List<BloodBankRegistration>>> GetPDFReportDetails(reportrequestdetails reportrequestdetails, int venueNo, int venueBranchNo);
        public Task<ErrorOr<List<BloodSampleResult>>> GetTestDetails(long RegistrationId);
        public Task<Tuple<int, string>> InsertLISRegistration(Int64 orderId, Contracts.BloodBankRegistration input, User user);

    }
}