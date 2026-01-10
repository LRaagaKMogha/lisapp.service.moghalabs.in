using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Common
{
    public static class ExportReportService
    {
        public static async Task<string> ExportPrint(string ReportParam, string ReportServiceURL)
        {
            string result = string.Empty;
            try
            {
                string serviceMethod = ReportServiceURL + "Report/ExportPrint";

                using (var client = new HttpClient())
                using (var content = new StringContent(ReportParam, Encoding.UTF8, "application/json"))
                {
                    // Force TLS 1.2
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var response = await client.PostAsync(serviceMethod, content);
                    response.EnsureSuccessStatusCode();

                    var responseText = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<string>(responseText) ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExportReportService.ExportPrint", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
    }
}
