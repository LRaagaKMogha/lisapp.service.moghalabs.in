using System;
using System.Collections.Generic;
using System.Linq;
using Service.IRepository.Inventory;
using Service.Common;
using Service.Model;
using Service.Model.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.SERVICE.Controllers.Inventory
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
        public List<GetPurchaseOrderresponse> GetPurchaseOrders(GetAllPORequest masterRequest)
        {
            List<GetPurchaseOrderresponse> Objresult = new List<GetPurchaseOrderresponse>();
            try
            {
                Objresult = _PurchaseOrderRepository.GetPurchaseOrders(masterRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetPurchaseOrders-", ExceptionPriority.Low, ApplicationType.APPSERVICE, masterRequest.venueno, (int)masterRequest.venuebranchno, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/PurchaseOrder/GetSupplierServiceDetails")]
        public List<GetSupplierServiceDTO> Getservice(int VenueNo, int VenueBranchNo, int SupplierNo, int StoreNo = 0, string type = "")
        {
            List<GetSupplierServiceDTO> Objresult = new List<GetSupplierServiceDTO>();
            try
            {
                Objresult = _PurchaseOrderRepository.GetSupplierServiceDetails(VenueNo, VenueBranchNo, SupplierNo, StoreNo, type).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetSupplierServiceDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
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
            List<GetPurchaseDetailsDTO> Objresult = new List<GetPurchaseDetailsDTO>();
            try
            {
                Objresult = _PurchaseOrderRepository.GetPurchaseDetailsById(venueNo, venueBranchNo, PurchaseNo).ToList();

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetPurchaseDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, PurchaseNo);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/PurchaseOrder/GetPOProductDetailsById")]
        public List<POProductDetailsDTO> GetPOProductDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<POProductDetailsDTO> Objresult = new List<POProductDetailsDTO>();
            try
            {
                Objresult = _PurchaseOrderRepository.GetPOProductDetailsById(venueNo, venueBranchNo, PurchaseNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetPOProductDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/PurchaseOrder/GetPOTaxDetailsById")]
        public List<GetTaxDatilsResponse> GetPOTaxDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<GetTaxDatilsResponse> Objresult = new List<GetTaxDatilsResponse>();
            try
            {
                Objresult = _PurchaseOrderRepository.GetPOTaxDetailsById(venueNo, venueBranchNo, PurchaseNo).ToList();

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetPOTaxDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/PurchaseOrder/GetPOOCDetailsById")]
        public List<otherChargeModal> GetPOOCDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<otherChargeModal> Objresult = new List<otherChargeModal>();
            try
            {
                Objresult = _PurchaseOrderRepository.GetPOOCDetailsById(venueNo, venueBranchNo, PurchaseNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetPOOCDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/PurchaseOrder/GetPOTermsDetailsById")]
        public List<Termsconditionlist> GetPOTermsDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<Termsconditionlist> Objresult = new List<Termsconditionlist>();
            try
            {
                Objresult = _PurchaseOrderRepository.GetPOTermsDetailsById(venueNo, venueBranchNo, PurchaseNo).ToList();

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderController.GetPOTermsDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return Objresult;
        }
        #endregion
    }
}