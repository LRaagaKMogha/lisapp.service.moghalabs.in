using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace BloodBankManagement.Models
{
    public class BloodBankPatient
    {
        public Int64 Identifier { get; set; }
        public string NRICNumber { get; set; } = "";
        public string PatientName { get; set; }
        public DateTime PatientDOB { get; set; }
        public Int64 NationalityId { get; set; }
        public Int64 GenderId { get; set; }
        public Int64 RaceId { get; set; }
        public Int64 ResidenceStatusId { get; set; }
        public string BloodGroup { get; set; }
        public int NoOfIterations { get; set; }
        public ICollection<BloodBankRegistration> BloodBankRegistrations { get; } = new List<BloodBankRegistration>();

        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public string AntibodyScreening { get; set; }
        public string AntibodyIdentified { get; set; }
        public string ColdAntibodyIdentified { get; set; }
        public string Comments { get; set; } = "";
        public DateTime? BloodGroupingDateTime { get; set; } = null;
        public DateTime? AntibodyScreeningDateTime { get; set; } = null;
        public DateTime? LatestAntibodyScreeningDateTime { get; set; } = null;

        public bool IsTransfusionReaction { get; set; }
        public BloodBankPatient()
        {

        }

        public BloodBankPatient(
            Int64 identifier,
            string nricNumber,
            string patientName,
            DateTime patientDOB,
            Int64 nationalityId,
            Int64 genderId,
            Int64 raceId,
            Int64 residenceStatusId,
            string bloodGroup,
            int noOfIterations,
            Int64 modifiedBy,
            string modifiedByUserName,
            string antibodyScreening,
            string antibodyIdentified,
            string coldAntibodyIdentified)
        {
            Identifier = identifier;
            NRICNumber = nricNumber;
            PatientName = patientName;
            PatientDOB = patientDOB;
            NationalityId = nationalityId;
            GenderId = genderId;
            RaceId = raceId;
            ResidenceStatusId = residenceStatusId;
            BloodGroup = bloodGroup;
            NoOfIterations = noOfIterations;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            AntibodyScreening = antibodyScreening;
            AntibodyIdentified = antibodyIdentified;
            ColdAntibodyIdentified = coldAntibodyIdentified;
        }

        public static ErrorOr<BloodBankPatient> Create(
            string nricNumber,
            string patientName,
            DateTime patientDOB,
            Int64 nationalityId,
            Int64 genderId,
            Int64 raceId,
            Int64 residenceStatusId,
            string bloodGroup,
            int noOfIterations,
            Int64 modifiedBy,
            string modifiedByUserName,
            string antibodyScreening,
            string antibodyIdentified,
            string coldAntibodyIdentified,
            Int64? identifier = null)
        {
            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }
            return new BloodBankPatient(identifier ?? 0, nricNumber, patientName, patientDOB, nationalityId, genderId, raceId, residenceStatusId, bloodGroup, noOfIterations, modifiedBy, modifiedByUserName, antibodyScreening, antibodyIdentified, coldAntibodyIdentified);
        }

    }
}