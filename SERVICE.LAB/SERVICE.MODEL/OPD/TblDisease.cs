using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class TblDiseaseCategory
    {
        public Int16 DiseaseCategoryNo { get; set; }
        public string DiseaseDescription { get; set; }
        public byte DisSequenceNo { get; set; }
        public bool? DisCatStatus { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int UserNo { get; set; }
        public int PageIndex { get; set; }
    }
    public partial class rtnDiseaseCategory
    {
        public Int16 DiseaseCategoryNo { get; set; }
    }
    public partial class reqDiseaseCategory
    {
        public Int16 DiseaseCategoryNo { get; set; }
        public bool? DisCatStatus { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int updatesSeqNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }

    public partial class lstDiseaseCategory
    {
        public Int16 DiseaseCategoryNo { get; set; }
        public string DiseaseDescription { get; set; }
        public byte DisSequenceNo { get; set; }
        public bool? DisCatStatus { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int updatesSeqNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }

    public partial class TblDiseaseMaster
    {
        public Int16 DiseaseCategoryNo { get; set; }
        public Int16 DiseaseMasterNo { get; set; }
        public string DisDescription { get; set; }
        public string ICDCode { get; set; }
        public Int16 DisMasSequenceNo { get; set; }
        public bool? DisStatus { get; set; }
        public bool? IsConfidential { get; set; }
        public Int16 VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int UserNo { get; set; }
        public int PageIndex { get; set; }
    }
    public partial class rtnDiseaseMaster
    {
        public Int16 DiseaseMasterNo { get; set; }
    }
    public partial class reqDiseaseMaster
    {
        public Int16 DiseaseCategoryNo { get; set; }
        public Int16 DiseaseMasterNo { get; set; }
        public bool? DisVsDrugStatus { get; set; }
        public Int16 VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UpdSeqNumber { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }

    public partial class lstDiseaseMaster
    {
        public Int16 DiseaseCategoryNo { get; set; }
        public Int16 DiseaseMasterNo { get; set; }
        public string DiseaseDescription { get; set; }
        public string DisDescription { get; set; }
        public string ICDCode { get; set; }
        public Int16 DisMasSequenceNo { get; set; }
        public bool? IsConfidential { get; set; }
        public bool? DisStatus { get; set; }
        public Int16 VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int updSeqNumber { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class lstDiseaseTemplateList
    {
        public int templateNo { get; set; }
        public int tempdiseaseNo { get; set; }
        public string? templateName { get; set; }
        public string? templateText { get; set; }
        public bool isDefault { get; set; }
        public int sequenceNo { get; set; }
        public bool status { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
    }
    public partial class templateresponse
    {
        public int templateNo { get; set; }
    }
    public partial class reqresponse
    {
        public string? templateText { get; set; }
    }
    public partial class DiseaseVsProductMapping
    {
        public int DiseaseVsProductMappingNo { get; set; }
        public Int16 DiseaseMasterNo { get; set; }
        public int ProductMasterNo { get; set; }
        public string DiseaseName { get; set; }
        public string DrugName { get; set; }
        public bool? disVsDrugStatus { get; set; }
        public Int16 VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class DiseaseVsTestMapping
    {
        public int DiseaseVsTestMappingNo { get; set; }
        public Int16 DiseaseMasterNo { get; set; }
        public int TestNo { get; set; }
        public string DiseaseName { get; set; }
        public string TestName { get; set; }
        public bool? disVsInvStatus { get; set; }
        public Int16 VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class reqDisVsDrugMaster
    {
        public int DiseaseVsProductMappingNo { get; set; }
        public Int16 DiseaseMasterNo { get; set; }
        public int ProductMasterNo { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int UserNo { get; set; }
    }
    public partial class rtnDisVsDrugMaster
    {
        public int DiseaseVsProductMappingNo { get; set; }
    }
    public partial class reqDisVsInvMaster
    {
        public int DiseaseVsTestMappingNo { get; set; }
        public Int16 DiseaseMasterNo { get; set; }
        public int TestNo { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int UserNo { get; set; }
    }
    public partial class rtnDisVsInvMaster
    {
        public int DiseaseVsTestMappingNo { get; set; }
    }
    public partial class MachineMasterDTO
    {
        public int machineNo { get; set; }
        public string machineName { get; set; }
        public int duration { get; set; }
        public bool? status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class reqMachineMaster
    {
        public Int16 machineNo { get; set; }
        public Int16 VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PageIndex { get; set; }
    }
    public partial class reqMachineMasterResponse
    {
        public int machineNo { get; set; }
    }
      
    public partial class InvMachineMasterRequest
    {
        public Int16 machineNo { get; set; }
        public string machineName { get; set; }
        public int duration { get; set; }
        public List<OPDMachineRes> timeslst { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int UserNo { get; set; }
    }
}
