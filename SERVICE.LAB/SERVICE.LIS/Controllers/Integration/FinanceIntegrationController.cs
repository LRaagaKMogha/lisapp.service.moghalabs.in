using Service.IRepository;
using Service.Common;
using Service.Model;
using Service.Model.Integration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class FinanceIntegrationController : ControllerBase
    {
        private readonly IFinanceIntegrationRepository _financeIntegrationRepository;
        public FinanceIntegrationController(IFinanceIntegrationRepository financeIntegrationRepository)
        {
            _financeIntegrationRepository = financeIntegrationRepository;
        }

        [HttpPost]
        [Route("api/Integration/FinanceSalesExportFile")]
        public Task<FinanceFileExport> FinanceSalesExportFile(SaleExportRequest saleExportRequest)
        {
            FinanceFileExport response = new FinanceFileExport();
            try
            {
                var user = HttpContext.Items["User"] as UserClaimsIdentity;
                response = _financeIntegrationRepository.FinanceSalesExportFile(saleExportRequest,user);                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, $"FinanceIntegrationController.FinanceSalesExportFile", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Task.FromResult(response);
        }

        [HttpPost]
        [Route("api/Integration/FinanceInvoiceExportFile")]
        public Task<FinanceFileExport> FinanceInvoiceExportFile(InvoiceExportRequest saleExportRequest)
        {
            FinanceFileExport response = new FinanceFileExport();
            try
            {
                var user = HttpContext.Items["User"] as UserClaimsIdentity;
                response = _financeIntegrationRepository.FinanceInvoiceExportFile(saleExportRequest, user);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, $"FinanceIntegrationController.FinanceInvoiceExportFile", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Task.FromResult(response);
        }

        [HttpPost]
        [Route("api/Integration/FinanceCustomerExportFile")]
        public Task<FinanceFileExport> FinanceCustomerExportFile(CustomerExportRequest customerExportRequest)
        {
            FinanceFileExport response = new FinanceFileExport();
            
            try
            {
                var user = HttpContext.Items["User"] as UserClaimsIdentity;
                response = _financeIntegrationRepository.FinanceCustomerFileExport(customerExportRequest, user);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, $"FinanceIntegrationController.FinanceCustomerExportFile", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Task.FromResult(response);
        }
    }
}
