using Shared.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Audit
{
    [DtoMapping(typeof(Orgresponse))]
    public class TblOrganismMapping : DtoToTableMapping<Orgresponse>
    {
        public override void SetUp()
        {
            TableName = "tbl_Organism";
            EntityIdProperty = nameof(Orgresponse.organismno);
            SubMenuCode = "Organism";
            AddProperty(x => x.organismshortcode, "OrganismCode");
            AddProperty(x => x.organismgroupno, "OrganismTypeNo");
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<Orgresponse, object>>>
            {
                x => x.userno
            };
        }
    }

    [DtoMapping(typeof(Orgtyperesponse))]
    public class TblOrgTypeMapping : DtoToTableMapping<Orgtyperesponse>
    {
        public override void SetUp()
        {
            TableName = "tbl_OrganismType";
            EntityIdProperty = nameof(Orgtyperesponse.organismtypeno);
            SubMenuCode = "Organism Type";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<Orgtyperesponse, object>>>
            {
                x => x.pageIndex, x => x.TotalRecords, x => x.currentseqNo
            };
        }
    }

    [DtoMapping(typeof(Antiresponse))]
    public class TblAntibioticMapping : DtoToTableMapping<Antiresponse>
    {
        public override void SetUp()
        {
            TableName = "tbl_Antibiotic";
            EntityIdProperty = nameof(Antiresponse.antibioticno);
            SubMenuCode = "Antibiotic";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<Antiresponse, object>>>
            {
                x => x.pageIndex, x => x.TotalRecords, x => x.newseqno
            };
        }
    }

    [DtoMapping(typeof(OrgAntinsertresponse))]
    public class TblOrgtypeantibioticMapping : DtoToTableMapping<OrgAntinsertresponse>
    {
        public override void SetUp()
        {
            TableName = "tbl_OrganismTypeAntibioticMap";
            EntityIdProperty = nameof(OrgAntinsertresponse.OrganismAntibioticMapNo);
            SubMenuCode = "Organism Type - Antibiotic";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<OrgAntinsertresponse, object>>>
            {
                x => x.userno
            };
        }
    }

    [DtoMapping(typeof(LstorgAntiRange))]
    public class TblOrgAntibioticRangeMapping : DtoToTableMapping<LstorgAntiRange>
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
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<LstorgAntiRange, object>>>
            {
                x => x.antibioticname, x => x.sequenceNo
            };
            EntityIdProperty = "Id";
            SubMenuCode = "Organism - Antibiotic Range";
        }
    }
}
