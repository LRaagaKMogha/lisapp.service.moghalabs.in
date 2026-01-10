using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BloodBankManagement.Services.BloodBankPatients;
using ErrorOr;
using MasterManagement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace BloodBankManagement.Controllers
{
    [CustomAuthorize("BloodBankMgmt")]

    public class BloodBankPatientController : ApiController
    {

        private readonly IBloodBankPatientService _patientService;
        public BloodBankPatientController(IBloodBankPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("{searchText}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodBankPatientResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBloodBankPatientSearch(string searchText)
        {
            ErrorOr<List<Models.BloodBankPatient>> response = await _patientService.GetBloodBankPatientSearch(searchText);
            ErrorOr<List<Models.RegisteredSpecialRequirement>> specialRequirements = new List<Models.RegisteredSpecialRequirement>();
            if (!response.IsError && response.Value.Count > 0)
            {
                specialRequirements = await _patientService.GetSpecialRequierments(response.Value.Select(x => x.Identifier).ToList());
            }

            return response.Match(
                patients => base.Ok(new ServiceResponse<List<Contracts.BloodBankPatientResponse>>("", "200", patients.Select(x => MapBloodBankPatientResponse(x, specialRequirements.Value)).ToList())),
                errors => Problem(errors));
        }

        private static Contracts.BloodBankPatientResponse MapBloodBankPatientResponse(Models.BloodBankPatient response, List<Models.RegisteredSpecialRequirement> specialRequirements)
        {
            var sps = specialRequirements.Where(x => x.PatientId == response.Identifier).Select(row => new Contracts.SpecialRequirementResponse(row.Identifier, row.SpecialRequirementId, row.Validity, row.ModifiedBy, row.ModifiedByUserName, row.LastModifiedDateTime, row.PatientId)).ToList();
            return new Contracts.BloodBankPatientResponse(response.Identifier, response.NRICNumber, response.PatientName, response.PatientDOB, response.NationalityId, response.GenderId, response.RaceId, response.ResidenceStatusId, response.BloodGroup, response.NoOfIterations, response.AntibodyScreening, response.AntibodyIdentified, response.ColdAntibodyIdentified, response.ModifiedBy, response.ModifiedByUserName, response.IsTransfusionReaction, response.Comments, response.LastModifiedDateTime, response.BloodGroupingDateTime, response.AntibodyScreeningDateTime, response.LatestAntibodyScreeningDateTime, sps);
        }
    }
}