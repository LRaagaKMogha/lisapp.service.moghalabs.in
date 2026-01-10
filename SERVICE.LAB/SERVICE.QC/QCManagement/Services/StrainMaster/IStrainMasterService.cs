using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QCManagement.Contracts;

namespace QCManagement.Services.StrainMaster
{
    public interface IStrainMasterService
    {
        Task<ErrorOr<List<Models.StrainMaster>>> CreateStrainMaster(List<Models.StrainMaster> strainMasters);
        Task<ErrorOr<List<Models.StrainMaster>>> UpsertStrainMasters(List<Models.StrainMaster> strainMasters);
        Task<ErrorOr<Models.StrainMaster>> GetStrainMaster(Int64 id);
        Task<ErrorOr<List<Models.StrainMaster>>> GetStrainMasters(StrainMasterFilterRequest request);
        Task<ErrorOr<bool>> UpdateStrainMasterStatus(List<Int64> strainMasterIds, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string comments = "", bool updateComments = false);
    }
}