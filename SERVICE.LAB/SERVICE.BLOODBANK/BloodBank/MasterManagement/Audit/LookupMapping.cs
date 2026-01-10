using MasterManagement.Contracts;
using Shared.Audit;

namespace MasterManagement.Audit
{
    [DtoMapping(typeof(UpsertLookupRequest))]
    public class LookupMapping : DtoToTableMapping<UpsertLookupRequest>
    {
        public override void SetUp()
        {
            TableName = "Lookups";
            EntityIdProperty = "Id"; //nameof(UpsertLookupRequest.Id)
            AddProperty(dto => dto.IsActive, "IsActive", value => new
            {
                Toggle = "Yes/No",
                Label = "Is Active"
            });
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<UpsertLookupRequest, object>>>
            {
                p => p.Description
            };
        }
       
    }

    [DtoMapping(typeof(UpsertNurseRequest))]
    public class NurseMapping : DtoToTableMapping<UpsertNurseRequest>
    {
        public override void SetUp()
        {
            TableName = "Nurses";
            EntityIdProperty = "Id";
            AddProperty(dto => dto.IsActive, "IsActive", value => new
            {
                Toggle = "Yes/No",
                Label = "Is Active"
            });
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<UpsertNurseRequest, object>>>
            {
                p => p.Description
            };
        }

    }

    [DtoMapping(typeof(UpsertProductRequest))]
    public class ProductMapping : DtoToTableMapping<UpsertProductRequest>
    {
        public override void SetUp()
        {
            TableName = "Products";
            EntityIdProperty = "Id";
            AddProperty(dto => dto.IsActive, "IsActive", value => new
            {
                Toggle = "Yes/No",
                Label = "Is Active"
            });
            AddProperty(dto => dto.CategoryId, "CategoryId", value => new
            {
                MasterKey = "Lookups",
                Type = "category",
                Label = "Product Category"
            });
            AddProperty(dto => dto.IsThawed, "IsThawed", value => new
            {
                Toggle = "Yes/No",
                Label = "Is Active"
            });
            AddProperty(dto => dto.SpecialRequirementIds, "", value => new
            {
                MasterKey = "SpecialRequirementId-Lookups",
                Label = "Special Requirements"
            },dto =>
            {
                return dto.SpecialRequirementIds.Select(sr => new { SpecialRequirementId = sr });
            });
        }

    }

    [DtoMapping(typeof(UpsertTariffRequest))]
    public class TariffMapping : DtoToTableMapping<UpsertTariffRequest>
    {
        public override void SetUp()
        {
            TableName = "Tariffs";
            EntityIdProperty = "Id";
            QueryMapping("select CAST(ProductId as VARCHAR(100)) + '_' + CAST(ResidenceId AS VARCHAR(100)) as Id, MRP, IsActive, ProductId, ResidenceId from Tariffs where ProductId in (@ProductIds) AND ResidenceId in (@ResidenceIds)",
                dtos => new
                {
                    ProductIds = dtos.Select(x => x.ProductId).ToList(),
                    ResidenceIds = dtos.Select(x => x.ResidenceId).ToList(),
                },
                dto => dto.ProductId + "_" + dto.ResidenceId);
            AddProperty(dto => dto.IsActive, "IsActive", value => new
            {
                Toggle = "Yes/No",
                Label = "Is Active"
            });
            AddProperty(dto => dto.ResidenceId, "ResidenceId", value => new
            {
                MasterKey = "Lookups",
                Type = "residence",
                Label = "Residence"
            });
            AddProperty(dto => dto.ProductId, "ProductId", value => new
            {
                MasterKey = "Products",
                Label = "Product Name"
            });
        }

    }
}
