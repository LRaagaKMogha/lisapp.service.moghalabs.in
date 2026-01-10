using AutoMapper;
using BloodBankManagement.Helpers;
using DEV.Common;
using Newtonsoft.Json;
using System.Net;

namespace BloodBankManagement.Services.Reports
{
    public class BBExportReportService
    {
        private readonly BloodBankDataContext dataContext;
        private readonly IMapper mapper;

        public static string ExportPrint(string ReportParam, string ReportServiceURL)
        {
            string result = string.Empty;
            try
            {
                string ServiceMethod = ReportServiceURL + "Report/ExportPrint";
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
                ServiceWebRequest.Proxy = null;
                var httpResponse = (HttpWebResponse)ServiceWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = JsonConvert.DeserializeObject<string>(streamReader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "BBExportReportService.ExportPrint", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }

    }
}