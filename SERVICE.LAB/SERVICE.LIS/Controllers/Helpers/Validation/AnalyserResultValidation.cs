using Service.Model;
using Service.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Service.API.SERVICE.Controllers
{
    public class AnalyserResultValidation
    {
        // Analyser Result Entry Validation //
        public static ErrorResponse GetAnalyserResult(AnalyserRequestresult req)
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
        public static ErrorResponse InsertAnalyserResult(Objresult req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            StringBuilder orderlistnos = new StringBuilder();

            // Formula Validation //
            if (req.Lstvisit.Count > 0)
            {
                foreach (var visit in req.Lstvisit)
                {
                    foreach (var order in visit.Lstorderlist)
                    {
                        foreach (var detail in order.Lstorderdetail)
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
            foreach (var vst in req.Lstvisit)
            {
                foreach (var ol in vst.Lstorderlist)
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

                    var lstw = vst.Lstorderlist.Where(s => s.ischecked == true).ToList();
                    if (lstw == null || lstw.Count == 0)
                    {
                        isemptyresulttrytosave++;
                    }

                    foreach (var od in ol.Lstorderdetail)
                    {
                        if (od.resulttype != "CU" && od.resulttype != "TE")
                        {
                            var lst = vst.Lstorderlist.Where(s => s.ischecked == true).ToList();
                            var lst1 = vst.Lstorderlist.Where(s => s.risrerun == true).ToList();
                            var lst2 = vst.Lstorderlist.Where(s => s.risrecollect == true).ToList();
                            var lst3 = vst.Lstorderlist.Where(s => s.risrecheck == true).ToList();
                            var lst4 = vst.Lstorderlist.Where(s => s.isnoresult == true).ToList();

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

                if (isemptyresulttrytosave == req.Lstvisit.Count)
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

        public static bool FunCalFormula(Lstvisit v, Lstorderlist ol, Lstorderdetail od, List<string> errors)
        {
            if (od.isformulaparameter == true)
            {
                foreach (var param in od.Formulaparameterjson)
                {
                    int formulaserviceno = param.serviceno;
                    string formulaservicetype = param.servicetype;

                    int ftindex = 0;
                    if (formulaservicetype == "T")
                    {
                        ftindex = ol.Lstorderdetail.FindIndex(service => service.testno == formulaserviceno);
                    }
                    else if (formulaservicetype == "S")
                    {
                        ftindex = ol.Lstorderdetail.FindIndex(service => service.subtestno == formulaserviceno);
                    }

                    var formulaDetail = ol.Lstorderdetail[ftindex];
                    var Formulajson = formulaDetail.Formulajson;
                    int decimalpoint = formulaDetail.decimalpoint;
                    bool isroundoff = formulaDetail.isroundoff;
                    bool isresval = false;
                    bool isdcresval = true;
                    decimal a = 0;
                    decimal val = 0;

                    foreach (var formula in Formulajson)
                    {
                        val = 0;
                        if (formula.value == 0)
                        {
                            var plst = new List<Lstorderdetail>();
                            if (formula.parameterservicetype == "T")
                            {
                                plst = ol.Lstorderdetail.Where(service => service.testno == formula.parameterserviceno).ToList();
                            }
                            else if (formula.parameterservicetype == "S")
                            {
                                plst = ol.Lstorderdetail.Where(service => service.subtestno == formula.parameterserviceno).ToList();
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