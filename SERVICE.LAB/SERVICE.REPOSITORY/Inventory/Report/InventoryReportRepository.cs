using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using DEV.Common;
using Service.Model;
using Microsoft.Extensions.Configuration;
using Service.Model.Inventory;
using Dev.IRepository.Inventory.Report;
using Service.Model.Inventory.Report;
using System.Threading.Tasks;

namespace Dev.Repository.Inventory.Report
{    
    public class InventoryReportRepository : IInventoryReportRepository
    {
        private IConfiguration _config;
        public InventoryReportRepository(IConfiguration config) { _config = config; }
        public async Task<InventoryReportOutput> GetInventoryReport(InventoryReportDTO ReportItem)
        {
            InventoryReportOutput result = new InventoryReportOutput();
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                var _Dictionary = ReportItem.ReportParamitem.ToDictionary(x => x.key, x => x.value);
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == ReportItem.ReportKey && x.VenueNo == ReportItem.venueNo
                    && x.VenueBranchNo == ReportItem.venueBranchNo).FirstOrDefault();
                    if (!Directory.Exists(tblReportMaster.ExportPath))
                    {
                        Directory.CreateDirectory(tblReportMaster.ExportPath);
                    }
                }
                DataTable datable = null;
                datable = objReportContext.getdatatable(_Dictionary, tblReportMaster.ProcedureName);

                ReportParamDto objitem = new ReportParamDto();
                objitem.datatable = CommonExtension.DatableToDicionary(datable);
                objitem.paramerter = _Dictionary;
                objitem.ReportPath = tblReportMaster.ReportPath;
                if (ReportItem.fileType == "excel")
                {
                    objitem.ExportPath = tblReportMaster.ExportPath + Guid.NewGuid().ToString("N").Substring(0, 4) + ".xls";
                    objitem.ExportFormat = FileFormat.EXCEL;
                }
                else
                {
                    objitem.ExportPath = tblReportMaster.ExportPath + Guid.NewGuid().ToString("N").Substring(0, 4) + ".pdf";
                    objitem.ExportFormat = FileFormat.PDF;
                }
                string ReportParam = JsonConvert.SerializeObject(objitem);

                //
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppReportServiceURL = "ReportServiceURL";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppReportServiceURL);
                string reportservceurl = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";
                //

                string filename = await ExportReportService.ExportPrint(ReportParam, reportservceurl);
                result.PatientExportFile = tblReportMaster.ExportURL + filename;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InventoryReportRepository.GetInventoryReport/ReportKey-" + ReportItem.ReportKey, ExceptionPriority.High, ApplicationType.REPOSITORY, ReportItem.venueNo, ReportItem.venueBranchNo, ReportItem.userID);
            }
            return result;
        }
        public string GetGridInventoryReport(InventoryReportDTO ReportItem)
        {
            string result = String.Empty;
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                var _Dictionary = ReportItem.ReportParamitem.ToDictionary(x => x.key, x => x.value);
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == ReportItem.ReportKey && x.VenueNo == ReportItem.venueNo
                    && x.VenueBranchNo == ReportItem.venueBranchNo).FirstOrDefault();

                }
                DataTable datable = objReportContext.getdatatable(_Dictionary, tblReportMaster.ProcedureName);
                result = JsonConvert.SerializeObject(datable);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InventoryReportRepository.GetGridInventoryReport/ReportKey-" + ReportItem.ReportKey, ExceptionPriority.High, ApplicationType.REPOSITORY, ReportItem.venueNo, ReportItem.venueBranchNo, ReportItem.userID);
            }
            return result;
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
    }
}
