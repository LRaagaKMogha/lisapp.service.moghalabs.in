namespace BloodBankManagement.Models
{
    public class BloodBankBilling
    {
        public Int64 Identifier { get; set; }
        public Int64 ProductId { get; set; }
        public Int64 TestId {  get; set; }
        public Int64 ClinicId {  get; set; }
        public string EntityId { get; set; } = "";
        public decimal MRP { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }
        public string Status {  get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public BloodBankRegistration BloodBankRegistration { get; set; }
        public Int64 BloodBankRegistrationId { get; set; }
        public bool IsBilled {  get; set; }
        public string ServiceType { get; set; }
        public BloodBankBilling()
        {

        }

        public BloodBankBilling(Int64 registrationId, Int64 productId, Int64 testId, Int64 clinicId, string entityId, decimal mrp, int unit, decimal price, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string serviceType)
        {
            BloodBankRegistrationId =   registrationId;
            ProductId = productId;
            TestId = testId;
            ClinicId = clinicId;
            EntityId = entityId;
            MRP = mrp;
            Unit = unit;
            Price = price;
            Status = status;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;    
            LastModifiedDateTime = lastModifiedDateTime;    
            ServiceType = serviceType;
        }
    }
}
