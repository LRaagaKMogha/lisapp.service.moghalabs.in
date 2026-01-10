using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.Audit
{
    [DtoMapping(typeof(InsNationRaceReq))]
    public class TblLookUpMapping : DtoToTableMapping<InsNationRaceReq>
    {
        public override void SetUp()
        {
            TableName = "Lookups";
            EntityIdProperty = nameof(InsNationRaceReq.CommonNo);
            SubMenuCode = "Lookups";
            AddProperty(x => x.CommonNo, "Id");
            AddProperty(x => x.Description, "Name");
            AddProperty(x => x.Status, "IsActive");
        }
    }
}
