//using MasterManagement.Contracts;
using Newtonsoft.Json.Linq;
using Shared.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Audit
{
    [DtoMapping(typeof(TblUnits))]
    public class TblUnitsMapping : DtoToTableMapping<TblUnits>
    {
        public override void SetUp()
        {
            TableName = "Tbl_Units";
            EntityIdProperty = nameof(TblUnits.UnitsNo);
            SubMenuCode = "Unit";
        }
    }

    [DtoMapping(typeof(TblContainer))]
    public class TblContainerMapping : DtoToTableMapping<TblContainer>
    {
        public override void SetUp()
        {
            TableName = "tbl_Container";
            EntityIdProperty = nameof(TblContainer.containerNo);
            SubMenuCode = "Container";
            AddProperty(x => x.description, "ContainerDisplayText", value => new
            {
                label = "Description"
            });
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<TblContainer, object>>>
            {
                x => x.userNo, x => x.totalRecords, x => x.pageIndex, x => x.isActive
            };
        }
    }

    [DtoMapping(typeof(TblSample))]
    public class TblSpecimenMapping : DtoToTableMapping<TblSample>
    {
        public override void SetUp()
        {
            TableName = "tbl_Sample";
            EntityIdProperty = nameof(TblSample.SampleNo);
            SubMenuCode = "Specimen";
            AddProperty(x => x.SequenceNo, "SeqNo", value => new { label = "Sequence Number" });
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<TblSample, object>>>
            {
                x => x.TotalRecords, x => x.pageIndex, x => x.updateseqNo
            };
        }
    }

    [DtoMapping(typeof(TblMethod))]
    public class TblMethodMapping : DtoToTableMapping<TblMethod>
    {
        public override void SetUp()
        {
            TableName = "tbl_Method";
            EntityIdProperty = nameof(TblMethod.MethodNo);
            SubMenuCode = "Method";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<TblMethod, object>>>
            {
                x => x.TotalRecords, x => x.pageIndex, x => x.VenueBranchNo
            };
        }
    }

    [DtoMapping(typeof(TblMainDepartment))]
    public class TblMainDeptMapping : DtoToTableMapping<TblMainDepartment>
    {
        public override void SetUp()
        {
            TableName = "tbl_MainDepartment";
            EntityIdProperty = nameof(TblMainDepartment.maindeptno);
            SubMenuCode = "Main Department";
            AddProperty(x => x.departmentname, "MainDeptName", x => new { ControlType = "Main Department Name" });
            AddProperty(x => x.displayname, "MainDeptDisplayText", x => new { ControlType = "Display Name" });
            AddProperty(x => x.shortcode, "DeptShortCode", x => new { ControlType = "Short Code" });
            AddProperty(x => x.sequenceno, "DeptSequenceNo", x => new { ControlType = "Sequence Number"});
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<TblMainDepartment, object>>>
            {
                x => x.pageIndex, x => x.venuebranchno, x => x.userno, x => x.totalRecords
            };
        }
    }

    [DtoMapping(typeof(TblDepartment))]
    public class TblSubDeptMapping : DtoToTableMapping<TblDepartment>
    {
        public override void SetUp()
        {
            TableName = "tbl_Department";
            EntityIdProperty = nameof(TblDepartment.DepartmentNo);
            SubMenuCode = "Sub Department";
        }
    }

    [DtoMapping(typeof(InsloincReq))]
    public class TblLOINCMasterMapping : DtoToTableMapping<InsloincReq>
    {
        public override void SetUp()
        {
            TableName = "tbl_LOINC";
            EntityIdProperty = nameof(InsloincReq.LoincNo);
            SubMenuCode = "LOINC";
            AddProperty(x => x.LoincCode, "LOINC_Code", value => new
            {
                label = "LOINC Code"
            });
            AddProperty(x => x.Hl7FieldID, "HL7_FIELD_SUBFIELD_ID", value => new
            {
                label = "HL7 Field ID"
            });
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<InsloincReq, object>>>
            {
                x => x.UserNo
            };
        }
    }

    [DtoMapping(typeof(InsSnomedReq))]
    public class TblSnomedMasterMapping : DtoToTableMapping<InsSnomedReq>
    {
        public override void SetUp()
        {
            TableName = "tbl_SnomedCodeMaster";
            EntityIdProperty = nameof(InsSnomedReq.SnomedNo);
            SubMenuCode = "Snomed";
            AddProperty(x => x.SnomedNo, "SnomedCodeMasterId");
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<InsSnomedReq, object>>>
            {
                x => x.VenueBranchNo, x => x.UserNo
            };
        }
    }    

    [DtoMapping(typeof(ServiceDTO))]
    public class TblServiceTATAMapping : DtoToTableMapping<ServiceDTO>
    {
        public override void SetUp()
        {
            TableName = "tbl_Test";
            IsAutoMap = false;
            QueryMapping(
            @$"select 'T' + CAST(TestNo as varchar(10)) as Id, processingDays, cutoffTime, processingMinutes from tbl_Test Where TestNo in @TestIds
            UNION ALL
            select 'G' + CAST(GroupNo as varchar(10)) as Id, processingDays, cutoffTime, processingMinutes from tbl_Group Where GroupNo in @GroupIds ",
            dtos => new
            {
                TestIds = dtos.Where(x => x.ServiceType == "T").Select(x => x.ServiceNo).ToList(),
                GroupIds = dtos.Where(x => x.ServiceType == "G").Select(x => x.ServiceNo).ToList(),
            },
            dto => dto.ServiceType + dto.ServiceNo.ToString()
            );

            AddProperty(x => x.cutoffTime, "cutoffTime", x => new { ControlType = "DateTimeHoursMinutes" });
            AddProperty(x => x.processingMinutes, "processingMinutes", x => new { ControlType = "DateTimeHoursMinutes" });
            AddProperty(x => x.processingDays, "processingDays", x => new { ControlType = "MultiSelectCheckBox" });
            EntityIdProperty = "Id";
            SubMenuCode = "TAT";
        }
    }

    [DtoMapping(typeof(TblSubtestheader))]
    public class TblSubtestheaderMapping : DtoToTableMapping<TblSubtestheader>
    {
        public override void SetUp()
        {
            TableName = "tbl_header";
            EntityIdProperty = nameof(TblSubtestheader.headerNo);
            AddProperty(x => x.sequenceNo, "sequencesNo");
            SubMenuCode = "Sub Test Header";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<TblSubtestheader, object>>>
            {
                x => x.userNo, x => x.pageIndex, x => x.TotalRecords
            };
        }
    }
}
