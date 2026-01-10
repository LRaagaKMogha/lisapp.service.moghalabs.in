using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository;
using DEV.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class EditBillingController : ControllerBase
    {
        private readonly IEditBillingRepository _IEditBillingRepository;
        public EditBillingController(IEditBillingRepository noteRepository)
        {
            _IEditBillingRepository = noteRepository;
        }


        [HttpGet]
        
        [Route("api/EditBilling/GetEditPatientDetails")]
        public GetEditPatientDetailsFinalResponse GetEditPatientDetails(long visitNo, int VenueNo, int VenueBranchNo)
        {
            GetEditPatientDetailsFinalResponse objresult = new GetEditPatientDetailsFinalResponse();
            try
            {
                objresult = _IEditBillingRepository.GetEditPatientDetails(visitNo, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/EditBilling/InsertEditBilling")]
        public ActionResult<FrontOffficeResponse> InsertEditBilling([FromBody] FrontOffficeDTO objDTO)
        {
            FrontOffficeResponse result = new FrontOffficeResponse();
            try
            {
                var _errormsg = RegistrationValidation.InsertEditBilling(objDTO);
                if (!_errormsg.status)
                {
                    result = _IEditBillingRepository.InsertEditBilling(objDTO);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("api/EditBilling/ValidatePTTTest")]
        public dynamic ValidatePTTTest(int ServiceNo, string ServiceType, int VisitNo, int VenueNo, int VenueBranchNo)
        {
            int objresult = 0;
            try
            {
                objresult = _IEditBillingRepository.ValidatePTTTest(ServiceNo, ServiceType, VisitNo, VenueNo, VenueBranchNo);

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
            return objresult;
        }
    }
}