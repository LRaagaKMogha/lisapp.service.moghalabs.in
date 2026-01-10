using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Audit;

namespace DEV.Model.Audit
{
    [DtoMapping(typeof(postManufacturerMasterDTO))]
    public class TblManufacturerMapping : DtoToTableMapping<postManufacturerMasterDTO>
    {
        public override void SetUp()
        {
            TableName = "tbl_IV_Manufacturer";
            EntityIdProperty = nameof(postManufacturerMasterDTO.manufacturerNo);
            SubMenuCode = "Manufacturer";
            AddProperty(x => x.mobileNo, "Mobile");
            AddProperty(x => x.phoneNo, "Phone");
            AddProperty(x => x.cstreet, "street");
            AddProperty(x => x.cplace, "place");
            AddProperty(x => x.ccityNo, "CityNo");
            AddProperty(x => x.cstateNo, "StateNo");
            AddProperty(x => x.ccountryNo, "CountryNo");
            AddProperty(x => x.cwhatsappNo, "WhatsappNo");
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<postManufacturerMasterDTO, object>>>
            {
                x => x.pageIndex, x => x.userNo
            };
        }
    }
}
