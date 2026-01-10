using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository
{
    public class ArchiveRepository: IArchiveRepository
    {
        private IConfiguration _config;
        public ArchiveRepository(IConfiguration config) { _config = config; }


        public List<LstSearch> ArchivePatientSearch(RequestCommonSearch req)
        {           
                List<LstSearch> lst = new List<LstSearch>();
                try
                {
                    using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _pagecode = new SqlParameter("pagecode", req.pagecode);
                        var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req.viewvenuebranchno);
                        var _searchby = new SqlParameter("searchby", req.searchby);
                        var _searchtext = new SqlParameter("searchtext", req.searchtext);
                        var _venueno = new SqlParameter("venueno", req.venueno);
                        var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                        var _userno = new SqlParameter("userno", req.userno);
                        lst = context.GetArchivePatientDTO.FromSqlRaw(
                            "Execute dbo.pro_SearchArchiveVisit @pagecode,@viewvenuebranchno,@searchby,@searchtext,@venueno,@venuebranchno,@userno",
                            _pagecode, _viewvenuebranchno, _searchby, _searchtext, _venueno, _venuebranchno, _userno).ToList();
                    }
                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex, "ArchiveRepository.ArchivePatientSearch", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
                }
                return lst;
            
        }

        public List<GetArchivePatientResponse> GetArchivePatientDetails(GetArchivePatientRequest req)
        {
            List<GetArchivePatientResponse> lst = new List<GetArchivePatientResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.ArchiveDefaultConnection)))
                {
                   
                    var _visitNo = new SqlParameter("PatientVisitNo", req.PatientVisitNo);
                    var _venueno = new SqlParameter("VenueNo", req.VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _userno = new SqlParameter("UserNo", req.UserNo);
                    lst = context.GetArchivePatientDetailsDTO.FromSqlRaw(
                        "Execute dbo.pro_GetArchiveVisit @PatientVisitNo,@VenueNo,@VenueBranchNo,@UserNo",
                         _visitNo, _venueno, _venuebranchno, _userno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ArchiveRepository.GetArchivePatientDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return lst;

        }
    }
}


