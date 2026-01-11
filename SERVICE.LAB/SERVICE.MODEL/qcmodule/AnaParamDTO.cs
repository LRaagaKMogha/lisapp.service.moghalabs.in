using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class AnaParamDto
    {
        public Int16 AnalyzerParamNo { get; set; }
        public Int16 AnalyzerMasterNo { get; set; }
        public string Description { get; set; }
        public Int16 SequenceNo { get; set; }
        public int SampleNo { get; set; }
        public bool Status { get; set; }
        public Int16 VenueNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int? venuebranchno { get; set; }
    }

    public class AnaParamDtoResponse
    {
        public Int16 AnalyzerMasterNo { get; set; }
    }

    public class AnaParamGetDto
    {
        public Int16 AnalyzerParamNo { get; set; }
        public string AnalyserName { get; set; }
        public string AssetCode { get; set; }
        public string ParameterCode { get; set; }
        public string SampleName { get; set; }
        public bool Status { get; set; }
        public Int16 VenueNo { get; set; }
        public Int16 AnalyzerMasterNo { get; set; }
        public int SampleNo { get; set; }
    }

    public class FetchAnaParamDto
    {
        public Int16 AnalyzerParamNo { get; set; }
        public string AnalyserName { get; set; }
        public string AssetCode { get; set; }
        public string ParameterCode { get; set; }
        public string SampleName { get; set; }
        public bool Status { get; set; }
        public Int16 VenueNo { get; set; }
        public Int16 AnalyzerMasterNo { get; set; }
        public int SampleNo { get; set; }
    }

    public partial class TblAnalyzerdata
    {
        public Int16 analyzerMasterNo { get; set; }
    }

    public partial class TblAnalyzerresponse
    {
        public Int16 analyzerMasterNo { get; set; }
        public string serialNo { get; set; }
        public string? description { get; set; }
        public string? assetCode { get; set; }
        public int venuebranchNo { get; set; }
        public bool? status { get; set; }
        public Int16 venueNo { get; set; }
        public int userNo { get; set; }
        public double PerUnitConsumption { get; set; }
    }
    public partial class testmapRequest
    {
        public int analyzerparamTestNo { get; set; }
        public Int16 venueNo { get; set; }
        public int branchNo { get; set; }
        public Int16 analyzerMasterNo { get; set; }
        public Int16 analyzerParamNo { get; set; }
        public int testNo { get; set; }
        public int subtestNo { get; set; }
        public int pageIndex { get; set; }
    }

    public partial class TbltestMap
    {
        public int analyzerparamTestNo { get; set; }
        public Int16 analyzerMasterNo { get; set; }
        public Int16 analyzerParamNo { get; set; }
        public int testNo { get; set; }
        public int subtestNo { get; set; }
        public string? testName { get; set; }
        public string? subtestName { get; set; }
        public string? analyzerName { get; set; }
        public string? paramName { get; set; }
        public Int16 venueNo { get; set; }
        public int userNo { get; set; }
        public bool? tstatus { get; set; }
        public int unitNo { get; set; }
        public int methodNo { get; set; }
        public int perUnitConsumption { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
        public string branchName { get; set; }
        public int VenueBranchNo { get; set; }
        public string ReagentName { get; set; }
        public string UnitName { get; set; }
    }
    public partial class responseTest
    {
        public int analyzerparamTestNo { get; set; }
        public Int16 analyzerMasterNo { get; set; }
        public Int16 analyzerParamNo { get; set; }
        public int testNo { get; set; }
        public int subtestNo { get; set; }
        public Int16 venueNo { get; set; }
        public int venuebranchno { get; set; }
        public int userNo { get; set; }
        public bool? tstatus { get; set; }
        public int unitNo { get; set; }
        public int methodNo { get; set; }
        public int perUnitConsumption { get; set; }
        public string branchName { get; set; }
        public string ReagentName { get; set; }
        public string UnitName { get; set; }
    }
    public partial class analVsparamVstestMap
    {
        public int analyzerparamTestNo { get; set; }
    }
    public partial class subrequest
    {
        public int venueNo { get; set; }
        public int testNo { get; set; }
    }
    public partial class subresponse
    {
        public int RowNo { get; set; }
        public string? subtestName { get; set; }
        public int subtestNo { get; set; }
        public int testNo { get; set; }
    }
}