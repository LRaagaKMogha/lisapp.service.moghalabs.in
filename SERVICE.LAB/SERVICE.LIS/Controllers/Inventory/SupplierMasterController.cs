using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model;
using DEV.Model.Inventory;
using DEV.Model.Inventory.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class SupplierMasterController : ControllerBase
    {
        private readonly ISupplierMasterRepository _supplierMasterRepository;
        public SupplierMasterController(ISupplierMasterRepository supplierMasterRepository)
        {
            _supplierMasterRepository = supplierMasterRepository;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/SupplierMaster/GetSupplierMasterDetails")]
        public List<GetSupplierMasterResponse> GetSupplierDetails(SupplierMasterRequest masterRequest)
        {
            List<GetSupplierMasterResponse> objResult = new List<GetSupplierMasterResponse>();
            try
            {
                objResult = _supplierMasterRepository.GetSupplierDetails(masterRequest);               
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SupplierMasterController.GetSupplierMasters - " + masterRequest.supplierNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, masterRequest.venueNo, masterRequest.venueBranchNo, masterRequest.userNo);
            }
            return objResult;
        }
            
        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/SupplierMaster/InsertSupplierMasterDetails")]        
        public int InsertSupplierMasterDetails(postSupplierMasterDTO supplierMaster)
        {
            int result = 0;
            try
            {
                result = _supplierMasterRepository.InsertSupplierMasterDetails(supplierMaster);
                string _CacheKey = CacheKeys.CommonMaster + "SUPPLIER" + supplierMaster.venueNo + supplierMaster.venueBranchNo;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SupplierMasterController.InsertSupplierMasterDetails - " + supplierMaster.updSupplierMaster.supplierMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, supplierMaster.venueNo, supplierMaster.venueBranchNo, supplierMaster.userNo);
            }
            return result;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/SupplierMaster/GetEditSuppiler")]
        public EditSupplierresponse GetEditSuppiler(UpdateSupplierMaster req)
        {
            EditSupplierresponse obj = new EditSupplierresponse();
            try
            {
                obj = _supplierMasterRepository.GetEditSuppiler(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SupplierMasterController.GetEditSuppiler - " + req.supplierMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return obj;
        }        
    }
}