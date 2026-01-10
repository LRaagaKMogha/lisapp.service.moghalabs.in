using Dev.IRepository;
using DEV.Common;
using DEV.Model.EF;
using DEV.Model.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Serilog;
using DEV.Model;

namespace Dev.Repository
{
    public class ManageSampleRepository : IManageSampleRepository
    {

        private IConfiguration _config;
        public ManageSampleRepository(IConfiguration config) { _config = config; }

        public List<GetManagesampleResponse> GetManageSampleDetails(CommonFilterRequestDTO RequestItem)
        {
            List<GetManageSampleDTO> objresult1 = new List<GetManageSampleDTO>();
            List<GetManagesampleResponse> lstManagesampleResponses = new List<GetManagesampleResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FromDate", RequestItem?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem?.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem?.visitNo);
                    var _RefferalType = new SqlParameter("RefferalType", RequestItem?.refferalType);
                    var _CustomerNo = new SqlParameter("CustomerNo", RequestItem?.CustomerNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem?.physicianNo);
                    var _PageIndex = new SqlParameter("PageIndex", RequestItem?.pageIndex);
                    var _PageCode = new SqlParameter("PageCode", RequestItem?.PageCode);
                    var _OrderStatus = new SqlParameter("OrderStatus", RequestItem?.orderStatus);
                    var _RouteNo = new SqlParameter("RouteNo", RequestItem?.routeNo);
                    var _FranchiseNo = new SqlParameter("FranchiseNo", RequestItem?.FranchiseNo);
                    var _specimenQty = new SqlParameter("specimenQty", 1);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem?.PatientNo);

                    var objresult = context.GetManageSampleDTO.FromSqlRaw(
                    "Execute dbo.Pro_Getmanagesample @FromDate, @ToDate, @Type, @VenueNo, @VenueBranchNo, @VisitNo, @RefferalType, @CustomerNo, @PhysicianNo, @PageIndex, @PageCode, @OrderStatus, @RouteNo, @FranchiseNo, @specimenQty, @PatientNo",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _VisitNo, _RefferalType, _CustomerNo, _PhysicianNo, _PageIndex, _PageCode, _OrderStatus, _RouteNo, _FranchiseNo, _specimenQty, _PatientNo).ToList();

                    int oldVisitNo = 0;
                    int newVisitNO = 0;
                    int oldSampleNo = 0;
                    int newSampleNo = 0;
                    int oldContainerNo = 0;
                    int newContainerNo = 0;
                    int oldTestNo = 0;
                    int newTestNo = 0;

                    foreach (var obj in objresult)
                    {
                        GetManagesampleResponse getManagesampleResponse = new GetManagesampleResponse();
                        List<SampleDetails> lstSample = new List<SampleDetails>();
                        List<TestDetails> lstTestDetails = new List<TestDetails>();

                        newVisitNO = obj.VisitNo;
                        var sampleDetailsById = objresult.Where(x => x.VisitNo == newVisitNO).Select(x => new { x.SampleName, x.SampleNo, x.OldSampleNo, x.ContainerName, x.ContainerNo, x.OldContainerNo, x.SampleCollectedDate, x.collectatsource, x.specimenQty, x.fastingOrNonfasting, x.BarcodeNo }).ToList().Distinct();

                        if (newVisitNO != oldVisitNo)
                        {
                            getManagesampleResponse.Age = obj.Age;
                            getManagesampleResponse.PatientName = obj.PatientName;
                            getManagesampleResponse.PatientNo = obj.PatientNo;
                            getManagesampleResponse.PrimaryId = obj.PrimaryId;
                            getManagesampleResponse.RefferedBy = obj.RefferedBy;
                            getManagesampleResponse.RegistrationDate = obj.RegistrationDate;
                            getManagesampleResponse.VenueBranchNo = obj.VenueBranchNo;
                            getManagesampleResponse.VenueNo = obj.VenueNo;
                            getManagesampleResponse.VisitId = obj.VisitId;
                            getManagesampleResponse.VisitNo = obj.VisitNo;
                            getManagesampleResponse.SelectPatient = false;
                            getManagesampleResponse.sampleDetails = lstSample;
                            getManagesampleResponse.TotalRecords = obj.TotalRecords;
                            getManagesampleResponse.PageIndex = obj.PageIndex;
                            getManagesampleResponse.IsStat = obj.IsStat;
                            getManagesampleResponse.TATFlag = obj.TATFlag;
                            getManagesampleResponse.IsRejected = obj.IsRejected;
                            getManagesampleResponse.IncludeInstruction = obj.IncludeInstruction;
                            getManagesampleResponse.IsIncludeInstruction = obj.IsincludeInstruction;
                            getManagesampleResponse.IDnumber = obj.IDnumber;
                            getManagesampleResponse.IsVipIndication = obj.IsVipIndication;
                           
                            oldVisitNo = obj.VisitNo;
                            oldSampleNo = 0;
                            oldContainerNo = 0;
                            
                            foreach (var sample in sampleDetailsById)
                            {
                                lstTestDetails = new List<TestDetails>();
                                newSampleNo = sample.SampleNo;
                                newContainerNo = sample.ContainerNo;
                               
                                if ((oldSampleNo != newSampleNo) || (oldContainerNo != newContainerNo))
                                {
                                    SampleDetails sampleDetails = new SampleDetails()
                                    {
                                        BarcodeNo = sample.BarcodeNo,
                                        ContainerName = sample.ContainerName,
                                        ContainerNo = sample.ContainerNo,
                                        OldContainerNo = sample.OldContainerNo,
                                        SampleCollectedDate = sample.SampleCollectedDate,
                                        SampleName = sample.SampleName,
                                        SampleNo = sample.SampleNo,
                                        OldSampleNo = sample.OldSampleNo,
                                        SelectSample = false,
                                        testDetails = lstTestDetails,
                                        collectatsource = sample.collectatsource,
                                        specimenQty = sample.specimenQty,
                                        fastingOrNonfasting = sample.fastingOrNonfasting
                                    };
                                    oldSampleNo = newSampleNo;
                                    oldContainerNo = newContainerNo;
                                    lstSample.Add(sampleDetails);

                                    var testDetailsById = objresult.Where(x => x.VisitNo == newVisitNO && x.SampleNo == newSampleNo && x.ContainerNo == sample.ContainerNo)//same sample with different container name, should be separate
                                        .Select(x => new { x.TestName, x.TestNo, x.SampleNo, x.OrderCode, x.OrderListNo, x.ServiceNo, x.OrdersNo, x.OrderType, x.IsSelectMultiSample, x.multiSampleTestno }).ToList();
                                    oldTestNo = 0;
                                   
                                    foreach (var test in testDetailsById)
                                    {
                                        List<MultiSampleList> lstMultiSample = new List<MultiSampleList>();
                                        //get the multi sample list
                                        if (test.IsSelectMultiSample)
                                        {
                                            GetMultiplsSampleRequest objSampRequest = new GetMultiplsSampleRequest();
                                            objSampRequest.TestNo = test.TestNo;
                                            objSampRequest.VenueNo = RequestItem.VenueNo;
                                            objSampRequest.VenueBranchNo = RequestItem.VenueBranchNo;
                                            objSampRequest.Type = test.OrderType;
                                            List<GetMultiplsSampleResponse> lstSampResponse = new List<GetMultiplsSampleResponse>();
                                            lstSampResponse = GetMultiplsSampleByTestId(objSampRequest);
                                            foreach (var multiSample in lstSampResponse)
                                            {
                                                MultiSampleList objRes = new MultiSampleList();
                                                string containernam = multiSample.ContainerName != null && multiSample.ContainerName != "" ? multiSample.ContainerName : "-";
                                                string samplenam = multiSample.SampleName != null ? multiSample.SampleName : "";
                                                string samplecontanrname = samplenam + '/' + containernam;
                                                objRes.SampleNo = multiSample.SampleNo;
                                                objRes.SampleName = samplecontanrname;// multiSample.SampleName;
                                                objRes.ContainerNo = multiSample.ContainerNo;
                                                objRes.ContainerName = multiSample.ContainerName;
                                                objRes.MethodNo = multiSample.MethodNo;
                                                objRes.MethodName = multiSample.MethodName;
                                                lstMultiSample.Add(objRes);
                                            }
                                        }
                                        else
                                        {
                                            lstMultiSample = new List<MultiSampleList>();
                                        }

                                        newTestNo = test.TestNo;
                                        if (oldTestNo != newTestNo)
                                        {
                                            TestDetails testDetails = new TestDetails()
                                            {
                                                TestName = test.TestName,
                                                TestNo = test.TestNo,
                                                OrderCode = test.OrderCode,
                                                OrderListNo = test.OrderListNo,
                                                OrdersNo = test.OrdersNo,
                                                OrderType = test.OrderType,
                                                ServiceNo = test.ServiceNo,
                                                lstMultiSamples = lstMultiSample
                                            };
                                            oldTestNo = newTestNo;
                                            lstTestDetails.Add(testDetails);
                                        }
                                    }

                                    getManagesampleResponse.sampleDetails = lstSample;
                                }
                            }

                            lstManagesampleResponses.Add(getManagesampleResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetManageSampleDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, RequestItem?.userNo);
            }
            return lstManagesampleResponses;
        }

        public GetManageOptionalResponse ManageOptionalTestPackage(CreateManageOptionalTestRequest request)
        {
            GetManageOptionalResponse getManageOptionalResponse = new GetManageOptionalResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PackageCode = new SqlParameter("@PackageCode", request.PackageCode);
                    var _TestCode = new SqlParameter("@TestCode", request.TestCodes);
                    var _PatientVisitNo = new SqlParameter("@PatientVisitNo", request.PatientVisitNo);
                    var _IsEditBill = new SqlParameter("@IsEditBill", request.IsEditBill);

                    var response = context.ManageOptionalTestPackage.FromSqlRaw(
                    "Execute dbo.Pro_ManageOptionalTest @PackageCode,@TestCode,@PatientVisitNo,@IsEditBill",
                    _PackageCode, _TestCode, _PatientVisitNo, _IsEditBill).ToList() ;
                    
                    getManageOptionalResponse = response.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.ManageOptionalTestinPackage" + request.PatientVisitNo, ExceptionPriority.Low, ApplicationType.REPOSITORY, request.VenueNo, request.VenueBranchNo, 0);
            }
            return getManageOptionalResponse;
        }

        public List<GetMultiplsSampleResponse> GetMultiplsSampleByTestId(GetMultiplsSampleRequest req)
        {
            List<GetMultiplsSampleResponse> lstMulti = new List<GetMultiplsSampleResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _testno = new SqlParameter("TestNo", req.TestNo);
                    var _type = new SqlParameter("Type", req.Type);
                    var _venueno = new SqlParameter("VenueNo", req.VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", req.VenueBranchNo);

                    lstMulti = context.GetMultiplsSampleByTestId.FromSqlRaw(
                    "Execute dbo.Pro_GetMultiSampleByTestId @Type,@VenueNo,@VenueBranchNo,@TestNo",
                    _type, _venueno, _venuebranchno, _testno).ToList(); ;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetMultiplsSampleByTestId" + req.TestNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req?.VenueNo, req?.VenueBranchNo, 0);
            }
            return lstMulti;
        }
        public List<CreateManageSampleResponse> CreateManageSample(List<CreateManageSampleRequest> createManageSample)
        {
            List<CreateManageSampleResponse> response = new List<CreateManageSampleResponse>();
            CommonHelper commonUtility = new CommonHelper();
            string strXML = commonUtility.ToXML(createManageSample);

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SampleXML = new SqlParameter("SampleXML", strXML);
                    var _CreatedBy = new SqlParameter("CreatedBy", createManageSample?.FirstOrDefault()?.collectedBy);
                    var _UserNo = new SqlParameter("UserNo", createManageSample?.FirstOrDefault()?.userNo);
                    var _TenantID = new SqlParameter("VenueNo", createManageSample?.FirstOrDefault()?.venueNo);
                    var _pagecode = new SqlParameter("pagecode", createManageSample?.FirstOrDefault()?.pagecode);
                    var _TenantBranchID = new SqlParameter("VenueBranchNo", createManageSample?.FirstOrDefault()?.venueBranchNo);

                    response = context.CreateManageSamples.FromSqlRaw(
                    "Execute dbo.Pro_InsertSamples @SampleXML, @CreatedBy, @VenueBranchNo, @VenueNo, @UserNo, @pagecode",
                    _SampleXML, _CreatedBy, _UserNo, _TenantID, _TenantBranchID, _pagecode).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.CreateManageSample", ExceptionPriority.High, ApplicationType.REPOSITORY, createManageSample?.FirstOrDefault()?.venueNo, createManageSample?.FirstOrDefault()?.venueBranchNo, createManageSample?.FirstOrDefault()?.userNo);
            }
            return response;
        }

        //Update Ref Range, changed smaple no/container no/method no
        public UpdateRefRangeResponse UpdateMultiSampleRefRange(UpdateRefRangeRequest req)
        {
            UpdateRefRangeResponse response = new UpdateRefRangeResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VisitNo = new SqlParameter("VisitNo", req?.VisitNo);
                    var _PatientNo = new SqlParameter("PatientNo", req?.PatientNo);
                    var _OrdersNo = new SqlParameter("OrdersNo", req?.OrdersNo);
                    var _OrderlistNo = new SqlParameter("OrderlistNo", req?.OrderlistNo);
                    var _TestNo = new SqlParameter("TestNo", req?.TestNo);
                    var _MethodNo = new SqlParameter("MethodNo", req?.MethodNo);
                    var _ContainerNo = new SqlParameter("ContainerNo", req?.ContainerNo);
                    var _SampleNo = new SqlParameter("SampleNo", req?.SampleNo);
                    var _UserNo = new SqlParameter("UserNo", req?.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);

                    response = context.UpdateMultiSampleRefRange.FromSqlRaw(
                    "Execute dbo.pro_UpdateMultiSampleRefRange @visitNo,@patientNo,@ordersNo,@orderlistNo,@testNo,@methodNo,@containerNo,@sampleNo,@venueBranchNo,@venueNo,@userNo",
                    _VisitNo, _PatientNo, _OrdersNo, _OrderlistNo, _TestNo, _MethodNo, _ContainerNo, _SampleNo, _VenueBranchNo, _VenueNo, _UserNo).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.UpdateMultiSampleRefRange", ExceptionPriority.High, ApplicationType.REPOSITORY, req?.VenueNo, req?.VenueBranchNo, req?.UserNo);
            }
            return response;
        }

        public List<SampleActionDTO> GetSampleActionDetails(SampleActionRequest req)
        {
            List<SampleActionDTO> objresult = new List<SampleActionDTO>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PageCode = new SqlParameter("PageCode", req?.PageCode);
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", req?.PageIndex);
                    var _Type = new SqlParameter("Type", req?.Type);
                    var _FromDate = new SqlParameter("FromDate", req?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", req?.ToDate);
                    var _deptno = new SqlParameter("deptno", req?.DeptNo);
                    var _Searchkey = new SqlParameter("Searchkey", req?.Searchkey.ValidateEmpty());
                    var _UserNo = new SqlParameter("userNo", req?.UserNo);
                    var _VisiNo = new SqlParameter("VisitNo", req?.VisitNo);
                    var _PatientNo = new SqlParameter("PatientNo", req?.PatientNo);

                    objresult = context.SampleActionDTO.FromSqlRaw(
                    "Execute dbo.pro_GetSampleActionDetails @PageCode,@VenueNo,@VenueBranchNo,@PageIndex,@Type,@FromDate,@ToDate,@DeptNo,@Searchkey,@userNo,@VisitNo,@PatientNo",
                    _PageCode, _VenueNo, _VenueBranchNo, _PageIndex, _Type, _FromDate, _ToDate, _deptno, _Searchkey, _UserNo, _VisiNo, _PatientNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetSampleActionDetails", ExceptionPriority.Medium, ApplicationType.REPOSITORY, req?.VenueNo, req?.VenueBranchNo, 0);
            }
            return objresult;
        }

        public CreateSampleActionResponse CreateSampleACK(List<CreateSampleActionRequest> insertActionDTOs)
        {

            List<CreateSampleActionResponse> response = new List<CreateSampleActionResponse>();
            CommonHelper commonUtility = new CommonHelper();
            string strXML = commonUtility.ToXML(insertActionDTOs);

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SampleXML = new SqlParameter("SampleXML", strXML);
                    var _CreatedBy = new SqlParameter("CreatedBy", insertActionDTOs?.FirstOrDefault()?.userNo);
                    var _UserNo = new SqlParameter("UserNo", insertActionDTOs?.FirstOrDefault()?.userNo);
                    var _TenantID = new SqlParameter("VenueNo", insertActionDTOs?.FirstOrDefault()?.VenueNo);
                    var _TenantBranchID = new SqlParameter("VenueBranchNo", insertActionDTOs?.FirstOrDefault()?.VenueBranchNo);
                    
                    response = context.InsertSamplesAcknowledgement.FromSqlRaw(
                    "Execute dbo.Pro_InsertSamplesAcknowledgement @SampleXML,@CreatedBy,@VenueBranchNo,@VenueNo,@UserNo",
                    _SampleXML, _CreatedBy, _UserNo, _TenantID, _TenantBranchID).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.CreateSampleACK", ExceptionPriority.Medium, ApplicationType.REPOSITORY, insertActionDTOs?.FirstOrDefault()?.VenueNo, insertActionDTOs?.FirstOrDefault()?.VenueBranchNo, 0);
            }
            return response?.FirstOrDefault();
        }

        public List<ExternalOrderDTO> GetInterfaceTest(List<CreateManageSampleRequest> req)
        {
            List<ExternalOrderDTO> lst = new List<ExternalOrderDTO>();
            CommonHelper commonUtility = new CommonHelper();
            string strXML = commonUtility.ToXML(req);

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SampleXML = new SqlParameter("SampleXML", strXML);
                    var _UserNo = new SqlParameter("UserNo", req?.FirstOrDefault()?.userNo);
                    var _TenantID = new SqlParameter("VenueNo", req?.FirstOrDefault()?.venueNo);
                    var _TenantBranchID = new SqlParameter("VenueBranchNo", req?.FirstOrDefault()?.venueBranchNo);
                    
                    lst = context.GetInterfaceTest.FromSqlRaw(
                    "Execute dbo.Pro_GetExternalOrder @SampleXML,@VenueBranchNo,@VenueNo,@UserNo",
                    _SampleXML, _UserNo, _TenantID, _TenantBranchID).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetInterfaceTest", ExceptionPriority.High, ApplicationType.REPOSITORY, req?.FirstOrDefault()?.venueNo, req?.FirstOrDefault()?.venueBranchNo, 0);
            }
            return lst;
        }

        public int? ACKInterfaceTest(int venueNo, int venueBranchNo, string barcode, string testNo)
        {
            Log.Information(string.Format("ACKInterfaceTest (Repo)- Inputresults - VenueNo - {0} , VenueBranchNo - {1} , barcode - {2} , testno - {3}", venueNo, venueBranchNo, barcode, testNo));
            CommonHelper commonUtility = new CommonHelper();
            int? result = 0;

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Barcode = new SqlParameter("Barcode", barcode);
                    var _TestNo = new SqlParameter("TestNo", testNo);
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    
                    var response = context.AckInterface.FromSqlRaw(
                    "Execute dbo.Pro_ACKInterfaceTest @Barcode,@TestNo,@VenueNo,@VenueBranchNo",
                    _Barcode, _TestNo, _VenueNo, _VenueBranchNo).ToList();

                    result = response?.FirstOrDefault()?.Status;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.ACKInterfaceTest", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, 0);
            }
            return result;
        }

        public List<GetSampleOutsourceResponse> GetSampleOutSource(GetSampleOutsourceRequest RequestItem)
        {
            List<GetSampleOutsourceResponse> lstSampleOutSource = new List<GetSampleOutsourceResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem?.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem?.VisitNo);
                    var _departmentNo = new SqlParameter("DepartmentNo", RequestItem?.DepartmentNo);
                    var _serviceNo = new SqlParameter("ServiceNo", RequestItem?.ServiceNo);
                    var _serviceType = new SqlParameter("ServiceType", RequestItem?.ServiceType);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem?.pageIndex);
                    var _vendor = new SqlParameter("vendorno", RequestItem?.VendorNo);
                    var _patientNo = new SqlParameter("PatientNo", RequestItem?.PatientNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem?.PhysicianNo);

                    lstSampleOutSource = context.GetSampleOutSourceDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetSampleOutSource @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@VisitNo,@DepartmentNo,@ServiceNo,@ServiceType,@PageIndex,@vendorNo,@PatientNo,@PhysicianNo",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _VisitNo, _departmentNo, _serviceNo, _serviceType, _pageIndex, _vendor, _patientNo, _PhysicianNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetSampleOutSource", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, 0);
            }
            return lstSampleOutSource;
        }

        public Int64 CreateSampleOutsource(List<CreateSampleOutSourceRequest> createOutSource)
        {

            List<CreateOutSourceResponse> response = new List<CreateOutSourceResponse>();
            CommonHelper commonUtility = new CommonHelper();
            string strXML = commonUtility.ToXML(createOutSource);

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SampleXML = new SqlParameter("SampleXML", strXML);
                    var _CreatedBy = new SqlParameter("CreatedBy", createOutSource?.FirstOrDefault()?.userNo);
                    var _UserNo = new SqlParameter("UserNo", createOutSource?.FirstOrDefault()?.userNo);
                    var _TenantID = new SqlParameter("VenueNo", createOutSource?.FirstOrDefault()?.venueNo);
                    var _TenantBranchID = new SqlParameter("VenueBranchNo", createOutSource?.FirstOrDefault()?.venueBranchNo);
                    
                    response = context.CreateSampleOutSource.FromSqlRaw(
                    "Execute dbo.Pro_InsertSampleOutSource @SampleXML,@CreatedBy,@VenueBranchNo,@VenueNo,@UserNo",
                    _SampleXML, _CreatedBy, _UserNo, _TenantID, _TenantBranchID).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.CreateSampleOutsource", ExceptionPriority.High, ApplicationType.REPOSITORY, createOutSource?.FirstOrDefault()?.venueNo, createOutSource?.FirstOrDefault()?.venueBranchNo, 0);
            }
            return response?.FirstOrDefault()?.resultStatus ?? 0;
        }

        public List<GetSampleOutSourceHistory> GetSampleOutsourceHistory(GetSampleOutSourceHistoryRequest req)
        {
            List<GetSampleOutSourceHistory> lst = new List<GetSampleOutSourceHistory>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", req?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", req?.ToDate);
                    var _Type = new SqlParameter("Type", req?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);
                    var _VendorNo = new SqlParameter("vendorNo", req?.VendorNo);
                    var _pageIndex = new SqlParameter("PageIndex", req?.pageIndex);
                    
                    lst = context.GetSampleOutSourceHistoryDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetSampleOutSourceHistory @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@vendorNo,@PageIndex",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _VendorNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetSampleOutSourceHistoryRepository", ExceptionPriority.High, ApplicationType.REPOSITORY, req?.VenueNo, req?.VenueBranchNo, 0);
            }
            return lst;
        }

        public List<GetSampleOutsourceResponse> GetResultACK(GetSampleOutsourceRequest RequestItem)
        {
            List<GetSampleOutsourceResponse> lstResultACK = new List<GetSampleOutsourceResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem?.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem?.VisitNo);
                    var _departmentNo = new SqlParameter("DepartmentNo", RequestItem?.DepartmentNo);
                    var _serviceNo = new SqlParameter("ServiceNo", RequestItem?.ServiceNo);
                    var _serviceType = new SqlParameter("ServiceType", RequestItem?.ServiceType);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem?.pageIndex);
                    var _vendorNo = new SqlParameter("VendorNo", RequestItem?.VendorNo);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem?.PatientNo);

                    lstResultACK = context.GetSampleOutSourceDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetResultACK @FROMDate, @ToDate, @Type, @VenueNo, @VenueBranchNo, @VisitNo, @DepartmentNo, @ServiceNo, @ServiceType, @PageIndex, @VendorNo,@PatientNo",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _VisitNo, _departmentNo, _serviceNo, _serviceType, _pageIndex, _vendorNo, _PatientNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetResultACK", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, 0);
            }
            return lstResultACK;
        }

        public CreateOutSourceResponse CreateResultACK(List<CreateSampleOutSourceRequest> createOutSource)
        {
            CreateOutSourceResponse response = new CreateOutSourceResponse();
            CommonHelper commonUtility = new CommonHelper();
            string strXML = commonUtility.ToXML(createOutSource);

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SampleXML = new SqlParameter("SampleXML", strXML);
                    var _CreatedBy = new SqlParameter("CreatedBy", createOutSource?.FirstOrDefault()?.userNo);
                    var _UserNo = new SqlParameter("UserNo", createOutSource?.FirstOrDefault()?.userNo);
                    var _TenantID = new SqlParameter("VenueNo", createOutSource?.FirstOrDefault()?.venueNo);
                    var _TenantBranchID = new SqlParameter("VenueBranchNo", createOutSource?.FirstOrDefault()?.venueBranchNo);

                    var response1 = context.CreateResultACK.FromSqlRaw(
                    "Execute dbo.Pro_InsertResultACK @SampleXML, @CreatedBy, @VenueBranchNo, @VenueNo, @UserNo",
                    _SampleXML, _CreatedBy, _UserNo, _TenantID, _TenantBranchID).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.CreateResultACK", ExceptionPriority.High, ApplicationType.REPOSITORY, createOutSource?.FirstOrDefault()?.venueNo, createOutSource?.FirstOrDefault()?.venueNo, 0);
            }
            return response;
        }

        public List<GetSampleTransferResponse> GetSampleTransfer(GetSampleOutsourceRequest RequestItem)
        {
            List<GetSampleTransferResponse> lstSampleTransfer = new List<GetSampleTransferResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem?.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem?.VisitNo);
                    var _departmentNo = new SqlParameter("DepartmentNo", RequestItem?.DepartmentNo);
                    var _serviceNo = new SqlParameter("ServiceNo", RequestItem?.ServiceNo);
                    var _serviceType = new SqlParameter("ServiceType", RequestItem?.ServiceType);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem?.pageIndex);
                    var _pagecount = new SqlParameter("pagecount", RequestItem?.pageCount);
                    
                    lstSampleTransfer = context.GetSampleTransferDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetSampleTransfer @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@VisitNo,@DepartmentNo,@ServiceNo,@ServiceType,@PageIndex,@pagecount",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _VisitNo, _departmentNo, _serviceNo, _serviceType, _pageIndex,_pagecount).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetSampleTransfer", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, 0);
            }
            return lstSampleTransfer;
        }

        public List<GetbranchSampleReceiveResponse> GetBranchSampleReceive(GetBranchSampleReceiveRequest RequestItem)
        {
            List<GetbranchSampleReceiveResponse> lstSampleTransfer = new List<GetbranchSampleReceiveResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem?.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem?.VisitNo);
                    var _departmentNo = new SqlParameter("DepartmentNo", RequestItem?.DepartmentNo);
                    var _serviceNo = new SqlParameter("ServiceNo", RequestItem?.ServiceNo);
                    var _serviceType = new SqlParameter("ServiceType", RequestItem?.ServiceType);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem?.pageIndex);
                    var _transferBranch = new SqlParameter("TransferBranch", RequestItem?.TransferBranchNo);
                    
                    lstSampleTransfer = context.GetbranchSampleReceiveDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetBranchReceive @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@VisitNo,@DepartmentNo,@ServiceNo,@ServiceType,@PageIndex,@TransferBranch",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _VisitNo, _departmentNo, _serviceNo, _serviceType, _pageIndex, _transferBranch).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetBranchSampleReceive", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, 0);
            }
            return lstSampleTransfer;
        }
        public List<SampleReportResponse> GetSampleTransferReport(CommonFilterRequestDTO RequestItem)
        {
            List<SampleReportResponse> lstPatientInfoResponse = new List<SampleReportResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem?.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem?.PatientNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem?.visitNo);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem?.pageIndex);
                    var _TransferBranchNo = new SqlParameter("TransferBranchNo", RequestItem?.transferBranchNo);
                    var _TransferModeNo = new SqlParameter("TransferModeNo", RequestItem?.transferModeNo);

                    lstPatientInfoResponse = context.GetSampleReportResponse.FromSqlRaw(
                    "Execute dbo.Pro_GetSampleTransferReport @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@PatientNo,@VisitNo,@PageIndex,@TransferBranchNo,@TransferModeNo",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _PatientNo, _VisitNo, _pageIndex, _TransferBranchNo, _TransferModeNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetPatientInfoDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, RequestItem?.userNo);
            }
            return lstPatientInfoResponse;
        }
        public Int64 CreateSampleTransfer(List<CreateSampleTransterRequest> createSampleTransterRequests)
        {

            List<CreateSampleTransferResponse> response = new List<CreateSampleTransferResponse>();
            CommonHelper commonUtility = new CommonHelper();
            string strXML = commonUtility.ToXML(createSampleTransterRequests);

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SampleXML = new SqlParameter("SampleXML", strXML);
                    var _CreatedBy = new SqlParameter("CreatedBy", createSampleTransterRequests?.FirstOrDefault()?.userNo);
                    var _UserNo = new SqlParameter("UserNo", createSampleTransterRequests?.FirstOrDefault()?.userNo);
                    var _TenantID = new SqlParameter("VenueNo", createSampleTransterRequests?.FirstOrDefault()?.venueNo);
                    var _TenantBranchID = new SqlParameter("VenueBranchNo", createSampleTransterRequests?.FirstOrDefault()?.venueBranchNo);
                    
                    response = context.CreateSampleTransfer.FromSqlRaw(
                    "Execute dbo.Pro_InsertSampleTransfer @SampleXML,@CreatedBy,@VenueBranchNo,@VenueNo,@UserNo",
                    _SampleXML, _CreatedBy, _UserNo, _TenantID, _TenantBranchID).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.CreateSampleTransfer", ExceptionPriority.High, ApplicationType.REPOSITORY, createSampleTransterRequests?.FirstOrDefault()?.venueNo, createSampleTransterRequests?.FirstOrDefault()?.venueBranchNo, 0); ;
            }
            return response?.FirstOrDefault()?.resultStatus ?? 0;
        }

        public Int64 CreateBranchSampleReceive(List<CreateBranchSampleReceiveRequest> createBranchReceive)
        {

            List<CreateSampleTransferResponse> response = new List<CreateSampleTransferResponse>();
            CommonHelper commonUtility = new CommonHelper();
            string strXML = commonUtility.ToXML(createBranchReceive);

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SampleXML = new SqlParameter("SampleXML", strXML);
                    var _CreatedBy = new SqlParameter("CreatedBy", createBranchReceive?.FirstOrDefault()?.userNo);
                    var _UserNo = new SqlParameter("UserNo", createBranchReceive?.FirstOrDefault()?.userNo);
                    var _TenantID = new SqlParameter("VenueNo", createBranchReceive?.FirstOrDefault()?.venueNo);
                    var _TenantBranchID = new SqlParameter("VenueBranchNo", createBranchReceive?.FirstOrDefault()?.venueBranchNo);
                    
                    response = context.CreateSampleTransfer.FromSqlRaw(
                    "Execute dbo.Pro_InsertBranchReceive @SampleXML,@CreatedBy,@VenueBranchNo,@VenueNo,@UserNo",
                    _SampleXML, _CreatedBy, _UserNo, _TenantID, _TenantBranchID).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.CreateBranchSampleReceive", ExceptionPriority.High, ApplicationType.REPOSITORY, createBranchReceive?.FirstOrDefault()?.venueNo, createBranchReceive?.FirstOrDefault()?.venueBranchNo, 0); ;
            }
            return response?.FirstOrDefault()?.resultStatus ?? 0;
        }
        public bool ValidateBarcodeNo(string BarcodeNo, int venueNo, int venueBranchNo)
        {
            bool response = false;
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _BarcodeNo = new SqlParameter("BarcodeNo", BarcodeNo);
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    
                    var result = context.Barcoderesult.FromSqlRaw(
                    "Execute dbo.Pro_ValidateBarcodeNo @BarcodeNo,@VenueNo,@VenueBranchNo",
                   _BarcodeNo, _VenueNo, _venueBranchNo).FirstOrDefault();
                    
                    response = result?.result ?? false;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.ValidateBarcodeNo", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return response;
        }

        public List<SearchBarcodeResponse> SearchByBarcode(RequestCommonSearch req)
        {
            List<SearchBarcodeResponse> lst = new List<SearchBarcodeResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req.viewvenuebranchno);
                    var _searchby = new SqlParameter("searchby", req.searchby);
                    var _searchtext = new SqlParameter("searchtext", req.searchtext);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    
                    lst = context.SearchByBarcode.FromSqlRaw(
                    "Execute dbo.pro_SearchByBarcode @pagecode,@viewvenuebranchno,@searchby,@searchtext,@venueno,@venuebranchno,@userno",
                    _pagecode, _viewvenuebranchno, _searchby, _searchtext, _venueno, _venuebranchno, _userno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.SearchByBarcode", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return lst;
        }
        
        public SpecimenMappingoutput GetSpecimenMappingResult(int venueno, int venuebranchno, int SpecimenNo,int type=0)
        {
            SpecimenMappingoutput lst = new SpecimenMappingoutput();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueNo", venueno);
                    var _venuebranchno = new SqlParameter("venueBranchNo", venuebranchno);
                    var _SpecimenNo = new SqlParameter("SpecimenNo", SpecimenNo);
                    var _type = new SqlParameter("type", type);
                    
                    lst = context.SpecimenMappingoutput.FromSqlRaw(
                    "Execute dbo.Pro_GetSpecimenMediaMapping @venueNo,@venueBranchNo,@SpecimenNo,@type",
                    _venueno, _venuebranchno, _SpecimenNo, _type).AsEnumerable()?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetSpecimenMappingResult", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return lst;
        }
        public SpecimenMappingResponse InsertSpecimenMappingResult(RequestSpecimenMedia requestitem)
        {
            SpecimenMappingResponse lst = new SpecimenMappingResponse();
            try
            {
                string MediaIds = "";
                var media = requestitem.lstmedia.Where(x => x.isDefault == true).Select(x => x.commonNo).ToList();
                if (media.Count > 0)
                {
                    MediaIds = string.Join(",", media);
                }
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueNo", requestitem.venueno);
                    var _venuebranchno = new SqlParameter("venueBranchNo", requestitem.venuebranchno);
                    var _Userno = new SqlParameter("Userno", requestitem.Userno);
                    var _SpecimenNo = new SqlParameter("SpecimenNo", requestitem.SpecimenNo);
                    var _MediaIds = new SqlParameter("MediaIds", MediaIds);
                    
                    lst = context.SpecimenMappingResponse.FromSqlRaw(
                    "Execute dbo.Pro_InsertSpecimenMediaMapping @venueNo,@venueBranchNo,@Userno,@SpecimenNo,@MediaIds",
                    _venueno, _venuebranchno, _Userno, _SpecimenNo, _MediaIds).AsEnumerable().FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.InsertSpecimenMappingResult", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return lst;
        }

        public List<BarcodePrintResponse> GetBarcodePrintDetails(BarcodePrintRequest request)
        {
            List<BarcodePrintResponse> objResponse = new List<BarcodePrintResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _RequestType = new SqlParameter("@RequestType", request.RequestType);
                    var _PtVisitNo = new SqlParameter("@PtVisitNo", request.PtVisitNo);
                    var _OrderListNo = new SqlParameter("@OrderListNo", request.OrderListNo);
                    var _ServiceNo = new SqlParameter("@ServiceNo", request.ServiceNo);
                    var _ServiceType = new SqlParameter("@ServiceType", request.ServiceType);
                    var _SampleNo = new SqlParameter("@SampleNo", request.SampleNo);
                    var _BarcodeNo = new SqlParameter("@BarcodeNo", request.BarcodeNo);
                    var _VenueNo = new SqlParameter("@VenueNo", request.VenueNo);
                    var _VenueBranchNo = new SqlParameter("@VenueBranchNo", request.VenueBranchNo);
                    var _UserNo = new SqlParameter("@UserNo", request.UserNo);

                    objResponse = context.BarcodePrintInfo.FromSqlRaw(
                    "Execute dbo.Pro_GetDetailsToPrintBarcode " +
                    "@RequestType, @PtVisitNo, @OrderListNo, @ServiceNo, @ServiceType, @SampleNo, @BarcodeNo, @VenueNo, @VenueBranchNo, @UserNo ",
                    _RequestType, _PtVisitNo, _OrderListNo, _ServiceNo, _ServiceType, _SampleNo, _BarcodeNo, _VenueNo, _VenueBranchNo, _UserNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetBarcodePrintDetails" + request.PtVisitNo, ExceptionPriority.Low, ApplicationType.REPOSITORY, request.VenueNo, request.VenueBranchNo, 0);
            }
            return objResponse;
        }
        public List<SearchBranchSampleBarcodeResponse> SearchBranchSampleByBarcode(requestCommonSearch req)
        {
            List<SearchBranchSampleBarcodeResponse> lst = new List<SearchBranchSampleBarcodeResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req.viewvenuebranchno);
                    var _searchby = new SqlParameter("searchby", req.searchby);
                    var _searchtext = new SqlParameter("searchtext", req.searchtext);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    
                    lst = context.SearchBranchSampleByBarcode.FromSqlRaw(
                    "Execute dbo.pro_SearchBranchSampleByBarcode @pagecode,@viewvenuebranchno,@searchby,@searchtext,@venueno,@venuebranchno,@userno",
                    _pagecode, _viewvenuebranchno, _searchby, _searchtext, _venueno, _venuebranchno, _userno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.SearchBranchSampleByBarcode", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return lst;
        }

        public List<BranchSampleActionDTO> GetBranchSampleActionDetails(SampleActionRequest req)
        {
            List<BranchSampleActionDTO> objresult = new List<BranchSampleActionDTO>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    req.Barcode = req.Barcode != null ? req.Barcode : "";
                    var _PageCode = new SqlParameter("PageCode", req.PageCode);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", req.PageIndex);
                    var _Type = new SqlParameter("Type", req.Type);
                    var _FromDate = new SqlParameter("FromDate", req.FromDate);
                    var _ToDate = new SqlParameter("ToDate", req.ToDate);
                    var _deptno = new SqlParameter("deptno", req.DeptNo);
                    var _Searchkey = new SqlParameter("Searchkey", req.Searchkey.ValidateEmpty());
                    var _UserNo = new SqlParameter("userNo", req.UserNo);
                    var _VisiNo = new SqlParameter("VisitNo", req.VisitNo);
                    var _Barcode = new SqlParameter("BarcodeNo", req.Barcode.ValidateEmpty());
                    var _ISAck = new SqlParameter("ISAckData", req.ISAck);

                    objresult = context.BranchSampleActionDTO.FromSqlRaw(
                    "Execute dbo.pro_GetBranchSampleActionDetails @PageCode,@VenueNo,@VenueBranchNo,@PageIndex,@Type,@FromDate,@ToDate,@DeptNo,@Searchkey,@userNo,@VisitNo,@BarcodeNo,@ISAckData",
                    _PageCode, _VenueNo, _VenueBranchNo, _PageIndex, _Type, _FromDate, _ToDate, _deptno, _Searchkey, _UserNo, _VisiNo, _Barcode, _ISAck).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetBranchSampleActionDetails", ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return objresult;
        }

        public List<PrePrintBarcodeOrderResponse> GetPrePrintBarcodelist(long visitNo, int VenueNo, int VenueBranchNo)
        {
            List<PrePrintBarcodeOrderResponse> objresult = new List<PrePrintBarcodeOrderResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo.ToString());
                    var _VisitNo = new SqlParameter("VisitNo", visitNo);
                    
                    objresult = context.PrePrintBarcodeOrderrequest.FromSqlRaw(
                    "Execute dbo.Pro_GetPrePrintBarCodeOrder @VenueNo,@VenueBranchNo,@VisitNo",
                    _VenueNo, _VenueBranchNo, _VisitNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleRepository.GetPrePrintBarcodelist/visitNo-" + visitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
    }
}
