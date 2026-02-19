using System;
using System.Collections.Generic;
using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class MultiPriceListController : ControllerBase
    {
        private readonly IMultiPriceListRepository _multiPriceListRepository;
        public MultiPriceListController(IMultiPriceListRepository multiPriceListRepository)
        {
            _multiPriceListRepository = multiPriceListRepository;
        }

        #region Get MultiPriceList Details
        /// <summary>
        /// Get MultiPriceList Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MultiPriceList/GetMultiPriceListDetails")]
        public List<GetmultiPriceListResponse> GetMultiPriceListDetails(GetmultiPriceListRequest getRequest)
        {
            List<GetmultiPriceListResponse> Objresult = new List<GetmultiPriceListResponse>();
            try
            {
                Objresult = _multiPriceListRepository.GetMultiPriceListDetails(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetMultiPriceListDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, 0);
            }
            return Objresult;
        }
        #endregion

        #region Insert MultiPriceList 
        /// <summary>
        /// Insert MultiPriceList 
        /// </summary>
        /// <param name="MultiPriceListitem"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("api/MultiPriceList/InsertMultiPriceListDetails")]

        public InsertMultiPriceListResponse InsertMultiPriceListDetails(InsertMultiPriceListRequest multiPriceListitem)
        {
            InsertMultiPriceListResponse result = new InsertMultiPriceListResponse();
            try
            {
                result = _multiPriceListRepository.InsertMultiPriceListDetails(multiPriceListitem);               
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertMultiPriceListDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }
        #endregion

    }
}