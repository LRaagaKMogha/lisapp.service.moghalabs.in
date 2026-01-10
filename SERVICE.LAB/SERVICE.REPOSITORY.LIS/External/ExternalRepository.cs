using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Serilog;

namespace Dev.Repository
{
    public class ExternalRepository : IExternalRepository
    {
        private IConfiguration _config;
        public ExternalRepository(IConfiguration config) { _config = config; }

        public int PostResult(ExternalResultDTO results)
        {
            int result = 0;
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _MachineId = new SqlParameter("MachineId", results.MachineId);
                    var _BarcodeNo = new SqlParameter("BarcodeNo", results.BarcodeNo);
                    var _TestSubtesttNo = new SqlParameter("TestSubtesttNo", results.TestSubtesttNo);
                    var _Result = new SqlParameter("Result", results.Result);
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", results.VenueBranchNo);
                    var _Comment = new SqlParameter("Comment", results.Comment);
                    var _Ivalue = new SqlParameter("Ivalue", results.Ivalue);
                    var _Hvalue = new SqlParameter("Hvalue", results.Hvalue);
                    var _Lvalue = new SqlParameter("Lvalue", results.Lvalue);
                    var _Rerun = new SqlParameter("IsReRun", results.IsReRun);
                    var _IsCalculationInput = new SqlParameter("IsCalculationInput", results.IsCalculationInput);
                    var objresult = context.InsertExternalResult.FromSqlRaw(
                         "Execute dbo.pro_InsertExternalResults @MachineId,@BarcodeNo,@TestSubtesttNo,@Result,@VenueNo,@VenueBranchNo,@Comment,@Ivalue,@Hvalue,@Lvalue,@IsReRun,@IsCalculationInput",
                      _MachineId, _BarcodeNo, _TestSubtesttNo, _Result, _VenueNo, _VenueBranchNo, _Comment, _Ivalue, _Hvalue, _Lvalue, _Rerun, _IsCalculationInput).ToList();

                    var rtnresult = objresult[0].Status;

                    if (rtnresult >= 1)
                    {
                        result = 1;
                    }
                    if (rtnresult > 1)
                    {
                        CreateTemplateResult(rtnresult, results.VenueNo, results.VenueBranchNo);
                    }
                    //image handling
                    if (!string.IsNullOrEmpty(results.Base64))
                    {
                        imagehandlingfrommachine(results);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalRepository.PostResult/BarcodeNo-" + results.BarcodeNo, ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, 0);
            }
            return result;
        }

        public int CreateTemplateResult(int PatientResultTemplateNo, int VenueNo, int VenueBranchNo)
        {
            int result = 0;
            CreateTemplateResultDTO obj = new CreateTemplateResultDTO();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientResultTemplateNo = new SqlParameter("PatientResultTemplateNo", PatientResultTemplateNo);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var objresult = context.GetExternalTemplateResult.FromSqlRaw(
                         "Execute dbo.pro_GetExternalTemplateResult @PatientResultTemplateNo,@VenueNo,@VenueBranchNo",
                      _PatientResultTemplateNo, _VenueNo, _VenueBranchNo).ToList();

                    obj = objresult[0];
                    //---------------------
                    if (obj.orderlistno > 1 && obj.templateno > 0 && obj.serviceno > 0)
                    {
                        string templatetext = "";
                        objAppSettingResponse = new AppSettingResponse();
                        string AppMasterFilePath = "MasterFilePath";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                        string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : ""; // _config.GetConnectionString(ConfigKeys.MasterFilePath);
                        path = path + VenueNo.ToString() + "/Template/" + obj.templateno.ToString() + ".ym";
                        if (File.Exists(path))
                        {
                            templatetext = File.ReadAllText(path).ToString();
                        }
                        objAppSettingResponse = new AppSettingResponse();
                        string AppTransTemplateFilePath = "TransTemplateFilePath";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                        string Trpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : ""; // _config.GetConnectionString(ConfigKeys.TransTemplateFilePath);
                        Trpath = Trpath + VenueNo.ToString() + "/" + obj.orderlistno.ToString() + "/";
                        if (!Directory.Exists(Trpath))
                        {
                            Directory.CreateDirectory(Trpath);
                        }
                        string createText = templatetext + Environment.NewLine;
                        File.WriteAllText(Trpath + obj.serviceno.ToString() + ".ym", createText);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalRepository.CreateTemplateResult/PatientResultTemplateNo-" + PatientResultTemplateNo, ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }
        public int imagehandlingfrommachine(ExternalResultDTO results)
        {
            int iOutput = 0;
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                string testno = results.TestSubtesttNo != null && results.TestSubtesttNo != ""? results.TestSubtesttNo :"";
                objAppSettingResponse = new AppSettingResponse();
                string AppMachineImagePath = "MachineImagePath";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMachineImagePath);
                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : ""; // _config.GetConnectionString(ConfigKeys.MachineImagePath);
                objAppSettingResponse = new AppSettingResponse();
                string AppMachineImageSeparation = "MachineImageSeparation";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMachineImageSeparation);
                string separation = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : ""; // _config.GetConnectionString(ConfigKeys.MachineImageSeparation);
                string folderName = results.VenueNo + "\\" + results.VenueBranchNo + "\\" + results.BarcodeNo;
                string finalPath = Path.Combine(path, folderName);
                if (!Directory.Exists(finalPath))
                {
                    Directory.CreateDirectory(finalPath);
                }
                if (results.Base64 != null && results.Base64.Length > 5)
                {
                    string[] images = results.Base64.Split(separation);
                    int f = 0;
                    foreach (var item in images)
                    {
                        string fileName = f.ToString() + testno + ".png";
                        string fullPath = Path.Combine(finalPath, fileName);
                        string base64 = item != null ? item.Split(',')[1] : item;
                        byte[] imageBytes = Convert.FromBase64String(base64);
                        System.IO.File.WriteAllBytes(fullPath, imageBytes);
                        f = f + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalRepository.imagehandlingfrommachine", ExceptionPriority.High, ApplicationType.REPOSITORY,results.VenueNo, results.VenueBranchNo, 0);
            }
            return iOutput;
        }

        public int PostCultureResult(ExternalBulkCultureResultDTO results)
        {
            int result = 0;
            try
            {
                XDocument odxml = new XDocument();
                XElement xorgantitbl = new XElement("organtitbl");
                if (results.lstCultureorg.Count > 0)
                {
                    foreach (var org in results.lstCultureorg)
                    {
                        var druglst = org.lstCulturedrug.Where(d => d.Dintrp != null && d.Dintrp != "").ToList();
                        if (druglst.Count == 0)
                        {
                            XElement xrow = new XElement("row",
                                    new XElement("organismno", org.Ono),
                                    new XElement("organismmccode", org.Ocode),
                                    new XElement("organismname", org.Oname),
                                    new XElement("antibioticno", 0),
                                    new XElement("antibioticmccode", 0),
                                    new XElement("antibioticname", ""),
                                    new XElement("antsequenceno", 0),
                                    new XElement("interp", 0)
                                    );
                            xorgantitbl.Add(xrow);
                        }
                        else
                        {
                            foreach (var ant in druglst)
                            {
                                XElement xrow = new XElement("row",
                                  new XElement("organismno", org.Ono),
                                  new XElement("organismmccode", org.Ocode),
                                  new XElement("organismname", org.Oname),
                                  new XElement("antibioticno", ant.Dno),
                                  new XElement("antibioticcode", ant.Dcode),
                                  new XElement("antibioticmccode", ant.Dmic),
                                  new XElement("antibioticname", ant.Dname),
                                  new XElement("interp", ant.Dintrp),
                                  new XElement("interpvalue", ant.DintrpVal)
                                  );
                                xorgantitbl.Add(xrow);
                            }
                        }
                    }
                }
                odxml.Add(xorgantitbl);
                string organtixml = odxml.ToString();                
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", results.Vno);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", results.Vbno);
                    var _barcode = new SqlParameter("Barcode", results.Bno);
                    var _machineId = new SqlParameter("MachineId", results.Mid);
                    var _orderlistNo = new SqlParameter("OrderlistNo", results.Orderlsitno);
                    var _serviceNo = new SqlParameter("ServiceNo", results.Servno);
                    var _organtixml = new SqlParameter("Organtixml", organtixml);
                    var _sample = new SqlParameter("Sample", results.Samp);
                    var lst = context.InsertCultureInterfaceResult.FromSqlRaw(
                                "Execute dbo.pro_InsertMBInterfaceResults @VenueNo,@VenueBranchNo,@Barcode,@MachineId,@OrderlistNo,@ServiceNo,@Organtixml,@Sample",
                                _venueNo, _venueBranchNo, _barcode, _machineId, _orderlistNo, _serviceNo, _organtixml, _sample).ToList();
                    result = lst[0].Status;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalRepository.PostCultureResult", ExceptionPriority.High, ApplicationType.REPOSITORY, results.Vno, results.Vbno, 0);
            }
            return result;
        }

        public Boolean CheckFormulaIsAvailable_ForCalculation(ExternalResultCalculationRequest req)
        {
            CheckFormulaIsAvailable objResult = new CheckFormulaIsAvailable();
            Boolean result = false;

            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _BarcodeNo = new SqlParameter("BarcodeNo", req.BarcodeNo);
                    var _TestSubtesttNo = new SqlParameter("TestSubtesttNo", req.TestSubtesttNo);
                    var _Result = new SqlParameter("Result", req.Result);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);

                    objResult = context.CheckFormulaIsAvailable_ForCalculation.FromSqlRaw(
                         "Execute dbo.pro_check_visit_having_formula_for_calculation @VenueNo, @VenueBranchNo, @BarcodeNo, @TestSubtesttNo",
                        _VenueNo, _VenueBranchNo, _BarcodeNo, _TestSubtesttNo).AsEnumerable().FirstOrDefault();

                    result = objResult.ReturnValue;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalRepository.PostResult/BarcodeNo-" + req.BarcodeNo, ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return result;
        }

        public List<ExternalResultCalculation> GetExternalFormulaOrderDetails(ExternalResultCalculationRequest req)
        {
            List <ExternalResultCalculation> result = new List<ExternalResultCalculation>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _BarcodeNo = new SqlParameter("BarcodeNo", req.BarcodeNo);
                    var _TestSubtesttNo = new SqlParameter("TestSubtesttNo", req.TestSubtesttNo);
                    var _Result = new SqlParameter("Result", req.Result);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);

                    var rtnResult = context.GetExternalFormulaOrderDetails.FromSqlRaw(
                         "Execute dbo.pro_Get_OrderDetails_FormulaCalculation @VenueNo, @VenueBranchNo, @BarcodeNo, @TestSubtesttNo",
                        _VenueNo, _VenueBranchNo, _BarcodeNo, _TestSubtesttNo).ToList();

                    foreach (var r in rtnResult)
                    {
                        ExternalResultCalculation objExtRslt = new ExternalResultCalculation();
                        objExtRslt.rowno = r.rowno;
                        objExtRslt.BarcodeNo = r.BarcodeNo;
                        objExtRslt.orderlistno = r.orderlistno;
                        objExtRslt.orderdetailsno = r.orderdetailsno;
                        objExtRslt.TestSubtesttNo = r.TestSubtesttNo;
                        objExtRslt.result = r.result;
                        objExtRslt.formulaservicetype = r.formulaservicetype;
                        objExtRslt.formulaserviceno = r.formulaserviceno;
                        objExtRslt.isformulaparameter = r.isformulaparameter;
                        objExtRslt.istformula=r.istformula;
                        objExtRslt.issformula=r.issformula;
                        objExtRslt.decimalpoint = r.decimalpoint;
                        objExtRslt.isroundoff= r.isroundoff;
                        objExtRslt.formulaparameterjson = JsonConvert.DeserializeObject<List<formulaparameterjson>>(r.formulaparameterjson);
                        objExtRslt.formulajson = JsonConvert.DeserializeObject<List<formulajson>>(r.formulajson);
                        objExtRslt.testno = r.testno;
                        objExtRslt.subtestno = r.subtestno;
                        objExtRslt.testtype = r.testtype;
                        objExtRslt.age = r.age;
                        objExtRslt.ageType = r.ageType;
                        objExtRslt.gender = r.gender;
                        objExtRslt.Comment = r.comment;

                        result.Add(objExtRslt);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalRepository.PostResult/BarcodeNo-" + req.BarcodeNo, ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return result;
        }

        public int ValidateAutoApporval(ExternalResultDTO req)
        {
            int result = 0;
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _BarcodeNo = new SqlParameter("BarcodeNo", req.BarcodeNo);
                    var _TestSubtesttNo = new SqlParameter("TestSubtesttNo", req.TestSubtesttNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);

                    var rtnResult = context.GetExternalApprovalResponseDTO.FromSqlRaw(
                         "Execute dbo.pro_InsertResult_Approval @VenueNo, @VenueBranchNo, @BarcodeNo, @TestSubtesttNo",
                        _VenueNo, _VenueBranchNo, _BarcodeNo, _TestSubtesttNo).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalRepository.ValidateAutoApporval/BarcodeNo-" + req.BarcodeNo, ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return result;
        }
        public double GetCalResult(string req)
        {
            double result = 0;
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Type = new SqlParameter("Type", "");
                    var _Formula = new SqlParameter("Formula", req);
                    var objresult = context.InsertExternalResult.FromSqlRaw("Execute dbo.pro_FormulaCalculation @Type,@Formula",_Type, _Formula).ToList();
                    result = result;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalRepository.FormulaCalculation", ExceptionPriority.High, ApplicationType.REPOSITORY, 1, 1, 0);
            }
            return result;
        }

        public int PostDifCultureResult(ExternalDifBulkCultureResultDTO results)
        {
            int result = 0;
            try
            {
                XDocument odxml = new XDocument();
                XElement xorgantitbl = new XElement("organtitbl");
                //organism 1 entry
                if (results.lstCultureorg1 != null && results.lstCultureorg1.Ono > 0)
                {
                    var druglst = results.lstCultureorg1.lstCulturedrug.Where(d => d.Dintrp != null && d.Dintrp != "").ToList();
                    if (druglst.Count == 0)
                    {
                        XElement xrow = new XElement("org1",
                                new XElement("organismno", results.lstCultureorg1.Ono),
                                new XElement("organismmccode", results.lstCultureorg1.Ocode),
                                new XElement("organismname", results.lstCultureorg1.Oname),
                                new XElement("antibioticno", 0),
                                new XElement("antibioticmccode", 0),
                                new XElement("antibioticname", ""),
                                new XElement("antsequenceno", 0),
                                new XElement("interp", ""),
                                new XElement("interpvalue", "")
                                );
                        xorgantitbl.Add(xrow);
                    }
                    else
                    {
                        foreach (var ant in druglst)
                        {
                            XElement xrow = new XElement("org1",
                              new XElement("organismno", results.lstCultureorg1.Ono),
                              new XElement("organismmccode", results.lstCultureorg1.Ocode),
                              new XElement("organismname", results.lstCultureorg1.Oname),
                              new XElement("antibioticno", ant.Dno),
                              new XElement("antibioticcode", ant.Dcode),
                              new XElement("antibioticmccode", ant.Dmic),
                              new XElement("antibioticname", ant.Dname),
                              new XElement("interp", ant.Dintrp),
                              new XElement("interpvalue", ant.DintrpVal)
                              );
                            xorgantitbl.Add(xrow);
                        }
                    }
                }
                else
                {
                    XElement xrow = new XElement("org1",
                               new XElement("organismno", 0),
                               new XElement("organismmccode", ""),
                               new XElement("organismname", ""),
                               new XElement("antibioticno", 0),
                               new XElement("antibioticmccode", 0),
                               new XElement("antibioticname", ""),
                               new XElement("antsequenceno", 0),
                               new XElement("interp", ""),
                               new XElement("interpvalue", "")
                               );
                    xorgantitbl.Add(xrow);
                }
                //organism 2 entry
                if (results.lstCultureorg2 != null && results.lstCultureorg2.Ono > 0)
                {
                    var druglst = results.lstCultureorg2.lstCulturedrug.Where(d => d.Dintrp != null && d.Dintrp != "").ToList();
                    if (druglst.Count == 0)
                    {
                        XElement xrow = new XElement("org2",
                                new XElement("organismno", results.lstCultureorg2.Ono),
                                new XElement("organismmccode", results.lstCultureorg2.Ocode),
                                new XElement("organismname", results.lstCultureorg2.Oname),
                                new XElement("antibioticno", 0),
                                new XElement("antibioticmccode", 0),
                                new XElement("antibioticname", ""),
                                new XElement("antsequenceno", 0),
                                new XElement("interp", 0),
                                new XElement("interpvalue", "")
                                );
                        xorgantitbl.Add(xrow);
                    }
                    else
                    {
                        foreach (var ant in druglst)
                        {
                            XElement xrow = new XElement("org2",
                              new XElement("organismno", results.lstCultureorg2.Ono),
                              new XElement("organismmccode", results.lstCultureorg2.Ocode),
                              new XElement("organismname", results.lstCultureorg2.Oname),
                              new XElement("antibioticno", ant.Dno),
                              new XElement("antibioticcode", ant.Dcode),
                              new XElement("antibioticmccode", ant.Dmic),
                              new XElement("antibioticname", ant.Dname),
                              new XElement("interp", ant.Dintrp),
                              new XElement("interpvalue", ant.DintrpVal)
                              );
                            xorgantitbl.Add(xrow);
                        }
                    }
                }
                else
                {
                    XElement xrow = new XElement("org2",
                               new XElement("organismno", 0),
                               new XElement("organismmccode", ""),
                               new XElement("organismname", ""),
                               new XElement("antibioticno", 0),
                               new XElement("antibioticmccode", 0),
                               new XElement("antibioticname", ""),
                               new XElement("antsequenceno", 0),
                               new XElement("interp", ""),
                               new XElement("interpvalue", "")
                               );
                    xorgantitbl.Add(xrow);
                }
                //organism 3 entry
                if (results.lstCultureorg3 != null && results.lstCultureorg3.Ono > 0)
                {
                    var druglst = results.lstCultureorg3.lstCulturedrug.Where(d => d.Dintrp != null && d.Dintrp != "").ToList();
                    if (druglst.Count == 0)
                    {
                        XElement xrow = new XElement("org3",
                                new XElement("organismno", results.lstCultureorg3.Ono),
                                new XElement("organismmccode", results.lstCultureorg3.Ocode),
                                new XElement("organismname", results.lstCultureorg3.Oname),
                                new XElement("antibioticno", 0),
                                new XElement("antibioticmccode", 0),
                                new XElement("antibioticname", ""),
                                new XElement("antsequenceno", 0),
                                new XElement("interp", ""),
                                new XElement("interpvalue", "")
                                );
                        xorgantitbl.Add(xrow);
                    }
                    else
                    {
                        foreach (var ant in druglst)
                        {
                            XElement xrow = new XElement("org3",
                              new XElement("organismno", results.lstCultureorg3.Ono),
                              new XElement("organismmccode", results.lstCultureorg3.Ocode),
                              new XElement("organismname", results.lstCultureorg3.Oname),
                              new XElement("antibioticno", ant.Dno),
                              new XElement("antibioticcode", ant.Dcode),
                              new XElement("antibioticmccode", ant.Dmic),
                              new XElement("antibioticname", ant.Dname),
                              new XElement("interp", ant.Dintrp),
                              new XElement("interpvalue", ant.DintrpVal)
                              );
                            xorgantitbl.Add(xrow);
                        }
                    }
                }
                else
                {
                    XElement xrow = new XElement("org3",
                               new XElement("organismno", 0),
                               new XElement("organismmccode", ""),
                               new XElement("organismname", ""),
                               new XElement("antibioticno", 0),
                               new XElement("antibioticmccode", 0),
                               new XElement("antibioticname", ""),
                               new XElement("antsequenceno", 0),
                               new XElement("interp", ""),
                               new XElement("interpvalue", "")
                               );
                    xorgantitbl.Add(xrow);
                }
                ///organism 4 entry
                if (results.lstCultureorg4 != null && results.lstCultureorg4.Ono > 0)
                {
                    var druglst = results.lstCultureorg4.lstCulturedrug.Where(d => d.Dintrp != null && d.Dintrp != "").ToList();
                    if (druglst.Count == 0)
                    {
                        XElement xrow = new XElement("org4",
                                new XElement("organismno", results.lstCultureorg4.Ono),
                                new XElement("organismmccode", results.lstCultureorg4.Ocode),
                                new XElement("organismname", results.lstCultureorg4.Oname),
                                new XElement("antibioticno", 0),
                                new XElement("antibioticmccode", 0),
                                new XElement("antibioticname", ""),
                                new XElement("antsequenceno", 0),
                                new XElement("interp", ""),
                                new XElement("interpvalue", "")
                                );
                        xorgantitbl.Add(xrow);
                    }
                    else
                    {
                        foreach (var ant in druglst)
                        {
                            XElement xrow = new XElement("org4",
                              new XElement("organismno", results.lstCultureorg4.Ono),
                              new XElement("organismmccode", results.lstCultureorg4.Ocode),
                              new XElement("organismname", results.lstCultureorg4.Oname),
                              new XElement("antibioticno", ant.Dno),
                              new XElement("antibioticcode", ant.Dcode),
                              new XElement("antibioticmccode", ant.Dmic),
                              new XElement("antibioticname", ant.Dname),
                              new XElement("interp", ant.Dintrp),
                              new XElement("interpvalue", ant.DintrpVal)
                              );
                            xorgantitbl.Add(xrow);
                        }
                    }
                }
                else
                {
                    XElement xrow = new XElement("org4",
                               new XElement("organismno", 0),
                               new XElement("organismmccode", ""),
                               new XElement("organismname", ""),
                               new XElement("antibioticno", 0),
                               new XElement("antibioticmccode", 0),
                               new XElement("antibioticname", ""),
                               new XElement("antsequenceno", 0),
                               new XElement("interp", ""),
                               new XElement("interpvalue", "")
                               );
                    xorgantitbl.Add(xrow);
                }
                ///organism 5 entry
                if (results.lstCultureorg5 != null && results.lstCultureorg5.Ono > 0)
                {
                    var druglst = results.lstCultureorg5.lstCulturedrug.Where(d => d.Dintrp != null && d.Dintrp != "").ToList();
                    if (druglst.Count == 0)
                    {
                        XElement xrow = new XElement("org5",
                                new XElement("organismno", results.lstCultureorg5.Ono),
                                new XElement("organismmccode", results.lstCultureorg5.Ocode),
                                new XElement("organismname", results.lstCultureorg5.Oname),
                                new XElement("antibioticno", 0),
                                new XElement("antibioticmccode", 0),
                                new XElement("antibioticname", ""),
                                new XElement("antsequenceno", 0),
                                new XElement("interp", ""),
                                new XElement("interpvalue", "")
                                );
                        xorgantitbl.Add(xrow);
                    }
                    else
                    {
                        foreach (var ant in druglst)
                        {
                            XElement xrow = new XElement("org5",
                              new XElement("organismno", results.lstCultureorg5.Ono),
                              new XElement("organismmccode", results.lstCultureorg5.Ocode),
                              new XElement("organismname", results.lstCultureorg5.Oname),
                              new XElement("antibioticno", ant.Dno),
                              new XElement("antibioticcode", ant.Dcode),
                              new XElement("antibioticmccode", ant.Dmic),
                              new XElement("antibioticname", ant.Dname),
                              new XElement("interp", ant.Dintrp),
                              new XElement("interpvalue", ant.DintrpVal)
                              );
                            xorgantitbl.Add(xrow);
                        }
                    }
                }
                else
                {
                    XElement xrow = new XElement("org5",
                               new XElement("organismno", 0),
                               new XElement("organismmccode", ""),
                               new XElement("organismname", ""),
                               new XElement("antibioticno", 0),
                               new XElement("antibioticmccode", 0),
                               new XElement("antibioticname", ""),
                               new XElement("antsequenceno", 0),
                               new XElement("interp", ""),
                               new XElement("interpvalue", "")
                               );
                    xorgantitbl.Add(xrow);
                }
                odxml.Add(xorgantitbl);
                string organtixml = odxml.ToString();
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", results.Vno);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", results.Vbno);
                    var _barcode = new SqlParameter("Barcode", results.Bno);
                    var _machineId = new SqlParameter("MachineId", results.Mid);
                    var _orderlistNo = new SqlParameter("OrderlistNo", results.Orderlsitno);
                    var _serviceNo = new SqlParameter("ServiceNo", results.Servno);
                    var _organtixml = new SqlParameter("Organtixml", organtixml);
                    var _sample = new SqlParameter("Sample", results.Samp);
                    var lst = context.InsertCultureInterfaceResult.FromSqlRaw(
                                "Execute dbo.pro_InsertMBDifInterfaceResults @VenueNo,@VenueBranchNo,@Barcode,@MachineId,@OrderlistNo,@ServiceNo,@Organtixml,@Sample",
                                _venueNo, _venueBranchNo, _barcode, _machineId, _orderlistNo, _serviceNo, _organtixml, _sample).ToList();
                    result = lst[0].Status;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalRepository.PostCultureResult", ExceptionPriority.High, ApplicationType.REPOSITORY, results.Vno, results.Vbno, 0);
            }
            return result;
        }
        public List<CommonMasterDto> GetEGFRList(int venueno, int venuebranchno, string MasterKey)
        {
            List<CommonMasterDto> objresult = new List<CommonMasterDto>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", venuebranchno);
                    var _MasterKey = new SqlParameter("MasterKey", MasterKey);
                    objresult = context.CommonMasterDTO.FromSqlRaw("Execute dbo.pro_CommonDetails @MasterKey,@venueno,@venuebranchno", _MasterKey, _venueno, _venuebranchno).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalRepository.EGFR-" + MasterKey, ExceptionPriority.Low, ApplicationType.REPOSITORY, venueno, venuebranchno, 0);
            }
            return objresult;
        }
    }
}
