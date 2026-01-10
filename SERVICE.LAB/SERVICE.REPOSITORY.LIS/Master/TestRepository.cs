using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using DEV.Model.EF.DocumentUpload;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static DEV.Model.DocumentUploadDTO;

namespace Dev.Repository
{
    public class TestRepository : ITestRepository
    {
        private IConfiguration _config;
        public TestRepository(IConfiguration config) {
            _config = config;
        }

        public List<lsttest> GetTestList(reqtest req)
        {
            List<lsttest> objResult = new List<lsttest>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _maindeptno = new SqlParameter("maindeptno", req.maindeptNo);
                    var _deptno = new SqlParameter("deptno", req.deptNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _testno = new SqlParameter("testNo", req.testNo);
                    var _pageIndex = new SqlParameter("pageIndex", req.pageIndex);
                    var _pageSize = new SqlParameter("pageSize", req.pageSize);
                    var _Userstatus = new SqlParameter("Userstatus", req.Userstatus);

                    var lst = context.GetTestList.FromSqlRaw(
                    "Execute dbo.pro_GetTestMaster @maindeptno,@deptno, @venueno, @venuebranchno, @testNo, @pageIndex, @pageSize, @Userstatus",
                    _maindeptno, _deptno, _venueno, _venuebranchno, _testno, _pageIndex, _pageSize, _Userstatus).ToList();

                    for (int i = 0; i < lst.Count; i++)
                    {
                        lsttest objTemp = new lsttest();

                        objTemp.rowNo = lst[i].rowNo;
                        objTemp.testNo = lst[i].testNo;
                        objTemp.testShortName = lst[i].testShortName;
                        objTemp.testName = lst[i].testName;
                        objTemp.testDisplayName = lst[i].testDisplayName;
                        objTemp.departmentName = lst[i].departmentName;
                        objTemp.IsDeptInactive = lst[i].IsDeptInactive;
                        objTemp.methodName = lst[i].methodName;
                        objTemp.IsMethodInactive = lst[i].IsMethodInactive;
                        objTemp.sampleName = lst[i].sampleName;
                        objTemp.IsSampleInactive = lst[i].IsSampleInactive;
                        objTemp.containerName = lst[i].containerName;
                        objTemp.IsContainerInactive = lst[i].IsContainerInactive;
                        objTemp.unitName = lst[i].unitName;
                        objTemp.IsUnitInactive = lst[i].IsUnitInactive;
                        objTemp.tsequenceNo = lst[i].tsequenceNo;
                        objTemp.isActive = lst[i].isActive;
                        objTemp.TotalRecords = lst[i].TotalRecords;
                        objTemp.PageIndex = lst[i].PageIndex;
                        objTemp.isSelectMultiSample = lst[i].isSelectMultiSample;

                        if (objTemp.isSelectMultiSample)
                        {
                            objTemp.lstmultisamplesreferencelist = new List<MultiSamplesReferenceList>();
                            objTemp.lstmultisamplesreferencelist = JsonConvert.DeserializeObject<List<MultiSamplesReferenceList>>(lst[i].multisampleXml);
                        }
                        objResult.Add(objTemp);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetTestList" + req.testNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return objResult;
        }
        public objtest GetEditTest(reqtest req)
        {
            objtest obj = new objtest();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _testno = new SqlParameter("testno", req.testNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _IsApproval = new SqlParameter("IsApproval", req.IsApproval);

                    var lst = context.GetEditTest.FromSqlRaw(
                    "Execute dbo.pro_GetSingleTestMaster @testno, @venueno, @venuebranchno, @IsApproval",
                    _testno, _venueno, _venuebranchno, _IsApproval).ToList();

                    obj.testNo = lst[0].testNo;
                    obj.machineCode = lst[0].machineCode;
                    obj.testShortName = lst[0].testShortName;
                    obj.testName = lst[0].testName;
                    obj.testDisplayName = lst[0].testDisplayName;
                    obj.deptNo = lst[0].deptNo;
                    obj.methodNo = lst[0].methodNo;
                    obj.sampleNo = lst[0].sampleNo;
                    obj.containerNo = lst[0].containerNo;
                    obj.unitNo = lst[0].unitNo;
                    obj.volume = lst[0].volume;
                    obj.tsequenceNo = lst[0].tsequenceNo;
                    obj.isAgeBased = lst[0].isAgeBased;
                    obj.gender = lst[0].gender;
                    obj.rate = lst[0].rate;
                    obj.barcodeSuffix = lst[0].barcodeSuffix;
                    obj.barcodePrefix = lst[0].barcodePrefix;
                    obj.resultType = lst[0].resultType;
                    obj.decimalPoint = lst[0].decimalPoint;
                    obj.isRoundOff = lst[0].isRoundOff;
                    obj.statMinutes = lst[0].statMinutes;
                    obj.isNonBillable = lst[0].isNonBillable;
                    obj.isQtyChange = lst[0].isQtyChange;
                    obj.isRateEditable = lst[0].isRateEditable;
                    obj.isAllowDiscount = lst[0].isAllowDiscount;
                    obj.isInterface = lst[0].isInterface;
                    obj.isautoapproval = lst[0].isautoapproval;
                    obj.isOutsource = lst[0].isOutsource;
                    obj.isNonMandatory = lst[0].isNonMandatory;
                    obj.isNonReportable = lst[0].isNonReportable;
                    obj.IsSampleAct = lst[0].IsSampleAct;
                    obj.isIndiviual = lst[0].isIndiviual;
                    obj.isNABL = lst[0].isNABL;
                    obj.isInterNotes = lst[0].isInterNotes;
                    obj.isBillDisclaimer = lst[0].isBillDisclaimer;
                    obj.isReportDisclaimer = lst[0].isReportDisclaimer;
                    obj.isConsent = lst[0].isConsent;
                    obj.isComments = lst[0].isComments;
                    obj.isActive = lst[0].isActive;
                    obj.venueNo = lst[0].venueNo;
                    obj.venueBranchNo = lst[0].venueBranchNo;
                    obj.userNo = lst[0].userNo;
                    obj.tcNo = lst[0].tcNo;
                    obj.comments = lst[0].comments;
                    obj.samplequantity = lst[0].samplequantity;
                    obj.isunacceptable = lst[0].isunacceptable;
                    obj.unacceptcondition = lst[0].unacceptcondition;
                    obj.isincludeinstruction = lst[0].isincludeinstruction;
                    obj.includeinstruction = lst[0].includeinstruction;
                    obj.isSecondReview = lst[0].isSecondReview;
                    obj.testCode = lst[0].testCode;
                    obj.languageText = lst[0].languageText;
                    obj.fromDate = lst[0].fromDate;
                    obj.toDate = lst[0].toDate;
                    obj.testProcessTime = lst[0].testProcessTime;
                    obj.isResultInWL = lst[0].isResultInWL;
                    obj.isSensitiveData = lst[0].isSensitiveData;
                    obj.loincNo = lst[0].loincNo;
                    obj.loincCode = lst[0].loincCode;
                    obj.OldServiceNo = lst[0].OldServiceNo;
                    obj.IsDeltaApproval = lst[0].IsDeltaApproval;
                    obj.DeltaRange = lst[0].DeltaRange;
                    obj.RestrictedValue = lst[0].RestrictedValue;
                    obj.ReptDeptHeaderNo = lst[0].ReptDeptHeaderNo;
                    obj.IsInfectionCtrlRept = lst[0].IsInfectionCtrlRept;
                    obj.IsNEHR = lst[0].IsNEHR;
                    obj.languagecode = lst[0].languagecode;
                    obj.IsNoPrintInRpt = lst[0].IsNoPrintInRpt;
                    obj.BarShortName = lst[0].BarShortName;
                    obj.SecDeptNo = lst[0].SecDeptNo;
                    obj.IsSubTestAvailable = lst[0].IsSubTestAvailable;
                    //
                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    string AppMasterFilePath = "MasterFilePath";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                    string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";
                    path = path + req.venueNo.ToString() + "/T/";

                    if (obj.isInterNotes == true)
                    {
                        string Fnpath_h = path + "/InterNotes/" + req.testNo + "_H" + ".ym";
                        if (File.Exists(Fnpath_h))
                        {
                            try
                            {
                                obj.interNotesHigh = File.ReadAllText(Fnpath_h);
                            }
                            catch (Exception ex)
                            {
                                obj.interNotesHigh = "";
                                MyDevException.Error(ex, "TestRepository.GetEditTest (InterNotes - High) - Test No. : " + req.testNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
                            }
                        }
                        string Fnpath_l = path + "/InterNotes/" + req.testNo + "_L" + ".ym";
                        if (File.Exists(Fnpath_l))
                        {
                            try
                            {
                                obj.interNotesLow = File.ReadAllText(Fnpath_l);
                            }
                            catch (Exception ex)
                            {
                                obj.interNotesLow = "";
                                MyDevException.Error(ex, "TestRepository.GetEditTest (InterNotes - Low) - Test No. : " + req.testNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
                            }
                        }
                        string Fnpath = path + "/InterNotes/" + req.testNo + ".ym";
                        if (File.Exists(Fnpath))
                        {
                            try
                            {
                                obj.interNotes = File.ReadAllText(Fnpath);
                            }
                            catch (Exception ex)
                            {
                                obj.interNotes = "";
                                MyDevException.Error(ex, "TestRepository.GetEditTest (InterNotes - Common) - Test No. : " + req.testNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
                            }
                        }
                    }
                    else
                    {
                        obj.interNotesHigh = "";
                        obj.interNotesLow = "";
                        obj.interNotes = "";
                    }                    
                    
                    if (obj.isBillDisclaimer == true)
                    {
                        string Fnpath = path + "/BillDisclaimer/" + req.testNo + ".ym";
                        try
                        {
                            obj.billDisclaimer = File.ReadAllText(Fnpath);
                        }
                        catch (Exception ex)
                        {
                            obj.billDisclaimer = "";
                            MyDevException.Error(ex, "TestRepository.GetEditTest (BillDisclaimer) - Test No. : " + req.testNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
                        }
                    }
                    else
                    {
                        obj.billDisclaimer = "";
                    }

                    if (obj.isReportDisclaimer == true)
                    {
                        string Fnpath = path + "/ReportDisclaimer/" + req.testNo + ".ym";
                        try
                        {
                            obj.reportDisclaimer = File.ReadAllText(Fnpath);
                        }
                        catch (Exception ex)
                        {
                            obj.reportDisclaimer = "";
                            MyDevException.Error(ex, "TestRepository.GetEditTest (ReportDisclaimer) - Test No. : " + req.testNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
                        }
                    }
                    else
                    {
                        obj.reportDisclaimer = "";
                    }

                    if (obj.isConsent == true)
                    {
                        string Fnpath = path + "/Consent/" + req.testNo + ".ym";
                        try
                        {
                            obj.consentNotes = File.ReadAllText(Fnpath);
                        }
                        catch (Exception ex)
                        {
                            obj.consentNotes = "";
                            MyDevException.Error(ex, "TestRepository.GetEditTest (Consent) - Test No. : " + req.testNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
                        }
                    }
                    else
                    {
                        obj.consentNotes = "";
                    }

                    if (obj.isComments == true)
                    {
                        string Fnpath = path + "/Comments/" + req.testNo + ".ym";
                        try
                        {
                            obj.comments = File.ReadAllText(Fnpath);
                        }
                        catch (Exception ex)
                        {
                            obj.comments = "";
                            MyDevException.Error(ex, "TestRepository.GetEditTest (Comments) - Test No. : " + req.testNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);                            
                        }
                    }
                    else
                    {
                        obj.comments = "";
                    }

                    obj.lsttestrefrange = JsonConvert.DeserializeObject<List<lsttestrefrange>>(lst[0].testrefrange);
                    obj.lsttestanalyrange = JsonConvert.DeserializeObject<List<lsttestanalyrange>>(lst[0].testanlyrange);
                    obj.lsttestPickList = JsonConvert.DeserializeObject<List<lsttestPickList>>(lst[0].testPickList);
                    obj.lstTemplateList = JsonConvert.DeserializeObject<List<lstTemplateList>>(lst[0].testTemplate);
                    obj.isUploadOption = lst[0].isUploadOption;
                    obj.isMultiEditor = lst[0].isMultiEditor;
                    obj.isFormulaFor = lst[0].isFormulaFor;
                    obj.isSelectMultiSample = lst[0].isSelectMultiSample;
                    obj.isNonConcurrentTest = lst[0].isNonConcurrentTest;
                    obj.nehrInterpreditationnotes = lst[0].nehrinternotes;
                    obj.IsSpecialCategory = lst[0].IsSpecialCategory;
                    if (obj.isSelectMultiSample)
                    {
                        obj.lstmultisamplesreferencelist = new List<MultiSamplesReferenceList>();
                        obj.lstmultisamplesreferencelist = JsonConvert.DeserializeObject<List<MultiSamplesReferenceList>>(lst[0].multisampleXml);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetEditTest - Test No. : " + req.testNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        public int InsertTest(objtest req)
        {
            int testno = 0;
            CommonHelper commonUtility = new CommonHelper();

            string testRRXML = "";
            string testAMRXML = "";
            if (req.lsttestrefrange != null && req.lsttestrefrange.Count > 0)
            {
                testRRXML = commonUtility.ToXML(req.lsttestrefrange);
            }
            if (req.lsttestanalyrange != null && req.lsttestanalyrange.Count > 0)
            {
                testAMRXML = commonUtility.ToXML(req.lsttestanalyrange);
            }
            string testPLXML = "";
            if (req.lsttestPickList != null && req.lsttestPickList.Count > 0)
            {
                testPLXML = commonUtility.ToXML(req.lsttestPickList);
            }
            //sample multi select 
            if (req.lstMultiSampleList != null && req.lstMultiSampleList.Count > 0)
            {
                req.selectMultiSampleJson = JsonConvert.SerializeObject(req.lstMultiSampleList);
            }
            string testMultiSampleList = "";
            if (req.lstmultisamplesreferencelist != null)
            {
                testMultiSampleList = commonUtility.ToXML(req.lstmultisamplesreferencelist);
            }

            req.lsttestrefrange.Clear();
            req.lsttestanalyrange.Clear();
            req.lsttestPickList.Clear();
            req.lstMultiSampleList?.Clear();
            req.lstmultisamplesreferencelist?.Clear();
            string testXML = commonUtility.ToXML(req);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _testno = new SqlParameter("testno", req.testNo);
                    var _testXML = new SqlParameter("testXML", testXML);
                    var _testRRXML = new SqlParameter("testRRXML", testRRXML);
                    var _testAMRXML = new SqlParameter("testAMRXML", testAMRXML);
                    var _testPLXML = new SqlParameter("testPLXML", testPLXML);
                    var _testMultiSampleList = new SqlParameter("testMultiSampleList", testMultiSampleList);
                    var _userno = new SqlParameter("userno", req.userNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _IsApproval = new SqlParameter("IsApproval", req.IsApproval);
                    var _IsReject = new SqlParameter("IsReject", req.IsReject);
                    var _RejectReason = new SqlParameter("RejectReason", req?.RejectReason);
                    var _OldServiceNo = new SqlParameter("OldServiceNo", req?.OldServiceNo);

                    var lst = context.InsertTest.FromSqlRaw(
                    "Execute dbo.pro_InsertTest @testno,@testXML,@testRRXML,@testAMRXML,@testPLXML,@userno,@venueno,@venuebranchno,@testMultiSampleList,@IsApproval,@IsReject,@RejectReason,@OldServiceNo",
                    _testno, _testXML, _testRRXML, _testAMRXML, _testPLXML, _userno, _venueno, _venuebranchno, _testMultiSampleList, _IsApproval, _IsReject, _RejectReason, _OldServiceNo).ToList();

                    testno = lst[0].testNo;
                    //
                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    string AppMasterFilePath = "MasterFilePath";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                    string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";
                    path = path + req.venueNo.ToString() + "/T/";

                    if (req.interNotesHigh != null && req.interNotesHigh.Length > 0)
                    {
                        string Fnpath = path + "/InterNotes/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.interNotesHigh + Environment.NewLine;
                        File.WriteAllText(Fnpath + testno + "_H" + ".ym", createText);
                    }
                    else
                    {
                        string Fnpath = path + "/InterNotes/";
                        if (Directory.Exists(Fnpath))
                        {
                            string actpath = Fnpath + testno + "_H" + ".ym";
                            if (File.Exists(actpath))
                            {
                                File.Delete(actpath);
                            }
                        }
                    }

                    if (req.interNotesLow != null && req.interNotesLow.Length > 0)
                    {
                        string Fnpath = path + "/InterNotes/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.interNotesLow + Environment.NewLine;
                        File.WriteAllText(Fnpath + testno + "_L" + ".ym", createText);
                    }
                    else
                    {
                        string Fnpath = path + "/InterNotes/";
                        if (Directory.Exists(Fnpath))
                        {
                            string actpath = Fnpath + testno + "_L" + ".ym";
                            if (File.Exists(actpath))
                            {
                                File.Delete(actpath);
                            }
                        }
                    }

                    if (req.isInterNotes == true)
                    {
                        string Fnpath = path + "/InterNotes/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.interNotes + Environment.NewLine;
                        File.WriteAllText(Fnpath + testno + ".ym", createText);
                    }
                    else
                    {
                        string Fnpath = path + "/InterNotes/";
                        if (Directory.Exists(Fnpath))
                        {
                            string actpath = Fnpath + testno + ".ym";
                            if (File.Exists(actpath))
                            {
                                File.Delete(actpath);
                            }
                        }
                    }
                    if (req.isBillDisclaimer == true)
                    {
                        string Fnpath = path + "/BillDisclaimer/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.billDisclaimer + Environment.NewLine;
                        File.WriteAllText(Fnpath + testno + ".ym", createText);
                    }
                    else
                    {
                        string Fnpath = path + "/BillDisclaimer/";
                        if (Directory.Exists(Fnpath))
                        {
                            string actpath = Fnpath + testno + ".ym";
                            if (File.Exists(actpath))
                            {
                                File.Delete(actpath);
                            }
                        }
                    }
                    if (req.isReportDisclaimer == true)
                    {
                        string Fnpath = path + "/ReportDisclaimer/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.reportDisclaimer + Environment.NewLine;
                        File.WriteAllText(Fnpath + testno + ".ym", createText);
                    }
                    else
                    {
                        string Fnpath = path + "/ReportDisclaimer/";
                        if (Directory.Exists(Fnpath))
                        {
                            string actpath = Fnpath + testno + ".ym";
                            if (File.Exists(actpath))
                            {
                                File.Delete(actpath);
                            }
                        }
                    }
                    if (req.isConsent == true)
                    {
                        string Fnpath = path + "/Consent/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.consentNotes + Environment.NewLine;
                        File.WriteAllText(Fnpath + testno + ".ym", createText);
                    }
                    else
                    {
                        string Fnpath = path + "/Consent/";
                        if (Directory.Exists(Fnpath))
                        {
                            string actpath = Fnpath + testno + ".ym";
                            if (File.Exists(actpath))
                            {
                                File.Delete(actpath);
                            }
                        }
                    }
                    if (req.isComments == true)
                    {
                        string Fnpath = path + "/Comments/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.comments + Environment.NewLine;
                        File.WriteAllText(Fnpath + testno + ".ym", createText);
                    }
                    else
                    {
                        string Fnpath = path + "/Comments/";
                        if (Directory.Exists(Fnpath))
                        {
                            string actpath = Fnpath + testno + ".ym";
                            if (File.Exists(actpath))
                            {
                                File.Delete(actpath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.InsertTest - Test No. : " + req.testNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return testno;
        }
        public rtntemplateNo InsertTemplateText(lstTemplateList req)
        {
            rtntemplateNo obj = new rtntemplateNo();
            int templateNo = 0;
            int templateApprovalNo = 0;
            string templateName = "";
            string contentBody = "";
            bool isApproval = false;

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _testno = new SqlParameter("testno", req.testNo);
                    var _templateNo = new SqlParameter("templateNo", req.templateNo);
                    var _templateName = new SqlParameter("templateName", req.templateName);
                    var _isDefault = new SqlParameter("isDefault", req.isDefault);
                    var _sequenceNo = new SqlParameter("sequenceNo", req.sequenceNo);
                    var _status = new SqlParameter("status", req.status);
                    var _userno = new SqlParameter("userno", req.userNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _OldServiceNo = new SqlParameter("OldServiceNo", req.OldServiceNo);
                    var _OldTemplateNo = new SqlParameter("OldTemplateNo", req.OldTemplateNo);
                    var _IsApproval = new SqlParameter("IsApproval", req.IsApproval);
                    var _IsReject = new SqlParameter("IsReject", req.IsReject);

                    var lst = context.InsertTemplateText.FromSqlRaw(
                    "Execute dbo.pro_InsertTemplateText @testno, @templateNo, @templateName, @isDefault, @sequenceNo," +
                    "@status, @userno, @venueno, @venuebranchno, @OldServiceNo, @OldTemplateNo, @IsApproval, @IsReject",
                    _testno, _templateNo, _templateName, _isDefault, _sequenceNo, _status, _userno, _venueno, _venuebranchno,
                    _OldServiceNo, _OldTemplateNo, _IsApproval, _IsReject).ToList();

                    obj.templateNo = lst[0].templateNo;
                    obj.templateApprovalNo = lst[0].templateApprovalNo;

                    templateNo = lst[0].templateNo;
                    templateApprovalNo = lst[0].templateApprovalNo;
                    templateName = req.templateName;
                    contentBody = req.templateText;
                    isApproval = req.IsApproval;

                    if (templateApprovalNo > 0)
                    {
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppMasterFilePath = "MasterFilePath";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                        string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";
                        // string path = _config.GetConnectionString(ConfigKeys.MasterFilePath);
                        path = path + req.venueNo.ToString() + "/Template/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string createText = req.templateText + Environment.NewLine;
                        File.WriteAllText(path + templateApprovalNo + ".ym", createText);
                    }
                }
                //try
                //{
                //    if (templateNo > 0 || templateApprovalNo > 0)
                //    {
                //        using (var context = new DocumentContext(_config.GetConnectionString(ConfigKeys.DocumentDBConnection)))
                //        {
                //            if (isApproval == true)
                //            {
                //                TestTemplateRequest templateRequest = new TestTemplateRequest();
                //                templateRequest.TemplateNo = templateNo;
                //                templateRequest.TemplateName = templateName;
                //                templateRequest.ContentBody = contentBody;
                //                templateRequest.VenueNo = (short)req.venueNo;
                //                templateRequest.Status = true;
                //                templateRequest.CreatedOn = DateTime.UtcNow;
                //                templateRequest.CreatedBy = (short)req.userNo;

                //                context.TemplateContent_Test.Add(templateRequest);
                //                context.SaveChanges();
                //            }
                //            else if (isApproval == false)
                //            {
                //                TestTemplateApprovalRequest templateApprovalRequest = new TestTemplateApprovalRequest();
                //                templateApprovalRequest.TemplateApprovalNo = templateApprovalNo;
                //                templateApprovalRequest.TemplateNo = templateNo;
                //                templateApprovalRequest.TemplateName = templateName;
                //                templateApprovalRequest.ContentBody = contentBody;
                //                templateApprovalRequest.VenueNo = (short)req.venueNo;
                //                templateApprovalRequest.Status = true;
                //                templateApprovalRequest.CreatedOn = DateTime.UtcNow;
                //                templateApprovalRequest.CreatedBy = (short)req.userNo;

                //                context.TemplateContentApproval_Test.Add(templateApprovalRequest);
                //                context.SaveChanges();
                //            }
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MyDevException.Error(ex, "TestRepository.InsertTemplateText - Test No. : " + req.testNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
                //}
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.InsertTemplateText - Test No. : " + req.testNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return obj;
        }

        public rtntemplateText GetTemplateText(reqtest req)
        {
            rtntemplateText obj = new rtntemplateText();
            try
            {
                //
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppMasterFilePath = "MasterFilePath";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";
                //  string path = _config.GetConnectionString(ConfigKeys.MasterFilePath);
                path = path + req.venueNo.ToString() + "/Template/" + req.templateNo.ToString() + ".ym";
                obj.templateText = File.ReadAllText(path).ToString();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetTemplateText", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        public int UpdateSequence(Objtestsequence req)
        {
            int i = 0;
            CommonHelper commonUtility = new CommonHelper();

            string testSeqXML = "";
            if (req.lsttestsequence.Count > 0)
            {
                testSeqXML = commonUtility.ToXML(req.lsttestsequence);
            }

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _testSeqXML = new SqlParameter("testSeqXML", testSeqXML);
                    var _testType = new SqlParameter("testType", req.testType);
                    var _userno = new SqlParameter("userno", req.userNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    
                    var lst = context.UpdateSequence.FromSqlRaw(
                    "Execute dbo.pro_UpdateSequence @testSeqXML, @testType, @userno, @venueno, @venuebranchno",
                    _testSeqXML, _testType, _userno, _venueno, _venuebranchno).ToList();
                    
                    i = lst[0].testNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.UpdateSequence", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return i;
        }
        public List<lstgrppkg> GetGroupPackageList(reqtest req)
        {
            List<lstgrppkg> lst = new List<lstgrppkg>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pageCode = new SqlParameter("pageCode", req.pageCode);
                    var _deptno = new SqlParameter("deptNo", req.deptNo);
                    var _venueno = new SqlParameter("venueNo", req.venueNo);
                    var _venuebranchno = new SqlParameter("venueBranchNo", req.venueBranchNo);
                    var _masterno = new SqlParameter("masterNo", req.masterNo);
                    var _Userstatus = new SqlParameter("Userstatus", req.Userstatus);
                    var _pageIndex = new SqlParameter("PageIndex", req.pageIndex == 0 ? 1 : req.pageIndex);
                    var _pageSize = new SqlParameter("PageSize", req.pageSize == 0 ? 10000 : req.pageSize);
                    
                    lst = context.GetGroupPackageList.FromSqlRaw(
                    "Execute dbo.pro_GetGroupPackageMaster @pageCode, @deptNo,@venueNo, @venueBranchNo, @masterNo, @Userstatus, @PageIndex, @PageSize",
                    _pageCode, _deptno, _venueno, _venuebranchno, _masterno, _Userstatus, _pageIndex, _pageSize).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetGroupPackageList" + req.masterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        public objgrppkg GetEditGroupPackage(reqtest req)
        {
            objgrppkg obj = new objgrppkg();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pageCode = new SqlParameter("pageCode", req.pageCode);
                    var _serviceNo = new SqlParameter("serviceNo", req.testNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _IsApproval = new SqlParameter("IsApproval", req.IsApproval);

                    var lst = context.GetEditGroupPackage.FromSqlRaw(
                    "Execute dbo.pro_GetSingleGroupPackageMaster @pageCode, @serviceNo, @venueno, @venuebranchno, @IsApproval",
                    _pageCode, _serviceNo, _venueno, _venuebranchno, _IsApproval).ToList();

                    obj.serviceNo = lst[0].serviceNo;
                    obj.tcNo = lst[0].tcNo;
                    obj.shortName = lst[0].shortName;
                    obj.serviceName = lst[0].serviceName;
                    obj.displayName = lst[0].displayName;
                    obj.deptNo = lst[0].deptNo;
                    obj.sampleNo = lst[0].sampleNo;
                    obj.containerNo = lst[0].containerNo;
                    obj.sequenceNo = lst[0].sequenceNo;
                    obj.rate = lst[0].rate;
                    obj.cutoffTime = lst[0].cutoffTime;
                    obj.processingMinutes = lst[0].processingMinutes;
                    obj.processingDays = lst[0].processingDays;
                    obj.IsChoice = lst[0].IsChoice;
                    obj.ChoiceCount = lst[0].ChoiceCount;
                    obj.isincludeinstruction = lst[0].isincludeinstruction;
                    obj.isRateEditable = lst[0].isRateEditable;
                    obj.isAllowDiscount = lst[0].isAllowDiscount;
                    obj.isOutsource = lst[0].isOutsource;
                    obj.isInterNotes = lst[0].isInterNotes;
                    obj.isBillDisclaimer = lst[0].isBillDisclaimer;
                    obj.isReportDisclaimer = lst[0].isReportDisclaimer;
                    obj.isConsent = lst[0].isConsent;
                    obj.isComments = lst[0].isComments;
                    obj.isActive = lst[0].isActive;
                    obj.venueNo = lst[0].venueNo;
                    obj.venueBranchNo = lst[0].venueBranchNo;
                    obj.userNo = lst[0].userNo;
                    obj.FromDate = lst[0].FromDate;
                    obj.ToDate = lst[0].ToDate; //--pkg bufferday
                    obj.isSecondReview = lst[0].isSecondReview;
                    obj.testCode = lst[0].testCode;
                    obj.languageText = lst[0].languageText;
                    obj.loincNo = lst[0].loincNo;
                    obj.loincCode = lst[0].loincCode;
                    obj.IsInfectionCtrlRept = lst[0].IsInfectionCtrlRept;
                    obj.gender = lst[0].gender;
                    obj.ReptDeptHeaderNo = lst[0].ReptDeptHeaderNo;
                    obj.languagecode = lst[0].languagecode;
                    obj.OldServiceNo = lst[0].OldServiceNo;
                    obj.BarShortName = lst[0].BarShortName;
                    obj.nehrInterpreditationnotes = lst[0].nehrInterPnotes;
                    obj.bufferDays = lst[0].bufferDays;
                    obj.bufferDate = lst[0].bufferDate; // --to date
                    obj.isdisplayinreport = lst[0].isdisplayinreport;
                    obj.IsSpecimen = lst[0].isSpecimen;
                    obj.IsSpecialCategory = lst[0].IsSpecialCategory;
                    obj.isUploadOption = lst[0].isUploadOption;

                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    string AppMasterFilePath = "MasterFilePath";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                    string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";

                    if (req.pageCode == "GRPMAS")
                    {
                        path = path + req.venueNo.ToString() + "/G/";
                    }
                    else if (req.pageCode == "PKGMAS")
                    {
                        path = path + req.venueNo.ToString() + "/P/";
                    }

                    if (obj.isInterNotes == true)
                    {
                        string Fnpath = path + "/InterNotes/" + req.testNo + ".ym";
                        obj.interNotes = File.ReadAllText(Fnpath);
                    }
                    else
                    {
                        obj.interNotes = "";
                    }
                    if (obj.isBillDisclaimer == true)
                    {
                        string Fnpath = path + "/BillDisclaimer/" + req.testNo + ".ym";
                        obj.billDisclaimer = File.ReadAllText(Fnpath);
                    }
                    else
                    {
                        obj.billDisclaimer = "";
                    }
                    if (obj.isReportDisclaimer == true)
                    {
                        string Fnpath = path + "/ReportDisclaimer/" + req.testNo + ".ym";
                        obj.reportDisclaimer = File.ReadAllText(Fnpath);
                    }
                    else
                    {
                        obj.reportDisclaimer = "";
                    }
                    if (obj.isConsent == true)
                    {
                        string Fnpath = path + "/Consent/" + req.testNo + ".ym";
                        obj.consentNotes = File.ReadAllText(Fnpath);
                    }
                    else
                    {
                        obj.consentNotes = "";
                    }
                    if (obj.isComments == true)
                    {
                        string Fnpath = path + "/Comments/" + req.testNo + ".ym";
                        obj.comments = File.ReadAllText(Fnpath);
                    }
                    else
                    {
                        obj.comments = "";
                    }
                    if (obj.isincludeinstruction == true)
                    {
                        string Fnpath = path + "/Instruction/" + req.testNo + ".ym";
                        obj.includeinstruction = File.ReadAllText(Fnpath);
                    }
                    else
                    {
                        obj.includeinstruction = "";
                    }
                    if (obj.IsSpecimen == true)
                    {
                        string Fnpath = path + "/Specimen/" + req.testNo + ".ym";
                        obj.Specimen = File.ReadAllText(Fnpath);
                    }
                    else
                    {
                        obj.Specimen = "";
                    }
                    obj.lstgrppkgservice = JsonConvert.DeserializeObject<List<lstgrppkgservice>>(lst[0].grppkgtests);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetEditGroupPackage ( " + req.pageCode + " ) - " + req.testNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        public int InsertGroupPackage(objgrppkg req)
        {
            int serviceNo = 0;
            CommonHelper commonUtility = new CommonHelper();

            string grouptestXML = "";
            if (req.lstgrppkgservice?.Count > 0)
            {
                grouptestXML = commonUtility.ToXML(req.lstgrppkgservice);
            }
            req.lstgrppkgservice?.Clear();
            string groupXML = commonUtility.ToXML(req);
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pageCode = new SqlParameter("pageCode", req.pageCode);
                    var _serviceNo = new SqlParameter("serviceNo", req.serviceNo);
                    var _groupXML = new SqlParameter("groupXML", groupXML);
                    var _grouptestXML = new SqlParameter("grouptestXML", grouptestXML);
                    var _userno = new SqlParameter("userno", req.userNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _IsApproval = new SqlParameter("IsApproval", req.IsApproval);
                    var _IsReject = new SqlParameter("IsReject", req.IsReject);
                    var _RejectReason = new SqlParameter("RejectReason", req?.RejectReason);
                    var _OldServiceNo = new SqlParameter("OldServiceNo", req?.OldServiceNo);

                    var lst = context.InsertGroupPackage.FromSqlRaw(
                    "Execute dbo.pro_InsertGroupPackage @pageCode,@serviceNo,@groupXML,@grouptestXML,@userno,@venueno,@venuebranchno,@IsApproval,@IsReject,@RejectReason,@OldServiceNo",
                    _pageCode, _serviceNo, _groupXML, _grouptestXML, _userno, _venueno, _venuebranchno, _IsApproval, _IsReject, _RejectReason, _OldServiceNo).ToList();
                    
                    serviceNo = lst[0].testNo;

                    //
                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    string AppMasterFilePath = "MasterFilePath";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                    string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";

                    if (req.pageCode == "GRPMAS")
                    {
                        path = path + req.venueNo.ToString() + "/G/";
                    }
                    else if (req.pageCode == "PKGMAS")
                    {
                        path = path + req.venueNo.ToString() + "/P/";
                    }

                    if (req.isInterNotes == true)
                    {
                        string Fnpath = path + "/InterNotes/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.interNotes + Environment.NewLine;
                        File.WriteAllText(Fnpath + serviceNo + ".ym", createText);
                    }
                    if (req.isBillDisclaimer == true)
                    {
                        string Fnpath = path + "/BillDisclaimer/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.billDisclaimer + Environment.NewLine;
                        File.WriteAllText(Fnpath + serviceNo + ".ym", createText);
                    }
                    if (req.isReportDisclaimer == true)
                    {
                        string Fnpath = path + "/ReportDisclaimer/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.reportDisclaimer + Environment.NewLine;
                        File.WriteAllText(Fnpath + serviceNo + ".ym", createText);
                    }
                    if (req.isConsent == true)
                    {
                        string Fnpath = path + "/Consent/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.consentNotes + Environment.NewLine;
                        File.WriteAllText(Fnpath + serviceNo + ".ym", createText);
                    }
                    if (req.isComments == true)
                    {
                        string Fnpath = path + "/Comments/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.comments + Environment.NewLine;
                        File.WriteAllText(Fnpath + serviceNo + ".ym", createText);
                    }
                    if (req.isincludeinstruction == true)
                    {
                        string Fnpath = path + "/Instruction/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.includeinstruction + Environment.NewLine;
                        File.WriteAllText(Fnpath + serviceNo + ".ym", createText);
                    }
                    if (req.IsSpecimen == true)
                    {
                        string Fnpath = path + "/Specimen/";
                        if (!Directory.Exists(Fnpath))
                        {
                            Directory.CreateDirectory(Fnpath);
                        }
                        string createText = req.Specimen + Environment.NewLine;
                        File.WriteAllText(Fnpath + serviceNo + ".ym", createText);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.InsertGroupPackage - " + req.serviceNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return serviceNo;
        }
        public List<lstgrppkgservice> GetSearchService(reqsearchservice req)
        {
            List<lstgrppkgservice> lst = new List<lstgrppkgservice>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _deptno = new SqlParameter("deptno", req.deptNo);
                    var _searchby = new SqlParameter("searchby", req.searchby);
                    var _searchtext = new SqlParameter("searchtext", req.searchtext);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);

                    lst = context.GetSearchService.FromSqlRaw(
                    "Execute dbo.pro_SearchTestGroupPackage @deptno,@searchby,@searchtext,@venueno,@venuebranchno",
                    _deptno, _searchby, _searchtext, _venueno, _venuebranchno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetSearchService", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        public List<lststest> GetSubTestList(reqtest req)
        {
            List<lststest> lst = new List<lststest>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _deptno = new SqlParameter("deptno", req.deptNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _masterNo = new SqlParameter("masterno", req.masterNo);
                    var _mastertype = new SqlParameter("mastertype", req.mastertype);
                    var _Userstatus = new SqlParameter("Userstatus", req.Userstatus);
                    
                    var rtndblst = context.GetSubTestList.FromSqlRaw(
                    "Execute dbo.pro_GetSubTestList @deptno,@venueno,@venuebranchno,@masterno,@mastertype,@Userstatus",
                    _deptno, _venueno, _venuebranchno, _masterNo, _mastertype, _Userstatus).ToList();

                    int testNo = 0;
                    int subTestNo = 0;

                    rtndblst = rtndblst.OrderBy(a => a.testNo).ToList();
                    List<lststest> lstv = new List<lststest>();
                    foreach (var v in rtndblst)
                    {
                        if (testNo != v.testNo)
                        {
                            testNo = v.testNo;
                            lststest objv = new lststest();
                            objv.rowNo = v.rowNo;
                            objv.testNo = v.testNo;
                            objv.testName = v.testName;
                            objv.departmentName = v.departmentName;
                            objv.sampleName = v.sampleName;
                            objv.containerName = v.containerName;
                            subTestNo = 0;
                            var ollst = rtndblst.Where(o => o.testNo == v.testNo).ToList();
                            ollst = ollst.OrderBy(a => a.stSequenceNo).ToList();
                            List<lstsubtest> lstol = new List<lstsubtest>();
                            foreach (var ol in ollst)
                            {
                                if (subTestNo != ol.subTestNo)
                                {
                                    subTestNo = ol.subTestNo;
                                    lstsubtest objol = new lstsubtest();
                                    objol.testNo = ol.testNo;
                                    objol.departmentNo = ol.departmentNo;
                                    objol.departmentName = ol.departmentName;
                                    objol.subTestDept = ol.subTestDept;
                                    objol.subTestNo = ol.subTestNo;
                                    objol.subTestName = ol.subTestName;
                                    objol.methodName = ol.methodName;
                                    objol.unitName = ol.unitName;
                                    objol.stSequenceNo = ol.stSequenceNo;
                                    objol.isActive = ol.isActive;
                                    lstol.Add(objol);
                                }
                            }
                            objv.lstsubtest = lstol;
                            lst.Add(objv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetSubTestList", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        public objsubtest GetEditSubTest(reqtest req)
        {
            objsubtest obj = new objsubtest();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _testno = new SqlParameter("testno", req.testNo);
                    var _subtestNo = new SqlParameter("subtestno", req.subTestNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _IsApproval = new SqlParameter("IsApproval", req.IsApproval);

                    var lst = context.GetEditSubTest.FromSqlRaw(
                    "Execute dbo.pro_GetSingleSubTestMaster @testno, @subtestno, @venueno, @venuebranchno, @IsApproval",
                    _testno, _subtestNo, _venueno, _venuebranchno, _IsApproval).ToList();

                    obj.testNo = lst[0].testNo;
                    obj.testName = lst[0].testName;
                    obj.departmentNo = lst[0].departmentNo;
                    obj.subTestNo = lst[0].subTestNo;
                    obj.machineCode = lst[0].machineCode;
                    obj.testShortName = lst[0].testShortName;
                    obj.subTestName = lst[0].subTestName;
                    obj.testDisplayName = lst[0].testDisplayName;
                    obj.methodNo = lst[0].methodNo;
                    obj.unitNo = lst[0].unitNo;
                    obj.headerNo = lst[0].headerNo;
                    obj.stSequenceNo = lst[0].stSequenceNo;
                    obj.isAgeBased = lst[0].isAgeBased;
                    obj.resultType = lst[0].resultType;
                    obj.decimalPoint = lst[0].decimalPoint;
                    obj.isRoundOff = lst[0].isRoundOff;
                    obj.isNonMandatory = lst[0].isNonMandatory;
                    obj.isNonReportable = lst[0].isNonReportable;
                    obj.isActive = lst[0].isActive;
                    obj.venueNo = lst[0].venueNo;
                    obj.venueBranchNo = lst[0].venueBranchNo;
                    obj.userNo = lst[0].userNo;
                    obj.languageText = lst[0].languageText;
                    obj.isExtraSubTest = lst[0].isExtraSubTest;
                    obj.IsDeltaApproval = lst[0].IsDeltaApproval;
                    obj.DeltaRange = lst[0].DeltaRange;
                    obj.languagecode = lst[0].languagecode;
                    obj.RestrictedValue = lst[0].RestrictedValue;
                    obj.IsNoPrintInRpt = lst[0].IsNoPrintInRpt;
                    obj.lsttestrefrange = JsonConvert.DeserializeObject<List<lsttestrefrange>>(lst[0].testrefrange);
                    obj.lsttestPickList = JsonConvert.DeserializeObject<List<lsttestPickList>>(lst[0].testPickList);
                    obj.lsttestanalyrange = JsonConvert.DeserializeObject<List<lsttestanalyrange>>(lst[0].testanlyrange);
                    obj.testCode = lst[0].testCode;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetEditSubTest -  Sub-Test No. : " + req.subTestNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        public int InsertSubTest(objsubtest req)
        {
            int subTestNo = 0;
            CommonHelper commonUtility = new CommonHelper();

            string testRRXML = "";
            string testAMRXML = "";
            if (req.lsttestrefrange.Count > 0)
            {
                testRRXML = commonUtility.ToXML(req.lsttestrefrange);
            }
            string testPLXML = "";
            if (req.lsttestPickList.Count > 0)
            {
                testPLXML = commonUtility.ToXML(req.lsttestPickList);
            }
            if (req.lsttestanalyrange != null && req.lsttestanalyrange.Count > 0)
            {
                testAMRXML = commonUtility.ToXML(req.lsttestanalyrange);
            }
            req.lsttestrefrange.Clear();
            req.lsttestPickList.Clear();
            string testXML = commonUtility.ToXML(req);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _testno = new SqlParameter("testno", req.testNo);
                    var _subTestNo = new SqlParameter("subTestNo", req.subTestNo);
                    var _testXML = new SqlParameter("testXML", testXML);
                    var _testRRXML = new SqlParameter("testRRXML", testRRXML);
                    var _testPLXML = new SqlParameter("testPLXML", testPLXML);
                    var _userno = new SqlParameter("userno", req.userNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _testAMRXML = new SqlParameter("testAMRXML", testAMRXML);

                    var lst = context.InsertSubTest.FromSqlRaw(
                    "Execute dbo.pro_InsertSubTest @testno, @subTestNo, @testXML, @testRRXML, @testPLXML, @userno, @venueno, @venuebranchno, @testAMRXML",
                    _testno, _subTestNo, _testXML, _testRRXML, _testPLXML, _userno, _venueno, _venuebranchno, _testAMRXML).ToList();

                    subTestNo = lst[0].testNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.InsertSubTest - Sub-Test No. : " + req.subTestNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return subTestNo;
        }
        public int InsertTestFormula(SaveFormulaRequest req)
        {
            int i = 0;

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _formulaNo = new SqlParameter("formulaNo", req.formulaNo);
                    var _formula = new SqlParameter("formula", req.formula);
                    var _parameterType = new SqlParameter("parameterType", req.parameterType);
                    var _serviceType = new SqlParameter("serviceType", req.serviceType);
                    var _formulaFor = new SqlParameter("formulaFor", req.formulaFor);
                    var _userno = new SqlParameter("userNo", req.userNo);
                    var _venueno = new SqlParameter("venueNo", req.venueNo);
                    var _venuebranchno = new SqlParameter("venueBranchNo", req.venueBranchNo);
                    var _genderCode = new SqlParameter("GenderCode", req.GenderCode);
                    
                    var lst = context.InsertTestFormula.FromSqlRaw(
                    "Execute dbo.pro_InsertFormulaMaster @formulaNo,@formula,@parameterType,@serviceType,@formulaFor,@venueNo,@venueBranchNo,@userNo,@GenderCode",
                    _formulaNo, _formula, _parameterType, _serviceType, _formulaFor, _venueno, _venuebranchno, _userno, _genderCode).ToList();
                    
                    i = lst[0].formulaNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.InsertTestFormula", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return i;
        }
        public List<GetFormulaResponse> GetTestFormula(GetFormulaRequest req)
        {
            List<GetFormulaResponse> obj = new List<GetFormulaResponse>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _testNo = new SqlParameter("testNo", req.testNo);
                    var _testtype = new SqlParameter("testtype", req.testtype);
                    var _venueno = new SqlParameter("venueNo", req.venueNo);
                    var _venuebranchno = new SqlParameter("venueBranchNo", req.venueBranchNo);
                    
                    obj = context.GetTestFormula.FromSqlRaw(
                    "Execute dbo.pro_GetFormulaDetails @testNo,@testtype,@venueNo,@venueBranchNo",
                    _testNo, _testtype, _venueno, _venuebranchno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetTestFormula", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        public CheckTestcodeExistsRes GetAlreadyExisitingTestCode(CheckTestcodeExists req)
        {
            CheckTestcodeExistsRes outp = new CheckTestcodeExistsRes();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _testNo = new SqlParameter("testNo", req.testNo);
                    var _testCode = new SqlParameter("testCode", req.testCode);
                    var _testtype = new SqlParameter("testtype", req.testtype);
                    var _vendorNo = new SqlParameter("vendorNo", req.vendorNo);
                    var _vendorCode = new SqlParameter("vendorCode", req.vendorCode);
                    var _vendor = new SqlParameter("vendor", req.vendor);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    
                    var obj = context.GetCheckTestcodeExists.FromSqlRaw(
                    "Execute dbo.pro_GetAlreadyExisitingTestCode @testNo,@testCode,@testtype,@venueno,@venuebranchno",
                    _testNo, _testCode, _testtype, _venueno, _venuebranchno).ToList();
                    
                    if (obj != null)
                    {
                        outp.outNo = obj[0].outNo;
                        outp.existing = obj[0].existing;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetAlreadyExisitingTestCode", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return outp;
        }
        public List<restestapprove> GetTestApprove(reqtestapprove req)
        {
            List<restestapprove> lst = new List<restestapprove>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FromDate", req.FromDate);
                    var _ToDate = new SqlParameter("ToDate", req.ToDate);
                    var _Type = new SqlParameter("Type", req.Type);
                    var _serviceType = new SqlParameter("serviceType", req.serviceType);
                    var _serviceNo = new SqlParameter("serviceNo", req.serviceNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _venueNo = new SqlParameter("venueNo", req.venueNo);
                    var _pageIndex = new SqlParameter("pageIndex", req.pageIndex);
                    
                    lst = context.GetTestApprove.FromSqlRaw(
                    "Execute dbo.pro_GetTestApproveDetails @FromDate,@ToDate,@Type,@serviceType,@serviceNo,@venueNo,@VenueBranchNo,@pageIndex",
                    _FromDate, _ToDate, _Type, _serviceType, _serviceNo, _venueNo, _VenueBranchNo, _pageIndex).ToList();
                    
                    //int testNo = 0;
                    //int subTestNo = 0;

                    //rtndblst = rtndblst.OrderBy(a => a.ServiceNo).ToList();
                    //List<restestapprove> lstv = new List<restestapprove>();
                    //foreach (var v in rtndblst)
                    //{
                    //    if (testNo != v.ServiceNo)
                    //    {
                    //        testNo = v.ServiceNo;
                    //        restestapprove objv = new restestapprove();
                    //        objv.RowNo = v.RowNo;
                    //        objv.ServiceNo = v.ServiceNo;
                    //        objv.TestName = v.TestName;
                    //        objv.DepartmentName = v.DepartmentName;
                    //        objv.SampleName = v.SampleName;
                    //        objv.ContainerName = v.ContainerName;
                    //        objv.ServiceTypeName = v.ServiceTypeName;
                    //        objv.ModifiedOn = v.ModifiedOn;
                    //        objv.ModifiedBy = v.ModifiedBy;
                    //        subTestNo = 0;
                    //        var ollst = rtndblst.Where(o => o.ServiceNo == v.ServiceNo).ToList();
                    //        ollst = ollst.OrderBy(a => a.STSequenceNo).ToList();
                    //        List<lstsubtestApp> lstol = new List<lstsubtestApp>();
                    //        foreach (var ol in ollst)
                    //        {
                    //            if (subTestNo != ol.SubTestNo)
                    //            {
                    //                subTestNo = ol.SubTestNo;
                    //                lstsubtestApp objol = new lstsubtestApp();
                    //                objol.Id = ol.RowNo;
                    //                objol.ServiceNo = ol.ServiceNo;
                    //                objol.SubTestNo = ol.SubTestNo;
                    //                objol.SubTestName = ol.SubTestName;
                    //                objol.MethodName = ol.MethodName;
                    //                objol.UnitName = ol.UnitName;
                    //                objol.STSequenceNo = ol.STSequenceNo;
                    //                objol.IsActive = ol.IsActive;
                    //                lstol.Add(objol);
                    //            }
                    //        }
                    //        objv.lstsubtestApp = lstol;
                    //        lst.Add(objv);
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetTestApprove - Service No. : " + req.serviceNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, 0, 0);
            }
            return lst;
        }
        public List<restestappHistory> GetApproveHistory(reqtestapprove req)
        {
            List<restestappHistory> lst = new List<restestappHistory>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FromDate", req.FromDate);
                    var _ToDate = new SqlParameter("ToDate", req.ToDate);
                    var _Type = new SqlParameter("Type", req.Type);
                    var _serviceType = new SqlParameter("serviceType", req.serviceType);
                    var _serviceNo = new SqlParameter("serviceNo", req.serviceNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _venueNo = new SqlParameter("venueNo", req.venueNo);
                    var _pageIndex = new SqlParameter("pageIndex", req.pageIndex);
                    
                    lst = context.GetApproveHistory.FromSqlRaw(
                    "Execute dbo.pro_GetApproveHistory @FromDate,@ToDate,@Type,@serviceType,@serviceNo,@venueNo,@VenueBranchNo,@pageIndex",
                    _FromDate, _ToDate, _Type, _serviceType, _serviceNo, _venueNo, _VenueBranchNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetApproveHistory - Service No. : " + req.serviceNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, 0, 0);
            }
            return lst;
        }
        public List<GetTATRes> GetTATMaster(GetTATReq req)
        {
            List<GetTATRes> lst = new List<GetTATRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _MainDeptNo = new SqlParameter("MainDeptNo", req.MainDeptNo);
                    var _DeptNo = new SqlParameter("DeptNo", req.DeptNo);
                    var _serviceNo = new SqlParameter("serviceNo", req.serviceNo);
                    var _serviceType = new SqlParameter("serviceType", req.serviceType);
                    var _venueNo = new SqlParameter("venueNo", req.venueNo);
                    var _pageIndex = new SqlParameter("pageIndex", req.pageIndex);
                    
                    lst = context.GetTATMaster.FromSqlRaw(
                    "Execute dbo.pro_GetTATMaster @MainDeptNo,@DeptNo,@serviceNo,@serviceType,@venueNo,@pageIndex",
                    _MainDeptNo, _DeptNo, _serviceNo, _serviceType, _venueNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetTATMaster - Service No. : " + req.serviceNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, 0, 0);
            }
            return lst;
        }
        public InsTATRes InsertTATMaster(InsTATReq req)
        {
            InsTATRes lst = new InsTATRes();
            CommonHelper commonUtility = new CommonHelper();
            try
            {
                string testXML = "";
                if (req?.testXML?.Count > 0)
                {
                    testXML = commonUtility.ToXML(req?.testXML);
                }
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", req.venueNo);
                    var _venueBranchNo = new SqlParameter("venueBranchNo", req.venueBranchNo);
                    var _userNo = new SqlParameter("userNo", req.userNo);
                    var _testXML = new SqlParameter("testXML", testXML);
                    
                    var obj = context.InsertTATMaster.FromSqlRaw(
                    "Execute dbo.pro_InsertTATMaster @venueNo,@venueBranchNo,@userNo,@testXML",
                    _venueNo, _venueBranchNo, _userNo, _testXML).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.InsertTATMaster - " + req.userNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return lst;
        }
        public List<GetloincRes> GetLoincMaster(GetloincReq req)
        {
            List<GetloincRes> lst = new List<GetloincRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _LoincNo = new SqlParameter("LoincNo", req.LoincNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _pageIndex = new SqlParameter("pageIndex", req.pageIndex);
                    var _ServiceNo = new SqlParameter("ServiceNo", req.ServiceNo);
                    var _DeptNo = new SqlParameter("DeptNo", req.DeptNo);

                    lst = context.GetLoincMaster.FromSqlRaw(
                    "Execute dbo.pro_GetLoincMaster @LoincNo,@venueNo,@pageIndex,@ServiceNo,@DeptNo",
                    _LoincNo, _VenueNo, _pageIndex, _ServiceNo, _DeptNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetLoincMaster -  Loinc No. : " + req.LoincNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, 0, 0);
            }
            return lst;
        }
        public InsloincRes InsertLoincMaster(InsloincReq req)
        {
            InsloincRes lst = new InsloincRes();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _LoincNo = new SqlParameter("LoincNo", req.LoincNo);
                    var _LoincCode = new SqlParameter("LoincCode", req.LoincCode);
                    var _status = new SqlParameter("status", req.status);
                    var _ComponentName = new SqlParameter("ComponentName", req.ComponentName);
                    var _ShortName = new SqlParameter("ShortName", req.ShortName);
                    var _Hl7FieldID = new SqlParameter("Hl7FieldID", req.Hl7FieldID);
                    var _Method = new SqlParameter("Method", req.Method);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", req.UserNo);
                    var _ServiceType = new SqlParameter("ServiceType", req.ServiceType);
                    var _ServiceNo = new SqlParameter("ServiceNo", req.ServiceNo);
                    var _SubtestNo = new SqlParameter("SubtestNo", req.SubtestNo);
                    
                    lst = context.InsertLoincMaster.FromSqlRaw(
                    "Execute dbo.pro_InsertLoincMaster @LoincNo,@LoincCode,@status,@ComponentName,@ShortName,@Hl7FieldID,@Method,@VenueNo,@VenueBranchNo,@UserNo,@ServiceType,@ServiceNo,@SubtestNo",
                    _LoincNo, _LoincCode, _status, _ComponentName, _ShortName, _Hl7FieldID, _Method, _VenueNo, _VenueBranchNo, _UserNo, _ServiceType, _ServiceNo, _SubtestNo).AsEnumerable()?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.InsertTATMaster - " + req.UserNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }

            return lst;
        }
        public List<GetSnomedRes> GetSnomedMaster(GetSnomedReq req)
        {
            List<GetSnomedRes> lst = new List<GetSnomedRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SnomedNo = new SqlParameter("SnomedNo", req.SnomedNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _pageIndex = new SqlParameter("pageIndex", req.pageIndex);

                    lst = context.GetSnomedMaster.FromSqlRaw(
                    "Execute dbo.pro_GetSnomedMaster @SnomedNo,@venueNo,@pageIndex",
                    _SnomedNo, _VenueNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetSnomedMaster - Snomed No. : " + req.SnomedNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, 0, 0);
            }
            return lst;
        }
        public InsSnomedRes InsertSnomedMaster(InsSnomedReq req)
        {
            InsSnomedRes lst = new InsSnomedRes();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SnomedNo = new SqlParameter("SnomedNo", req.SnomedNo);
                    var _SnomedCode = new SqlParameter("SnomedCode", req.SnomedCode);
                    var _status = new SqlParameter("status", req.status);
                    var _Description = new SqlParameter("Description", req.Description);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", req.UserNo);
                    var _IsDefault = new SqlParameter("IsDefault", req.IsDefault);

                    lst = context.InsertSnomedMaster.FromSqlRaw(
                    "Execute dbo.pro_InsertSnomedMaster @SnomedNo,@SnomedCode,@status,@Description,@VenueNo,@VenueBranchNo,@UserNo,@IsDefault",
                    _SnomedNo, _SnomedCode, _status, _Description, _VenueNo, _VenueBranchNo, _UserNo, _IsDefault).AsEnumerable()?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.InsertSnomedMaster - " + req.UserNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }

            return lst;
        }
        public List<IntegrationPackageRes> GetIntegrationPackage(IntegrationPackageReq req)
        {
            List<IntegrationPackageRes> lst = new List<IntegrationPackageRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {            
                    var _deptno = new SqlParameter("deptNo", req.deptNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _masterno = new SqlParameter("masterNo", req.masterNo);
                    var _pageIndex = new SqlParameter("PageIndex", req.pageIndex);
                    var _pageSize = new SqlParameter("PageSize", req.pageSize);

                    lst = context.GetIntegrationPackageRes.FromSqlRaw(
                    "Execute dbo.pro_GetIntegrationMapping @deptNo,@VenueNo,@VenueBranchNo,@masterNo,@pageIndex,@pageSize",
                    _deptno, _VenueNo, _VenueBranchNo, _masterno , _pageIndex , _pageSize).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetIntegrationPackage", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, 0, 0);
            }
            return lst;
        }
        public IntegrationPackageResult InsertIntegrationPackage(IntegrationPackageReq req)
        {
            IntegrationPackageResult lst = new IntegrationPackageResult();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Id = new SqlParameter("Id", req.Id);
                    var _SourceCode = new SqlParameter("SourceCode", req.SourceCode.ValidateEmpty());
                    var _TestNo = new SqlParameter("TestNo", req.TestNo);
                    var _UserNo = new SqlParameter("UserNo", req.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _Type = new SqlParameter("Type", req.Type);

                    lst = context.InsertIntegrationPackageResult.FromSqlRaw(
                    "Execute dbo.pro_InsertIntegrationMapping @Id,@SourceCode,@TestNo,@UserNo,@VenueNo,@VenueBranchNo,@Type",
                    _Id, _SourceCode, _TestNo, _UserNo, _VenueNo, _VenueBranchNo, _Type).AsEnumerable()?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.InsertIntegrationPackage", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return lst;
        }
        public objgrppkg GetPackageInstrauction(reqtest req)
        {
            objgrppkg obj = new objgrppkg();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pageCode = new SqlParameter("pageCode", req.pageCode);
                    var _serviceNo = new SqlParameter("serviceNo", req.testNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _IsApproval = new SqlParameter("IsApproval", req.IsApproval);

                    var lst = context.GetPackageInstrauction.FromSqlRaw(
                    "Execute dbo.pro_GetSingleGroupPackageMaster @pageCode,@serviceNo,@venueno,@venuebranchno,@IsApproval",
                    _pageCode, _serviceNo, _venueno, _venuebranchno, _IsApproval).ToList();

                    if (lst.Count > 0)
                    {
                        obj.isincludeinstruction = lst[0].isincludeinstruction;
                        obj.isComments = lst[0].isComments;
                        obj.IsSpecimen = lst[0].isSpecimen;

                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppMasterFilePath = "MasterFilePath";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                        string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";

                        string Fnpath = "";
                        if (req.pageCode == "PKGMASINS")
                        {
                            path = path + req.venueNo.ToString() + "/P/";
                        }
                        if (obj.isComments == true)
                        {
                            Fnpath = path + "/Comments/" + lst[0].serviceNo + ".ym";
                            obj.comments = File.ReadAllText(Fnpath);
                        }
                        else
                        {
                            obj.comments = "";
                        }
                        if (obj.isincludeinstruction == true)
                        {
                            Fnpath = path + "/Instruction/" + lst[0].serviceNo + ".ym";
                            obj.includeinstruction = File.ReadAllText(Fnpath);
                        }
                        else
                        {
                            obj.includeinstruction = "";
                        }
                        if (obj.IsSpecimen == true)
                        {
                            Fnpath = path + "/Specimen/" + lst[0].serviceNo + ".ym";
                            obj.Specimen = File.ReadAllText(Fnpath);
                        }
                        else
                        {
                            obj.Specimen = "";
                        }
                        obj.lstgrppkgservice = JsonConvert.DeserializeObject<List<lstgrppkgservice>>(lst[0].grppkgtests);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetEditGroupPackageInstruction ( " + req.pageCode + " ) - " + req.testNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        public List<PrintPackageDetails> GetPrintPakg(reqsearchservice req)
        {
            List<PrintPackageDetails> lst = new List<PrintPackageDetails>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _packageNo = new SqlParameter("PackageNo", req.deptNo);
                    var _venueno = new SqlParameter("VenueNo", req.venueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", req.venueBranchNo);
                    
                    lst = context.GetPrintPakg.FromSqlRaw(
                    "Execute dbo.Pro_GetPackageDetailsByNo @PackageNo,@VenueNo,@VenueBranchNo",
                    _packageNo, _venueno, _venuebranchno).ToList();

                    //
                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    string AppMasterFilePath = "MasterFilePath";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                    string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";

                    path = path + req.venueNo.ToString() + "/P/";

                    if (lst[0].IsComments == true)
                    {
                        string Fnpath = path + "/Comments/" + req.deptNo + ".ym";
                        lst[0].Comments = File.ReadAllText(Fnpath);
                    }
                    else
                    {
                        lst[0].Comments = "";
                    }
                    if (lst[0].IsSpecimen == true)
                    {
                        string Fnpath = path + "/Specimen/" + req.deptNo + ".ym";
                        lst[0].Specimen = File.ReadAllText(Fnpath);
                    }
                    else
                    {
                        lst[0].Specimen = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetPrintPakg", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        public List<GetStatinMasterDetailsRes> GetStatinMasterDetails(GetStatinMasterDetailsReq req)
        {
            List<GetStatinMasterDetailsRes> lst = new List<GetStatinMasterDetailsRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter( "venueNo",req.venueNo);
                    var _pageIndex = new SqlParameter("pageIndex", req.pageIndex);
                    var _searchBy = new SqlParameter("searchBy", req.searchBy);
                    
                    lst = context.GetStainMasterDetails.FromSqlRaw(
                    "Execute dbo.pro_GetStainDetails @venueNo,@pageIndex,@searchBy",
                    _venueNo, _pageIndex, _searchBy).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetStatinMasterDetailsRes", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, 0, 0);
            }
            return lst;
        }
        public StainMasterInsertRes InsertStatinMasterDetails(StainMasterInsertReq req)
        {
            StainMasterInsertRes _res = new StainMasterInsertRes();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _stainId = new SqlParameter("stainId", req.stainId);
                    var _stainName = new SqlParameter("stainName", req.stainName);
                    var _staincode = new SqlParameter("stainCode", req.stainCode);
                    var _description = new SqlParameter("description", req.description);
                    var _status = new SqlParameter("status", req.status);
                    var _venueNo = new SqlParameter("venueNo", req.venueNo);
                    
                    _res = context.insertStainMaster.FromSqlRaw(
                    "Execute dbo.pro_InsertStainDetails @stainId,@stainName,@stainCode,@description,@status,@venueNo",
                    _stainId, _stainName,_staincode,_description,_status,_venueNo).AsEnumerable().First();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestRepository.GetStatinMasterDetailsRes", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, 0, 0);
            }
            return _res;
        }
    }
}
