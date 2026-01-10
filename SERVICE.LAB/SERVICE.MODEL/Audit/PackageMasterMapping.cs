using Shared.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.Audit
{
    [DtoMapping(typeof(IntegrationPackageReq))]
    public class TblPackageMappingIntegration : DtoToTableMapping<IntegrationPackageReq>
    {
        public override void SetUp()
        {
            TableName = "tbl_IntegrationPackageMapping";
            EntityIdProperty = "Id";
            SubMenuCode = "Integration Package Mapping";
            AddProperty(x => x.SourceCode, "SourcePkgCode");
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<IntegrationPackageReq, object>>>
            {
                x => x.masterNo, x => x.Type, x => x.TestNo, x => x.UserNo
            };
        }
    }
}
