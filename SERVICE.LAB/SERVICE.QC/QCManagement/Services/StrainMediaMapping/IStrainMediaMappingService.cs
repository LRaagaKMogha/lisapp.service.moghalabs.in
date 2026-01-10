using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QCManagement.Contracts;

namespace QCManagement.Services.StrainMediaMapping
{
    public interface IStrainMediaMappingService
    {
        Task<ErrorOr<List<Models.StrainMediaMapping>>> CreateStrainMediaMapping(List<Models.StrainMediaMapping> strainMediaMapping);
        Task<ErrorOr<List<Models.StrainMediaMapping>>> UpsertStrainMediaMappings(List<Models.StrainMediaMapping> mappings);
        Task<ErrorOr<Models.StrainMediaMapping>> GetStrainMediaMapping(Int64 id);
        Task<ErrorOr<List<Models.StrainMediaMapping>>> GetStrainMediaMappings(StrainMediaMappingFilterRequest request);
        Task<ErrorOr<bool>> UpdateMappingStatus(List<Int64> mappingIds, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string comments = "", bool updateMappingComments = false);
    }
}
