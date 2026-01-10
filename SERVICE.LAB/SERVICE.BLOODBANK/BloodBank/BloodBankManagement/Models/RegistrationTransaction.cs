using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Models
{
    public class RegistrationTransaction
    {

        public Int64 Identifier { get; set; }
        public string RegistrationStatus { get; set; } = "";
        public string Comments { get; set; } = "";
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public string EntityType { get; set; }
        public Int64? EntityId { get; set; }
        public string EntityAction { get; set; }
        public BloodBankRegistration BloodBankRegistration { get; set; }
        public Int64 RegistrationId { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;

        public RegistrationTransaction()
        {

        }

        public RegistrationTransaction(Int64 registrationId, string registrationStatus, string comments, Int64 modifiedBy, string entityType, Int64 entityId, string entityAction, string modifiedByUserName, DateTime lastModifiedDateTime)
        {
            RegistrationId = registrationId;
            RegistrationStatus = registrationStatus;
            Comments = comments;
            EntityType = entityType;
            EntityId = entityId;
            EntityAction = entityAction;
            ModifiedBy = modifiedBy;
            ModifiedByUserName  = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
        }
    }
}