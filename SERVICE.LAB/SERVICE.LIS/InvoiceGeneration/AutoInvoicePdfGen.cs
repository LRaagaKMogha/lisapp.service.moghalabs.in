using Dev.Repository;
using Service.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DEV.API.SERVICE.InvoiceGeneration
{
    public class AutoInvoicePdfGen
    {
        public static void AutoInvoiceGenerate(IConfiguration _config)
        {
            FrontOfficeRepository _frontOffice = new FrontOfficeRepository(_config);
            InvoiceRepository _invoiceRepo = new InvoiceRepository(_config);
            var lstvenueDetails = _invoiceRepo.InvoiceVenueDetails();
            if (lstvenueDetails != null && lstvenueDetails.Count() > 0)
            {
                foreach (var venueDetails in lstvenueDetails)
                {
                    List<CustomerList> lstCustomer = _frontOffice.GetCustomers(venueDetails.venueNo, venueDetails.venueBranchNo, 0, 0, false, true, true, false, -1,0);
                    var lstCusno = lstCustomer.Where(x => x.IsClinic == false).Select(s => s.customerNo).Distinct().ToList();
                    foreach (var customer in lstCusno)
                    {
                        reqinvoice reqinvoice = new reqinvoice
                        {
                            customerNo = customer,
                            fromdate = "",
                            pageCode = "",
                            todate = "",
                            type = "Today",
                            venueBranchNo = venueDetails.venueBranchNo,
                            venueNo = venueDetails.venueNo,
                            isAutoInvoiceGenerate = 1
                        };
                        List<lstcustomerVisit> _lstcstVisit = _invoiceRepo.GetCustomerVisit(reqinvoice);
                        if (_lstcstVisit != null && _lstcstVisit.Count() > 0)
                        {
                            _lstcstVisit.ForEach(x => x.isChecked = true);
                            decimal _billGross = 0, _billDiscount = 0, _billNet = 0, _billCollected = 0, _invoiceGross = 0, _invoiceDiscount = 0,
                               discountPercentage = 0;
                            int _discountNo = 0;
                            string _discountType = "";
                            foreach (var x in _lstcstVisit)
                            {
                                _billGross += x.grossAmount;
                                _billDiscount += x.discountAmount;
                                _billNet += x.netAmount;
                                _billCollected += x.collectedAmount;
                                _invoiceGross += x.dueAmount;
                            }
                            objInvoiceCreate obj = new objInvoiceCreate();
                            obj.invoiceNo = 0;
                            obj.customerNo = customer;
                            obj.billGross = _billGross;
                            obj.billDiscount = _billDiscount;
                            obj.billNet = _billNet;
                            obj.billCollected = _billCollected;
                            obj.invoiceGross = _invoiceGross;
                            obj.discountNo = _discountNo;
                            obj.discountApprovedBy = 0;
                            obj.discountDescription = "";
                            obj.discountType = _discountType;
                            obj.discountPercentage = 0;
                            obj.invoiceDiscount = _invoiceDiscount;
                            obj.invoiceNet = (_billGross - _invoiceDiscount);
                            obj.invoiceDescription = "";
                            obj.venueNo = venueDetails.venueNo;
                            obj.venueBranchNo = venueDetails.venueBranchNo;
                            obj.userNo = 0;
                            obj.lstcustomerVisit = _lstcstVisit;

                            _invoiceRepo.InsertInvoiceCreate(obj);
                        }
                    }
                }
            }
        }
    }
}
