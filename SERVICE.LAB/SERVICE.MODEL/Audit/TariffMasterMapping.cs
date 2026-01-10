using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Audit;

namespace DEV.Model.Audit
{
    [DtoMapping(typeof(InsTariffReq))]
    public class TblClientTariffMapping : DtoToTableMapping<InsTariffReq> 
    {
        public override void SetUp()
        {
            TableName = "tbl_Referrer_Tariff_Mapping";
            EntityIdProperty = nameof(InsTariffReq.ClientTariffMapNo);
            SubMenuCode = "Client - Tariff Mapping";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<InsTariffReq, object>>>
            {
                x => x.UserNo
            };
        }
    }
}
