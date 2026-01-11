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
using Service.Model.PatientInfo;
using Service.Model.Sample;
using System.Text;
using RtfPipe.Tokens;

namespace DEV.API.SERVICE.Controllers
{
    public class ResultAckValidation
    {
        // Result Acknowldgement Validation //
        public static ErrorResponse GetResultACK(GetSampleOutsourceRequest RequestItem)
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

        // Insert Result Acknowldgement //
        public static ErrorResponse CreateResultACK(List<CreateSampleOutSourceRequest> createOutSource)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (createOutSource.Count > 0)
            {
                //foreach (var resultAck in createOutSource)
                //{
                //    if (resultAck.directApproval == true)
                //    {
                //        errors.Add("Please uploaded the file for direct approval record");
                //    }
                //}
            }
            else
            {
                errors.Add("Select Any one patient to save");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Result Recall Validation //
        public static ErrorResponse InsertRecall(objrecall req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            List <lstRecallServicves> objRC = new List<lstRecallServicves>();
            objRC = req.lstRecallServicves.Where(x => x.isChecked == true).ToList();

            if(objRC.Count <= 0)
            {
                errors.Add("Select at least one service");
            }

            // Check for recall remarks
            foreach (var service in objRC)
            {
                if (service.isChecked == true && string.IsNullOrWhiteSpace(service.comments))
                {
                    errors.Add($"Enter the recall remarks for {service.serviceName}");
                    break;
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Copy Lab Results Validation //
        public static ErrorResponse GetVisitMerge(VisitMergeRequest req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.fromvisitno < 0)
            {
                errors.Add("Please select the from lab accession no");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        public static ErrorResponse SaveVisitMerge(SaveResultforVisitMergeResponse req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.lstResultforVisitMerge != null && req.lstResultforVisitMerge.Count > 0)
            {
                var mergedData = req.lstResultforVisitMerge.Where(d => d.ismerged == true).ToList();
                if (mergedData != null && mergedData.Count < 0)
                {
                    errors.Add("No data found");
                }
            }
            else
            {
                errors.Add("Not allowed empty save");
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