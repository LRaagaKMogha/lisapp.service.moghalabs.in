using System;
using Service.IRepository;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Service.API.SERVICE.Controllers
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
        [Route("api/EditBilling/GetEditPatientdetails")]
        public async Task<GetEditPatientDetailsFinalResponse> GetEditPatientdetails(long visitNo, int VenueNo, int VenueBranchNo)
        {
            GetEditPatientDetailsFinalResponse Objresult = new GetEditPatientDetailsFinalResponse();
            try
            {
                Objresult = await _IEditBillingRepository.GetEditPatientDetails(visitNo, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
            return Objresult;
        }

        [HttpPost]
        [Route("api/EditBilling/InsertEditBilling")]
        public async Task<ActionResult<FrontOffficeResponse>> InsertEditBilling([FromBody] FrontOffficeDTO objDTO)
        {
            FrontOffficeResponse result = new FrontOffficeResponse();

            try
            {
                var _errormsg = RegistrationValidation.InsertEditBilling(objDTO);
                if (!_errormsg.status)
                {
                    result = await _IEditBillingRepository.InsertEditBilling(objDTO);
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
            int Objresult = 0;
            try
            {
                Objresult = _IEditBillingRepository.ValidatePTTTest(ServiceNo, ServiceType, VisitNo, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
            return Objresult;
        }
    }
}