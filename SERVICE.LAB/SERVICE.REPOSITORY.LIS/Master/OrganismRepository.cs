using DEV.Model;
using DEV.Model.EF;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DEV.Common;
using Serilog;

namespace Dev.Repository
{
    public class OrganismRepository : IOrganismRepository
    {
        private IConfiguration _config;
        public OrganismRepository(IConfiguration config) { _config = config; }
        public List<lstorganism> GetOrganismMaster(reqsearchorganism req)
        {
            List<lstorganism> lst = new List<lstorganism>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _flag = new SqlParameter("flag", req.flag);
                    var _searchtext = new SqlParameter("searchtext", req.searchtext);
                    var _organismtypeno = new SqlParameter("organismtypeno", req.organismtypeno);
                    var _organismno = new SqlParameter("organismno", req.organismno);                   
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _status = new SqlParameter("status", req.status);
                    //context.GetOrganismMaster.FromSqlRaw(
                    //    "Execute dbo.pro_GetOrganismMaster @flag,@searchtext,@organismtypeno,@organismno,@venueno,@venuebranchno,@status",
                    //    _flag, _searchtext, _organismtypeno, _organismno, _venueno, _venuebranchno, _status).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OrganismRepository.GetOrganismMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }  
    }
}
