using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IArchiveRepository
    {
        List<LstSearch> ArchivePatientSearch(RequestCommonSearch req);
        List<GetArchivePatientResponse> GetArchivePatientDetails(GetArchivePatientRequest req);
    }
}

