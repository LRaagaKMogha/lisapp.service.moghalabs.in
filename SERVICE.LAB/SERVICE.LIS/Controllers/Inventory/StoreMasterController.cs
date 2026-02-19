using Service.IRepository.Inventory;
using Service.Common;
using Service.Model.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Service.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class StoreMasterController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IStoreMasterRepository _storeMasterRepository;
        public StoreMasterController(IStoreMasterRepository StoreMasterRepository, IConfiguration config)
        {
            _storeMasterRepository = StoreMasterRepository;
            _config = config;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/StoreMaster/GetStoreMasterDetails")]
        public List<StoreMasterResponseDTO> GetStoreMasterDetails(StoreMasterRequestDTO storeMasterRequest)
        {
            List<StoreMasterResponseDTO> Objresult = new List<StoreMasterResponseDTO>();
            try
            {
                Objresult = _storeMasterRepository.GetStoreMasterDetails(storeMasterRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StoreMasterController.GetStoreMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, storeMasterRequest.VenueNo, 0, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVMASTERS,INVOPERATIONS")]
        [HttpGet]
        [Route("api/StoreMaster/GetAllStoreByBranch")]
        public List<StoreDetails> GetAllStoreByBranch(int VenueNo, int VenueBranchNo)
        {
            List<StoreDetails> Objresult = new List<StoreDetails>();
            try
            {
                Objresult = _storeMasterRepository.GetAllStoreByBranch(VenueNo,VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StoreMasterController.GetAllStoreByBranch", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, 0, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/StoreMaster/InsertStoreMaster")]
        public StoreMasterInsertResponseDTO InsertStoreMaster(StoreMasterInsertDTO req)
        {
            StoreMasterInsertResponseDTO Objresult = new StoreMasterInsertResponseDTO();
            try
            {
                Objresult = _storeMasterRepository.InsertStoreMaster(req);
                string _CacheKey = CacheKeys.CommonMaster + "STOREMASTER" + req.VenueNo + req.VenueBranchNo;
                MemoryCacheRepository.RemoveItem(_CacheKey);

                Objresult = _storeMasterRepository.InsertStoreMaster(req);
                string _CacheKey1 = CacheKeys.CommonMaster + "STORES" + req.VenueNo + req.VenueBranchNo;
                MemoryCacheRepository.RemoveItem(_CacheKey1);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StoreMasterController.InsertStoreMasterDetails" + req.StoreID.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return Objresult;
        }
    }
}