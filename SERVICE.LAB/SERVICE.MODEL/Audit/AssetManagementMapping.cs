using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Audit;

namespace DEV.Model.Audit
{
    [DtoMapping(typeof(postAssetManagementDTO))]
    public class TblAssetManagementMapping : DtoToTableMapping<postAssetManagementDTO>
    {
        public override void SetUp()
        {
            TableName = "tbl_InventoryInstruments";
            EntityIdProperty = nameof(postAssetManagementDTO.InstrumentsNo);
            SubMenuCode = "AssetMangement";
            AddProperty(x => x.InstrumentsNo, "InstrumentsNo");
            AddProperty(x => x.InstrumentsName, "InstrumentsName");
            AddProperty(x => x.InstallationDate, "InstallationDate");
            AddProperty(x => x.ModificationDate, "ModificationDate");
            AddProperty(x => x.Remark, "Remark");
            //AddProperty(x => x.DocumentPath, "DocumentPath");
            AddProperty(x => x.manufacturerName, "manufacturerName");
            AddProperty(x => x.venueNo, "venueNo");
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<postAssetManagementDTO, object>>>
            {
                x => x.pageIndex, x => x.userNo
            };
        }
    }
}
