using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model;
using DEV.Model.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderReposistory _PurchaseOrderRepository;
        public PurchaseOrderController(IPurchaseOrderReposistory noteRepository)
        {
            _PurchaseOrderRepository = noteRepository;
        }

        #region Get PurchaseOrder Details
        /// <summary>
        /// Get PurchaseOrder Details
        /// </summary>
        /// <returns></returns>

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/PurchaseOrder/GetPurchaseOrderDetails")]
        public List<GetPurchaseOrderResponse> GetPurchaseOrders(GetAllPORequest masterRequest)
        {
            List<GetPurchaseOrderResponse> objresult = new List<GetPurchaseOrderResponse>();
            try
            {
                objresult = _PurchaseOrderRepository.GetPurchaseOrders(masterRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetPurchaseOrders-", ExceptionPriority.Low, ApplicationType.APPSERVICE, masterRequest.venueno, (int)masterRequest.venuebranchno, 0);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/PurchaseOrder/GetSupplierServiceDetails")]
        public List<GetSupplierServiceDTO> GetService(int VenueNo, int VenueBranchNo, int SupplierNo, int StoreNo = 0, string type = "")
        {
            List<GetSupplierServiceDTO> objresult = new List<GetSupplierServiceDTO>();
            try
            {
                objresult = _PurchaseOrderRepository.GetSupplierServiceDetails(VenueNo, VenueBranchNo, SupplierNo, StoreNo, type).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetSupplierServiceDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/PurchaseOrder/InsertPurchaseOrder")]
        public CommonAdminResponse InsertPurchaseOrder(InsertPurchaseOrder purchaseorderEditlst)
        {
            CommonAdminResponse result = new CommonAdminResponse();
            try
            {
                result = _PurchaseOrderRepository.InsertPurchaseOrder(purchaseorderEditlst);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.InsertPurchaseOrder-", ExceptionPriority.Low, ApplicationType.APPSERVICE, purchaseorderEditlst.venueNo, purchaseorderEditlst.venueBranchNo, purchaseorderEditlst.createdby);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/PurchaseOrder/GetPurchaseDetailsById")]
        public List<GetPurchaseDetailsDTO> GetPurchaseDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<GetPurchaseDetailsDTO> objresult = new List<GetPurchaseDetailsDTO>();
            try
            {
                objresult = _PurchaseOrderRepository.GetPurchaseDetailsById(venueNo, venueBranchNo, PurchaseNo).ToList();

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetPurchaseDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, PurchaseNo);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/PurchaseOrder/GetPOProductDetailsById")]
        public List<POProductDetailsDTO> GetPOProductDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<POProductDetailsDTO> objresult = new List<POProductDetailsDTO>();
            try
            {
                objresult = _PurchaseOrderRepository.GetPOProductDetailsById(venueNo, venueBranchNo, PurchaseNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetPOProductDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/PurchaseOrder/GetPOTaxDetailsById")]
        public List<GetTaxDatilsResponse> GetPOTaxDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<GetTaxDatilsResponse> objresult = new List<GetTaxDatilsResponse>();
            try
            {
                objresult = _PurchaseOrderRepository.GetPOTaxDetailsById(venueNo, venueBranchNo, PurchaseNo).ToList();

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetPOTaxDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/PurchaseOrder/GetPOOCDetailsById")]
        public List<otherChargeModal> GetPOOCDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<otherChargeModal> objresult = new List<otherChargeModal>();
            try
            {
                objresult = _PurchaseOrderRepository.GetPOOCDetailsById(venueNo, venueBranchNo, PurchaseNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetPOOCDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/PurchaseOrder/GetPOTermsDetailsById")]
        public List<Termsconditionlist> GetPOTermsDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<Termsconditionlist> objresult = new List<Termsconditionlist>();
            try
            {
                objresult = _PurchaseOrderRepository.GetPOTermsDetailsById(venueNo, venueBranchNo, PurchaseNo).ToList();

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetPOTermsDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }
        #endregion
    }
}