using Service.Model;
using Service.Model.Integration;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IFinanceIntegrationRepository
    {
        List<SaleExportResponse> GetFinanceSalesDetails(SaleExportRequest saleExportRequest, UserClaimsIdentity user);
        List<InvoiceExportResponse> GetFinanceInvoiceDetails(InvoiceExportRequest saleExportRequest, UserClaimsIdentity user);
        FinanceFileExport FinanceCustomerFileExport(CustomerExportRequest customerExportRequest, UserClaimsIdentity user);
        FinanceFileExport FinanceSalesExportFile(SaleExportRequest saleExportRequest,UserClaimsIdentity user);
        FinanceFileExport FinanceInvoiceExportFile(InvoiceExportRequest saleExportRequest, UserClaimsIdentity user);
        void InsertCustomerDetails(string ExistingCustomerCode, TblCustomer customerDto, UserClaimsIdentity user);
    }
}
