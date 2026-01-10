using BloodBankManagement.Contracts;
using Shared.Audit;

namespace BloodBankManagement.Audit
{

    [DtoMapping(typeof(RecallRequest))]
    public class RecallMapping : DtoToTableMapping<RecallRequest>
    {
        public override void SetUp()
        {
            TableName = "BloodBankRegistration";
            EntityIdProperty = "RegistrationId";
            IsAutoMap = false;
        }
    }

    [DtoMapping(typeof(UpdateBloodSampleInventoryStatusRequest), "Transfused")]
    public class TransfusedMapping : DtoToTableMapping<UpdateBloodSampleInventoryStatusRequest>
    {
        public override void SetUp()
        {
            TableName = "BloodSampleInventories";
            EntityIdProperty = "RegistrationId";
            EntityIdSelector = dto => dto.BloodSampleInventories.First().RegistrationId.ToString();
            IsAutoMap = false;
            AddProperty(x => x.NurseId, "", value => new { MasterKey = "Nurses" });
            AddProperty(x => x.WardId, "", value => new { MasterKey = "clinic" });
            AddProperty(x => x.BloodSampleInventories, "", value => new { MasterKey = "InventoryId-BloodBankInventories,DonationId-BloodBankInventories" }, dtos =>
            {
                return dtos.BloodSampleInventories.Select(dto => new { InventoryId = dto.InventoryId, Status = dto.Status, Temprature = dto.Temperature, TransfusionDateTime = dto.TransfusionDateTime, TransfusionComments = dto.TransfusionComments });
            });
        }
    }

    [DtoMapping(typeof(UpdateBloodSampleInventoryStatusRequest), "Returned")]
    public class BloodProductReturnMapping : DtoToTableMapping<UpdateBloodSampleInventoryStatusRequest>
    {
        public override void SetUp()
        {
            TableName = "BloodSampleInventories";
            EntityIdProperty = "RegistrationId";
            EntityIdSelector = dto => dto.BloodSampleInventories.First().RegistrationId.ToString();
            IsAutoMap = false;
            AddProperty(x => x.NurseId, "", value => new { MasterKey = "Nurses" });
            AddProperty(x => x.WardId, "", value => new { MasterKey = "clinic" });
            AddProperty(x => x.BloodSampleInventories, "", value => new { MasterKey = "InventoryId-BloodBankInventories,DonationId-BloodBankInventories" }, dtos =>
            {
                return dtos.BloodSampleInventories.Select(dto => new { InventoryId = dto.InventoryId, Status = dto.Status, Temprature = dto.Temperature });
            });
        }
    }

    [DtoMapping(typeof(UpdateBloodSampleInventoryStatusRequest), "ProductIssued")]
    public class ProductIssueMapping : DtoToTableMapping<UpdateBloodSampleInventoryStatusRequest>
    {
        public override void SetUp()
        {
            TableName = "BloodSampleInventories";
            EntityIdProperty = "RegistrationId";
            EntityIdSelector = dto => dto.BloodSampleInventories.First().RegistrationId.ToString();
            IsAutoMap = false;
            AddProperty(x => x.NurseId, "", value => new { MasterKey = "Nurses" });
            AddProperty(x => x.IssuingComments, "");
            AddProperty(x => x.WardId, "", value => new { MasterKey = "clinic" });
            AddProperty(x => x.BloodSampleInventories, "", value => new { MasterKey = "InventoryId-BloodBankInventories,DonationId-BloodBankInventories" }, dtos =>
            {
                return dtos.BloodSampleInventories.Select(dto => new { InventoryId = dto.InventoryId, DonationId = dto.InventoryId + "DonationId" });
            });
        }
    }

    [DtoMapping(typeof(UpdateBloodSampleInventoryStatusRequest), "CaseCancel")]
    public class CaseCancelMapping : DtoToTableMapping<UpdateBloodSampleInventoryStatusRequest>
    {
        public override void SetUp()
        {
            TableName = "BloodSampleInventories";
            EntityIdProperty = "RegistrationId";
            EntityIdSelector = dto => dto.BloodSampleInventories.First().RegistrationId.ToString();
            IsAutoMap = false;
            AddProperty(x => x.NurseId, "", value => new { MasterKey = "Nurses" });
            AddProperty(x => x.IssuingComments, "");
            AddProperty(x => x.WardId, "", value => new { MasterKey = "clinic" });
            AddProperty(x => x.BloodSampleInventories, "", value => new { MasterKey = "InventoryId-BloodBankInventories,DonationId-BloodBankInventories" }, dtos =>
            {
                return dtos.BloodSampleInventories.Select(dto => new { InventoryId = dto.InventoryId, DonationId = dto.InventoryId + "DonationId" });
            });
        }
    }


    [DtoMapping(typeof(UpsertInventoryRequest))]
    public class InventoryAssignmentMapping : DtoToTableMapping<UpsertInventoryRequest>
    {
        public override void SetUp()
        {
            TableName = "BloodSampleResults";
            EntityIdProperty = nameof(UpsertInventoryRequest.RegistrationId);
            IsAutoMap = false;
            AddProperty(x => x.RegistrationId, "");
            AddProperty(x => x.ProductId, "", value => new { MasterKey = "Products" });
            AddProperty(x => x.IsRedCells, "");
            AddProperty(x => x.InventoriesData, "", value => new { MasterKey = "InventoryId-BloodBankInventories,ModifiedProductId-Products,ProductId-Products" }, dtos =>
            {
                return dtos.InventoriesData.Select(dto => new { InventoryId = dto.InventoryId, CompatibilityValidTill = dto.CompatibilityValidTill, Comments = dto.Comments, ModifiedProductId = dto.ModifiedProductId, ProductId = dtos.ProductId });
            });
        }
    }

    [DtoMapping(typeof(UpsertBloodSampleResultRequest))]
    public class BloodSampleResultMapping : DtoToTableMapping<UpsertBloodSampleResultRequest>
    {
        public override void SetUp()
        {
            TableName = "BloodSampleResults";
            EntityIdProperty = nameof(UpsertBloodSampleResultRequest.RegistrationId);
            IsAutoMap = false;
            AddProperty(x => x.RegistrationId, "");
            AddProperty(x => x.InventoryId, "", value => new { MasterKey = "BloodBankInventories" });
            AddProperty(x => x.TestId, "", value => new { MasterKey = "SubTests" });
            AddProperty(x => x.ParentTestId, "", value => new { MasterKey = "Tests" });
            AddProperty(x => x.TestValue, "");
            AddProperty(x => x.Comments, "");
            AddProperty(x => x.ReCheck, "");
        }
    }

    [DtoMapping(typeof(UpsertBloodSampleInventoryRequest))]
    public class InventoryAssociationMapping : DtoToTableMapping<UpsertBloodSampleInventoryRequest>
    {
        public override void SetUp()
        {
            TableName = "BloodBankRegistrations";
            EntityIdProperty = nameof(UpsertBloodSampleInventoryRequest.RegistrationId);
            AddProperty(x => x.RegistrationId, "RegistrationId", value => new { MasterKey = "BloodBankRegistration" });
            AddProperty(x => x.ProductId, "", value => new { MasterKey = "Products" });
            AddProperty(x => x.InventoryId, "", value => new { MasterKey = "BloodBankInventories" });
            AddProperty(x => x.CrossMatchingTestId, "", value => new { MasterKey = "Tests" });
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<UpsertBloodSampleInventoryRequest, object>>>
            {
                p => p.IsMatched, p => p.IsComplete, p => p.BloodSampleResultId, p => p.CompatibilityValidTill, p => p.IssuedDateTime
            };
        }
    }

    [DtoMapping(typeof(SampleResponse))]
    public class SampleReceivingMapping : DtoToTableMapping<SampleResponse>
    {
        public override void SetUp()
        {
            TableName = "BloodBankRegistrations";
            EntityIdProperty = nameof(SampleResponse.Identifier);
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<SampleResponse, object>>>
            {
                p => p.RegistrationId, p => p.ParentRegistrationId, p => p.PatientId
            };
            AddProperty(x => x.RegistrationId, "RegistrationId", value => new { MasterKey = "BloodBankRegistration" });
            AddProperty(x => x.SampleTypeId, "SampleTypeId", value => new { MasterKey = "SAMPLE" });
        }
    }


    [DtoMapping(typeof(UpdateStandardPatientRequest))]
    public class UpdateStandardPatientRequestMapping : DtoToTableMapping<UpdateStandardPatientRequest>
    {
        public override void SetUp()
        {
            TableName = "BloodBankRegistrations";
            EntityIdProperty = nameof(UpsertBloodBankRegistrationRequest.Identifier);
            IsAutoMap = false;
            AddProperty(x => x.NRICNo, "");
            AddProperty(x => x.PatientName, "");
            AddProperty(x => x.PatientDOB, "");
            AddProperty(x => x.GenderId, "GenderId", value => new { MasterKey = "Lookups", Type = "gender" });
            AddProperty(x => x.NationalityId, "NationalityId", value => new { MasterKey = "Lookups", Type = "nationality" });
            AddProperty(x => x.RaceId, "RaceId", value => new { MasterKey = "Lookups", Type = "race" });
            AddProperty(x => x.ResidenceStatusId, "ResidenceStatusId", value => new { MasterKey = "Lookups", Type = "residence" });
            AddProperty(x => x.Result, "");
            AddProperty(x => x.Comments, "");
        }
    }

    [DtoMapping(typeof(UpsertBloodBankRegistrationRequest))]
    public class RegistrationMapping : DtoToTableMapping<UpsertBloodBankRegistrationRequest>
    {
        public override void SetUp()
        {
            TableName = "BloodBankRegistrations";
            EntityIdProperty = nameof(UpsertBloodBankRegistrationRequest.Identifier);
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<UpsertBloodBankRegistrationRequest, object>>>
            {
                p => p.ProductTotal, p => p.LabAccessionNumber, p => p.IsActive, p => p.NurseId, p => p.IssuingComments
            };
            AddProperty(x => x.Identifier, "RegistrationId");
            AddProperty(x => x.NationalityId, "NationalityId", value => new { MasterKey = "Lookups", Type = "nationality" });
            AddProperty(x => x.GenderId, "GenderId", value => new { MasterKey = "Lookups", Type = "gender" });
            AddProperty(x => x.RaceId, "RaceId", value => new { MasterKey = "Lookups", Type = "race" });
            AddProperty(x => x.ResidenceStatusId, "ResidenceStatusId", value => new { MasterKey = "Lookups", Type = "residence" });
            AddProperty(x => x.ClinicalDiagnosisId, "ClinicalDiagnosisId", value => new { MasterKey = "Lookups", Type = "clinicaldiagnosis" });
            AddProperty(x => x.IndicationOfTransfusionId, "IndicationOfTransfusionId", value => new { MasterKey = "Lookups", Type = "transfusionindicator" });
            AddProperty(x => x.WardId, "WardId", value => new { MasterKey = "WARDMASTR" });
            AddProperty(x => x.ClinicId, "ClinicId", value => new { MasterKey = "clinic" });
            AddProperty(x => x.DoctorId, "DoctorId", value => new { MasterKey = "Physician" });
            AddProperty(x => x.NurseId, "NurseId", value => new { MasterKey = "Nurses" });
            AddProperty(x => x.Products, "", value => new { MasterKey = "ProductId-Products" }, value =>
            {
                return value.Products.Select(p => new { ProductId = p.ProductId, MRP = p.MRP, Unit = p.Unit, Price = p.Price }).ToList();
            });
            AddProperty(x => x.Results, "", value => new { MasterKey = "TestId-Tests" }, value =>
            {
                return value.Results.Where(x => x.ParentTestId == 0).Select(r => new { TestId = r.TestId, Value = r.TestValue, Comments = r.Comments }).ToList();
            });
            AddProperty(x => x.SpecialRequirements, "", value => new { MasterKey = "SpecialRequiermentId-Lookups", Type = "specialrequirement" }, value =>
            {
                return value.SpecialRequirements.Select(s => new { SpecialRequirementId = s.SpecialRequirementId, Validity = s.Validity }).ToList();
            });
        }
    }
}
