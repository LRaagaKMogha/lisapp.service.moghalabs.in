using Dev.IRepository.External.Patient;
using DEV.Common;
using Service.Model.EF.External.Patient;
using Service.Model.External.Billing;
using Service.Model.External.Patient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository.External.Patient
{
    public class PatientInformationRepository : IPatientInformationRepository
    {
        private IConfiguration _config;
        public PatientInformationRepository(IConfiguration config) { _config = config; }
        public List<LstPatientInfo> GetPatientInfo(int pVenueNo, int pVenueBranchNo, string pDtFrom, string pDtTo)
        {
            List<LstPatientInfo> objResponse = new List<LstPatientInfo>();

            try
            {
                using (var context = new PatientInfoContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);
                    var _dtFrom = new SqlParameter("FromDate", pDtFrom);
                    var _dtTo = new SqlParameter("ToDate", pDtTo);

                    objResponse = context.FetchPatientInformation.FromSqlRaw(
                           "Execute dbo.Pro_Ex_GetPatientInfo" +
                           " @VenueNo, @VenueBranchNo, @FromDate, @ToDate",
                             _venueNo, _venueBranchNo, _dtFrom, _dtTo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInformationRepository.GetPatientInfo", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }
    }
}
