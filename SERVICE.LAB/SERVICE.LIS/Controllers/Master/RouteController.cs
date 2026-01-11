using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteRepository _RouteRepository;
        public RouteController(IRouteRepository noteRepository)
        {
            _RouteRepository = noteRepository;
        }

        #region Get Route Details
        /// <summary>
        /// Get Route Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Route/GetRouteDetails")]
        public IEnumerable<TblRoute> GetRouteDetails(GetCommonMasterRequest getCommonMaster)
        {
            List<TblRoute> objresult = new List<TblRoute>();
            try
            {
                // objresult = MemoryCacheRepository.GetCacheItem<List<TblRoute>>(CacheKeys.RouteMaster);
                //if (objresult == null)
                //{
                objresult = _RouteRepository.GetRouteDetails(getCommonMaster);
                //    MemoryCacheRepository.AddItem(CacheKeys.RouteMaster, objresult, 1000);
                //}
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetRouteDetails-", ExceptionPriority.Low, ApplicationType.APPSERVICE, getCommonMaster.venueno, (int)getCommonMaster.venuebranchno, 0);
            }
            return objresult;
        }

        #endregion

        #region Insert Route 
        /// <summary>
        /// Insert Route 
        /// </summary>
        /// <param name="Routeitem"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("api/Route/InsertRouteDetails")]
        public int InsertRouteDetails([FromBody] TblRoute Routeitem)
        {
            int result = 0;
            try
            {
                _RouteRepository.InsertRouteDetails(Routeitem);
                MemoryCacheRepository.RemoveItem(CacheKeys.RouteMaster);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertRouteDetails-", ExceptionPriority.Low, ApplicationType.APPSERVICE, Routeitem.VenueNo, (int)Routeitem.VenueBranchNo, 0);
            }
            return result;
        }
        #endregion

        [HttpPost]
        [Route("api/route/GetrouteMaster")]
        public IEnumerable<Routelst> GetrouteMaster(RouteMasterRequest routeitems)
        {
            List<Routelst> result = new List<Routelst>();
            try
            {
                result = _RouteRepository.GetrouteMaster(routeitems);
                string _CacheKey = CacheKeys.CommonMaster + "Route" + routeitems.VenueNo + routeitems.VenueBranchNo;
                MemoryCacheRepository.GetCacheItem<List<CommonMasterDto>>(_CacheKey);
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RouteController.GetrouteMaster" + routeitems.RouteNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, routeitems.VenueNo, routeitems.VenueBranchNo, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/route/InsertRouteMaster")]
        public RouteMasterResponse InsertRouteMaster(Routelst route)
        {
            RouteMasterResponse objresult = new RouteMasterResponse();
            try
            {
                objresult = _RouteRepository.InsertRouteMaster(route);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RouteController.InsertRouteMaster" + route.RouteNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, route.VenueNo, route.VenueBranchNo, 0);
            }
            return objresult;
        }
    }
}