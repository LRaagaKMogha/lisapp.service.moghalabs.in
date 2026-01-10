using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model;
using DEV.Model.Inventory.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Dev.IRepository;

namespace DEV.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class CollectionDetailsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICollectionDetailsRepository _CollectionDetailsRepository;
        public CollectionDetailsController(ICollectionDetailsRepository CollectionDetailsRepository, IConfiguration config)
        {
            _CollectionDetailsRepository = CollectionDetailsRepository;
            _config = config;
        }

        [HttpPost]
        [Route("api/CollectionDetails/UpdateCollectionDetails")]

        public resCollectDTS UpdateCollectionDetails(updateCollectDTS collectupd)
        {
            resCollectDTS lst = new resCollectDTS();
            try
            {
                lst = _CollectionDetailsRepository.UpdateCollectionDetails(collectupd);
                string _CacheKey = CacheKeys.CommonMaster + "COLLECTIONDETAILS" + collectupd.VenueNo + collectupd.VenueBranchNo;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UpdateCollectionDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, collectupd.VenueNo, collectupd.VenueBranchNo, 0);
            }
            return lst;
        }


        [HttpPost]
        [Route("api/CollectionDetails/GetCollectionDetails")]
        public List<lstCollectDTS> GetCollectionDetails(reqCollectDTS collectreq)
        {
            List<lstCollectDTS> lst = new List<lstCollectDTS>();
            try
            {
                lst = _CollectionDetailsRepository.GetCollectionDetails(collectreq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetCollectionDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, collectreq.VenueNo, collectreq.VenueBranchNo, 0);
            }
            return lst;
        }
        
    }
}