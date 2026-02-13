using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IAnaParamRepository
    {
        AnaParamDtoResponse InsertAnaParam(AnaParamDto AnaParamobj);
        List<AnaParamGetDto> GetAnaParamDetails(int VenueNo, int VenueBranchNo, int Analyzerno, int Sampleno);
        List<FetchAnaParamDto> FetchAnalyzerParamDetails(int VenueNo, int VenueBranchNo, int Analyzerno, int Sampleno);
    }
}
