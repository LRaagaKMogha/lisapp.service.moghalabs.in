using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IAnalyzerMasterRepository
    {
       // List<GetAnalyzerMasterResponse> GetAnalyzerMasterDetails(GetAnalyzerMasterRequest getRequest);

        List<TblAnalyzer> GetAnalyzerMasterDetails(GetCommonMasterRequest getanalyzer);

        TblAnalyzerdata InsertAnalyzerDetails(TblAnalyzerresponse TblAnalyzerresponse);
        AnaParamDtoResponse InsertAnaParam(AnaParamDto AnaParamobj);
        List<AnaParamGetDto> GetAnaParamDetails(int VenueNo, int VenueBranchNo, int GetAnaParamDetails, int Analyzerno, int Sampleno);

        // InsertTariffMasterResponse InsertTariffMasterDetails(InsertTariffMasterRequest tariffMasteritem);
        List<TbltestMap> GetAnalVsParamVsTest(testmapRequest testmapRequest);
        analVsparamVstestMap InsertAnalVsParamVsTest(responseTest responseTest);
        List<subresponse> GetSubTest(subrequest subrequest);

    }
}