using Shared.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.Audit
{
    [DtoMapping(typeof(orgresponse))]
    public class TblOrganismMapping : DtoToTableMapping<orgresponse>
    {
        public override void SetUp()
        {
            TableName = "tbl_Organism";
            EntityIdProperty = nameof(orgresponse.organismno);
            SubMenuCode = "Organism";
            AddProperty(x => x.organismshortcode, "OrganismCode");
            AddProperty(x => x.organismgroupno, "OrganismTypeNo");
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<orgresponse, object>>>
            {
                x => x.userno
            };
        }
    }

    [DtoMapping(typeof(orgtyperesponse))]
    public class TblOrgTypeMapping : DtoToTableMapping<orgtyperesponse>
    {
        public override void SetUp()
        {
            TableName = "tbl_OrganismType";
            EntityIdProperty = nameof(orgtyperesponse.organismtypeno);
            SubMenuCode = "Organism Type";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<orgtyperesponse, object>>>
            {
                x => x.pageIndex, x => x.TotalRecords, x => x.currentseqNo
            };
        }
    }

    [DtoMapping(typeof(antiresponse))]
    public class TblAntibioticMapping : DtoToTableMapping<antiresponse>
    {
        public override void SetUp()
        {
            TableName = "tbl_Antibiotic";
            EntityIdProperty = nameof(antiresponse.antibioticno);
            SubMenuCode = "Antibiotic";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<antiresponse, object>>>
            {
                x => x.pageIndex, x => x.TotalRecords, x => x.newseqno
            };
        }
    }

    [DtoMapping(typeof(orgAntinsertresponse))]
    public class TblOrgTypeAntibioticMapping : DtoToTableMapping<orgAntinsertresponse>
    {
        public override void SetUp()
        {
            TableName = "tbl_OrganismTypeAntibioticMap";
            EntityIdProperty = nameof(orgAntinsertresponse.organismAntibioticMapNo);
            SubMenuCode = "Organism Type - Antibiotic";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<orgAntinsertresponse, object>>>
            {
                x => x.userno
            };
        }
    }

    [DtoMapping(typeof(lstorgAntiRange))]
    public class TblOrgAntibioticRangeMapping : DtoToTableMapping<lstorgAntiRange>
    {
        public override void SetUp()
        {
            TableName = "tbl_OrganismAntibioticRange";
            IsAutoMap = false;
            QueryMapping(
            @$"select CAST(organismAntibioticRangeNo as varchar(10)) as Id, OrganismNo, AntibioticNo, IntermediateFrom, IntermediateTo, ResistantFrom, ResistantTo, SensitiveFrom, SensitiveTo, DisplayRR, Status from tbl_OrganismAntibioticRange Where OrganismAntibioticRangeNo in @RangeIds ",
            dtos => new
            {
                RangeIds = dtos.Select(x => x.organismAntibioticRangeNo).ToList(),
            },
            dto => dto.organismAntibioticRangeNo.ToString()
            );
            AddProperty(x => x.interprange, "DisplayRR", x => new { ControlType = "Display Range" });
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<lstorgAntiRange, object>>>
            {
                x => x.antibioticname, x => x.sequenceNo
            };
            EntityIdProperty = "Id";
            SubMenuCode = "Organism - Antibiotic Range";
        }
    }
}
