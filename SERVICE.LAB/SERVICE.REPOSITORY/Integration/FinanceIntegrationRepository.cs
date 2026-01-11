using AutoMapper;
using Azure;
using Dev.IRepository;
using Dev.Repository.Integration;
using Dev.Repository.Integration.externalservices;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Service.Model.EF.External.CommonMasters;
using Service.Model.External.CommonMasters;
using Service.Model.Integration;
using Service.Model.Sample;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using RCMS;
using SimpleImpersonation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Dev.Repository
{
    public class FinanceIntegrationRepository : IFinanceIntegrationRepository
    {
        private IConfiguration _config;
        private IMapper _mapper;
        private readonly IFrontOfficeRepository _IFrontOfficeRepository;
        private readonly IManageSampleRepository _manageSampleRepository;
        private readonly IClientMasterRepository _clientMasterRepository;
        public string url;
        public FinanceIntegrationRepository(IConfiguration config, IMapper mapper, IClientMasterRepository clientMasterRepository)
        {
            _config = config;
            _mapper = mapper;
            _clientMasterRepository = clientMasterRepository;
            this.url = _config["Urls:FinanceOutput"];
        }        

        public List<FinanceIntegrationMaster> GetFinanceMasterDetails(string FinanceType, UserClaimsIdentity user)
        {
            List<FinanceIntegrationMaster> financeIntegrationMasters = new List<FinanceIntegrationMaster>();

            using (var _dbContext = new FinanceIntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                financeIntegrationMasters = _dbContext.GetFinanceMasterDetails.Where(x=> x.FinanceType == FinanceType).OrderBy(x=> x.SeqNo).ToList();
            }
            return financeIntegrationMasters;
        }
        public List<SaleExportResponse> GetFinanceSalesDetails(SaleExportRequest saleExportRequest, UserClaimsIdentity user)
        {
            List<SaleExportResponse> saleExportResponses = new List<SaleExportResponse>();

            using (var context = new FinanceIntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                var _PostingDate = new SqlParameter("PostingDate", saleExportRequest.PostingDate);
                var _VenueNo = new SqlParameter("VenueNo", user.VenueNo.ToString());
                var _VenueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo.ToString());
                saleExportResponses = context.GetFinanceSalesDetails.FromSqlRaw
                    ("Execute dbo.Pro_GetFinanceSalesDetails @PostingDate,@venueNo,@venueBranchNo", _PostingDate, _VenueNo, _VenueBranchNo).ToList();
            }
            return saleExportResponses;
        }

        public List<InvoiceExportResponse> GetFinanceAllInvoiceDetails(DateTime PostingDate,UserClaimsIdentity user)
        {
            List<InvoiceExportResponse> InvoiceExportResponses = new List<InvoiceExportResponse>();

            using (var context = new FinanceIntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                var _PostingDate = new SqlParameter("PostingDate", PostingDate);
                var _VenueNo = new SqlParameter("VenueNo", user.VenueNo.ToString());
                var _VenueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo.ToString());
                InvoiceExportResponses = context.GetFinanceInvoiceDetails.FromSqlRaw
                    ("Execute dbo.Pro_GetFinanceAllInvoiceDetails @PostingDate,@venueNo,@venueBranchNo", _PostingDate, _VenueNo, _VenueBranchNo).ToList();
            }
            return InvoiceExportResponses;
        }
        public List<InvoiceExportResponse> GetFinanceInvoiceDetails(InvoiceExportRequest invoiceExportRequest, UserClaimsIdentity user)
        {
            List<InvoiceExportResponse> InvoiceExportResponses = new List<InvoiceExportResponse>();

            using (var context = new FinanceIntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                var _StartDate = new SqlParameter("StartDate", invoiceExportRequest.StartDate);
                var _EndDate = new SqlParameter("EndDate", invoiceExportRequest.EndDate);
                var _VenueNo = new SqlParameter("VenueNo", user.VenueNo.ToString());
                var _VenueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo.ToString());
                InvoiceExportResponses = context.GetFinanceInvoiceDetails.FromSqlRaw
                    ("Execute dbo.Pro_GetFinanceInvoiceDetails @StartDate,@EndDate,@venueNo,@venueBranchNo", _StartDate, _EndDate, _VenueNo, _VenueBranchNo).ToList();
            }
            return InvoiceExportResponses;
        }
        public List<InvoiceExportResponse> GetFinanceCreditNoteDetails(InvoiceExportRequest invoiceExportRequest, UserClaimsIdentity user)
        {
            List<InvoiceExportResponse> InvoiceExportResponses = new List<InvoiceExportResponse>();

            using (var context = new FinanceIntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                var _StartDate = new SqlParameter("StartDate", invoiceExportRequest.StartDate);
                var _EndDate = new SqlParameter("EndDate", invoiceExportRequest.EndDate);
                var _VenueNo = new SqlParameter("VenueNo", user.VenueNo.ToString());
                var _VenueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo.ToString());
                InvoiceExportResponses = context.GetFinanceCreditNoteDetails.FromSqlRaw
                    ("Execute dbo.Pro_GetFinanceCreditNoteDetails @StartDate,@EndDate,@venueNo,@venueBranchNo", _StartDate, _EndDate, _VenueNo, _VenueBranchNo).ToList();
            }
            return InvoiceExportResponses;
        }

        public List<InvoiceExportResponse> GetFinanceAllCreditNoteDetails(DateTime PostingDate, UserClaimsIdentity user)
        {
            List<InvoiceExportResponse> InvoiceExportResponses = new List<InvoiceExportResponse>();

            using (var context = new FinanceIntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                var _PostingDate = new SqlParameter("@PostingDate", PostingDate);
                var _VenueNo = new SqlParameter("VenueNo", user.VenueNo.ToString());
                var _VenueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo.ToString());
                InvoiceExportResponses = context.GetFinanceCreditNoteDetails.FromSqlRaw
                    ("Execute dbo.Pro_GetFinanceAllCreditNoteDetails @PostingDate,@venueNo,@venueBranchNo", _PostingDate, _VenueNo, _VenueBranchNo).ToList();
            }
            return InvoiceExportResponses;
        }
        public FinanceFileExport FinanceInvoiceExportFile(InvoiceExportRequest invoiceExportRequest, UserClaimsIdentity user)
        {
            FinanceFileExport financeFileExport = new FinanceFileExport();
            var response = GetFinanceMasterDetails("I", user);

            var invoiceresponse = GetFinanceInvoiceDetails(invoiceExportRequest, user);
            var creditresponse = GetFinanceCreditNoteDetails(invoiceExportRequest, user);

            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse outputpath = new AppSettingResponse();
            outputpath = _IMasterRepository.GetSingleAppSetting("InvoiceFilePath");
            //outputpath.ConfigValue = this.url +@"\FinanceOutput\Invoices\";

            var today = DateTime.Today;
            var monthStart = new DateTime(today.Year, today.Month, 1);
            

            var clients = _clientMasterRepository.GetAllClientBySubClinic(user.VenueNo, user.VenueBranchNo);

            try
            {
                var credentails = new UserCredentials("SG-RMG", "lissvc_uat", "L*um1U+BQte@#7w4");
                using SafeAccessTokenHandle safeAccessTokenHandle = credentails.LogonUser(LogonType.Interactive);

                WindowsIdentity.RunImpersonated(safeAccessTokenHandle, () =>
                {
                    string fileName = string.Concat(outputpath.ConfigValue, "LIS_RDN_INV_", DateTime.Now.ToString("yyyyMMdd"), "_", DateTime.Now.ToString("HHmmss")) + ".dat";

                    var invoiceExportResponses = invoiceresponse.GroupBy(x => x.PatientVisitNo).ToList();
                    var creditExportResponses = creditresponse.GroupBy(x => x.PatientVisitNo).ToList();

                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        foreach (var item in response)
                        {
                            writer.Write(item.FieldName);
                            writer.Write('\t');
                        }

                        writer.WriteLine();

                        foreach (var grpresponse in invoiceExportResponses)
                        {
                            //Invoice Accursal
                            var packagebills = grpresponse.Where(x => x.ServiceType == 'P').ToList();

                            var nonpkgbills = grpresponse.Where(x => x.ServiceType != 'P').ToList();

                            decimal pkgAmount = packagebills.Sum(x => x.Amount);
                            decimal nonpkgAmount = nonpkgbills.Sum(x => x.Amount);
                            decimal taxableAmount = Math.Round((pkgAmount + nonpkgAmount) * (Convert.ToDecimal(0.09)),2); // Tax value hard coded as 9%. Should be taken from TaxMaster
                            decimal GrossAmount = pkgAmount + nonpkgAmount + taxableAmount;
                            decimal GrossAmountWithoutTax = pkgAmount + nonpkgAmount;

                            var invoiceaccursal = grpresponse.FirstOrDefault();
                            int i = 0, j = response.Count;
                            string saleIndexes = "1,2,3,5,7,9,10,11,31,33,34,36,37,41";
                            List<int> TagIds = saleIndexes.Split(',')
                                                .Select(t => int.Parse(t))
                                                .ToList();
                            if (GrossAmount > 0)
                            {
                                for (i = 0; i < j; i++)
                                {
                                    if (!TagIds.Exists(x => x == i))
                                    {
                                        writer.Write(response[i].DefaultValue);
                                    }
                                    else
                                    {
                                        switch (i)
                                        {
                                            case 1:
                                                writer.Write(invoiceaccursal.VisitDttm.ToString("dd.MM.yyyy"));
                                                break;
                                            case 2:
                                                writer.Write(invoiceaccursal.VisitDttm.Month == invoiceaccursal.GenerateDTTM.Month ?
                                                    invoiceaccursal.GenerateDTTM.ToString("dd.MM.yyyy") :
                                                    invoiceaccursal.VisitDttm.AddDays(DateTime.DaysInMonth(invoiceaccursal.VisitDttm.Year, invoiceaccursal.VisitDttm.Month) - invoiceaccursal.VisitDttm.Day).ToString("dd.MM.yyyy"));
                                                break;
                                            case 3:
                                                writer.Write(invoiceaccursal.ReceiptNo.ToString());
                                                break;
                                            case 5:
                                                writer.Write("Invoice");
                                                break;
                                            case 7:
                                                writer.Write(!invoiceaccursal.IsInternal ? invoiceaccursal.CustomerCode : string.Empty);
                                                break;
                                            case 9:
                                                writer.Write(invoiceaccursal.IsTaxable ? GrossAmount : GrossAmountWithoutTax);
                                                break;
                                            case 10:
                                                writer.Write(pkgAmount);
                                                break;
                                            case 11:
                                                writer.Write(nonpkgAmount);
                                                break;
                                            case 31:
                                                writer.Write(invoiceaccursal.IsTaxable ? "O9" : "S0");
                                                break;
                                            case 33:
                                                writer.Write(invoiceaccursal.IsTaxable ? taxableAmount : 0);
                                                break;
                                            case 34:
                                                writer.Write("RDN");
                                                break;
                                            case 36:
                                                writer.Write(invoiceaccursal.VisitID.ToString());
                                                break;
                                            case 37:
                                                writer.Write(invoiceaccursal.PatientName);
                                                break;
                                            case 41:
                                                writer.Write(invoiceaccursal.IsInternal ? (clients.FirstOrDefault(x=> x.SubCustomerCode.Trim() == invoiceaccursal.CustomerCode.Trim()) != null ?
                                                                                                clients.FirstOrDefault(x => x.SubCustomerCode.Trim() == invoiceaccursal.CustomerCode.Trim()).CustomerCode: invoiceaccursal.CustomerCode) :"EL");
                                                break;
                                        }
                                    }
                                    writer.Write('\t');
                                }
                                writer.WriteLine();
                            }
                        }

                        //Invoice Reversal
                        foreach (var grpresponse in creditExportResponses)
                        {
                            var packagebills = grpresponse.Where(x => x.ServiceType == 'P').ToList();

                            var nonpkgbills = grpresponse.Where(x => x.ServiceType != 'P').ToList();

                            decimal pkgAmount = packagebills.Sum(x => x.Amount);
                            decimal nonpkgAmount = nonpkgbills.Sum(x => x.Amount);
                            decimal taxableAmount = Math.Round((pkgAmount + nonpkgAmount) * (Convert.ToDecimal(0.09)),2); // Tax value hard coded as 9%. Should be taken from TaxMaster
                            decimal GrossAmount = pkgAmount + nonpkgAmount + taxableAmount;
                            decimal GrossAmountWithoutTax = pkgAmount + nonpkgAmount;

                            var invoiceaccursal = grpresponse.FirstOrDefault();
                            int i = 0, j = response.Count;
                            string saleIndexes = "1,2,3,5,7,9,10,11,31,33,34,36,37,41";
                            List<int> TagIds = saleIndexes.Split(',')
                                                .Select(t => int.Parse(t))
                                                .ToList();

                            if (GrossAmount > 0)
                            {
                                for (i = 0; i < j; i++)
                                {
                                    if (!TagIds.Exists(x => x == i))
                                    {
                                        writer.Write(response[i].DefaultValue);
                                    }
                                    else
                                    {
                                        switch (i)
                                        {
                                            case 1:
                                                writer.Write(invoiceaccursal.VisitDttm.ToString("dd.MM.yyyy"));
                                                break;
                                            case 2:
                                                writer.Write(invoiceaccursal.VisitDttm.Month == invoiceaccursal.GenerateDTTM.Month ?
                                                    invoiceaccursal.GenerateDTTM.ToString("dd.MM.yyyy") :
                                                    invoiceaccursal.VisitDttm.AddDays(DateTime.DaysInMonth(invoiceaccursal.VisitDttm.Year, invoiceaccursal.VisitDttm.Month) - invoiceaccursal.VisitDttm.Day).ToString("dd.MM.yyyy"));
                                                break;
                                            case 3:
                                                writer.Write(invoiceaccursal.ReceiptNo.ToString());
                                                break;
                                            case 5:
                                                writer.Write("Invoice Reversal");
                                                break;
                                            case 7:
                                                writer.Write(!invoiceaccursal.IsInternal ? invoiceaccursal.CustomerCode : string.Empty);
                                                break;
                                            case 9:
                                                writer.Write(invoiceaccursal.IsTaxable ? -GrossAmount : -GrossAmountWithoutTax);
                                                break;
                                            case 10:
                                                writer.Write(-pkgAmount);
                                                break;
                                            case 11:
                                                writer.Write(-nonpkgAmount);
                                                break;
                                            case 31:
                                                writer.Write(invoiceaccursal.IsTaxable ? "O9" : "S0");
                                                break;
                                            case 33:
                                                writer.Write(invoiceaccursal.IsTaxable ? -taxableAmount : 0);
                                                break;
                                            case 34:
                                                writer.Write("RDN");
                                                break;
                                            case 36:
                                                writer.Write(invoiceaccursal.VisitID.ToString());
                                                break;
                                            case 37:
                                                writer.Write(invoiceaccursal.PatientName);
                                                break;
                                            case 41:
                                                writer.Write(invoiceaccursal.IsInternal ? (clients.FirstOrDefault(x => x.SubCustomerCode.Trim() == invoiceaccursal.CustomerCode.Trim()) != null ?
                                                                                                clients.FirstOrDefault(x => x.SubCustomerCode.Trim() == invoiceaccursal.CustomerCode.Trim()).CustomerCode : invoiceaccursal.CustomerCode) : "EL");
                                                break;
                                        }
                                    }
                                    writer.Write('\t');
                                }
                                writer.WriteLine();
                            }
                        }
                    }

                    financeFileExport.FileName = fileName;
                    financeFileExport.ExportedDateTime = DateTime.Now;
                    financeFileExport.ExportPath = outputpath.Description;
                });
            }
            catch (Exception ex)
            {

            }
            return financeFileExport;
        }
        public FinanceFileExport FinanceSalesExportFile(SaleExportRequest saleExportRequest, UserClaimsIdentity user)
        {
            FinanceFileExport financeFileExport = new FinanceFileExport();
            var response = GetFinanceMasterDetails("S", user);

            var salesreponse = GetFinanceSalesDetails(saleExportRequest, user);
            
            var invoiceresponse = GetFinanceAllInvoiceDetails(saleExportRequest.PostingDate,user);
            var creditresponse = GetFinanceAllCreditNoteDetails(saleExportRequest.PostingDate, user);

            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse outputpath = new AppSettingResponse();
            outputpath = _IMasterRepository.GetSingleAppSetting("SalesFilePath");
            //outputpath.ConfigValue = this.url + @"\FinanceOutput\Sales\";

            //var today = DateTime.Today;
            //var monthStart = new DateTime(today.Year, today.Month, 1);

            try
            {
                var credentails = new UserCredentials("SG-RMG", "lissvc_uat", "L*um1U+BQte@#7w4");

                using SafeAccessTokenHandle safeAccessTokenHandle = credentails.LogonUser(LogonType.Interactive);

                WindowsIdentity.RunImpersonated(safeAccessTokenHandle, () =>
                {
                    string fileName = string.Concat(outputpath.ConfigValue, "LIS_RDN_SRF_", DateTime.Now.ToString("yyyyMMdd"), "_", DateTime.Now.ToString("HHmmss")) + ".dat";

                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        foreach (var item in response)
                        {
                            writer.Write(item.FieldName);
                            writer.Write('\t');
                        }
                        writer.WriteLine();
                        int i = 0, j = response.Count;

                        string saleIndexes = "1,2,3,5,9,10,11,31,33,34,36,37";
                        List<int> TagIds = saleIndexes.Split(',')
                                            .Select(t => int.Parse(t))
                                            .ToList();
                        //Sales Accursal

                        var packagebills = salesreponse.Where(x => x.ServiceType == 'P').ToList();

                        var nonpkgbills = salesreponse.Where(x => x.ServiceType != 'P').ToList();

                        decimal pkgAmount = packagebills.Sum(x => x.Amount);
                        decimal nonpkgAmount = nonpkgbills.Sum(x => x.Amount);
                        decimal GrossAmount = pkgAmount + nonpkgAmount;

                        var InvoiceAmount = invoiceresponse.Sum(x => x.Amount);
                        var InvoicePkgAmount = invoiceresponse.Where(x => x.ServiceType == 'P').Sum(x => x.Amount);
                        var InvoiceNonPkgAmount = invoiceresponse.Where(x => x.ServiceType != 'P').Sum(x => x.Amount);

                        var CreditNoteAmount = creditresponse.Sum(x => x.Amount);
                        var CreditNotePkgAmount = creditresponse.Where(x => x.ServiceType == 'P').Sum(x => x.Amount);
                        var CreditNoteNonPkgAmount = creditresponse.Where(x => x.ServiceType != 'P').Sum(x => x.Amount);


                        if (GrossAmount > 0)
                        {
                            for (i = 0; i < j; i++)
                            {
                                if (!TagIds.Exists(x => x == i))
                                {
                                    writer.Write(response[i].DefaultValue);
                                }
                                else
                                {
                                    switch (i)
                                    {
                                        case 1:
                                            writer.Write(saleExportRequest.PostingDate.ToString("dd.MM.yyyy"));
                                            break;
                                        case 2:
                                            writer.Write(saleExportRequest.PostingDate.ToString("dd.MM.yyyy"));
                                            break;
                                        case 3:
                                            writer.Write(saleExportRequest.PostingDate.ToString("dd.MM.yyyy"));
                                            break;
                                        case 5:
                                            writer.Write("Sales Accrual");
                                            break;
                                        case 9:
                                            writer.Write(GrossAmount - InvoiceAmount + CreditNoteAmount);
                                            break;
                                        case 10:
                                            writer.Write(pkgAmount - InvoicePkgAmount + CreditNotePkgAmount);
                                            break;
                                        case 11:
                                            writer.Write(nonpkgAmount - InvoiceNonPkgAmount + CreditNoteNonPkgAmount);
                                            break;
                                        case 31:
                                            writer.Write("S0");
                                            break;
                                        case 33:
                                            writer.Write("0");
                                            break;
                                        case 34:
                                            writer.Write("RDN");
                                            break;
                                        case 36:
                                            writer.Write(saleExportRequest.PostingDate.ToString("dd.MM.yyyy"));
                                            break;
                                        case 37:
                                            writer.Write("Sales Accrual");
                                            break;
                                    }
                                }
                                writer.Write('\t');
                            }

                            using (var _dbContext = new FinanceIntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                            {
                                FinanceSales financeSales = new FinanceSales
                                {
                                    Status = true,
                                    PostingDate = saleExportRequest.PostingDate.Date.AddDays(1),
                                    GrossAmount = GrossAmount - InvoiceAmount + CreditNoteAmount,
                                    PackageAmount = pkgAmount - InvoicePkgAmount + CreditNotePkgAmount,
                                    NonPackageAmount = nonpkgAmount - InvoiceNonPkgAmount +CreditNoteNonPkgAmount,
                                    VenueBranchNo = user.VenueBranchNo,
                                    VenueNo = user.VenueNo,
                                    CreatedOn = DateTime.Now,
                                    ModifiedOn = DateTime.Now
                                };
                                _dbContext.FinanceSales.Add(financeSales);
                                _dbContext.SaveChanges();
                            }
                        }

                        writer.WriteLine();
                        //Sales Reversal

                        FinanceSales reversalsales = new FinanceSales();

                        using (var _dbContext = new FinanceIntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                        {
                            reversalsales = _dbContext.FinanceSales.Where(x => x.PostingDate == saleExportRequest.PostingDate.Date).FirstOrDefault();
                        }

                        //    salesreponse = salesreponse.Where(x => x.CreatedOn.Date < saleExportRequest.PostingDate.Date).ToList();
                        //packagebills = salesreponse.Where(x => x.ServiceType == 'P').ToList();
                        //nonpkgbills = salesreponse.Where(x => x.ServiceType != 'P').ToList();

                        pkgAmount = reversalsales.PackageAmount; //packagebills.Sum(x => x.Amount);
                        nonpkgAmount = reversalsales.NonPackageAmount;//nonpkgbills.Sum(x => x.Amount);
                        GrossAmount = reversalsales.GrossAmount;//pkgAmount + nonpkgAmount;

                        if (GrossAmount > 0)
                        {
                            for (i = 0; i < j; i++)
                            {
                                if (!TagIds.Exists(x => x == i))
                                {
                                    writer.Write(response[i].DefaultValue);
                                }
                                else
                                {
                                    switch (i)
                                    {
                                        case 1:
                                            writer.Write(saleExportRequest.PostingDate.AddDays(-1).ToString("dd.MM.yyyy"));
                                            break;
                                        case 2:
                                            writer.Write(saleExportRequest.PostingDate.ToString("dd.MM.yyyy"));
                                            break;
                                        case 3:
                                            writer.Write(saleExportRequest.PostingDate.AddDays(-1).ToString("dd.MM.yyyy"));
                                            break;
                                        case 5:
                                            writer.Write("Sales Reversal");
                                            break;
                                        case 9:
                                            writer.Write(-(GrossAmount));
                                            break;
                                        case 10:
                                            writer.Write(-(pkgAmount));
                                            break;
                                        case 11:
                                            writer.Write(-(nonpkgAmount));
                                            break;
                                        case 31:
                                            writer.Write("S0");
                                            break;
                                        case 33:
                                            writer.Write("0");
                                            break;
                                        case 34:
                                            writer.Write("RDN");
                                            break;
                                        case 36:
                                            writer.Write(saleExportRequest.PostingDate.AddDays(-1).ToString("dd.MM.yyyy"));
                                            break;
                                        case 37:
                                            writer.Write("Sales Reversal Accrual");
                                            break;
                                    }
                                }
                                writer.Write('\t');

                            }
                        }

                        writer.WriteLine();
                    }
                    financeFileExport.FileName = fileName;
                    financeFileExport.ExportedDateTime = DateTime.Now;
                    financeFileExport.ExportPath = outputpath.Description;

                });
            }
            catch(Exception ex) { }

            return financeFileExport;
        }
        public FinanceFileExport FinanceCustomerFileExport(CustomerExportRequest customerExportRequest, UserClaimsIdentity user)
        {
            FinanceFileExport financeFileExport = new FinanceFileExport();
            var response = GetFinanceMasterDetails("C", user);

            MasterRepository _IMasterRepository = new MasterRepository(_config);

            AppSettingResponse outputpath = new AppSettingResponse();
            outputpath = _IMasterRepository.GetSingleAppSetting("CustomerFilePath");
            //outputpath.ConfigValue = this.url + @"\FinanceOutput\Customer\";

            var citylist = _IMasterRepository.GetCommonMasterList(user.VenueNo, user.VenueBranchNo, "CityName");
            var countrylist = _IMasterRepository.GetCommonMasterList(user.VenueNo, user.VenueBranchNo, "CountryName");

            List<FinanceCustomer> financeCustomers = new List<FinanceCustomer>();
            using (var context = new FinanceIntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                financeCustomers = context.GetFinanceCustomers.Where(x=> x.CreatedOn.Date >= customerExportRequest.StartDate.Date && x.CreatedOn.Date <= customerExportRequest.EndDate.Date).ToList();
            }

            try
            {
                var credentails = new UserCredentials("SG-RMG", "lissvc_uat", "L*um1U+BQte@#7w4");
                using SafeAccessTokenHandle safeAccessTokenHandle = credentails.LogonUser(LogonType.Interactive);

                WindowsIdentity.RunImpersonated(safeAccessTokenHandle, () =>
                {
                    string fileName = string.Concat(outputpath.ConfigValue, "LIS_RDN_CUST_", DateTime.Now.ToString("yyyyMMdd"), "_", DateTime.Now.ToString("HHmmss")) + ".dat";

                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        foreach (var item in response)
                        {
                            writer.Write(item.FieldName);
                            writer.Write('\t');
                        }
                        writer.WriteLine();

                        string customerindexes = "2,3,5,8,9,10,11,12,13,14,15,17";
                        List<int> TagIds = customerindexes.Split(',')
                                            .Select(t => int.Parse(t))
                                            .ToList();

                        int i = 0, j = response.Count;
                        foreach (var customerDto in financeCustomers)
                        {
                            for (i = 0; i < j; i++)
                            {
                                if (!TagIds.Exists(x => x == i))
                                {
                                    writer.Write(response[i].DefaultValue);
                                }
                                else
                                {
                                    switch (i)
                                    {
                                        case 2:
                                            writer.Write(customerDto.UniqueID1);
                                            break;
                                        case 3:
                                            writer.Write(customerDto.UniqueID2);
                                            break;
                                        case 5:
                                            writer.Write(customerDto.PatientName);
                                            break;
                                        case 8:
                                            writer.Write(customerDto.BlkHseLotNo);
                                            break;
                                        case 9:
                                            writer.Write(customerDto.FloorNo);
                                            break;
                                        case 10:
                                            writer.Write(customerDto.UnitNo);
                                            break;
                                        case 11:
                                            writer.Write(customerDto.Street);
                                            break;
                                        case 12:
                                            writer.Write(customerDto.Building);
                                            break;
                                        case 13:
                                            writer.Write(customerDto.PostalCode);
                                            break;
                                        case 14:
                                            writer.Write(citylist.FirstOrDefault(x => x.CommonNo.ToString() == customerDto.City) != null ?
                                                citylist.FirstOrDefault(x => x.CommonNo.ToString() == customerDto.City).CommonName : string.Empty);
                                            break;
                                        case 15:
                                            writer.Write("SG");//countrylist.FirstOrDefault(x => x.CommonNo == customerDto.Country).CommonName
                                            break;
                                        case 17:
                                            writer.Write(customerDto.MobileNumber);
                                            break;
                                    }
                                }
                                writer.Write('\t');
                            }
                            writer.WriteLine();
                        }
                    }
                    financeFileExport.FileName = fileName;
                    financeFileExport.ExportedDateTime = DateTime.Now;
                    financeFileExport.ExportPath = outputpath.Description;
                });
                   
            }
            catch (Exception ex) { }

            return financeFileExport;
        }

        public void InsertCustomerDetails(string ExistingCustomerCode, TblCustomer customerDto, UserClaimsIdentity user)
        {
            using (var context = new FinanceIntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                context.Add(new FinanceCustomer()
                {
                   UniqueID1 = ExistingCustomerCode,
                   UniqueID2 = customerDto.CustomerCode,
                   PatientName = customerDto.CustomerName,
                   Street = customerDto.Street,
                   PostalCode = customerDto.Pincode,
                   City = customerDto.City.ToString(),
                   MobileNumber = customerDto.CustomerMobileNo,
                   CreatedOn = DateTime.Now,
                   ModifiedOn = DateTime.Now,
                   VenueNo = user.VenueNo,
                   VenueBranchNo = user.VenueBranchNo,
                   BlkHseLotNo = customerDto.BlkHseLotNo,
                   FloorNo = customerDto.FloorNo,
                   UnitNo = customerDto.UnitNo,
                   Building = customerDto.Building

                });
                context.SaveChanges();
            }
        }
    }
}
