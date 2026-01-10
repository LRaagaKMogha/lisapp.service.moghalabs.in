using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using DEV.Model.Report;
using DEV.Model.Sample;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dev.Repository
{
    public class ReportRepository : IReportRepository
    {

        private IConfiguration _config;
        public ReportRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Common Report 
        /// </summary>
        /// <param name="ReportItem"></param>
        /// <returns></returns>
        public async Task<ReportOutput> GetReport(ReportDTO ReportItem)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                var _Dictionary = ReportItem.ReportParamitem.ToDictionary(x => x.key, x => x.value);
                //string departmentNo = _Dictionary["DepartmentNo"];

                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == ReportItem.ReportKey && x.VenueNo == ReportItem.venueNo
                    && x.VenueBranchNo == ReportItem.venueBranchNo).FirstOrDefault();
                    if (!Directory.Exists(tblReportMaster?.ExportPath))
                    {
                        Directory.CreateDirectory(tblReportMaster?.ExportPath);
                }
                }

                DataTable datable = null;
                if (tblReportMaster?.ProcedureName != "pro_CollectionMIS_8")
                    datable = objReportContext.getdatatable(_Dictionary, tblReportMaster?.ProcedureName);
                else
                {
                    datable = objReportContext.getdatatable(_Dictionary, tblReportMaster?.ProcedureName);
                    string dataHtml = ConvertDataTableToHTML(objReportContext.getdatatable(_Dictionary, "Pro_UserCollectionSummaryReport"));
                    int columnNumber = 18; //Put your column X number here

                    for (int i = 0; i < datable.Rows.Count; i++)
                    {
                        datable.Rows[i][columnNumber] = dataHtml;
                    }
                }

                ReportParamDto objitem = new ReportParamDto();
                objitem.datatable = CommonExtension.DatableToDicionary(datable);
                objitem.paramerter = _Dictionary;


                objitem.ReportPath = tblReportMaster?.ReportPath;

                if (ReportItem.fileType == "excel")
                {
                    objitem.ExportPath = tblReportMaster?.ExportPath + "x_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".xls";
                    objitem.ExportFormat = FileFormat.EXCEL;
                }
                else
                {
                    objitem.ExportPath = tblReportMaster?.ExportPath + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                    objitem.ExportFormat = FileFormat.PDF;
                }
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
                result.PatientExportFile = tblReportMaster?.ExportURL + filename;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetReport/ReportKey-" + ReportItem.ReportKey, ExceptionPriority.High, ApplicationType.REPOSITORY, ReportItem.venueNo, ReportItem.venueBranchNo, ReportItem.userID);
            }
            return result;
        }
        public string GetGridReport(ReportDTO ReportItem)
        {
            string result = String.Empty;
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                var _Dictionary = ReportItem.ReportParamitem.ToDictionary(x => x.key, x => x.value);
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    tblReportMaster = context.TblReportMaster?.Where(x => x.ReportKey == ReportItem.ReportKey && x.VenueNo == ReportItem.venueNo
                    && x.VenueBranchNo == ReportItem.venueBranchNo).FirstOrDefault();

                }
                DataTable datable = objReportContext.getdatatable(_Dictionary, tblReportMaster?.ProcedureName);
                result = JsonConvert.SerializeObject(datable);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetGridReport/ReportKey-" + ReportItem.ReportKey, ExceptionPriority.High, ApplicationType.REPOSITORY, ReportItem.venueNo, ReportItem.venueBranchNo, ReportItem.userID);
            }
            return result;
        }
        public ReportOutput GetSensitivityReport(CommonFilterRequestDTO ReportItem)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                Dictionary<string, string> _Dictionary = new Dictionary<string, string>();
                _Dictionary.Add("FromDate", ReportItem.FromDate);
                _Dictionary.Add("ToDate", ReportItem.ToDate);
                _Dictionary.Add("serviceNo", ReportItem.serviceNo.ToString());
                if (ReportItem.reporttype == 3)
                {
                    _Dictionary.Add("customerNo", ReportItem.CustomerNo.ToString());
                }
                _Dictionary.Add("serviceType", ReportItem.serviceType);
                _Dictionary.Add("VenueNo", ReportItem.VenueNo.ToString());
                _Dictionary.Add("VenueBranchNo", ReportItem.VenueBranchNo.ToString());
                DataTable datable = null;
                if (ReportItem.reporttype == 1)
                {
                    datable = objReportContext.getdatatable(_Dictionary, "pro_microStatsReport");
                }
                else if (ReportItem.reporttype == 2)
                {
                    datable = objReportContext.getdatatable(_Dictionary, "pro_SupportingAntibiogram");
                }
                else if (ReportItem.reporttype == 3)
                {
                    datable = objReportContext.getdatatable(_Dictionary, "pro_microBloodCultureReport");
                }
                StringBuilder data = ConvertDataTableToCsvFile(datable);
                byte[] buffer = Encoding.ASCII.GetBytes(data.ToString());
                result.PatientExportFile = Convert.ToBase64String(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetSensitivityReport", ExceptionPriority.High, ApplicationType.REPOSITORY, ReportItem.VenueNo, ReportItem.VenueBranchNo, ReportItem.userNo);
            }
            return result;
        }

        public ReportOutput GetStatisticalReport(CommonFilterRequestDTO ReportItem)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                Dictionary<string, string> _Dictionary = new Dictionary<string, string>();
                _Dictionary.Add("FromDate", ReportItem.FromDate);
                _Dictionary.Add("ToDate", ReportItem.ToDate);
                _Dictionary.Add("serviceNo", ReportItem.serviceNo.ToString());
                _Dictionary.Add("customerNo", ReportItem.CustomerNo.ToString().ValidateEmpty());
                _Dictionary.Add("clinicno", ReportItem.clinicno.ToString().ValidateEmpty());
                _Dictionary.Add("maindepartmentno", ReportItem.maindepartmentno.ToString().ValidateEmpty());
                _Dictionary.Add("vendorNo", ReportItem.vendorNo.ToString().ValidateEmpty());                
                _Dictionary.Add("departmentNo", ReportItem.departmentNo.ToString().ValidateEmpty());
                _Dictionary.Add("serviceType", ReportItem.serviceType.ValidateEmpty());
                _Dictionary.Add("Reporttype", ReportItem.reporttype.ToString());
                _Dictionary.Add("VenueNo", ReportItem.VenueNo.ToString());
                _Dictionary.Add("VenueBranchNo", ReportItem.VenueBranchNo.ToString());
                DataTable datable = objReportContext.getdatatable(_Dictionary, "pro_exportstatisticalReport");
                StringBuilder data = ConvertDataTableToCsvFile(datable);
                byte[] buffer = Encoding.ASCII.GetBytes(data.ToString());
                result.PatientExportFile = Convert.ToBase64String(buffer, 0, buffer.Length);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetSensitivityReport", ExceptionPriority.High, ApplicationType.REPOSITORY, ReportItem.VenueNo, ReportItem.VenueBranchNo, ReportItem.userNo);
            }
            return result;
        }
        public StringBuilder ConvertDataTableToCsvFile(DataTable dtData)
        {
            StringBuilder data = new StringBuilder();

            //Taking the column names.
            for (int column = 0; column < dtData.Columns.Count; column++)
            {
                //Making sure that end of the line, shoould not have comma delimiter.
                if (column == dtData.Columns.Count - 1)
                    data.Append(dtData.Columns[column].ColumnName.ToString().Replace(",", ";"));
                else
                    data.Append(dtData.Columns[column].ColumnName.ToString().Replace(",", ";") + ',');
            }

            data.Append(Environment.NewLine);//New line after appending columns.

            for (int row = 0; row < dtData.Rows.Count; row++)
            {
                for (int column = 0; column < dtData.Columns.Count; column++)
                {
                    ////Making sure that end of the line, shoould not have comma delimiter.
                    if (column == dtData.Columns.Count - 1)
                        data.Append(dtData.Rows[row][column].ToString().Replace(",", ";"));
                    else
                        data.Append(dtData.Rows[row][column].ToString().Replace(",", ";") + ',');
                }

                //Making sure that end of the file, should not have a new line.
                if (row != dtData.Rows.Count - 1)
                    data.Append(Environment.NewLine);
            }
            return data;
        }

        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table border = 1 style='font-size:12pt;font-family:arial;width:100%;border-collapse: collapse;'>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<th>" + dt.Columns[i].ColumnName + "</th>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
        public List<TATResponse> GetTATReport(CommonFilterRequestDTO RequestItem)
        {
            List<TATResponse> lstTATResponse = new List<TATResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    //var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _RefferalType = new SqlParameter("RefferalType", RequestItem.refferalType);
                    var _CustomerNo = new SqlParameter("CustomerNo", RequestItem.CustomerNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem.physicianNo);
                    var _DepartmentNo = new SqlParameter("DepartmentNo", RequestItem.departmentNo);
                    var _TestNo = new SqlParameter("ServiceNo", RequestItem.serviceNo);
                    var _TestServiceType = new SqlParameter("ServiceType", RequestItem.serviceType);
                    var _OrderStatus = new SqlParameter("OrderStatus", RequestItem.orderStatus);
                    var _maindeptNo = new SqlParameter("maindeptNo", RequestItem.maindeptNo);

                    lstTATResponse = context.GetTATReportDTO.FromSqlRaw(
                        "Execute dbo.Pro_GetTATReport @FROMDate,@ToDate,@VenueNo,@VenueBranchNo,@RefferalType,@CustomerNo,@PhysicianNo,@DepartmentNo,@ServiceNo,@ServiceType,@OrderStatus,@maindeptNo",
                    _FromDate, _ToDate, _VenueNo, _VenueBranchNo, _RefferalType, _CustomerNo, _PhysicianNo, _DepartmentNo, _TestNo, _TestServiceType, _OrderStatus, _maindeptNo).ToList();


                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetTATReport/visitNo-" + RequestItem.visitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstTATResponse;
        }

        public List<TATResponseNew> GetTATReportNew(CommonFilterRequestDTO RequestItem)
        {
            List<TATResponseNew> lstTATResponse = new List<TATResponseNew>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _RefferalType = new SqlParameter("RefferalType", RequestItem.refferalType);
                    var _CustomerNo = new SqlParameter("CustomerNo", RequestItem.CustomerNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem.physicianNo);
                    var _DepartmentNo = new SqlParameter("DepartmentNo", RequestItem.departmentNo);
                    var _TestNo = new SqlParameter("ServiceNo", RequestItem.serviceNo);
                    var _TestServiceType = new SqlParameter("ServiceType", RequestItem.serviceType);
                    var _OrderStatus = new SqlParameter("OrderStatus", RequestItem.orderStatus);
                    var _isStat = new SqlParameter("isStat", RequestItem.isStat);
                    var _TatStatus = new SqlParameter("tatStatus", RequestItem.tatStatus);

                    lstTATResponse = context.GetTATReportNewDTO.FromSqlRaw(
                        "Execute dbo.Pro_GetTATReport_New @Type,@FROMDate,@ToDate,@VenueNo,@VenueBranchNo,@RefferalType,@CustomerNo,@PhysicianNo,@DepartmentNo,@ServiceNo,@ServiceType,@OrderStatus,@isStat,@tatStatus",
                    _Type, _FromDate, _ToDate, _VenueNo, _VenueBranchNo, _RefferalType, _CustomerNo, _PhysicianNo, _DepartmentNo, _TestNo, _TestServiceType, _OrderStatus, _isStat, _TatStatus).ToList();


                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetTATReportNew/visitNo-" + RequestItem.visitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstTATResponse;
        }

        public List<TATReportDetailsResponse> GetTATReportDetails(CommonFilterRequestDTO RequestItem)
        {
            List<TATReportDetailsResponse> lstTATResponse = new List<TATReportDetailsResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _DepartmentNo = new SqlParameter("DepartmentNo", RequestItem.departmentNo);
                    var _FilterType = new SqlParameter("FilterType", RequestItem.orderStatus);
                    var _TestNo = new SqlParameter("ServiceNo", RequestItem.serviceNo);
                    var _TestServiceType = new SqlParameter("ServiceType", RequestItem.serviceType);
                    var _isStat = new SqlParameter("isStat", RequestItem.isStat);

                    lstTATResponse = context.GetTATReportDetails.FromSqlRaw(
                        "Execute dbo.pro_TATReport_Details @Type,@FROMDate,@ToDate,@VenueNo,@VenueBranchNo,@DepartmentNo,@FilterType,@ServiceNo,@ServiceType,@isStat",
                    _Type, _FromDate, _ToDate, _VenueNo, _VenueBranchNo, _DepartmentNo, _FilterType, _TestNo, _TestServiceType, _isStat).ToList();


                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetTATReportDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstTATResponse;
        }

        public List<GetICMRResponse> GetICMRResult(CommonFilterRequestDTO RequestItem)
        {
            List<GetICMRResponse> lstGetICMRResponse = new List<GetICMRResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _CustomerNo = new SqlParameter("CustomerNo", RequestItem.CustomerNo);
                    var _PageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);

                    lstGetICMRResponse = context.GetICMRResponseDTO.FromSqlRaw(
                        "Execute dbo.Pro_GetICMRResultResponse @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@CustomerNo,@PageIndex",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _CustomerNo, _PageIndex).ToList();


                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetICMRResult/CustomerNo-" + RequestItem.CustomerNo, ExceptionPriority.Low, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstGetICMRResponse;
        }
        public List<AuditLogResponse> GetAudioLog(CommonFilterRequestDTO RequestItem)
        {
            List<AuditLogResponse> lstAuditLogDTO = new List<AuditLogResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem.PatientNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem.visitNo);
                    var _userNo = new SqlParameter("UserNo", RequestItem.userNo);
                    var _PageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);

                    var AuditLogDTOdata = context.AuditLogDTO.FromSqlRaw(
                         "Execute dbo.Pro_GetAuditLogReport @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@PatientNo,@VisitNo,@UserNo,@PageIndex",
                     _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _PatientNo, _VisitNo, _userNo, _PageIndex).ToList();

                    lstAuditLogDTO = GetAuditResponse(AuditLogDTOdata, RequestItem.VenueNo, RequestItem.VenueBranchNo);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetAudioLog/PatientNo-" + RequestItem.PatientNo, ExceptionPriority.Low, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstAuditLogDTO;
        }
        public List<AuditLogResponse> GetAuditResponse(List<AuditLogDTO> auditLogDTO, int VenueNo, int VenueBranchNo)
        {
            List<AuditLogResponse> objresult = new List<AuditLogResponse>();
            try
            {
                int oldPatientVisitNo = 0;
                int newPatientVisitNo = 0;
                int OldAuditLogNo = 0;
                int newAuditLogNo = 0;
                foreach (var auditlogList in auditLogDTO)
                {
                    AuditLogResponse Responseitem = new AuditLogResponse();
                    List<AuditDetailDTO> lstAuditDetail = new List<AuditDetailDTO>();
                    newPatientVisitNo = auditlogList.PatientVisitNo;
                    var auditLogItem = auditLogDTO.Where(x => x.PatientVisitNo == newPatientVisitNo).Select(x => new
                    {
                        x.AuditLogDate,
                        x.Comments,
                        x.AuditTypeNo,
                        x.AuditTypeName,
                        x.AuditLogNo
                    }).ToList();
                    if (newPatientVisitNo != oldPatientVisitNo)
                    {
                        Responseitem.PatientVisitNo = auditlogList.PatientVisitNo;
                        Responseitem.PatientId = auditlogList.PatientId;
                        Responseitem.PatientNo = auditlogList.PatientNo;
                        Responseitem.PatientName = auditlogList.PatientName;
                        Responseitem.VisitDTTM = auditlogList.VisitDTTM;
                        Responseitem.VisitID = auditlogList.VisitID;
                        Responseitem.pageIndex = auditlogList.pageIndex;
                        Responseitem.TotalRecords = auditlogList.TotalRecords;
                        oldPatientVisitNo = auditlogList.PatientVisitNo;
                        OldAuditLogNo = 0;
                        foreach (var Item in auditLogItem)
                        {
                            newAuditLogNo = (int)Item.AuditLogNo;
                            if (OldAuditLogNo != newAuditLogNo)
                            {
                                AuditDetailDTO objaudit = new AuditDetailDTO()
                                {
                                    AuditLogDate = Item.AuditLogDate,
                                    AuditLogNo = Item.AuditLogNo,
                                    AuditTypeNo = Item.AuditTypeNo,
                                    AuditTypeName = Item.AuditTypeName,
                                    Comments = Item.Comments
                                };
                                OldAuditLogNo = newAuditLogNo;
                                lstAuditDetail.Add(objaudit);

                            }
                            Responseitem.Auditdetail = lstAuditDetail;
                        }
                        objresult.Add(Responseitem);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetAuditResponse", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<AuditHistory> GetAuditHistory(int FirstAuditLogNo, int SecondAuditLogNo, int Type)
        {
            List<AuditHistory> objresult = new List<AuditHistory>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FirstAuditLogNo = new SqlParameter("FirstAuditLogNo", FirstAuditLogNo);
                    var _SecondAuditLogNo = new SqlParameter("SecondAuditLogNo", SecondAuditLogNo);
                    var _Type = new SqlParameter("Type", Type);
                    objresult = context.AuditHistory.FromSqlRaw(
                        "Execute dbo.Pro_GetAuditHistory @FirstAuditLogNo,@SecondAuditLogNo,@Type",
                     _FirstAuditLogNo, _SecondAuditLogNo, _Type).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeRepository.GetAuditHistory", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
        public List<AdvancePaymentList> GetAdvancePayment(CommonFilterRequestDTO RequestItem)
        {
            List<AdvancePaymentList> objresult = new List<AdvancePaymentList>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _CustomerNo = new SqlParameter("CustomerNo", RequestItem.CustomerNo);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);

                    objresult = context.AdvancePaymentList.FromSqlRaw(
                        "Execute dbo.pro_GetAdvancePayment @FROMDate, @ToDate, @Type, @CustomerNo, @VenueNo, @VenueBranchNo, @PageIndex",
                    _FromDate, _ToDate, _Type, _CustomerNo, _VenueNo, _VenueBranchNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetAdvancePayment", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
        public AdvancePaymentListResponse InsertAdvancePayment(AdvancePaymentListRequest RequestItem)
        {
            AdvancePaymentListResponse objresult = new AdvancePaymentListResponse();
            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string PaymentXML = commonUtility.ToXML(RequestItem.advancePaymentTypes);
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PaymentXML = new SqlParameter("PaymentXML", PaymentXML);
                    var _CustomerNo = new SqlParameter("CustomerNo", RequestItem.CustomerNo);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _TransactionDateTime = new SqlParameter("TransactionDateTime", RequestItem.TransactionDateTime);
                    var _Amount = new SqlParameter("Amount", RequestItem.Amount);
                    var _Remarks = new SqlParameter("Remarks", RequestItem.Remarks);
                    var _userno = new SqlParameter("userno", RequestItem.userno);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.venueno);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.venuebranchno);

                    objresult = context.AdvancePaymentListRequest.FromSqlRaw(
                        "Execute dbo.pro_InsertAdvancePayment @CustomerNo, @Type, @TransactionDateTime, @Amount, @Remarks, @userno, @VenueNo, @VenueBranchNo, @PaymentXML",
                    _CustomerNo, _Type, _TransactionDateTime, _Amount, _Remarks, _userno, _VenueNo, _VenueBranchNo, _PaymentXML).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.InsertAdvancePayment", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }

        #region Cash Expenses
        public List<CashExpenseDTO> GetCashExpenses(GetCashExpenseParam RequestItem)
        {
            List<CashExpenseDTO> objresult = new List<CashExpenseDTO>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("venueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("venueBranchNo", RequestItem.VenueBranchNo);
                    var _IssuedByNo = new SqlParameter("issuedByNo", RequestItem.IssuedByNo);
                    var _IssuedToTypeNo = new SqlParameter("issuedToTypeNo", RequestItem.IssuedToTypeNo);
                    var _IssuedTo = new SqlParameter("issuedTo", RequestItem.IssuedTo);
                    var _ExpenseCategory = new SqlParameter("expenseCategory", RequestItem.ExpenseCategory);
                    var _pageindex = new SqlParameter("pageindex", RequestItem.pageIndex);
                    var _type = new SqlParameter("type", RequestItem.Type);
                    var _fromdate = new SqlParameter("fromdate", RequestItem.Fromdate);
                    var _todate = new SqlParameter("todate", RequestItem.ToDate);
                    var _UserNo = new SqlParameter("UserNo", RequestItem.UserNo);

                    objresult = context.GetCashExpenses.FromSqlRaw(
                        "Execute dbo.Pro_GetCashExpesnses @venueNo,@venueBranchNo,@issuedByNo,@issuedToTypeNo,@issuedTo,@expenseCategory,@pageindex,@type,@fromdate,@todate,@UserNo",
                    _VenueNo, _VenueBranchNo, _IssuedByNo, _IssuedToTypeNo, _IssuedTo, _ExpenseCategory,_pageindex,_type,_fromdate,_todate, _UserNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetCashExpenses", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
        public InsertCashExpenseDTO InsertCashExpenses(SaveCashExpenseDTO RequestItem)
        {
            InsertCashExpenseDTO objresult = new InsertCashExpenseDTO();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("venueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _CreatedBy = new SqlParameter("createdBy", RequestItem.CreatedBy);
                    var _IssuedDate = new SqlParameter("issuedDate", RequestItem.IssuedDate);
                    var _IssueFrom = new SqlParameter("issueFrom", RequestItem.IssueFrom);
                    var _IssueToType = new SqlParameter("issueToType", RequestItem.IssueToType);
                    var _IssueToUserNo = new SqlParameter("issueToUserNo", RequestItem.IssueToUserNo);
                    var _IssueToUserName = new SqlParameter("issueToUserName", RequestItem.IssueToUserName);
                    var _ExpenseCate = new SqlParameter("expenseCate", RequestItem.ExpenseCate);
                    var _Amount = new SqlParameter("amount", RequestItem.Amount);
                    var _Reason = new SqlParameter("reason", RequestItem.Reason);
                    var _ExpenseEntryNo = new SqlParameter("expenseEntryNo", RequestItem.ExpenseEntryNo);
                    var _Status = new SqlParameter("status", RequestItem.Status);

                    objresult = context.InsertCashExpenses.FromSqlRaw(
                        "Execute dbo.Pro_InsertCashExpesnses @venueNo,@venueBranchNo,@createdBy,@issuedDate,@issueFrom,@issueToType,@issueToUserNo,@issueToUserName,@expenseCate,@amount,@reason,@expenseEntryNo,@status",
                    _VenueNo, _VenueBranchNo, _CreatedBy, _IssuedDate, _IssueFrom, _IssueToType, _IssueToUserNo, _IssueToUserName, _ExpenseCate, _Amount, _Reason, _ExpenseEntryNo, _Status).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.InsertCashExpenses", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }

        public List<GetReqExpensesResponse> GetReqExpenses(GetReqExpensesParam RequestItem)
        {
            List<GetReqExpensesResponse> objresult = new List<GetReqExpensesResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("venueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("venueBranchNo", RequestItem.VenueBranchNo);
                    var _IssuedByNo = new SqlParameter("issuedByNo", RequestItem.IssuedByNo == null ? 0 : RequestItem.IssuedByNo);
                    var _IssuedToTypeNo = new SqlParameter("issuedToTypeNo", RequestItem.IssuedToTypeNo == null ? 0 : RequestItem.IssuedToTypeNo);
                    var _IssuedTo = new SqlParameter("issuedTo", RequestItem.IssuedTo == null ? 0 : RequestItem.IssuedTo);
                    var _ExpenseCategory = new SqlParameter("expenseCategory", RequestItem.ExpenseCategory == null ? 0 : RequestItem.ExpenseCategory);
                    var _PageIndex = new SqlParameter("pageIndex", RequestItem.PageIndex);
                    var _Type = new SqlParameter("type", RequestItem.Type);
                    var _Fromdate = new SqlParameter("fromdate", RequestItem.Fromdate);
                    var _Todate = new SqlParameter("todate", RequestItem.Todate);
                    var _ApprovalReq = new SqlParameter("ApprovalReq", RequestItem.ApprovalReq);

                    objresult = context.GetReqExpenses.FromSqlRaw(
                       "Execute dbo.Pro_GetReqExpenses @venueNo,@venueBranchNo,@IssuedByNo,@IssuedToTypeNo,@IssuedTo,@ExpenseCategory,@PageIndex,@Type,@Fromdate,@ToDate,@ApprovalReq",
                   _VenueNo, _VenueBranchNo, _IssuedByNo, _IssuedToTypeNo, _IssuedTo, _ExpenseCategory, _PageIndex, _Type, _Fromdate, _Todate, _ApprovalReq).AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetReqExpenses", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
        public InsertCashExpenseDTO ApproveExpenses(ApproveExpenses RequestItem)
        {
            InsertCashExpenseDTO objresult = new InsertCashExpenseDTO();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _CreatedBy = new SqlParameter("CreatedBy", RequestItem.CreatedBy == null ? 0 : RequestItem.CreatedBy);
                    var _Amount = new SqlParameter("Amount", RequestItem.Amount == null ? 0 : RequestItem.Amount);
                    var _Reason = new SqlParameter("Reason", RequestItem.Reason.ValidateEmpty());
                    var _ExpenseEntryNo = new SqlParameter("ExpenseEntryNo", RequestItem.ExpenseEntryNo == null ? 0 : RequestItem.ExpenseEntryNo);
                    var _IsApprove = new SqlParameter("IsApprove", RequestItem.IsApprove == null ? false : RequestItem.IsApprove);

                    objresult = context.ApproveExpenses.FromSqlRaw(
                        "Execute dbo.Pro_ApproveExpenses @VenueNo,@VenueBranchNo,@CreatedBy,@Amount,@Reason,@ExpenseEntryNo,@IsApprove",
                    _VenueNo, _VenueBranchNo, _CreatedBy, _Amount, _Reason, _ExpenseEntryNo, _IsApprove).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.ApproveExpenses", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
        #endregion

        public async Task<ReportOutput> GetReportbylist(ReportDTO ReportItem)
        {
            ReportOutput objresult = new ReportOutput();
            try
            {
                TblReportMaster tblReportMaster = new TblReportMaster();
                var _Dictionary = ReportItem.ReportParamitem.ToDictionary(x => x.key, x => x.value);
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == ReportItem.ReportKey && x.VenueNo == ReportItem.venueNo
                        && x.VenueBranchNo == ReportItem.venueBranchNo).FirstOrDefault();
                    if (!Directory.Exists(tblReportMaster?.ExportPath))
                    {
                        Directory.CreateDirectory(tblReportMaster?.ExportPath);
                    }
                }
                var param = _Dictionary.Where(x => x.Key == "Outputlstvalue").Select(d => d.Value).SingleOrDefault();
                string tablelst = param != null ? param.ToString() : "";
                DataTable datable = ManuallyConvertJsonToDataTable(tablelst);
                ReportParamDto objitem = new ReportParamDto();
                objitem.datatable = CommonExtension.DatableToDicionary(datable);
                objitem.paramerter = _Dictionary;
                objitem.ReportPath = tblReportMaster?.ReportPath;
                if (ReportItem.fileType == "excel")
                {
                    objitem.ExportPath = tblReportMaster?.ExportPath + "x_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".xls";
                    objitem.ExportFormat = FileFormat.EXCEL;
                }
                else
                {
                    objitem.ExportPath = tblReportMaster?.ExportPath + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                    objitem.ExportFormat = FileFormat.PDF;
                }
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
                objresult.PatientExportFile = tblReportMaster?.ExportURL + filename;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetReport/ReportKey-" + ReportItem.ReportKey, ExceptionPriority.High, ApplicationType.REPOSITORY, ReportItem.venueNo, ReportItem.venueBranchNo, ReportItem.userID);
            }
            return objresult;
        }

        public DataTable ManuallyConvertJsonToDataTable(string sampleJson)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (string.IsNullOrWhiteSpace(sampleJson))
                {
                    return dataTable;
                }
                var cleanedJson = Regex.Replace(sampleJson, "\\\\|\n|\r|\t|\\[|\\]|\"", "");
                var items = Regex.Split(cleanedJson, "},{").AsSpan();
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = items[i].Replace("{", "").Replace("}", "");
                }
                var columns = Regex.Split(items[0], ",").AsSpan();
                foreach (string column in columns)
                {
                    var parts = Regex.Split(column, ":").AsSpan();
                    dataTable.Columns.Add(parts[0].Trim());
                }
                for (int i = 0; i < items.Length; i++)
                {
                    var row = dataTable.NewRow();
                    var values = Regex.Split(items[i], ",").AsSpan();
                    for (int j = 0; j < values.Length; j++)
                    {
                        var parts = Regex.Split(values[j], ":").AsSpan();
                        if (int.TryParse(parts[1].Trim(), out int temp))
                            row[j] = temp;
                        else
                            row[j] = parts[1].Trim();
                    }
                    dataTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetReport/ReportKey-", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return dataTable;
        }

        public ReportOutput GetWorkloadReport(CommonFilterRequestDTO ReportItem)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                Dictionary<string, string> _Dictionary = new Dictionary<string, string>();
                _Dictionary.Add("FromDate", ReportItem.FromDate);
                _Dictionary.Add("ToDate", ReportItem.ToDate);
                _Dictionary.Add("VenueNo", ReportItem.VenueNo.ToString());
                _Dictionary.Add("VenueBranchNo", ReportItem.VenueBranchNo.ToString());
                _Dictionary.Add("UserNo", ReportItem.userNo.ToString());
                _Dictionary.Add("DepartmentType", ReportItem.serviceType.ToString());
                DataTable datable = objReportContext.getdatatable(_Dictionary, "Pro_GetWorkLoad_Statistics");
                datable.Columns.Remove("Month");
                StringBuilder data = ConvertDataTableToCsvFile(datable);
                byte[] buffer = Encoding.ASCII.GetBytes(data.ToString());
                result.PatientExportFile = Convert.ToBase64String(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetWorkloadReport", ExceptionPriority.High, ApplicationType.REPOSITORY, ReportItem.VenueNo, ReportItem.VenueBranchNo, ReportItem.userNo);
            }
            return result;
        }
        public ReportOutput GetNonGynaeWorkLoadReport(CommonFilterRequestDTO ReportItem)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                Dictionary<string, string> _Dictionary = new Dictionary<string, string>();
                _Dictionary.Add("FromDate", ReportItem.FromDate);
                _Dictionary.Add("ToDate", ReportItem.ToDate);
                _Dictionary.Add("VenueNo", ReportItem.VenueNo.ToString());
                _Dictionary.Add("VenueBranchNo", ReportItem.VenueBranchNo.ToString());
                DataTable datable = objReportContext.getdatatable(_Dictionary, "Pro_GetNonGynae_WorkLoad");
                datable.Columns.Remove("SpecimenDate");
                StringBuilder data = ConvertDataTableToCsvFile(datable);
                byte[] buffer = Encoding.ASCII.GetBytes(data.ToString());
                result.PatientExportFile = Convert.ToBase64String(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetNonGynaeWorkLoadReport", ExceptionPriority.High, ApplicationType.REPOSITORY, ReportItem.VenueNo, ReportItem.VenueBranchNo, ReportItem.userNo);
            }
            return result;
        }
        public ReportOutput GetCytoQCReport(CommonFilterRequestDTO ReportItem)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                Dictionary<string, string> _Dictionary = new Dictionary<string, string>();
                _Dictionary.Add("FromDate", ReportItem.FromDate);
                _Dictionary.Add("ToDate", ReportItem.ToDate);
                _Dictionary.Add("VenueNo", ReportItem.VenueNo.ToString());
                _Dictionary.Add("VenueBranchNo", ReportItem.VenueBranchNo.ToString());
                DataTable datable = objReportContext.getdatatable(_Dictionary, "Pro_GetCytoQC");
                datable.Columns.Remove("SpecimenDate");
                StringBuilder data = ConvertDataTableToCsvFile(datable);
                byte[] buffer = Encoding.ASCII.GetBytes(data.ToString());
                result.PatientExportFile = Convert.ToBase64String(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetCytoQCReport", ExceptionPriority.High, ApplicationType.REPOSITORY, ReportItem.VenueNo, ReportItem.VenueBranchNo, ReportItem.userNo);
            }
            return result;
        }


        public ReportOutput GetCytoWorkloadReport(SlidePrintingRequest ReportItem)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                Dictionary<string, string> _Dictionary = new Dictionary<string, string>();
                _Dictionary.Add("FromDate", ReportItem.FromDate);
                _Dictionary.Add("ToDate", ReportItem.ToDate);
                _Dictionary.Add("VenueNo", ReportItem.VenueNo.ToString());
                _Dictionary.Add("VenueBranchNo", ReportItem.VenueBranchNo.ToString());
                _Dictionary.Add("UserNo", ReportItem.userNo.ToString());
                //_Dictionary.Add("DepartmentType", ReportItem.serviceType.ToString());
                _Dictionary.Add("FromRPNo", ReportItem.fromrp.ToString());
                _Dictionary.Add("ToRPNo", ReportItem.torp.ToString());
                DataTable datable = objReportContext.getdatatable(_Dictionary, "pro_GetPapSmearTest");
                StringBuilder data = ConvertDataTableToCsvFile(datable);
                byte[] buffer = Encoding.ASCII.GetBytes(data.ToString());
                result.PatientExportFile = Convert.ToBase64String(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetCytoWorkloadReport", ExceptionPriority.High, ApplicationType.REPOSITORY, ReportItem.VenueNo, ReportItem.VenueBranchNo, ReportItem.userNo);
            }
            return result;
        }

        public string GetStaffBillingDetailsMIS(UserFrontOfficeMIS RequestItem)
        {
            string result = string.Empty;
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                objdictionary.Add("Type", RequestItem.Type.ToString());
                objdictionary.Add("Action", RequestItem.Action.ToString());
                objdictionary.Add("FromDate", RequestItem.FromDate.ToString());
                objdictionary.Add("ToDate", RequestItem.ToDate.ToString());
                objdictionary.Add("ReferralTypeNo", RequestItem.ReferralTypeNo.ToString());
                objdictionary.Add("ReferralNo", RequestItem.ReferralNo.ToString());
                objdictionary.Add("MarketingNo", RequestItem.MarketingNo.ToString());
                objdictionary.Add("RiderNo", RequestItem.RiderNo.ToString());
                objdictionary.Add("DeptNo", RequestItem.DeptNo.ToString());
                objdictionary.Add("BillUserNo", RequestItem.BillUserNo.ToString());
                objdictionary.Add("ViewVenueBranchNo", RequestItem.ViewVenueBranchNo.ToString());
                objdictionary.Add("VenueNo", RequestItem.VenueNo.ToString());
                objdictionary.Add("VenueBranchNo", RequestItem.VenueBranchNo.ToString());
                objdictionary.Add("UserNo", RequestItem.UserNo.ToString());
                objdictionary.Add("Filter", RequestItem.Filter.ToString());
                objdictionary.Add("DiscountTypeNo", RequestItem.DiscountTypeNo.ToString());
                DataTable dta = objReportContext.getdatatable(objdictionary, "Pro_GetBranchWiseStaffMISBillDetails");
                result = JsonConvert.SerializeObject(dta);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetStaffBillingDetailsMIS", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.UserNo);
            }
            return result;
        }

        public string GetUserwiseFrontOfficeMIS(UserFrontOfficeMIS RequestItem)
        {
            string result = string.Empty;
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                objdictionary.Add("Type", RequestItem.Type.ToString());
                objdictionary.Add("Action", RequestItem.Action.ToString());
                objdictionary.Add("FromDate", RequestItem.FromDate.ToString());
                objdictionary.Add("ToDate", RequestItem.ToDate.ToString());
                objdictionary.Add("ReferralTypeNo", RequestItem.ReferralTypeNo.ToString());
                objdictionary.Add("ReferralNo", RequestItem.ReferralNo.ToString());
                objdictionary.Add("MarketingNo", RequestItem.MarketingNo.ToString());
                objdictionary.Add("RiderNo", RequestItem.RiderNo.ToString());
                objdictionary.Add("DeptNo", RequestItem.DeptNo.ToString());
                objdictionary.Add("BillUserNo", RequestItem.BillUserNo.ToString());
                objdictionary.Add("ViewVenueBranchNo", RequestItem.ViewVenueBranchNo.ToString());
                objdictionary.Add("VenueNo", RequestItem.VenueNo.ToString());
                objdictionary.Add("VenueBranchNo", RequestItem.VenueBranchNo.ToString());
                objdictionary.Add("UserNo", RequestItem.UserNo.ToString());
                objdictionary.Add("Filter", RequestItem.Filter.ToString());
                objdictionary.Add("DiscountTypeNo", RequestItem.DiscountTypeNo.ToString());
                DataTable dta = objReportContext.getdatatable(objdictionary, "Pro_GetDaywiseFrontOfficeMIS");
                result = JsonConvert.SerializeObject(dta);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetUserwiseFrontOfficeMIS", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.UserNo);
            }
            return result;
        }
    }
}
