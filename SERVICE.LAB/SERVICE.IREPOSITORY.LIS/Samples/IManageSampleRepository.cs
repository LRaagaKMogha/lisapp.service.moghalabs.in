using DEV.Model;
using DEV.Model.FrontOffice.PatientDue;
using DEV.Model.Sample;
using System;
using System.Collections.Generic;

namespace Dev.IRepository
{
    public interface IManageSampleRepository
    {
        List<GetManagesampleResponse> GetManageSampleDetails(CommonFilterRequestDTO RequestItem);
        List<CreateManageSampleResponse> CreateManageSample(List<CreateManageSampleRequest> createManageSample);
        List<SampleActionDTO> GetSampleActionDetails(SampleActionRequest SampleActionItem);
        CreateSampleActionResponse CreateSampleACK(List<CreateSampleActionRequest> insertActionDTOs);
        List<ExternalOrderDTO> GetInterfaceTest(List<CreateManageSampleRequest> req);
        int? ACKInterfaceTest(int venueNo, int venueBranchNo, string barcode, string testNo);
        List<GetSampleOutsourceResponse> GetSampleOutSource(GetSampleOutsourceRequest RequestItem);
        Int64 CreateSampleOutsource(List<CreateSampleOutSourceRequest> createOutSource);
        List<GetSampleOutSourceHistory> GetSampleOutsourceHistory(GetSampleOutSourceHistoryRequest req);
        List<GetSampleOutsourceResponse> GetResultACK(GetSampleOutsourceRequest RequestItem);
        CreateOutSourceResponse CreateResultACK(List<CreateSampleOutSourceRequest> createOutSource);
        List<GetSampleTransferResponse> GetSampleTransfer(GetSampleOutsourceRequest RequestItem);
        List<GetbranchSampleReceiveResponse> GetBranchSampleReceive(GetBranchSampleReceiveRequest RequestItem);
        Int64 CreateSampleTransfer(List<CreateSampleTransterRequest> createSampleTransterRequests);
        Int64 CreateBranchSampleReceive(List<CreateBranchSampleReceiveRequest> createBranchReceive);
        List<GetMultiplsSampleResponse> GetMultiplsSampleByTestId(GetMultiplsSampleRequest req);
        List<SampleReportResponse> GetSampleTransferReport(CommonFilterRequestDTO RequestItem);
        bool ValidateBarcodeNo(string BarcodeNo, int venueNo, int venueBranchNo);
        UpdateRefRangeResponse UpdateMultiSampleRefRange(UpdateRefRangeRequest req);
        List<SearchBarcodeResponse> SearchByBarcode(RequestCommonSearch req);
        SpecimenMappingResponse InsertSpecimenMappingResult(RequestSpecimenMedia requestitem);
        SpecimenMappingoutput GetSpecimenMappingResult(int venueno, int venuebranchno, int SpecimenNo, int type = 0);
        GetManageOptionalResponse ManageOptionalTestPackage(CreateManageOptionalTestRequest request);
        List<BarcodePrintResponse> GetBarcodePrintDetails(BarcodePrintRequest RequestItem);
        List<SearchBranchSampleBarcodeResponse> SearchBranchSampleByBarcode(requestCommonSearch req);
        List<BranchSampleActionDTO> GetBranchSampleActionDetails(SampleActionRequest req);
        List<PrePrintBarcodeOrderResponse> GetPrePrintBarcodelist(long visitNo, int VenueNo, int VenueBranchNo);
    }
}




