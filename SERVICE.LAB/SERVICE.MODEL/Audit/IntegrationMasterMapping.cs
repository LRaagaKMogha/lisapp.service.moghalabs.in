using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Audit;

namespace DEV.Model.Audit
{
    [DtoMapping(typeof(IntegrationPackageReq))]
    public class TblIntegrationMapping : DtoToTableMapping<IntegrationPackageReq>
    {
        public override void SetUp()
        {
            TableName = "tbl_IntegrationPackageMapping";
            EntityIdProperty = nameof(IntegrationPackageReq.Id);
            SubMenuCode = "Integration Package";
            AddProperty(x => x.SourceCode, "SourcePkgCode");
            AddProperty(x => x.TestNo, "LISPkgCode");
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<IntegrationPackageReq, object>>>
            {
                x => x.pageIndex, x => x.pageSize, x => x.UserNo, x => x.deptNo, x => x.masterNo
            };
        }
    }
}
