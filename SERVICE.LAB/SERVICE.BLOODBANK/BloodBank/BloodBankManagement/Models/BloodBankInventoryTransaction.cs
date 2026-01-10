using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Models
{
    public class BloodBankInventoryTransaction
    {
        public Int64 Identifier { get; set; }
        public string InventoryStatus { get; set; } = "";
        public string Comments { get; set; } = "";
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public BloodBankInventory BloodBankInventory { get; set; }
        public Int64 InventoryId { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;

        public BloodBankInventoryTransaction()
        {

        }

        public BloodBankInventoryTransaction(Int64 inventoryId, string inventoryStatus, string comments, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime)
        {
            InventoryId = inventoryId;
            InventoryStatus = inventoryStatus;
            Comments = comments;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
        }

    }
}