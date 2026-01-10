using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record BloodBankPatientResponse
    (
        Int64 Identifier,
        string NricNumber,
        string PatientName,
        DateTime PatientDOB,
        Int64 NationalityId,
        Int64 GenderId,
        Int64 RaceId,
        Int64 ResidenceStatusId,
        string BloodGroup,
        int NoOfIterations,
        string AntibodyScreening,
        string AntibodyIdentified,
        string ColdAntibodyIdentified,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        bool IsTransfusionReaction,
        string Comments,
        DateTime LastModifiedDateTime,
        DateTime? BloodGroupingDateTime,
        DateTime? AntibodyScreeningDateTime,
        DateTime? LatestAntibodyScreeningDateTime,
        List<SpecialRequirementResponse>? SpecialRequirements = null
    );
}