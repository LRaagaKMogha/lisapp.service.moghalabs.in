using System;
using System.Collections.Generic;
using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Service.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceRepository _InsuranceRepository;
        public InsuranceController(IInsuranceRepository noteRepository)
        {
            _InsuranceRepository = noteRepository;
        }
        [HttpGet]
        [Route("api/Insurance/GetNetworkMasterDetails")]
        public List<NetworkMasterDTO> GetNetworkMasterDetails(int venueNo, int venueBranchNo, int pageIndex)
        {
            List<NetworkMasterDTO> objresult = new List<NetworkMasterDTO>();
            try
            {
                objresult = _InsuranceRepository.GetNetworkMasterDetails(venueNo, venueBranchNo, pageIndex);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsuranceController.GetNetworkMasterDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/Insurance/InsertNetworkMasterDetails")]
        public NetworkMasterDTOResponse InsertNetworkMasterDetails(NetworkMasterRequest objDTO)
        {
            NetworkMasterDTOResponse result = new NetworkMasterDTOResponse();
            try
            {

                result = _InsuranceRepository.InsertNetworkMasterDetails(objDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsuranceController.InsertNetworkMasterDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return result;
        }
        [HttpGet]
        [Route("api/Insurance/GetCompanyMasterDetails")]
        public List<CompanyMasterDTO> GetCompanyMasterDetails(int venueNo, int venueBranchNo, int pageIndex)
        {
            List<CompanyMasterDTO> objresult = new List<CompanyMasterDTO>();
            try
            {
                objresult = _InsuranceRepository.GetCompanyMasterDetails(venueNo, venueBranchNo, pageIndex);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsuranceController.GetCompanyMasterDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/Insurance/InsertCompanyMasterDetails")]
        public CompanyMasterDTOResponse InsertCompanyMasterDetails(CompanyMasterRequest objDTO)
        {
            CompanyMasterDTOResponse result = new CompanyMasterDTOResponse();
            try
            {

                result = _InsuranceRepository.InsertCompanyMasterDetails(objDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsuranceController.InsertCompanyMasterDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return result;
        }
        [HttpPost]
        [Route("api/Insurance/InsertDeductionMaster")]
        public DeductionDTOResponse InsertDeductionMaster(DeductionMasterDTO objDTO)
        {
            DeductionDTOResponse result = new DeductionDTOResponse();
            try
            {
                objDTO.Deductionlist =JsonConvert.DeserializeObject<List<DeductionDetailsDTO>>(objDTO.Deductiondata);
                result = _InsuranceRepository.InsertDeductionMaster(objDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsuranceController.InsertDeductionMaster", ExceptionPriority.Medium, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return result;
        }
        [HttpGet]
        [Route("api/Insurance/GetDeductionMaster")]
        public List<DeductionResponse> GetDeductionMaster(int venueNo, int venueBranchNo, int pageIndex)
        {
            List<DeductionResponse> lstDeductionDTO = new List<DeductionResponse>();
            try
            {

                lstDeductionDTO = _InsuranceRepository.GetDeductionMaster(venueNo, venueBranchNo, pageIndex);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsuranceController.GetDeductionMaster", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return lstDeductionDTO;
        }

    }
}