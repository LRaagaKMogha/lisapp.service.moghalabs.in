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
        List<TbltestMap> GetAnalVsParamVsTest(TestmapRequest TestmapRequest);
        AnalVsparamVstestMap InsertAnalVsParamVsTest(ResponseTest ResponseTest);
        List<Subresponse> GetSubTest(Subrequest Subrequest);
    }
}