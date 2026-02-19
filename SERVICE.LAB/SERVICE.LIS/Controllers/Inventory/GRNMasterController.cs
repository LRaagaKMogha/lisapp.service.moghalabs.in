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
            List<GetAllGRNResponse> Objresult = new List<GetAllGRNResponse>();
            try
            {
                Objresult = _GRNMasterRepository.GetAllGRN(masterRequest).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.GetAllGRN", ExceptionPriority.Medium, ApplicationType.APPSERVICE, masterRequest.venueno, (int)masterRequest.venuebranchno, (int)masterRequest.masterNo);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/GRNMaster/GetPOBySupplierDetails")]
        public List<GetPOBySupplierResponse> GetPOBySupplierDetails(int venueNo, int venueBranchNo, int supplierNo)
        {
            List<GetPOBySupplierResponse> Objresult = new List<GetPOBySupplierResponse>();
            try
            {
                Objresult = _GRNMasterRepository.GetPOBySupplierDetails(venueNo, venueBranchNo, supplierNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.GetPOBySupplierDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, supplierNo);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/GRNMaster/GetProductByPO")]
        public List<GetProductsByPOResponse> GetProductByPO(int venueNo, int venueBranchNo, int poNumber)
        {
            List<GetProductsByPOResponse> Objresult = new List<GetProductsByPOResponse>();
            try
            {
                Objresult = _GRNMasterRepository.GetProductByPO(venueNo, venueBranchNo, poNumber).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.GetProductByPO", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, poNumber);
            }
            return Objresult;
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
            List<otherChargeModal> Objresult = new List<otherChargeModal>();
            try
            {
                Objresult = _GRNMasterRepository.GetGRNOCDetailsById(venueNo, venueBranchNo, grnMasterNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.GetGRNOCDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/GRNMaster/GetGRNProductDetails")]
        public List<GetProductsByPOResponse> GetGRNProductDetails(int venueNo, int venueBranchNo, int grnMasterNo)
        {
            List<GetProductsByPOResponse> Objresult = new List<GetProductsByPOResponse>();
            try
            {
                Objresult = _GRNMasterRepository.GetGRNProductDetails(venueNo, venueBranchNo, grnMasterNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.GetGRNProductDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, grnMasterNo);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/GRNMaster/UpdateInvoiceDetails")]
        public CommonAdminResponse UpdateInvoiceDetails(InvoiceUpdateRequest req)
        {
            CommonAdminResponse Objresult = new CommonAdminResponse();
            try
            {
                Objresult = _GRNMasterRepository.UpdateInvoiceDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterController.UpdateInvoiceDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.BranchNo, req.UserNo);
            }
            return Objresult;
        }
    }
}