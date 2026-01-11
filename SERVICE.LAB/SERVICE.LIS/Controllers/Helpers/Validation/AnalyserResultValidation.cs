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

namespace DEV.API.SERVICE.Controllers
{
    public class AnalyserResultValidation
    {
        // Analyser Result Entry Validation //
        public static ErrorResponse GetAnalyserResult(analyserrequestresult req)
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

        // Insert Analyser Result Entry //
        public static ErrorResponse InsertAnalyserResult(objresult req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            StringBuilder orderlistnos = new StringBuilder();

            // Formula Validation //
            if (req.lstvisit.Count > 0)
            {
                foreach (var visit in req.lstvisit)
                {
                    foreach (var order in visit.lstorderlist)
                    {
                        foreach (var detail in order.lstorderdetail)
                        {
                            if (detail.isformulaparameter == true)
                            {
                                if (!FunCalFormula(visit, order, detail, errors))
                                {
                                    errors.Add("Formula calculation is invalid");
                                }
                            }
                        }
                    }
                }
            }

            // Analyser Result Entry Save Validation //
            var isAbnormalorCriticalResultsAvail = 0;
            var isemptyresulttrytosave = 0;
            foreach (var vst in req.lstvisit)
            {
                foreach (var ol in vst.lstorderlist)
                {
                    if (ol.ischecked == true)
                    {
                        if (orderlistnos.Length == 0)
                        {
                            orderlistnos.Append(ol.orderlistno);
                        }
                        else
                        {
                            orderlistnos.Append(",").Append(ol.orderlistno);
                        }
                    }

                    if ((ol.risrerun == true || ol.risrecollect == true || ol.risrecheck == true) && req.action != "SV")
                    {
                        errors.Add("You checked Rerun Or Recollect Or Recheck. Please uncheck and then take preview or save draft");
                        break;
                    }

                    if (ol.risrerun == true || ol.risrecollect == true || ol.risrecheck == true)
                    {
                        ol.ischecked = false;
                    }

                    var lstw = vst.lstorderlist.Where(s => s.ischecked == true).ToList();
                    if (lstw == null || lstw.Count == 0)
                    {
                        isemptyresulttrytosave++;
                    }

                    foreach (var od in ol.lstorderdetail)
                    {
                        if (od.resulttype != "CU" && od.resulttype != "TE")
                        {
                            var lst = vst.lstorderlist.Where(s => s.ischecked == true).ToList();
                            var lst1 = vst.lstorderlist.Where(s => s.risrerun == true).ToList();
                            var lst2 = vst.lstorderlist.Where(s => s.risrecollect == true).ToList();
                            var lst3 = vst.lstorderlist.Where(s => s.risrecheck == true).ToList();
                            var lst4 = vst.lstorderlist.Where(s => s.isnoresult == true).ToList();

                            //if (lst.Count == 0 && lst1.Count == 0 && lst2.Count == 0 && lst3.Count == 0 && (lst4 == null || (lst4.Count == 0 && lst4 != null)))
                            //{
                            //    errors.Add("Enter the result");
                            //    break;
                            //}

                            if ((ol.ischecked == true && od.isnonmandatory == false || (ol.ischecked == true && od.isnonmandatory == false && od.noresult == false))
                                && string.IsNullOrEmpty(od.result) || od.result.TrimStart() == string.Empty)
                            {
                                errors.Add("Enter result for mandatory test(s).");
                                break;
                            }
                            else if (od.resulttype == "NU" && float.Parse(od.result) < 0)
                            { }
                            else if (!string.IsNullOrEmpty(od.resultflag))
                            {
                                if (od.resultflag == "CL" || od.resultflag == "CH")
                                {
                                    isAbnormalorCriticalResultsAvail = isAbnormalorCriticalResultsAvail + 1;
                                }
                            }
                        }
                    }
                }

                if (isemptyresulttrytosave == req.lstvisit.Count)
                {
                    errors.Add("Enter the result");
                    break;
                }

                if (req.pagecode == "PCRA" && isAbnormalorCriticalResultsAvail > 0 && isAbnormalorCriticalResultsAvail.ToString().Trim() != string.Empty)
                {
                    if (!Confirm("Proceed with abnormal/critical result(s)?"))
                    {
                        return errorResponse;
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
        private static bool Confirm(string message)
        {
            Console.WriteLine(message + " [y/n]");
            var response = Console.ReadLine();
            return response?.ToLower() == "y";
        }
    }
}