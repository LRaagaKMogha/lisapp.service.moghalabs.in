using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IAnalyzerMasterRepository
    {
        List<TblAnalyzer> GetAnalyzerMasterDetails(GetCommonMasterRequest getanalyzer);
        TblAnalyzerdata InsertAnalyzerDetails(TblAnalyzerresponse TblAnalyzerresponse);
        AnaParamDtoResponse InsertAnaParam(AnaParamDto AnaParamobj);
        List<AnaParamGetDto> GetAnaParamDetails(int VenueNo, int VenueBranchNo, int GetAnaParamDetails, int Analyzerno, int Sampleno);
        List<TbltestMap> GetAnalVsParamVsTest(testmapRequest testmapRequest);
        analVsparamVstestMap InsertAnalVsParamVsTest(responseTest responseTest);
        List<subresponse> GetSubTest(subrequest subrequest);
    }
}