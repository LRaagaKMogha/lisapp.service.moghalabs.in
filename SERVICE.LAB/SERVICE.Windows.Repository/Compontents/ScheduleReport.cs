using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Service.Win.Common;
using System.Net;
using System.Xml.Linq;
using Service.Windows.Repository;
using System.Data.Entity;

namespace Service.Win.Repository
{
    public class ScheduleReport
    {
        public void PrintPatientReport()
        {
            try
            {
                var Reportlist = GetScheduleReportList();
                foreach (var reportqueue in Reportlist)
                {
                    var patientlst = GetPatientrecord(reportqueue.FromDate, reportqueue.Todate, reportqueue.VenueNo, reportqueue.VenueBranchNo);
                    foreach (var patientrecord in patientlst)
                    {
                        string filename = CommonExportReport(patientrecord.PatientVisitNo, reportqueue.VenueNo, reportqueue.VenueBranchNo, 0, "PCPI", patientrecord.ResultTypeNo, patientrecord.PatientName, reportqueue.IsHeader);
                    }
                    UpdateScheduleReport(reportqueue.ScheduleReportNo);
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
        }
        public string CommonExportReport(int patientVisitNo, int venueno, int venuebranchno, int userno, string pagecode, int? Resulttypeno, string PatientName, bool isheaderfooter)
        {
            string result = string.Empty;
            try
            {

                string Key = "";
                if (Resulttypeno == 1) Key = "PATIENTREPORT";
                else if (Resulttypeno == 2) Key = "MBPATIENTREPORT";
                else if (Resulttypeno == 3) Key = "TEMPPATIENTREPORT";
                else if (Resulttypeno == 4) Key = "MTEMPPATIENTREPORT";

                Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                objdictionary.Add("PageCode", pagecode);
                objdictionary.Add("PatientVisitNo", patientVisitNo.ToString());
                objdictionary.Add("OrderListNos", "");
                objdictionary.Add("IsLogo", isheaderfooter.ToString());
                objdictionary.Add("IsNABLlogo", "");

                objdictionary.Add("UserNo", "0");
                objdictionary.Add("VenueNo", venueno.ToString());
                objdictionary.Add("VenueBranchNo", venuebranchno.ToString());

                List<tbl_ReportMaster> reportlst = GetReportMaster();
                var tblReportMaster = reportlst.Where(x => x.ReportKey == Key && x.VenueNo == venueno
                 && x.VenueBranchNo == venuebranchno).FirstOrDefault();

                if (!Directory.Exists(tblReportMaster.ExportPath))
                {
                    Directory.CreateDirectory(tblReportMaster.ExportPath);
                }

                PatientName = string.Concat(PatientName.Where(c => !char.IsWhiteSpace(c)));
                string iFile = PatientName + "_" + Guid.NewGuid().ToString("N").Substring(0, 4) + ".pdf";
                objdictionary.Add("QRCodeURL", tblReportMaster.ExportURL + iFile);

                string LiveConnection = ConfigurationManager.ConnectionStrings["LiveConnection"].ConnectionString;
                DataContext objDataContext = new DataContext(LiveConnection);
                DataTable datable = objDataContext.getdatatable(objdictionary, tblReportMaster.ProcedureName);

                if (Key == "TEMPPATIENTREPORT" || Key == "MTEMPPATIENTREPORT")
                {
                    string path = ConfigurationManager.AppSettings["TransTemplateFilePath"].ToString();
                    path = path + venueno.ToString() + "/" + datable.Rows[0]["orderListNo"].ToString() + "/" + datable.Rows[0]["serviceNo"].ToString() + ".ym";

                    if (File.Exists(path))
                    {
                        string content = File.ReadAllText(path);
                        datable.Rows[0]["result"] = content;
                    }
                    else
                    {
                        if (Resulttypeno == 4)
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
                                                string fileformat = ConfigurationManager.AppSettings["MultiTemplateFormat"].ToString();
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
                        ////Multi Editor Option for histopathology
                        //string extension = System.IO.Path.GetExtension(path);
                        //path = path.Substring(0, path.Length - extension.Length);
                        //if (Directory.Exists(path))
                        //{
                        //    string contents = string.Empty;
                        //    string fileformat = ConfigurationManager.AppSettings["MultiTemplateFormat"].ToString();
                        //    foreach (string file in Directory.EnumerateFiles(path, fileformat))
                        //    {
                        //        contents = contents != null && contents != "" ? contents + "<br /><br /><br />" + File.ReadAllText(file) : File.ReadAllText(file);
                        //    }
                        //    datable.Rows[0]["result"] = contents;
                        //}

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
                                    string path = ConfigurationManager.AppSettings["TransFilePath"].ToString();
                                    path = path + venueno.ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["OrderListNo"]).ToString() + ".ym";
                                    if (File.Exists(path))
                                    {
                                        dr["GroupInterNotes"] = File.ReadAllText(path);
                                        grpnotes = dr["GroupInterNotes"].ToString();
                                    }
                                }
                                else if (Convert.ToInt32(dr["GroupInter"]) == 1)
                                {
                                    string path = ConfigurationManager.AppSettings["MasterFilePath"].ToString();
                                    path = path + venueno.ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["ServiceNo"]).ToString() + ".ym";
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
                                    string path = ConfigurationManager.AppSettings["TransFilePath"].ToString();
                                    path = path + venueno.ToString() + "/T/InterNotes/" + Convert.ToInt32(dr["OrderDetailsNo"]).ToString() + ".ym";
                                    if (File.Exists(path))
                                    {
                                        dr["TestInterNotes"] = File.ReadAllText(path);
                                        tstnotes = dr["TestInterNotes"].ToString();
                                    }
                                }
                                else if (Convert.ToInt32(dr["TestInter"]) == 1)
                                {
                                    string path = ConfigurationManager.AppSettings["MasterFilePath"].ToString();
                                    path = path + venueno.ToString() + "/T/InterNotes/" + Convert.ToInt32(dr["TestNo"]).ToString() + ".ym";
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
                ReportParamDTO objitem = new ReportParamDTO();
                objitem.datatable = CommonExtension.DatableToDicionary(datable);
                objitem.paramerter = objdictionary;
                objitem.ReportPath = tblReportMaster.ReportPath;
                objitem.ExportPath = tblReportMaster.ExportPath + iFile;
                objitem.ExportFormat = "PDF";
                string ReportParam = JsonConvert.SerializeObject(objitem);
                string filename = ExportPrint(ReportParam);
                result = tblReportMaster.ExportURL + filename + "|" + objitem.ExportPath;
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
            return result;
        }
        public List<tbl_ScheduleReport> GetScheduleReportList()
        {
            List<tbl_ScheduleReport> result = new List<tbl_ScheduleReport>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    result = context.tbl_ScheduleReport.Where(x => x.IsGenerate == 1 && x.Status == true).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
            return result;
        }

        public List<pro_GetScheduledPatientreport_Result> Getscheduledpatientreport()
        {
            List<pro_GetScheduledPatientreport_Result> result = new List<pro_GetScheduledPatientreport_Result>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    result = context.pro_GetScheduledPatientreport().ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
            return result;
        }

        public int UpdateScheduleReport(int ScheduleReportNo)
        {
            int result = 0;
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    var data = context.tbl_ScheduleReport.FirstOrDefault(a => a.ScheduleReportNo == ScheduleReportNo);
                    data.IsGenerate = 2;
                    context.Entry(data).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
            return result;
        }
        public List<Pro_SchedulePatientReport_Result> GetPatientrecord(DateTime Fromdate, DateTime Todate, int VenueNo, int VenueBranchNo)
        {
            List<Pro_SchedulePatientReport_Result> result = new List<Pro_SchedulePatientReport_Result>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    result = context.Pro_SchedulePatientReport(Fromdate, Todate, VenueNo, VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
            return result;
        }
        public List<tbl_ReportMaster> GetReportMaster()
        {
            List<tbl_ReportMaster> objresult = new List<tbl_ReportMaster>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    objresult = context.tbl_ReportMaster.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
            return objresult;
        }
        public string ExportPrint(string ReportParam)
        {
            string result = string.Empty;
            try
            {
                string ServiceMethod = ConfigurationManager.AppSettings["ReportServiceURL"].ToString() + "Report/ExportPrint";

                var ServiceWebRequest = WebRequest.CreateHttp(ServiceMethod);
                ServiceWebRequest.ContentType = "application/json; charset=utf-8";
                ServiceWebRequest.Method = "POST";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (var streamWriter = new StreamWriter(ServiceWebRequest.GetRequestStream()))
                {
                    string json = ReportParam;
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)ServiceWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = JsonConvert.DeserializeObject<string>(streamReader.ReadToEnd());
                }
                httpResponse.Close();
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
            return result;
        }

        public void GetAutoApproval()
        {
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    var objresult = context.pro_GetAutoApprovalResults().ToList();
                    foreach (var item in objresult)
                    {
                        PushMessage((int)item.PatientVisitNo, item.VenueNo, item.VenueBranchNo, item.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
        }
        private void PushMessage(int patientVisitNo, int venueno, int venuebranchno, string fullname)
        {
            List<Pro_GetCustomerNotification_Result> objresult = new List<Pro_GetCustomerNotification_Result>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    objresult = context.Pro_GetCustomerNotification(patientVisitNo, venueno, venuebranchno,0).ToList();
                    foreach (var item in objresult)
                    {
                        if (!string.IsNullOrEmpty(item.Address))
                        {
                            bool isheaderfooter;
                            if (venuebranchno == 2 && venuebranchno == 6)
                            {
                                if (item.MessageType.ToLower() == "sms")
                                    isheaderfooter = true;
                                else
                                    isheaderfooter = false;
                            }
                            else
                            {
                                isheaderfooter = true;
                            }
                            string resultoutput = CommonExportReport(patientVisitNo, venueno, venuebranchno, 0, "PCPR", 1, fullname.Replace(".", ""), isheaderfooter);
                            if (!string.IsNullOrEmpty(resultoutput))
                            {
                                MobileEntityHelper objMobileEntityHelper = new MobileEntityHelper();

                                string shortURL = CommonExtension.URLShorten(resultoutput.Split('|')[0].ToString(), ConfigurationManager.AppSettings["FireBaseAPIkey"].ToString());
                                Dictionary<string, string> objMessageItem = new Dictionary<string, string>();
                                objMessageItem.Add("#PaitentName#", item.FullName);
                                objMessageItem.Add("#VisitID#", item.VisitID);
                                objMessageItem.Add("#URL#", shortURL);

                                XElement XMLMessageNode = new XElement("MessageQueue", objMessageItem?.Select(kv => new XElement("Content",
                                 new XAttribute("key", kv.Key), new XAttribute("value", kv.Value))));

                                Dictionary<string, string> objAttachment = new Dictionary<string, string>();
                                objAttachment.Add(item.VisitID + ".pdf", resultoutput.Split('|')[1].ToString());
                                objAttachment.Add(Path.GetFileName(item.EmbedURL), item.EmbedURL);

                                XElement XMLAttachmentNode = XMLAttachmentNode = new XElement("AttachmentXML", objAttachment?.Select(kv => new XElement("AttachmentContent",
                                    new XAttribute("key", kv.Key), new XAttribute("value", kv.Value))));

                                objMobileEntityHelper.PushMessage("Patient_Approve_" + item.MessageType, item.MessageType, item.Address, "", "",
                                    XMLMessageNode.ToString(), true, XMLAttachmentNode.ToString(), DateTime.Now, venueno, venuebranchno, 0);

                                context.pro_UpdateAutoApprovalResults(patientVisitNo, venueno, venuebranchno);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
        }

        //public string PrintPatientReport1(int patientVisitNo, int venueno, int venuebranchno, int userno, string pagecode, int? Resulttypeno, string PatientName, bool isheaderfooter, string exportPath = null)
        //{
        //    Console.WriteLine("PrintPatientReport - Start");

        //    string DefaultConnection = string.Empty;
        //    string result = string.Empty;
        //    //List<ReportOutput> result = new List<ReportOutput>();
        //    try
        //    {
        //        //if (!PatientItem.isdefault)
        //        //    DefaultConnection = _config.GetValue<string>(ConfigKeys.DefaultConnection);
        //        //else
        //        //    DefaultConnection = _config.GetValue<string>(ConfigKeys.ArchiveDefaultConnection);
        //        ////get the config for the resulttype, pdf should be generated as per the config order
        //        //MasterRepository _IMasterRepository2 = new MasterRepository(_config);
        //        //ConfigurationDTO objConfigurationDTO2 = new ConfigurationDTO();
        //        string resulttypeorderconfig = "ResultTypeOrder";
        //        //objConfigurationDTO2 = _IMasterRepository2.GetSingleConfiguration(PatientItem.venueno, PatientItem.venuebranchno, resulttypeorderconfig);
        //        string configval = ConfigurationManager.AppSettings["ResultTypeOrder"].ToString();
        //        string formatedResulttypes = string.Empty;
        //        //if (Resulttypeno != null &&
        //        //    Resulttypeno != "" && PatientItem.resulttypenos.IndexOf(',') > 0 &&  configval != null)
        //        //{
        //        //    for (int t = 0; t < configval.Length; t++)
        //        //    {
        //        //        if (formatedResulttypes != null && formatedResulttypes != "")
        //        //        {
        //        //            formatedResulttypes = PatientItem.resulttypenos.IndexOf(configval[t]) >= 0
        //        //                ? formatedResulttypes + "," + configval[t].ToString() : formatedResulttypes;
        //        //        }
        //        //        else
        //        //        {
        //        //            formatedResulttypes = PatientItem.resulttypenos.IndexOf(configval[t]) >= 0
        //        //                ? configval[t].ToString() : "";
        //        //        }
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    formatedResulttypes = PatientItem.resulttypenos != null && PatientItem.resulttypenos != "" ? PatientItem.resulttypenos : "";
        //        //}
        //        ////
        //        //var lstresulttypenos = formatedResulttypes.Split(',');
        //        //var deltaneededlst = PatientItem.isDeltaNeeded != null && PatientItem.isDeltaNeeded != "" ? PatientItem.isDeltaNeeded.Split(',') : new string[] { };
        //        ////deleta selected orderlistno's
        //        //var ordlst = PatientItem.orderlistnos.Split(',');
        //        //var deltaavailorderlst = string.Empty;
        //        //if (deltaneededlst != null && deltaneededlst.Count() > 0)
        //        //{
        //        //    for (int q = 0; q < deltaneededlst.Count(); q++)
        //        //    {
        //        //        if (deltaneededlst[q] == "true" || deltaneededlst[q] == "True")
        //        //        {
        //        //            deltaavailorderlst = deltaavailorderlst != null && deltaavailorderlst != "" ?
        //        //                deltaavailorderlst + "," + ordlst[q].ToString() : ordlst[q].ToString();
        //        //        }
        //        //    }
        //        //}
        //        //var deltaselectorderlst = deltaavailorderlst != null && deltaavailorderlst != "" ? deltaavailorderlst.Split(',') : new string[] { };
        //        //
        //        var Key = "";
        //        //for (int i = 0; i < Resulttypeno.Length; i++)
        //        //{
        //        Key = "";
        //        if (Resulttypeno == 1) Key = "PATIENTREPORT";
        //        else if (Resulttypeno == 2) Key = "MBPATIENTREPORT";
        //        else if (Resulttypeno == 3) Key = "TEMPPATIENTREPORT";
        //        else if (Resulttypeno == 4) Key = "MTEMPPATIENTREPORT";



        //        //ReportOutput item = new ReportOutput();
        //        Dictionary<string, string> objdictionary = new Dictionary<string, string>();
        //        objdictionary.Add("PageCode", pagecode);
        //        objdictionary.Add("PatientVisitNo", patientVisitNo.ToString());
        //        objdictionary.Add("OrderListNos", "");
        //        objdictionary.Add("IsLogo", isheaderfooter.ToString());
        //        objdictionary.Add("IsNABLlogo", "");

        //        objdictionary.Add("UserNo", "0");
        //        objdictionary.Add("VenueNo", venueno.ToString());
        //        objdictionary.Add("VenueBranchNo", venuebranchno.ToString());


        //        List<tbl_ReportMaster> reportlst = GetReportMaster();
        //        var tblReportMaster = reportlst.Where(x => x.ReportKey == Key && x.VenueNo == venueno
        //         && x.VenueBranchNo == venuebranchno).FirstOrDefault();

        //        //ReportContext objReportContext = new ReportContext(DefaultConnection);
        //        //TblReportMaster tblReportMaster = new TblReportMaster();
        //        ////changes made for gmt - sims demo by senba
        //        //if (PatientItem.isProvisional == true ||
        //        //        (PatientItem.venueno == 1 && PatientItem.venuebranchno == 1 &&
        //        //        (PatientItem.pagecode == "PCRE" ||
        //        //          PatientItem.pagecode == "PCRV" ||
        //        //          PatientItem.pagecode == "PCRA")))

        //        //    {
        //        //        //MasterRepository _IMasterRepository = new MasterRepository(_config);
        //        //        //ConfigurationDTO objConfigurationDTO = new ConfigurationDTO();
        //        //        string watermarkinreportconfig = ConfigurationManager.AppSettings["IsWaterMarkinReport"].ToString();
        //        //        //objConfigurationDTO = _IMasterRepository.GetSingleConfiguration(PatientItem.venueno, PatientItem.venuebranchno, watermarkinreportconfig);
        //        //        if (watermarkinreportconfig != null && watermarkinreportconfig == "1")
        //        //        {
        //        //            Key = Key + "WATERMARK";
        //        //        }
        //        //    }

        //        string watermarkinreportconfig = ConfigurationManager.AppSettings["IsWaterMarkinReport"].ToString();
        //        //objConfigurationDTO = _IMasterRepository.GetSingleConfiguration(PatientItem.venueno, PatientItem.venuebranchno, watermarkinreportconfig);
        //        if (watermarkinreportconfig != null && watermarkinreportconfig == "1")
        //        {
        //            Key = Key + "WATERMARK";
        //        }



        //        tblReportMaster.ExportPath = exportPath ?? tblReportMaster.ExportPath;

        //        if (!Directory.Exists(tblReportMaster.ExportPath))
        //        {
        //            Directory.CreateDirectory(tblReportMaster.ExportPath);
        //        }


        //        string patientvisitid = PatientName.ToString();

        //        string resultStatusDDAvailConfig4 = ConfigurationManager.AppSettings["GUIDNoNeedReportName"].ToString();

        //        string iFile = string.Empty;
        //        if (resultStatusDDAvailConfig4 != null && resultStatusDDAvailConfig4 == "1")
        //        {
        //            iFile = patientvisitid + ".pdf";
        //        }
        //        else
        //        {
        //            iFile = patientvisitid + "_" + Guid.NewGuid().ToString("N").Substring(0, 4) + ".pdf";
        //        }
        //        objdictionary.Add("QRCodeURL", tblReportMaster.ExportURL + iFile);
        //        objdictionary.Add("IsProvisional", ConfigurationManager.AppSettings["IsProvisional"].ToString());

        //        if (Key == "MBPATIENTREPORT" || Key == "MTEMPPATIENTREPORT")
        //        {
        //            //MasterRepository _IMasterRepository = new MasterRepository(_config);
        //            //ConfigurationDTO objConfigurationDTO = new ConfigurationDTO();
        //            string resultStatusDDAvailConfig = ConfigurationManager.AppSettings["IsRsultStatusDDAvail"].ToString();
        //            //objConfigurationDTO = _IMasterRepository.GetSingleConfiguration(PatientItem.venueno, PatientItem.venuebranchno, resultStatusDDAvailConfig);
        //            if (resultStatusDDAvailConfig != null && resultStatusDDAvailConfig == "1")
        //            {
        //                objdictionary.Add("ReportStatus", ConfigurationManager.AppSettings["ReportStatus"].ToString());
        //            }
        //        }

        //        string LiveConnection = ConfigurationManager.ConnectionStrings["LiveConnection"].ConnectionString;
        //        DataContext objReportContext = new DataContext(LiveConnection);
        //        DataTable datable = objReportContext.getdatatable(objdictionary, tblReportMaster.ProcedureName);

        //        if (Key == "TEMPPATIENTREPORT" || Key == "MTEMPPATIENTREPORT" || Key == "TEMPPATIENTREPORTWATERMARK")
        //        {
        //            if (Key == "MTEMPPATIENTREPORT")
        //            {
        //                //COMMENTS ADDED FOR MULTITEMP REPORT                           

        //                if (datable?.Rows[0]["IsComments"] != null && datable?.Rows[0]["IsComments"].ToString() == "True")
        //                {
        //                    string commentspath = ConfigurationManager.AppSettings["MasterFilePath"].ToString();
        //                    commentspath = commentspath + venueno.ToString() + "//" + "T" + "//" + "Comments" + "//" + datable?.Rows[0]["ServiceNo"]?.ToString() + ".ym";

        //                    if (File.Exists(commentspath))
        //                    {
        //                        string content = File.ReadAllText(commentspath);
        //                        for (int z = 0; z < datable.Rows.Count; z++)
        //                        {
        //                            if (datable.Rows[z]["IsComments"] != null && datable?.Rows[z]["IsComments"].ToString() == "True")
        //                            {
        //                                datable.Rows[z]["comments"] = content;
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            string path = ConfigurationManager.AppSettings["TransTemplateFilePath"].ToString();
        //            string deveditorconfigvalue = ConfigurationManager.AppSettings["DevExpressEditorConfigValue"].ToString();

        //            // string devExpressEditor = string.Empty;
        //            //List<ConfigurationDTO> lstConfigList = new List<ConfigurationDTO>();
        //            //IMasterRepository objMasterRepository = new MasterRepository(_config);
        //            //lstConfigList = objMasterRepository.GetConfigurationList(PatientItem.venueno, PatientItem.venuebranchno);
        //            //devExpressEditor = lstConfigList != null ? lstConfigList.Where(d => d.ConfigurationKey == deveditorconfigvalue).Select(d => d.ConfigValue).SingleOrDefault().ToString() : "";
        //            ////if (devExpressEditor != null && devExpressEditor == "1")
        //            ////{
        //            int datablecount = datable.Rows.Count;//if we have 2 tempalte test inside the package, then that 2 test should shown 
        //            int startcount = 1;
        //            datablecount = Resulttypeno == 3 ? datablecount : 1;
        //            for (int s = 0; s < datablecount; s++)
        //            {
        //                path = ConfigurationManager.AppSettings["TransTemplateFilePath"].ToString();
        //                path = path + venueno.ToString() + "/" + datable?.Rows[s]["orderListNo"]?.ToString() + "/" + datable?.Rows[s]["serviceNo"]?.ToString() + ".rtf";
        //                if (!File.Exists(path))
        //                {
        //                    path = ConfigurationManager.AppSettings["TransTemplateFilePath"].ToString();
        //                    path = path + venueno.ToString() + "/" + datable?.Rows[s]["orderListNo"]?.ToString() + "/" + datable?.Rows[s]["serviceNo"]?.ToString() + ".ym";
        //                }

        //                if (File.Exists(path))
        //                {
        //                    string content = File.ReadAllText(path);
        //                    datable.Rows[s]["result"] = content;
        //                }
        //                else
        //                {
        //                    if (Resulttypeno == 4 && startcount == 1)
        //                    {
        //                        int oldorderlistno = 0;
        //                        int neworderlistno = 0;
        //                        int oldserviceno = 0;
        //                        int newserviceno = 0;
        //                        //Multi Editor Option for histopathology
        //                        string extension = System.IO.Path.GetExtension(path);
        //                        path = path.Substring(0, path.Length - extension.Length);
        //                        if (Directory.Exists(path))
        //                        {
        //                            foreach (DataRow row in datable.Rows)
        //                            {

        //                                foreach (DataColumn column in datable.Columns)
        //                                {
        //                                    if (row["SubTestNo"] != null && Convert.ToInt32(row["SubTestNo"].ToString()) > 0)
        //                                    {
        //                                        if (column.ColumnName.ToLower() == "result") // This will check the null values also (if you want to check).
        //                                        {
        //                                            if (oldorderlistno != neworderlistno && neworderlistno > 0)
        //                                            {
        //                                                path = path.Replace(neworderlistno.ToString(), oldorderlistno.ToString());
        //                                                path = path.Replace(newserviceno.ToString(), oldserviceno.ToString());
        //                                            }
        //                                            neworderlistno = row["orderlistno"] != null ? Convert.ToInt32(row["orderlistno"]) : 0;
        //                                            newserviceno = row["serviceNo"] != null ? Convert.ToInt32(row["serviceNo"]) : 0;

        //                                            string fileformat = ConfigurationManager.AppSettings["MultiTemplateFormat"].ToString();
        //                                            string overallpath = path + "/" + row["SubTestNo"].ToString() + fileformat;
        //                                            //check result type based report data avail 
        //                                            int ismultiflag = datable.Columns.Contains("IsMultiEditor") && row["IsMultiEditor"] != null && Convert.ToInt32(row["IsMultiEditor"].ToString()) > 0 ? 1 : 0;
        //                                            string oldreporttypepath = string.Empty;
        //                                            //if (PatientItem.reportstatus != null && PatientItem.reportstatus > 0 && ismultiflag != null && ismultiflag > 0)
        //                                            //{
        //                                            //    oldreporttypepath = path + "/" + PatientItem.reportstatus.ToString() + "/" + row["SubTestNo"].ToString() + fileformat;
        //                                            //}
        //                                            //if (!String.IsNullOrEmpty(oldreporttypepath) && File.Exists(oldreporttypepath))
        //                                            //{
        //                                            //    row["result"] = File.ReadAllText(oldreporttypepath);
        //                                            //}
        //                                            ////
        //                                            //else if (File.Exists(overallpath))
        //                                            //{
        //                                            //    row["result"] = File.ReadAllText(overallpath);
        //                                            //}
        //                                            //else
        //                                            //{
        //                                            //    row["result"] = "";
        //                                            //}
        //                                        }
        //                                    }
        //                                }
        //                                oldorderlistno = row["orderlistno"] != null ? Convert.ToInt32(row["orderlistno"]) : 0;
        //                                oldserviceno = row["serviceNo"] != null ? Convert.ToInt32(row["serviceNo"]) : 0;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        datable.Rows[s]["result"] = "";
        //                    }
        //                }
        //                startcount = startcount++;
        //            }
        //        }
        //        else if (Key == "PATIENTREPORT")
        //        {
        //            int sno = 0;
        //            string grpnotes = "";
        //            int tno = 0;
        //            string tstnotes = "";
        //            foreach (DataRow dr in datable.Rows)
        //            {
        //                if (Convert.ToBoolean(dr["IsGrpTestInter"]) == false)
        //                {
        //                    if (sno != Convert.ToInt32(dr["ServiceNo"]))
        //                    {
        //                        grpnotes = "";
        //                        sno = Convert.ToInt32(dr["ServiceNo"]);
        //                        if (Convert.ToInt32(dr["GroupInter"]) == 2)
        //                        {
        //                            string path = ConfigurationManager.AppSettings["TransFilePath"].ToString();
        //                            path = path + venueno.ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["OrderListNo"]).ToString() + ".ym";
        //                            if (File.Exists(path))
        //                            {
        //                                dr["GroupInterNotes"] = File.ReadAllText(path);
        //                                grpnotes = dr["GroupInterNotes"].ToString();
        //                            }
        //                        }
        //                        else if (Convert.ToInt32(dr["GroupInter"]) == 1)
        //                        {
        //                            string path = ConfigurationManager.AppSettings["MasterFilePath"].ToString();
        //                            path = path + venueno.ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["ServiceNo"]).ToString() + ".ym";
        //                            if (File.Exists(path))
        //                            {
        //                                dr["GroupInterNotes"] = File.ReadAllText(path);
        //                                grpnotes = dr["GroupInterNotes"].ToString();
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        dr["GroupInterNotes"] = grpnotes;
        //                    }
        //                }
        //                else if (Convert.ToBoolean(dr["IsGrpTestInter"]) == true)
        //                {
        //                    if (tno != Convert.ToInt32(dr["TestNo"]))
        //                    {
        //                        tstnotes = "";
        //                        tno = Convert.ToInt32(dr["TestNo"]);
        //                        if (Convert.ToInt32(dr["TestInter"]) == 2)
        //                        {
        //                            string path = ConfigurationManager.AppSettings["TransFilePath"].ToString();
        //                            path = path + venueno.ToString() + "/T/InterNotes/" + Convert.ToInt32(dr["OrderDetailsNo"]).ToString() + ".ym";
        //                            if (File.Exists(path))
        //                            {
        //                                dr["TestInterNotes"] = File.ReadAllText(path);
        //                                tstnotes = dr["TestInterNotes"].ToString();
        //                            }
        //                        }
        //                        else if (Convert.ToInt32(dr["TestInter"]) == 1)
        //                        {
        //                            string internotesresflag = dr["resultflag"] != null ? dr["resultflag"].ToString() : "";
        //                            string FPath = internotesresflag == "H" ? Convert.ToInt32(dr["TestNo"]).ToString() + "_H" + ".ym" : internotesresflag == "L" ? Convert.ToInt32(dr["TestNo"]).ToString() + "_L" + ".ym" : Convert.ToInt32(dr["TestNo"]).ToString() + ".ym";
        //                            string path = ConfigurationManager.AppSettings["MasterFilePath"].ToString();
        //                            path = path + venueno.ToString() + "/T/InterNotes/" + FPath;
        //                            if (File.Exists(path))
        //                            {
        //                                dr["TestInterNotes"] = File.ReadAllText(path);
        //                                tstnotes = dr["TestInterNotes"].ToString();
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        dr["TestInterNotes"] = tstnotes;
        //                    }
        //                }
        //                if (datable.Columns.Contains("GraphImage1"))
        //                {
        //                    //machine graph attached to the final report
        //                    string GraphURL = dr["GraphURL"].ToString();
        //                    string actualmachineimagepath = ConfigurationManager.AppSettings["MachineImagePath"].ToString();
        //                    string machineimagepath = ConfigurationManager.AppSettings["MachineImagePath"].ToString();
        //                    machineimagepath = machineimagepath + "//" + venueno + "//" + dr["ProcessingBranchNo"] + "//" + dr["BarcodeNoNew"].ToString();

        //                    if (Directory.Exists(machineimagepath))
        //                    {
        //                        string[] files = Directory.GetFiles(machineimagepath);
        //                        if (files != null && files.Length > 0)
        //                        {
        //                            for (int g = 0; g < files.Length; g++)

        //                            {
        //                                string machinefullpath = files[g];
        //                                string serviceno = machinefullpath.Replace(machineimagepath, "").Replace("//", "").Replace("\\", "").Replace(".png", "");
        //                                var servicenolst = serviceno.Split('T').Length > 1 ? serviceno.Split('T') : serviceno.Split('S');

        //                                string servcno = servicenolst.Length > 1 ? servicenolst[1].ToString() : "";
        //                                if (servcno == dr["TestNo"].ToString() || (servcno == dr["SubTestNo"].ToString()))
        //                                {
        //                                    if (g == 0 || dr["GraphImage1"].ToString() == "")
        //                                    {
        //                                        dr["GraphImage1"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
        //                                    }
        //                                    else if (datable.Columns.Contains("GraphImage2") && (g == 1 || dr["GraphImage2"].ToString() == ""))
        //                                    {
        //                                        dr["GraphImage2"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
        //                                    }
        //                                    else if (datable.Columns.Contains("GraphImage3") && (g == 2 || dr["GraphImage3"].ToString() == ""))
        //                                    {
        //                                        dr["GraphImage3"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
        //                                    }
        //                                    else if (datable.Columns.Contains("GraphImage4") && (g == 3 || dr["GraphImage4"].ToString() == ""))
        //                                    {
        //                                        dr["GraphImage4"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
        //                                    }
        //                                    else if (datable.Columns.Contains("GraphImage5") && (g == 4 || dr["GraphImage5"].ToString() == ""))
        //                                    {
        //                                        dr["GraphImage5"] = machinefullpath.Replace(actualmachineimagepath, GraphURL);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //                ////individual delata - generated while approve the result - need to be aattached in final report 
        //                //int isdeltaoptionselect = deltaselectorderlst != null && deltaselectorderlst.Count() > 0 &&
        //                //    deltaselectorderlst.Contains(dr["orderlistno"].ToString()) ? 1 : 0;

        //                //if (datable.Columns.Contains("IndividualDeltaPath") && datable.Columns.Contains("individualDeltaURL") && isdeltaoptionselect == 1)
        //                //{
        //                //    //generate delta report
        //                //    PatientDeltaReport obj = new PatientDeltaReport();
        //                //    List<ReportOutput> lstobj = new List<ReportOutput>();
        //                //    PatientReportRepository reportrep = new PatientReportRepository(_config);
        //                //    obj.PatientVisitNo = Convert.ToInt32(PatientItem.patientvisitno);
        //                //    obj.OrderListNos = dr["OrderListNo"] != null ? dr["OrderListNo"].ToString() : "0";//;
        //                //    obj.SubTestNo = dr["SubTestNo"] != null ? Convert.ToInt32(dr["SubTestNo"]) : 0;// od.subtestno;
        //                //    obj.PageCode = PatientItem.pagecode;
        //                //    obj.VenueNo = PatientItem.venueno;
        //                //    obj.VenueBranchNo = PatientItem.venuebranchno;
        //                //    obj.UserNo = PatientItem.userno;
        //                //    obj.QRCodeURL = "";
        //                //    obj.IsLogo = false;
        //                //    obj.IsNABLlogo = false;
        //                //    obj.IsDraft = false;
        //                //    obj.IsSubReport = obj.SubTestNo > 0 ? Convert.ToInt16(1) : Convert.ToInt16(0);
        //                //    obj.DIsdefault = false;
        //                //    obj.DProcess = 0;
        //                //    obj.TestNo = dr["TestNo"] != null ? Convert.ToInt32(dr["TestNo"]) : 0;
        //                //    lstobj = reportrep.PrintDelateReport(obj);
        //                //    if (lstobj != null && lstobj.Count > 0)
        //                //    {
        //                //        dr["IndividualDeltaPath"] = lstobj[0].PatientExportFolderPath;
        //                //        dr["individualDeltaURL"] = lstobj[0].PatientExportFile;
        //                //    }

        //                //}
        //            }
        //        }
        //        else if (Key == "PATIENTREPORTWATERMARK")
        //        {
        //            //foreach (DataRow dr in datable.Rows)
        //            //{
        //            //    //individual delata - generated while approve the result - need to be aattached in final report 
        //            //    int isdeltaoptionselect = deltaselectorderlst != null && deltaselectorderlst.Count() > 0 &&
        //            //        deltaselectorderlst.Contains(dr["orderlistno"].ToString()) ? 1 : 0;
        //            //    if (datable.Columns.Contains("IndividualDeltaPath") && datable.Columns.Contains("individualDeltaURL") && isdeltaoptionselect == 1)
        //            //    {
        //            //        //generate delta report
        //            //        PatientDeltaReport obj = new PatientDeltaReport();
        //            //        List<ReportOutput> lstobj = new List<ReportOutput>();
        //            //        PatientReportRepository reportrep = new PatientReportRepository(_config);
        //            //        obj.PatientVisitNo = Convert.ToInt32(PatientItem.patientvisitno);
        //            //        obj.OrderListNos = dr["OrderListNo"] != null ? dr["OrderListNo"].ToString() : "0";//;
        //            //        obj.SubTestNo = dr["SubTestNo"] != null ? Convert.ToInt32(dr["SubTestNo"]) : 0;// od.subtestno;
        //            //        obj.PageCode = PatientItem.pagecode;
        //            //        obj.VenueNo = PatientItem.venueno;
        //            //        obj.VenueBranchNo = PatientItem.venuebranchno;
        //            //        obj.UserNo = PatientItem.userno;
        //            //        obj.QRCodeURL = "";
        //            //        obj.IsLogo = false;
        //            //        obj.IsNABLlogo = false;
        //            //        obj.IsDraft = false;
        //            //        obj.IsSubReport = obj.SubTestNo > 0 ? Convert.ToInt16(1) : Convert.ToInt16(0);
        //            //        obj.DIsdefault = false;
        //            //        obj.DProcess = 0;
        //            //        obj.TestNo = dr["TestNo"] != null ? Convert.ToInt32(dr["TestNo"]) : 0;
        //            //        lstobj = reportrep.PrintDelateReport(obj);
        //            //        if (lstobj != null && lstobj.Count > 0)
        //            //        {
        //            //            dr["IndividualDeltaPath"] = lstobj[0].PatientExportFolderPath;
        //            //            dr["individualDeltaURL"] = lstobj[0].PatientExportFile;
        //            //        }

        //            //    }
        //            //}
        //        }
        //        //assign all images path into last test of the group if graph is available for the group test
        //        if (datable.Columns.Contains("GraphImage1"))
        //        {
        //            var results = from DataRow myRow in datable.Rows
        //                          where (string)myRow["GraphImage1"] != "" && (string)myRow["GroupName"] != ""
        //                          select myRow;
        //            // test have grpah image inside the group
        //            if (results != null && results.ToList().Count() > 0)
        //            {

        //                int imagescount = 1;
        //                int graphavailcount = results.ToList().Count();
        //                string groupname = String.Empty;
        //                for (int g = 0; g < graphavailcount; g++)
        //                {
        //                    var groupcountlst = from DataRow myRow in datable.Rows
        //                                        where (string)myRow["GroupName"] == results.ToList()[g].ItemArray[23].ToString()
        //                                        select myRow;
        //                    //if grpah have more than a group test
        //                    if (groupname != results.ToList()[g].ItemArray[23].ToString())
        //                    {
        //                        imagescount = 1;
        //                    }
        //                    int groupcount = groupcountlst != null ? groupcountlst.ToList().Count() : 0;
        //                    string graph1 = results.ToList()[g].ItemArray[79].ToString();
        //                    string graph2 = results.ToList()[g].ItemArray[80].ToString();
        //                    string graph3 = results.ToList()[g].ItemArray[81].ToString();
        //                    string graph4 = results.ToList()[g].ItemArray[82].ToString();
        //                    string graph5 = results.ToList()[g].ItemArray[83].ToString();

        //                    int dtgroupcount = 0;
        //                    foreach (DataRow dtrow in datable?.Rows)
        //                    {
        //                        if (dtrow["GroupName"].ToString() != "" && dtrow["GroupName"].ToString() != null && dtrow["GroupName"].ToString() == results.ToList()[g].ItemArray[23].ToString())
        //                        {
        //                            dtgroupcount = dtgroupcount + 1;
        //                            if (groupcount == dtgroupcount)
        //                            {
        //                                if (imagescount == 1)
        //                                {
        //                                    dtrow["GraphImage1"] = graph1;
        //                                    if (graph2 != "") { dtrow["GraphImage2"] = graph2; }
        //                                    if (graph3 != "") { dtrow["GraphImage3"] = graph3; }
        //                                    if (graph4 != "") { dtrow["GraphImage4"] = graph4; }
        //                                    if (graph5 != "") { dtrow["GraphImage5"] = graph5; }
        //                                }
        //                                if (imagescount == 2)
        //                                {
        //                                    if (graph1 != "" && dtrow["GraphImage2"].ToString() == "") { dtrow["GraphImage2"] = graph1; }
        //                                    if (graph2 != "" && dtrow["GraphImage3"].ToString() == "") { dtrow["GraphImage3"] = graph2; }
        //                                    if (graph3 != "" && dtrow["GraphImage4"].ToString() == "") { dtrow["GraphImage4"] = graph3; }
        //                                    if (graph4 != "" && dtrow["GraphImage5"].ToString() == "") { dtrow["GraphImage5"] = graph4; }
        //                                }
        //                                if (imagescount == 3)
        //                                {
        //                                    if (graph1 != "" && dtrow["GraphImage3"].ToString() == "") { dtrow["GraphImage3"] = graph1; }
        //                                    if (graph2 != "" && dtrow["GraphImage4"].ToString() == "") { dtrow["GraphImage4"] = graph2; }
        //                                    if (graph3 != "" && dtrow["GraphImage5"].ToString() == "") { dtrow["GraphImage5"] = graph3; }
        //                                }
        //                                if (imagescount == 4)
        //                                {
        //                                    if (graph1 != "" && dtrow["GraphImage4"].ToString() == "") { dtrow["GraphImage4"] = graph1; }
        //                                    if (graph2 != "" && dtrow["GraphImage5"].ToString() == "") { dtrow["GraphImage5"] = graph2; }
        //                                }
        //                                if (imagescount == 5)
        //                                {
        //                                    if (graph1 != "" && dtrow["GraphImage5"].ToString() == "") { dtrow["GraphImage5"] = graph1; }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    groupname = results.ToList()[g].ItemArray[23].ToString();
        //                    imagescount = imagescount + 1;
        //                }
        //                string ggroupname = String.Empty;
        //                //remove graph path for all other test name except last test of group 
        //                for (int g = 0; g < graphavailcount; g++)
        //                {
        //                    var groupcountlst = from DataRow myRow in datable.Rows
        //                                        where (string)myRow["GroupName"] == results.ToList()[g].ItemArray[23].ToString()
        //                                        select myRow;
        //                    if (groupcountlst != null && groupcountlst.ToList().Count() > 0)
        //                    {
        //                        string lastGrpTestName = groupcountlst != null ? groupcountlst.ToList()[groupcountlst.ToList().Count() - 1].ItemArray[30].ToString() : "";
        //                        string lastGrpTestNo = groupcountlst != null ? groupcountlst.ToList()[groupcountlst.ToList().Count() - 1].ItemArray[29].ToString() : "";
        //                        if (lastGrpTestName != null && lastGrpTestName != "")
        //                        {
        //                            if (ggroupname != results.ToList()[g].ItemArray[23].ToString() && results.ToList()[g].ItemArray[23].ToString() != "")
        //                            {
        //                                foreach (DataRow dtbrow in datable.Rows)
        //                                {
        //                                    if (dtbrow["GroupName"].ToString() != "" && dtbrow["GroupName"].ToString() == results.ToList()[g].ItemArray[23].ToString() &&
        //                                        (dtbrow["TestName"].ToString() != lastGrpTestName))
        //                                    {
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    ggroupname = results.ToList()[g].ItemArray[23].ToString();
        //                }
        //            }
        //        }
        //        //

        //        ReportParamDTO objitem = new ReportParamDTO();
        //        objitem.datatable = CommonExtension.DatableToDicionary(datable);
        //        objitem.paramerter = objdictionary;
        //        objitem.ReportPath = tblReportMaster.ReportPath;
        //        objitem.ExportPath = tblReportMaster.ExportPath + iFile;
        //        objitem.ExportFormat = ".pdf";
        //        string ReportParam = JsonConvert.SerializeObject(objitem);
        //        //string filename = ExportReportService.ExportPrint(ReportParam, _config.GetValue<string>(ConfigKeys.ReportServiceURL));
        //        string filename = ExportPrint(ReportParam);
        //        result = tblReportMaster.ExportURL + filename + "|" + objitem.ExportPath;

        //        //if (Resulttypeno == 3)
        //        //        item.PatientExportFile = CommonHelper.URLShorten(tblReportMaster.ExportURL + filename, _config.GetValue<string>(ConfigKeys.FireBaseAPIkey));
        //        //    else
        //        //        item.PatientExportFile = tblReportMaster.ExportURL + filename;

        //        //    item.PatientExportFolderPath = objitem.ExportPath;
        //        //    //attached outsourced documents
        //        //    string ackpath = _config.GetValue<string>(ConfigKeys.ResultAckUpload);
        //        //    int oldtestno = 0; int newtestno = 0;
        //        //    int oldtestno1 = 0; int newtestno1 = 0;
        //        //    int isbillmerged = 0, isDeltaMerged = 0;
        //        //    string initialindividualdeltapath = string.Empty;
        //        //    initialindividualdeltapath = item.PatientExportFolderPath;
        //        //    foreach (DataRow row in datable.Rows)
        //        //    {
        //        //        if (datable.Columns.Contains("BillIncluded") && isbillmerged == 0 && (PatientItem.patientreportwithbill != null && PatientItem.patientreportwithbill > 0))
        //        //        {
        //        //            int BillIncluded = row["BillIncluded"] != null && row["BillIncluded"] != "" ? Convert.ToInt32(row["BillIncluded"]) : 0;
        //        //            if (BillIncluded > 0)
        //        //            {
        //        //                foreach (DataColumn column in datable.Columns)
        //        //                {
        //        //                    string billfullpath = "";
        //        //                    ReportOutput obj = new ReportOutput();
        //        //                    ReportRequestDTO reqstbill = new ReportRequestDTO();
        //        //                    FrontOfficeRepository _IFrontOfficeRepository = new FrontOfficeRepository(_config);

        //        //                    reqstbill.VenueNo = PatientItem.venueno;
        //        //                    reqstbill.VenueBranchNo = PatientItem.venuebranchno;
        //        //                    reqstbill.visitNo = PatientItem.patientvisitno != null && PatientItem.patientvisitno != "" ? Convert.ToInt32(PatientItem.patientvisitno) : 0;
        //        //                    reqstbill.userNo = PatientItem.userno;
        //        //                    reqstbill.print = "PATIENTBILL";//"PATIENTBILLSFORMAT"
        //        //                    obj = _IFrontOfficeRepository.PrintBill(reqstbill);
        //        //                    billfullpath = obj != null ? obj.PatientExportFolderPath : "";

        //        //                    if (billfullpath != null && billfullpath != "")
        //        //                    {
        //        //                        using (PdfDocument one = PdfReader.Open(objitem.ExportPath, PdfDocumentOpenMode.Import))
        //        //                        using (PdfDocument two = PdfReader.Open(billfullpath, PdfDocumentOpenMode.Import))
        //        //                        using (PdfDocument outPdf = new PdfDocument())
        //        //                        {
        //        //                            CopyPages(one, outPdf);
        //        //                            CopyPages(two, outPdf);

        //        //                            outPdf.Save(objitem.ExportPath);
        //        //                        }
        //        //                    }
        //        //                }
        //        //            }
        //        //            isbillmerged = 1;
        //        //        }
        //        //if (datable.Columns.Contains("DeltaReportIncluded") && isDeltaMerged == 0)
        //        //{
        //        //    int DeltaReportIncluded = row["DeltaReportIncluded"] != null && row["DeltaReportIncluded"] != "" ? Convert.ToInt32(row["DeltaReportIncluded"]) : 0;
        //        //    if (DeltaReportIncluded > 0)
        //        //    {
        //        //        foreach (DataColumn column in datable.Columns)
        //        //        {
        //        //            if (isDeltaMerged == 0)
        //        //            {
        //        //                string billfullpath = ""; string Key1 = "DELTAREPORT";
        //        //                FrontOfficeRepository _IFrontOfficeRepository = new FrontOfficeRepository(_config);
        //        //                Dictionary<string, string> objdictionary1 = new Dictionary<string, string>();

        //        //                objdictionary1.Add("PageCode", PatientItem.pagecode);
        //        //                objdictionary1.Add("PatientVisitNo", PatientItem.patientvisitno);
        //        //                objdictionary1.Add("OrderListNos", PatientItem.orderlistnos);
        //        //                objdictionary1.Add("IsLogo", PatientItem.isheaderfooter.ToString());
        //        //                objdictionary1.Add("IsNABLlogo", PatientItem.isNABLlogo.ToString());

        //        //                objdictionary1.Add("UserNo", PatientItem.userno.ToString());
        //        //                objdictionary1.Add("VenueNo", PatientItem.venueno.ToString());
        //        //                objdictionary1.Add("VenueBranchNo", PatientItem.venuebranchno.ToString());
        //        //                ReportContext objReportContext1 = new ReportContext(DefaultConnection);
        //        //                TblReportMaster tblReportMaster1 = new TblReportMaster();
        //        //                using (var context = new LIMSContext(DefaultConnection))
        //        //                {
        //        //                    tblReportMaster1 = context.TblReportMaster.Where(x => x.ReportKey == Key1 && x.VenueNo == PatientItem.venueno
        //        //                    && x.VenueBranchNo == PatientItem.venuebranchno).FirstOrDefault();
        //        //                    if (!Directory.Exists(tblReportMaster1.ExportPath))
        //        //                    {
        //        //                        Directory.CreateDirectory(tblReportMaster1.ExportPath);
        //        //                    }
        //        //                }
        //        //                string PatientName1 = string.Concat(PatientItem.patientvisitno.Where(c => !char.IsWhiteSpace(c)));
        //        //                string iFile1 = PatientName1 + "_DeltaReport_" + Guid.NewGuid().ToString("N").Substring(0, 4) + ".pdf";
        //        //                //   objdictionary1.Add("QRCodeURL", tblReportMaster1.ExportURL + iFile1);

        //        //                DataTable datable1 = objReportContext1.getdatatable(objdictionary1, tblReportMaster1.ProcedureName);
        //        //                ReportParamDTO objitem1 = new ReportParamDTO();
        //        //                objitem1.datatable = CommonExtension.DatableToDicionary(datable1);
        //        //                objitem1.paramerter = objdictionary1;
        //        //                objitem1.ReportPath = tblReportMaster1.ReportPath;
        //        //                objitem1.ExportPath = tblReportMaster1.ExportPath + iFile1;
        //        //                objitem1.ExportFormat = FileFormat.PDF;
        //        //                string ReportParam1 = JsonConvert.SerializeObject(objitem1);
        //        //                string filename1 = ExportReportService.ExportPrint(ReportParam1, _config.GetValue<string>(ConfigKeys.ReportServiceURL));
        //        //                billfullpath = tblReportMaster1.ExportPath + filename1;
        //        //                if (billfullpath != null && billfullpath != "")
        //        //                {
        //        //                    using (PdfDocument one = PdfReader.Open(objitem.ExportPath, PdfDocumentOpenMode.Import))
        //        //                    using (PdfDocument two = PdfReader.Open(billfullpath, PdfDocumentOpenMode.Import))
        //        //                    using (PdfDocument outPdf = new PdfDocument())
        //        //                    {
        //        //                        CopyPages(one, outPdf);
        //        //                        CopyPages(two, outPdf);

        //        //                        outPdf.Save(objitem.ExportPath);
        //        //                    }
        //        //                }
        //        //            }
        //        //            isDeltaMerged = 1;
        //        //        }
        //        //    }
        //        //}
        //        //if (datable.Columns.Contains("TestNo"))
        //        //{
        //        //    foreach (DataColumn column in datable.Columns)
        //        //    {
        //        //        if (row["VisitID"] != null && column.ColumnName == "VisitID")
        //        //        {
        //        //            newtestno = row["TestNo"] != null && row["TestNo"] != "" ? Convert.ToInt32(row["TestNo"]) : 0;
        //        //            if (newtestno != oldtestno)
        //        //            { //if test have subtest then lnly once need to add outsource report
        //        //                ackpath = _config.GetValue<string>(ConfigKeys.ResultAckUpload);
        //        //                ackpath = ackpath + "//" + PatientItem?.venueno + "//" + PatientItem?.venuebranchno + "//" + row["VisitID"] + "//" + row["TestNo"];
        //        //                if (Directory.Exists(ackpath))
        //        //                {
        //        //                    string[] files = Directory.GetFiles(ackpath);
        //        //                    if (files != null && files.Length > 0)
        //        //                    {
        //        //                        string resultname = Path.GetFileName(files[0]);
        //        //                        string ackfullpath = files[0];// ackpath + "//" + resultname;
        //        //                        string ackfilename = Path.GetFileNameWithoutExtension(ackfullpath);
        //        //                        string extension = Path.GetExtension(files[0]);
        //        //                        if (extension != null && (extension.ToLower() == ".pdf" || extension.ToLower() == "pdf"))
        //        //                        {
        //        //                            var ackfilenames = ackfilename.Split("$$");
        //        //                            if (ackfilenames != null && ackfilenames.Length > 0 &&
        //        //                                ackfilenames[ackfilenames.Length - 1] != null && ackfilenames[ackfilenames.Length - 1].ToLower() == "yes")
        //        //                            {
        //        //                                using (PdfDocument one = PdfReader.Open(objitem.ExportPath, PdfDocumentOpenMode.Import))
        //        //                                using (PdfDocument two = PdfReader.Open(ackfullpath, PdfDocumentOpenMode.Import))
        //        //                                using (PdfDocument outPdf = new PdfDocument())
        //        //                                {
        //        //                                    CopyPages(one, outPdf);
        //        //                                    CopyPages(two, outPdf);

        //        //                                    outPdf.Save(objitem.ExportPath);
        //        //                                }
        //        //                            }
        //        //                        }
        //        //                    }
        //        //                }
        //        //                //tempalte report - add images in entry screen - final image added to the report
        //        //                if (Key == "TEMPPATIENTREPORT" || Key == "MTEMPPATIENTREPORT" || Key == "TEMPPATIENTREPORTWATERMARK")
        //        //                {
        //        //                    var webRootPath = Directory.GetCurrentDirectory();
        //        //                    PatientReportImagesDTO reportImagesDTO = new PatientReportImagesDTO();
        //        //                    reportImagesDTO.VenueNo = PatientItem.venueno.ToString();
        //        //                    reportImagesDTO.VenueBranchNo = PatientItem.venuebranchno.ToString();
        //        //                    reportImagesDTO.PatientNo = row["PatientNo"] != null && row["PatientNo"] != "" ? row["PatientNo"].ToString() : "0";
        //        //                    reportImagesDTO.PatientVisitNo = row["PatientVisitNo"] != null && row["PatientVisitNo"] != "" ? row["PatientVisitNo"].ToString() : "0";
        //        //                    reportImagesDTO.TestNo = row["TestNo"] != null && row["TestNo"] != "" ? row["TestNo"].ToString() : "0";
        //        //                    var folderName = "images";
        //        //                    string filefullimgpath = string.Empty;
        //        //                    string filefullpdfpath = string.Empty;
        //        //                    var newPath = Path.Combine(
        //        //                         webRootPath,
        //        //                         folderName,
        //        //                         reportImagesDTO.VenueNo,
        //        //                         reportImagesDTO.VenueBranchNo,
        //        //                         reportImagesDTO.PatientNo,
        //        //                         reportImagesDTO.PatientVisitNo,
        //        //                         reportImagesDTO.TestNo
        //        //                         );
        //        //                    filefullimgpath = newPath + "\\" + "FinalImage.png";
        //        //                    filefullpdfpath = newPath + "\\" + "FinalImage.pdf";
        //        //                    int width = 600;
        //        //                    //png to pdf
        //        //                    if (File.Exists(filefullimgpath))
        //        //                    {
        //        //                        using (var document = new PdfDocument())
        //        //                        {
        //        //                            PdfPage page = document.AddPage();
        //        //                            using (XImage img = XImage.FromFile(filefullimgpath))
        //        //                            {
        //        //                                // Calculate new height to keep image ratio
        //        //                                var height = (int)(((double)width / (double)img.PixelWidth) * img.PixelHeight);

        //        //                                // Change PDF Page size to match image
        //        //                                page.Width = width;
        //        //                                page.Height = height;

        //        //                                XGraphics gfx = XGraphics.FromPdfPage(page);
        //        //                                gfx.DrawImage(img, 0, 0, width, height);
        //        //                            }
        //        //                            document.Save(filefullpdfpath);
        //        //                        }
        //        //                        using (PdfDocument one = PdfReader.Open(objitem.ExportPath, PdfDocumentOpenMode.Import))
        //        //                        using (PdfDocument two = PdfReader.Open(filefullpdfpath, PdfDocumentOpenMode.Import))
        //        //                        using (PdfDocument outPdf = new PdfDocument())
        //        //                        {
        //        //                            CopyPages(one, outPdf);
        //        //                            CopyPages(two, outPdf);

        //        //                            outPdf.Save(objitem.ExportPath);
        //        //                        }
        //        //                    }
        //        //                }
        //        //                //
        //        //            }
        //        //            oldtestno = newtestno;
        //        //        }
        //        //    }
        //        //}
        //        //if (datable.Columns.Contains("ActualBarcode") && datable.Columns.Contains("UploadedFile"))
        //        //{
        //        //    foreach (DataColumn column in datable.Columns)
        //        //    {
        //        //        if (row["VisitID"] != null && column.ColumnName == "VisitID")
        //        //        {
        //        //            newtestno1 = row["TestNo"] != null && row["TestNo"] != "" ? Convert.ToInt32(row["TestNo"]) : 0;
        //        //            if (newtestno1 != oldtestno1)
        //        //            { //if test have subtest then lnly once need to add outsource report
        //        //                ackpath = _config.GetValue<string>(ConfigKeys.ResultEntryUploadPath);
        //        //                //ackpath = ackpath + "//ResultUpload" + "//" + PatientItem?.venueno + "//" + PatientItem?.venuebranchno + "//" + row["ActualBarcode"] + "//" + row["PatientVisitNo"] + "//" + row["TestNo"];
        //        //                ackpath = ackpath + "//ResultUpload" + "//" + PatientItem?.venueno + "//" + row["ActualBarcode"] + "//" + row["PatientVisitNo"] + "//" + row["TestNo"];
        //        //                if (Directory.Exists(ackpath))
        //        //                {
        //        //                    string[] files = Directory.GetFiles(ackpath);
        //        //                    if (files != null && files.Length > 0)
        //        //                    {
        //        //                        //check multi upload for a single test
        //        //                        MasterRepository _IMasterRepository = new MasterRepository(_config);
        //        //                        ConfigurationDTO objConfigurationDTO = new ConfigurationDTO();
        //        //                        string resultStatusDDAvailConfig = "IsRsultMultiUpload";
        //        //                        objConfigurationDTO = _IMasterRepository.GetSingleConfiguration(PatientItem.venueno, PatientItem.venuebranchno, resultStatusDDAvailConfig);
        //        //                        if (objConfigurationDTO != null && objConfigurationDTO.ConfigValue == 1 && files.Length > 1)
        //        //                        {
        //        //                            for (int z = 0; z < files.Length; z++)
        //        //                            {
        //        //                                string resultname = Path.GetFileName(files[z]);
        //        //                                string ackfullpath = files[z];// ackpath + "//" + resultname;
        //        //                                string ackfilename = Path.GetFileNameWithoutExtension(ackfullpath);
        //        //                                string extension = Path.GetExtension(files[z]);
        //        //                                if (extension != null && (extension.ToLower() == ".pdf" || extension.ToLower() == "pdf"))
        //        //                                {
        //        //                                    using (PdfDocument one = PdfReader.Open(objitem.ExportPath, PdfDocumentOpenMode.Import))
        //        //                                    using (PdfDocument two = PdfReader.Open(ackfullpath, PdfDocumentOpenMode.Import))
        //        //                                    using (PdfDocument outPdf = new PdfDocument())
        //        //                                    {
        //        //                                        CopyPages(one, outPdf);
        //        //                                        CopyPages(two, outPdf);

        //        //                                        outPdf.Save(objitem.ExportPath);
        //        //                                    }
        //        //                                }
        //        //                            }
        //        //                        }
        //        //                        else
        //        //                        {
        //        //                            string resultname = Path.GetFileName(files[0]);
        //        //                            string ackfullpath = files[0];// ackpath + "//" + resultname;
        //        //                            string ackfilename = Path.GetFileNameWithoutExtension(ackfullpath);
        //        //                            string extension = Path.GetExtension(files[0]);
        //        //                            if (extension != null && (extension.ToLower() == ".pdf" || extension.ToLower() == "pdf"))
        //        //                            {
        //        //                                using (PdfDocument one = PdfReader.Open(objitem.ExportPath, PdfDocumentOpenMode.Import))
        //        //                                using (PdfDocument two = PdfReader.Open(ackfullpath, PdfDocumentOpenMode.Import))
        //        //                                using (PdfDocument outPdf = new PdfDocument())
        //        //                                {
        //        //                                    CopyPages(one, outPdf);
        //        //                                    CopyPages(two, outPdf);

        //        //                                    outPdf.Save(objitem.ExportPath);
        //        //                                }
        //        //                            }
        //        //                        }
        //        //                    }
        //        //                }
        //        //            }
        //        //            oldtestno1 = newtestno1;
        //        //        }
        //        //    }
        //        //}
        //        //int isdeltaoptionselect = deltaselectorderlst != null && deltaselectorderlst.Count() > 0 &&
        //        //       deltaselectorderlst.Contains(row["orderlistno"].ToString()) ? 1 : 0;
        //        //if (datable.Columns.Contains("IndividualDeltaPath") && datable.Columns.Contains("individualDeltaURL") && isdeltaoptionselect == 1)
        //        //{
        //        //    if (row["IndividualDeltaPath"] != null && row["IndividualDeltaPath"] != "")
        //        //    {
        //        //        string temppath = row["IndividualDeltaPath"] != null && row["IndividualDeltaPath"] != "" ? row["IndividualDeltaPath"].ToString() : "";
        //        //        using (PdfDocument one = PdfReader.Open(item.PatientExportFolderPath, PdfDocumentOpenMode.Import))
        //        //        //s = s + 1;
        //        //        using (PdfDocument two = PdfReader.Open(temppath, PdfDocumentOpenMode.Import))
        //        //        using (PdfDocument outPdf = new PdfDocument())
        //        //        {
        //        //            CopyPages(one, outPdf);
        //        //            CopyPages(two, outPdf);

        //        //            outPdf.Save(initialindividualdeltapath);
        //        //        }
        //        //    }
        //        //}
        //        //}
        //        //result.Add(item);
        //        //}
        //        //if (result != null && result.Count > 1)
        //        //{
        //        //    MasterRepository _IMasterRepository = new MasterRepository(_config);
        //        //    ConfigurationDTO objConfigurationDTO = new ConfigurationDTO();
        //        //    string VisitwiseReportConfig = "IsVisitwiseReport";
        //        //    objConfigurationDTO = _IMasterRepository.GetSingleConfiguration(PatientItem.venueno, PatientItem.venuebranchno, VisitwiseReportConfig);
        //        //    if (objConfigurationDTO != null && objConfigurationDTO.ConfigValue == 1)
        //        //    {
        //        //        string initalfilefullpath = string.Empty;
        //        //        int f = 0;
        //        //        for (int s = 0; s < result.Count; s++)
        //        //        {
        //        //            if (result[s].PatientExportFolderPath != null && result[s].PatientExportFolderPath != "")
        //        //            {
        //        //                if (s == 0)
        //        //                {
        //        //                    initalfilefullpath = result[s].PatientExportFolderPath;
        //        //                }
        //        //                f = s == 0 ? 1 : s + 1;
        //        //                using (PdfDocument one = PdfReader.Open(result[0].PatientExportFolderPath, PdfDocumentOpenMode.Import))
        //        //                //s = s + 1;
        //        //                using (PdfDocument two = PdfReader.Open(result[f].PatientExportFolderPath, PdfDocumentOpenMode.Import))
        //        //                using (PdfDocument outPdf = new PdfDocument())
        //        //                {
        //        //                    CopyPages(one, outPdf);
        //        //                    CopyPages(two, outPdf);

        //        //                    outPdf.Save(initalfilefullpath);
        //        //                }
        //        //                if (f == result.Count - 1)
        //        //                {
        //        //                    s = f;
        //        //                    result.RemoveRange(1, s);
        //        //                }
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("ScheduleReport - ", ex.Message);
        //        Console.ReadLine();
        //        //MyDEVException.Error(ex, "PatientReportRepository.PrintPatientReport/patientvisitno-" + PatientItem.patientvisitno, ExceptionPriority.High, ApplicationType.REPOSITORY, PatientItem.venueno, PatientItem.venuebranchno, PatientItem.userno);
        //    }

        //    Console.WriteLine("PrintPatientReport - End");
        //    return result;
        //}
        //void CopyPages(PdfDocument from, PdfDocument to)
        //{
        //    for (int i = 0; i < from.PageCount; i++)
        //    {
        //        to.AddPage(from.Pages[i]);
        //    }
        //}

        public class ReportOutput
        {
            public string PatientExportFile { get; set; }
            public string PatientExportFolderPath { get; set; }
            public string ExportURL { get; set; }

        }

        public string PrintPatientReport(int patientVisitNo, int venueno, int venuebranchno, int userno, string pagecode, int? Resulttypeno, string PatientName, bool isheaderfooter, string exportPath = null)
        {
            Console.WriteLine("PrintPatientReport - Start");

            string DefaultConnection = string.Empty;
            string result = string.Empty;
            //List<ReportOutput> result = new List<ReportOutput>();
            try
            {
              
               string configval = ConfigurationManager.AppSettings["ResultTypeOrder"].ToString();
                string formatedResulttypes = string.Empty;
                
                var Key = "";
                //for (int i = 0; i < Resulttypeno.Length; i++)
                //{
                Key = "";
                if (Resulttypeno == 1) Key = "PATIENTREPORT";
                else if (Resulttypeno == 2) Key = "MBPATIENTREPORT";
                else if (Resulttypeno == 3) Key = "TEMPPATIENTREPORT";
                else if (Resulttypeno == 4) Key = "MTEMPPATIENTREPORT";



                //ReportOutput item = new ReportOutput();
                Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                objdictionary.Add("PageCode", pagecode);
                objdictionary.Add("PatientVisitNo", patientVisitNo.ToString());
                objdictionary.Add("OrderListNos", "");
                objdictionary.Add("IsLogo", isheaderfooter.ToString());
                objdictionary.Add("IsNABLlogo", "");
                objdictionary.Add("UserNo", "0");
                objdictionary.Add("VenueNo", venueno.ToString());
                objdictionary.Add("VenueBranchNo", venuebranchno.ToString());


                List<tbl_ReportMaster> reportlst = GetReportMaster();
                var tblReportMaster = reportlst.Where(x => x.ReportKey == Key && x.VenueNo == venueno
                 && x.VenueBranchNo == venuebranchno).FirstOrDefault();


                tblReportMaster.ExportPath = exportPath ?? tblReportMaster.ExportPath;

                Console.WriteLine("PrintPatientReport - Report Path " + tblReportMaster.ExportPath);

                if (!Directory.Exists(tblReportMaster.ExportPath))
                {
                    Console.WriteLine("PrintPatientReport - Directory Path1 " + tblReportMaster.ExportPath);
                    Directory.CreateDirectory(tblReportMaster.ExportPath);
                }


                string patientvisitid = PatientName.ToString();

                string resultStatusDDAvailConfig4 = ConfigurationManager.AppSettings["GUIDNoNeedReportName"].ToString();

                string iFile = string.Empty;
                if (resultStatusDDAvailConfig4 != null && resultStatusDDAvailConfig4 == "1")
                {
                    iFile = patientvisitid + ".pdf";
                }
                else
                {
                    iFile = patientvisitid + "_" + Guid.NewGuid().ToString("N").Substring(0, 4) + ".pdf";
                }
                objdictionary.Add("QRCodeURL", tblReportMaster.ExportURL + iFile);
                objdictionary.Add("IsProvisional", ConfigurationManager.AppSettings["IsProvisional"].ToString());

                if (Key == "MBPATIENTREPORT" || Key == "MTEMPPATIENTREPORT")
                {
                   
                    string resultStatusDDAvailConfig = ConfigurationManager.AppSettings["IsRsultStatusDDAvail"].ToString();
                    if (resultStatusDDAvailConfig != null && resultStatusDDAvailConfig == "1")
                    {
                        objdictionary.Add("ReportStatus", ConfigurationManager.AppSettings["ReportStatus"].ToString());
                    }
                }

                string LiveConnection = ConfigurationManager.ConnectionStrings["LiveConnection"].ConnectionString;
                DataContext objReportContext = new DataContext(LiveConnection);
                DataTable datable = objReportContext.getdatatable(objdictionary, tblReportMaster.ProcedureName);

                if (Key == "TEMPPATIENTREPORT" || Key == "MTEMPPATIENTREPORT" || Key == "TEMPPATIENTREPORTWATERMARK")
                {
                    Console.WriteLine("PrintPatientReport - Key " + Key);

                    if (Key == "MTEMPPATIENTREPORT")
                    {
                        //COMMENTS ADDED FOR MULTITEMP REPORT                           

                        if (datable?.Rows[0]["IsComments"] != null && datable?.Rows[0]["IsComments"].ToString() == "True")
                        {
                            string commentspath = ConfigurationManager.AppSettings["MasterFilePath"].ToString();
                            commentspath = commentspath + venueno.ToString() + "//" + "T" + "//" + "Comments" + "//" + datable?.Rows[0]["ServiceNo"]?.ToString() + ".ym";

                            if (File.Exists(commentspath))
                            {
                                string content = File.ReadAllText(commentspath);
                                for (int z = 0; z < datable.Rows.Count; z++)
                                {
                                    if (datable.Rows[z]["IsComments"] != null && datable?.Rows[z]["IsComments"].ToString() == "True")
                                    {
                                        datable.Rows[z]["comments"] = content;
                                    }
                                }
                            }
                        }
                    }

                    string path = ConfigurationManager.AppSettings["TransTemplateFilePath"].ToString();
                    string deveditorconfigvalue = ConfigurationManager.AppSettings["DevExpressEditorConfigValue"].ToString();

                    int datablecount = datable.Rows.Count;//if we have 2 tempalte test inside the package, then that 2 test should shown 
                    int startcount = 1;
                    datablecount = Resulttypeno == 3 ? datablecount : 1;
                    for (int s = 0; s < datablecount; s++)
                    {
                        path = ConfigurationManager.AppSettings["TransTemplateFilePath"].ToString();
                        path = path + venueno.ToString() + "/" + datable?.Rows[s]["orderListNo"]?.ToString() + "/" + datable?.Rows[s]["serviceNo"]?.ToString() + ".rtf";
                        if (!File.Exists(path))
                        {
                            path = ConfigurationManager.AppSettings["TransTemplateFilePath"].ToString();
                            path = path + venueno.ToString() + "/" + datable?.Rows[s]["orderListNo"]?.ToString() + "/" + datable?.Rows[s]["serviceNo"]?.ToString() + ".ym";
                        }

                        if (File.Exists(path))
                        {
                            string content = File.ReadAllText(path);
                            datable.Rows[s]["result"] = content;
                        }
                        else
                        {
                            if (Resulttypeno == 4 && startcount == 1)
                            {
                                int oldorderlistno = 0;
                                int neworderlistno = 0;
                                int oldserviceno = 0;
                                int newserviceno = 0;
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
                                                    if (oldorderlistno != neworderlistno && neworderlistno > 0)
                                                    {
                                                        path = path.Replace(neworderlistno.ToString(), oldorderlistno.ToString());
                                                        path = path.Replace(newserviceno.ToString(), oldserviceno.ToString());
                                                    }
                                                    neworderlistno = row["orderlistno"] != null ? Convert.ToInt32(row["orderlistno"]) : 0;
                                                    newserviceno = row["serviceNo"] != null ? Convert.ToInt32(row["serviceNo"]) : 0;

                                                    string fileformat = ConfigurationManager.AppSettings["MultiTemplateFormat"].ToString();
                                                    string overallpath = path + "/" + row["SubTestNo"].ToString() + fileformat;
                                                    //check result type based report data avail 
                                                    int ismultiflag = datable.Columns.Contains("IsMultiEditor") && row["IsMultiEditor"] != null && Convert.ToInt32(row["IsMultiEditor"].ToString()) > 0 ? 1 : 0;
                                                    string oldreporttypepath = string.Empty;
                                                  
                                                }
                                            }
                                        }
                                        oldorderlistno = row["orderlistno"] != null ? Convert.ToInt32(row["orderlistno"]) : 0;
                                        oldserviceno = row["serviceNo"] != null ? Convert.ToInt32(row["serviceNo"]) : 0;
                                    }
                                }
                            }
                            else
                            {
                                datable.Rows[s]["result"] = "";
                            }
                        }
                        startcount = startcount++;
                    }
                }
                else if (Key == "PATIENTREPORT")
                {
                    Console.WriteLine("PrintPatientReport - Key " + Key);

                    int sno = 0;
                    string grpnotes = "";
                    int tno = 0;
                    string tstnotes = "";
                    Console.WriteLine("PrintPatientReport - Datatable Rows " + datable.Rows.Count);
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
                                    string path = ConfigurationManager.AppSettings["TransFilePath"].ToString();
                                    path = path + venueno.ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["OrderListNo"]).ToString() + ".ym";
                                    if (File.Exists(path))
                                    {
                                        dr["GroupInterNotes"] = File.ReadAllText(path);
                                        grpnotes = dr["GroupInterNotes"].ToString();
                                    }
                                }
                                else if (Convert.ToInt32(dr["GroupInter"]) == 1)
                                {
                                    string path = ConfigurationManager.AppSettings["MasterFilePath"].ToString();
                                    path = path + venueno.ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["ServiceNo"]).ToString() + ".ym";
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
                                    string path = ConfigurationManager.AppSettings["TransFilePath"].ToString();
                                    path = path + venueno.ToString() + "/T/InterNotes/" + Convert.ToInt32(dr["OrderDetailsNo"]).ToString() + ".ym";
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
                                    string path = ConfigurationManager.AppSettings["MasterFilePath"].ToString();
                                    path = path + venueno.ToString() + "/T/InterNotes/" + FPath;
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
                        if (datable.Columns.Contains("GraphImage1"))
                        {
                            //machine graph attached to the final report
                            string GraphURL = dr["GraphURL"].ToString();
                            string actualmachineimagepath = ConfigurationManager.AppSettings["MachineImagePath"].ToString();
                            string machineimagepath = ConfigurationManager.AppSettings["MachineImagePath"].ToString();
                            machineimagepath = machineimagepath + "//" + venueno + "//" + dr["ProcessingBranchNo"] + "//" + dr["BarcodeNoNew"].ToString();

                            if (Directory.Exists(machineimagepath))
                            {
                                string[] files = Directory.GetFiles(machineimagepath);
                                if (files != null && files.Length > 0)
                                {
                                    for (int g = 0; g < files.Length; g++)

                                    {
                                        string machinefullpath = files[g];
                                        string serviceno = machinefullpath.Replace(machineimagepath, "").Replace("//", "").Replace("\\", "").Replace(".png", "");
                                        var servicenolst = serviceno.Split('T').Length > 1 ? serviceno.Split('T') : serviceno.Split('S');

                                        string servcno = servicenolst.Length > 1 ? servicenolst[1].ToString() : "";
                                        if (servcno == dr["TestNo"].ToString() || (servcno == dr["SubTestNo"].ToString()))
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
                else if (Key == "PATIENTREPORTWATERMARK")
                {
                  
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
                            foreach (DataRow dtrow in datable?.Rows)
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
                tblReportMaster = reportlst.Where(x => x.ReportKey == Key && x.VenueNo == venueno
                 && x.VenueBranchNo == venuebranchno).FirstOrDefault();
                                    
                ReportParamDTO objitem = new ReportParamDTO();
                objitem.datatable = CommonExtension.DatableToDicionary(datable);
                objitem.paramerter = objdictionary;
                objitem.ReportPath = tblReportMaster.ReportPath;
                objitem.ExportPath = tblReportMaster.ExportPath + iFile;
                objitem.ExportFormat = "PDF";
                Console.WriteLine("ScheduleReport - ReportPath - " + tblReportMaster.ReportPath);
                Console.WriteLine("ScheduleReport - ExportPath - " + objitem.ExportPath);


                string ReportParam = JsonConvert.SerializeObject(objitem);
                string filename = ExportPrint(ReportParam);
                
                result = tblReportMaster.ExportURL + filename + "|" + objitem.ExportPath;
                Console.WriteLine("ScheduleReport - result " + result);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ScheduleReport - ", ex.Message);
                Console.ReadLine();
            }

            Console.WriteLine("PrintPatientReport - End");
            return result;
        }
    }
}
