using Shared.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DEV.Model.Audit
{
    [DtoMapping(typeof(responsevendor))]
    public class TblVendorMasterMapping : DtoToTableMapping<responsevendor>
    {
        public override void SetUp()
        {
            TableName = "tbl_Vendor";
            EntityIdProperty = nameof(responsevendor.vendorno);
            SubMenuCode = "Vendor";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<responsevendor, object>>>
            {
                x => x.userNo
            };
        }
    }

    [DtoMapping(typeof(getcontactlst))]
    public class TblVendorContactMapping : DtoToTableMapping<getcontactlst>
    {
        public override void SetUp()
        {
            TableName = "tbl_VendorVsContact";
            IsAutoMap = false;

            QueryMapping(
            @$"select CAST(vendorContactNo as varchar(10)) as Id, Name, Designation, MobileNo, WhatsAppNo, EmailId from tbl_VendorVsContact Where vendorContactNo in @ContactIds ",
            dtos => new
            {
                ContactIds = dtos.Select(x => x.vendorContactNo).ToList(),
            },
            dto => dto.vendorContactNo.ToString()
            );
            AddProperty(x => x.cname, "Name", x => new { ControlType = "Name" });
            AddProperty(x => x.cdesignation, "Designation", x => new { ControlType = "Designation" });
            AddProperty(x => x.cmobileNo, "MobileNo", x => new { ControlType = "MobileNo" });
            AddProperty(x => x.cwhatsAppNo, "WhatsAppNo", x => new { ControlType = "WhatsAppNo" });
            AddProperty(x => x.cemailId, "EmailId", x => new { ControlType = "EmailId" });
            EntityIdProperty = "Id";
            SubMenuCode = "Vendor - Contact";
        }
    }

    [DtoMapping(typeof(getservicelst))]
    public class TblVendorServiceMapping : DtoToTableMapping<getservicelst>
    {
        public override void SetUp()
        {
            TableName = "tbl_VendorVsServices";
            IsAutoMap = false;
            QueryMapping(
            @$"select 'T' + CAST(ServiceNo as varchar(10)) as Id, Amount, VendorTestCode, ProcessingDays, ReportDays, CutoffHour, CutoffMin, Status from tbl_VendorVsServices Where ServiceNo in @TestIds And ServiceTyoe = 1
            UNION ALL
            select 'G' + CAST(ServiceNo as varchar(10)) as Id, Amount, VendorTestCode, ProcessingDays, ReportDays, CutoffHour, CutoffMin, Status from tbl_VendorVsServices Where ServiceNo in @GroupIds And ServiceTyoe = 2
            UNION ALL
            select 'P' + CAST(ServiceNo as varchar(10)) as Id, Amount, VendorTestCode, ProcessingDays, ReportDays, CutoffHour, CutoffMin, Status from tbl_VendorVsServices Where ServiceNo in @PackageIds And ServiceTyoe = 3 ",
            dtos => new
            {
                TestIds = dtos.Where(x => x.serviceType == 1).Select(x => x.serviceNo).ToList(),
                GroupIds = dtos.Where(x => x.serviceType == 2).Select(x => x.serviceNo).ToList(),
                PackageIds = dtos.Where(x => x.serviceType == 3).Select(x => x.serviceNo).ToList(),
            },
            dto => dto.serviceType == 1 ? "T" + dto.serviceNo.ToString() : dto.serviceType == 2 ? "G" + dto.serviceNo.ToString() : "P" + dto.serviceNo.ToString()
            );
            AddProperty(x => x.serviceCode, "VendorTestCode", x => new { ControlType = "Service Code" });
            AddProperty(x => x.serviceType, "");
            EntityIdProperty = "Id";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<getservicelst, object>>>
            {
                x => x.serviceName, x => x.totalRecords, x => x.pageSize
            };
            SubMenuCode = "Vendor - Services";
        }
    }
}
