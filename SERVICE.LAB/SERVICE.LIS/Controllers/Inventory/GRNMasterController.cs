using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GRNMasterController : ControllerBase
    {
        private readonly IGRNMasterReposistory _GRNMasterRepository;
        public GRNMasterController(IGRNMasterReposistory noteRepository)
        {
            _GRNMasterRepository = noteRepository;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/GRNMaster/GetAllGRN")]
        public List<GetAllGRNResponse> GetAllGRN(GetAllGRNRequest masterRequest)
        {
            List<GetAllGRNResponse> objresult = new List<GetAllGRNResponse>();
            try
            {
                objresult = _GRNMasterRepository.GetAllGRN(masterRequest).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.GetAllGRN", ExceptionPriority.Medium, ApplicationType.APPSERVICE, masterRequest.venueno, (int)masterRequest.venuebranchno, (int)masterRequest.masterNo);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/GRNMaster/GetPOBySupplierDetails")]
        public List<GetPOBySupplierResponse> GetPOBySupplierDetails(int venueNo, int venueBranchNo, int supplierNo)
        {
            List<GetPOBySupplierResponse> objresult = new List<GetPOBySupplierResponse>();
            try
            {
                objresult = _GRNMasterRepository.GetPOBySupplierDetails(venueNo, venueBranchNo, supplierNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.GetPOBySupplierDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, supplierNo);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/GRNMaster/GetProductByPO")]
        public List<GetProductsByPOResponse> GetProductByPO(int venueNo, int venueBranchNo, int poNumber)
        {
            List<GetProductsByPOResponse> objresult = new List<GetProductsByPOResponse>();
            try
            {
                objresult = _GRNMasterRepository.GetProductByPO(venueNo, venueBranchNo, poNumber).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.GetProductByPO", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, poNumber);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/GRNMaster/InsertGRNMaster")]
        public CommonAdminResponse InsertGRNMaster(InsertGRNMasterRequest insertGRNMaster)
        {
            CommonAdminResponse result = new CommonAdminResponse();
            try
            {
                result = _GRNMasterRepository.InsertGRNMaster(insertGRNMaster);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.InsertGRNMaster-", ExceptionPriority.Low, ApplicationType.APPSERVICE, insertGRNMaster.venueNo, insertGRNMaster.venueBranchNo, insertGRNMaster.createdby);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/GRNMaster/GetGRNOCDetailsById")]
        public List<otherChargeModal> GetGRNOCDetailsById(int venueNo, int venueBranchNo, int grnMasterNo)
        {
            List<otherChargeModal> objresult = new List<otherChargeModal>();
            try
            {
                objresult = _GRNMasterRepository.GetGRNOCDetailsById(venueNo, venueBranchNo, grnMasterNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.GetGRNOCDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/GRNMaster/GetGRNProductDetails")]
        public List<GetProductsByPOResponse> GetGRNProductDetails(int venueNo, int venueBranchNo, int grnMasterNo)
        {
            List<GetProductsByPOResponse> objresult = new List<GetProductsByPOResponse>();
            try
            {
                objresult = _GRNMasterRepository.GetGRNProductDetails(venueNo, venueBranchNo, grnMasterNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.GetGRNProductDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, grnMasterNo);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/GRNMaster/UpdateInvoiceDetails")]
        public CommonAdminResponse UpdateInvoiceDetails(InvoiceUpdateRequest req)
        {
            CommonAdminResponse objresult = new CommonAdminResponse();
            try
            {
                objresult = _GRNMasterRepository.UpdateInvoiceDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.UpdateInvoiceDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.BranchNo, req.UserNo);
            }
            return objresult;
        }
    }
}