using Service.Win.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Data;
using System.Configuration;
using Service.Win.Common.Model;

namespace Service.Win.Repository
{
    public class BulkReportDownload
    {
        public List<ReportDownloadListDTO> FetchPatientRecord()
        {
            List<ReportDownloadListDTO> result = new List<ReportDownloadListDTO>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    result = context.pro_get_scheduled_patientreport().ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
            return result;
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

                string LiveConnection = System.Configuration.ConfigurationManager.ConnectionStrings["YMEntities"].ConnectionString;
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
                string ServiceMethod = System.Configuration.ConfigurationManager.AppSettings["ReportServiceURL"].ToString() + "Report/ExportPrint";
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
    }
}
