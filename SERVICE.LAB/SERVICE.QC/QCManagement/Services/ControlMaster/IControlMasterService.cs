using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QCManagement.Contracts;

namespace QCManagement.Services.ControlMaster
{
    public interface IControlMasterService
    {
        Task<ErrorOr<Models.ControlMaster>> CreateControlMaster(Models.ControlMaster controlMaster);
        Task<ErrorOr<Models.ControlMaster>> UpdateControlMaster(Models.ControlMaster controlMaster);
        Task<ErrorOr<Models.ControlMaster>> PrepareControlMaster(Contracts.PrepareControlMasterRequest request);
        
        Task<ErrorOr<Models.ControlMaster>> GetControlMaster(Int64 id);
        Task<ErrorOr<List<Models.ControlMaster>>> GetControlMasters(ControlMasterFilterRequest request);
        // Task<ErrorOr<List<Contracts.TestResponse>>> GetBloodBankTests();
        // Task<ErrorOr<List<Contracts.SubTestResponse>>> GetBloodBankSubTests();        
    }
}