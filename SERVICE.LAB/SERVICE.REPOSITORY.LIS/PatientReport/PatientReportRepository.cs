using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using DEV.Model.EF;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using DEV.Common;
using System.Data;
using Serilog;
using System.Text.RegularExpressions;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;
using Microsoft.AspNetCore.Mvc;
using RtfPipe;
using PdfSharp.Pdf.Advanced;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Threading.Tasks;
//using BloodBankManagement.Contracts;

namespace Dev.Repository
{
    public class PatientReportRepository : IPatientReportRepository
    {
        private IConfiguration _config;
        public PatientReportRepository(IConfiguration config) { _config = config; }
        public List<lstpatientreport> GetPatientReport(requestpatientreport req)
        {
            List<lstpatientreport> lst = new List<lstpatientreport>();
            try
            {
                TempalteSearchResponse objOut = new TempalteSearchResponse();
                if (!string.IsNullOrEmpty(req.searchKey))
                {
                    objOut = GetPatientImpressionBasedReport(req);
                }
                else
                {
                    objOut.patientvisitno = "";
                    objOut.orderlistno = "";
                }
                using (var context = new PatientReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req?.pagecode);
                    var _venueno = new SqlParameter("venueno", req?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req?.venuebranchno);
                    var _userno = new SqlParameter("userno", req?.userno);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req?.viewvenuebranchno);
                    var _pageindex = new SqlParameter("pageindex", req?.pageindex);
                    var _type = new SqlParameter("type", req?.type);
                    var _fromdate = new SqlParameter("fromdate", req?.fromdate);
                    var _todate = new SqlParameter("todate", req?.todate);
                    var _patientno = new SqlParameter("patientno", req?.patientno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req?.patientvisitno);
                    var _deptno = new SqlParameter("deptno", req?.deptno);
                    var _serviceno = new SqlParameter("serviceno", req?.serviceno);
                    var _servicetype = new SqlParameter("servicetype", req?.servicetype);
                    var _refferraltypeno = new SqlParameter("refferraltypeno", req?.refferraltypeno);
                    var _customerno = new SqlParameter("customerno", req?.customerno);
                    var _physicianno = new SqlParameter("physicianno", req?.physicianno);
                    var _riderno = new SqlParameter("riderno", req?.riderno);
                    var _excutiveno = new SqlParameter("excutiveno", req?.excutiveno);
                    var _isstat = new SqlParameter("isstat", req?.isstat);
                    var _isSTATFilter = new SqlParameter("isSTATFilter", req.isSTATFilter);
                    var _isdue = new SqlParameter("isdue", req?.isdue);
                    var _isabnormal = new SqlParameter("isabnormal", req?.isabnormal);
                    var _iscritical = new SqlParameter("iscritical", req?.iscritical);
                    var _istat = new SqlParameter("istat", req?.istat);
                    var _orderstatus = new SqlParameter("orderstatus", req?.orderstatus);
                    var _printstatus = new SqlParameter("printstatus", req?.printstatus);
                    var _cpprintstatus = new SqlParameter("cpprintstatus", req?.cpprintstatus);
                    var _smsstatus = new SqlParameter("smsstatus", req?.smsstatus);
                    var _emailstatus = new SqlParameter("emailstatus", req?.emailstatus);
                    var _whatsappstatus = new SqlParameter("whatsappstatus", req?.whatsappstatus);
                    var _loginType = new SqlParameter("loginType", req?.loginType);
                    var _routeNo = new SqlParameter("routeNo", req?.routeNo);
                    var _Deliverymode = new SqlParameter("Deliverymode", req?.deliverymode);
                    var _WardNo = new SqlParameter("wardNo", req?.wardNo);
                    var _maindeptNo = new SqlParameter("maindeptNo", req?.maindeptNo);
                    var _searchkeyorderlistnos = new SqlParameter("searchkeyorderlistnos", objOut.orderlistno);
                    var _searchkeyvisitnos = new SqlParameter("searchkeyvisitnos", objOut.patientvisitno);
                    var _pageCount = new SqlParameter("pageCount", req.pageCount);
                    var _MultiFieldsSearch = new SqlParameter("multiFieldsSearch", req.multiFieldsSearch);

                    var rtndblst = context.GetPatientReport.FromSqlRaw(
                    "Execute dbo.pro_PatientReport @pagecode,@venueno,@venuebranchno,@userno,@viewvenuebranchno,@pageindex,@type,@fromdate,@todate,@patientno,@patientvisitno, @deptno," +
                    "@serviceno,@servicetype,@refferraltypeno,@customerno,@physicianno,@riderno,@excutiveno,@isstat,@isSTATFilter,@isdue,@isabnormal,@iscritical,@istat,@orderstatus," +
                    "@printstatus,@cpprintstatus,@smsstatus,@emailstatus,@loginType,@routeNo,@Deliverymode,@wardNo,@maindeptNo,@searchkeyorderlistnos,@searchkeyvisitnos, @whatsappstatus,@pageCount,@multiFieldsSearch",
                    _pagecode, _venueno, _venuebranchno, _userno, _viewvenuebranchno, _pageindex, _type, _fromdate, _todate, _patientno, _patientvisitno, _deptno, _serviceno, _servicetype, 
                    _refferraltypeno, _customerno, _physicianno, _riderno, _excutiveno, _isstat, _isSTATFilter, _isdue, _isabnormal, _iscritical, _istat, _orderstatus, _printstatus, 
                    _cpprintstatus, _smsstatus, _emailstatus, _loginType, _routeNo, _Deliverymode, _WardNo, _maindeptNo, _searchkeyorderlistnos, _searchkeyvisitnos, _whatsappstatus, _pageCount, _MultiFieldsSearch).ToList();

                    int patientvisitno = 0;
                    foreach (var v in rtndblst)
                    {
                        if (patientvisitno != v.patientvisitno)
                        {
                            patientvisitno = v.patientvisitno;
                            lstpatientreport obj = new lstpatientreport();
                            obj.ischecked = false;
                            obj.patientno = v.patientno;
                            obj.rhNo = v.rhNo;
                            obj.patientvisitno = v.patientvisitno;
                            obj.patientid = v.patientid;
                            obj.fullname = v.fullname;
                            obj.agegender = v.agegender;
                            obj.patientmobile = v.patientmobile;
                            obj.patientemailid = v.patientemailid;
                            obj.ispatientimage = v.ispatientimage;
                            obj.visitid = v.visitid;
                            obj.extenalvisitid = v.extenalvisitid;
                            obj.visitdttm = v.visitdttm;
                            obj.refferraltypeno = v.refferraltypeno;
                            obj.referraltype = v.referraltype;
                            obj.customerno = v.customerno;
                            obj.customername = v.customername;
                            obj.customeremailid = v.customeremailid;
                            obj.customermobile = v.customermobile;
                            obj.islabheader = v.islabheader;
                            obj.isreportblocked = v.isreportblocked;
                            obj.isinternotes = v.isinternotes;
                            obj.physicianno = v.physicianno;
                            obj.physicianname = v.physicianname;
                            obj.physicianemailid = v.physicianemailid;
                            obj.physicianmobile = v.physicianmobile;
                            obj.riderno = v.riderno;
                            obj.ridername = v.ridername;
                            obj.excutiveno = v.excutiveno;
                            obj.excutivename = v.excutivename;
                            obj.isstat = v.isstat;
                            obj.rctdttm = v.rctdttm;
                            obj.modeofdispatch = v.modeofdispatch;
                            obj.isvisitstatus = v.isvisitstatus;
                            obj.taskdttm = v.taskdttm;
                            obj.isdue = v.isdue;
                            obj.cancelled = v.cancelled;
                            obj.visabnormal = v.visabnormal;
                            obj.viscritical = v.viscritical;
                            obj.vistat = v.vistat;
                            obj.orderstatus = v.orderstatus;
                            obj.printstatus = v.printstatus;
                            obj.cpprintstatus = v.cpprintstatus;
                            obj.smsstatus = v.smsstatus;
                            obj.emailstatus = v.emailstatus;
                            obj.whatsappstatus = v.whatsappstatus;
                            obj.visremarks = v.visremarks;
                            obj.viscpremarks = v.viscpremarks;
                            obj.TotalRecords = v.TotalRecords;
                            obj.DuePrint = v.DuePrint;
                            obj.Deliverymodes = v.Deliverymodes;
                            obj.VenueBranchName = v.VenueBranchName;
                            obj.IDnumber = v.IDnumber;
                            obj.IsVipIndication = v.IsVipIndication;

                            obj.isPatientReportSMS = v.isPatientReportSMS;
                            obj.isPatientReportEmail = v.isPatientReportEmail;
                            obj.isPatientReportWhatsapp = v.isPatientReportWhatsapp;
                            obj.isCustomerReportSMS = v.isCustomerReportSMS;
                            obj.isCustomerReportEmail = v.isCustomerReportEmail;
                            obj.isCustomerReportWhatsapp = v.isCustomerReportWhatsapp;
                            obj.isPhysicianreportSms = v.isPhysicianreportSms;
                            obj.isPhysicianreportEmail = v.isPhysicianreportEmail;
                            obj.isPhysicianreportWhatsapp = v.isPhysicianreportWhatsapp;

                            obj.communicationPtName = v.communicationPtName;
                            obj.communicationVisitId = v.communicationVisitId;
                            obj.communicationVisitDate = v.communicationVisitDate;

                            obj.emailIdToShow = v.emailIdToShow;
                            obj.mobileNumberToShow = v.mobileNumberToShow;

                            var rollst = rtndblst.Where(o => o.patientvisitno == v.patientvisitno).ToList();

                            List<lstreportorderlist> lstrol = new List<lstreportorderlist>();
                            foreach (var s in rollst)
                            {
                                lstreportorderlist objrol = new lstreportorderlist();
                                obj.ischecked = false;
                                objrol.orderno = s.orderno;
                                objrol.orderlistno = s.orderlistno;
                                objrol.servicetype = s.servicetype;
                                objrol.serviceno = s.serviceno;
                                objrol.servicecode = s.servicecode;
                                objrol.servicename = s.servicename;
                                objrol.departmentno = s.departmentno;
                                objrol.departmentname = s.departmentname;
                                objrol.resulttypeno = s.resulttypeno;
                                objrol.orderliststatus = s.orderliststatus;
                                objrol.orderliststatustext = s.orderliststatustext;
                                objrol.colorcode = s.colorcode;
                                objrol.barcodeno = s.barcodeno;
                                objrol.isrecollect = s.isrecollect;
                                objrol.isrecall = s.isrecall;
                                objrol.iscancelled = s.iscancelled;
                                objrol.enteredby = s.enteredby;
                                objrol.enteredon = s.enteredon;
                                objrol.validatedby = s.validatedby;
                                objrol.validatedon = s.validatedon;
                                objrol.approvedby = s.approvedby;
                                objrol.approvedon = s.approvedon;
                                objrol.isabnormal = s.isabnormal;
                                objrol.iscritical = s.iscritical;
                                objrol.istat = s.istat;
                                objrol.issms = s.issms;
                                objrol.smsdttm = s.smsdttm;
                                objrol.isemail = s.isemail;
                                objrol.emaildttm = s.emaildttm;
                                objrol.isprint = s.isprint;
                                objrol.printdttm = s.printdttm;
                                objrol.iswsms = s.iswsms;
                                objrol.whatsappdttm = s.whatsappdttm;
                                objrol.iscpreportshow = s.iscpreportshow;
                                objrol.cpreportshowdttm = s.cpreportshowdttm;
                                objrol.iscpprint = s.iscpprint;
                                objrol.cpprintdttm = s.cpprintdttm;
                                objrol.isremarks = s.isremarks;
                                objrol.iscpremarks = s.iscpremarks;
                                objrol.isservicestatus = s.isservicestatus;
                                objrol.istrand = s.istrand;
                                lstrol.Add(objrol);
                            }
                            obj.lstreportorderlist = lstrol;
                            lst.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.GetPatientReport - PatientVisitNo : " + req.patientvisitno, ExceptionPriority.High, ApplicationType.REPOSITORY, req?.venueno, req?.venuebranchno, (int)(req?.userno));
            }
            return lst;
        }
        /// <summary>
        /// Print Patient Report
        /// </summary>
        /// <param name="PatientItem"></param>
        /// <returns></returns>
        public async Task<List<ReportOutput>> PrintPatientReport(PatientReportDTO PatientItem)
        {
            string DefaultConnection = string.Empty;
            List<ReportOutput> result = new List<ReportOutput>();
            bool isdirectapprovalavail = false;
            string directapprovalorderlistno = "";
            string directapprovaltestno = "";
            string directapprovalvisitid = "";
            try
            {
                Int16 languagecode = PatientItem.pritlanguagetype != null && PatientItem.pritlanguagetype>0? PatientItem.pritlanguagetype: Convert.ToInt16(0);
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                ConfigurationDto objConfigurationDTO = new ConfigurationDto();
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();

                if (!PatientItem.isdefault)
                    DefaultConnection = _config.GetConnectionString(ConfigKeys.DefaultConnection);
                else
                    DefaultConnection = _config.GetConnectionString(ConfigKeys.ArchiveDefaultConnection);

                var lstresulttypenos = PatientItem?.resulttypenos.Split(',');
                var Key = "";
                for (int i = 0; i < lstresulttypenos?.Length; i++)
                {
                    isdirectapprovalavail = false;
                    directapprovalorderlistno = "";
                    directapprovaltestno = "";
                    directapprovalvisitid = "";
                    Key = "";
                    if (lstresulttypenos[i] == "1" && (languagecode == 1 || languagecode == 0))
                    {
                        Key = ReportKey.PATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "2" && (languagecode == 1 || languagecode == 0))
                    {
                        Key = ReportKey.MBPATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "3" && (languagecode == 1 || languagecode == 0))
                    {
                        Key = ReportKey.TEMPPATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "4" && (languagecode == 1 || languagecode == 0))
                    {
                        Key = ReportKey.MTEMPPATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "1" && languagecode > 1)
                    {
                        Key = ReportKey.LANGPATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "2" && languagecode > 1)
                    {
                        Key = ReportKey.LANGMBPATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "3" && languagecode > 1)
                    {
                        Key = ReportKey.LANGTEMPPATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "4" && languagecode > 1)
                    {
                        Key = ReportKey.LANGMTEMPPATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "11")
                    {
                        Key = ReportKey.TRNDPATIENTREPORT;
                    }
                    ReportOutput item = new ReportOutput();
                    Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                    objdictionary.Add("PageCode", PatientItem?.pagecode);
                    objdictionary.Add("PatientVisitNo", PatientItem?.patientvisitno);
                    objdictionary.Add("OrderListNos", PatientItem?.orderlistnos);
                    objdictionary.Add("IsLogo", PatientItem?.isheaderfooter.ToString());
                    objdictionary.Add("IsNABLlogo", PatientItem?.isNABLlogo.ToString());

                    objdictionary.Add("UserNo", PatientItem?.userno.ToString());
                    objdictionary.Add("VenueNo", PatientItem?.venueno.ToString());
                    objdictionary.Add("VenueBranchNo", PatientItem?.venuebranchno.ToString());
                    ReportContext objReportContext = new ReportContext(DefaultConnection);
                    TblReportMaster tblReportMaster = new TblReportMaster();

                    if (PatientItem?.pagecode == "PCRE" || PatientItem?.pagecode == "PCRV" || PatientItem?.pagecode == "PCRA")
                    {
                        //MasterRepository _IMasterRepository = new MasterRepository(_config);
                        //ConfigurationDto objConfigurationDTO = new ConfigurationDto();
                        objConfigurationDTO = new ConfigurationDto();
                        string watermarkinreportconfig = "IsWaterMarkinReport";
                        objConfigurationDTO = _IMasterRepository.GetSingleConfiguration(PatientItem?.venueno, PatientItem?.venuebranchno, watermarkinreportconfig);
                        if (objConfigurationDTO != null && objConfigurationDTO.ConfigValue == 1)
                        {
                            Key = Key + "WATERMARK";
                        }
                    }
                    using (var context = new LIMSContext(DefaultConnection))
                    {
                        tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == Key && x.VenueNo == PatientItem.venueno
                        && x.VenueBranchNo == PatientItem.venuebranchno).FirstOrDefault();
                        if (!Directory.Exists(tblReportMaster?.ExportPath))
                        {
                            Directory.CreateDirectory(tblReportMaster?.ExportPath);
                    }
                    }
                    string PatientName = string.Concat(PatientItem?.patientvisitno.Where(c => !char.IsWhiteSpace(c)));
                    string iFile = Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                    objdictionary.Add("QRCodeURL", tblReportMaster?.ExportURL + iFile);
                    objdictionary.Add("IsProvisional", PatientItem?.isProvisional.ToString());
                    objdictionary.Add("printlanguagetype", languagecode.ToString());
                    if (Key == ReportKey.MBPATIENTREPORT)
                    {
                        //MasterRepository _IMasterRepository = new MasterRepository(_config);
                        //ConfigurationDto objConfigurationDTO = new ConfigurationDto();
                        objConfigurationDTO = new ConfigurationDto();
                        string resultStatusDDAvailConfig = "IsRsultStatusDDAvail";
                        objConfigurationDTO = _IMasterRepository.GetSingleConfiguration(PatientItem.venueno, PatientItem.venuebranchno, resultStatusDDAvailConfig);
                        if (objConfigurationDTO != null && objConfigurationDTO.ConfigValue == 1)
                        {
                            objdictionary.Add("ReportStatus", PatientItem.reportstatus.ToString());
                        }
                    }
                    System.Data.DataTable datable = objReportContext.getdatatable(objdictionary, tblReportMaster?.ProcedureName);
                    if (datable != null && datable.Rows.Count > 0)
                    {
                        //directapproval - result ack page
                        int orderlistno = 0;
                        foreach (DataRow dr in datable.Rows)
                        {
                            if (datable.Columns.Contains("IsDirectApproval") && Convert.ToBoolean(dr["IsDirectApproval"]) == true)
                            {
                                isdirectapprovalavail = true;
                                if (Convert.ToInt32(dr["OrderListNo"]) != orderlistno)
                                {
                                    directapprovalorderlistno = directapprovalorderlistno != null && directapprovalorderlistno != "" ?
                                        directapprovalorderlistno + ',' + Convert.ToInt32(dr["OrderListNo"]).ToString() : Convert.ToInt32(dr["OrderListNo"]).ToString();
                                    directapprovaltestno = directapprovaltestno != null && directapprovaltestno != "" ?
                                        directapprovaltestno + ',' + Convert.ToInt32(dr["ServiceNo"]).ToString() : Convert.ToInt32(dr["ServiceNo"]).ToString();
                                    directapprovalvisitid = dr["VisitID"].ToString();
                                }
                            }
                            orderlistno = Convert.ToInt32(dr["OrderListNo"]);
                        }
                        if (isdirectapprovalavail == true && directapprovalorderlistno != null && directapprovalorderlistno != "")
                        {
                            string[] orderlsts = directapprovalorderlistno.Split(',');
                            if (orderlsts != null && orderlsts.Length > 0) {
                                for (int o = 0; o < orderlsts.Length; o++)
                                {
                                    for (int q = datable.Rows.Count - 1; q >= 0; q--)
                                    {
                                        DataRow dr = datable.Rows[q];
                                        if (dr.RowState != DataRowState.Deleted)
                                        {
                                            if (Convert.ToInt32(dr["OrderListNo"]) == Convert.ToInt32(orderlsts[o]))
                                                dr.Delete();
                                        }
                                    }
                                }
                                datable.AcceptChanges();
                            }
                        }
                    }
                    if (datable != null && datable.Rows.Count > 0)
                    {
                        if (Key == "TEMPPATIENTREPORT" || Key == "MTEMPPATIENTREPORT" || Key == "LANTEMPPATIENTREPORT" || Key == "LANMTEMPPATIENTREPORT" || Key == "TEMPPATIENTREPORTWATERMARK")
                        {
                            objAppSettingResponse = new AppSettingResponse();
                            string AppTransTemplateFilePath = "TransTemplateFilePath";
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                            // string path = _config.GetConnectionString(ConfigKeys.TransTemplateFilePath);
                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";
                            objAppSettingResponse = new AppSettingResponse();
                            string AppMasterFilePath = "MasterFilePath";
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                            string pathForReportDis = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";
                            pathForReportDis = pathForReportDis + PatientItem?.venueno.ToString() + "/T/";
                            //
                            objAppSettingResponse = new AppSettingResponse();
                            string AppDevExpressEditorConfig = "DevExpressEditorConfig";
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppDevExpressEditorConfig);
                            string deveditorconfigvalue = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.DevExpressEditorConfig);
                            string devExpressEditor = string.Empty;
                            List<ConfigurationDto> lstConfigList = new List<ConfigurationDto>();
                            IMasterRepository objMasterRepository = new MasterRepository(_config);
                            lstConfigList = objMasterRepository.GetConfigurationList(PatientItem.venueno, PatientItem.venuebranchno);
                            devExpressEditor = lstConfigList != null ? lstConfigList.Where(d => d.ConfigurationKey == deveditorconfigvalue).Select(d => d.ConfigValue).SingleOrDefault().ToString() : "";
                            //if (devExpressEditor != null && devExpressEditor == "1")
                            //{
                            string restorePath = path;
                            for (int j = 0; j < datable.Rows.Count; j++)
                            {
                                path = path + PatientItem?.venueno.ToString() + "/" + datable?.Rows[j]["orderListNo"]?.ToString() + "/" + datable?.Rows[j]["serviceNo"]?.ToString() + ".rtf";
                                if (!File.Exists(path))
                                {
                                    objAppSettingResponse = new AppSettingResponse();
                                    AppTransTemplateFilePath = "TransTemplateFilePath";
                                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                                    path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                    ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.TransTemplateFilePath);
                                    path = path + PatientItem?.venueno.ToString() + "/" + datable?.Rows[j]["orderListNo"]?.ToString() + "/" + datable?.Rows[j]["serviceNo"]?.ToString() + ".ym";
                                }
                                // }
                                //else
                                //{
                                //    path = path + PatientItem?.venueno.ToString() + "/" + datable?.Rows[0]["orderListNo"]?.ToString() + "/" + datable?.Rows[0]["serviceNo"]?.ToString() + ".ym";                       
                                //}
                                if (File.Exists(path))
                                {
                                    string content = File.ReadAllText(path);
                                    datable.Rows[j]["result"] = content;
                                    string getPath = pathForReportDis + "/ReportDisclaimer/" + datable?.Rows[j]["serviceNo"]?.ToString() + ".ym";
                                    if (File.Exists(getPath))
                                        if (datable.Rows.Count > 0 && datable.Columns.Contains("IsReportDisclaimer"))
                                            if (datable.Rows[j]["IsReportDisclaimer"].ToString() == "1")
                                                if (datable.Rows.Count > 0 && datable.Columns.Contains("ReportDisclaimer"))
                                                    datable.Rows[j]["ReportDisclaimer"] = File.ReadAllText(getPath).ToString();
                                }
                                else
                                {
                                    if (lstresulttypenos[i] == "4")
                                    {
                                        //Multi Editor Option for histopathology
                                        string extension = System.IO.Path.GetExtension(path);
                                        path = path.Substring(0, path.Length - extension.Length);
                                        if (Directory.Exists(path))
                                        {
                                            foreach (DataRow row in datable.Rows)
                                            {
                                                foreach (DataColumn column in datable.Columns)
                                                {
                                                    if (row["SubTestNo"] != null && Convert.ToInt32(row["SubTestNo"].ToString()) > 0)
                                                    {
                                                        if (column.ColumnName.ToLower() == "result") // This will check the null values also (if you want to check).
                                                        {
                                                            objAppSettingResponse = new AppSettingResponse();
                                                            string AppMultiTemplateFormat = "MultiTemplateFormat";
                                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMultiTemplateFormat);
                                                            string fileformat = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                    ? objAppSettingResponse.ConfigValue : "";//_config.GetConnectionString(ConfigKeys.MultiTemplateFormat);
                                                            string overallpath = path + "/" + row["SubTestNo"].ToString() + fileformat;
                                                            if (File.Exists(overallpath))
                                                            {
                                                                row["result"] = File.ReadAllText(overallpath);
                                                            }
                                                            else
                                                            {
                                                                row["result"] = "";
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        datable.Rows[j]["result"] = "";
                                    }
                                }
                                path = restorePath;
                            }
                        }
                        else if (Key == "MBPATIENTREPORT" || Key == "MBPATIENTREPORTWATERMARK")
                        {

                            int tno = 0;
                            string tstnotes = "";
                            //foreach (DataRow dr in datable.Rows)
                            //{
                                    if (datable.Rows[0].Table.Columns.Contains("ServiceNo") && tno != Convert.ToInt32(datable.Rows[0]["ServiceNo"]))
                                    {
                                        tstnotes = "";
                                        tno = Convert.ToInt32(datable.Rows[0]["ServiceNo"]);
                                            tstnotes = "";
                                            tno = Convert.ToInt32(datable.Rows[0]["ServiceNo"]);
                                            if (datable.Rows[0].Table.Columns.Contains("TestInter") && Convert.ToInt32(datable.Rows[0]["TestInter"]) == 1)
                                            {
                                                string FPath = Convert.ToInt32(datable.Rows[0]["ServiceNo"]).ToString() + ".ym";
                                                objAppSettingResponse = new AppSettingResponse();
                                                string AppMasterFilePath = "MasterFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                              ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.MasterFilePath);
                                                path = path + PatientItem?.venueno.ToString() + "/T/InterNotes/" + FPath;
                                                if (datable.Rows[0].Table.Columns.Contains("TestInterNotes") && File.Exists(path))
                                                {
                                                      tstnotes = File.ReadAllText(path);
                                                       //  dr["TestInterNotes"] = tstnotes != null ? tstnotes:"";
                                                      datable.AsEnumerable()
                                                             .ToList()
                                                             .ForEach(row => row.SetField("TestInterNotes", !String.IsNullOrEmpty(tstnotes) ? tstnotes : ""));
                                                 }   
                                            }   
                                    } 
                           // }
                        }

                        else if (Key == "PATIENTREPORT" || Key == "LANGPATIENTREPORT" || Key == "TRNDPATIENTREPORT" || Key == "PATIENTREPORTWATERMARK")
                        {

                            int sno = 0;
                            string grpnotes = "";
                            int tno = 0;
                            string tstnotes = "";
                            foreach (DataRow dr in datable.Rows)
                            {
                                if (Convert.ToBoolean(dr["IsGrpTestInter"]) == false)
                                {
                                    if (sno != Convert.ToInt32(dr["ServiceNo"]))
                                    {
                                        grpnotes = "";
                                        sno = Convert.ToInt32(dr["ServiceNo"]);
                                        if (Convert.ToInt32(dr["GroupInter"]) == 2)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppTransFilePath = "TransFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.TransFilePath);
                                            path = path + PatientItem?.venueno.ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["OrderListNo"]).ToString() + ".ym";
                                            if (File.Exists(path))
                                            {
                                                dr["GroupInterNotes"] = File.ReadAllText(path);
                                                grpnotes = dr["GroupInterNotes"].ToString();
                                            }
                                        }
                                        else if (Convert.ToInt32(dr["GroupInter"]) == 1)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppMasterFilePath = "MasterFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.MasterFilePath);
                                            path = path + PatientItem?.venueno.ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["ServiceNo"]).ToString() + ".ym";
                                            if (File.Exists(path))
                                            {
                                                dr["GroupInterNotes"] = File.ReadAllText(path);
                                                grpnotes = dr["GroupInterNotes"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dr["GroupInterNotes"] = grpnotes;
                                    }
                                }
                                else if (Convert.ToBoolean(dr["IsGrpTestInter"]) == true)
                                {
                                    if (tno != Convert.ToInt32(dr["TestNo"]))
                                    {
                                        tstnotes = "";
                                        tno = Convert.ToInt32(dr["TestNo"]);
                                        if (Convert.ToInt32(dr["TestInter"]) == 2)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppTransFilePath = "TransFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.TransFilePath);
                                            path = path + PatientItem?.venueno.ToString() + "/T/InterNotes/" + Convert.ToInt32(dr["OrderDetailsNo"]).ToString() + ".ym";
                                            if (File.Exists(path))
                                            {
                                                dr["TestInterNotes"] = File.ReadAllText(path);
                                                tstnotes = dr["TestInterNotes"].ToString();
                                            }
                                        }
                                        else if (Convert.ToInt32(dr["TestInter"]) == 1)
                                        {
                                            string internotesresflag = dr["resultflag"] != null ? dr["resultflag"].ToString() : "";
                                            string FPath = internotesresflag == "H" ? Convert.ToInt32(dr["TestNo"]).ToString() + "_H" + ".ym" : internotesresflag == "L" ? Convert.ToInt32(dr["TestNo"]).ToString() + "_L" + ".ym" : Convert.ToInt32(dr["TestNo"]).ToString() + ".ym";
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppMasterFilePath = "MasterFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.MasterFilePath);
                                            path = path + PatientItem?.venueno.ToString() + "/T/InterNotes/" + FPath;
                                            if (File.Exists(path))
                                            {
                                                dr["TestInterNotes"] = File.ReadAllText(path);
                                                tstnotes = dr["TestInterNotes"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dr["TestInterNotes"] = tstnotes;
                                    }
                                }
                                if (datable.Columns.Contains("IsGrpReportdisclaimer"))
                                {
                                    if (Convert.ToBoolean(dr["IsGrpReportdisclaimer"]) == true)
                                    {
                                        objAppSettingResponse = new AppSettingResponse();
                                        string AppMasterFilePath = "MasterFilePath";
                                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                        string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.MasterFilePath);
                                        path = path + PatientItem?.venueno.ToString() + "/G/ReportDisclaimer/" + dr["ServiceNo"].ToString() + ".ym";
                                        if (File.Exists(path))
                                        {
                                            dr["GrpReportDisclaimer"] = File.ReadAllText(path).ToString();
                                        }
                                        else
                                        {
                                            dr["GrpReportDisclaimer"] = "";
                                        }
                                    }
                                }
                                if (datable.Columns.Contains("IsTestReportdisclaimer"))
                                {
                                    if (Convert.ToBoolean(dr["IsTestReportdisclaimer"]) == true)
                                    {
                                        objAppSettingResponse = new AppSettingResponse();
                                        string AppMasterFilePath = "MasterFilePath";
                                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                        string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.MasterFilePath);
                                        path = path + PatientItem?.venueno.ToString() + "/T/ReportDisclaimer/" + dr["ServiceNo"].ToString() + ".ym";
                                        if (File.Exists(path))
                                        {
                                            dr["TestReportDisclaimer"] = File.ReadAllText(path).ToString();
                                        }
                                        else
                                        {
                                            dr["TestReportDisclaimer"] = "";
                                        }
                                    }
                                }
                                if (datable.Columns.Contains("GraphImage1"))
                                {
                                    //machine graph attached to the final report
                                    string GraphURL = dr["GraphURL"].ToString();
                                    objAppSettingResponse = new AppSettingResponse();
                                    string AppMachineImagePath = "MachineImagePath";
                                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMachineImagePath);
                                    string actualmachineimagepath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.MachineImagePath);                                
                                    string machineimagepath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";//  _config.GetConnectionString(ConfigKeys.MachineImagePath);
                                    machineimagepath = machineimagepath + "//" + PatientItem?.venueno + "//" + PatientItem?.venuebranchno + "//" + dr["BarcodeNoNew"].ToString();
                                    if (Directory.Exists(machineimagepath))
                                    {
                                        string[] files = Directory.GetFiles(machineimagepath);
                                        if (files != null && files.Length > 0)
                                        {
                                            for (int g = 0; g < files.Length; g++)
                                            {
                                                string machinefullpath = files[g];
                                                string serviceno = machinefullpath.Replace(machineimagepath, "").Replace("//", "").Replace("\\", "").Replace(".png", "");
                                                var servicenolst = serviceno.Split("T").Length > 1 ? serviceno.Split("T") : serviceno.Split("S");
                                                string servcno = servicenolst.Length > 1 ? servicenolst[1].ToString() : "";
                                                if (servcno == dr["TestNo"].ToString())
                                                {
                                                    if (g == 0 || dr["GraphImage1"].ToString() == "")
                                                    {
                                                        dr["GraphImage1"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
                                                    }
                                                    else if (datable.Columns.Contains("GraphImage2") && (g == 1 || dr["GraphImage2"].ToString() == ""))
                                                    {
                                                        dr["GraphImage2"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
                                                    }
                                                    else if (datable.Columns.Contains("GraphImage3") && (g == 2 || dr["GraphImage3"].ToString() == ""))
                                                    {
                                                        dr["GraphImage3"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
                                                    }
                                                    else if (datable.Columns.Contains("GraphImage4") && (g == 3 || dr["GraphImage4"].ToString() == ""))
                                                    {
                                                        dr["GraphImage4"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
                                                    }
                                                    else if (datable.Columns.Contains("GraphImage5") && (g == 4 || dr["GraphImage5"].ToString() == ""))
                                                    {
                                                        dr["GraphImage5"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //assign all images path into last test of the group if graph is available for the group test
                        if (datable.Columns.Contains("GraphImage1"))
                        {
                            var results = from DataRow myRow in datable.Rows
                                          where (string)myRow["GraphImage1"] != "" && (string)myRow["GroupName"] != ""
                                          select myRow;
                            // test have grpah image inside the group
                            if (results != null && results.ToList().Count() > 0)
                            {

                                int imagescount = 1;
                                int graphavailcount = results.ToList().Count();
                                string groupname = String.Empty;
                                for (int g = 0; g < graphavailcount; g++)
                                {
                                    var groupcountlst = from DataRow myRow in datable.Rows
                                                        where (string)myRow["GroupName"] == results.ToList()[g].ItemArray[23].ToString()
                                                        select myRow;
                                    //if grpah have more than a group test
                                    if (groupname != results.ToList()[g].ItemArray[23].ToString())
                                    {
                                        imagescount = 1;
                                    }
                                    int groupcount = groupcountlst != null ? groupcountlst.ToList().Count() : 0;
                                    string graph1 = results.ToList()[g].ItemArray[79].ToString();
                                    string graph2 = results.ToList()[g].ItemArray[80].ToString();
                                    string graph3 = results.ToList()[g].ItemArray[81].ToString();
                                    string graph4 = results.ToList()[g].ItemArray[82].ToString();
                                    string graph5 = results.ToList()[g].ItemArray[83].ToString();

                                    int dtgroupcount = 0;
                                    foreach (DataRow dtrow in datable.Rows)
                                    {
                                        if (dtrow["GroupName"].ToString() != "" && dtrow["GroupName"].ToString() != null && dtrow["GroupName"].ToString() == results.ToList()[g].ItemArray[23].ToString())
                                        {
                                            dtgroupcount = dtgroupcount + 1;
                                            if (groupcount == dtgroupcount)
                                            {
                                                if (imagescount == 1)
                                                {
                                                    dtrow["GraphImage1"] = graph1;
                                                    if (graph2 != "") { dtrow["GraphImage2"] = graph2; }
                                                    if (graph3 != "") { dtrow["GraphImage3"] = graph3; }
                                                    if (graph4 != "") { dtrow["GraphImage4"] = graph4; }
                                                    if (graph5 != "") { dtrow["GraphImage5"] = graph5; }
                                                }
                                                if (imagescount == 2)
                                                {
                                                    if (graph1 != "" && dtrow["GraphImage2"].ToString() == "") { dtrow["GraphImage2"] = graph1; }
                                                    if (graph2 != "" && dtrow["GraphImage3"].ToString() == "") { dtrow["GraphImage3"] = graph2; }
                                                    if (graph3 != "" && dtrow["GraphImage4"].ToString() == "") { dtrow["GraphImage4"] = graph3; }
                                                    if (graph4 != "" && dtrow["GraphImage5"].ToString() == "") { dtrow["GraphImage5"] = graph4; }
                                                }
                                                if (imagescount == 3)
                                                {
                                                    if (graph1 != "" && dtrow["GraphImage3"].ToString() == "") { dtrow["GraphImage3"] = graph1; }
                                                    if (graph2 != "" && dtrow["GraphImage4"].ToString() == "") { dtrow["GraphImage4"] = graph2; }
                                                    if (graph3 != "" && dtrow["GraphImage5"].ToString() == "") { dtrow["GraphImage5"] = graph3; }
                                                }
                                                if (imagescount == 4)
                                                {
                                                    if (graph1 != "" && dtrow["GraphImage4"].ToString() == "") { dtrow["GraphImage4"] = graph1; }
                                                    if (graph2 != "" && dtrow["GraphImage5"].ToString() == "") { dtrow["GraphImage5"] = graph2; }
                                                }
                                                if (imagescount == 5)
                                                {
                                                    if (graph1 != "" && dtrow["GraphImage5"].ToString() == "") { dtrow["GraphImage5"] = graph1; }
                                                }
                                            }
                                        }
                                    }
                                    groupname = results.ToList()[g].ItemArray[23].ToString();
                                    imagescount = imagescount + 1;
                                }
                                string ggroupname = String.Empty;
                                //remove graph path for all other test name except last test of group 
                                for (int g = 0; g < graphavailcount; g++)
                                {
                                    var groupcountlst = from DataRow myRow in datable.Rows
                                                        where (string)myRow["GroupName"] == results.ToList()[g].ItemArray[23].ToString()
                                                        select myRow;
                                    if (groupcountlst != null && groupcountlst.ToList().Count() > 0)
                                    {
                                        string lastGrpTestName = groupcountlst != null ? groupcountlst.ToList()[groupcountlst.ToList().Count() - 1].ItemArray[30].ToString() : "";
                                        string lastGrpTestNo = groupcountlst != null ? groupcountlst.ToList()[groupcountlst.ToList().Count() - 1].ItemArray[29].ToString() : "";
                                        if (lastGrpTestName != null && lastGrpTestName != "")
                                        {
                                            if (ggroupname != results.ToList()[g].ItemArray[23].ToString() && results.ToList()[g].ItemArray[23].ToString() != "")
                                            {
                                                foreach (DataRow dtbrow in datable.Rows)
                                                {
                                                    if (dtbrow["GroupName"].ToString() != "" && dtbrow["GroupName"].ToString() == results.ToList()[g].ItemArray[23].ToString() &&
                                                        (dtbrow["TestName"].ToString() != lastGrpTestName))
                                                    {
                                                        //dtbrow["GraphImage1"] = dtbrow["GraphImage1"].ToString().Replace(dtbrow["GraphImage1"].ToString(),"");
                                                        //dtbrow["GraphImage2"] = dtbrow["GraphImage2"].ToString().Replace(dtbrow["GraphImage2"].ToString(),"");
                                                        //dtbrow["GraphImage3"] = dtbrow["GraphImage3"].ToString().Replace(dtbrow["GraphImage3"].ToString(),"");
                                                        //dtbrow["GraphImage4"] = dtbrow["GraphImage4"].ToString().Replace(dtbrow["GraphImage4"].ToString(),"");
                                                        //dtbrow["GraphImage5"] = dtbrow["GraphImage5"].ToString().Replace(dtbrow["GraphImage5"].ToString(), "");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    ggroupname = results.ToList()[g].ItemArray[23].ToString();
                                }
                            }
                        }
                        //

                        ReportParamDto objitem = new ReportParamDto();
                        objitem.datatable = CommonExtension.DatableToDicionary(datable);
                        objitem.paramerter = objdictionary;
                        objitem.ReportPath = tblReportMaster.ReportPath;
                        objitem.ExportPath = tblReportMaster.ExportPath + iFile;
                        objitem.ExportFormat = FileFormat.PDF;
                        string ReportParam = JsonConvert.SerializeObject(objitem);
                        objAppSettingResponse = new AppSettingResponse();
                        string AppReportServiceURL = "ReportServiceURL";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppReportServiceURL);
                        string ReportServiceURL = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";
                        string filename = await ExportReportService.ExportPrint(ReportParam, ReportServiceURL);
                        if (PatientItem?.process == 3)
                            item.PatientExportFile = tblReportMaster.ExportURL + filename;// CommonHelper.URLShorten(tblReportMaster.ExportURL + filename, _config.GetConnectionString(ConfigKeys.FireBaseAPIkey));
                        else
                            item.PatientExportFile = tblReportMaster.ExportURL + filename;

                        item.PatientExportFolderPath = objitem.ExportPath;
                        //attached outsourced documents
                        objAppSettingResponse = new AppSettingResponse();
                        string AppResultAckUpload = "ResultAckUpload";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload);
                        string ackpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.ResultAckUpload);
                        int oldtestno = 0; int newtestno = 0;
                        int isbillmerged = 0, isDeltaMerged = 0;
                        foreach (DataRow row in datable.Rows)
                        {
                            if (datable.Columns.Contains("BillIncluded") && isbillmerged == 0 && (PatientItem?.patientreportwithbill != null && PatientItem?.patientreportwithbill > 0))
                            {
                                int BillIncluded = row["BillIncluded"] != null && row["BillIncluded"] != "" ? Convert.ToInt32(row["BillIncluded"]) : 0;
                                if (BillIncluded > 0)
                                {
                                    foreach (DataColumn column in datable.Columns)
                                    {
                                        string billfullpath = "";
                                        ReportOutput obj = new ReportOutput();
                                        ReportRequestDTO reqstbill = new ReportRequestDTO();
                                        FrontOfficeRepository _IFrontOfficeRepository = new FrontOfficeRepository(_config);

                                        reqstbill.VenueNo = PatientItem.venueno;
                                        reqstbill.VenueBranchNo = PatientItem.venuebranchno;
                                        reqstbill.visitNo = PatientItem?.patientvisitno != null && PatientItem?.patientvisitno != "" ? Convert.ToInt32(PatientItem?.patientvisitno) : 0;
                                        reqstbill.userNo = PatientItem.userno;
                                        reqstbill.print = "PATIENTBILL";//"PATIENTBILLSFORMAT"
                                        obj = await _IFrontOfficeRepository.PrintBill(reqstbill);
                                        billfullpath = obj != null ? obj.PatientExportFolderPath : "";

                                        if (billfullpath != null && billfullpath != "")
                                        {
                                            using (PdfDocument one = PdfReader.Open(objitem.ExportPath, PdfDocumentOpenMode.Import))
                                            using (PdfDocument two = PdfReader.Open(billfullpath, PdfDocumentOpenMode.Import))
                                            using (PdfDocument outPdf = new PdfDocument())
                                            {
                                                CopyPages(one, outPdf);
                                                CopyPages(two, outPdf);

                                                outPdf.Save(objitem.ExportPath);
                                            }
                                        }
                                    }
                                }
                                isbillmerged = 1;
                            }
                            if (datable.Columns.Contains("DeltaReportIncluded") && isDeltaMerged == 0)
                            {
                                int DeltaReportIncluded = row["DeltaReportIncluded"] != null && row["DeltaReportIncluded"] != "" ? Convert.ToInt32(row["DeltaReportIncluded"]) : 0;
                                if (DeltaReportIncluded > 0)
                                {
                                    foreach (DataColumn column in datable.Columns)
                                    {
                                        if (isDeltaMerged == 0)
                                        {
                                            string billfullpath = ""; string Key1 = "DELTAREPORT";
                                            FrontOfficeRepository _IFrontOfficeRepository = new FrontOfficeRepository(_config);
                                            Dictionary<string, string> objdictionary1 = new Dictionary<string, string>();

                                            objdictionary1.Add("PageCode", PatientItem?.pagecode);
                                            objdictionary1.Add("PatientVisitNo", PatientItem?.patientvisitno);
                                            objdictionary1.Add("OrderListNos", PatientItem?.orderlistnos);
                                            objdictionary1.Add("IsLogo", PatientItem?.isheaderfooter.ToString());
                                            objdictionary1.Add("IsNABLlogo", PatientItem?.isNABLlogo.ToString());

                                            objdictionary1.Add("UserNo", PatientItem?.userno.ToString());
                                            objdictionary1.Add("VenueNo", PatientItem?.venueno.ToString());
                                            objdictionary1.Add("VenueBranchNo", PatientItem?.venuebranchno.ToString());
                                            ReportContext objReportContext1 = new ReportContext(DefaultConnection);
                                            TblReportMaster tblReportMaster1 = new TblReportMaster();
                                            using (var context = new LIMSContext(DefaultConnection))
                                            {
                                                tblReportMaster1 = context.TblReportMaster.Where(x => x.ReportKey == Key1 && x.VenueNo == PatientItem.venueno
                                                && x.VenueBranchNo == PatientItem.venuebranchno).FirstOrDefault();
                                                if (!Directory.Exists(tblReportMaster1.ExportPath))
                                                {
                                                    Directory.CreateDirectory(tblReportMaster1.ExportPath);
                                                }
                                            }
                                            string PatientName1 = string.Concat(PatientItem?.patientvisitno.Where(c => !char.IsWhiteSpace(c)));
                                            string iFile1 = "d_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";

                                            System.Data.DataTable datable1 = objReportContext1.getdatatable(objdictionary1, tblReportMaster1.ProcedureName);
                                            ReportParamDto objitem1 = new ReportParamDto();
                                            objitem1.datatable = CommonExtension.DatableToDicionary(datable1);
                                            objitem1.paramerter = objdictionary1;
                                            objitem1.ReportPath = tblReportMaster1.ReportPath;
                                            objitem1.ExportPath = tblReportMaster1.ExportPath + iFile1;
                                            objitem1.ExportFormat = FileFormat.PDF;
                                            string ReportParam1 = JsonConvert.SerializeObject(objitem1);
                                            objAppSettingResponse = new AppSettingResponse();
                                            AppReportServiceURL = "ReportServiceURL";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppReportServiceURL);
                                            ReportServiceURL = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                ? objAppSettingResponse.ConfigValue : "";
                                            string filename1 = await ExportReportService.ExportPrint(ReportParam1, ReportServiceURL);
                                            billfullpath = tblReportMaster1.ExportPath + filename1;
                                            if (billfullpath != null && billfullpath != "")
                                            {
                                                using (PdfDocument one = PdfReader.Open(objitem.ExportPath, PdfDocumentOpenMode.Import))
                                                using (PdfDocument two = PdfReader.Open(billfullpath, PdfDocumentOpenMode.Import))
                                                using (PdfDocument outPdf = new PdfDocument())
                                                {
                                                    CopyPages(one, outPdf);
                                                    CopyPages(two, outPdf);

                                                    outPdf.Save(objitem.ExportPath);
                                                }
                                            }
                                        }
                                        isDeltaMerged = 1;
                                    }
                                }
                            }
                            if (datable.Columns.Contains("ACK_TestNo") && datable.Columns.Contains("IsIncludeInReport"))
                            {
                                foreach (DataColumn column in datable.Columns)
                                {
                                    if (Convert.ToBoolean(row["IsIncludeInReport"]) == true && row["VisitID"] != null && column.ColumnName == "VisitID")
                                    {
                                        newtestno = row["ACK_TestNo"] != null && row["ACK_TestNo"].ToString() != "" ? Convert.ToInt32(row["ACK_TestNo"]) : 0;
                                        if (newtestno != oldtestno)
                                        { //if test have subtest then lnly once need to add outsource report
                                            objAppSettingResponse = new AppSettingResponse();
                                            AppResultAckUpload = "ResultAckUpload";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload);
                                            ackpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ?
                                                objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.ResultAckUpload);
                                            //ackpath = ackpath + "//" + PatientItem?.venueno + "//" + PatientItem?.venuebranchno + "//" + row["VisitID"] + "//" + row["TestNo"];
                                            ackpath = ackpath + "//" + PatientItem?.venueno + "//" + PatientItem?.patientvisitno + "//" + row["ACK_TestNo"];
                                            if (Directory.Exists(ackpath))
                                            {
                                                string[] files = Directory.GetFiles(ackpath);
                                                if (files != null && files.Length > 0)
                                                {
                                                    for (int f = 0; f < files.Length; f++)
                                                    {
                                                        string resultname = Path.GetFileName(files[f]);
                                                        string ackfullpath = files[f];// ackpath + "//" + resultname;
                                                        string ackfilename = Path.GetFileNameWithoutExtension(ackfullpath);
                                                        string extension = Path.GetExtension(files[f]);
                                                        if (extension != null && (extension.ToLower() == ".pdf" || extension.ToLower() == "pdf"))
                                                        {   
                                                            using (PdfDocument one = PdfReader.Open(objitem.ExportPath, PdfDocumentOpenMode.Import))
                                                            using (PdfDocument two = PdfReader.Open(ackfullpath, PdfDocumentOpenMode.Import))
                                                            using (PdfDocument outPdf = new PdfDocument())
                                                            {
                                                                CopyPages(one, outPdf);
                                                                CopyPages(two, outPdf);

                                                                outPdf.Save(objitem.ExportPath);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        oldtestno = newtestno;
                                    }
                                }
                            }
                        }
                        result.Add(item);
                    }
                    //result ack - direct approval - without dummy report 
                    string AppResultAckUpload3 = "ResultAckUpload";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload3);
                    string resultackuplod = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                                    ? objAppSettingResponse.ConfigValue : "";                    
                    if (resultackuplod != null && resultackuplod != "" && directapprovaltestno != null && directapprovaltestno !="")
                    {
                        item = new ReportOutput();
                        var overallackfiles = new List<string>();                        
                        //if (result != null && result.Count > 0)
                        //{
                        //}
                        //else
                        {
                            if (directapprovaltestno != null && directapprovaltestno != "")
                            {
                                var directapprovaltestnolst = directapprovaltestno.Split(',');
                                if (directapprovaltestnolst != null && directapprovaltestnolst.Length > 0)
                                {
                                    for (int p = 0; p < directapprovaltestnolst.Length; p++)
                                    {
                                        //string ackfolderName = PatientItem.venueno.ToString() + "//" + PatientItem.venuebranchno.ToString() + "//" + directapprovalvisitid + "//" + directapprovaltestnolst[p].ToString();
                                        //string ackfolderNameNew = PatientItem.venueno.ToString() + "//" + PatientItem.venuebranchno.ToString() + "//" + directapprovalvisitid + "//" + directapprovaltestnolst[p].ToString()+"//MergedFolder";
                                        string ackfolderName = PatientItem.venueno.ToString() +  "//" + PatientItem?.patientvisitno + "//" + directapprovaltestnolst[p].ToString();
                                        string ackfolderNameNew = PatientItem.venueno.ToString() + "//" + PatientItem?.patientvisitno + "//" + directapprovaltestnolst[p].ToString() + "//MergedFolder";
                                        string newPath = Path.Combine(resultackuplod, ackfolderName);
                                        string newPathNew = Path.Combine(resultackuplod, ackfolderNameNew);                                        
                                        if (!Directory.Exists(newPathNew))
                                        {
                                            Directory.CreateDirectory(newPathNew);
                                        }
                                        else
                                        {
                                            System.IO.DirectoryInfo di = new DirectoryInfo(newPathNew);
                                            foreach (FileInfo itemv in di.GetFiles())
                                            {
                                                itemv.Delete();
                                            }
                                        }
                                        newPathNew = newPathNew + "//" + directapprovalvisitid+".pdf";                                        
                                        if (Directory.Exists(newPath))
                                        {
                                            string[] filePaths = Directory.GetFiles(newPath);
                                            if (filePaths != null && filePaths.Length > 1)
                                            {
                                                for (int f = 0; f < filePaths.Length; f++)
                                                {
                                                    if (f == 0)
                                                    {
                                                        string path = filePaths[f].ToString();
                                                        Byte[] bytes = System.IO.File.ReadAllBytes(path);
                                                        String base64String = Convert.ToBase64String(bytes);
                                                        byte[] imageBytes = Convert.FromBase64String(base64String);
                                                        System.IO.File.WriteAllBytes(newPathNew, imageBytes);
                                                    }                                                                                                        
                                                    if (f >0 && f < filePaths.Length)
                                                    {
                                                        string path2 = filePaths[f].ToString();

                                                        using (PdfDocument one = PdfReader.Open(newPathNew, PdfDocumentOpenMode.Import))
                                                        using (PdfDocument two = PdfReader.Open(path2, PdfDocumentOpenMode.Import))
                                                        using (PdfDocument outPdf = new PdfDocument())
                                                        {
                                                            CopyPages(one, outPdf);
                                                            CopyPages(two, outPdf);

                                                            outPdf.Save(newPathNew);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (filePaths != null && filePaths.Length > 0)
                                            {
                                                string path = filePaths[0].ToString();
                                                Byte[] bytes = System.IO.File.ReadAllBytes(path);
                                                String base64String = Convert.ToBase64String(bytes);
                                                byte[] imageBytes = Convert.FromBase64String(base64String);
                                                System.IO.File.WriteAllBytes(newPathNew, imageBytes);
                                            }
                                            overallackfiles.Add(newPathNew);
                                        }                                           
                                    }
                                    tblReportMaster.ExportPath = tblReportMaster?.ExportPath;
                                    if (!Directory.Exists(tblReportMaster?.ExportPath))
                                    {
                                        Directory.CreateDirectory(tblReportMaster?.ExportPath);
                                    }                                    
                                    string iFile2 = directapprovalvisitid + "_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                                    string actpath = tblReportMaster?.ExportPath + "//" + iFile2;
                                    if (overallackfiles != null && overallackfiles.Count > 0)
                                    {
                                        for (int j = 0; j < overallackfiles.Count; j++)
                                        {
                                            if (j == 0)
                                            {
                                                string path = overallackfiles[0].ToString();
                                                Byte[] bytes = System.IO.File.ReadAllBytes(path);
                                                String base64String = Convert.ToBase64String(bytes);
                                                byte[] imageBytes = Convert.FromBase64String(base64String);
                                                System.IO.File.WriteAllBytes(actpath, imageBytes);
                                            }
                                            else
                                            {
                                                string path2 = overallackfiles[j].ToString();
                                                using (PdfDocument one = PdfReader.Open(actpath, PdfDocumentOpenMode.Import))
                                                using (PdfDocument two = PdfReader.Open(path2, PdfDocumentOpenMode.Import))
                                                using (PdfDocument outPdf = new PdfDocument())
                                                {
                                                    CopyPages(one, outPdf);
                                                    CopyPages(two, outPdf);

                                                    outPdf.Save(actpath);
                                                }
                                            }
                                        }
                                        item.PatientExportFile = tblReportMaster.ExportURL + iFile2;
                                        item.PatientExportFolderPath = actpath;
                                        result.Add(item);
                                    }
                                }
                            }
                        }
                    }
                    //
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.PrintPatientReport/patientvisitno-" + PatientItem?.patientvisitno, ExceptionPriority.High, ApplicationType.REPOSITORY, PatientItem?.venueno, PatientItem?.venuebranchno, PatientItem?.userno);
            }
            return result;
        }
        void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }

        /// <summary>
        /// Get CSA Transaction
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<TblCsatransaction> GetCsaTransaction(CsaRequest req)
        {
            List<TblCsatransaction> objresult = new List<TblCsatransaction>();
            try
            {
                using (var context = new PatientReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", req.fromdate);
                    var _ToDate = new SqlParameter("ToDate", req.todate);
                    var _Type = new SqlParameter("Type", req.type);
                    var _pageIndex = new SqlParameter("PageIndex", req.pageIndex);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    objresult = context.TblCsatransaction.FromSqlRaw("Execute dbo.Pro_CsaTransactionDetails @FROMDate,@ToDate,@Type,@PageIndex,@VenueNo,@VenueBranchNo", _FromDate, _ToDate, _Type, _pageIndex, _VenueNo, _VenueBranchNo).ToList();
                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.GetCsaTransaction", ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return objresult;
        }
        /// <summary>
        /// Insert CSA Acknowledged
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public int InsertCSAAcknowledgement(TblCsatransaction req)
        {
            int result = 0;
            try
            {
                using (var context = new PatientReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _CsatransactionNo = new SqlParameter("CsatransactionNo", req.CsatransactionNo);
                    var objresult = context.InsertCsatransaction.FromSqlRaw("Execute dbo.Pro_InsertCSAAcknowledgement @CsatransactionNo", _CsatransactionNo).FirstOrDefault();
                    result = objresult?.result ?? 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.InsertCSAAcknowledgement/CustomerNo-" + req.CustomerNo, ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }

        //Report Log Insert
        public int InsertReportLog(PatientReportLog results)
        {
            int rtnresult = 0;
            try
            {
                using (var context = new PatientReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", results.PatientVisitNo);
                    var _OrderListNo = new SqlParameter("OrderListNo", results.OrderListNo);
                    var _VisitTestNo = new SqlParameter("VisitTestNo", results.VisitTestNo);
                    var _VisitTestType = new SqlParameter("VisitTestType", results?.VisitTestType ?? "");
                    var _LogType = new SqlParameter("LogType", results?.LogType ?? "");
                    var _UserType = new SqlParameter("UserType", results.UserType);
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo);
                    var _ReportUserNo = new SqlParameter("ReportUserNo", results.ReportUserNo);

                    var objresult = context.InsertReportLog.FromSqlRaw(
                        "Execute dbo.Pro_InsertPatientReportLog " +
                        "@PatientVisitNo, @OrderListNo, @VisitTestNo, @VisitTestType, @LogType, @UserType, @VenueNo, @VenueBranchNo, @ReportUserNo",
                        _PatientVisitNo, _OrderListNo, _VisitTestNo, _VisitTestType, _LogType, _UserType, _VenueNo, _VenueBranchNo, _ReportUserNo).ToList();

                    rtnresult = objresult?[0]?.ReportLogNo ?? 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.InsertReportLog/PatientVisitNo-" + results.PatientVisitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, 0);
            }
            return rtnresult;
        }

        public List<PatientReportLogRespose> GetReportLog(PatientReportLog req)
        {
            List<PatientReportLogRespose> lst = new List<PatientReportLogRespose>();
            PatientReportLogRespose objReportLogRespose = new PatientReportLogRespose();
            try
            {
                using (var context = new PatientReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _patientVisitNo = new SqlParameter("PatientVisitNo", req.PatientVisitNo);
                    var _visitTestNo = new SqlParameter("VisitTestNo", req.VisitTestNo);
                    var _venueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);

                    var rtndblst = context.GetReportLog.FromSqlRaw(
                        "Execute dbo.Pro_GetPatientReportLog @PatientVisitNo,@VisitTestNo,@VenueNo,@VenueBranchNo",
                        _patientVisitNo, _visitTestNo, _venueNo, _venueBranchNo).ToList();

                    rtndblst = rtndblst.OrderBy(a => a.LogDTTM).ToList();
                    foreach (var v in rtndblst)
                    {
                        objReportLogRespose = new PatientReportLogRespose();
                        objReportLogRespose.ReportLogDTTM = v.ReportLogDTTM;
                        objReportLogRespose.LogType = v.LogType;
                        objReportLogRespose.ReportUser = v.ReportUser;
                        objReportLogRespose.UserType = v.UserType;
                        lst.Add(objReportLogRespose);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.GetReportLog/patientvisitno-" + req.PatientVisitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return lst;
        }
        /// <summary>
        /// Delta Report Print Separately
        /// </summary>
        /// <param name="PatientItem"></param>
        /// <returns></returns>
        /// 
        public async Task<List<ReportOutput>> PrintDelateReport(PatientReportDTO PatientItem)
        {
            string DefaultConnection = string.Empty;
            List<ReportOutput> result = new List<ReportOutput>();
            try
            {
                if (!PatientItem.isdefault)
                    DefaultConnection = _config.GetConnectionString(ConfigKeys.DefaultConnection);
                else
                    DefaultConnection = _config.GetConnectionString(ConfigKeys.ArchiveDefaultConnection);

                var lstresulttypenos = PatientItem?.resulttypenos.Split(',');
                var Key = "";
                for (int i = 0; i < lstresulttypenos?.Length; i++)
                {
                    Key = "";
                    if (lstresulttypenos[i] == "6")
                    {
                        Key = "DELTAREPORT";
                    }

                    ReportOutput item = new ReportOutput();
                    Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                    objdictionary.Add("PageCode", PatientItem?.pagecode);
                    objdictionary.Add("PatientVisitNo", PatientItem?.patientvisitno);
                    objdictionary.Add("OrderListNos", PatientItem?.orderlistnos);
                    objdictionary.Add("IsLogo", PatientItem?.isheaderfooter.ToString());
                    objdictionary.Add("IsNABLlogo", PatientItem?.isNABLlogo.ToString());

                    objdictionary.Add("UserNo", PatientItem?.userno.ToString());
                    objdictionary.Add("VenueNo", PatientItem?.venueno.ToString());
                    objdictionary.Add("VenueBranchNo", PatientItem?.venuebranchno.ToString());
                    ReportContext objReportContext = new ReportContext(DefaultConnection);
                    TblReportMaster tblReportMaster = new TblReportMaster();
                    using (var context = new LIMSContext(DefaultConnection))
                    {
                        tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == "DELTAREPORT" && x.VenueNo == PatientItem.venueno
                        && x.VenueBranchNo == PatientItem.venuebranchno).FirstOrDefault();
                        if (!Directory.Exists(tblReportMaster.ExportPath))
                        {
                            Directory.CreateDirectory(tblReportMaster.ExportPath);
                        }
                    }
                    string PatientName = string.Concat(PatientItem?.patientvisitno.Where(c => !char.IsWhiteSpace(c)));
                    string iFile = "d_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                    objdictionary.Add("QRCodeURL", tblReportMaster.ExportURL + iFile);

                    System.Data.DataTable datable = objReportContext.getdatatable(objdictionary, tblReportMaster.ProcedureName);

                    ReportParamDto objitem = new ReportParamDto();
                    objitem.datatable = CommonExtension.DatableToDicionary(datable);
                    objitem.paramerter = objdictionary;
                    objitem.ReportPath = tblReportMaster.ReportPath;
                    objitem.ExportPath = tblReportMaster.ExportPath + iFile;
                    objitem.ExportFormat = FileFormat.PDF;
                    string ReportParam = JsonConvert.SerializeObject(objitem);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    string AppReportServiceURL = "ReportServiceURL";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppReportServiceURL);
                    string serviceurl = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";
                    string filename = await ExportReportService.ExportPrint(ReportParam, serviceurl);
                    if (PatientItem?.process == 3)
                        item.PatientExportFile = tblReportMaster.ExportURL + filename;// CommonHelper.URLShorten(tblReportMaster.ExportURL + filename, _config.GetConnectionString(ConfigKeys.FireBaseAPIkey));
                    else
                        item.PatientExportFile = tblReportMaster.ExportURL + filename;

                    item.PatientExportFolderPath = objitem.ExportPath;
                    result.Add(item);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.PrintDelateReport/patientvisitno-" + PatientItem?.patientvisitno, ExceptionPriority.High, ApplicationType.REPOSITORY, PatientItem?.venueno, PatientItem?.venuebranchno, PatientItem?.userno);
            }
            return result;
        }

        //template content search
        public TempalteSearchResponse GetPatientImpressionBasedReport(requestpatientreport req)
        {
            TempalteSearchResponse objOut = new TempalteSearchResponse();
            List<PatientImpressionResponse> lstPatientInfoResponse = new List<PatientImpressionResponse>();
            try
            {
                using (var context = new PatientReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _fromdate = new SqlParameter("fromdate", req.fromdate);
                    var _todate = new SqlParameter("todate", req.todate);
                    var _type = new SqlParameter("type", req.type);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _customerno = new SqlParameter("customerno", req.customerno);
                    var _physicianno = new SqlParameter("physicianno", req.physicianno);
                    var _departmentno = new SqlParameter("deptno", req.deptno);
                    var _serviceno = new SqlParameter("serviceno", req.serviceno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    //
                    var _pagecode = new SqlParameter("pagecode", req?.pagecode);
                    var _userno = new SqlParameter("userno", req?.userno);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req?.viewvenuebranchno);
                    var _pageindex = new SqlParameter("pageindex", req?.pageindex);
                    var _patientno = new SqlParameter("patientno", req?.patientno);
                    var _servicetype = new SqlParameter("servicetype", req?.servicetype);
                    var _refferraltypeno = new SqlParameter("refferraltypeno", req?.refferraltypeno);
                    var _riderno = new SqlParameter("riderno", req?.riderno);
                    var _excutiveno = new SqlParameter("excutiveno", req?.excutiveno);
                    var _isstat = new SqlParameter("isstat", req?.isstat);
                    var _isdue = new SqlParameter("isdue", req?.isdue);
                    var _isabnormal = new SqlParameter("isabnormal", req?.isabnormal);
                    var _iscritical = new SqlParameter("iscritical", req?.iscritical);
                    var _istat = new SqlParameter("istat", req?.istat);
                    var _orderstatus = new SqlParameter("orderstatus", req?.orderstatus);
                    var _printstatus = new SqlParameter("printstatus", req?.printstatus);
                    var _cpprintstatus = new SqlParameter("cpprintstatus", req?.cpprintstatus);
                    var _smsstatus = new SqlParameter("smsstatus", req?.smsstatus);
                    var _emailstatus = new SqlParameter("emailstatus", req?.emailstatus);
                    var _loginType = new SqlParameter("loginType", req?.loginType);
                    var _routeNo = new SqlParameter("routeNo", req?.routeNo);
                    var _Deliverymode = new SqlParameter("Deliverymode", req?.deliverymode);
                    var _WardNo = new SqlParameter("wardNo", req?.wardNo);
                    var _maindeptNo = new SqlParameter("maindeptNo", req?.maindeptNo);
                    //

                    List<PatientDataImpressionResponse> result = context.GetPatientImpressionListReport.FromSqlRaw(
                        "Execute dbo.Pro_GetPatientSearchImpressionReport @pagecode,@venueno,@venuebranchno,@userno,@viewvenuebranchno,@pageindex,@type,@fromdate,@todate,@patientno,@patientvisitno,@deptno,@serviceno,@servicetype,@refferraltypeno,@customerno,@physicianno,@riderno,@excutiveno,@isstat,@isdue,@isabnormal,@iscritical,@istat,@orderstatus,@printstatus,@cpprintstatus,@smsstatus,@emailstatus,@loginType,@routeNo,@Deliverymode,@wardNo,@maindeptNo",
                    _pagecode, _venueno, _venuebranchno, _userno, _viewvenuebranchno, _pageindex, _type, _fromdate, _todate, _patientno, _patientvisitno, _departmentno, _serviceno, _servicetype, _refferraltypeno, _customerno, _physicianno, _riderno, _excutiveno, _isstat, _isdue, _isabnormal, _iscritical, _istat, _orderstatus, _printstatus, _cpprintstatus, _smsstatus, _emailstatus, _loginType, _routeNo, _Deliverymode, _WardNo, _maindeptNo).ToList();

                    string availorderlisno = string.Empty;
                    string availvisitno = string.Empty;

                    foreach (var resultItem in result)
                    {
                        var path = _config.GetValue<string>(ConfigKeys.TransTemplateFilePath);
                        path = path + req.venueno.ToString() + "\\" + resultItem.OrderListNo.ToString();

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        if (!string.IsNullOrWhiteSpace(req.searchKey) || req.patientvisitno != 0)
                        {
                            var extensions = new List<string> { ".ym", ".rtf" };
                            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                                                .Where(f => extensions.IndexOf(Path.GetExtension(f)) >= 0).ToArray();

                            foreach (string file in files)
                            {
                                string[] lines = File.ReadAllLines(file);
                                string firstOccurrence = lines.FirstOrDefault(l => l.ToLower().Contains(req.searchKey.ToLower()));
                                if (firstOccurrence != null)
                                {
                                    if (availorderlisno != null && availorderlisno != "")
                                    {
                                        availorderlisno = availorderlisno + ',' + resultItem.OrderListNo.ToString();
                                        availvisitno = availvisitno + ',' + resultItem.PatientVisitNo.ToString();
                                    }
                                    else
                                    {
                                        availorderlisno = resultItem.OrderListNo.ToString();
                                        availvisitno = resultItem.PatientVisitNo.ToString();
                                    }
                                }
                            }
                        }
                    }
                    objOut.orderlistno = availorderlisno;
                    objOut.patientvisitno = availvisitno;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetPatientImpressionListReport", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return objOut;
        }
        public List<GetAuditReportRes> GetAuditTrailReport(GetAuditReportReq req)
        {
            List<GetAuditReportRes> objresult = new List<GetAuditReportRes>();
            try
            {
                using (var context = new PatientReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FROMDate = new SqlParameter("FROMDate", req.FROMDate);
                    var _ToDate = new SqlParameter("ToDate", req.ToDate);
                    var _Type = new SqlParameter("Type", req.Type);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _ATTypeNo = new SqlParameter("ATTypeNo", req.ATTypeNo);
                    var _ATCatyNo = new SqlParameter("ATCatyNo", req.ATCatyNo);
                    var _ATSubCatyNo = new SqlParameter("ATSubCatyNo", req.ATSubCatyNo);
                    var _ATFieldNo = new SqlParameter("ATFieldNo", req.ATFieldNo);
                    var _UserNo = new SqlParameter("UserNo", req.UserNo);
                    var _PageIndex = new SqlParameter("PageIndex", req.PageIndex);
                    var _Patientvisitno = new SqlParameter("Patientvisitno", req.Patientvisitno);
                    var _pageCount = new SqlParameter("pageCount", req.pageCount);
                    var _searchTypeCode = new SqlParameter("SearchTypeCode", req.searchTypeCode);
                    var _searchTypeValue = new SqlParameter("SearchTypeValue", req.searchTypeValue);

                    var lst = context.GetAuditTrailReport.FromSqlRaw("Execute dbo.Pro_GetAuditTrailLogs " +
                        "@FROMDate, @ToDate, @Type, @VenueNo, @VenueBranchNo, @ATTypeNo, @ATCatyNo, @ATSubCatyNo, @ATFieldNo, " +
                        "@UserNo, @PageIndex, @Patientvisitno, @pageCount, @SearchTypeCode, @SearchTypeValue",
                        _FROMDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _ATTypeNo, _ATCatyNo, _ATSubCatyNo, _ATFieldNo, 
                        _UserNo, _PageIndex, _Patientvisitno, _pageCount, _searchTypeCode, _searchTypeValue).ToList();

                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();

                    string AppTransTemplateFilePath = "TransTemplateFilePath";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                    string TemplateFilePath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";

                    string AppTransTemplateAuditFileUrl = "AuditTrailTemplateReportURL";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateAuditFileUrl);
                    string TemplateAuditFileUrl = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";

                    string AuditFilePath = TemplateFilePath + "AuditTrail" + "\\" + req.VenueNo.ToString() + "\\";
                    string OldFile = "OldData";
                    string NewFile = "NewData";
                    string ReadPath = "";
                    int width = 800;
                    int height = 0;
                    int defaultHeight = 1120;
                    int pageCount = 5;

                    foreach (var z in lst)
                    {
                        GetAuditReportRes objTemp = new GetAuditReportRes();

                        objTemp.PageIndex = z.PageIndex;
                        objTemp.TotalRecords = z.TotalRecords;
                        objTemp.RowNum = z.RowNum;
                        objTemp.LogNo = z.LogNo;
                        objTemp.TypeNo = z.TypeNo;
                        objTemp.ShortCode = z.ShortCode;
                        objTemp.TypeDesc = z.TypeDesc;
                        objTemp.CatyNo = z.CatyNo;
                        objTemp.CatyDesc = z.CatyDesc;
                        objTemp.SubCatyNo = z.SubCatyNo;
                        objTemp.SubCatyDesc = z.SubCatyDesc;
                        objTemp.FieldNo = z.FieldNo;
                        objTemp.FieldDesc = z.FieldDesc;

                        objTemp.OldValue = z.OldValue;
                        objTemp.NewValue = z.NewValue;
                        objTemp.ListValue = JsonConvert.DeserializeObject<List<AuditTrailListValue>>(z.ListValue);

                        objTemp.TranOn = z.TranOn;
                        objTemp.TranByNo = z.TranByNo;
                        objTemp.TranByName = z.TranByName;
                        objTemp.Comments = z.Comments;
                        objTemp.PatientNo = z.PatientNo;
                        objTemp.PatientId = z.PatientId;
                        objTemp.PatientVisitNo = z.PatientVisitNo;
                        objTemp.CatyGroup = z.CatyGroup;
                        objTemp.EntityName = z.EntityName;
                        objTemp.TranDesc = z.TranDesc;
                        objTemp.ResultTypeNo = z.ResultTypeNo;
                        objTemp.OutputFolderName = z.OutputFolderName;
                        objTemp.OldTemplateValue = string.Empty;
                        objTemp.NewTemplateValue = string.Empty;

                        if (objTemp.ResultTypeNo == 3)
                        {
                            ReadPath = AuditFilePath + objTemp.OutputFolderName + "\\";
                            //ReadPath = @"\\?\" + ReadPath;
                            //ReadPath = "D:\\PUBLISH\\AuditTrail\\6119_1779\\";

                            ////Using RtfPipe

                            //if (File.Exists(ReadPath + OldFile + ".rtf"))
                            //{
                            //    objTemp.OldTemplateValue = ConvertRtfToHtml(File.ReadAllText(ReadPath + OldFile + ".rtf"));
                            //}
                            //if (File.Exists(ReadPath + NewFile + ".rtf"))
                            //{
                            //    objTemp.NewTemplateValue = ConvertRtfToHtml(File.ReadAllText(ReadPath + NewFile + ".rtf"));
                            //}

                            //pageCount = GetRtfPageCount(ReadPath + OldFile + ".rtf");
                            if (pageCount > 0)
                            {
                                height = defaultHeight * pageCount;
                            }
                            else
                            {
                                height = defaultHeight;
                            }
                            ConvertRtfToImage(ReadPath + OldFile + ".rtf", ReadPath + OldFile + ".png", width, height);

                            //pageCount = GetRtfPageCount(ReadPath + NewFile + ".rtf");
                            if (pageCount > 0)
                            {
                                height = defaultHeight * pageCount;
                            }
                            else
                            {
                                height = defaultHeight;
                            }
                            ConvertRtfToImage(ReadPath + NewFile + ".rtf", ReadPath + NewFile + ".png", width, height);

                            //if (File.Exists(ReadPath + OldFile + ".png"))
                            //{
                            //    byte[] imageBytes = File.ReadAllBytes(ReadPath + OldFile + ".png");
                            //    objTemp.OldTemplateValue = Convert.ToBase64String(imageBytes);
                            //}
                            //if (File.Exists(ReadPath + NewFile + ".png"))
                            //{
                            //    byte[] imageBytes = File.ReadAllBytes(ReadPath + NewFile + ".png");
                            //    objTemp.NewTemplateValue = Convert.ToBase64String(imageBytes);
                            //}

                            if (File.Exists(ReadPath + OldFile + ".png"))
                            {
                                objTemp.OldTemplateUrl = TemplateAuditFileUrl + req.VenueNo.ToString() + "/" + objTemp.OutputFolderName + "/" + OldFile + ".png";
                            }
                            if (File.Exists(ReadPath + NewFile + ".png"))
                            {
                                objTemp.NewTemplateUrl = TemplateAuditFileUrl + req.VenueNo.ToString() + "/" + objTemp.OutputFolderName + "/" + NewFile + ".png";
                            }
                        }

                        objresult.Add(objTemp);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.GetAuditTrailReport", ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return objresult;
        }

        public AuditTrailVisitHistoryResponse GetAuditTrailVisitHistory(GetAuditTrailVisitReq req)
        {
            AuditTrailVisitHistoryResponse objResult = new AuditTrailVisitHistoryResponse();
            try
            {
                using (var context = new PatientReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _PatientvisitNo = new SqlParameter("Patientvisitno", req.Patientvisitno);

                    var lst = context.GetAuditTrailVisitHistory.FromSqlRaw(
                        "Execute dbo.pro_getPatientVisitNoHistory " +
                        "@PatientvisitNo, @VenueNo, @VenueBranchNo ",
                        _PatientvisitNo, _VenueNo, _VenueBranchNo).ToList();

                    objResult.RowNo = lst[0].RowNo;
                    objResult.PtId = lst[0].PtId;
                    objResult.PtNo = lst[0].PtNo;
                    objResult.PtName = lst[0].PtName;
                    objResult.PtAgeType = lst[0].PtAgeType;
                    objResult.PtGender = lst[0].PtGender;
                    objResult.PtAgeGender = lst[0].PtAgeGender;
                    objResult.PtMobileNo = lst[0].PtMobileNo;
                    objResult.PtEmailId = lst[0].PtEmailId;
                    objResult.RefTypeDesc = lst[0].RefTypeDesc;
                    objResult.RefTypeName = lst[0].RefTypeName;
                    objResult.Physician = lst[0].Physician;
                    objResult.PatientVisitNo = lst[0].PatientVisitNo;
                    objResult.LabAccessionNo = lst[0].LabAccessionNo;
                    objResult.RegDtTm = lst[0].RegDtTm;
                    objResult.IdNumber = lst[0].IdNumber;

                    objResult.RegistrationList = JsonConvert.DeserializeObject<List<AuditTrailRegistration>>(lst[0].lstRegistration);
                    objResult.EditRegistrationList = JsonConvert.DeserializeObject<List<AuditTrailEditRegistration>>(lst[0].lstEditRegistration);
                    objResult.SampleCollectionList = JsonConvert.DeserializeObject<List<AuditTrailSampleCollection>>(lst[0].lstSampleCollection);
                    objResult.SampleAccessionList = JsonConvert.DeserializeObject<List<AuditTrailSampleAccession>>(lst[0].lstSampleAccession);
                    objResult.SampleRejectionList = JsonConvert.DeserializeObject<List<AuditTrailSampleRejection>>(lst[0].lstSampleRejection);
                    objResult.ResultEntryList = JsonConvert.DeserializeObject<List<AuditTrailResultEntry>>(lst[0].lstResultEntry);
                    objResult.ResultEntrySecondReview = JsonConvert.DeserializeObject<List<AuditTrailResultSecondReview>>(lst[0].lstResultEntrySecondReview);
                    objResult.RerunResultList = JsonConvert.DeserializeObject<List<AuditTrailRerunResult>>(lst[0].lstRerunResult);
                    objResult.ResultRecallList = JsonConvert.DeserializeObject<List<AuditTrailRecallResult>>(lst[0].lstRecallResult);
                    objResult.ResultValidationList = JsonConvert.DeserializeObject<List<AuditTrailResultValidation>>(lst[0].lstResultValidation);
                    objResult.ReportPrintList = JsonConvert.DeserializeObject<List<AuditTrailReportPrint>>(lst[0].lstReportPrint);
                    objResult.CancelRegistrationList = JsonConvert.DeserializeObject<List<AuditTrailCancelRegistration>>(lst[0].lstCancelRegistration);
                    objResult.SendOutList = JsonConvert.DeserializeObject<List<AuditTrailSendOut>>(lst[0].lstSendOut);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.GetAuditTrailVisitHistoty", ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return objResult;
        }

        public static string ConvertRtfToHtml(string rtfText)
        {
            var html = Rtf.ToHtml(CleanRtfContent(rtfText.Trim()));
            return html;
        }
        static string CleanRtfContent(string rtfContent)
        {
            int lastClosingBraceIndex = rtfContent.LastIndexOf('}');
            if (lastClosingBraceIndex != -1)
            {
                rtfContent = rtfContent.Substring(0, lastClosingBraceIndex + 1);
            }
            return rtfContent.Trim();
        }
        public static void ConvertRtfToImage(string rtfFilePath, string outputImagePath, int width, int height)
        {
            string rtfContent = File.ReadAllText(rtfFilePath);

            using (RichTextBox rtb = new RichTextBox())
            {
                rtb.Rtf = rtfContent;
                rtb.Width = width;
                rtb.Height = height;

                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.Clear(System.Drawing.Color.White);
                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, width, height);
                        rtb.DrawToBitmap(bitmap, rect);
                        bitmap.Save(outputImagePath, ImageFormat.Png);
                    }
                }
            }
        }
        public static int GetPageCount(string filePath)
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document wordDoc = null;
            try
            {
                wordApp.Visible = false;
                wordDoc = wordApp.Documents.Open(filePath, ReadOnly: true);
                int pageCount = wordDoc.ComputeStatistics(WdStatistic.wdStatisticPages, false);
                return pageCount;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.GetAuditTrailVisitHistoty", ExceptionPriority.High, ApplicationType.REPOSITORY, 1, 1, 1);
                return -1;
            }
            finally
            {
                if (wordDoc != null)
                {
                    wordDoc.Close(false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordDoc);
                }
                wordApp.Quit(false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            }
        }

        public static int GetRtfPageCount(string rtfPath)
        {
            try
            {
                string rtfContent = File.ReadAllText(rtfPath);
                MigraDoc.DocumentObjectModel.Document document = new MigraDoc.DocumentObjectModel.Document();
                MigraDoc.DocumentObjectModel.Section section = document.AddSection();

                MigraDoc.DocumentObjectModel.Paragraph paragraph = section.AddParagraph();
                paragraph.AddFormattedText(rtfContent, (TextFormat)TextDataFormat.Rtf);

                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
                pdfRenderer.Document = document;
                pdfRenderer.RenderDocument();
                return pdfRenderer.PdfDocument.PageCount;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.GetAuditTrailVisitHistoty", ExceptionPriority.High, ApplicationType.REPOSITORY, 1, 1, 1);
                return -1;
            }
        }

        public List<lstamendedpatientreport> GetAmendedPatientReport(requestamendedpatientreport req)
        {
            List<lstamendedpatientreport> lst = new List<lstamendedpatientreport>();
            try
            {   
                using (var context = new PatientReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req?.pagecode);
                    var _venueno = new SqlParameter("venueno", req?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req?.venuebranchno);
                    var _userno = new SqlParameter("userno", req?.userno);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req?.viewvenuebranchno);
                    var _pageindex = new SqlParameter("pageindex", req?.pageindex);
                    var _type = new SqlParameter("type", req?.type);
                    var _fromdate = new SqlParameter("fromdate", req?.fromdate);
                    var _todate = new SqlParameter("todate", req?.todate);
                    var _patientno = new SqlParameter("patientno", req?.patientno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req?.patientvisitno);
                    var _deptno = new SqlParameter("deptno", req?.deptno);
                    var _serviceno = new SqlParameter("serviceno", req?.serviceno);
                    var _servicetype = new SqlParameter("servicetype", req?.servicetype);
                    var _refferraltypeno = new SqlParameter("refferraltypeno", req?.refferraltypeno);
                    var _customerno = new SqlParameter("customerno", req?.customerno);
                    var _physicianno = new SqlParameter("physicianno", req?.physicianno);
                    var _orderstatus = new SqlParameter("orderstatus", req?.orderstatus);
                    var _printstatus = new SqlParameter("printstatus", req?.printstatus);
                    var _cpprintstatus = new SqlParameter("cpprintstatus", req?.cpprintstatus);
                    var _loginType = new SqlParameter("loginType", req?.loginType);
                    var _maindeptNo = new SqlParameter("maindeptNo", req?.maindeptNo);

                    var rtndblst = context.GetAmendedPatientReport.FromSqlRaw(
                        "Execute dbo.pro_AmendedPatientReport" +
                        " @pagecode, @venueno, @venuebranchno, @userno, @viewvenuebranchno, @pageindex, @type, @fromdate, @todate," +
                        " @patientno, @patientvisitno, @maindeptNo, @deptno, @serviceno, @servicetype, @refferraltypeno, @customerno, @physicianno, " +
                        " @orderstatus, @printstatus, @cpprintstatus, @loginType",
                        _pagecode, _venueno, _venuebranchno, _userno, _viewvenuebranchno, _pageindex, _type, _fromdate, _todate, 
                        _patientno, _patientvisitno, _maindeptNo, _deptno, _serviceno, _servicetype, _refferraltypeno, _customerno, _physicianno,
                        _orderstatus, _printstatus, _cpprintstatus, _loginType).ToList();

                    int amendmentno = 0;
                    foreach (var v in rtndblst)
                    {
                        if (amendmentno != v.amendmentno)
                        {
                            amendmentno = v.amendmentno;

                            lstamendedpatientreport obj = new lstamendedpatientreport();
                            obj.ischecked = false;
                            obj.patientno = v.patientno;
                            obj.rhNo = v.rhNo;
                            obj.patientvisitno = v.patientvisitno;
                            obj.patientid = v.patientid;
                            obj.fullname = v.fullname;
                            obj.agegender = v.agegender;
                            obj.patientmobile = v.patientmobile;
                            obj.patientemailid = v.patientemailid;
                            obj.ispatientimage = v.ispatientimage;
                            obj.visitid = v.visitid;
                            obj.extenalvisitid = v.extenalvisitid;
                            obj.visitdttm = v.visitdttm;
                            obj.refferraltypeno = v.refferraltypeno;
                            obj.referraltype = v.referraltype;
                            obj.customerno = v.customerno;
                            obj.customername = v.customername;
                            obj.customeremailid = v.customeremailid;
                            obj.customermobile = v.customermobile;
                            obj.islabheader = v.islabheader;
                            obj.isreportblocked = v.isreportblocked;
                            obj.isinternotes = v.isinternotes;
                            obj.physicianno = v.physicianno;
                            obj.physicianname = v.physicianname;
                            obj.physicianemailid = v.physicianemailid;
                            obj.physicianmobile = v.physicianmobile;

                            obj.isPatientReportSMS = v.isPatientReportSMS;
                            obj.isPatientReportEmail = v.isPatientReportEmail;
                            obj.isPatientReportWhatsapp = v.isPatientReportWhatsapp;
                            obj.isCustomerReportSMS = v.isCustomerReportSMS;
                            obj.isCustomerReportEmail = v.isCustomerReportEmail;
                            obj.isCustomerReportWhatsapp = v.isCustomerReportWhatsapp;
                            obj.isPhysicianreportSms = v.isPhysicianreportSms;
                            obj.isPhysicianreportEmail = v.isPhysicianreportEmail;
                            obj.isPhysicianreportWhatsapp = v.isPhysicianreportWhatsapp;

                            obj.isstat = v.isstat;
                            obj.rctdttm = v.rctdttm;
                            obj.modeofdispatch = v.modeofdispatch;
                            obj.isvisitstatus = v.isvisitstatus;
                            obj.taskdttm = v.taskdttm;
                            obj.isdue = v.isdue;
                            obj.cancelled = v.cancelled;
                            obj.visabnormal = v.visabnormal;
                            obj.viscritical = v.viscritical;
                            obj.vistat = v.vistat;
                            obj.orderstatus = v.orderstatus;
                            obj.printstatus = v.printstatus;
                            obj.cpprintstatus = v.cpprintstatus;
                            obj.smsstatus = v.smsstatus;
                            obj.emailstatus = v.emailstatus;
                            obj.visremarks = v.visremarks;
                            obj.viscpremarks = v.viscpremarks;
                            obj.TotalRecords = v.TotalRecords;
                            obj.DuePrint = v.DuePrint;
                            obj.Deliverymodes = v.Deliverymodes;
                            obj.VenueBranchName = v.VenueBranchName;
                            obj.IDnumber = v.IDnumber;
                            obj.IsVipIndication = v.IsVipIndication;
                            obj.amendmentcode = v.amendmentcode;
                            obj.amendmentno = v.amendmentno;
                            obj.amendmenton = v.amendmenton;
                            obj.amendmentby = v.amendmentby;

                            obj.emailIdToShow = v.emailIdToShow;
                            obj.mobileNumberToShow = v.mobileNumberToShow;

                            var rollst = rtndblst.Where(o => o.patientvisitno == v.patientvisitno && o.amendmentno == v.amendmentno).ToList();

                            List<lstamendedreportorderlist> lstrol = new List<lstamendedreportorderlist>();
                            foreach (var s in rollst)
                            {
                                lstamendedreportorderlist objrol = new lstamendedreportorderlist();
                                obj.ischecked = false;
                                objrol.orderno = s.orderno;
                                objrol.orderlistno = s.orderlistno;
                                objrol.servicetype = s.servicetype;
                                objrol.serviceno = s.serviceno;
                                objrol.servicecode = s.servicecode;
                                objrol.servicename = s.servicename;
                                objrol.departmentno = s.departmentno;
                                objrol.departmentname = s.departmentname;
                                objrol.resulttypeno = s.resulttypeno;
                                objrol.orderliststatus = s.orderliststatus;
                                objrol.orderliststatustext = s.orderliststatustext;
                                objrol.colorcode = s.colorcode;
                                objrol.barcodeno = s.barcodeno;
                                objrol.isrecollect = s.isrecollect;
                                objrol.isrecall = s.isrecall;
                                objrol.iscancelled = s.iscancelled;
                                objrol.enteredby = s.enteredby;
                                objrol.enteredon = s.enteredon;
                                objrol.validatedby = s.validatedby;
                                objrol.validatedon = s.validatedon;
                                objrol.approvedby = s.approvedby;
                                objrol.approvedon = s.approvedon;
                                objrol.isabnormal = s.isabnormal;
                                objrol.iscritical = s.iscritical;
                                objrol.istat = s.istat;
                                objrol.issms = s.issms;
                                objrol.smsdttm = s.smsdttm;
                                objrol.isemail = s.isemail;
                                objrol.emaildttm = s.emaildttm;
                                objrol.isprint = s.isprint;
                                objrol.printdttm = s.printdttm;
                                objrol.iscpreportshow = s.iscpreportshow;
                                objrol.cpreportshowdttm = s.cpreportshowdttm;
                                objrol.iscpprint = s.iscpprint;
                                objrol.cpprintdttm = s.cpprintdttm;
                                objrol.isremarks = s.isremarks;
                                objrol.iscpremarks = s.iscpremarks;
                                objrol.isservicestatus = s.isservicestatus;
                                objrol.istrand = s.istrand;
                                objrol.amendmentreason = s.amendmentreason;
                                lstrol.Add(objrol);
                            }
                            obj.lstamendedreportorderlist = lstrol;
                            lst.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.GetAmendedPatientReport/patientvisitno-" + req.patientvisitno, ExceptionPriority.High, ApplicationType.REPOSITORY, req?.venueno, req?.venuebranchno, (int)(req?.userno));
            }
            return lst;
        }

        public async Task<List<ReportOutput>> PrintAmendedPatientReport(AmendedPatientReportDTO PatientItem)
        {
            string DefaultConnection = string.Empty;
            bool isdirectapprovalavail = false;
            string directapprovalorderlistno = "";
            string directapprovaltestno = "";
            string directapprovalvisitid = "";

            List<ReportOutput> result = new List<ReportOutput>();
            try
            {
                Int16 languagecode = PatientItem.pritlanguagetype != null && PatientItem.pritlanguagetype > 0 ? PatientItem.pritlanguagetype : Convert.ToInt16(0);
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                ConfigurationDto objConfigurationDTO = new ConfigurationDto();
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();

                if (!PatientItem.isdefault)
                    DefaultConnection = _config.GetConnectionString(ConfigKeys.DefaultConnection);
                else
                    DefaultConnection = _config.GetConnectionString(ConfigKeys.ArchiveDefaultConnection);

                var lstresulttypenos = PatientItem?.resulttypenos.Split(',');
                var Key = "";
                for (int i = 0; i < lstresulttypenos?.Length; i++)
                {
                    Key = "";
                    isdirectapprovalavail = false;
                    directapprovalorderlistno = "";
                    directapprovaltestno = "";
                    directapprovalvisitid = "";

                    if (lstresulttypenos[i] == "1" && (languagecode == 1 || languagecode == 0))
                    {
                        Key = ReportKey.AMENDEDPATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "2" && (languagecode == 1 || languagecode == 0))
                    {
                        Key = ReportKey.AMENDEDMBPATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "3" && (languagecode == 1 || languagecode == 0))
                    {
                        Key = ReportKey.AMENDEDTEMPPATIENTREPORT;
                    }

                    ReportOutput item = new ReportOutput();
                    Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                    objdictionary.Add("PageCode", PatientItem?.pagecode);
                    objdictionary.Add("PatientVisitNo", PatientItem?.patientvisitno);
                    objdictionary.Add("OrderListNos", PatientItem?.orderlistnos);
                    objdictionary.Add("IsLogo", PatientItem?.isheaderfooter.ToString());
                    objdictionary.Add("IsNABLlogo", PatientItem?.isNABLlogo.ToString());
                    objdictionary.Add("UserNo", PatientItem?.userno.ToString());
                    objdictionary.Add("VenueNo", PatientItem?.venueno.ToString());
                    objdictionary.Add("VenueBranchNo", PatientItem?.venuebranchno.ToString());
                    objdictionary.Add("AmendmentNo", PatientItem?.amendmentno.ToString());
                    ReportContext objReportContext = new ReportContext(DefaultConnection);
                    TblReportMaster tblReportMaster = new TblReportMaster();

                    using (var context = new LIMSContext(DefaultConnection))
                    {
                        tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == Key && x.VenueNo == PatientItem.venueno
                        && x.VenueBranchNo == PatientItem.venuebranchno).FirstOrDefault();
                        if (!Directory.Exists(tblReportMaster?.ExportPath))
                        {
                            Directory.CreateDirectory(tblReportMaster?.ExportPath);
                        }
                    }
                    
                    string PatientName = string.Concat(PatientItem?.patientvisitno.Where(c => !char.IsWhiteSpace(c)));
                    string iFile = "a_" +Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                    objdictionary.Add("QRCodeURL", tblReportMaster?.ExportURL + iFile);
                    objdictionary.Add("IsProvisional", PatientItem?.isProvisional.ToString());
                    objdictionary.Add("pritlanguagetype", languagecode.ToString());
                    
                    if (Key == ReportKey.AMENDEDMBPATIENTREPORT)
                    {
                        objConfigurationDTO = new ConfigurationDto();
                        string resultStatusDDAvailConfig = "IsRsultStatusDDAvail";
                        objConfigurationDTO = _IMasterRepository.GetSingleConfiguration(PatientItem.venueno, PatientItem.venuebranchno, resultStatusDDAvailConfig);
                        if (objConfigurationDTO != null && objConfigurationDTO.ConfigValue == 1)
                        {
                            objdictionary.Add("ReportStatus", PatientItem.reportstatus.ToString());
                        }
                    }

                    System.Data.DataTable datable = objReportContext.getdatatable(objdictionary, tblReportMaster?.ProcedureName);
                    
                    if (datable != null && datable.Rows.Count > 0)
                    {
                        //directapproval - result ack page
                        int orderlistno = 0;
                        foreach (DataRow dr in datable.Rows)
                        {
                            if (datable.Columns.Contains("IsDirectApproval") && Convert.ToBoolean(dr["IsDirectApproval"]) == true)
                            {
                                isdirectapprovalavail = true;
                                if (Convert.ToInt32(dr["OrderListNo"]) != orderlistno)
                                {
                                    directapprovalorderlistno = directapprovalorderlistno != null && directapprovalorderlistno != "" ?
                                        directapprovalorderlistno + ',' + Convert.ToInt32(dr["OrderListNo"]).ToString() : Convert.ToInt32(dr["OrderListNo"]).ToString();
                                    directapprovaltestno = directapprovaltestno != null && directapprovaltestno != "" ?
                                        directapprovaltestno + ',' + Convert.ToInt32(dr["ServiceNo"]).ToString() : Convert.ToInt32(dr["ServiceNo"]).ToString();
                                    directapprovalvisitid = dr["VisitID"].ToString();
                                }
                            }
                            orderlistno = Convert.ToInt32(dr["OrderListNo"]);
                        }
                        if (isdirectapprovalavail == true && directapprovalorderlistno != null && directapprovalorderlistno != "")
                        {
                            string[] orderlsts = directapprovalorderlistno.Split(',');
                            if (orderlsts != null && orderlsts.Length > 0)
                            {
                                for (int o = 0; o < orderlsts.Length; o++)
                                {
                                    for (int q = datable.Rows.Count - 1; q >= 0; q--)
                                    {
                                        DataRow dr = datable.Rows[q];
                                        if (dr.RowState != DataRowState.Deleted)
                                        {
                                            if (Convert.ToInt32(dr["OrderListNo"]) == Convert.ToInt32(orderlsts[o]))
                                                dr.Delete();
                                        }
                                    }
                                }
                                datable.AcceptChanges();
                            }
                        }
                    }

                    if (datable != null && datable.Rows.Count > 0)
                    {
                        if (Key == "AMENDEDTEMPPATIENTREPORT")
                        {
                            string AppTransTemplateFilePath = "AmendmentTransTemplateFilePath";
                            string AppMasterFilePath = "MasterFilePath";
                            
                            objAppSettingResponse = new AppSettingResponse();
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";
                            
                            objAppSettingResponse = new AppSettingResponse();
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                            string pathForReportDis = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";
                            pathForReportDis = pathForReportDis + PatientItem?.venueno.ToString() + "/T/";
                            string restorePath = path;
               
                            for (int j = 0; j < datable.Rows.Count; j++)
                            {
                                path = path + PatientItem?.venueno.ToString() + "/" + datable?.Rows[j]["amendmentNo"]?.ToString() + "/" + datable?.Rows[j]["orderListNo"]?.ToString() + "/" + datable?.Rows[j]["serviceNo"]?.ToString() + ".rtf";
                        
                                if (File.Exists(path))
                                {
                                    string content = File.ReadAllText(path);
                                    datable.Rows[j]["result"] = content;
                                    string getPath = pathForReportDis + "/ReportDisclaimer/" + datable?.Rows[j]["serviceNo"]?.ToString() + ".ym";
                                    if (File.Exists(getPath))
                                        if (datable.Rows.Count > 0 && datable.Columns.Contains("IsReportDisclaimer"))
                                            if (datable.Rows[j]["IsReportDisclaimer"].ToString() == "1")
                                                if (datable.Rows.Count > 0 && datable.Columns.Contains("ReportDisclaimer"))
                                                    datable.Rows[j]["ReportDisclaimer"] = File.ReadAllText(getPath).ToString();
                                }
                                else
                                {                                    
                                    datable.Rows[j]["result"] = "";
                                }
                                path = restorePath;
                            }
                        }
                        else if (Key == "AMENDEDPATIENTREPORT")
                        {
                            int sno = 0;
                            string grpnotes = "";
                            int tno = 0;
                            string tstnotes = "";
                            foreach (DataRow dr in datable.Rows)
                            {
                                if (Convert.ToBoolean(dr["IsGrpTestInter"]) == false)
                                {
                                    if (sno != Convert.ToInt32(dr["ServiceNo"]))
                                    {
                                        grpnotes = "";
                                        sno = Convert.ToInt32(dr["ServiceNo"]);
                                        if (Convert.ToInt32(dr["GroupInter"]) == 2)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppTransFilePath = "TransFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";
                                            path = path + PatientItem?.venueno.ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["OrderListNo"]).ToString() + ".ym";
                                            if (File.Exists(path))
                                            {
                                                dr["GroupInterNotes"] = File.ReadAllText(path);
                                                grpnotes = dr["GroupInterNotes"].ToString();
                                            }
                                        }
                                        else if (Convert.ToInt32(dr["GroupInter"]) == 1)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppMasterFilePath = "MasterFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";
                                            path = path + PatientItem?.venueno.ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["ServiceNo"]).ToString() + ".ym";
                                            if (File.Exists(path))
                                            {
                                                dr["GroupInterNotes"] = File.ReadAllText(path);
                                                grpnotes = dr["GroupInterNotes"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dr["GroupInterNotes"] = grpnotes;
                                    }
                                }
                                else if (Convert.ToBoolean(dr["IsGrpTestInter"]) == true)
                                {
                                    if (tno != Convert.ToInt32(dr["TestNo"]))
                                    {
                                        tstnotes = "";
                                        tno = Convert.ToInt32(dr["TestNo"]);
                                        if (Convert.ToInt32(dr["TestInter"]) == 2)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppTransFilePath = "TransFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";
                                            path = path + PatientItem?.venueno.ToString() + "/T/InterNotes/" + Convert.ToInt32(dr["OrderDetailsNo"]).ToString() + ".ym";
                                            if (File.Exists(path))
                                            {
                                                dr["TestInterNotes"] = File.ReadAllText(path);
                                                tstnotes = dr["TestInterNotes"].ToString();
                                            }
                                        }
                                        else if (Convert.ToInt32(dr["TestInter"]) == 1)
                                        {
                                            string internotesresflag = dr["resultflag"] != null ? dr["resultflag"].ToString() : "";
                                            string FPath = internotesresflag == "H" ? Convert.ToInt32(dr["TestNo"]).ToString() + "_H" + ".ym" : internotesresflag == "L" ? Convert.ToInt32(dr["TestNo"]).ToString() + "_L" + ".ym" : Convert.ToInt32(dr["TestNo"]).ToString() + ".ym";
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppMasterFilePath = "MasterFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";
                                            path = path + PatientItem?.venueno.ToString() + "/T/InterNotes/" + FPath;
                                            if (File.Exists(path))
                                            {
                                                dr["TestInterNotes"] = File.ReadAllText(path);
                                                tstnotes = dr["TestInterNotes"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dr["TestInterNotes"] = tstnotes;
                                    }
                                }
                                if (datable.Columns.Contains("IsGrpReportdisclaimer"))
                                {
                                    if (Convert.ToBoolean(dr["IsGrpReportdisclaimer"]) == true)
                                    {
                                        objAppSettingResponse = new AppSettingResponse();
                                        string AppMasterFilePath = "MasterFilePath";
                                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                        string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";
                                        path = path + PatientItem?.venueno.ToString() + "/G/ReportDisclaimer/" + dr["ServiceNo"].ToString() + ".ym";
                                        if (File.Exists(path))
                                        {
                                            dr["GrpReportDisclaimer"] = File.ReadAllText(path).ToString();
                                        }
                                        else
                                        {
                                            dr["GrpReportDisclaimer"] = "";
                                        }
                                    }
                                }
                                if (datable.Columns.Contains("IsTestReportdisclaimer"))
                                {
                                    if (Convert.ToBoolean(dr["IsTestReportdisclaimer"]) == true)
                                    {
                                        objAppSettingResponse = new AppSettingResponse();
                                        string AppMasterFilePath = "MasterFilePath";
                                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                        string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";
                                        path = path + PatientItem?.venueno.ToString() + "/T/ReportDisclaimer/" + dr["ServiceNo"].ToString() + ".ym";
                                        if (File.Exists(path))
                                        {
                                            dr["TestReportDisclaimer"] = File.ReadAllText(path).ToString();
                                        }
                                        else
                                        {
                                            dr["TestReportDisclaimer"] = "";
                                        }
                                    }
                                }
                                if (datable.Columns.Contains("GraphImage1"))
                                {
                                    //machine graph attached to the final report
                                    string GraphURL = dr["GraphURL"].ToString();
                                    objAppSettingResponse = new AppSettingResponse();
                                    string AppMachineImagePath = "MachineImagePath";
                                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMachineImagePath);
                                    string actualmachineimagepath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";                               
                                    string machineimagepath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";
                                    machineimagepath = machineimagepath + "//" + PatientItem?.venueno + "//" + PatientItem?.venuebranchno + "//" + dr["BarcodeNoNew"].ToString();
                                    if (Directory.Exists(machineimagepath))
                                    {
                                        string[] files = Directory.GetFiles(machineimagepath);
                                        if (files != null && files.Length > 0)
                                        {
                                            for (int g = 0; g < files.Length; g++)
                                            {
                                                string machinefullpath = files[g];
                                                string serviceno = machinefullpath.Replace(machineimagepath, "").Replace("//", "").Replace("\\", "").Replace(".png", "");
                                                var servicenolst = serviceno.Split("T").Length > 1 ? serviceno.Split("T") : serviceno.Split("S");
                                                string servcno = servicenolst.Length > 1 ? servicenolst[1].ToString() : "";
                                                if (servcno == dr["TestNo"].ToString())
                                                {
                                                    if (g == 0 || dr["GraphImage1"].ToString() == "")
                                                    {
                                                        dr["GraphImage1"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
                                                    }
                                                    else if (datable.Columns.Contains("GraphImage2") && (g == 1 || dr["GraphImage2"].ToString() == ""))
                                                    {
                                                        dr["GraphImage2"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
                                                    }
                                                    else if (datable.Columns.Contains("GraphImage3") && (g == 2 || dr["GraphImage3"].ToString() == ""))
                                                    {
                                                        dr["GraphImage3"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
                                                    }
                                                    else if (datable.Columns.Contains("GraphImage4") && (g == 3 || dr["GraphImage4"].ToString() == ""))
                                                    {
                                                        dr["GraphImage4"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
                                                    }
                                                    else if (datable.Columns.Contains("GraphImage5") && (g == 4 || dr["GraphImage5"].ToString() == ""))
                                                    {
                                                        dr["GraphImage5"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //assign all images path into last test of the group if graph is available for the group test
                        if (datable.Columns.Contains("GraphImage1"))
                        {
                            var results = from DataRow myRow in datable.Rows
                                          where (string)myRow["GraphImage1"] != "" && (string)myRow["GroupName"] != ""
                                          select myRow;
                            // test have grpah image inside the group
                            if (results != null && results.ToList().Count() > 0)
                            {

                                int imagescount = 1;
                                int graphavailcount = results.ToList().Count();
                                string groupname = String.Empty;
                                for (int g = 0; g < graphavailcount; g++)
                                {
                                    var groupcountlst = from DataRow myRow in datable.Rows
                                                        where (string)myRow["GroupName"] == results.ToList()[g].ItemArray[23].ToString()
                                                        select myRow;
                                    //if grpah have more than a group test
                                    if (groupname != results.ToList()[g].ItemArray[23].ToString())
                                    {
                                        imagescount = 1;
                                    }
                                    int groupcount = groupcountlst != null ? groupcountlst.ToList().Count() : 0;
                                    string graph1 = results.ToList()[g].ItemArray[79].ToString();
                                    string graph2 = results.ToList()[g].ItemArray[80].ToString();
                                    string graph3 = results.ToList()[g].ItemArray[81].ToString();
                                    string graph4 = results.ToList()[g].ItemArray[82].ToString();
                                    string graph5 = results.ToList()[g].ItemArray[83].ToString();

                                    int dtgroupcount = 0;
                                    foreach (DataRow dtrow in datable.Rows)
                                    {
                                        if (dtrow["GroupName"].ToString() != "" && dtrow["GroupName"].ToString() != null && dtrow["GroupName"].ToString() == results.ToList()[g].ItemArray[23].ToString())
                                        {
                                            dtgroupcount = dtgroupcount + 1;
                                            if (groupcount == dtgroupcount)
                                            {
                                                if (imagescount == 1)
                                                {
                                                    dtrow["GraphImage1"] = graph1;
                                                    if (graph2 != "") { dtrow["GraphImage2"] = graph2; }
                                                    if (graph3 != "") { dtrow["GraphImage3"] = graph3; }
                                                    if (graph4 != "") { dtrow["GraphImage4"] = graph4; }
                                                    if (graph5 != "") { dtrow["GraphImage5"] = graph5; }
                                                }
                                                if (imagescount == 2)
                                                {
                                                    if (graph1 != "" && dtrow["GraphImage2"].ToString() == "") { dtrow["GraphImage2"] = graph1; }
                                                    if (graph2 != "" && dtrow["GraphImage3"].ToString() == "") { dtrow["GraphImage3"] = graph2; }
                                                    if (graph3 != "" && dtrow["GraphImage4"].ToString() == "") { dtrow["GraphImage4"] = graph3; }
                                                    if (graph4 != "" && dtrow["GraphImage5"].ToString() == "") { dtrow["GraphImage5"] = graph4; }
                                                }
                                                if (imagescount == 3)
                                                {
                                                    if (graph1 != "" && dtrow["GraphImage3"].ToString() == "") { dtrow["GraphImage3"] = graph1; }
                                                    if (graph2 != "" && dtrow["GraphImage4"].ToString() == "") { dtrow["GraphImage4"] = graph2; }
                                                    if (graph3 != "" && dtrow["GraphImage5"].ToString() == "") { dtrow["GraphImage5"] = graph3; }
                                                }
                                                if (imagescount == 4)
                                                {
                                                    if (graph1 != "" && dtrow["GraphImage4"].ToString() == "") { dtrow["GraphImage4"] = graph1; }
                                                    if (graph2 != "" && dtrow["GraphImage5"].ToString() == "") { dtrow["GraphImage5"] = graph2; }
                                                }
                                                if (imagescount == 5)
                                                {
                                                    if (graph1 != "" && dtrow["GraphImage5"].ToString() == "") { dtrow["GraphImage5"] = graph1; }
                                                }
                                            }
                                        }
                                    }
                                    groupname = results.ToList()[g].ItemArray[23].ToString();
                                    imagescount = imagescount + 1;
                                }
                                string ggroupname = String.Empty;
                                //remove graph path for all other test name except last test of group 
                                for (int g = 0; g < graphavailcount; g++)
                                {
                                    var groupcountlst = from DataRow myRow in datable.Rows
                                                        where (string)myRow["GroupName"] == results.ToList()[g].ItemArray[23].ToString()
                                                        select myRow;
                                    if (groupcountlst != null && groupcountlst.ToList().Count() > 0)
                                    {
                                        string lastGrpTestName = groupcountlst != null ? groupcountlst.ToList()[groupcountlst.ToList().Count() - 1].ItemArray[30].ToString() : "";
                                        string lastGrpTestNo = groupcountlst != null ? groupcountlst.ToList()[groupcountlst.ToList().Count() - 1].ItemArray[29].ToString() : "";
                                    }
                                    ggroupname = results.ToList()[g].ItemArray[23].ToString();
                                }
                            }
                        }
                        //

                        ReportParamDto objitem = new ReportParamDto();
                        objitem.datatable = CommonExtension.DatableToDicionary(datable);
                        objitem.paramerter = objdictionary;
                        objitem.ReportPath = tblReportMaster.ReportPath;
                        objitem.ExportPath = tblReportMaster.ExportPath + iFile;
                        objitem.ExportFormat = FileFormat.PDF;
                        string ReportParam = JsonConvert.SerializeObject(objitem);
                        objAppSettingResponse = new AppSettingResponse();
                        string AppReportServiceURL = "ReportServiceURL";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppReportServiceURL);
                        string ReportServiceURL = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";
                        string filename = await ExportReportService.ExportPrint(ReportParam, ReportServiceURL);
                        if (PatientItem?.process == 3)
                            item.PatientExportFile = tblReportMaster.ExportURL + filename;
                        else
                            item.PatientExportFile = tblReportMaster.ExportURL + filename;

                        item.PatientExportFolderPath = objitem.ExportPath;
                        //attached outsourced documents
                        objAppSettingResponse = new AppSettingResponse();
                        string AppResultAckUpload = "ResultAckUpload";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload);
                        string ackpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";
                        int oldtestno = 0; int newtestno = 0;
                        int isbillmerged = 0, isDeltaMerged = 0;
                        foreach (DataRow row in datable.Rows)
                        {
                            if (datable.Columns.Contains("BillIncluded") && isbillmerged == 0 && (PatientItem?.patientreportwithbill != null && PatientItem?.patientreportwithbill > 0))
                            {
                                int BillIncluded = row["BillIncluded"] != null && row["BillIncluded"].ToString() != "" ? Convert.ToInt32(row["BillIncluded"]) : 0;
                                if (BillIncluded > 0)
                                {
                                    foreach (DataColumn column in datable.Columns)
                                    {
                                        string billfullpath = "";
                                        ReportOutput obj = new ReportOutput();
                                        ReportRequestDTO reqstbill = new ReportRequestDTO();
                                        FrontOfficeRepository _IFrontOfficeRepository = new FrontOfficeRepository(_config);

                                        reqstbill.VenueNo = PatientItem.venueno;
                                        reqstbill.VenueBranchNo = PatientItem.venuebranchno;
                                        reqstbill.visitNo = PatientItem?.patientvisitno != null && PatientItem?.patientvisitno != "" ? Convert.ToInt32(PatientItem?.patientvisitno) : 0;
                                        reqstbill.userNo = PatientItem.userno;
                                        reqstbill.print = "PATIENTBILL";//"PATIENTBILLSFORMAT"
                                        obj = await _IFrontOfficeRepository.PrintBill(reqstbill);
                                        billfullpath = obj != null ? obj.PatientExportFolderPath : "";

                                        if (billfullpath != null && billfullpath != "")
                                        {
                                            using (PdfDocument one = PdfReader.Open(objitem.ExportPath, PdfDocumentOpenMode.Import))
                                            using (PdfDocument two = PdfReader.Open(billfullpath, PdfDocumentOpenMode.Import))
                                            using (PdfDocument outPdf = new PdfDocument())
                                            {
                                                CopyPages(one, outPdf);
                                                CopyPages(two, outPdf);

                                                outPdf.Save(objitem.ExportPath);
                                            }
                                        }
                                    }
                                }
                                isbillmerged = 1;
                            }
                            if (datable.Columns.Contains("DeltaReportIncluded") && isDeltaMerged == 0)
                            {
                                int DeltaReportIncluded = row["DeltaReportIncluded"] != null && row["DeltaReportIncluded"].ToString() != "" ? Convert.ToInt32(row["DeltaReportIncluded"]) : 0;
                                if (DeltaReportIncluded > 0)
                                {
                                    foreach (DataColumn column in datable.Columns)
                                    {
                                        if (isDeltaMerged == 0)
                                        {
                                            string billfullpath = ""; string Key1 = "DELTAREPORT";
                                            FrontOfficeRepository _IFrontOfficeRepository = new FrontOfficeRepository(_config);
                                            Dictionary<string, string> objdictionary1 = new Dictionary<string, string>();

                                            objdictionary1.Add("PageCode", PatientItem?.pagecode);
                                            objdictionary1.Add("PatientVisitNo", PatientItem?.patientvisitno);
                                            objdictionary1.Add("OrderListNos", PatientItem?.orderlistnos);
                                            objdictionary1.Add("IsLogo", PatientItem?.isheaderfooter.ToString());
                                            objdictionary1.Add("IsNABLlogo", PatientItem?.isNABLlogo.ToString());

                                            objdictionary1.Add("UserNo", PatientItem?.userno.ToString());
                                            objdictionary1.Add("VenueNo", PatientItem?.venueno.ToString());
                                            objdictionary1.Add("VenueBranchNo", PatientItem?.venuebranchno.ToString());
                                            ReportContext objReportContext1 = new ReportContext(DefaultConnection);
                                            TblReportMaster tblReportMaster1 = new TblReportMaster();
                                            using (var context = new LIMSContext(DefaultConnection))
                                            {
                                                tblReportMaster1 = context.TblReportMaster.Where(x => x.ReportKey == Key1 && x.VenueNo == PatientItem.venueno
                                                && x.VenueBranchNo == PatientItem.venuebranchno).FirstOrDefault();
                                                if (!Directory.Exists(tblReportMaster1.ExportPath))
                                                {
                                                    Directory.CreateDirectory(tblReportMaster1.ExportPath);
                                                }
                                            }
                                            string PatientName1 = string.Concat(PatientItem?.patientvisitno.Where(c => !char.IsWhiteSpace(c)));
                                            string iFile1 = "d_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";

                                            System.Data.DataTable datable1 = objReportContext1.getdatatable(objdictionary1, tblReportMaster1.ProcedureName);
                                            ReportParamDto objitem1 = new ReportParamDto();
                                            objitem1.datatable = CommonExtension.DatableToDicionary(datable1);
                                            objitem1.paramerter = objdictionary1;
                                            objitem1.ReportPath = tblReportMaster1.ReportPath;
                                            objitem1.ExportPath = tblReportMaster1.ExportPath + iFile1;
                                            objitem1.ExportFormat = FileFormat.PDF;
                                            string ReportParam1 = JsonConvert.SerializeObject(objitem1);
                                            objAppSettingResponse = new AppSettingResponse();
                                            AppReportServiceURL = "ReportServiceURL";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppReportServiceURL);
                                            ReportServiceURL = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                ? objAppSettingResponse.ConfigValue : "";
                                            string filename1 = await ExportReportService.ExportPrint(ReportParam1, ReportServiceURL);
                                            billfullpath = tblReportMaster1.ExportPath + filename1;
                                            if (billfullpath != null && billfullpath != "")
                                            {
                                                using (PdfDocument one = PdfReader.Open(objitem.ExportPath, PdfDocumentOpenMode.Import))
                                                using (PdfDocument two = PdfReader.Open(billfullpath, PdfDocumentOpenMode.Import))
                                                using (PdfDocument outPdf = new PdfDocument())
                                                {
                                                    CopyPages(one, outPdf);
                                                    CopyPages(two, outPdf);

                                                    outPdf.Save(objitem.ExportPath);
                                                }
                                            }
                                        }
                                        isDeltaMerged = 1;
                                    }
                                }
                            }
                            if (datable.Columns.Contains("TestNo") && datable.Columns.Contains("IsIncludeInReport"))
                            {
                                foreach (DataColumn column in datable.Columns)
                                {
                                    if (Convert.ToBoolean(row["IsIncludeInReport"]) == true && row["VisitID"] != null && column.ColumnName == "VisitID")
                                    {
                                        newtestno = row["TestNo"] != null && row["TestNo"].ToString() != "" ? Convert.ToInt32(row["TestNo"]) : 0;
                                        if (newtestno != oldtestno)
                                        { //if test have subtest then lnly once need to add outsource report
                                            objAppSettingResponse = new AppSettingResponse();
                                            AppResultAckUpload = "ResultAckUpload";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload);
                                            ackpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != "" ? objAppSettingResponse.ConfigValue : "";
                                            //ackpath = ackpath + "//" + PatientItem?.venueno + "//" + PatientItem?.venuebranchno + "//" + row["VisitID"] + "//" + row["TestNo"];
                                           // ackpath = ackpath + "//" + PatientItem?.venueno + "//" + PatientItem?.patientvisitno + "//" + row["TestNo"];
                                            ackpath = ackpath + "//" + PatientItem?.venueno + "//" + PatientItem?.patientvisitno + "//" + row["TestNo1"];
                                            if (Directory.Exists(ackpath))
                                            {
                                                string[] files = Directory.GetFiles(ackpath);
                                                if (files != null && files.Length > 0)
                                                {
                                                    for (int f = 0; f < files.Length; f++)
                                                    {
                                                        string resultname = Path.GetFileName(files[f]);
                                                        string ackfullpath = files[f];
                                                        string ackfilename = Path.GetFileNameWithoutExtension(ackfullpath);
                                                        string extension = Path.GetExtension(files[f]);
                                                        if (extension != null && (extension.ToLower() == ".pdf" || extension.ToLower() == "pdf"))
                                                        {
                                                            using (PdfDocument one = PdfReader.Open(objitem.ExportPath, PdfDocumentOpenMode.Import))
                                                            using (PdfDocument two = PdfReader.Open(ackfullpath, PdfDocumentOpenMode.Import))
                                                            using (PdfDocument outPdf = new PdfDocument())
                                                            {
                                                                CopyPages(one, outPdf);
                                                                CopyPages(two, outPdf);

                                                                outPdf.Save(objitem.ExportPath);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        oldtestno = newtestno;
                                    }
                                }
                            }
                        }
                        result.Add(item);
                    }
                    //result ack - direct approval - without dummy report 
                    string AppResultAckUpload3 = "ResultAckUpload";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload3);
                    string resultackuplod = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                                    ? objAppSettingResponse.ConfigValue : "";
                    if (resultackuplod != null && resultackuplod != "" && directapprovaltestno != null && directapprovaltestno != "")
                    {
                        item = new ReportOutput();
                        var overallackfiles = new List<string>();
                        
                        if (directapprovaltestno != null && directapprovaltestno != "")
                        {
                            var directapprovaltestnolst = directapprovaltestno.Split(',');
                            if (directapprovaltestnolst != null && directapprovaltestnolst.Length > 0)
                            {
                                for (int p = 0; p < directapprovaltestnolst.Length; p++)
                                {
                                    //string ackfolderName = PatientItem.venueno.ToString() + "//" + PatientItem.venuebranchno.ToString() + "//" + directapprovalvisitid + "//" + directapprovaltestnolst[p].ToString();
                                    //string ackfolderNameNew = PatientItem.venueno.ToString() + "//" + PatientItem.venuebranchno.ToString() + "//" + directapprovalvisitid + "//" + directapprovaltestnolst[p].ToString() + "//MergedFolder";
                                    string ackfolderName = PatientItem.venueno.ToString() + "//"  + PatientItem?.patientvisitno + "//" + directapprovaltestnolst[p].ToString();
                                    string ackfolderNameNew = PatientItem.venueno.ToString() + "//"  + PatientItem?.patientvisitno + "//" + directapprovaltestnolst[p].ToString() + "//MergedFolder";
                                    string newPath = Path.Combine(resultackuplod, ackfolderName);
                                    string newPathNew = Path.Combine(resultackuplod, ackfolderNameNew);
                                    if (!Directory.Exists(newPathNew))
                                    {
                                        Directory.CreateDirectory(newPathNew);
                                    }
                                    else
                                    {
                                        System.IO.DirectoryInfo di = new DirectoryInfo(newPathNew);
                                        foreach (FileInfo itemv in di.GetFiles())
                                        {
                                            itemv.Delete();
                                        }
                                    }
                                    newPathNew = newPathNew + "//" + directapprovalvisitid + ".pdf";
                                    if (Directory.Exists(newPath))
                                    {
                                        string[] filePaths = Directory.GetFiles(newPath);
                                        if (filePaths != null && filePaths.Length > 1)
                                        {
                                            for (int f = 0; f < filePaths.Length; f++)
                                            {
                                                if (f == 0)
                                                {
                                                    string path = filePaths[f].ToString();
                                                    Byte[] bytes = System.IO.File.ReadAllBytes(path);
                                                    String base64String = Convert.ToBase64String(bytes);
                                                    byte[] imageBytes = Convert.FromBase64String(base64String);
                                                    System.IO.File.WriteAllBytes(newPathNew, imageBytes);
                                                }
                                                if (f > 0 && f < filePaths.Length)
                                                {
                                                    string path2 = filePaths[f].ToString();

                                                    using (PdfDocument one = PdfReader.Open(newPathNew, PdfDocumentOpenMode.Import))
                                                    using (PdfDocument two = PdfReader.Open(path2, PdfDocumentOpenMode.Import))
                                                    using (PdfDocument outPdf = new PdfDocument())
                                                    {
                                                        CopyPages(one, outPdf);
                                                        CopyPages(two, outPdf);

                                                        outPdf.Save(newPathNew);
                                                    }
                                                }
                                            }
                                        }
                                        else if (filePaths != null && filePaths.Length > 0)
                                        {
                                            string path = filePaths[0].ToString();
                                            Byte[] bytes = System.IO.File.ReadAllBytes(path);
                                            String base64String = Convert.ToBase64String(bytes);
                                            byte[] imageBytes = Convert.FromBase64String(base64String);
                                            System.IO.File.WriteAllBytes(newPathNew, imageBytes);
                                        }
                                        overallackfiles.Add(newPathNew);
                                    }
                                }
                                tblReportMaster.ExportPath = tblReportMaster?.ExportPath;
                                if (!Directory.Exists(tblReportMaster?.ExportPath))
                                {
                                    Directory.CreateDirectory(tblReportMaster?.ExportPath);
                                }
                                string iFile2 = directapprovalvisitid + "_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                                string actpath = tblReportMaster?.ExportPath + "//" + iFile2;
                                if (overallackfiles != null && overallackfiles.Count > 0)
                                {
                                    for (int j = 0; j < overallackfiles.Count; j++)
                                    {
                                        if (j == 0)
                                        {
                                            string path = overallackfiles[0].ToString();
                                            Byte[] bytes = System.IO.File.ReadAllBytes(path);
                                            String base64String = Convert.ToBase64String(bytes);
                                            byte[] imageBytes = Convert.FromBase64String(base64String);
                                            System.IO.File.WriteAllBytes(actpath, imageBytes);
                                        }
                                        else
                                        {
                                            string path2 = overallackfiles[j].ToString();
                                            using (PdfDocument one = PdfReader.Open(actpath, PdfDocumentOpenMode.Import))
                                            using (PdfDocument two = PdfReader.Open(path2, PdfDocumentOpenMode.Import))
                                            using (PdfDocument outPdf = new PdfDocument())
                                            {
                                                CopyPages(one, outPdf);
                                                CopyPages(two, outPdf);

                                                outPdf.Save(actpath);
                                            }
                                        }
                                    }
                                    item.PatientExportFile = tblReportMaster.ExportURL + iFile2;
                                    item.PatientExportFolderPath = actpath;
                                    result.Add(item);
                                }
                            }
                        }                        
                    }
                    //
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.PrintAmendedPatientReport/patientvisitno-" + PatientItem?.patientvisitno, ExceptionPriority.High, ApplicationType.REPOSITORY, PatientItem?.venueno, PatientItem?.venuebranchno, PatientItem?.userno);
            }
            return result;
        }

        public List<GetATSubCatyMasterSearchResponse> GetATSubCatyMasters(GetATSubCatyMasterSearchReq req)
        {
            List<GetATSubCatyMasterSearchResponse> objresult = new List<GetATSubCatyMasterSearchResponse>();
            try
            {
                using (var context = new PatientReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", req.UserNo);
                    var _PageCode = new SqlParameter("PageCode", req.pageCode);
                    var _searchTypeCode = new SqlParameter("SearchTypeCode", req.searchByCode);
                    var _searchTypeText = new SqlParameter("SearchTypeText", req.searchByText);

                    objresult = context.GetATSubCatyMastersData.FromSqlRaw("Execute dbo.Pro_GetATSubCatyMastersData " +
                        "@VenueNo, @VenueBranchNo, @UserNo, @PageCode, @SearchTypeCode, @SearchTypeText",
                        _VenueNo, _VenueBranchNo, _UserNo, _PageCode, _searchTypeCode, _searchTypeText).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.GetATSubCatyMasters", ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return objresult;
        }
        public  async Task <string> GetPdfFileName(List<PatientReportDTO> PatientItem)
        {
            string filename = "";
            var list = PatientItem.Select(x => new { x.patientvisitno, x.venueno }).Distinct().ToList();
            if (list != null && list.Count == 1)
            {
                string patientVisitNo = list[0].patientvisitno;
                using (var _dbContext = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var patientTransactions = await _dbContext.PatientTransactions
                        .Where(x => x.PatientVisitNo == Int32.Parse(list[0].patientvisitno) && x.VenueNo == list[0].venueno 
                        && x.Status == true)
                        .ToListAsync();
                    if (patientTransactions != null && patientTransactions.Count() > 0)
                        filename = patientTransactions[0].VisitID.ToString() + "_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                    else
                        filename = DateTime.Now.ToString("yyyyMMdd") + "_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                }
            }
            else
            {
                filename = DateTime.Now.ToString("yyyyMMdd") + "_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
            }
            return filename;
        }

        public async Task<List<OPDReportOutput>> PrintOPDPatientReport(PatientReportOPDDTO PatientOPDItem)
        {
            string DefaultConnection = string.Empty;
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            List<OPDReportOutput> result = new List<OPDReportOutput>();

            try
            {
                DefaultConnection = _config.GetConnectionString(ConfigKeys.DefaultConnection);
                string formatedResulttypes = string.Empty;
                formatedResulttypes = PatientOPDItem.OutputTypeNo != null && PatientOPDItem.OutputTypeNo != "" ? PatientOPDItem.OutputTypeNo : "";
                var lstOutputTypeNos = formatedResulttypes.Split(',');
                var Key = "";

                for (int i = 0; i < lstOutputTypeNos.Length; i++)
                {
                    Key = "";
                    if (lstOutputTypeNos[i] == "1" || lstOutputTypeNos[i] == "0")
                    {
                        Key = OPDReportKey.OPDCONSULTATIONREPORT;
                    }
                    else if (lstOutputTypeNos[i] == "2")
                    {
                        Key = OPDReportKey.TMPOPDCONSULTATIONREPORT;
                    }

                    OPDReportOutput item = new OPDReportOutput();
                    Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                    objdictionary.Add("AppointmentNo", PatientOPDItem.AppointmentNo);
                    objdictionary.Add("AppointmentDate", PatientOPDItem.AppointmentDate);
                    objdictionary.Add("PhysicianNo", PatientOPDItem.PhysicianNo.ToString());
                    objdictionary.Add("IsLogo", PatientOPDItem.IsHeaderFooter.ToString());
                    objdictionary.Add("IsEmpty", PatientOPDItem.IsEmpty.ToString());
                    objdictionary.Add("UserNo", PatientOPDItem.UserNo.ToString());
                    objdictionary.Add("VenueNo", PatientOPDItem.VenueNo.ToString());
                    objdictionary.Add("VenueBranchNo", PatientOPDItem.VenueBranchNo.ToString());
                    ReportContext objReportContext = new ReportContext(DefaultConnection);
                    TblReportMaster tblReportMaster = new TblReportMaster();

                    if (PatientOPDItem.IsEmpty == true)
                    {
                        Key += "EMPTY";
                    }
                    using (var context = new LIMSContext(DefaultConnection))
                    {
                        tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == Key && x.VenueNo == PatientOPDItem.VenueNo && x.VenueBranchNo == PatientOPDItem.VenueBranchNo).FirstOrDefault();
                       
                        if (tblReportMaster != null && !Directory.Exists(tblReportMaster.ExportPath))
                        {
                            Directory.CreateDirectory(tblReportMaster.ExportPath);
                        }
                    }
                    string opdApptNo = string.Concat(PatientOPDItem.AppointmentNo.Where(c => !char.IsWhiteSpace(c)));
                    string iFile = string.Empty;
                    iFile = opdApptNo + ".pdf";

                    System.Data.DataTable opdDataTable = objReportContext.getdatatable(objdictionary, tblReportMaster.ProcedureName);

                    if (Key == "TMPOPDCONSULTATIONREPORT" || Key == "TMPOPDCONSULTATIONREPORTEMPTY")
                    {
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("OPDTransTemplateFilePath");
                        string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                                        ? objAppSettingResponse.ConfigValue : "";

                        List<ConfigurationDto> lstConfigList = new List<ConfigurationDto>();
                        lstConfigList = _IMasterRepository.GetConfigurationList(PatientOPDItem.VenueNo, PatientOPDItem.VenueBranchNo);

                        int datablecount = opdDataTable.Rows.Count;
                        int startcount = 1;
                        datablecount = lstOutputTypeNos[i] == "2" ? datablecount : 1;

                        for (int s = 0; s < datablecount; s++)
                        {
                            path = path + PatientOPDItem?.VenueNo.ToString() + "\\" + opdDataTable?.Rows[s]["PatientNo"]?.ToString() + "\\" + opdDataTable?.Rows[s]["OPDAppointmentNo"]?.ToString();

                            if (Directory.Exists(path))
                            {
                                string[] filePaths = Directory.GetFiles(path);
                                if (filePaths != null && filePaths.Length > 0)
                                {
                                    if (File.Exists(filePaths[0].ToString()))
                                    {
                                        string content = File.ReadAllText(filePaths[0].ToString());
                                        opdDataTable.Rows[s]["TempResult"] = content;
                                    }
                                }
                            }
                            startcount = startcount++;
                        }
                    }

                    ReportParamDto objitem = new ReportParamDto();
                    objitem.datatable = CommonExtension.DatableToDicionary(opdDataTable);
                    objitem.paramerter = objdictionary;
                    objitem.ReportPath = tblReportMaster.ReportPath;
                    objitem.ExportPath = tblReportMaster.ExportPath + iFile;
                    objitem.ExportFormat = FileFormat.PDF;
                    string ReportParam = JsonConvert.SerializeObject(objitem);

                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("ReportServiceURL");
                    string ReportServiceURL = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";

                    string filename = await ExportReportService.ExportPrint(ReportParam, ReportServiceURL);

                    if (PatientOPDItem.Process == 3)
                        item.PatientExportFile = await CommonHelper.URLShorten(tblReportMaster.ExportURL + filename, _config.GetValue<string>(ConfigKeys.FireBaseAPIkey));
                    else
                        item.PatientExportFile = tblReportMaster.ExportURL + filename;

                    item.PatientExportFolderPath = objitem.ExportPath;

                    result.Add(item);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.PrintOPDPatientReport - AppointmentNo : " + PatientOPDItem.AppointmentNo, ExceptionPriority.High, ApplicationType.REPOSITORY, PatientOPDItem.VenueNo, PatientOPDItem.VenueBranchNo, PatientOPDItem.UserNo);
            }
            return result;
        }
    }
}