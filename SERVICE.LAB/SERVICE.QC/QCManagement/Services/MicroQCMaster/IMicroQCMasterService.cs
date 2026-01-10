using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QCManagement.Contracts;

namespace QCManagement.Services.MicroQCMaster
{
    public interface IMicroQCMasterService
    {
        Task<ErrorOr<List<Models.MicroQCMaster>>> CreateMicroQCMaster(List<Models.MicroQCMaster> microQCMasters);
        Task<ErrorOr<List<Models.MicroQCMaster>>> UpsertMicroQCMasters(List<Models.MicroQCMaster> microQCMasters);
        Task<ErrorOr<Models.MicroQCMaster>> GetMicroQCMaster(Int64 id);
        Task<ErrorOr<List<Models.MicroQCMaster>>> GetMicroQCMasters(MicroQCMasterFilterRequest request);
        Task<ErrorOr<bool>> UpdateMicroQCMasterStatus(List<Int64> ids, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string comments = "", bool updateComments = false);
    }
}
