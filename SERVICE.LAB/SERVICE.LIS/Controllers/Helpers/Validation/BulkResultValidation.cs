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
using Microsoft.AspNetCore.Mvc;
using DEV.Model.PatientInfo;
using DEV.Model.Sample;
using System.Text;
using StackExchange.Redis;

namespace DEV.API.SERVICE.Controllers
{
    public class BulkResultValidation
    {
        // Bulk Result (Entry & Validation) //
        public static ErrorResponse GetBulkResultEtry(analyserrequestresult req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.gentype == "Custom" || req.gentype == string.Empty)
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

        // Bulk Result (Entry & Validation) //
        public static ErrorResponse SaveBulkResultEtry(List<objbulkresult> req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            StringBuilder orderlistnos = new StringBuilder();

            if (req == null || req.Count == 0)
            {
                errors.Add("No data provided.");
            }
            else
            {
                int ischeckedavail = 0;
                int isresultempty = 0;

                foreach (var bulkresult in req)
                {
                    foreach (var detail in bulkresult.lstbulkresultdetails)
                    {
                        if (detail.ischecked == true || detail.isrerun == true || detail.isrecollect == true || detail.isrecheck == true)
                        {
                            ischeckedavail = 1;
                            break;
                        }
                    }
                }

                if (ischeckedavail == 0)
                {
                    errors.Add("Please select any test.");
                }
                else
                {
                    foreach (var bulkresult in req)
                    {
                        int patientno = 0;

                        foreach (var detail in bulkresult.lstbulkresultdetails)
                        {
                            if (patientno != detail.patientno)
                            {
                                patientno = detail.patientno;

                                var patientlstsubdataschecked = bulkresult.lstbulkresultdetails
                                    .Where(d => d.patientno == patientno && d.ischecked == true).ToList();
                                var patientlstsubdatas = bulkresult.lstbulkresultdetails
                                    .Where(d => d.patientno == patientno && d.ischecked == true && !string.IsNullOrEmpty(d.result)).ToList();

                                if (!patientlstsubdatas.Any() && patientlstsubdataschecked.Any())
                                {
                                    isresultempty = 1;
                                    break;
                                }
                            }
                        }
                    }

                    if (isresultempty > 0)
                    {
                        errors.Add("Please enter the result(s).");
                    }
                }
            }

            if (req.Count > 0)
            {
                foreach (var bulkresult in req)
                {
                    foreach (var detail in bulkresult.lstbulkresultdetails)
                    {
                        if (detail.isformulaparameter == true)
                        {
                            //if (!FunCalFormula(visit, order, detail, errors))
                            //{
                            //    errors.Add("Formula calculation is invalid");
                            //}
                        }
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

        public static bool FunCalFormula(lstvisit v, lstorderlist ol, lstorderdetail od, List<string> errors)
        {
            if (od.isformulaparameter == true)
            {
                foreach (var param in od.formulaparameterjson)
                {
                    int formulaserviceno = param.serviceno;
                    string formulaservicetype = param.servicetype;

                    int ftindex = 0;
                    if (formulaservicetype == "T")
                    {
                        ftindex = ol.lstorderdetail.FindIndex(service => service.testno == formulaserviceno);
                    }
                    else if (formulaservicetype == "S")
                    {
                        ftindex = ol.lstorderdetail.FindIndex(service => service.subtestno == formulaserviceno);
                    }

                    var formulaDetail = ol.lstorderdetail[ftindex];
                    var formulajson = formulaDetail.formulajson;
                    int decimalpoint = formulaDetail.decimalpoint;
                    bool isroundoff = formulaDetail.isroundoff;
                    bool isresval = false;
                    bool isdcresval = true;
                    decimal a = 0;
                    decimal val = 0;

                    foreach (var formula in formulajson)
                    {
                        val = 0;
                        if (formula.value == 0)
                        {
                            var plst = new List<lstorderdetail>();
                            if (formula.parameterservicetype == "T")
                            {
                                plst = ol.lstorderdetail.Where(service => service.testno == formula.parameterserviceno).ToList();
                            }
                            else if (formula.parameterservicetype == "S")
                            {
                                plst = ol.lstorderdetail.Where(service => service.subtestno == formula.parameterserviceno).ToList();
                            }
                            if (decimal.TryParse(plst[0].result, out var parsedResult))
                            {
                                val = parsedResult;
                            }
                            else
                            {
                                val = 0;
                            }
                            if (val > 0 && isresval == false)
                            {
                                isresval = true;
                            }
                        }
                        else
                        {
                            val = formula.value;
                        }

                        if (val == 0)
                        {
                            isdcresval = false;
                        }

                        if (string.IsNullOrEmpty(formula.foperator))
                        {
                            a = val;
                        }
                        else
                        {
                            switch (formula.foperator)
                            {
                                case "+":
                                    a += val;
                                    break;
                                case "-":
                                    a -= val;
                                    break;
                                case "*":
                                    a *= val;
                                    break;
                                case "/":
                                    if (val > 0)
                                    {
                                        a /= val;
                                    }
                                    else
                                    {
                                        a = 0;
                                    }
                                    break;
                                case "=":
                                    if (a > val)
                                    {
                                        errors.Add("Invalid formula input(s)");
                                        od.result = "";
                                        od.resultflag = "";
                                        return FunCalFormula(v, ol, od, errors);
                                    }
                                    else if (a < val && isdcresval == true)
                                    {
                                        errors.Add("Invalid formula input(s)");
                                        od.result = "";
                                        od.resultflag = "";
                                        return FunCalFormula(v, ol, od, errors);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}