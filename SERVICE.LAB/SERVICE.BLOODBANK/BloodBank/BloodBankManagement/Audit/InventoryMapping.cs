using BloodBankManagement.Contracts;
using MasterManagement.Contracts;
using Shared.Audit;

namespace BloodBankManagement.Audit
{


    [DtoMapping(typeof(UpsertBloodBankInventoryStatusRequest))]
    public class InventoryStatusMapping : DtoToTableMapping<UpsertBloodBankInventoryStatusRequest>
    {
        public override void SetUp()
        {
            TableName = "BloodBankInventories";
            EntityIdProperty = nameof(UpsertBloodBankInventoryStatusRequest.InventoryId);
            AddProperty(x => x.InventoryId, "Identifier");
        }
    }
    [DtoMapping(typeof(UpsertBloodBankInventoryRequest))]
    public class InventoryMapping : DtoToTableMapping<UpsertBloodBankInventoryRequest>
    {
        public override void SetUp()
        {
            TableName = "BloodBankInventories";
            EntityIdProperty = nameof(BloodBankInventoryResponse.Identifier);
            AddProperty(x => x.ProductCode, "ProductCode", value => new
            {
                MasterKey = "Product",
                Label = "Product Name",
            });
            AddProperty(dto => dto.IsVisualInspectionPassed, "IsVisualInspectionPassed", value => new
            {
                Toggle = "Yes/No",
                Label = "VisualInspection Passed?"
            });
        }
    }
}
