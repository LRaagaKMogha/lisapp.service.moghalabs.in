using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Audit;

namespace DEV.Model.Audit
{
    [DtoMapping(typeof(CommericalInsReq))]
    public class TblCompanyMasterMapping : DtoToTableMapping<CommericalInsReq>
    {
        public override void SetUp()
        {
            TableName = "tbl_Company";
            EntityIdProperty = nameof(CommericalInsReq.CompanyNo);
            SubMenuCode = "Company";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<CommericalInsReq, object>>>
            {
                x => x.userNo
            };
        }
    }

    [DtoMapping(typeof(GSTInsReq))]
    public class  TblGSTMasterMapping : DtoToTableMapping<GSTInsReq>
    {
        public override void SetUp()
        {
            TableName = "tbl_TaxMaster";
            EntityIdProperty = nameof(GSTInsReq.TaxMastNo);
            SubMenuCode = "GST Master";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<GSTInsReq, object>>>
            {
                x => x.userNo
            };
        }
    }
}
