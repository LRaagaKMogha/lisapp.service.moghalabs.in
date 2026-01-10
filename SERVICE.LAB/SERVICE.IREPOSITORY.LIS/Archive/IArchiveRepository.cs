using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IArchiveRepository
    {
        List<LstSearch> ArchivePatientSearch(RequestCommonSearch req);
        List<GetArchivePatientResponse> GetArchivePatientDetails(GetArchivePatientRequest req);
    }
}

