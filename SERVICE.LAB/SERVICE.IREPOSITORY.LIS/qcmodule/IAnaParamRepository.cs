using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IAnaParamRepository
    {
        AnaParamDtoResponse InsertAnaParam(AnaParamDto AnaParamobj);
        List<AnaParamGetDto> GetAnaParamDetails(int VenueNo, int VenueBranchNo, int Analyzerno, int Sampleno);
        List<FetchAnaParamDto> FetchAnalyzerParamDetails(int VenueNo, int VenueBranchNo, int Analyzerno, int Sampleno);
    }
}
