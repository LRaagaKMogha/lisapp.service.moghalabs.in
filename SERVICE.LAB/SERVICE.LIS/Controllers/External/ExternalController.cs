using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Service.Model.Integration;
using System.Runtime.ConstrainedExecution;
using System.Data;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient.Server;
using SixLabors.ImageSharp;
using Dev.Repository;
using StackExchange.Redis;
using RtfPipe;
using Didstopia.PDFSharp.Drawing.BarCodes;

namespace DEV.API.SERVICE.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        private readonly IExternalRepository _IExternalRepository;
        public ExternalController(IExternalRepository IExternalRepository)
        {
            _IExternalRepository = IExternalRepository;
        }
        [HttpPost]
        [Route("api/External/PostResults")]
        public int PostResults(ExternalResultDTO results)
        {
            int result = 0;
            try
            {
                result = _IExternalRepository.PostResult(results);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalController.PostResults/BarcodeNo-" + results.BarcodeNo, ExceptionPriority.High, ApplicationType.APPSERVICE, results.VenueNo, results.VenueBranchNo, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/External/bulkPostResults")]
        public List<ExternalbulkResultResponseDTO> bulkPostResults(List<ExternalBulkResultDTO> results)
        {
            List<ExternalbulkResultResponseDTO> output = new List<ExternalbulkResultResponseDTO>();
            try
            {
                foreach (var item in results)
                {
                    ExternalResultDTO data = new ExternalResultDTO();
                    data.MachineId = item.MId;
                    data.VenueNo = item.vbNo;
                    data.VenueBranchNo = item.VNo;

                    foreach (var subitem in item.lsttest)
                    {
                        data.BarcodeNo = subitem.bno;
                        data.TestSubtesttNo = subitem.tsn;
                        data.Result = subitem.rt;
                        data.Base64 = subitem.base64;
                        data.Comment = subitem.coment;
                        data.Ivalue = subitem.ival;
                        data.Hvalue = subitem.hval;
                        data.Lvalue = subitem.lval;
                        data.IsReRun = subitem.isrerun;
                        int result = _IExternalRepository.PostResult(data);

                        //try
                        //{
                        //    ExternalResultCalculationRequest newReq = new ExternalResultCalculationRequest();
                        //    newReq.BarcodeNo = subitem.bno;
                        //    newReq.TestSubtesttNo = subitem.tsn;
                        //    newReq.Result = subitem.rt;
                        //    newReq.MachineId = data.MachineId;
                        //    newReq.VenueNo = data.VenueNo;
                        //    newReq.VenueBranchNo = data.VenueBranchNo;
                        //    var response = _IExternalRepository.CheckFormulaIsAvailable_ForCalculation(newReq);

                        //    if (response == true)
                        //    {
                        //        List<ExternalResultCalculation> objCalc = new List<ExternalResultCalculation>();
                        //        objCalc = _IExternalRepository.GetExternalFormulaOrderDetails(newReq);

                        //        ExternalResultDTO dataSub = new ExternalResultDTO();
                        //        dataSub.MachineId = item.MId;
                        //        dataSub.VenueNo = item.vbNo;
                        //        dataSub.VenueBranchNo = item.VNo;

                        //        objCalc = objCalc.Where(BarCode => BarCode.BarcodeNo == newReq.BarcodeNo).ToList();
                        //        var od = objCalc.Where(t => t.TestSubtesttNo == newReq.TestSubtesttNo).ToList();

                        //        int res = Calculator(objCalc, od);

                        //        foreach (var dataResult in objCalc)
                        //        {
                        //            if (dataResult.result != "" && dataResult.result != "0.0" && dataResult.result.ToLower() != "nan" &&
                        //                (dataResult.istformula == true || dataResult.issformula == true))
                        //            {
                        //                dataSub.BarcodeNo = dataResult.BarcodeNo;
                        //                dataSub.TestSubtesttNo = dataResult.TestSubtesttNo;
                        //                dataSub.Result = dataResult.result;
                        //                dataSub.Comment = dataResult.Comment;
                        //                dataSub.Base64 = "";
                        //                dataSub.Hvalue = "";
                        //                dataSub.Ivalue = "";
                        //                dataSub.Lvalue = "";
                        //                dataSub.IsReRun = false;
                        //                dataSub.IsCalculationInput = 1;
                        //                int result1 = _IExternalRepository.PostResult(dataSub);
                        //            }
                        //        }

                        //    }
                        //}
                        //catch (Exception)
                        //{
                        //}

                        ExternalbulkResultResponseDTO outitem = new ExternalbulkResultResponseDTO();
                        outitem.status = result;
                        outitem.bno = subitem.bno;
                        outitem.tsn = subitem.tsn;
                        output.Add(outitem);

                        //_IExternalRepository.ValidateAutoApporval(data);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalController.bulkPostResults", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return output;
        }
        [HttpPost]
        [Route("api/External/bulkPostCultureResults")]
        public List<ExternalbulkCultureResponseDTO> bulkPostCultureResults(List<ExternalBulkCultureResultDTO> results)
        {
            List<ExternalbulkCultureResponseDTO> output = new List<ExternalbulkCultureResponseDTO>();
            try
            {
                foreach (var item in results)
                {
                    ExternalBulkCultureResultDTO data = new ExternalBulkCultureResultDTO();
                    data.Mid = item.Mid;
                    data.Vno = item.Vno;
                    data.Vbno = item.Vbno;
                    data.Bno = item.Bno;
                    data.Orderlsitno = item.Orderlsitno;
                    data.Servno = item.Servno;
                    data.Samp = item.Samp;
                    data.lstCultureorg = new List<BulkCultureOrgResultDTO>();
                    foreach (var orgitem in item.lstCultureorg)
                    {
                        BulkCultureOrgResultDTO obj = new BulkCultureOrgResultDTO();
                        obj.Ono = orgitem.Ono;
                        obj.Ocode = orgitem.Ocode;
                        obj.Oname = orgitem.Oname;
                        obj.Base64 = orgitem.Base64;
                        obj.lstCulturedrug = new List<BulkCultureDrugResultDTO>();
                        foreach (var drgitem in orgitem.lstCulturedrug)
                        {
                            BulkCultureDrugResultDTO dobj = new BulkCultureDrugResultDTO();
                            dobj.Dno = drgitem.Dno;
                            dobj.Dcode = drgitem.Dcode;
                            dobj.Dname = drgitem.Dname;
                            dobj.Dmic = drgitem.Dmic;
                            dobj.Dintrp = drgitem.Dintrp;
                            dobj.DintrpVal = drgitem.DintrpVal;
                            obj.lstCulturedrug.Add(dobj);
                        }
                        data.lstCultureorg.Add(obj);
                    }
                    int result = _IExternalRepository.PostCultureResult(data);
                    ExternalbulkCultureResponseDTO outitem = new ExternalbulkCultureResponseDTO();
                    outitem.status = result;
                    outitem.bno = item.Bno;
                    outitem.tsn = item.Servno != null ? item.Servno.ToString() : "0";
                    output.Add(outitem);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalController.bulkPostCultureResults", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return output;
        }

        private int Calculator(List<ExternalResultCalculation> req, List<ExternalResultCalculation> od)
        {
            int result = -1;
            try
            {
                var formulaTest = "";
                foreach (var orderDetails in req)
                {
                    if (orderDetails.istformula == true || orderDetails.issformula == true)
                    {

                        int decimalPoint = orderDetails.decimalpoint;
                        bool isRoundOff = orderDetails.isroundoff;
                        double a = 0;
                        double val = 0;
                        var rowval = "";
                        int indx = orderDetails.formulajson.Count();

                        if (indx > 0)
                        {
                            formulaTest = "(";
                        }

                        for (var i = 0; i < orderDetails.formulajson.Count(); i++)
                        {
                            var formulaserviceno = orderDetails.formulajson[i].parameterserviceno;
                            var formulaservicetype = orderDetails.formulajson[i].parameterservicetype;

                            //----Egfr
                            var lstEgfr = _IExternalRepository.GetEGFRList(1, 1, "EGFRTEST");
                            var iseGFRval = lstEgfr.Where(s => s.CommonNo == formulaserviceno);
                            if (iseGFRval != null && iseGFRval.Count() > 0)
                            {
                                var index = lstEgfr.FindIndex(s => s.CommonNo == formulaserviceno);
                                var ftindex = 0;
                                var finid = 0;
                                if (formulaservicetype == "T")
                                {
                                    ftindex = req.FindIndex(t => t.testno == formulaserviceno);
                                    if (ftindex >= 0)
                                    {
                                        finid = ftindex;
                                    }
                                }
                                else if (formulaservicetype == "S")
                                {
                                    ftindex = req.FindIndex(t => t.subtestno == formulaserviceno);
                                    if (ftindex >= 0)
                                    {
                                        finid = ftindex;
                                    }
                                }
                                var decimalpoint = od[0].decimalpoint;
                                var egfrreult = req[ftindex].result;
                                if (egfrreult != null && egfrreult != "")
                                {
                                    var formattedResult = "0";
                                    try
                                    {
                                        var eval = double.Parse(req[ftindex].result);
                                        var resultdouble = Calculate(orderDetails.gender, eval, orderDetails.age);

                                        formattedResult = resultdouble.ToString($"F{0}");// double.Parse(resultdouble.ToString($"F{decimalpoint}"));

                                        if (formattedResult != "0")
                                        {
                                            orderDetails.result = formattedResult;
                                            result = 1;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        orderDetails.result = "";
                                        result = -1;
                                    }

                                }
                                return result;
                            }
                            //-----END
                            else
                            {
                                val = 0;
                                if (orderDetails.formulajson[i].value == 0)
                                {
                                    var plst = new List<ExternalResultCalculation>();
                                    if (orderDetails.formulajson[i].parameterservicetype == "T")
                                    {
                                        plst = req.Where(service => service.testno == orderDetails.formulajson[i].parameterserviceno).ToList();
                                    }
                                    else if (orderDetails.formulajson[i].parameterservicetype == "S")
                                    {
                                        plst = req.Where(service => service.subtestno == orderDetails.formulajson[i].parameterserviceno).ToList();
                                    }
                                    if (plst != null && plst.Count() > 0)
                                    {
                                        if (plst[0].result == "")
                                        {
                                            rowval = "0";
                                        }
                                        else
                                        {
                                            val = double.Parse((plst[0].result.Replace(">", "")).Replace("<", "")); ;
                                        }
                                    }
                                    else
                                    {
                                        val = 0;
                                    }
                                }
                                else
                                {
                                    val = double.Parse(orderDetails.formulajson[i].value.ToString());
                                }

                                if (orderDetails.formulajson[i].foperator == "")
                                {
                                    if (val != 0)
                                        formulaTest += orderDetails.formulajson[i].foperator + val;
                                    else
                                        formulaTest += orderDetails.formulajson[i].foperator + rowval;
                                }
                                else if (orderDetails.formulajson[i].foperator == "+")
                                {
                                    if (val != 0)
                                        formulaTest += orderDetails.formulajson[i].foperator + val;
                                    else
                                        formulaTest += orderDetails.formulajson[i].foperator + rowval;
                                }
                                else if (orderDetails.formulajson[i].foperator == "-")
                                {
                                    if (val != 0)
                                        formulaTest += orderDetails.formulajson[i].foperator + val;
                                    else
                                        formulaTest += orderDetails.formulajson[i].foperator + rowval;
                                }
                                else if (orderDetails.formulajson[i].foperator == "*")
                                {
                                    if (val != 0)
                                        formulaTest += orderDetails.formulajson[i].foperator + val;
                                    else
                                        formulaTest += orderDetails.formulajson[i].foperator + rowval;
                                }
                                else if (orderDetails.formulajson[i].foperator == "/")
                                {
                                    if (val != 0)
                                        formulaTest += orderDetails.formulajson[i].foperator + val;
                                    else
                                        formulaTest += orderDetails.formulajson[i].foperator + rowval;
                                }
                                else if (orderDetails.formulajson[i].foperator == "(")
                                {
                                    if (val != 0)
                                        formulaTest += orderDetails.formulajson[i].foperator + val;
                                    else
                                        formulaTest += orderDetails.formulajson[i].foperator + rowval;
                                }
                                else if (orderDetails.formulajson[i].foperator == ")")
                                {
                                    if (val != 0)
                                        formulaTest += orderDetails.formulajson[i].foperator + val;
                                    else
                                        formulaTest += orderDetails.formulajson[i].foperator + rowval;
                                }
                                else if (orderDetails.formulajson[i].foperator == "^")
                                {
                                    if (val != 0)
                                        formulaTest += orderDetails.formulajson[i].foperator + val;
                                    else
                                        formulaTest += orderDetails.formulajson[i].foperator + rowval;
                                }
                                else if (orderDetails.formulajson[i].foperator == "^")
                                {
                                    if (val != 0)
                                        formulaTest += "**" + val; //Math.pow(height, 0.725)
                                    else
                                        formulaTest += "**";
                                    //if (val != 0)
                                    //    formulaTest += orderDetails.formulajson[i].foperator + val;
                                    //else
                                    //    formulaTest += orderDetails.formulajson[i].foperator + rowval;
                                }
                            }
                        }

                        if (indx > 0)
                        {
                            formulaTest += ")";
                        }

                        if (formulaTest != "")
                        {
                            var formattedResult = "0";
                            try
                            {
                                // Step 1: Evaluate the formula string
                                var dataTable = new DataTable();
                                var results = dataTable.Compute(formulaTest, "");

                                // Step 2: Parse the result to a float
                                if (Double.IsInfinity(Convert.ToDouble(results)))
                                {
                                    //formattedResult = "";
                                }
                                else
                                {
                                    var resultAsDouble = Convert.ToDouble(results);
                                    // Step 3: Format the number to a fixed number of decimal places
                                    formattedResult = resultAsDouble.ToString($"F{decimalPoint}");
                                }
                            }
                            catch
                            {
                                //formattedResult = "0";
                                orderDetails.IsCalculationInput = 1;
                                result = -1;
                            }
                            if (formattedResult != "0")
                            {
                                orderDetails.result = formattedResult;
                                orderDetails.IsCalculationInput = 1;
                                result = 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.formula", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        private double Calculate(string sex, double testValue, int age)
        {
            // Default Male values
            double k = 0.9;
            double a = -0.302;
            double last = 1.000;
            double scr = Math.Round(CalculateDivision(testValue, k), 6);

            // Female values
            if (sex == "F")
            {
                k = 0.7;
                a = -0.241;
                last = 1.012;
                scr = Math.Round(CalculateDivision(testValue, k), 6);
            }

            double min = CalculateMin(scr, 1);
            double max = CalculateMax(scr, 1);

            return 142 * Math.Pow(min, a) * Math.Pow(max, -1.2) * Math.Pow(0.9938, age) * last;
        }

        private double CalculateDivision(double value, double divisor)
        {
            return value / divisor;
        }

        private double CalculateMin(double a, double b)
        {
            return Math.Min(a, b);
        }

        private double CalculateMax(double a, double b)
        {
            return Math.Max(a, b);
        }

        private double EvaluateFormula(string formulaTest, int decimalPoint)
        {
            try
            {
                return Math.Round(double.Parse(new DataTable().Compute(formulaTest, null).ToString()), decimalPoint);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        //dif culture 
        [HttpPost]
        [Route("api/External/difbulkPostCultureResults")]
        public List<ExternalDifbulkCultureResponseDTO> difbulkPostCultureResults(List<ExternalDifBulkCultureResultNewDTO> results)
        {
            List<ExternalDifbulkCultureResponseDTO> output = new List<ExternalDifbulkCultureResponseDTO>();
            try
            {
                if (results != null && results.Count > 0)
                {
                    string Mid = results[0].Mid;
                    string Bno = results[0].Bno;
                    int Orderlsitno = results[0].Orderlsitno;
                    int Vno = results[0].Vno;
                    int Vbno = results[0].Vbno;
                    int Servno = results[0].Servno;
                    string Samp = results[0].Samp;
                    ExternalDifBulkCultureResultDTO data = new ExternalDifBulkCultureResultDTO();
                    data.Mid = Mid;
                    data.Vno = Vno;
                    data.Vbno = Vbno;
                    data.Bno = Bno;
                    data.Orderlsitno = Orderlsitno;
                    data.Servno = Servno;
                    data.Samp = Samp;
                    data.lstCultureorg1 = new DifBulkCultureOrgResultDTO();
                    var lstOrg1 = results.Where(d => d.OrgType == "T11").ToList();
                    if (lstOrg1 != null && lstOrg1.Count > 0)
                    {
                        DifBulkCultureOrgResultDTO obj = new DifBulkCultureOrgResultDTO();
                        obj.Ono = lstOrg1[0].Ono;
                        obj.Ocode = lstOrg1[0].Ocode;
                        obj.Oname = lstOrg1[0].Oname;
                        obj.Base64 = lstOrg1[0].Base64;
                        obj.lstCulturedrug = new List<DifBulkCultureDrugResultDTO>();
                        foreach (var drgitem in lstOrg1)
                        {
                            DifBulkCultureDrugResultDTO dobj = new DifBulkCultureDrugResultDTO();
                            dobj.Dno = drgitem.Dno;
                            dobj.Dcode = drgitem.Dcode;
                            dobj.Dname = drgitem.Dname;
                            dobj.Dmic = drgitem.Dmic;
                            dobj.Dintrp = drgitem.Dintrp;
                            dobj.DintrpVal = drgitem.DintrpVal;
                            obj.lstCulturedrug.Add(dobj);
                        }
                        data.lstCultureorg1 = obj;
                    }
                    var lstOrg2 = results.Where(d => d.OrgType == "T12").ToList();
                    if (lstOrg2 != null && lstOrg2.Count > 0)
                    {
                        DifBulkCultureOrgResultDTO obj = new DifBulkCultureOrgResultDTO();
                        obj.Ono = lstOrg2[0].Ono;
                        obj.Ocode = lstOrg2[0].Ocode;
                        obj.Oname = lstOrg2[0].Oname;
                        obj.Base64 = lstOrg2[0].Base64;
                        obj.lstCulturedrug = new List<DifBulkCultureDrugResultDTO>();
                        foreach (var drgitem in lstOrg2)
                        {
                            DifBulkCultureDrugResultDTO dobj = new DifBulkCultureDrugResultDTO();
                            dobj.Dno = drgitem.Dno;
                            dobj.Dcode = drgitem.Dcode;
                            dobj.Dname = drgitem.Dname;
                            dobj.Dmic = drgitem.Dmic;
                            dobj.Dintrp = drgitem.Dintrp;
                            dobj.DintrpVal = drgitem.DintrpVal;
                            obj.lstCulturedrug.Add(dobj);
                        }
                        data.lstCultureorg2 = obj;
                    }
                    var lstOrg3 = results.Where(d => d.OrgType == "T13").ToList();
                    if (lstOrg3 != null && lstOrg3.Count > 0)
                    {
                        DifBulkCultureOrgResultDTO obj = new DifBulkCultureOrgResultDTO();
                        obj.Ono = lstOrg3[0].Ono;
                        obj.Ocode = lstOrg3[0].Ocode;
                        obj.Oname = lstOrg3[0].Oname;
                        obj.Base64 = lstOrg3[0].Base64;
                        obj.lstCulturedrug = new List<DifBulkCultureDrugResultDTO>();
                        foreach (var drgitem in lstOrg3)
                        {
                            DifBulkCultureDrugResultDTO dobj = new DifBulkCultureDrugResultDTO();
                            dobj.Dno = drgitem.Dno;
                            dobj.Dcode = drgitem.Dcode;
                            dobj.Dname = drgitem.Dname;
                            dobj.Dmic = drgitem.Dmic;
                            dobj.Dintrp = drgitem.Dintrp;
                            dobj.DintrpVal = drgitem.DintrpVal;
                            obj.lstCulturedrug.Add(dobj);
                        }
                        data.lstCultureorg3 = obj;
                    }
                    var lstOrg4 = results.Where(d => d.OrgType == "T14").ToList();
                    if (lstOrg4 != null && lstOrg4.Count > 0)
                    {
                        DifBulkCultureOrgResultDTO obj = new DifBulkCultureOrgResultDTO();
                        obj.Ono = lstOrg4[0].Ono;
                        obj.Ocode = lstOrg4[0].Ocode;
                        obj.Oname = lstOrg4[0].Oname;
                        obj.Base64 = lstOrg4[0].Base64;
                        obj.lstCulturedrug = new List<DifBulkCultureDrugResultDTO>();
                        foreach (var drgitem in lstOrg4)
                        {
                            DifBulkCultureDrugResultDTO dobj = new DifBulkCultureDrugResultDTO();
                            dobj.Dno = drgitem.Dno;
                            dobj.Dcode = drgitem.Dcode;
                            dobj.Dname = drgitem.Dname;
                            dobj.Dmic = drgitem.Dmic;
                            dobj.Dintrp = drgitem.Dintrp;
                            dobj.DintrpVal = drgitem.DintrpVal;
                            obj.lstCulturedrug.Add(dobj);
                        }
                        data.lstCultureorg4 = obj;
                    }
                    var lstOrg5 = results.Where(d => d.OrgType == "T15").ToList();
                    if (lstOrg5 != null && lstOrg5.Count > 0)
                    {
                        DifBulkCultureOrgResultDTO obj = new DifBulkCultureOrgResultDTO();
                        obj.Ono = lstOrg5[0].Ono;
                        obj.Ocode = lstOrg5[0].Ocode;
                        obj.Oname = lstOrg5[0].Oname;
                        obj.Base64 = lstOrg5[0].Base64;
                        obj.lstCulturedrug = new List<DifBulkCultureDrugResultDTO>();
                        foreach (var drgitem in lstOrg5)
                        {
                            DifBulkCultureDrugResultDTO dobj = new DifBulkCultureDrugResultDTO();
                            dobj.Dno = drgitem.Dno;
                            dobj.Dcode = drgitem.Dcode;
                            dobj.Dname = drgitem.Dname;
                            dobj.Dmic = drgitem.Dmic;
                            dobj.Dintrp = drgitem.Dintrp;
                            dobj.DintrpVal = drgitem.DintrpVal;
                            obj.lstCulturedrug.Add(dobj);
                        }
                        data.lstCultureorg4 = obj;
                    }

                    int result = _IExternalRepository.PostDifCultureResult(data);
                    ExternalDifbulkCultureResponseDTO outitem = new ExternalDifbulkCultureResponseDTO();
                    outitem.status = result;
                    outitem.bno = Bno;
                    outitem.tsn = Servno != null ? "T" + Servno.ToString() : "T0";
                    output.Add(outitem);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalController.difbulkPostCultureResults", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return output;
        }
        //
    }

}