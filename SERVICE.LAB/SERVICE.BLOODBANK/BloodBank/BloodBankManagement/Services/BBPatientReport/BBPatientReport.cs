using AutoMapper;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using DEV.Common;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BloodBankManagement.Services.Reports
{
    public class BBPatientReport : IBBPatientReport
    {
        private readonly BloodBankDataContext dataContext;
        private readonly IMapper mapper;
        protected readonly IConfiguration Configuration;
        public BBPatientReport(BloodBankDataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.Configuration = configuration;
        }

        public async Task<ErrorOr<List<BBReportOutputDetails>>> PrintReport(Contracts.BBPatientReportRequestParam request)
        {
            List<BBReportOutputDetails> lst = new List<BBReportOutputDetails>();
            try
            {
                string DefaultConnection = string.Empty;
                string key = string.Empty;
                key = request.ReportKey != null && request.ReportKey != "" ? request.ReportKey : "BBReportPrint";
                DefaultConnection = Configuration.GetConnectionString("WebApiDatabase");

                BBReportOutputDetails item = new BBReportOutputDetails();

                Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                objdictionary.Add("PageCode", request.PageCode);
                objdictionary.Add("PatientNo", request.PatientNo.ToString());
                objdictionary.Add("RegistrationNo", request.RegistrationNo.ToString());
                objdictionary.Add("IdentityNo", request.IdentityNo.ToString());
                objdictionary.Add("VenueNo", request.VenueNo.ToString());
                objdictionary.Add("VenueBranchNo", request.VenueBranchNo.ToString());
                objdictionary.Add("UserNo", request.UserNo.ToString());
                objdictionary.Add("TestNos", request.TestNos);
                objdictionary.Add("TestNo", request.TestNo.ToString());
                objdictionary.Add("IsLogo", request.IsLogo.ToString());
                objdictionary.Add("ReportType", request.ReportType);
                BBReportContext objReportContext = new BBReportContext(DefaultConnection);
                //  BBTblReportMaster tblReportMaster = new BBTblReportMaster(0,"","","","","","","","",0,0,false);
                BBTblReportMasterDetails tblReportMaster = new BBTblReportMasterDetails();

                using (var context = new BloodBankDataContext(Configuration))
                {
                    var _reportKey = new SqlParameter("ReportKey", key);
                    var _venueno = new SqlParameter("VenueNo", request.VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", request.VenueBranchNo);

                    var tblReportMasters = await Task.Run(() => context.TblReportMaster.FromSqlRaw(
                     "Execute dbo.pro_GetReportMasterDetails @ReportKey,@VenueNo,@VenueBranchNo",
                       _reportKey, _venueno, _venuebranchno).ToList());
                    //   tblReportMaster = dataContext.TblReportMaster.Where(s => s.ReportKey == key && s.VenueNo == request.VenueNo && s.VenueBranchNo == request.VenueBranchNo).AsSplitQuery().FirstOrDefault();
                    tblReportMaster = tblReportMasters != null && tblReportMasters.Count > 0 ? tblReportMasters.First() : new BBTblReportMasterDetails();
                    if (!Directory.Exists(tblReportMaster.ExportPath))
                    {
                        Directory.CreateDirectory(tblReportMaster.ExportPath);
                    }
                }
                string regId = request.RegistrationNo.ToString();
                string fileName = regId + "_" + Guid.NewGuid().ToString("N").Substring(0, 4) + ".pdf";
                DataTable datable = objReportContext.getdatatable(objdictionary, tblReportMaster.ProcedureName);

                //BBReportParamDTO objitem1 = new BBReportParamDTO(Dictionary<string, Object>[],Dictionary<string,string>,"","","");
                BBReportParamDTODetails objitem = new BBReportParamDTODetails();
                objitem.Datatable = CommonExtension.DatableToDicionary(datable);
                objitem.Paramerter = objdictionary;
                objitem.ReportPath = tblReportMaster.ReportPath;
                objitem.ExportPath = tblReportMaster.ExportPath + fileName;
                objitem.ExportFormat = FileFormat.PDF;
                string ReportParam = JsonConvert.SerializeObject(objitem);
                string filename = BBExportReportService.ExportPrint(ReportParam, this.Configuration.GetValue<string>(ConfigKeys.ReportServiceURL));

                item.PatientExportFile = tblReportMaster.ExportURL + filename;
                item.PatientExportFolderPath = objitem.ExportPath;

                lst.Add(item);
            }
            catch (Exception ex)
            {

            }
            return lst;
        }
    }
}