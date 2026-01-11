using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Dev.IRepository.Samples;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Service.Model.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Dev.Repository.Samples
{
    public class WorkListRepository : IWorkListRepository
    {
        private IConfiguration _config;
        public WorkListRepository(IConfiguration config) { _config = config; }

        public List<WorkListResponse> GetWorkList(WorkListRequest RequestItem)
        {
            List<WorkListResponse> lstWorkListResponse = new List<WorkListResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _fromDate = new SqlParameter("FromDate", RequestItem.FromDate);
                    var _toDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _type = new SqlParameter("Type", RequestItem.Type);
                    var _venueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _customerNo = new SqlParameter("CustomerNo", RequestItem.CustomerNo);
                    var _visitNo = new SqlParameter("VisitNo", RequestItem.visitNo);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);
                    var _departmentNo = new SqlParameter("DepartmentNo", RequestItem.departmentNo);
                    var _sampleNo = new SqlParameter("SampleNo", RequestItem.SampleNo);
                    var _testNo = new SqlParameter("TestNo", RequestItem.TestNo);
                    var _refferalType = new SqlParameter("RefferalType", RequestItem.refferalType);
                    var _physicianNo = new SqlParameter("PhysicianNo", RequestItem.physicianNo);
                    var _pageCount = new SqlParameter("PageCount", RequestItem.pageCount);
                    var _GridShowFilter = new SqlParameter("GridShowFilter", RequestItem.GridShowFilter);
                    var _AnalyzerNo = new SqlParameter("AnalyzerNo", RequestItem.AnalyzerNo);
                    var _serviceType = new SqlParameter("serviceType", RequestItem.serviceType);
                    var _userno = new SqlParameter("userno", RequestItem.userNo);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem.PatientNo);

                    XDocument TestXML = new XDocument(new XElement("TestXML", from Item in RequestItem.lstsearch
                                                                              select
                     new XElement("TestList",
                     new XElement("TestNo", Item.testNo),
                     new XElement("TestType", Item.testType)
                     )));

                    var _TestXML = new SqlParameter("TestXML", TestXML.ToString());

                    lstWorkListResponse = context.GetWorkListDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetWorkListInfo @FromDate, @ToDate, @Type, @VenueNo, @VenueBranchNo, @CustomerNo, @VisitNo, " +
                    "@PageIndex, @DepartmentNo, @SampleNo, @TestNo, @RefferalType, @PhysicianNo,@PageCount,@GridShowFilter,@AnalyzerNo,@serviceType,@testxml,@userno,@PatientNo",
                    _fromDate, _toDate, _type, _venueNo, _venueBranchNo, _customerNo, _visitNo,
                    _pageIndex, _departmentNo, _sampleNo, _testNo, _refferalType, _physicianNo, _pageCount, _GridShowFilter, _AnalyzerNo, _serviceType, _TestXML,_userno, _PatientNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "WorkListRepository.GetWorkList", ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueNo, RequestItem.userNo);
            }
            return lstWorkListResponse;
        }
        public List<HistoWorlkListRes> GetHistoWorkList(WorkListRequest RequestItem)
        {
            List<HistoWorlkListRes> lstWorkListResponse = new List<HistoWorlkListRes>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _fromDate = new SqlParameter("FromDate", RequestItem.FromDate);
                    var _toDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _type = new SqlParameter("Type", RequestItem.Type);
                    var _venueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _customerNo = new SqlParameter("CustomerNo", RequestItem.CustomerNo);
                    var _visitNo = new SqlParameter("VisitNo", RequestItem.visitNo);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);
                    var _departmentNo = new SqlParameter("DepartmentNo", RequestItem.departmentNo);
                    var _sampleNo = new SqlParameter("SampleNo", RequestItem.SampleNo);
                    var _testNo = new SqlParameter("TestNo", RequestItem.TestNo);
                    var _refferalType = new SqlParameter("RefferalType", RequestItem.refferalType);
                    var _physicianNo = new SqlParameter("PhysicianNo", RequestItem.physicianNo);
                    var _pageCount = new SqlParameter("PageCount", RequestItem.pageCount);
                    var _GridShowFilter = new SqlParameter("GridShowFilter", RequestItem.GridShowFilter);
                    var _AnalyzerNo = new SqlParameter("AnalyzerNo", RequestItem.AnalyzerNo);
                    var _userNo = new SqlParameter("userNo", RequestItem.userNo);
                    
                    lstWorkListResponse = context.GetHistoWorkListDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetHistoWorkListInfo @FromDate, @ToDate, @Type, @VenueNo, @VenueBranchNo, @CustomerNo, @VisitNo, " +
                    "@PageIndex, @DepartmentNo, @SampleNo, @TestNo, @RefferalType, @PhysicianNo,@PageCount,@GridShowFilter,@AnalyzerNo,@userNo",
                    _fromDate, _toDate, _type, _venueNo, _venueBranchNo, _customerNo, _visitNo,
                    _pageIndex, _departmentNo, _sampleNo, _testNo, _refferalType, _physicianNo, _pageCount, _GridShowFilter, _AnalyzerNo,_userNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "WorkListRepository.GetHistoWorkList", ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueNo, RequestItem.userNo);
            }
            return lstWorkListResponse;
        }
        public async Task<List<WorkListResponse>> PrintPatientReport(WorkListRequest PatientItem)
        {
            List<WorkListResponse> result = new List<WorkListResponse>();
            try
            {
                var Key = "WorkList Report";

                WorkListResponse item = new WorkListResponse();
                Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == Key && x.VenueNo == PatientItem.VenueNo
                    && x.VenueBranchNo == PatientItem.VenueBranchNo).FirstOrDefault();
                    if (!Directory.Exists(tblReportMaster?.ExportPath))
                    {
                        Directory.CreateDirectory(tblReportMaster?.ExportPath);
                    }
                }
                DataTable datable = objReportContext.getdatatable(objdictionary, tblReportMaster?.ProcedureName);

                ReportParamDto objitem = new ReportParamDto();
                objitem.datatable = CommonExtension.DatableToDicionary(datable);
                objitem.paramerter = objdictionary;
                objitem.ReportPath = tblReportMaster?.ReportPath;
                objitem.ExportFormat = FileFormat.PDF;
                string ReportParam = JsonConvert.SerializeObject(objitem);
                //
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppReportServiceURL = "ReportServiceURL";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppReportServiceURL);
                string dpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";
                string filename = await ExportReportService.ExportPrint(ReportParam, dpath);
                result.Add(item);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "WorkListRepository.PrintPatientReport", ExceptionPriority.High, ApplicationType.REPOSITORY, PatientItem.VenueNo, PatientItem.VenueBranchNo, PatientItem.userNo);
            }
            return result;
        }
        public List<WorkListHistoryRes> InsertWorkListHistory(WorkListHistoryReq Req)
        {
            List<WorkListHistoryRes> Response = new List<WorkListHistoryRes>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _OrderList = new SqlParameter("OrderList", Req.OrderList);
                    var _VenueNo = new SqlParameter("VenueNo", Req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", Req.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", Req.UserNo);
                    var _GridShowFilter = new SqlParameter("GridShowFilter", Req.GridShowFilter);
                    var _Type = new SqlParameter("Type", Req.Type);
                    var _FromDate = new SqlParameter("FromDate", Req.FromDate);
                    var _ToDate = new SqlParameter("ToDate", Req.ToDate);
                    var _reportFormat = new SqlParameter("reportFormat", Req.reportFormat);

                    Response = context.InsertWorkListHistory.FromSqlRaw(
                    "Execute dbo.pro_InsertWorklistHistory @OrderList, @VenueNo, @VenueBranchNo, @UserNo,@GridShowFilter,@Type,@FromDate,@ToDate,@reportFormat",
                    _OrderList, _VenueNo, _VenueBranchNo, _UserNo, _GridShowFilter,_Type, _FromDate, _ToDate,_reportFormat).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "WorkListRepository.InsertWorkListHistory", ExceptionPriority.Medium, ApplicationType.REPOSITORY, Req.VenueNo, Req.VenueNo, Req.UserNo);
            }
            return Response;
        }
        public List<GetWorkListHistoryRes> GetWorkListHistory(GetWorkListHistoryReq Req)
        {
            List<GetWorkListHistoryRes> Response = new List<GetWorkListHistoryRes>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _WorklistNo = new SqlParameter("WorklistNo", Req.WorklistNo);
                    var _VenueNo = new SqlParameter("VenueNo", Req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", Req.VenueBranchNo);
                    var _pageIndex = new SqlParameter("pageIndex", Req.pageIndex);
                    var _type = new SqlParameter("type", Req.type);
                    var _fromdate = new SqlParameter("fromdate", Req.fromdate);
                    var _todate = new SqlParameter("todate", Req.todate);
                    var _deptno = new SqlParameter("departno", Req.departno);
                    var _testno = new SqlParameter("testno", Req.testno);
                    var _testType = new SqlParameter("testType", Req.testType);
                    var _gridStatus = new SqlParameter("gridStatus", Req.gridStatus);
                    var _userNo = new SqlParameter("userNo", Req.userNo);
                    var _isDengutest = new SqlParameter("isDenguTest", Req.isDenguTest);

                    Response = context.GetWorkListHistory.FromSqlRaw(
                    "Execute dbo.pro_GetWorklistHistory @WorklistNo, @VenueNo, @VenueBranchNo,@pageIndex,@type,@fromdate,@todate,@departno,@testno,@testType,@gridStatus,@userno,@isdenguTest",
                    _WorklistNo, _VenueNo, _VenueBranchNo, _pageIndex,_type,_fromdate,_todate,_deptno,_testno,_testType,_gridStatus,_userNo,_isDengutest).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "WorkListRepository.GetWorkListHistory", ExceptionPriority.Medium, ApplicationType.REPOSITORY, Req.VenueNo, Req.VenueNo, Req.WorklistNo);
            }
            return Response;
        }
        public List<UserDeptmentDetails> GetUserDeptDetails(getUserNo RequestItem)
        {
            List<UserDeptmentDetails> Response = new List<UserDeptmentDetails>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.venueBranchNo);
                    var _userNo = new SqlParameter("UserNo",RequestItem.userNo);

                    Response = context.GetDeptDetails.FromSqlRaw(
                    "Execute dbo.pro_GetWorklistUsetDept  @VenueNo, @VenueBranchNo,@UserNo",
                    _VenueNo, _VenueBranchNo,_userNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "WorkListRepository.GetUserDeptDetails", ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.venueNo, RequestItem.venueBranchNo, RequestItem.userNo);
            }
            return Response;
        }

        public SingleTestCheckRes getTestCheck(SingleTestCheck RequestItem)
        {
            SingleTestCheckRes Response = new SingleTestCheckRes();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _testno = new SqlParameter("testNo", RequestItem.testno);
                    var _testType = new SqlParameter("testType", RequestItem.testType);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.venueBranchNo);

                    var result = context.getSubtestCheck.FromSqlRaw(
                    "Execute dbo.Pro_GetsubTestCheck  @testNo,@testType,@venueNo, @venueBranchNo",
                    _testno,_testType, _VenueNo, _VenueBranchNo).ToList();
                    Response.subTestCheck = result.Select(x => x.subTestCheck).First();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "WorkListRepository.getTestCheck", ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.venueNo, RequestItem.venueBranchNo, RequestItem.testno);
            }
            return Response;
        }

        public List<DenguTestRes> getDenguTest(DenguTestReq RequestItem)
        {
            List<DenguTestRes> _response = new List<DenguTestRes>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {;
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.venuno);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.venueBranchNo);

                    _response = context.getDenguTestDetails.FromSqlRaw(
                    "Execute dbo.Pro_GetDenguTestDetails @venueNo, @venueBranchNo",
                    _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "WorkListRepository.getDenguTest", ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.venuno, RequestItem.venueBranchNo, 0);
            }
            return _response;
        }
    } 
}
