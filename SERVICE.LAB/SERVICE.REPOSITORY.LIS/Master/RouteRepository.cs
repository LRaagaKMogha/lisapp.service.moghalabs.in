using DEV.Model;
using DEV.Model.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using DEV.Common;
using Serilog;

namespace Dev.Repository
{
    public class RouteRepository : IRouteRepository
    {
        private IConfiguration _config;
        public RouteRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get Route Details
        /// </summary>
        /// <returns></returns>
        public List<TblRoute> GetRouteDetails(GetCommonMasterRequest getCommonMaster)
        {
            List<TblRoute> objresult = new List<TblRoute>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    if(getCommonMaster.masterNo >0)
                    {
                        objresult = context.TblRoute.Where(x => x.VenueNo == getCommonMaster.venueno && x.VenueBranchNo == getCommonMaster.venuebranchno && x.RouteNo == getCommonMaster.masterNo && x.Status == true).ToList();
                    }
                    else
                    {
                        objresult = context.TblRoute.Where(x => x.VenueNo == getCommonMaster.venueno && x.VenueBranchNo == getCommonMaster.venuebranchno && x.Status ==true).ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetRouteDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, getCommonMaster.venueno, getCommonMaster.venuebranchno, 0);
            }
            return objresult;
        }
        /// <summary>
        /// Search Route
        /// </summary>
        /// <param name="RouteName"></param>
        /// <returns></returns>
        public List<TblRoute> SearchRoute(string RouteName)
        {
            List<TblRoute> objresult = new List<TblRoute>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.TblRoute.Where(a => a.RouteName == RouteName).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SearchRoute - " + RouteName, ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;

        }
        /// <summary>
        /// Insert Route Details
        /// </summary>
        /// <param name="Routeitem"></param>
        /// <returns></returns>
        public int InsertRouteDetails(TblRoute Routeitem)
        {
            int result = 0;
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    if (Routeitem.RouteNo > 0)
                    {
                        Routeitem.ModifiedOn = DateTime.Now;
                        Routeitem.ModifiedBy = Routeitem.CreatedBy;
                        Routeitem.VenueNo = Routeitem.VenueNo;
                        Routeitem.VenueBranchNo = Routeitem.VenueBranchNo;
                        context.Entry(Routeitem).State = EntityState.Modified;
                    }
                    else
                    {
                        Routeitem.CreatedOn = DateTime.Now;
                        Routeitem.CreatedBy = Routeitem.CreatedBy;
                        Routeitem.ModifiedOn = DateTime.Now;
                        Routeitem.VenueNo = Routeitem.VenueNo;
                        Routeitem.VenueBranchNo = Routeitem.VenueBranchNo;
                        context.TblRoute.Add(Routeitem);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertRouteDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, Routeitem.VenueNo, Routeitem.VenueBranchNo, 0);
            }
            return result;
        }

        public List<Routelst> GetrouteMaster(RouteMasterRequest routeitems)
        {
            List<Routelst> objresult = new List<Routelst>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _RouteNo = new SqlParameter("RouteNo", routeitems?.RouteNo);
                    var _VenueNo = new SqlParameter("VenueNo", routeitems?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", routeitems?.VenueBranchNo);
                    var _pageIndex = new SqlParameter("pageIndex", routeitems?.pageIndex);

                    objresult = context.GetrouteMaster.FromSqlRaw(
                        "Execute dbo.pro_GetRoutemaster @RouteNo,@VenueNo,@VenueBranchNo,@pageIndex",
                         _RouteNo, _VenueNo, _VenueBranchNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RouteRepository.GetrouteMaster" + routeitems.RouteNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, routeitems.VenueNo, routeitems.VenueBranchNo, 0);
            }
            return objresult;
        }
        public RouteMasterResponse InsertRouteMaster(Routelst route)
        {
            RouteMasterResponse objresult = new RouteMasterResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _routeNo = new SqlParameter("routeNo", route?.RouteNo);
                    var _routecode = new SqlParameter("routecode", route?.RouteCode);
                    var _routeName = new SqlParameter("routeName", route?.RouteName);
                    var _description = new SqlParameter("description", route?.Description);
                    var _sequenceNo = new SqlParameter("sequenceNo", route?.SequenceNo);
                    var _status = new SqlParameter("status", route?.Status);
                    var _userNo = new SqlParameter("userNo", route?.UserNo);
                    var _Venueno = new SqlParameter("Venueno", route?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", route?.VenueBranchNo);


                    var obj = context.InsertRouteMaster.FromSqlRaw(
                        "Execute dbo.pro_Insertroutemaster @routeNo,@routecode,@routeName,@description,@sequenceNo,@status,@userNo,@venueno,@VenueBranchNo",
                         _routeNo, _routecode, _routeName, _description, _sequenceNo, _status, _userNo, _Venueno, _VenueBranchNo).AsEnumerable().FirstOrDefault();
                    objresult.RouteNo = obj.RouteNo;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RouteRepository.InsertRouteMaster" + route.RouteNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, route.VenueNo, route.VenueBranchNo, route.UserNo);
            }
            return objresult;
        }

    }
}
