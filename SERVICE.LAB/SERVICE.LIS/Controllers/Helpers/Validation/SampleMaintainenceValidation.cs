using Service.Model;
using Service.Model.Common;
using DEV.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RtfPipe.Tokens;
using Service.Model.Sample;

namespace DEV.API.SERVICE.Controllers
{
    public class SampleMaintainenceValidation
    {
        // Sample Collection Validation //
        public static ErrorResponse GetManageSampleDetails(CommonFilterRequestDTO RequestItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (RequestItem.Type == "Custom" || RequestItem.Type == string.Empty)
            {
                if (string.IsNullOrEmpty(RequestItem.FromDate) || RequestItem.FromDate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(RequestItem.ToDate) || RequestItem.ToDate.TrimStart() == string.Empty)
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

        public static ErrorResponse CreateManageSample(List<CreateManageSampleRequest> createManageSample)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (createManageSample.Count > 0)
            {
                foreach(var item in createManageSample)
                {
                    if (item.ishigtemprature == true)
                    {
                        errors.Add("Temperature value should be entered for sample name");
                    }
                }
            }
            else
            {
                errors.Add("Select any Patient");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Sample Accession Validation //
        public static ErrorResponse CreateSampleACK(List<CreateSampleActionRequest> insertActionDTOs)
        { 
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (insertActionDTOs.Count > 0)
            {
                foreach (var sampleAction in insertActionDTOs)
                {
                    if (sampleAction.isAccept == false && sampleAction.isReject == false && sampleAction.IsOutSource == false)
                    {
                        errors.Add("Please Select Accept / Reject !");
                    }
                    if (sampleAction.isReject == true && (string.IsNullOrEmpty(sampleAction.remarks) || sampleAction.remarks.TrimStart() == string.Empty))
                    {
                        errors.Add("Please Enter The Reject Remarks");
                    }
                }
            }
            else
            {
                errors.Add("Select any Patient");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        public static ErrorResponse GetSampleActionDetails(SampleActionRequest req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.Type == "Custom" || req.Type == string.Empty)
            {
                if (string.IsNullOrEmpty(req.FromDate) || req.FromDate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(req.ToDate) || req.ToDate.TrimStart() == string.Empty)
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

        // Send Out Validation //
        public static ErrorResponse CreateSampleOutsource(List<CreateSampleOutSourceRequest> createOutSource)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            foreach (var sendOut in createOutSource)
            {
                if (sendOut.vendorNo == 0)
                {
                    errors.Add("Vendor No is required");
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Sample Send - Out //
        public static ErrorResponse GetSampleOutSource(GetSampleOutsourceRequest RequestItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (RequestItem.Type == "Custom" || RequestItem.Type == string.Empty)
            {
                if (string.IsNullOrEmpty(RequestItem.FromDate) || RequestItem.FromDate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(RequestItem.ToDate) || RequestItem.ToDate.TrimStart() == string.Empty)
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

        // Sample Send - Out History //
        public static ErrorResponse GetSampleOutsourceHistory(GetSampleOutSourceHistoryRequest createOutSource)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (createOutSource.Type == "Custom" || createOutSource.Type == string.Empty)
            {
                if (string.IsNullOrEmpty(createOutSource.FromDate) || createOutSource.FromDate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(createOutSource.ToDate) || createOutSource.ToDate.TrimStart() == string.Empty)
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

        // WorkSheet Validation //
        public static ErrorResponse InsertWorkListHistory(WorkListHistoryReq Req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(Req.OrderList) || Req.OrderList.Trim() == string.Empty)
            {
                errors.Add("Select at least one patient.");
            }

            if (Req.Type == "Custom" || Req.Type == string.Empty)
            {
                if (string.IsNullOrEmpty(Req.FromDate) || Req.FromDate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(Req.ToDate) || Req.ToDate.TrimStart() == string.Empty)
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

        public static ErrorResponse GetWorkListDetails(WorkListRequest RequestItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (RequestItem.Type == "Custom" || RequestItem.Type == string.Empty)
            {
                if (string.IsNullOrEmpty(RequestItem.FromDate) || RequestItem.FromDate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(RequestItem.ToDate) || RequestItem.ToDate.TrimStart() == string.Empty)
                {
                    errors.Add("Select The From Date and To Date");
                }
            }

            // Validate for Duplicate Test Names //
            for (int i = 0; i < RequestItem.lstsearch.Count; i++)
            {
                var v = RequestItem.lstsearch[i];
                if (v.testNo > 0)
                {
                    var isduplicate = RequestItem.lstsearch.Where(x => x.testNo == v.testNo && x.testType == v.testType).ToList();
                    if (isduplicate.Count > 1)
                    {
                        errors.Add("TestName is already in filter!");
                        break;
                    }
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Print History //
        public static ErrorResponse GetWorkListHistory(GetWorkListHistoryReq Req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (Req.type == "Custom" || Req.type == string.Empty)
            {
                if (string.IsNullOrEmpty(Req.fromdate) || Req.fromdate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(Req.todate) || Req.todate.TrimStart() == string.Empty)
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

        // Histo worklist Validation //
        public static ErrorResponse GetHistoWorkListDetails(WorkListRequest RequestItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (RequestItem.Type == "Custom" || RequestItem.Type == string.Empty)
            {
                if (string.IsNullOrEmpty(RequestItem.FromDate) || RequestItem.FromDate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(RequestItem.ToDate) || RequestItem.ToDate.TrimStart() == string.Empty)
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

        // Slide preparation Validation //
        public static ErrorResponse GetSlidePrintingDetails(SlidePrintingRequest RequestItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (RequestItem.Type == "Custom" || RequestItem.Type == string.Empty)
            {
                if (string.IsNullOrEmpty(RequestItem.FromDate) || RequestItem.FromDate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(RequestItem.ToDate) || RequestItem.ToDate.TrimStart() == string.Empty)
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

        public static ErrorResponse SaveSlidePrintingDetails(SlidePrintPatientDetailsResponse slidePrintPatientDetails)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (slidePrintPatientDetails.specimens != null && slidePrintPatientDetails.specimens.Count > 0)
            {
                var duplicateSpecimenTypes = slidePrintPatientDetails.specimens
                    .GroupBy(s => s.SpecimenType)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                foreach (var specimen in slidePrintPatientDetails.specimens)
                {
                    if (duplicateSpecimenTypes.Contains(specimen.SpecimenType))
                    {
                        specimen.SpecimenType = 0;
                        errors.Add("Specimen Type Already Exists");
                    }
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
        public static ErrorResponse GetBarcodePrintDetailsValidation(BarcodePrintRequest RequestItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if(RequestItem.RequestType == string.Empty || RequestItem.RequestType == "O")
            {
                if (RequestItem.OrderListNo.ToString() == "0" || RequestItem.OrderListNo.ToString() == string.Empty)
                {
                    errors.Add("Order List Number is Mandatory");
                }
            }
            else if(RequestItem.RequestType == "S")
            {
                if (RequestItem.SampleNo.ToString() == "0" || RequestItem.SampleNo.ToString() == string.Empty)
                {
                    errors.Add("Sample / Specimen Number is Mandatory");
                }
            }
            
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
    }
}