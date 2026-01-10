using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DEV.Web.External
{
    public partial class Report : System.Web.UI.Page
    {
        public string Encode = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["qn"] != null)
            {
                Encode = Request.QueryString["qn"].ToString();
                DisplayReports(Encode);
            }
        }
        public void DisplayReports(string en)
        {
            string filename = string.Empty;
            try
            {
                string DecryptData = Dev.Win.Common.CommonSecurity.base64Decode(en);
                string[] parameter = DecryptData.Split('|');
                Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                objdictionary.Add("PageCode", parameter[0]);
                objdictionary.Add("PatientVisitNo", parameter[1]);
                objdictionary.Add("OrderListNos", parameter[2]);
                objdictionary.Add("IsLogo", parameter[3]);
                objdictionary.Add("IsNABLlogo", parameter[4]);
                objdictionary.Add("UserNo", parameter[5]);
                objdictionary.Add("VenueNo", parameter[6]);
                objdictionary.Add("VenueBranchNo", parameter[7]);

                string ReportKey = string.Empty;
                if (parameter[8] == "1")
                {
                    ReportKey = "PATIENTREPORT";
                }
                if (parameter[8] == "2")
                {
                    ReportKey = "MBPATIENTREPORT";
                }
                if (parameter[8] == "3")
                {
                    ReportKey = "TEMPPATIENTREPORT";
                }
                else if (parameter[8] == "4")
                {
                    ReportKey = "MTEMPPATIENTREPORT";
                }
                //report status based output/premilinary/final
                if (ReportKey == "MBPATIENTREPORT" || ReportKey == "MTEMPPATIENTREPORT")
                {
                    if (parameter.Length >= 10 && parameter[9] != null && parameter[9] != "")
                    {
                        objdictionary.Add("ReportStatus", parameter[9]);
                    }
                }

                ReportContext objReportContext = new ReportContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                var reportcontext = objReportContext.getreportcontext(Convert.ToInt32(parameter[6]), Convert.ToInt32(parameter[7]), ReportKey);
                using (DataTable datable = objReportContext.getdatatable(objdictionary, reportcontext.ProcedureName))
                {
                    if (ReportKey == "TEMPPATIENTREPORT" || ReportKey == "MTEMPPATIENTREPORT")
                    {
                        if (ReportKey == "MTEMPPATIENTREPORT")
                        {
                            //COMMENTS ADDED FOR MULTITEMP REPORT
                            if (datable?.Rows[0]["IsComments"] != null && datable?.Rows[0]["IsComments"].ToString() == "True")
                            {
                                string commentspath = "C:\\inetpub\\lims.bbtlabs.com\\REPORTSERVICE\\MasterFiles\\";
                                // _config.GetValue<string>(ConfigKeys.MasterFilePath);
                                commentspath = commentspath + parameter[6].ToString() + "//" + "T" + "//" + "Comments" + "//" + datable?.Rows[0]["ServiceNo"]?.ToString() + ".ym";
                                if (File.Exists(commentspath))
                                {
                                    string content = File.ReadAllText(commentspath);
                                    for (int z = 0; z < datable.Rows.Count; z++)
                                    {
                                        datable.Rows[z]["comments"] = content;
                                    }
                                }
                            }
                        }
                        // string path = _config.GetValue<string>(ConfigKeys.TransTemplateFilePath);
                        string path = "C:\\inetpub\\lims.bbtlabs.com\\REPORTSERVICE\\TransTemplateFiles\\";
                        path = path + parameter[6].ToString() + "/" + datable.Rows[0]["orderListNo"].ToString() + "/" + datable.Rows[0]["serviceNo"].ToString() + ".ym";

                        if (File.Exists(path))
                        {
                            string content = File.ReadAllText(path);
                            datable.Rows[0]["result"] = content;
                        }
                        else
                        {
                            if (parameter[8] == "4")
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
                                                    string fileformat = ".ym";
                                                    string overallpath = path + "/" + row["SubTestNo"].ToString() + fileformat;
                                                    //check result type based report data avail 
                                                    int ismultiflag = datable.Columns.Contains("IsMultiEditor") && row["IsMultiEditor"] != null && Convert.ToInt32(row["IsMultiEditor"].ToString()) > 0 ? 1 : 0;
                                                    string oldreporttypepath = string.Empty;
                                                    int reprtstatus = parameter.Length >= 10 && parameter[9] != null && parameter[9] != "" ? Convert.ToInt32(parameter[9]) : 0;
                                                    if (reprtstatus > 0 && ismultiflag > 0)
                                                    {
                                                        oldreporttypepath = path + "/" + reprtstatus.ToString() + "/" + row["SubTestNo"].ToString() + fileformat;
                                                    }
                                                    if (!String.IsNullOrEmpty(oldreporttypepath) && File.Exists(oldreporttypepath))
                                                    {
                                                        row["result"] = File.ReadAllText(oldreporttypepath);
                                                    }
                                                    //
                                                    else if (File.Exists(overallpath))
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
                    else if (ReportKey == "PATIENTREPORT")
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
                                        string path = "C:\\inetpub\\lims.bbtlabs.com\\REPORTSERVICE\\TransFiles\\";
                                        //   string path = _config.GetValue<string>(ConfigKeys.TransFilePath);
                                        path = path + parameter[6].ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["OrderListNo"]).ToString() + ".ym";
                                        if (File.Exists(path))
                                        {
                                            dr["GroupInterNotes"] = File.ReadAllText(path);
                                            grpnotes = dr["GroupInterNotes"].ToString();
                                        }
                                    }
                                    else if (Convert.ToInt32(dr["GroupInter"]) == 1)
                                    {
                                        string path = "C:\\inetpub\\lims.bbtlabs.com\\REPORTSERVICE\\MasterFiles\\";
                                        //string path = _config.GetValue<string>(ConfigKeys.MasterFilePath);
                                        path = path + parameter[6].ToString() + "/G/InterNotes/" + Convert.ToInt32(dr["ServiceNo"]).ToString() + ".ym";
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
                                        string path = "C:\\inetpub\\lims.bbtlabs.com\\REPORTSERVICE\\TransFiles\\";
                                        // string path = _config.GetValue<string>(ConfigKeys.TransFilePath);
                                        path = path + parameter[6].ToString() + "/T/InterNotes/" + Convert.ToInt32(dr["OrderDetailsNo"]).ToString() + ".ym";
                                        if (File.Exists(path))
                                        {
                                            dr["TestInterNotes"] = File.ReadAllText(path);
                                            tstnotes = dr["TestInterNotes"].ToString();
                                        }
                                    }
                                    else if (Convert.ToInt32(dr["TestInter"]) == 1)
                                    {
                                        string path = "C:\\inetpub\\lims.bbtlabs.com\\REPORTSERVICE\\MasterFiles\\";
                                        // string path = _config.GetValue<string>(ConfigKeys.MasterFilePath);
                                        path = path + parameter[6].ToString() + "/T/InterNotes/" + Convert.ToInt32(dr["TestNo"]).ToString() + ".ym";
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

                    using (XtraReport report = XtraReport.FromFile(reportcontext.ReportPath))
                    {
                        foreach (var item in objdictionary)
                        {
                            using (DevExpress.XtraReports.Parameters.Parameter param = new DevExpress.XtraReports.Parameters.Parameter())
                            {
                                param.Name = item.Key;
                                param.Value = item.Value;
                                report.Parameters.Add(param);
                            }
                        }
                        report.DataSource = datable;
                        filename = Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                        report.ExportToPdf(reportcontext.ExportPath + filename);
                        Response.Redirect(reportcontext.ExportURL + filename);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}