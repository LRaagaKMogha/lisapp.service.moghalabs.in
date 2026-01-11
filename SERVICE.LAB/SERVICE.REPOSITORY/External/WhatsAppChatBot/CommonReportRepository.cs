using Dev.IRepository;
using Dev.IRepository.External.WhatsAppChatBot;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Service.Model.External.WhatsAppChatBot;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Repository.External.WhatsAppChatBot
{
    public class CommonReportRepository : ICommonReportRepository
    {
        private IConfiguration _config;
        private IMasterRepository _mRepository;
        public CommonReportRepository(IConfiguration config, IMasterRepository mRepository) 
        {
            _config = config;
            _mRepository = mRepository;
        }

        public async Task<List<PatientReportResponse>> GetPatientReport(PatientReportRequest objReq)
        {
            List<PatientReportResponse> objResponse = new List<PatientReportResponse>();
            MasterRepository _IMasterRepository = new MasterRepository(_config);            
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            string DefaultConnection = string.Empty;
            try
            {
                if (objReq.IsDefault)
                    DefaultConnection = _config.GetConnectionString(ConfigKeys.DefaultConnection);
                else
                    DefaultConnection = _config.GetConnectionString(ConfigKeys.ArchiveDefaultConnection);

                var lstresulttypenos = objReq.ResultTypeNos.Split(',');
                var Key = "";
                for (int i = 0; i < lstresulttypenos.Length; i++)
                {
                    Key = "";
                    if (lstresulttypenos[i] == "1")
                    {
                        Key = ReportKey.PATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "2")
                    {
                        Key = ReportKey.MBPATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "3")
                    {
                        Key = ReportKey.TEMPPATIENTREPORT;
                    }
                    else if (lstresulttypenos[i] == "4")
                    {
                        Key = ReportKey.MTEMPPATIENTREPORT;
                    }

                    PatientReportResponse item = new PatientReportResponse();

                    Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                    objdictionary.Add("PageCode", objReq.PageCode);
                    objdictionary.Add("PatientVisitNo", objReq.PatientVisitNo);
                    objdictionary.Add("IsLogo", objReq.IsHeaderFooter.ToString());
                    objdictionary.Add("IsNABLlogo", objReq.IsNABLlogo.ToString());

                    objdictionary.Add("UserNo", objReq.UserNo.ToString());
                    objdictionary.Add("VenueNo", objReq.VenueNo.ToString());
                    objdictionary.Add("VenueBranchNo", objReq.VenueBranchNo.ToString());

                    ReportContext objReportContext = new ReportContext(DefaultConnection);
                    TblReportMaster tblReportMaster = new TblReportMaster();

                    using (var context = new LIMSContext(DefaultConnection))
                    {
                        tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == Key && x.VenueNo == objReq.VenueNo
                        && x.VenueBranchNo == objReq.VenueBranchNo).FirstOrDefault();
                        if (!Directory.Exists(tblReportMaster?.ExportPath))
                        {
                            Directory.CreateDirectory(tblReportMaster?.ExportPath);
                        }
                    }
                    string iFile = Guid.NewGuid().ToString("N").Substring(0, 6).Trim() + ".pdf";
                    objdictionary.Add("QRCodeURL", tblReportMaster?.ExportURL + iFile);

                    DataTable datable = objReportContext.getdatatable(objdictionary, tblReportMaster?.ProcedureName);

                    if (datable.Rows.Count > 0)
                    {
                        if (Key == "TEMPPATIENTREPORT" || Key == "MTEMPPATIENTREPORT")
                        {
                            objAppSettingResponse = new AppSettingResponse();
                            string AppTransTemplateFilePath = "TransTemplateFilePath";
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.TransTemplateFilePath);
                            objAppSettingResponse = new AppSettingResponse();
                            string AppDevExpressEditorConfig = "DevExpressEditorConfig";
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppDevExpressEditorConfig);
                            string deveditorconfigvalue = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.DevExpressEditorConfig);
                            string devExpressEditor = string.Empty;
                            List<ConfigurationDto> lstConfigList = new List<ConfigurationDto>();

                            lstConfigList = _mRepository.GetConfigurationList(objReq.VenueNo, objReq.VenueBranchNo);
                            devExpressEditor = lstConfigList != null ? lstConfigList.Where(d => d.ConfigurationKey == deveditorconfigvalue).Select(d => d.ConfigValue).SingleOrDefault().ToString() : "";
                            path = path + objReq?.VenueNo.ToString() + "/" + datable?.Rows[0]["orderListNo"]?.ToString() + "/" + datable?.Rows[0]["serviceNo"]?.ToString() + ".rtf";
                            if (!File.Exists(path))
                            {
                                objAppSettingResponse = new AppSettingResponse();
                                AppTransTemplateFilePath = "TransTemplateFilePath";
                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                                path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.TransTemplateFilePath);
                                path = path + objReq?.VenueNo.ToString() + "/" + datable?.Rows[0]["orderListNo"]?.ToString() + "/" + datable?.Rows[0]["serviceNo"]?.ToString() + ".ym";
                            }
                            if (File.Exists(path))
                            {
                                string content = File.ReadAllText(path);
                                datable.Rows[0]["result"] = content;
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
                                                            ? objAppSettingResponse.ConfigValue : "";// _config.GetConnectionString(ConfigKeys.MultiTemplateFormat);
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
                                    datable.Rows[0]["result"] = "";
                                }
                            }
                        }
                        else if (Key == "PATIENTREPORT")
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
                                                            ? objAppSettingResponse.ConfigValue : "";//  _config.GetConnectionString(ConfigKeys.TransFilePath);
                                            path = path + objReq.VenueNo.ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["OrderListNo"]).ToString() + ".ym";
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
                                                            ? objAppSettingResponse.ConfigValue : "";//_config.GetConnectionString(ConfigKeys.MasterFilePath);
                                            path = path + objReq.VenueNo.ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["ServiceNo"]).ToString() + ".ym";
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
                                                            ? objAppSettingResponse.ConfigValue : "";//  _config.GetConnectionString(ConfigKeys.TransFilePath);                                            
                                            path = path + objReq.VenueNo.ToString() + "/T/InterNotes/" + Convert.ToInt32(dr["OrderDetailsNo"]).ToString() + ".ym";
                                            if (File.Exists(path))
                                            {
                                                dr["TestInterNotes"] = File.ReadAllText(path);
                                                tstnotes = dr["TestInterNotes"].ToString();
                                            }
                                        }
                                        else if (Convert.ToInt32(dr["TestInter"]) == 1)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppMasterFilePath = "MasterFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                            ? objAppSettingResponse.ConfigValue : "";//_config.GetConnectionString(ConfigKeys.MasterFilePath);
                                            
                                            path = path + objReq.VenueNo.ToString() + "/T/InterNotes/" + Convert.ToInt32(dr["TestNo"]).ToString() + ".ym";
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
                            }
                        }

                        ReportParamDto objitem = new ReportParamDto();
                        objitem.datatable = CommonExtension.DatableToDicionary(datable);
                        objitem.paramerter = objdictionary;
                        objitem.ReportPath = tblReportMaster?.ReportPath;
                        objitem.ExportPath = tblReportMaster?.ExportPath + iFile;
                        objitem.ExportFormat = FileFormat.PDF;
                        string ReportParam = JsonConvert.SerializeObject(objitem);
                        objAppSettingResponse = new AppSettingResponse();
                        string AppReportServiceURL = "ReportServiceURL";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppReportServiceURL);
                        string serviceurl = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                            ? objAppSettingResponse.ConfigValue : ""; 
                        string filename = await ExportReportService.ExportPrint(ReportParam, serviceurl);

                        objAppSettingResponse = new AppSettingResponse();
                        string AppFireBaseAPIkey = "FireBaseAPIkey";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppFireBaseAPIkey);
                        string FIREBASEAPI = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                            ? objAppSettingResponse.ConfigValue : "";

                        if (objReq.Process == 3)
                            item.PatientExportFile = await CommonHelper.URLShorten(tblReportMaster?.ExportURL + filename, FIREBASEAPI);
                        else
                            item.PatientExportFile = tblReportMaster?.ExportURL + filename;

                        item.PatientExportFolderPath = objitem.ExportPath;
                        //attached outsourced documents
                        objAppSettingResponse = new AppSettingResponse();
                        string AppResultAckUpload = "ResultAckUpload";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload);
                        string ackpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                            ? objAppSettingResponse.ConfigValue : ""; //_config.GetConnectionString(ConfigKeys.ResultAckUpload);
                        int oldtestno = 0; int newtestno = 0;
                        foreach (DataRow row in datable.Rows)
                        {
                            if (datable.Columns.Contains("TestNo"))
                            {
                                foreach (DataColumn column in datable.Columns)
                                {
                                    if (row["VisitID"] != null && column.ColumnName == "VisitID")
                                    {
                                        newtestno = !string.IsNullOrEmpty(row["TestNo"].ToString()) ? Convert.ToInt32(row["TestNo"]) : 0;
                                        if (newtestno != oldtestno)
                                        { //if test have subtest then lnly once need to add outsource report
                                            objAppSettingResponse = new AppSettingResponse();
                                            AppResultAckUpload = "ResultAckUpload";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload);
                                            ackpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                            ? objAppSettingResponse.ConfigValue : ""; //_config.GetConnectionString(ConfigKeys.ResultAckUpload);
                                            ackpath = ackpath + "//" + objReq?.VenueNo  + "//" + objReq.PatientVisitNo + "//" + row["TestNo"];
                                            if (Directory.Exists(ackpath))
                                            {
                                                string[] files = Directory.GetFiles(ackpath);
                                                if (files != null && files.Length > 0)
                                                {
                                                    string resultname = Path.GetFileName(files[0]);
                                                    string ackfullpath = files[0];
                                                    string ackfilename = Path.GetFileNameWithoutExtension(ackfullpath);
                                                    string extension = Path.GetExtension(files[0]);
                                                    if (extension != null && (extension.ToLower() == ".pdf" || extension.ToLower() == "pdf"))
                                                    {
                                                        var ackfilenames = ackfilename.Split("$$");
                                                        if (ackfilenames != null && ackfilenames.Length > 0 &&
                                                            ackfilenames[ackfilenames.Length - 1] != null && ackfilenames[ackfilenames.Length - 1].ToLower() == "yes")
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
                    }
                    else
                    {
                        item.PatientExportFile = null;
                        item.PatientExportFolderPath = null;
                        item.ExportURL = null;
                    }
                    objResponse.Add(item);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientMasterRepository.GetPatientReport", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }

        void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }
    } 
}
