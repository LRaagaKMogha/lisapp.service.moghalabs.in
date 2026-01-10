using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DEV.ReportService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public class ReportController : ApiController
    {
        [HttpGet]
        public string Connect()
        {          
            return "Connected";
        }

        [HttpPost]
        public string ExportPrint(ReportParamDTO objlist)
        {
            string result = string.Empty;           

            try
            {
                using (DataTable datatable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(objlist.datatable), (typeof(DataTable))))
                {
                    using (XtraReport report = XtraReport.FromFile(objlist.ReportPath))
                    {
                        foreach (var item in objlist.paramerter)
                        {
                            using (DevExpress.XtraReports.Parameters.Parameter param = new DevExpress.XtraReports.Parameters.Parameter())
                            {
                                param.Name = item.Key;
                                param.Value = item.Value;
                                report.Parameters.Add(param);
                            }
                        }
                        report.DataSource = datatable;
                        if (objlist.ExportFormat == "PDF")
                        {
                            report.ExportToPdf(objlist.ExportPath);
                        }
                        else if (objlist.ExportFormat == "EXCEL")
                        {
                            report.ExportToXlsx(objlist.ExportPath);
                        }
                        result = Path.GetFileName(objlist.ExportPath);
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
            return result;
        }
    }
}
