using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository
{
    public class UnitRepository : IUnitRepository
    {
        private IConfiguration _config;
        public UnitRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get Unit Details
        /// </summary>
        /// <returns></returns>
        public List<lstunits> GetUnits(reqUnits req)
        {
            List<lstunits> lst = new List<lstunits>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _unitsno = new SqlParameter("unitsno", req?.unitsNo);
                    var _status = new SqlParameter("status", req?.status);
                    var _isinventory = new SqlParameter("isinventory", req?.isinventory);
                    var _venueno = new SqlParameter("venueno", req?.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req?.venueBranchNo);
                    var _pageIndex = new SqlParameter("pageIndex", req?.pageIndex);
                    lst = context.GetUnitList.FromSqlRaw(
                        "Execute dbo.pro_GetUnitMaster @unitsno,@status,@isinventory,@venueno,@venuebranchno,@pageIndex",
                          _unitsno,_status,_isinventory, _venueno, _venuebranchno,_pageIndex).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UnitRepository.GetUnitList" + req.unitsNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }        

        /// <summary>
        /// Insert Unit Details
        /// </summary>
        /// <param name="Unititem"></param>
        /// <returns></returns>
        public rtnUnit InsertUnitDetails(TblUnits req)
        {
            //int result = 0;
            rtnUnit res = new rtnUnit();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _unitno = new SqlParameter("unitno", req?.UnitsNo);
                    var _unitcode = new SqlParameter("unitcode", req?.UnitsCode);
                    var _unitname = new SqlParameter("unitname", req?.UnitsName);
                    var _status = new SqlParameter("status", req?.Status);
                    var _isinventory = new SqlParameter("isinventory", req?.IsInventory);
                    var _userno = new SqlParameter("userno", req?.CreatedBy);
                    var _venueno = new SqlParameter("venueno", req?.VenueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req?.VenueBranchNo);
                    res = context.InsertUnit.FromSqlRaw(
                       "Execute dbo.pro_InsertUnit @unitno,@unitcode,@unitname,@status,@isinventory,@userno,@venueno,@venuebranchno",
                        _unitno, _unitcode, _unitname, _status, _isinventory, _userno, _venueno, _venuebranchno).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UnitRepository.InsertUnitDetails" + req.UnitsNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return res;
        }
    }
}
