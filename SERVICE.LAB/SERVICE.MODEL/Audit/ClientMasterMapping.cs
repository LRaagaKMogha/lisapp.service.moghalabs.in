using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Audit;

namespace Service.Model.Audit
{
    [DtoMapping(typeof(TblCustomer))]
    public class TblCustomerMapping : DtoToTableMapping<TblCustomer>
    {
        public override void SetUp()
        {
            TableName = "tbl_Customer";
            EntityIdProperty = nameof(TblCustomer.CustomerNo);
            SubMenuCode = "Client";

            //IsAutoMap = false;
            //QueryMapping(
            //@$"select CAST(CustomerNo as varchar(10)) as CustomerNo, CustomerCode, CustomerName, CustomerEmail, CustomerMobileNo, ClientUsername, Street, Building,
            //    UnitNo, FloorNo, BlkHseLotNo, IsSpeciality, IsInternal, IsTaxable, IsClinic, collectatsource, IsPatientEmail, isreportwhatsapp, isbillwhatsapp,
            //    IsBillSMS, IsBillEmail, RestrictionDays, IsFranchisee, routeNo, hcicode, secondaryemail, RiderNo, MarketingNo, IsShowAmount, Country, State, Active,
            //    ClientBlock, ReportDispatchBlock, BillingBlock, CpBlock, CpBillView, sampleScreen, fCBilling, CpReportView, Pincode, City, Area, Address, ClientPayType,
            //    ContactPersonName, Status, IsInterNotes, IsReportEmail, IsReportSms, AllowBilling, Gstno, Id, Idtype, CustomerType, CreditPeriod, CreditLimit, ActiveDate,
            //    Password, UserName
            //    from tbl_Customer Where CustomerNo in @CustomerIds ",
            //dtos => new
            //{
            //    CustomerIds = dtos.Select(x => x.CustomerNo).ToList(),
            //},
            //dto => dto.CustomerNo.ToString()
            //);
            //EntityIdProperty = "CustomerNo";

            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<TblCustomer, object>>>
            {
                x => x.IsApproval, x => x.IsReject, x => x.RejectReason, x => x.OldCustomerNo
            };
        }
    }

    [DtoMapping(typeof(PostCustomersubuserMaster))]
    public class TblCustomerSubMapping : DtoToTableMapping<PostCustomersubuserMaster>
    {
        public override void SetUp()
        {
            TableName = "tbl_Customer_SubUser";
            EntityIdProperty = nameof(PostCustomersubuserMaster.CustomerSubUserNo);
            SubMenuCode = "Client User";
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<PostCustomersubuserMaster, object>>>
            {
                x => x.tblsubcustomer
            };
        }
    }

    [DtoMapping(typeof(TblCustomersubuser))]
    public class TblSubUserVsClientMapping : DtoToTableMapping<TblCustomersubuser>
    {
        public override void SetUp()
        {
            TableName = "tbl_SubCustomerMapping";
            IsAutoMap = false;
            QueryMapping(
            @$"select CAST(CustomerNo as varchar(10)) as Id, SubCustomerNo, Status from tbl_SubCustomerMapping Where CustomerNo in @CustomerIds ",
            dtos => new
            {
                CustomerIds = dtos.Select(x => x.CustomerNo).ToList(),
            },
            dto => dto.CustomerNo.ToString()
            );
            IgnoreProperties = new List<System.Linq.Expressions.Expression<Func<TblCustomersubuser, object>>>
            {
                x => x.CustomerNo
            };
            EntityIdProperty = "Id";
            SubMenuCode = "Client - Clinic Mapping";
        }
    }
}
