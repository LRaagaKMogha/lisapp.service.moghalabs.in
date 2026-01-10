using System;
using System.Configuration;
using System.IO;
using System.Net;
using DEV.Model.EF;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DEV.Windows.Repository
{
    public class ScheduleReport
    {
        public void PrintPatientReport()
        {
            try
            {
                Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                //objdictionary.Add("PatientVisitNo", req.visitNo.ToString());
                ReportContext _reportContext = new ReportContext(ConfigurationManager.AppSettings["Connectionstring"].ToString());
                DataTable dt = _reportContext.getdatatable(objdictionary, "pro_microStatsReport");
                if (dt.Rows.Count > 0)
                {
                    // Path where you want to save the CSV file
                    string csvFilePath = @"C:\YourDirectory\YourFile.csv";

                    // Create a StreamWriter to write CSV
                    using (StreamWriter writer = new StreamWriter(csvFilePath))
                    {
                        // Write header row
                        foreach (DataColumn column in dt.Columns)
                        {
                            writer.Write(column.ColumnName + ",");
                        }
                        writer.WriteLine();

                        // Write data rows
                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (var item in row.ItemArray)
                            {
                                writer.Write(item.ToString() + ",");
                            }
                            writer.WriteLine();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
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

            }
            return result;
        }


    }
}
