using System;
using System.Collections.Generic;
using System.Linq;
using Dev.IRepository.Inventory;
using DEV.Common;
using Service.Model;
using Service.Model.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class GRNReturnController : ControllerBase
    {
        private readonly IGRNReturnReposistory _GRNReturnRepository;
        public GRNReturnController(IGRNReturnReposistory noteRepository)
        {
            _GRNReturnRepository = noteRepository;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/GRNReturn/GetAllGRNReturn")]
        public List<GetAllGRNReturnResponse> GetAllGRNReturn(GetAllGRNReturnRequest masterRequest)
        {
            List<GetAllGRNReturnResponse> objresult = new List<GetAllGRNReturnResponse>();
            try
            {
                objresult = _GRNReturnRepository.GetAllGRNReturn(masterRequest).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNReturnController.GetAllGRNReturn", ExceptionPriority.Medium, ApplicationType.APPSERVICE, masterRequest.venueno, (int)masterRequest.venuebranchno, (int)masterRequest.masterNo);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/GRNReturn/GetGRNBySupplierDetails")]
        public List<GetGRNBySupplierResponse> GetGRNBySupplierDetails(int venueNo, int venueBranchNo, int supplierNo)
        {
            List<GetGRNBySupplierResponse> objresult = new List<GetGRNBySupplierResponse>();
            try
            {
                objresult = _GRNReturnRepository.GetGRNBySupplierDetails(venueNo, venueBranchNo, supplierNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNReturnController.GetGRNBySupplierDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, supplierNo);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/GRNReturn/GetProductByGRN")]
        public List<GetProductsByGRNResponse> GetProductByGRN(int venueNo, int venueBranchNo, int grnNo)
        {
            List<GetProductsByGRNResponse> objresult = new List<GetProductsByGRNResponse>();
            try
            {
                objresult = _GRNReturnRepository.GetProductByGRN(venueNo, venueBranchNo, grnNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNReturnController.GetProductByGRN", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo,Convert.ToInt16(grnNo));
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/GRNReturn/GetGRNReturnProduct")]
        public List<GetProductsByGRNNo> GetGRNReturnProduct(int venueNo, int venueBranchNo, int grnRtnNo)
        {
            List<GetProductsByGRNNo> objresult = new List<GetProductsByGRNNo>();
            try
            {
                objresult = _GRNReturnRepository.GetGRNReturnProduct(venueNo, venueBranchNo, grnRtnNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNReturnController.GetGRNReturnProduct", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, Convert.ToInt16(grnRtnNo));
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/GRNReturn/InsertGRNReturn")]
        public CommonAdminResponse InsertGRNReturn(PostGRN insertGRNReturn)
        {
            CommonAdminResponse result = new CommonAdminResponse();
            try
            {
                result = _GRNReturnRepository.InsertGRNReturn(insertGRNReturn);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNReturnController.InsertGRNReturn", ExceptionPriority.Low, ApplicationType.APPSERVICE, insertGRNReturn.VenueNo, insertGRNReturn.VenueBranchNo, insertGRNReturn.Createdby);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/GRNReturn/GetGRNOCDetailsById")]
        public List<otherChargeModal> GetGRNOCDetailsById(int venueNo, int venueBranchNo, int GRNReturnNo)
        {
            List<otherChargeModal> objresult = new List<otherChargeModal>();
            try
            {
                objresult = _GRNReturnRepository.GetGRNOCDetailsById(venueNo, venueBranchNo, GRNReturnNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNReturnController.GetGRNOCDetailsById", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }
    }
}