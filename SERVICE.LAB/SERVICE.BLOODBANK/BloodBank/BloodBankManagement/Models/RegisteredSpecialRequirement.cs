using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Models
{
    public class RegisteredSpecialRequirement
    {
        public Int64 Identifier { get; set; }

        public Int64 SpecialRequirementId { get; set; }
        public DateTime Validity { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; } = "";
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public BloodBankRegistration BloodBankRegistration { get; set; }
        public Int64 RegistrationId { get; set; }
        public Int64 PatientId { get; set; }
        public RegisteredSpecialRequirement()
        {

        }

        public RegisteredSpecialRequirement(Int64 identifier, Int64 registrationId, Int64 specialRequirementId, DateTime validity, Int64 modifiedBy, string modifiedByUserName, Int64 patientId)
        {
            Identifier = identifier;
            RegistrationId = registrationId;
            SpecialRequirementId = specialRequirementId;
            Validity = validity;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            PatientId = patientId;
        }
    }
}