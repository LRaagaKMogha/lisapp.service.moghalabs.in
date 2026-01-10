using DEV.Model;
using DEV.Model.Common;
using DEV.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Linq;
using DEV.Model.Sample;

namespace DEV.API.SERVICE.Controllers
{
    public class InvoiceValidation
    {
        // Credit Note Report //
        public static ErrorResponse InsertInvoiceCreditNote(objInvoiceCreditNote req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.invoiceNo) || req.invoiceNo.TrimStart() == string.Empty)
                errors.Add("Invoice No is required");

            // Check if atleast one visit is selected
            var selectedVisits = req.lstCreditNoteVisit?.Where(s => s.isChecked).ToList();
            if (selectedVisits == null || selectedVisits.Count == 0)
            {
                errors.Add("Select atleast one visit");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Invoice Create Report //
        public static ErrorResponse GetCustomerVisit(reqinvoice req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
           
            if (string.IsNullOrEmpty(req.fromdate) || req.fromdate.TrimStart() == string.Empty ||
                    string.IsNullOrEmpty(req.todate) || req.todate.TrimStart() == string.Empty)
            {
                errors.Add("Select The From Date and To Date");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        public static ErrorResponse InsertInvoiceCreate(objInvoiceCreate req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.customerNo == 0)
            {
                errors.Add("Customer No is required");
            }

            // Check if at least one visit is selected
            var selectedVisits = req.lstcustomerVisit?.Where(s => s.isChecked).ToList();
            if (selectedVisits == null || selectedVisits.Count == 0)
            {
                errors.Add("Select at least one visit");
            }

            // Check discount conditions
            else if ((req.invoiceDiscount > 0 || !string.IsNullOrEmpty(req.discountType)) && req.discountNo == 0)
            {
                errors.Add("Select the discount name");
            }

            else if ((req.invoiceDiscount > 0 || req.discountNo > 0) && string.IsNullOrEmpty(req.discountType))
            {
                errors.Add("Select the discount type");
            }

            else if ((req.discountNo > 0 || !string.IsNullOrEmpty(req.discountType)) && req.invoiceDiscount == 0)
            {
                errors.Add("Enter the discount amount");
            }

            else if ((req.invoiceDiscount > 0 || req.discountNo > 0 || !string.IsNullOrEmpty(req.discountType)) && string.IsNullOrEmpty(req.discountDescription))
            {
                errors.Add("Enter the discount description");
            }

            if (req.discountType == "P" && req.discountPercentage > 100)
            {
                errors.Add("Invalid Discount Percent");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Invoice Payment Report //
        public static ErrorResponse InsertInvoicePayment(objInvoicePayment req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.customerNo == 0 || string.IsNullOrEmpty(req.invoiceReceiptNo) || req.invoiceReceiptNo.TrimStart() == string.Empty)
            {
                errors.Add("Search the Customer Name / Invoive Receipt No");
            }

            //var selectedInvoices = req.lstinvoicePaymentMode?.Where(s => s.Amount >= 0).ToList();
            //if (selectedInvoices == null || selectedInvoices.Count == 0)
            //{
            //    errors.Add("Select at least one invoice");
            //}

            // Check discount conditions
            if ((req.paymentDiscount > 0 || !string.IsNullOrEmpty(req.discountType)) && (req.discountNo == 0))
            {
                errors.Add("Select the discount name");
            }

            else if ((req.paymentDiscount > 0 || req.discountNo > 0) && (string.IsNullOrEmpty(req.discountType)))
            {
                errors.Add("Select the discount type");
            }

            else if ((req.discountNo > 0 || !string.IsNullOrEmpty(req.discountType)) && (req.paymentDiscount == 0))
            {
                errors.Add("Enter the discount amount");
            }

            else if ((req.paymentDiscount > 0 || req.discountNo > 0 || !string.IsNullOrEmpty(req.discountType)) && (string.IsNullOrEmpty(req.discountDescription)))
            {
                errors.Add("Enter the discount description");
            }

            if (req.paymentDiscount == 0 && (req.tds == 0) && (req.advanceAdjusted == 0) && (req.creditNotes == 0) && (req.paymentCollected == 0))
            {
                errors.Add("Pay anyone mode");
            }

            if (req.discountType == "P" && (req.discountPercentage > 100))
            {
                errors.Add("Invalid Discount Percent");
            }

            else if (req.discountType == "A" && (req.totalInvoicePayable < req.paymentDiscount))
            {
                errors.Add("Discount amount given less than or equal to invoice payable");
            }

            //else if (req.invoicePayable < req.tds)
            //{
            //    errors.Add("TDS given less than or equal to invoice payable");
            //}

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Invoice Report //
        public static ErrorResponse GetCustomerInvoice(reqinvoice req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.type == "Custom" || req.type == string.Empty)
            {
                if (string.IsNullOrEmpty(req.fromdate) || req.fromdate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(req.todate) || req.todate.TrimStart() == string.Empty)
                {
                    errors.Add("Select The From Date and To Date");
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Invoice Cancel //
        public static ErrorResponse InvoiceCancel(objInvoiceCancel req)
        { 
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.cancelReason) || req.cancelReason.TrimStart() == string.Empty)
                errors.Add("Cancel Reason is required");
 
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
        public static ErrorResponse UpdateTDSFlag(InvoiceTDSUpdateRequest req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.invoiceNo == null || req.invoiceNo == 0)
                errors.Add("Invalid Invoice Number");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
    }
}