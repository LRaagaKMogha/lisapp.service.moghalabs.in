using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using DEV.Model.EF.DocumentUpload;
using DEV.Model.Integration;
using DEV.Model.Sample;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DEV.Model.DocumentUploadDTO;

namespace Dev.Repository
{
    public class ResultRepository : IResultRepository
    {
        private IConfiguration _config;
        public ResultRepository(IConfiguration config) { _config = config; }
        public List<lstsearchresultvisit> SearchResultVisit(requestsearchresultvisit req)
        {
            List<lstsearchresultvisit> lst = new List<lstsearchresultvisit>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req.viewvenuebranchno);
                    var _searchby = new SqlParameter("searchby", req.searchby);
                    var _searchtext = new SqlParameter("searchtext", req.searchtext);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    
                    lst = context.SearchResultVisit.FromSqlRaw(
                    "Execute dbo.pro_SearchResultVisit @pagecode,@viewvenuebranchno,@searchby,@searchtext,@venueno,@venuebranchno,@userno",
                    _pagecode, _viewvenuebranchno, _searchby, _searchtext, _venueno, _venuebranchno, _userno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.SearchResultVisit", ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return lst;
        }
        public List<lstresultvisit> GetResultVisit(requestresultvisit req)
        {
            List<lstresultvisit> lst = new List<lstresultvisit>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req.viewvenuebranchno);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _pageindex = new SqlParameter("pageindex", req.pageindex);
                    var _type = new SqlParameter("type", req.type);
                    var _fromdate = new SqlParameter("fromdate", req.fromdate);
                    var _todate = new SqlParameter("todate", req.todate);
                    var _deptno = new SqlParameter("deptno", req.deptno);
                    var _serviceno = new SqlParameter("serviceno", req.serviceno);
                    var _servicetype = new SqlParameter("servicetype", req.servicetype);
                    var _ismachinevalue = new SqlParameter("ismachinevalue", req.ismachinevalue);
                    var _RefferalType = new SqlParameter("RefferalType", req.refferalType);
                    var _CustomerNo = new SqlParameter("CustomerNo", req.customerNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", req.physicianNo);
                    var _RouteNo = new SqlParameter("RouteNo", req.routeNo);
                    var _IsReRunFilter = new SqlParameter("IsReRunFilter", req.isReRunFilter);
                    var _IsRecallFilter = new SqlParameter("IsRecallFilter", req.isRecallFilter);
                    var _IsRecollectFilter = new SqlParameter("IsRecollectFilter", req.isRecollectFilter);
                    var _IsRecheckFilter = new SqlParameter("IsRecheckFilter", req.isRecheckFilter);
                    var _MachineId = new SqlParameter("MachineId", req.machineId);
                    var _maindeptNo = new SqlParameter("maindeptNo", req.maindeptNo);
                    var _testStatusFilterNo = new SqlParameter("TestStatusFilterNo", req.testStatusFilterNo);
                    var _isSTATFilter = new SqlParameter("isSTATFilter", req.isSTATFilter);
                    var _isTATFilter = new SqlParameter("isTATFilter", req.isTATFilter);
                    var _companyNo = new SqlParameter("companyNo", req.companyNo);
                    var _multiDeptNo = new SqlParameter("multiDeptNo", req.multiDeptNo);
                    var _patientno = new SqlParameter("patientno", req.patientno);
                    var _multiFieldsSearch = new SqlParameter("multiFieldsSearch", req.multiFieldsSearch);

                    var rtndblst = context.GetResultVisit.FromSqlRaw(
                    "Execute dbo.pro_GetResultVisit @pagecode,@viewvenuebranchno,@venueno,@venuebranchno,@userno,@pageindex,@type,@fromdate,@todate,@deptno,@serviceno,@servicetype,@ismachinevalue," +
                    "@RefferalType,@CustomerNo,@PhysicianNo,@RouteNo,@IsRecallFilter,@IsRecheckFilter,@IsRecollectFilter,@IsReRunFilter,@MachineId,@maindeptNo, @testStatusFilterNo,@isSTATFilter,@isTATFilter,@companyNo,@multiDeptNo,@patientno,@MultiFieldsSearch",
                    _pagecode, _viewvenuebranchno, _venueno, _venuebranchno, _userno, _pageindex, _type, _fromdate, _todate, _deptno, _serviceno, _servicetype, _ismachinevalue, _RefferalType, _CustomerNo, _PhysicianNo,
                    _RouteNo, _IsRecallFilter, _IsRecheckFilter, _IsRecollectFilter, _IsReRunFilter, _MachineId, _maindeptNo, _testStatusFilterNo, _isSTATFilter, _isTATFilter, _companyNo, _multiDeptNo,_patientno, _multiFieldsSearch).ToList();
                    
                    int patientvisitno = 0;
                    rtndblst = rtndblst.OrderBy(a => a.patientvisitno).ToList();
                    
                    foreach (var v in rtndblst)
                    {
                        if (patientvisitno != v.patientvisitno)
                        {
                            patientvisitno = v.patientvisitno;
                            lstresultvisit obj = new lstresultvisit();
                            obj.patientno = v.patientno;
                            obj.rhNo = v.rhNo;
                            obj.patientvisitno = v.patientvisitno;
                            obj.patientid = v.patientid;
                            obj.fullname = v.fullname;
                            obj.agetype = v.agetype;
                            obj.gender = v.gender;
                            obj.visitid = v.visitid;
                            obj.extenalvisitid = v.extenalvisitid;
                            obj.visitdttm = v.visitdttm;
                            obj.taskdttm = v.taskdttm;
                            obj.referraltype = v.referraltype;
                            obj.customername = v.customername;
                            obj.physicianname = v.physicianname;
                            obj.visStat = v.visStat;
                            obj.visAbnormal = v.visAbnormal;
                            obj.visCritical = v.visCritical;
                            obj.visTAT = v.visTAT;
                            obj.visRemarks = v.visRemarks;
                            obj.visCPRemarks = v.visCPRemarks;
                            obj.totalRecords = v.totalRecords;
                            obj.venueBranchName = v.venueBranchName;
                            obj.nricnumber = v.nricnumber;
                            obj.isVipIndication = v.isVipIndication;
                            obj.isSecondReviewAvail = v.isSecondReviewAvail;
                            var serlst = rtndblst.Where(o => o.patientvisitno == v.patientvisitno).ToList();
                            List<lstservice> lsts = new List<lstservice>();
                            
                            foreach (var s in serlst)
                            {
                                lstservice objs = new lstservice();
                                objs.patientvisitno = s.patientvisitno;
                                objs.orderlistno = s.orderlistno;
                                objs.servicetype = s.servicetype;
                                objs.serviceno = s.serviceno;
                                objs.servicename = s.servicename;
                                objs.resulttypeno = s.resulttypeno;
                                objs.orderliststatus = s.orderliststatus;
                                objs.orderliststatustext = s.orderliststatustext;
                                objs.barcodeno = s.barcodeno;
                                objs.isMachineValue = s.isMachineValue;
                                objs.isRecheck = s.isRecheck;
                                objs.isRecollect = s.isRecollect;
                                objs.isRecall = s.isRecall;
                                objs.isReRun = s.isReRun;
                                objs.isOutSource = s.isOutSource;
                                objs.isAbnormal = s.isAbnormal;
                                objs.isCritical = s.isCritical;
                                objs.isTAT = s.isTAT;
                                objs.isRemarks = s.isRemarks;
                                objs.isCPRemarks = s.isCPRemarks;
                                objs.tATFlag = s.tATFlag;
                                objs.departmentName = s.departmentName;
                                objs.availStatus = s.availStatus;
                                objs.isVipIndication = s.isVipIndication;
                                lsts.Add(objs);
                            }
                            obj.lstservice = lsts;
                            lst.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetResultVisit", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            //return lst.OrderByDescending(x => x.visStat).ToList();//12656
            return lst;
        }
        public List<deltaresult> GetDeltaResult(requestdeltaresult req)
        {
            List<deltaresult> lst = new List<deltaresult>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _patientno = new SqlParameter("patientno", req.patientno);
                    var _testno = new SqlParameter("testno", req.testno);
                    var _subtestno = new SqlParameter("subtestno", req.subtestno);
                    
                    lst = context.GetDeltaResult.FromSqlRaw(
                    "Execute dbo.pro_GetDeltaResult @venueno,@venuebranchno,@userno,@patientno,@testno,@subtestno",
                    _venueno, _venuebranchno, _userno, _patientno, _testno, _subtestno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetDeltaResult" + req.patientno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return lst;
        }
        public objresult GetVisitHistoy(requestdeltaresult req)
        {
            objresult obj = new objresult();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _patientno = new SqlParameter("patientno", req.patientno);
                    var _mrdnumber = new SqlParameter("mrdnumber", req.mrdnumber);
                    var _nricnumber = new SqlParameter("nricnumber", req.nricnumber);
                    
                    var rtndblst = context.GetVisitHistoy.FromSqlRaw(
                    "Execute dbo.pro_GetVisitHistoy @venueno,@venuebranchno,@userno,@patientno,@mrdnumber,@nricnumber",
                    _venueno, _venuebranchno, _userno, _patientno, _mrdnumber, _nricnumber).ToList();

                    int patientvisitno = 0;
                    int orderlistno = 0;

                    rtndblst = rtndblst.OrderBy(a => a.patientvisitno).ToList();
                    List<lstvisit> lstv = new List<lstvisit>();
                    foreach (var v in rtndblst)
                    {
                        if (patientvisitno != v.patientvisitno)
                        {
                            patientvisitno = v.patientvisitno;
                            lstvisit objv = new lstvisit();
                            objv.patientno = v.patientno;
                            objv.patientvisitno = v.patientvisitno;
                            objv.patientid = v.patientid;
                            objv.fullname = v.fullname;
                            objv.agetype = v.agetype;
                            objv.gender = v.gender;
                            objv.visitid = v.visitid;
                            objv.visitdttm = v.visitdttm;
                            objv.referraltype = v.referraltype;
                            objv.customername = v.customername;
                            objv.physicianname = v.physicianname;

                            orderlistno = 0;
                            var ollst = rtndblst.Where(o => o.patientvisitno == v.patientvisitno).ToList();
                            ollst = ollst.OrderBy(d => d.departmentseqno).ThenBy(s => s.serviceseqno).ToList();
                            List<lstorderlist> lstol = new List<lstorderlist>();
                            foreach (var ol in ollst)
                            {
                                if (orderlistno != ol.orderlistno)
                                {
                                    orderlistno = ol.orderlistno;
                                    lstorderlist objol = new lstorderlist();
                                    objol.patientvisitno = ol.patientvisitno;
                                    objol.orderlistno = ol.orderlistno;
                                    objol.departmentname = ol.departmentname;
                                    objol.departmentseqno = ol.departmentseqno;
                                    objol.samplename = ol.samplename;
                                    objol.servicetype = ol.servicetype;
                                    objol.serviceno = ol.serviceno;
                                    objol.servicename = ol.servicename;
                                    objol.serviceseqno = ol.serviceseqno;
                                    objol.resulttypeno = ol.resulttypeno;

                                    if (ol.servicetype == "G")
                                    {
                                        objol.isgrouptd = true;
                                    }
                                    else if (ol.subtestname != "" && ol.servicetype != "G")
                                    {
                                        objol.isgrouptd = true;
                                    }
                                    objol.internotes = "";

                                    var odlst = ollst.Where(o => o.orderlistno == ol.orderlistno).ToList();
                                    odlst = odlst.OrderBy(t => t.tseqno).ThenBy(st => st.subtestno).ToList();
                                    List<lstorderdetail> lstod = new List<lstorderdetail>();
                                    foreach (var t in odlst)
                                    {
                                        lstorderdetail objod = new lstorderdetail();
                                        objod.orderlistno = ol.orderlistno;
                                        objod.orderdetailsno = t.orderdetailsno;
                                        objod.testtype = t.testtype;
                                        objod.testno = t.testno;
                                        objod.testname = t.testname;
                                        objod.tseqno = t.tseqno;
                                        objod.subtestno = t.subtestno;
                                        objod.subtestname = t.subtestname;
                                        objod.sseqno = t.sseqno;
                                        objod.resulttype = t.resulttype;
                                        objod.methodname = t.methodname;
                                        objod.unitname = t.unitname;
                                        objod.displayrr = t.displayrr;
                                        objod.result = t.result;
                                        objod.resultflag = t.resultflag;
                                        objod.resultcomments = t.resultcomments;
                                        objod.internotes = "";
                                        lstod.Add(objod);
                                    }
                                    objol.lstorderdetail = lstod;
                                    lstol.Add(objol);
                                }
                            }
                            objv.lstorderlist = lstol;
                            lstv.Add(objv);
                        }
                    }
                    obj.lstvisit = lstv;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetVisitHistoy" + req.patientno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }
        public async Task<objresult> GetResult(requestresult req)
        {
            objresult obj = new objresult();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _deptno = new SqlParameter("deptno", req.deptno);
                    var _serviceno = new SqlParameter("serviceno", req.serviceno);
                    var _servicetype = new SqlParameter("servicetype", req.servicetype);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req.viewvenuebranchno);

                    var rtndblst = await context.GetResult.FromSqlRaw(
                    "Execute dbo.pro_GetResult @pagecode,@venueno,@venuebranchno,@userno,@patientvisitno,@deptno,@serviceno,@servicetype,@viewvenuebranchno",
                    _pagecode, _venueno, _venuebranchno, _userno, _patientvisitno, _deptno, _serviceno, _servicetype, _viewvenuebranchno).ToListAsync();

                    int patientvisitno = 0;
                    int orderlistno = 0;
                    int id = 0;

                    rtndblst = rtndblst.OrderBy(a => a.patientvisitno).ToList();
                    List<lstvisit> lstv = new List<lstvisit>();
                    foreach (var v in rtndblst)
                    {
                        if (patientvisitno != v.patientvisitno)
                        {
                            patientvisitno = v.patientvisitno;
                            lstvisit objv = new lstvisit();
                            objv.patientno = v.patientno;
                            objv.patientvisitno = v.patientvisitno;
                            objv.patientid = v.patientid;
                            objv.fullname = v.fullname;
                            objv.agetype = v.agetype;
                            objv.gender = v.gender;
                            objv.visitid = v.visitid;
                            objv.extenalvisitid = v.extenalvisitid;
                            objv.visitdttm = v.visitdttm;
                            objv.referraltype = v.referraltype;
                            objv.customername = v.customername;
                            objv.physicianname = v.physicianname;
                            objv.visstat = v.visstat;
                            objv.visremarks = v.visremarks;
                            objv.address = v.address;
                            objv.dob = v.dob;
                            objv.urntype = v.urntype;
                            objv.urnid = v.urnid;
                            objv.samplecollectedon = v.samplecollectedon;
                            objv.enteredon = v.enteredon;
                            objv.validatedon = v.validatedon;
                            objv.approvedon = v.approvedon;
                            objv.NotifyCount = v.notifyCount;
                            objv.venueBranchName = v.venueBranchName;
                            objv.nricNumber = v.nricNumber;
                            objv.isAbnormalAvail = v.isAbnormalAvail;
                            objv.isVipIndication = v.isVipIndication;
                            objv.IsOtherDeptSrvAvailble = v.IsOtherDeptSrvAvailble;
                            objv.allergyInfo = v.allergyInfo;
                            objv.rhNo = v.rhNo;

                            orderlistno = 0;
                            var ollst = rtndblst.Where(o => o.patientvisitno == v.patientvisitno).ToList();
                            ollst = ollst.OrderBy(d => d.departmentseqno).ThenBy(s => s.serviceseqno).ToList();
                            List<lstorderlist> lstol = new List<lstorderlist>();
                            int deptno = 0;
                            foreach (var ol in ollst)
                            {
                                if (orderlistno != ol.orderlistno)
                                {
                                    orderlistno = ol.orderlistno;
                                    lstorderlist objol = new lstorderlist();
                                    objol.patientvisitno = ol.patientvisitno;
                                    objol.orderlistno = ol.orderlistno;
                                    objol.departmentno = ol.departmentno;
                                    objol.departmentname = ol.departmentname;
                                    objol.departmentseqno = ol.departmentseqno;
                                    objol.samplename = ol.samplename;
                                    objol.barcodeno = ol.barcodeno;
                                    objol.servicetype = ol.servicetype;
                                    objol.serviceno = ol.serviceno;
                                    objol.servicename = ol.servicename;
                                    objol.oTTestCode = ol.oTTestCode;
                                    objol.serviceseqno = ol.serviceseqno;
                                    objol.serviceCode = ol.serviceCode;
                                    objol.resulttypeno = ol.resulttypeno;
                                    objol.risrerun = false;
                                    objol.risrecollect = false;
                                    objol.risrecheck = false;
                                    objol.isrecall = ol.isrecall;
                                    objol.isoutsource = ol.isoutsource;
                                    objol.isoutsourceattachment = ol.isoutsourceattachment;
                                    objol.isattachment = ol.isattachment;
                                    objol.isremarks = ol.isremarks;
                                    objol.istat = ol.istat;
                                    objol.iscontestinter = ol.iscontestinter;
                                    objol.groupinter = ol.groupinter;
                                    objol.ischecked = ol.ischecked;
                                    objol.isrerun = ol.isrerun;
                                    objol.isrecollect = ol.isrecollect;
                                    objol.isrecheck = ol.isrecheck;
                                    objol.isMultiEditor = ol.isMultiEditor;
                                    objol.isSecondReview = ol.isSecondReview;
                                    objol.isSecondReviewAvail = ol.isSecondReviewAvail;
                                    objol.isNotify = ol.isNotify;
                                    objol.isVipIndication = ol.isVipIndication;
                                    objol.isLock = ol.isLock;
                                    objol.snomedId = ol.snomedId;
                                    objol.isDC = ol.isDC;
                                    objol.isWBC = ol.isWBC;
                                    objol.ivalue = ol.ivalue;
                                    objol.hvalue = ol.hvalue;
                                    objol.lvalue = ol.lvalue;
                                    objol.isInfectionControl = ol.isInfectionControl;
                                    objol.isPediatricSample = ol.isPediatricSample;
                                    objol.isOBTest = ol.isOBTest;
                                    objol.isOBNegative = ol.isOBNegative;
                                    objol.groupMultiSampleComments = ol.groupMultiSampleComments;
                                    objol.isPermutation = ol.isPermutation;
                                    objol.permutationCode = ol.permutationCode;
                                    objol.permutationCodeType = ol.permutationCodeType;
                                    objol.IsPartialEntryMaster = ol.IsPartialEntryMaster;
                                    objol.IsPartialValidationMaster = ol.IsPartialValidationMaster;
                                    objol.IsPartialEntryTrans = ol.IsPartialEntryTrans;
                                    objol.IsPartialValidationTrans = ol.IsPartialValidationTrans;

                                    // objol.dIComment = ol.dIComment != null && ol.dIComment != "" ? ol.dIComment: objol.dIComment;
                                    if (deptno != objol.departmentno)
                                        objol.isIHLValueAvail = ol.isIHLValueAvail;
                                    deptno = objol.departmentno;
                                    //  objol.isSubTestDeptNotMapd = ol.isSubTestDeptNotMapd;
                                    //non mapped department shown
                                    objol.isDeptAvail = ol.isDeptAvail;
                                    if (objol.isMultiEditor != null && objol.isMultiEditor == 1)
                                    {
                                        //multi editor - single test scenario - saved/drafted details shown in reportstatus, ICMR PatientId, SRF Number, abnormal, critical values
                                        requestresult reqtemplate = new requestresult();
                                        reqtemplate.pagecode = req.pagecode;
                                        reqtemplate.venueno = req.venueno;
                                        reqtemplate.venuebranchno = req.venuebranchno;
                                        reqtemplate.patientvisitno = req.patientvisitno;
                                        reqtemplate.serviceno = objol.serviceno;
                                        reqtemplate.deptno = req.deptno;
                                        reqtemplate.servicetype = objol.servicetype;
                                        objresulttemplate objtemplate = new objresulttemplate();
                                        objtemplate = GetResultTemplate(reqtemplate);
                                        if (objtemplate != null)
                                        {
                                            objol.isabnormal = objtemplate.isabnormal;
                                            objol.iscritical = objtemplate.iscritical;
                                            objol.icmrPatientId = objtemplate.icmrPatientId;
                                            objol.srfNumber = objtemplate.srfNumber;
                                            objol.reportstatus = objtemplate.reportstatus;
                                        }
                                    }
                                    if (ol.servicetype == "G")
                                    {
                                        objol.isgrouptd = true;
                                    }
                                    else if (ol.subtestname != "" && ol.servicetype != "G")
                                    {
                                        objol.isgrouptd = true;
                                    }

                                    if (objol.iscontestinter == false)
                                    {
                                        if (objol.groupinter == 2)
                                        {
                                            //                                            
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppTransFilePath = "TransFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                    ? objAppSettingResponse.ConfigValue : "";
                                            //string path = _config.GetConnectionString(ConfigKeys.TransFilePath);
                                            path = path + req.venueno.ToString() + "/G/InterNotes/" + objol.orderlistno + ".ym";
                                            if (File.Exists(path))
                                            {
                                                objol.internotes = File.ReadAllText(path);
                                            }
                                            else
                                            {
                                                objol.internotes = "";
                                            }
                                        }
                                        else if (objol.groupinter == 1)
                                        {
                                            //                                            
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppMasterFilePath = "MasterFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                    ? objAppSettingResponse.ConfigValue : "";
                                            //string path = _config.GetConnectionString(ConfigKeys.MasterFilePath);
                                            path = path + req.venueno.ToString() + "/G/InterNotes/" + objol.serviceno + ".ym";
                                            if (File.Exists(path))
                                            {
                                                objol.internotes = File.ReadAllText(path);
                                            }
                                            else
                                            {
                                                objol.internotes = "";
                                            }
                                        }
                                        else
                                        {
                                            objol.internotes = "";
                                        }
                                    }
                                    else
                                    {
                                        objol.internotes = "";
                                    }
                                    var odlst = ollst.Where(o => o.orderlistno == ol.orderlistno).ToList();
                                    //odlst = odlst.OrderBy(t => t.tseqno).ThenBy(st => st.subtestno).ToList();
                                    odlst = odlst.OrderBy(t => t.tseqno).ThenBy(st => st.sseqno).ToList();//issue raised in PSH 
                                    List<lstorderdetail> lstod = new List<lstorderdetail>();
                                    foreach (var t in odlst)
                                    {
                                        lstorderdetail objod = new lstorderdetail();
                                        objod.id = id;
                                        id = id + 1;
                                        objod.orderlistno = ol.orderlistno;
                                        objod.orderdetailsno = t.orderdetailsno;
                                        objod.testtype = t.testtype;
                                        objod.testno = t.testno;
                                        objod.testname = t.testname;
                                        objod.oDTestCode = t.oDTestCode;
                                        objod.tseqno = t.tseqno;
                                        objod.subtestno = t.subtestno;
                                        objod.subtestname = t.subtestname;
                                        objod.sseqno = t.sseqno;
                                        objod.resulttype = t.resulttype;
                                        objod.mastermethodno = t.mastermethodno;
                                        objod.masterunitno = t.masterunitno;
                                        objod.masterllcolumn = t.masterllcolumn;
                                        objod.masterhlcolumn = t.masterhlcolumn;
                                        objod.masterdisplayrr = t.masterdisplayrr;
                                        objod.crllcolumn = t.crllcolumn;
                                        objod.crhlcolumn = t.crhlcolumn;
                                        objod.minrange = t.minrange;
                                        objod.maxrange = t.maxrange;
                                        objod.isnonmandatory = t.isnonmandatory;
                                        objod.isdelta = t.isdelta;
                                        objod.istformula = t.istformula;
                                        objod.issformula = t.issformula;
                                        objod.decimalpoint = t.decimalpoint;
                                        objod.isroundoff = t.isroundoff;
                                        objod.isformulaparameter = t.isformulaparameter;
                                        objod.formulaserviceno = t.formulaserviceno;
                                        objod.formulaservicetype = t.formulaservicetype;
                                        objod.formulajson = JsonConvert.DeserializeObject<List<formulajson>>(t.formulajson);
                                        objod.formulaparameterjson = JsonConvert.DeserializeObject<List<formulaparameterjson>>(t.formulaparameterjson);
                                        objod.picklistjson = JsonConvert.DeserializeObject<List<picklistjson>>(t.picklistjson);
                                        objod.headerno = t.headerno;
                                        objod.isedit = t.isedit;
                                        objod.testinter = t.testinter;
                                        objod.methodno = t.methodno;
                                        objod.methodname = t.methodname;
                                        objod.unitno = t.unitno;
                                        objod.unitname = t.unitname;
                                        objod.llcolumn = t.llcolumn;
                                        objod.hlcolumn = t.hlcolumn;
                                        objod.displayrr = t.displayrr;
                                        objod.isMultiEditor = t.isMultiEditor;
                                        objod.statusName = t.statusName;
                                        objod.lesserValue = t.lesserValue;
                                        objod.greaterValue = t.greaterValue;
                                        objod.isNotify = t.isNotify;
                                        objod.calcprevresult = t.calcprevresult;
                                        objod.calcprevresultper = t.calcprevresultper;
                                        objod.calcprevresultdif = t.calcprevresultdif;
                                        objod.isVipIndication = t.isVipIndication;
                                        objod.snomedId = t.snomedId;
                                        objod.isDC = t.isDC;
                                        objod.isWBC = t.isWBC;
                                        objod.isSubTestDeptNotMapd = t.isSubTestDeptNotMapd;
                                        objod.isExtraSubTest = t.isExtraSubTest;
                                        objod.isPCVTest = t.isPCVTest;
                                        objod.isHGBAvail = t.isHGBAvail;
                                        objod.isPTTAvail = t.isPTTAvail;
                                        //objod.pCVCalcRange = t.pCVCalcRange;
                                        //objod.hGBRestrictValue = t.hGBRestrictValue;
                                        //objod.hGBCalcValue = t.hGBCalcValue;
                                        objod.pTTRestrictedValue = t.pTTRestrictedValue;
                                        objod.hGBMessage = t.hGBMessage;
                                        objod.pTTMessage = t.pTTMessage;
                                        objod.isHGBTest = t.isHGBTest;
                                        objod.validFromRange = t.validFromRange;
                                        objod.validToRange = t.validToRange;
                                        objod.isPCVValidation = t.isPCVValidation;
                                        objod.isPttRestrictedValueExists = t.isPttRestrictedValueExists;
                                        objod.ivalue = t.ivalue;
                                        objod.hvalue = t.hvalue;
                                        objod.lvalue = t.lvalue;
                                        objod.isIHLValueAvail = t.isIHLValueAvail;
                                        objod.isDeltaApproval = t.isDeltaApproval;
                                        objod.deltaRange = t.deltaRange;
                                        objod.isDeltaApprovalRestriction = t.isDeltaApprovalRestriction;
                                        objod.approvedprevresult = t.approvedprevresult;
                                        objod.isLogicNeeded = t.isLogicNeeded;
                                        objod.isprevdiresultavail = t.isprevdiresultavail;
                                        objod.isMCHC = t.isMCHC;
                                        objod.isIndRerun = t.isIndRerun;
                                        objol.isrerun = objod.isIndRerun == true ? false : (objol.isrerun == false ? false : objol.isrerun);
                                        objod.logicneededjson = JsonConvert.DeserializeObject<List<logicConceptResponse>>(t.logicneededjson);
                                        objod.isExtraSubtestEnable = t.isExtraSubTestEnable;
                                        objod.isBlast = t.isBlast;
                                        objod.isAMC = t.isAMC;
                                        objod.blasDTTM = t.blasDTTM;
                                        objod.isOBNegative = t.isOBNegative;
                                        objod.isOBTest = t.isOBTest;
                                        objol.dIComment = t.dIComment != null && t.dIComment != "" ? t.dIComment : objol.dIComment;
                                        objod.ObTestCode = t.ObTestCode;
                                        objod.ObPositiveCode = t.ObPositiveCode;
                                        objod.isPermutation = t.isPermutation;
                                        objod.permutationCode = t.permutationCode;
                                        objod.permutationCodeType = t.permutationCodeType;
                                        objod.resultAckType = t.resultAckType;
                                        objod.prevABORHResult = t.prevABORHResult;
                                        objod.prevDIResults = JsonConvert.DeserializeObject<List<DIResult>>(t.prevDIResults);
                                        if (t.isMultiEditor == 1)
                                        {
                                            //                                            
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppTransTemplateFilePath = "TransTemplateFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                    ? objAppSettingResponse.ConfigValue : "";
                                            //string path = _config.GetConnectionString(ConfigKeys.TransTemplateFilePath);
                                            path = path + req.venueno.ToString() + "/" + objod.orderlistno.ToString() + "/" + objod.testno.ToString() + "/" + objod.subtestno.ToString() + ".ym";

                                            if (File.Exists(path))
                                            {
                                                string content = File.ReadAllText(path);
                                                objod.result = content;
                                            }
                                            else
                                            {
                                                objod.result = "";
                                            }
                                        }
                                        else
                                        {
                                            objod.result = t.result;
                                            if (t.result != "" && objol.ischecked == false)
                                            {
                                                objol.ischecked = true;
                                            }
                                        }
                                        objod.formularesult = t.formularesult;
                                        objod.diresult = t.diresult;
                                        objod.resultflag = t.resultflag;
                                        objod.resultcomments = t.resultcomments;
                                        objod.risrerunod = false;
                                        objod.isrerunod = t.isrerunod;
                                        objod.isUploadOption = t.isUploadOption;
                                        objod.uploadedfile = t.uploadedFile;
                                        objod.approvalDoctor = t.approvalDoctor;
                                        objol.isnoresult = t.isnoresult;
                                        objod.noresult = t.isnoresult;
                                        objod.isPBFTest = t.isPBFTest;
                                        objol.isUploadOption = objol.servicetype =="G" && t.isUploadOption == true ? true :false;
                                        if (objod.isPBFTest == true && (objod.result == null || objod.result == ""))
                                        {
                                            PBFTestRequest objj = new PBFTestRequest();
                                            PBFTestResponse outobj = new PBFTestResponse();
                                            objj.venueno = req.venueno;
                                            objj.venuebranchno = req.venuebranchno;
                                            objj.patientvisitno = objol.patientvisitno;
                                            objj.orderdetailsno = objod.orderdetailsno;
                                            outobj = GetPBFAutoComment(objj);
                                            if (outobj != null && outobj.status == 1) {
                                                objod.result = outobj.resultComment;                                                
                                            }
                                        }
                                        if (objol.iscontestinter == true)
                                        {
                                            if (objod.testinter == 2)
                                            {
                                                objAppSettingResponse = new AppSettingResponse();
                                                string AppTransFilePath = "TransFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";
                                                // string path = _config.GetConnectionString(ConfigKeys.TransFilePath);
                                                path = path + req.venueno.ToString() + "/T/InterNotes/" + objod.orderdetailsno + ".ym";
                                                if (File.Exists(path))
                                                {
                                                    objod.internotes = File.ReadAllText(path);
                                                }
                                                else
                                                {
                                                    objod.internotes = "";
                                                }
                                            }
                                            else if (objod.testinter == 1)
                                            {
                                                objAppSettingResponse = new AppSettingResponse();
                                                string AppMasterFilePath = "MasterFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";
                                                //string path = _config.GetConnectionString(ConfigKeys.MasterFilePath);
                                                path = path + req.venueno.ToString() + "/T/InterNotes/" + objod.testno + ".ym";
                                                if (File.Exists(path))
                                                {
                                                    objod.internotes = File.ReadAllText(path);
                                                }
                                                else
                                                {
                                                    objod.internotes = "";
                                                }
                                                objAppSettingResponse = new AppSettingResponse();
                                                AppMasterFilePath = "MasterFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                                string Fhpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";
                                                //string Fhpath = _config.GetConnectionString(ConfigKeys.MasterFilePath);
                                                Fhpath = Fhpath + req.venueno.ToString() + "/T/InterNotes/" + objod.testno + "_H" + ".ym";
                                                if (File.Exists(Fhpath))
                                                {
                                                    objod.interNotesHigh = File.ReadAllText(Fhpath);
                                                }
                                                else
                                                {
                                                    objod.interNotesHigh = "";
                                                }
                                                objAppSettingResponse = new AppSettingResponse();
                                                AppMasterFilePath = "MasterFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                                string Flpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";
                                                //string Flpath = _config.GetConnectionString(ConfigKeys.MasterFilePath);
                                                Flpath = Flpath + req.venueno.ToString() + "/T/InterNotes/" + objod.testno + "_L" + ".ym";
                                                if (File.Exists(Flpath))
                                                {
                                                    objod.interNotesLow = File.ReadAllText(Flpath);
                                                }
                                                else
                                                {
                                                    objod.interNotesLow = "";
                                                }
                                            }
                                            else
                                            {
                                                objod.internotes = "";
                                            }
                                        }
                                        else
                                        {
                                            objod.internotes = "";
                                        }

                                        objod.prevresult = t.prevresult;
                                        objod.prevresultrefrange = String.Empty;
                                        objod.prevresultdttm = String.Empty;
                                        string prevrefrange = t.prevresultrefrange;
                                        objod.prevresultdttm = prevrefrange != null ? prevrefrange.Split("$$")[1] : "";
                                        objod.prevresultrefrange = prevrefrange != null ? prevrefrange.Split("$$")[0] : "";
                                        //non mapped department shown
                                        objod.isDeptAvail = t.isDeptAvail;
                                        objod.isSensitiveData = t.isSensitiveData;
                                        objod.subtestDeptNo = t.subtestDeptNo;
                                        objod.isAbnormalRemove = t.isAbnormalRemove;
                                        if (t.isDeptAvail > 1)
                                        {
                                            objol.ischecked = false;
                                        }
                                        lstod.Add(objod);
                                    }
                                    objol.lstorderdetail = lstod;
                                    lstol.Add(objol);
                                }
                            }
                            objv.lstorderlist = lstol;
                            lstv.Add(objv);
                        }
                    }
                    obj.lstvisit = lstv;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetResult - " + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }

        public async Task<resultrtn> InsertResult(objresult req)
        {
            resultrtn obj = new resultrtn();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    XDocument odxml = new XDocument();
                    XElement xodtbl = new XElement("odtbl");

                    foreach (var vst in req.lstvisit)
                    {
                        int oldserviceno = 0;
                        foreach (var ol in vst.lstorderlist)
                        {
                            //for draft - we need to allow empty result's as well also
                            if (req.action=="SD" || (ol.ischecked == true || ol.risrerun == true || ol.risrecollect == true || ol.risrecheck == true || ol.isnoresult == true || (ol.isLock == true || ol.isLockChanged == true)))
                            {
                                int oldtestno = 0;
                                foreach (var od in ol.lstorderdetail)
                                {
                                    if (od.resulttype != "C" && od.resulttype != "TE" && 
                                        ((req.pagecode == "PCRA" && od.ischecked == true || ol.risrerun == true || ol.risrecheck == true || (ol.isLock == true || ol.isLockChanged == true)) ||
                                        (req.pagecode != "PCRA" && od.ischecked == true || ol.risrecollect == true || ol.isnoresult == true || (ol.isLock == true || ol.isLockChanged == true))))
                                    {
                                        XElement xrow = new XElement("row",
                                        new XElement("patientvisitno", ol.patientvisitno),
                                        new XElement("orderlistno", ol.orderlistno),
                                        new XElement("ischecked", ol.ischecked),
                                        new XElement("islock", ol.isLock),
                                        new XElement("isrerun", ol.risrerun),
                                        new XElement("isrecollect", ol.risrecollect),
                                        new XElement("isrecheck", ol.risrecheck),
                                        new XElement("isnoresult", od.noresult),
                                        //new XElement("isoledit", ol.isoledit),
                                        new XElement("groupinter", ol.groupinter),
                                        new XElement("isattachment", ol.isattachment),
                                        new XElement("resulttypeno", ol.resulttypeno),
                                        new XElement("orderdetailsno", od.orderdetailsno),
                                        new XElement("testtype", od.testtype),
                                        new XElement("testno", od.testno),
                                        new XElement("testname", od.testname),
                                        new XElement("tseqno", od.tseqno),
                                        new XElement("subtestno", od.subtestno),
                                        new XElement("subtestname", od.subtestname),
                                        new XElement("sseqno", od.sseqno),
                                        new XElement("methodno", od.methodno),
                                        new XElement("methodname", od.methodname),
                                        new XElement("unitno", od.unitno),
                                        new XElement("unitname", od.unitname),
                                        new XElement("llcolumn", od.llcolumn),
                                        new XElement("hlcolumn", od.hlcolumn),
                                        new XElement("displayrr", od.displayrr),
                                        new XElement("result", od.result),
                                        new XElement("formularesult", od.formularesult),
                                        new XElement("diresult", od.diresult),
                                        new XElement("resultflag", od.resultflag),
                                        new XElement("resulttype", od.resulttype),
                                        new XElement("headerno", od.headerno),
                                        new XElement("resultcomments", od.resultcomments),
                                        new XElement("isedit", od.isedit),
                                        new XElement("testinter", od.testinter),
                                        new XElement("internotes", ""),
                                        new XElement("risrerunod", od.risrerunod),
                                        new XElement("uploadedpath", od.uploadedfile),
                                        new XElement("approvalDoctor", od.approvalDoctor),
                                        new XElement("isSecondReview", ol.isSecondReview),
                                        new XElement("snomedId", ol.snomedId),
                                        new XElement("ivalue", od.ivalue),
                                        new XElement("hvalue", od.hvalue),
                                        new XElement("lvalue", od.lvalue),
                                        new XElement("isextrasubtestenable", od.isExtraSubtestEnable),
                                        new XElement("isPediatricSample", ol.isPediatricSample),
                                        new XElement("groupMultiSampleComments", ol.groupMultiSampleComments),
                                        new XElement("isPartialEntryTrans", ol.IsPartialEntryTrans),
                                        new XElement("isPartialValidationTrans", ol.IsPartialValidationTrans),
                                        new XElement("isAbnormalRemove", od.isAbnormalRemove)
                                        );
                                        xodtbl.Add(xrow);

                                        if (ol.iscontestinter == false)
                                        {
                                            if (ol.groupinter == 2 && oldserviceno != ol.serviceno)
                                            {
                                                oldserviceno = ol.serviceno;

                                                objAppSettingResponse = new AppSettingResponse();
                                                string AppTransFilePath = "TransFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";
                                                path = path + req.venueno.ToString() + "/G/InterNotes/";

                                                if (!Directory.Exists(path))
                                                {
                                                    Directory.CreateDirectory(path);
                                                }
                                                string createText = ol.internotes + Environment.NewLine;
                                                File.WriteAllText(path + ol.orderlistno + ".ym", createText);
                                            }
                                        }
                                        else if (ol.iscontestinter == true)
                                        {
                                            if (od.testinter == 2 && oldtestno != od.testno)
                                            {
                                                oldtestno = od.testno;

                                                objAppSettingResponse = new AppSettingResponse();
                                                string AppTransFilePath = "TransFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";
                                                path = path + req.venueno.ToString() + "/T/InterNotes/";

                                                if (!Directory.Exists(path))
                                                {
                                                    Directory.CreateDirectory(path);
                                                }
                                                var internotespath = od.resultflag == "H" ? od.interNotesHigh : od.resultflag == "L" ? od.interNotesLow : od.internotes;
                                                string createText = internotespath + Environment.NewLine;
                                                File.WriteAllText(path + od.orderdetailsno + ".ym", createText);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    odxml.Add(xodtbl);
                    req.odxml = odxml.ToString();

                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _action = new SqlParameter("action", req.action);
                    var _odxml = new SqlParameter("odxml", req.odxml);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);

                    var lst = await context.InsertResult.FromSqlRaw(
                    "Execute dbo.pro_InsertResult @pagecode, @action, @odxml, @venueno, @venuebranchno, @userno",
                    _pagecode, _action, _odxml, _venueno, _venuebranchno, _userno).ToListAsync();

                    obj = lst[0];

                    if (req.pagecode == "PCRA" && req.action == "SV" && obj.patientvisitno > 0)
                    {
                        int output = await PushDueMessage(obj.patientvisitno, req.venueno, req.venuebranchno, req.userno, req.pagecode, "1", req.lstvisit[0].fullname);
                        if (output == 0)
                        {
                            await PushMessage(obj.patientvisitno, req.venueno, req.venuebranchno, req.userno, req.pagecode, "1", req.lstvisit[0].fullname);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.InsertResult - " + req.lstvisit.FirstOrDefault().patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }

            return obj;
        }
        private async Task<int> PushDueMessage(int patientVisitNo, int venueno, int venuebranchno, int userno, string pagecode, string resulttypenos, string fullname)
        {
            int result = 0;
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", patientVisitNo);
                    var _venueno = new SqlParameter("VenueNo", venueno);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", venuebranchno);
                    var _Type = new SqlParameter("Type", 1);
                    var lst = context.GetCustomerMsgDetails.FromSqlRaw(
                   "Execute dbo.Pro_GetCustomerNotification @PatientVisitNo,@VenueNo,@VenueBranchNo,@Type", _PatientVisitNo, _venueno, _venuebranchno, _Type).ToList();
                    foreach (var item in lst)
                    {
                        if (!string.IsNullOrEmpty(item.Address))
                        {
                            ReportRequestDTO PatientItem = new ReportRequestDTO();
                            FrontOfficeRepository objRepository = new FrontOfficeRepository(_config);
                            PatientItem.visitNo = Convert.ToInt32(patientVisitNo);
                            PatientItem.userNo = userno;
                            PatientItem.VenueNo = venueno;
                            PatientItem.VenueBranchNo = venuebranchno;
                            ReportOutput data = await objRepository.PrintBill(PatientItem);
                            NotificationDto objDTO = new NotificationDto();
                            CommonRepository objCommonRepository = new CommonRepository(_config);
                            objDTO.Address = item.Address;
                            objDTO.MessageType = item.MessageType;
                            objDTO.TemplateKey = "Patient_Approve_Due" + item.MessageType + "";
                            objDTO.VenueNo = venueno;
                            objDTO.VenueBranchNo = venuebranchno;
                            objDTO.UserNo = userno;
                            objDTO.ScheduleTime = DateTime.Now;
                            ////
                            MasterRepository _IMasterRepository = new MasterRepository(_config);
                            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                            objAppSettingResponse = new AppSettingResponse();
                            string AppFireBaseAPIkey = "FireBaseAPIkey";
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppFireBaseAPIkey);
                            string firebaseapikey = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                    ? objAppSettingResponse.ConfigValue : "";

                            string shortURL = await CommonHelper.URLShorten(data.PatientExportFile, firebaseapikey);
                            Dictionary<string, string> objMessageItem = new Dictionary<string, string>();
                            objMessageItem.Add("#PaitentName#", item.FullName);
                            objMessageItem.Add("#VisitID#", item.VisitID);
                            objMessageItem.Add("#DueAmount#", item.DueAmount.ToString());
                            objMessageItem.Add("#URL#", shortURL);
                            objDTO.MessageItem = objMessageItem;
                            objDTO.IsAttachment = true;
                            objDTO.PatientVisitNo = Convert.ToInt32(patientVisitNo);
                            Dictionary<string, string> objAttachment = new Dictionary<string, string>();
                            if (item.MessageType == "WSMS")
                                objAttachment.Add(item.FullName + ".pdf", data.PatientExportFile);
                            else
                                objAttachment.Add(item.VisitID + ".pdf", data.PatientExportFolderPath);
                            if (item.Isembed) objAttachment.Add(Path.GetFileName(item.EmbedURL), item.EmbedURL);
                            objDTO.AttachmentItem = objAttachment;
                            objCommonRepository.SendMessage(objDTO);
                            result = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.PushDueMessage" + patientVisitNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, venueno, venuebranchno, userno);
            }
            return result;
        }

        private async Task<int> PushMessage(int patientVisitNo, int venueno, int venuebranchno, int userno, string pagecode, string resulttypenos, string fullname)
        {
            int result = 0;
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", patientVisitNo);
                    var _venueno = new SqlParameter("VenueNo", venueno);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", venuebranchno);
                    
                    var lst = context.GetCustomerMsgDetails.FromSqlRaw(
                    "Execute dbo.Pro_GetCustomerNotification @PatientVisitNo,@VenueNo,@VenueBranchNo", 
                    _PatientVisitNo, _venueno, _venuebranchno).ToList();
                    
                    foreach (var item in lst)
                    {
                        if (!string.IsNullOrEmpty(item.Address))
                        {
                            PatientReportDTO PatientItem = new PatientReportDTO();
                            PatientReportRepository objRepository = new PatientReportRepository(_config);
                            PatientItem.fullname = fullname.Replace(".", "");
                         
                            if (venuebranchno == 2 || venuebranchno == 6)
                            {
                                if (item.MessageType.ToLower() == "sms")
                                    PatientItem.isheaderfooter = true;
                                else
                                    PatientItem.isheaderfooter = false;
                            }
                            else
                            {
                                PatientItem.isheaderfooter = true;
                            }

                            PatientItem.orderlistnos = "";
                            PatientItem.pagecode = "PCPR";
                            PatientItem.patientvisitno = patientVisitNo.ToString();
                            PatientItem.process = 2;
                            PatientItem.resulttypenos = resulttypenos;
                            PatientItem.userno = userno;
                            PatientItem.venueno = venueno;
                            PatientItem.venuebranchno = venuebranchno;
                            List<ReportOutput> data = await objRepository.PrintPatientReport(PatientItem);
                            NotificationDto objDTO = new NotificationDto();
                            CommonRepository objCommonRepository = new CommonRepository(_config);
                            objDTO.Address = item.Address;
                            objDTO.MessageType = item.MessageType;
                            objDTO.TemplateKey = "Patient_Approve_" + item.MessageType + "";
                            objDTO.VenueNo = venueno;
                            objDTO.VenueBranchNo = venuebranchno;
                            objDTO.UserNo = userno;
                            objDTO.ScheduleTime = DateTime.Now;
                            ////                            
                            objAppSettingResponse = new AppSettingResponse();
                            string AppFireBaseAPIkey = "FireBaseAPIkey";
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppFireBaseAPIkey);
                            string firebaseapikey = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                    ? objAppSettingResponse.ConfigValue : "";

                            string shortURL = await CommonHelper.URLShorten(data[0].PatientExportFile, firebaseapikey);
                            Dictionary<string, string> objMessageItem = new Dictionary<string, string>();
                            objMessageItem.Add("#PaitentName#", item.FullName);
                            objMessageItem.Add("#VisitID#", item.VisitID);
                            objMessageItem.Add("#URL#", shortURL);
                            objDTO.MessageItem = objMessageItem;
                            objDTO.IsAttachment = true;
                            objDTO.PatientVisitNo = Convert.ToInt32(patientVisitNo);
                            Dictionary<string, string> objAttachment = new Dictionary<string, string>();
                            if (item.MessageType == "WSMS")
                                objAttachment.Add(item.FullName + ".pdf", data[0].PatientExportFile);
                            else
                                objAttachment.Add(item.VisitID + ".pdf", data[0].PatientExportFolderPath);
                            if (item.Isembed) objAttachment.Add(Path.GetFileName(item.EmbedURL), item.EmbedURL);
                            //bill attachment added for bansal                            
                            ConfigurationDto objConfigurationDTO = new ConfigurationDto();
                            //                            
                            objAppSettingResponse = new AppSettingResponse();
                            string AppIsReportWithBilAtach = "IsReportWithBilAtach";
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppIsReportWithBilAtach);

                            string billattachwithreportconfig = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : ""; //_config.GetConnectionString(ConfigKeys.IsReportWithBilAtach);
                            objConfigurationDTO = _IMasterRepository.GetSingleConfiguration(venueno, venuebranchno, billattachwithreportconfig);
                            if (objConfigurationDTO != null && objConfigurationDTO.ConfigValue == 1)
                            {
                                ReportRequestDTO PatientItemn = new ReportRequestDTO();
                                FrontOfficeRepository objRepositoryn = new FrontOfficeRepository(_config);
                                PatientItemn.visitNo = Convert.ToInt32(patientVisitNo);
                                PatientItemn.userNo = userno;
                                PatientItemn.VenueNo = venueno;
                                PatientItemn.VenueBranchNo = venuebranchno;
                                ReportOutput datan = await objRepositoryn.PrintBill(PatientItemn);
                                //                            
                                objAppSettingResponse = new AppSettingResponse();
                                AppFireBaseAPIkey = "FireBaseAPIkey";
                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppFireBaseAPIkey);
                                string sFireBaseAPIkey = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";

                                string shortURLn = await CommonHelper.URLShorten(datan.PatientExportFile, sFireBaseAPIkey);
                                objMessageItem = new Dictionary<string, string>();
                                objMessageItem.Add("#PaitentName#", item.FullName);
                                objMessageItem.Add("#VisitID#", item.VisitID);
                                objMessageItem.Add("#URL#", shortURLn);
                                objDTO.MessageItem = objMessageItem;
                                objDTO.IsAttachment = true;
                                objDTO.PatientVisitNo = Convert.ToInt32(patientVisitNo);
                                
                                if (item.MessageType == "WSMS")
                                    objAttachment.Add(item.FullName + "_Bill" + ".pdf", datan.PatientExportFile);
                                else
                                    objAttachment.Add(item.VisitID + "_Bill" + ".pdf", datan.PatientExportFolderPath);

                                if (item.Isembed) objAttachment.Add(Path.GetFileName(item.EmbedURL), item.EmbedURL);
                            }

                            objDTO.AttachmentItem = objAttachment;
                            objCommonRepository.SendMessage(objDTO);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.PushMessage - " + patientVisitNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, venueno, venuebranchno, userno);
            }
            return result;
        }

        public objresultmb GetResultMB(requestresult req)
        {
            objresultmb obj = new objresultmb();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _deptno = new SqlParameter("deptno", req.deptno);
                    var _serviceno = new SqlParameter("serviceno", req.serviceno);
                    var _servicetype = new SqlParameter("servicetype", req.servicetype);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req.viewvenuebranchno);
                    var rtndblst = context.GetResultMB.FromSqlRaw(
                        "Execute dbo.pro_GetResultMB @pagecode,@venueno,@venuebranchno,@patientvisitno,@deptno,@serviceno,@servicetype,@userno,@viewvenuebranchno",
                        _pagecode, _venueno, _venuebranchno, _patientvisitno, _deptno, _serviceno, _servicetype, _userno,_viewvenuebranchno).ToList();

                    var v = rtndblst[0];
                    obj.patientno = v.patientno;
                    obj.patientvisitno = v.patientvisitno;
                    obj.patientid = v.patientid;
                    obj.fullname = v.fullname;
                    obj.agetype = v.agetype;
                    obj.gender = v.gender;
                    obj.visitid = v.visitid;
                    obj.extenalvisitid = v.extenalvisitid;
                    obj.visitdttm = v.visitdttm;
                    obj.venueBranchName = v.venueBranchName;
                    obj.referraltype = v.referraltype;
                    obj.customername = v.customername;
                    obj.AllergyInfo = v.AllergyInfo;
                    obj.physicianname = v.physicianname;
                    obj.visstat = v.visstat;
                    obj.visremarks = v.visremarks;
                    obj.dob = v.dob;
                    obj.urntype = v.urntype;
                    obj.urnid = v.urnid;
                    obj.samplecollectedon = v.samplecollectedon;
                    obj.enteredon = v.enteredon;
                    obj.validatedon = v.validatedon;
                    obj.approvedon = v.approvedon;

                    obj.orderlistno = v.orderlistno;
                    obj.departmentno = v.departmentno;
                    obj.departmentname = v.departmentname;
                    obj.departmentseqno = v.departmentseqno;
                    obj.samplename = v.samplename;
                    obj.barcodeno = v.barcodeno;
                    obj.servicetype = v.servicetype;
                    obj.serviceno = v.serviceno;
                    obj.servicename = v.servicename;
                    obj.serviceseqno = v.serviceseqno;
                    obj.risrerun = false;
                    obj.risrecollect = false;
                    obj.risrecheck = false;
                    obj.isrecall = v.isrecall;
                    obj.isoutsource = v.isoutsource;
                    obj.isoutsourceattachment = v.isoutsourceattachment;
                    obj.isattachment = v.isattachment;
                    obj.oisremarks = v.oisremarks;
                    obj.oistat = v.oistat;
                    obj.isoledit = v.isoledit;
                    obj.isabnormal = v.isabnormal;
                    obj.iscritical = v.iscritical;
                    obj.ischecked = v.ischecked;
                    obj.isrerun = v.risrerun;
                    obj.isrecollect = v.risrecollect;
                    obj.isrecheck = v.risrecheck;

                    obj.reportstatus = v.reportstatus;
                    obj.resultstatus = v.resultstatus;
                    obj.resultpattern = v.resultpattern;
                    obj.patterndescription = v.patterndescription;
                    obj.colonycount = v.colonycount;
                    obj.wetpreparation = v.wetpreparation;
                    obj.lstgramstainmb = JsonConvert.DeserializeObject<List<lstgramstainmb>>(v.gramstainjson);
                    obj.lstgramstainmbbottle = JsonConvert.DeserializeObject<List<lstgramstainmbbottle>>(v.gramstainbottlejson);
                    obj.gramstaintext = v.gramstaintext;
                    obj.gramstainbottletext = v.gramstainbottletext;
                    obj.comments = v.comments;
                    obj.approvalDoctor = v.approvalDoctor;
                    obj.nricnumber = v.nricnumber;
                    obj.isSensitiveData = v.isSensitiveData;
                    obj.isSecondReviewAvail = v.isSecondReviewAvail;
                    obj.isSecondReview = v.isSecondReview;
                    obj.isLock = v.isLock;
                    obj.snomedId = v.snomedId;
                    obj.isInfectionControl = v.isInfectionControl;
                    obj.SrcOfSpecimenNo = v.SrcOfSpecimenNo;
                    obj.SrcOfSpecimenDesc = v.SrcOfSpecimenDesc;
                    obj.SrcOfSpecimenOthers = v.SrcOfSpecimenOthers;
                    obj.isNoMicroOrgSeen = v.isNoMicroOrgSeen;
                    obj.CCUnitNo = v.CCUnitNo;
                    obj.UnitText = v.UnitText;
                    obj.ColonyCountText = v.ColonyCountText;

                    List<lstorg> lstorg = new List<lstorg>();
                    if (v.mbdrugjson == "")
                    {
                    }
                    else
                    {
                        var lstmbdrugjson = JsonConvert.DeserializeObject<List<lstmbdrugjson>>(v.mbdrugjson);
                        var orgno = 0;
                        var orgnonew = 0;
                        //lstmbdrugjson = lstmbdrugjson.OrderBy(t => t.osequenceno).ToList();
                        foreach (var o in lstmbdrugjson)
                        {
                            if (orgno != o.organismno || orgnonew != o.orgno)
                            {
                                orgno = o.organismno;
                                orgnonew = o.orgno;
                                lstorg objorg = new lstorg();
                                objorg.organismtypeno = o.organismtypeno;
                                objorg.organismno = o.organismno;
                                objorg.organismmccode = o.organismmccode;
                                objorg.organismname = o.organismname;
                                objorg.sequenceno = o.osequenceno;
                                objorg.notes = o.notes;
                                objorg.interptype = o.interptype;
                                objorg.resultpattern = o.resultpattern;
                                objorg.resultpatterntext = o.resultpatterntext;
                                objorg.colonycount = o.colonycount;
                                objorg.colonycounttext = o.colonycounttext;
                                objorg.orgbasednotes = o.orgbasednotes;
                                objorg.orgno = o.orgno;
                                objorg.isInterface = o.isInterface;
                                var druglst = lstmbdrugjson.Where(d => d.organismno == o.organismno && d.orgno == o.orgno).ToList();
                                druglst = druglst.OrderBy(t => t.asequenceno).ToList();
                                List<lstdrug> lstdrug = new List<lstdrug>();
                                foreach (var dl in druglst)
                                {
                                    lstdrug objdrug = new lstdrug();
                                    objdrug.organismno = dl.organismno;
                                    objdrug.antibioticno = dl.antibioticno;
                                    objdrug.antibioticmccode = dl.antibioticmccode;
                                    objdrug.antibioticname = dl.antibioticname;
                                    objdrug.sequenceno = dl.asequenceno;
                                    objdrug.interp = dl.interp;
                                    objdrug.isshow = dl.isshow;
                                    objdrug.interpvalue = dl.interpvalue;
                                    objdrug.orgType = dl.orgType;
                                    lstdrug.Add(objdrug);
                                }
                                objorg.lstdrug = lstdrug;
                                lstorg.Add(objorg);
                            }
                        }
                    }
                    obj.lstorg = lstorg;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetResultMB" + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }

        public resultrtn InsertResultMB(objresultmb req)
        {
            resultrtn obj = new resultrtn();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _action = new SqlParameter("action", req.action);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);

                    var _isabnormal = new SqlParameter("isabnormal", req.isabnormal);
                    var _iscritical = new SqlParameter("iscritical", req.iscritical);
                    var _ischecked = new SqlParameter("ischecked", req.ischecked);
                    var _isrerun = new SqlParameter("isrerun", req.risrerun);
                    var _isrecollect = new SqlParameter("isrecollect", req.risrecollect);
                    var _isrecheck = new SqlParameter("isrecheck", req.risrecheck);
                    var _isattachment = new SqlParameter("isattachment", req.isattachment);

                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _orderlistno = new SqlParameter("orderlistno", req.orderlistno);
                    var _serviceno = new SqlParameter("serviceno", req.serviceno);
                    var _reportstatus = new SqlParameter("reportstatus", req.reportstatus);
                    var _resultstatus = new SqlParameter("resultstatus", req.resultstatus);
                    var _resultpattern = new SqlParameter("resultpattern", req.resultpattern);
                    var _patterndescription = new SqlParameter("patterndescription", req.patterndescription);
                    var _colonycount = new SqlParameter("colonycount", req.colonycount);
                    var _wetpreparation = new SqlParameter("wetpreparation", req.wetpreparation);
                    string gramstainjson = JsonConvert.SerializeObject(req.lstgramstainmb);
                    var _gramstainjson = new SqlParameter("gramstainjson", gramstainjson);
                    string gramstainbottlejson = JsonConvert.SerializeObject(req.lstgramstainmbbottle);
                    var _gramstainbottlejson = new SqlParameter("gramstainbottlejson", gramstainbottlejson);
                    var _gramstaintext = new SqlParameter("gramstaintext", req.gramstaintext);
                    var _gramstainbottletext = new SqlParameter("gramstainbottletext", req.gramstainbottletext);
                    var _comments = new SqlParameter("comments", req.comments);
                    var _approvalDoctor = new SqlParameter("approvalDoctor", req.approvalDoctor);
                    var _isSecondReviewAvail = new SqlParameter("isSecondReviewAvail", req.isSecondReview);
                    var _isLock = new SqlParameter("isLock", req.isLock);
                    var _snomedId = new SqlParameter("snomedId", req.snomedId);
                    var _SrcOfSpecimenNo = new SqlParameter("SrcOfSpecimenNo", req.SrcOfSpecimenNo);
                    var _SrcOfSpecimenDesc = new SqlParameter("SrcOfSpecimenDesc", req.SrcOfSpecimenDesc);
                    var _SrcOfSpecimenOthers = new SqlParameter("SrcOfSpecimenOthers", req.SrcOfSpecimenOthers);
                    var _NoMicroOrgSeen = new SqlParameter("NoMicroOrgSeen", req.isNoMicroOrgSeen);
                    var _CCUnitNo = new SqlParameter("CCUnitNo", req.CCUnitNo);
                    var _UnitText = new SqlParameter("UnitText", req.UnitText);
                    var _ColonyCountText = new SqlParameter("ColonyCountText", req.ColonyCountText);

                    XDocument odxml = new XDocument();
                    XElement xorgantitbl = new XElement("organtitbl");
                    if (req.lstorg.Count > 0)
                    {
                        foreach (var org in req.lstorg)
                        {
                            var druglst = org.lstdrug.Where(d => d.antibioticno > 0).ToList();
                            if (druglst.Count == 0)
                            {
                                XElement xrow = new XElement("row",
                                        new XElement("patientvisitno", req.patientvisitno),
                                        new XElement("orderlistno", req.orderlistno),
                                        new XElement("organismtypeno", org.organismtypeno),
                                        new XElement("organismno", org.organismno),
                                        new XElement("organismmccode", org.organismmccode),
                                        new XElement("organismname", org.organismname),
                                        new XElement("orgsequenceno", org.sequenceno),
                                        new XElement("notes", org.notes),
                                        new XElement("interptype", org.interptype),
                                        new XElement("resultpattern", org.resultpattern),
                                        new XElement("colonycount", org.colonycount),
                                        new XElement("orgbasednotes", org.orgbasednotes),
                                        new XElement("antibioticno", 0),
                                        new XElement("antibioticmccode", 0),
                                        new XElement("antibioticname", ""),
                                        new XElement("antsequenceno", 0),
                                        new XElement("interp", 0),
                                        new XElement("isshow", 0),
                                        new XElement("interpvalue", ""),
                                         new XElement("isreportview", "0"),
                                      new XElement("orgno", org.orgno),
                                      new XElement("isInterface", org.isInterface),
                                      new XElement("orgType", "")

                                        );
                                xorgantitbl.Add(xrow);
                            }
                            else
                            {
                                foreach (var ant in druglst)
                                {
                                    XElement xrow = new XElement("row",
                                      new XElement("patientvisitno", req.patientvisitno),
                                      new XElement("orderlistno", req.orderlistno),
                                      new XElement("organismtypeno", org.organismtypeno),
                                      new XElement("organismno", org.organismno),
                                      new XElement("organismmccode", org.organismmccode),
                                      new XElement("organismname", org.organismname),
                                      new XElement("orgsequenceno", org.sequenceno),
                                      new XElement("notes", org.notes),
                                      new XElement("interptype", org.interptype),
                                      new XElement("resultpattern", org.resultpattern),
                                      new XElement("colonycount", org.colonycount),
                                      new XElement("orgbasednotes", org.orgbasednotes),
                                      new XElement("antibioticno", ant.antibioticno),
                                      new XElement("antibioticmccode", ant.antibioticmccode),
                                      new XElement("antibioticname", ant.antibioticname),
                                      new XElement("antsequenceno", ant.sequenceno),
                                       new XElement("interp", ant.interp),
                                      new XElement("isshow", ant.isshow),
                                      new XElement("interpvalue", ant.interpvalue),
                                      new XElement("isreportview", ant.isshow),
                                      new XElement("orgno", org.orgno),
                                      new XElement("isInterface", org.isInterface),
                                      new XElement("orgType", ant.orgType)
                                      );
                                    xorgantitbl.Add(xrow);
                                }
                            }
                        }
                    }
                    odxml.Add(xorgantitbl);
                    req.organtixml = odxml.ToString();
                    var _organtixml = new SqlParameter("organtixml", req.organtixml);
                    var _ismbnoresult = new SqlParameter("ismbnoresult", req.ismbnoresult);

                    var lst = context.InsertResultMB.FromSqlRaw(
                    "Execute dbo.pro_InsertResultMB @pagecode, @action, @venueno, @venuebranchno, @userno, " +
                    "@isabnormal, @iscritical, @ischecked, @isrerun, @isrecollect, @isrecheck, @isattachment, " +
                    "@patientvisitno,@orderlistno,@serviceno,@reportstatus,@resultstatus,@resultpattern,@patterndescription," +
                    "@colonycount,@wetpreparation,@gramstainjson,@gramstainbottlejson,@gramstaintext,@gramstainbottletext,@comments,@organtixml," +
                    "@approvalDoctor, @ismbnoresult, @isSecondReviewAvail, @isLock, @SnomedId, @SrcOfSpecimenNo, @SrcOfSpecimenDesc, @SrcOfSpecimenOthers, @NoMicroOrgSeen,@CCUnitNo,@UnitText,@ColonyCountText",
                    _pagecode, _action, _venueno, _venuebranchno, _userno, _isabnormal, _iscritical, _ischecked, _isrerun, _isrecollect, _isrecheck, _isattachment,
                    _patientvisitno, _orderlistno, _serviceno, _reportstatus, _resultstatus, _resultpattern, _patterndescription, _colonycount, _wetpreparation,
                    _gramstainjson, _gramstainbottlejson, _gramstaintext, _gramstainbottletext, _comments, _organtixml, _approvalDoctor, _ismbnoresult, _isSecondReviewAvail,
                    _isLock, _snomedId, _SrcOfSpecimenNo, _SrcOfSpecimenDesc, _SrcOfSpecimenOthers, _NoMicroOrgSeen, _CCUnitNo, _UnitText, _ColonyCountText).ToList();

                    obj = lst[0];

                    if (req.pagecode == "PCRA" && req.action == "SV" && obj.patientvisitno > 0)
                    {
                        PushMessage(obj.patientvisitno, req.venueno, req.venuebranchno, req.userno, req.pagecode, "2", req.fullname);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.InsertResultMB - " + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }

        public List<orgtypeantibiotic> GetOrgTypeAntibiotic(requestresult req)
        {
            List<orgtypeantibiotic> lst = new List<orgtypeantibiotic>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _organismno = new SqlParameter("organismno", req.deptno);
                    var _organismtypeno = new SqlParameter("organismtypeno", req.serviceno);
                    var _type = new SqlParameter("type", req.type);

                    lst = context.GetOrgTypeAntibiotic.FromSqlRaw(
                    "Execute dbo.pro_GetOrgTypeAntibiotic @venueno,@venuebranchno,@organismno,@organismtypeno,@type",
                    _venueno, _venuebranchno, _organismno, _organismtypeno, _type).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetOrgTypeAntibiotic", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }

        public objresulttemplate GetResultTemplate(requestresult req)
        {
            objresulttemplate obj = new objresulttemplate();
            bool lGetResult = false;

            //
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            string AppTransTemplateFilePath = "TransTemplateFilePath";
            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);

            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _deptno = new SqlParameter("deptno", req.deptno);
                    var _serviceno = new SqlParameter("serviceno", req.serviceno);
                    var _servicetype = new SqlParameter("servicetype", req.servicetype);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req.viewvenuebranchno);

                    var rtndblst = context.GetResultTemplate.FromSqlRaw(
                    "Execute dbo.pro_GetResultTemplate @pagecode,@venueno,@venuebranchno,@patientvisitno,@deptno,@serviceno,@servicetype,@userno,@viewvenuebranchno",
                    _pagecode, _venueno, _venuebranchno, _patientvisitno, _deptno, _serviceno, _servicetype, _userno, _viewvenuebranchno).ToList();

                    if (rtndblst != null && rtndblst.Count > 0)
                    {
                        var v = rtndblst[0];
                        obj.patientno = v.patientno;
                        obj.patientvisitno = v.patientvisitno;
                        obj.patientid = v.patientid;
                        obj.fullname = v.fullname;
                        obj.agetype = v.agetype;
                        obj.gender = v.gender;
                        obj.visitid = v.visitid;
                        obj.extenalvisitid = v.extenalvisitid;
                        obj.visitdttm = v.visitdttm;
                        obj.venueBranchName = v.venueBranchName;
                        obj.referraltype = v.referraltype;
                        obj.customername = v.customername;
                        obj.physicianname = v.physicianname;
                        obj.visstat = v.visstat;
                        obj.visremarks = v.visremarks;

                        obj.address = v.address;
                        obj.dob = v.dob;
                        obj.urntype = v.urntype;
                        obj.urnid = v.urnid;
                        obj.samplecollectedon = v.samplecollectedon;
                        obj.enteredon = v.enteredon;
                        obj.validatedon = v.validatedon;
                        obj.approvedon = v.approvedon;

                        obj.orderlistno = v.orderlistno;
                        obj.departmentno = v.departmentno;
                        obj.departmentname = v.departmentname;
                        obj.departmentseqno = v.departmentseqno;
                        obj.samplename = v.samplename;
                        obj.barcodeno = v.barcodeno;
                        obj.servicetype = v.servicetype;
                        obj.serviceno = v.serviceno;
                        obj.servicename = v.servicename;
                        obj.serviceseqno = v.serviceseqno;
                        obj.risrerun = false;
                        obj.risrecollect = false;
                        obj.risrecheck = false;
                        obj.isrecall = v.isrecall;
                        obj.isoutsource = v.isoutsource;
                        obj.isoutsourceattachment = v.isoutsourceattachment;
                        obj.isattachment = v.isattachment;
                        obj.oisremarks = v.oisremarks;
                        obj.oistat = v.oistat;
                        obj.isoledit = v.isoledit;
                        obj.isabnormal = v.isabnormal;
                        obj.iscritical = v.iscritical;
                        obj.ischecked = v.ischecked;
                        obj.isrerun = v.risrerun;
                        obj.isrecollect = v.risrecollect;
                        obj.isrecheck = v.risrecheck;
                        obj.icmrPatientId = v.icmrPatientId;
                        obj.srfNumber = v.srfNumber;
                        obj.templateno = v.templateno;
                        obj.reportstatus = v.reportstatus;
                        obj.nricnumber = v.nricnumber;
                        obj.isSensitiveData = v.isSensitiveData;
                        obj.isSecondReviewAvail = v.isSecondReviewAvail;
                        obj.isSecondReview = v.isSecondReview;
                        obj.isLock = v.isLock;
                        obj.snomedId = v.snomedId;
                        obj.isAbnormalAvail = v.isAbnormalAvail;
                        obj.tissueAudit = v.tissueAudit;
                        obj.isInfectionControl = v.isInfectionControl;
                        obj.malignantCase = v.malignantCase;
                        obj.defaultPathologist = v.defaultPathologist;

                        if (req.pagecode == "PCRA")
                        {
                            //get previous result template content
                            string prevpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";
                            string fldrpath = prevpath + "AuditTrail/" + req.venueno.ToString() + "/";
                            string auditbeforechanges = prevpath + "AuditTrail/" + req.venueno.ToString() + "/" + obj.orderlistno.ToString() + "_" + obj.serviceno.ToString() + "/";
                            string auditfilename = "OldData";
                            prevpath = prevpath + req.venueno.ToString() + "/" + obj.orderlistno.ToString() + "/" + obj.serviceno.ToString() + ".rtf";
                            if (File.Exists(prevpath))
                            {
                                string content = File.ReadAllText(prevpath);
                                if (!Directory.Exists(fldrpath))
                                {
                                    try
                                    {
                                        Directory.CreateDirectory(fldrpath);
                                    }
                                    catch (Exception ex)
                                    {
                                        MyDevException.Error(ex, "ResultRepository.GetResultTemplate - " + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
                                    }
                                }
                                if (!Directory.Exists(auditbeforechanges))
                                {
                                    try
                                    {
                                        Directory.CreateDirectory(auditbeforechanges);
                                    }
                                    catch (Exception ex)
                                    {
                                        MyDevException.Error(ex, "ResultRepository.GetResultTemplate - " + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
                                    }
                                }
                                try
                                {
                                    if (File.Exists(auditbeforechanges + auditfilename + ".rtf"))
                                    {
                                        File.Delete(auditbeforechanges + auditfilename + ".rtf");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MyDevException.Error(ex, "ResultRepository.GetResultTemplate - " + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
                                }

                                try
                                {
                                    string createText = content + Environment.NewLine;
                                    File.WriteAllText(auditbeforechanges + auditfilename + ".rtf", createText);
                                }
                                catch (Exception ex)
                                {
                                    MyDevException.Error(ex, "ResultRepository.GetResultTemplate - " + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
                                }
                            }
                        }
                    }
                }
                //
                using (var context = new DocumentContext(_config.GetConnectionString(ConfigKeys.DocumentDBConnection)))
                {
                    lGetResult = false;
                    try
                    {
                        var tResult = context.InsertTemplatePatientResult.FirstOrDefault(r => r.PatientVisitNo == req.patientvisitno && r.TestNo == req.serviceno);

                        if (tResult != null && tResult.Result != string.Empty)
                        {
                            obj.result = tResult.Result;
                            lGetResult = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MyDevException.Error(ex, "ResultRepository.GetResultTemplate - " + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
                    }
                }
                //
                if (!lGetResult)
                {
                    string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";

                    path = path + req.venueno.ToString() + "/" + obj.orderlistno.ToString() + "/" + obj.serviceno.ToString() + ".ym";

                    try
                    {
                        if (File.Exists(path))
                        {
                            string content = File.ReadAllText(path);
                            obj.result = content;
                        }
                        else
                        {
                            obj.result = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        MyDevException.Error(ex, "ResultRepository.GetResultTemplate - " + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetResultTemplate - " + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }

        public resultrtn InsertResultTemplate(objresulttemplate req)
        {
            resultrtn obj = new resultrtn();

            try
            {
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                string AppTransTemplateFilePath = "TransTemplateFilePath";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                string auditfolder = string.Empty;
                if (req.pagecode == "PCRA")
                {
                    //get current result template content                    
                    string curpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                          ? objAppSettingResponse.ConfigValue : "";
                    string fldrpath = curpath + "AuditTrail/" + req.venueno.ToString() + "/";
                    auditfolder = req.orderlistno.ToString() + "_" + req.serviceno.ToString();
                    string auditafterchanges = curpath + "AuditTrail/" + req.venueno.ToString() + "/" + auditfolder + "/";
                    string auditfilename = "NewData";
                    curpath = curpath + req.venueno.ToString() + "/" + req.orderlistno.ToString() + "/" + req.serviceno.ToString() + ".rtf";
                    if (File.Exists(curpath))
                    {
                        string content = File.ReadAllText(curpath);
                        if (!Directory.Exists(fldrpath))
                        {
                            Directory.CreateDirectory(fldrpath);
                        }
                        if (!Directory.Exists(auditafterchanges))
                        {
                            Directory.CreateDirectory(auditafterchanges);
                        }
                        if (File.Exists(auditafterchanges + auditfilename + ".rtf"))
                        {
                            File.Delete(auditafterchanges + auditfilename + ".rtf");
                        }
                        string createText = content + Environment.NewLine;
                        File.WriteAllText(auditafterchanges + auditfilename + ".rtf", createText);
                    }
                    //
                }
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _action = new SqlParameter("action", req.action);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);

                    var _isabnormal = new SqlParameter("isabnormal", req.isabnormal);
                    var _iscritical = new SqlParameter("iscritical", req.iscritical);
                    var _ischecked = new SqlParameter("ischecked", req.ischecked);
                    var _isrerun = new SqlParameter("isrerun", req.risrerun);
                    var _isrecollect = new SqlParameter("isrecollect", req.risrecollect);
                    var _isrecheck = new SqlParameter("isrecheck", req.risrecheck);
                    var _isattachment = new SqlParameter("isattachment", req.isattachment);

                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _orderlistno = new SqlParameter("orderlistno", req.orderlistno);
                    var _serviceno = new SqlParameter("serviceno", req.serviceno);
                    var _reportstatus = new SqlParameter("reportstatus", req.reportstatus);
                    var _templateno = new SqlParameter("templateno", req.templateno);
                    var _icmrPatientId = new SqlParameter("icmrPatientId", req.icmrPatientId);
                    var _srfNumber = new SqlParameter("srfNumber", req.srfNumber);
                    var _approvalDoctor = new SqlParameter("approvalDoctor", req.approvalDoctor);
                    var _isnoresult = new SqlParameter("istmpnoresult", req.istmpnoresult);
                    var _isSecondReviewAvail = new SqlParameter("isSecondReviewAvail", req.isSecondReview);
                    var _isLock = new SqlParameter("isLock", req.isLock);
                    var _snomedId = new SqlParameter("snomedId", req.snomedId);
                    var _tissueAudit = new SqlParameter("tissueAudit", req.tissueAudit);
                    var _requiredPath = new SqlParameter("requiredPath", auditfolder);
                    var _malignantCase = new SqlParameter("malignantCase", req.malignantCase.ValidateEmpty());

                    if ((req.isMultiEditor == 1 && req.multieditorcount == 1) || (req.isMultiEditor != 1))
                    {
                        var lst = context.InsertResultTemplate.FromSqlRaw(
                        "Execute dbo.pro_InsertResultTemplate @pagecode,@action,@venueno,@venuebranchno,@userno,@isabnormal,@iscritical,@ischecked,@isrerun,@isrecollect,@isrecheck,@isattachment,@patientvisitno,@orderlistno,@serviceno,@reportstatus,@templateno,@icmrPatientId,@srfNumber,@approvalDoctor,@istmpnoresult,@isSecondReviewAvail,@isLock,@SnomedId,@tissueAudit,@requiredPath,@malignantCase",
                        _pagecode, _action, _venueno, _venuebranchno, _userno, _isabnormal, _iscritical, _ischecked, _isrerun, _isrecollect, _isrecheck, _isattachment, _patientvisitno, _orderlistno, _serviceno, _reportstatus, _templateno, _icmrPatientId, _srfNumber, _approvalDoctor, _isnoresult, _isSecondReviewAvail, _isLock, _snomedId, _tissueAudit, _requiredPath,_malignantCase).ToList();

                        obj = lst[0];
                    }

                    //
                    obj.isMultiEditor = req.isMultiEditor;
                    obj.multieditorcount = req.isMultiEditor == 1 ? req.multieditorcount : 0;

                    string path = string.Empty;
                    path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";

                    if (req.isMultiEditor == 1)
                    {
                        path = path + req.venueno.ToString() + "/" + req.orderlistno.ToString() + "/" + req.serviceno.ToString() + "/";
                    }
                    else
                    {
                        path = path + req.venueno.ToString() + "/" + req.orderlistno.ToString() + "/";
                    }
                    if (!Directory.Exists(path))
                    {
                        try
                        {
                            Directory.CreateDirectory(path);
                        }
                        catch (Exception ex)
                        {
                            MyDevException.Error(ex, "ResultRepository.InsertResultTemplate - Patient Visit No. : " + req.patientvisitno.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
                        }
                    }

                    string createText = req.result + Environment.NewLine;
                    if (req.isMultiEditor == 1 && req.subtestno != null && req.subtestno > 0)
                    {
                        try
                        {
                            File.WriteAllText(path + req.subtestno.ToString() + ".ym", createText);
                        }
                        catch (Exception ex)
                        {
                            MyDevException.Error(ex, "ResultRepository.InsertResultTemplate - Patient Visit No. : " + req.patientvisitno.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
                        }
                    }
                    else
                    {
                        try
                        {
                            File.WriteAllText(path + req.serviceno.ToString() + ".ym", createText);
                        }
                        catch (Exception ex)
                        {
                            MyDevException.Error(ex, "ResultRepository.InsertResultTemplate - Patient Visit No. : " + req.patientvisitno.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
                        }
                    }
                    //

                    if (req.pagecode == "PCRA" && req.action == "SV" && obj.patientvisitno > 0)
                    {
                        PushMessage(obj.patientvisitno, req.venueno, req.venuebranchno, req.userno, req.pagecode, "3", req.fullname);
                    }
                }

                try
                {                    
                    using (var context = new DocumentContext(_config.GetConnectionString(ConfigKeys.DocumentDBConnection)))
                    {
                        try
                        {
                            context.InsertTemplatePatientResult
                            .Where(x => x.PatientVisitNo == req.patientvisitno && x.OrderListNo == req.orderlistno && x.Status == true)
                            .ExecuteUpdate(setters => setters
                                .SetProperty(x => x.Status, false)
                                .SetProperty(x => x.ModifiedOn, DateTime.UtcNow)
                                .SetProperty(x => x.ModifiedBy, (short)req.userno)
                            );
                        }
                        catch (Exception ex)
                        {
                            MyDevException.Error(ex, "ResultRepository.InsertResultTemplate - Patient Visit No. : " + req.patientvisitno.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
                        }

                        try
                        {
                            TemplatePatientResultInsertRequest objTempResult = new TemplatePatientResultInsertRequest();
                            objTempResult.PatientVisitNo = req.patientvisitno;
                            objTempResult.OrderListNo = req.orderlistno;
                            objTempResult.TestNo = req.serviceno;
                            objTempResult.TemplateNo = (short)req.templateno;
                            objTempResult.PageAction = req.pagecode + '-' + req.action;
                            objTempResult.PageCode = req.pagecode;
                            objTempResult.Result = req.result;
                            objTempResult.VenueNo = (short)req.venueno;
                            objTempResult.BranchNo = (short)req.venuebranchno;
                            objTempResult.Status = true;
                            objTempResult.CreatedOn = DateTime.UtcNow;
                            objTempResult.CreatedBy = (short)req.userno;

                            context.InsertTemplatePatientResult.Add(objTempResult);
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MyDevException.Error(ex, "ResultRepository.InsertResultTemplate - Patient Visit No. : " + req.patientvisitno.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex, "ResultRepository.InsertResultTemplate - Patient Visit No. : " + req.patientvisitno.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.InsertResultTemplate - " + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }

        public objrecall GetRecall(requestresult req)
        {
            objrecall obj = new objrecall();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _patientno = new SqlParameter("patientno", req.patientno);
                    
                    var rtndblst = context.GetRecall.FromSqlRaw(
                    "Execute dbo.pro_GetRecallVisit @pagecode,@venueno,@venuebranchno,@patientvisitno,@patientno",
                    _pagecode, _venueno, _venuebranchno, _patientvisitno, _patientno).ToList();

                    int patientvisitno = 0;
                    obj.patientVisitNo = rtndblst[0].patientVisitNo;
                    obj.patientID = rtndblst[0].patientID;
                    obj.fullName = rtndblst[0].fullName;
                    obj.ageType = rtndblst[0].ageType;
                    obj.gender = rtndblst[0].gender;
                    obj.visitID = rtndblst[0].visitID;
                    obj.extenalVisitID = rtndblst[0].extenalVisitID;
                    obj.visitDTTM = rtndblst[0].visitDTTM;
                    obj.referredBy = rtndblst[0].referredBy;
                    obj.venueBranchName = rtndblst[0].venueBranchName;

                    List<lstRecallServicves> lstv = new List<lstRecallServicves>();
                    foreach (var v in rtndblst)
                    {
                        patientvisitno = v.patientVisitNo;
                        lstRecallServicves objv = new lstRecallServicves();
                        objv.patientVisitNo = v.patientVisitNo;
                        objv.orderListNo = v.orderListNo;
                        objv.barcodeNo = v.barcodeNo;
                        objv.serviceType = v.serviceType;
                        objv.serviceNo = v.serviceNo;
                        objv.serviceName = v.serviceName;
                        objv.orderListStatus = v.orderListStatus;
                        objv.orderListStatusText = v.orderListStatusText;
                        lstv.Add(objv);
                    }
                    obj.lstRecallServicves = lstv;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetRecall - " + req.patientvisitno.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }

        public recallResponse InsertRecall(objrecall req)
        {
            recallResponse objRes = new recallResponse();
            recallDataResponse obj = new recallDataResponse();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    CommonHelper commonUtility = new CommonHelper();
                    string recallXML = "";
                    if (req.lstRecallServicves.Count > 0)
                    {
                        recallXML = commonUtility.ToXML(req.lstRecallServicves);
                    }
                    req.lstRecallServicves.Clear();

                    var _recallXML = new SqlParameter("recallXML", recallXML);
                    var _patientVisitNo = new SqlParameter("patientVisitNo", req.patientVisitNo);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);

                    var data = context.InsertRecall.FromSqlRaw(
                    "Execute dbo.pro_InsertRecall @patientVisitNo,@recallXML,@venueno,@venuebranchno,@userno",
                    _patientVisitNo, _recallXML, _venueno, _venuebranchno, _userno).ToList();

                    objRes.patientvisitno = data[0].patientvisitno;
                    objRes.isMultiEditor = data[0].isMultiEditor;
                    objRes.multieditorcount = data[0].multieditorcount;
                    objRes.RsltAmendNo = data[0].RsltAmendNo;
                    objRes.RsltAmendCode = data[0].RsltAmendCode;
                    objRes.lstTestDetails = JsonConvert.DeserializeObject<List<RecallTestDetailsResponse>>(data[0]?.lstTestDetails ?? "");

                    if (objRes.lstTestDetails != null && objRes.lstTestDetails.Count > 0)
                    {
                        string sourcePath = "", destPath = "";
                        string sourceFolder, destFolder = "";
                        string amendNo = objRes.RsltAmendNo.ToString();
                        string createText = "", content = "";

                        sourceFolder = "TransTemplateFilePath";
                        destFolder = "AmendmentTransTemplateFilePath";

                        MasterRepository _IMasterRepository = new MasterRepository(_config);

                        AppSettingResponse objSource = new AppSettingResponse();
                        objSource = _IMasterRepository.GetSingleAppSetting(sourceFolder);
                        sourcePath = objSource != null && objSource.ConfigValue != null && objSource.ConfigValue != ""
                                ? objSource.ConfigValue : "";

                        AppSettingResponse objDest = new AppSettingResponse();
                        objDest = _IMasterRepository.GetSingleAppSetting(destFolder);
                        destPath = objDest != null && objDest.ConfigValue != null && objDest.ConfigValue != ""
                                ? objDest.ConfigValue : "";

                        foreach (var z in objRes.lstTestDetails)
                        {   
                            sourcePath = sourcePath + req.venueno.ToString() + "\\" + z.orderListNo.ToString() + "\\";
                            destPath = destPath + req.venueno.ToString() + "\\" + amendNo + '\\' + z.orderListNo.ToString() + "\\";

                            if (File.Exists(sourcePath + z.testNo.ToString() + ".rtf"))
                            {
                                content = File.ReadAllText(sourcePath + z.testNo.ToString() + ".rtf");

                                if (!Directory.Exists(destPath))
                                {
                                    Directory.CreateDirectory(destPath);
                                }
                                
                                createText = content + Environment.NewLine;
                                File.WriteAllText(destPath + z.testNo.ToString() + ".rtf", createText);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.InsertRecall - " + req.patientVisitNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return objRes;
        }

        public List<objresulttemplate> GetBulkResult(requestresultvisit req)
        {
            List<objresulttemplate> lst = new List<objresulttemplate>();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req.viewvenuebranchno);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _type = new SqlParameter("type", req.type);
                    var _fromdate = new SqlParameter("fromdate", req.fromdate);
                    var _todate = new SqlParameter("todate", req.todate);
                    var _RefferalType = new SqlParameter("RefferalType", req.refferalType);
                    var _CustomerNo = new SqlParameter("CustomerNo", req.customerNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", req.physicianNo);
                    var _PatientvisitNo = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _TemplateNo = new SqlParameter("templateno", req.templateno);
                    var _ismachinevalue = new SqlParameter("ismachinevalue", req.ismachinevalue);

                    var rtndblst = context.GetBulkResult.FromSqlRaw(
                    "Execute dbo.pro_GetBulkResult @pagecode,@viewvenuebranchno,@venueno,@venuebranchno,@userno,@type,@fromdate,@todate,@RefferalType,@CustomerNo,@PhysicianNo,@patientvisitno,@templateno,@ismachinevalue",
                    _pagecode, _viewvenuebranchno, _venueno, _venuebranchno, _userno, _type, _fromdate, _todate, _RefferalType, _CustomerNo, _PhysicianNo, _PatientvisitNo, _TemplateNo, _ismachinevalue).ToList();

                    foreach (var v in rtndblst)
                    {
                        objresulttemplate obj = new objresulttemplate();
                        obj.patientno = v.patientno;
                        obj.patientvisitno = v.patientvisitno;
                        obj.patientid = v.patientid;
                        obj.fullname = v.fullname;
                        obj.agetype = v.agetype;
                        obj.gender = v.gender;
                        obj.visitid = v.visitid;
                        obj.extenalvisitid = v.extenalvisitid;
                        obj.visitdttm = v.visitdttm;
                        obj.referraltype = v.referraltype;
                        obj.customername = v.customername;
                        obj.physicianname = v.physicianname;
                        obj.visstat = v.visstat;
                        obj.visremarks = v.visremarks;

                        obj.address = v.address;
                        obj.dob = v.dob;
                        obj.urntype = v.urntype;
                        obj.urnid = v.urnid;
                        obj.samplecollectedon = v.samplecollectedon;
                        obj.enteredon = v.enteredon;
                        obj.validatedon = v.validatedon;
                        obj.approvedon = v.approvedon;

                        obj.orderlistno = v.orderlistno;
                        obj.departmentno = v.departmentno;
                        obj.departmentname = v.departmentname;
                        obj.departmentseqno = v.departmentseqno;
                        obj.samplename = v.samplename;
                        obj.barcodeno = v.barcodeno;
                        obj.servicetype = v.servicetype;
                        obj.serviceno = v.serviceno;
                        obj.servicename = v.servicename;
                        obj.serviceseqno = v.serviceseqno;
                        obj.risrerun = false;
                        obj.risrecollect = false;
                        obj.risrecheck = false;
                        obj.isrecall = v.isrecall;
                        obj.isoutsource = v.isoutsource;
                        obj.ismachinevalue = v.ismachinevalue;
                        obj.isoutsourceattachment = v.isoutsourceattachment;
                        obj.isattachment = v.isattachment;
                        obj.oisremarks = v.oisremarks;
                        obj.oistat = v.oistat;
                        obj.isoledit = v.isoledit;
                        obj.isabnormal = v.isabnormal;
                        obj.iscritical = v.iscritical;
                        obj.ischecked = v.ischecked;
                        obj.isrerun = v.risrerun;
                        obj.isrecollect = v.risrecollect;
                        obj.isrecheck = v.risrecheck;
                        obj.icmrPatientId = v.icmrPatientId;
                        obj.srfNumber = v.srfNumber;
                        obj.templateno = v.templateno;
                        obj.reportstatus = v.reportstatus;
                        obj.colorCode = v.colorCode;
                        obj.IsPartialEntryMaster = v.IsPartialEntryMaster;
                        obj.IsPartialEntryTrans = v.IsPartialEntryTrans;
                        obj.IsPartialValidationMaster = v.IsPartialValidationMaster;
                        obj.IsPartialValidationTrans = v.IsPartialValidationTrans;

                        if (req.pagecode == "PCRE")
                        {
                            objAppSettingResponse = new AppSettingResponse();
                            string AppMasterFilePath = "MasterFilePath";
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                    ? objAppSettingResponse.ConfigValue : "";

                            path = path + req.venueno.ToString() + "/Template/" + obj.templateno.ToString() + ".ym";
                            if (File.Exists(path))
                            {
                                obj.result = File.ReadAllText(path).ToString();
                            }
                            else
                            {
                                obj.result = "";
                            }
                        }
                        else
                        {
                            objAppSettingResponse = new AppSettingResponse();
                            string AppTransTemplateFilePath = "TransTemplateFilePath";
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                    ? objAppSettingResponse.ConfigValue : "";

                            path = path + req.venueno.ToString() + "/" + obj.orderlistno.ToString() + "/" + obj.serviceno.ToString() + ".ym";

                            if (File.Exists(path))
                            {
                                string content = File.ReadAllText(path);
                                obj.result = content;
                            }
                            else
                            {
                                obj.result = "";
                            }
                        }
                        lst.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetBulkResult", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return lst;
        }

        public resultrtn InsertBulkResult(objbulkresulttemplate req)
        {
            resultrtn obj = new resultrtn();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _action = new SqlParameter("action", req.action);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);

                    CommonHelper commonUtility = new CommonHelper();
                    string bulkresultXML = "";
                    if (req.lstbulkresult.Count > 0)
                    {
                        bulkresultXML = commonUtility.ToXML(req.lstbulkresult);
                    }

                    var _bulkresultXML = new SqlParameter("bulkresultXML", bulkresultXML);

                    var lst = context.InsertResultTemplate.FromSqlRaw(
                        "Execute dbo.pro_InsertBulkResultTemplate @pagecode,@action,@venueno,@venuebranchno,@userno,@bulkresultXML",
                        _pagecode, _action, _venueno, _venuebranchno, _userno, _bulkresultXML).ToList();

                    obj = lst[0];
                    //
                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    string AppTransTemplateFilePath = "TransTemplateFilePath";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                    string dpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";

                    string path = dpath + req.venueno.ToString() + "/";
                    foreach (var v in req.lstbulkresult)
                    {
                        if (!Directory.Exists(path + v.orderlistno.ToString() + "/"))
                        {
                            Directory.CreateDirectory(path + v.orderlistno.ToString() + "/");
                        }
                        string file = path + "/" + v.orderlistno.ToString() + "/";

                        string createText = v.result + Environment.NewLine;
                        File.WriteAllText(file + v.serviceno.ToString() + ".ym", createText);

                        if (req.pagecode == "PCRA" && req.action == "SV" && v.patientvisitno > 0)
                        {
                            PushMessage(v.patientvisitno, req.venueno, req.venuebranchno, req.userno, req.pagecode, "3", v.fullname);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.InsertBulkResult", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }

        public List<covidresult> GetCovidWorkOrder(covidWorkOrderreq req)
        {
            List<covidresult> lst = new List<covidresult>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _type = new SqlParameter("type", req.type);
                    var _fromdate = new SqlParameter("fromdate", req.fromdate);
                    var _todate = new SqlParameter("todate", req.todate);
                    var _patientno = new SqlParameter("patientno", req.patientno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _refferraltypeno = new SqlParameter("refferraltypeno", req.refferraltypeno);
                    var _customerno = new SqlParameter("customerno", req.customerno);
                    var _physicianno = new SqlParameter("physicianno", req.physicianno);
                    var _isCompleted = new SqlParameter("isCompleted", req.isCompleted);
                    var _orderstatus = new SqlParameter("orderstatus", req.orderstatus);
                    var _routeNo = new SqlParameter("routeNo", req.routeNo);

                    lst = context.GetCovidWorkOrder.FromSqlRaw(
                    "Execute dbo.pro_GetCovidWorkOrder @pagecode,@venueno,@venuebranchno,@userno,@type,@fromdate,@todate,@patientno,@patientvisitno,@refferraltypeno,@customerno,@physicianno,@orderstatus,@isCompleted,@routeNo",
                    _pagecode, _venueno, _venuebranchno, _userno, _type, _fromdate, _todate, _patientno, _patientvisitno, _refferraltypeno, _customerno, _physicianno, _orderstatus, _isCompleted, _routeNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetCovidWorkOrder" + req.pagecode.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return lst;
        }

        public resultrtn InsertCovidWorkOrder(covidWorkOrder req)
        {
            resultrtn obj = new resultrtn();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    CommonHelper commonUtility = new CommonHelper();
                    string covidXML = "";
                    if (req.lstcovidresult.Count > 0)
                    {
                        covidXML = commonUtility.ToXML(req.lstcovidresult);
                    }
                    req.lstcovidresult.Clear();

                    var _covidXML = new SqlParameter("covidXML", covidXML);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    
                    var lst = context.InsertCovidWorkOrder.FromSqlRaw(
                    "Execute dbo.pro_InsertCovidWorkOrder @covidXML,@venueno,@venuebranchno,@userno",
                    _covidXML, _venueno, _venuebranchno, _userno).ToList();

                    obj = lst[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.InsertCovidWorkOrder" + req.venueno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }

        public List<ApprovalDoctorResponse> ApprovalDoctorList(ApprovalDoctorRequest req)
        {
            List<ApprovalDoctorResponse> lst = new List<ApprovalDoctorResponse>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _deptNo = new SqlParameter("deptNo", req.deptNo);

                    lst = context.GetApprovalDoctorList.FromSqlRaw(
                    "Execute dbo.pro_GetApprovalDoctors @VenueNo,@VenueBranchNo,@UserNo,@DeptNo",
                    _venueno, _venuebranchno, _userno, _deptNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.ApprovalDoctorList" + req.venueno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return lst;
        }
        public List<PatientImpressionResponse> GetPatientImpression(CommonFilterRequestDTO RequestItem)
        {
            List<PatientImpressionResponse> lstPatientInfoResponse = new List<PatientImpressionResponse>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _CustomerNo = new SqlParameter("CustomerNo", RequestItem.CustomerNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem.physicianNo);
                    var _DepartmentNo = new SqlParameter("DepartmentNo", RequestItem.departmentNo);
                    var _ServiceNo = new SqlParameter("ServiceNo", RequestItem.serviceNo);
                    var _IsReport = new SqlParameter("IsReport", RequestItem.routeNo);
                    var _ServiceType = new SqlParameter("ServiceType", RequestItem.serviceType);
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", RequestItem.visitNo);
                    var _userNo = new SqlParameter("UserNo", RequestItem.userNo);
                    var _IDType = new SqlParameter("IDType", RequestItem.AnalyzerNo);//Id Type (Nric/fin)
                    var _Nationality = new SqlParameter("Nationality", RequestItem.FranchiseNo);//nationality
                    var _searchType = new SqlParameter("searchType", RequestItem.searchType);
                    var _searchKeyword = new SqlParameter("searchKeyword", RequestItem.SearchKey);
                    //var _refferalType = new SqlParameter("@refferalType", SqlDbType.Int);
                    //_refferalType.Value = RequestItem.refferalType != 0 ? (object)RequestItem.refferalType : 0;
                    var _refferalType = new SqlParameter("refferalType", RequestItem.refferalType);


                    List<PatientDataImpressionResponse> result = context.GetPatientImpressionList.FromSqlRaw(
                    "Execute dbo.Pro_GetPatientSearchImpression @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@CustomerNo,@PhysicianNo,@DepartmentNo,@ServiceNo,@IsReport,@ServiceType,@PatientVisitNo,@UserNo,@IDType,@Nationality,@searchType,@searchKeyword,@refferalType",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _CustomerNo, _PhysicianNo, _DepartmentNo, _ServiceNo, _IsReport, _ServiceType, _PatientVisitNo, _userNo, _IDType, _Nationality,_searchType,_searchKeyword, _refferalType).ToList();

                    List<string> lstitem = new List<string>();
                    if (RequestItem.searchType == 1)
                    {
                        foreach (var resultItem in result)
                        {
                            MasterRepository _IMasterRepository = new MasterRepository(_config);
                            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                            objAppSettingResponse = new AppSettingResponse();
                            string AppTransTemplateFilePath = "TransactionTemplateFilePath";
                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                            string dpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                    ? objAppSettingResponse.ConfigValue : "";

                            var path = dpath;
                            path = path + RequestItem.VenueNo.ToString() + "\\" + resultItem.OrderListNo.ToString();

                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            if (!string.IsNullOrWhiteSpace(RequestItem.SearchKey))
                            {
                                var extensions = new List<string> { ".ym", ".rtf" };
                                string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                                                    .Where(f => extensions.IndexOf(Path.GetExtension(f)) >= 0).ToArray();

                                foreach (string file in files)
                                {
                                    string[] lines = File.ReadAllLines(file);
                                    string firstOccurrence = lines.FirstOrDefault(l => l.Contains(RequestItem.SearchKey));
                                    if (firstOccurrence != null)
                                    {
                                        lstitem.Add(resultItem.OrderListNo.ToString());
                                    }
                                }
                            }
                            else
                            {
                                lstitem.Add(resultItem.OrderListNo.ToString());
                            }
                        }
                    }
                    else if (RequestItem.searchType == 2)
                    {
                        result.ForEach(x => lstitem.Add(x.OrderListNo.ToString()));
                    }
                    else
                    {
                        result.ForEach(x => lstitem.Add(x.OrderListNo.ToString()));
                    }
                    
                    lstitem = lstitem.Distinct().ToList();
                    
                    if (lstitem.Count > 0)
                    {
                        _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                        _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                        _Type = new SqlParameter("Type", RequestItem.Type);
                        _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                        _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                        _CustomerNo = new SqlParameter("CustomerNo", RequestItem.CustomerNo);
                        _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem.physicianNo);
                        _DepartmentNo = new SqlParameter("DepartmentNo", RequestItem.departmentNo);
                        _ServiceNo = new SqlParameter("ServiceNo", RequestItem.serviceNo);
                        XDocument ServiceXML = new XDocument(new XElement("Orders", from orderlistno in lstitem
                                                                                    select
                         new XElement("ServiceList",
                         new XElement("OrderListNo", orderlistno)
                         )));
                        var _orderxml = new SqlParameter("orderxml", ServiceXML.ToString());
                        var _PageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);
                        var _serviceTypes = new SqlParameter("ServiceType", RequestItem.serviceType);
                        var _IsReports = new SqlParameter("IsReport", RequestItem.routeNo);
                        var _PatientVisitNos = new SqlParameter("PatientVisitNo", RequestItem.visitNo);
                        var _userNos = new SqlParameter("UserNo", RequestItem.userNo);
                        var _isPageIndexReq = new SqlParameter("isPageIndexReq", RequestItem.isStat);
                        var _refferalTypee = new SqlParameter("refferalType", RequestItem.refferalType);
                        //var _refferalTypee = new SqlParameter("@refferalType", SqlDbType.Int);
                        //_refferalTypee.Value = RequestItem.refferalType != 0 ? (object)RequestItem.refferalType : 0;




                        lstPatientInfoResponse = context.GetPatientImpressionoutput.FromSqlRaw(
                        "Execute dbo.Pro_GetPatientImpression @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@CustomerNo,@PhysicianNo,@DepartmentNo,@ServiceNo,@orderxml,@PageIndex,@IsReport,@ServiceType,@PatientVisitNo,@UserNo,@isPageIndexReq,@refferalType",
                        _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _CustomerNo, _PhysicianNo, _DepartmentNo, _ServiceNo, _orderxml, _PageIndex, _IsReports, _serviceTypes, _PatientVisitNos, _userNos, _isPageIndexReq, _refferalTypee).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetPatientImpression", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstPatientInfoResponse;
        }

        public async Task<objresult> GetResultExceptUserMapped(requestresult req)
        {
            objresult obj = new objresult();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _deptno = new SqlParameter("deptno", req.deptno);
                    var _serviceno = new SqlParameter("serviceno", req.serviceno);
                    var _servicetype = new SqlParameter("servicetype", req.servicetype);

                    var rtndblst = await context.GetResultExceptUserMapped.FromSqlRaw(
                    "Execute dbo.pro_GetResultExceptUserMapped @pagecode,@venueno,@venuebranchno,@userno,@patientvisitno,@deptno,@serviceno,@servicetype",
                    _pagecode, _venueno, _venuebranchno, _userno, _patientvisitno, _deptno, _serviceno, _servicetype).ToListAsync();

                    int patientvisitno = 0;
                    int orderlistno = 0;
                    int id = 0;

                    rtndblst = rtndblst.OrderBy(a => a.patientvisitno).ToList();
                    List<lstvisit> lstv = new List<lstvisit>();
                    foreach (var v in rtndblst)
                    {
                        if (patientvisitno != v.patientvisitno)
                        {
                            patientvisitno = v.patientvisitno;
                            lstvisit objv = new lstvisit();
                            objv.patientno = v.patientno;
                            objv.patientvisitno = v.patientvisitno;
                            objv.patientid = v.patientid;
                            objv.fullname = v.fullname;
                            objv.agetype = v.agetype;
                            objv.gender = v.gender;
                            objv.visitid = v.visitid;
                            objv.extenalvisitid = v.extenalvisitid;
                            objv.visitdttm = v.visitdttm;
                            objv.referraltype = v.referraltype;
                            objv.customername = v.customername;
                            objv.physicianname = v.physicianname;
                            objv.visstat = v.visstat;
                            objv.visremarks = v.visremarks;
                            objv.address = v.address;
                            objv.dob = v.dob;
                            objv.urntype = v.urntype;
                            objv.urnid = v.urnid;
                            objv.samplecollectedon = v.samplecollectedon;
                            objv.enteredon = v.enteredon;
                            objv.validatedon = v.validatedon;
                            objv.approvedon = v.approvedon;
                            objv.NotifyCount = v.notifyCount;
                            objv.venueBranchName = v.venueBranchName;
                            orderlistno = 0;

                            var ollst = rtndblst.Where(o => o.patientvisitno == v.patientvisitno).ToList();
                            ollst = ollst.OrderBy(d => d.departmentseqno).ThenBy(s => s.serviceseqno).ToList();

                            List<lstorderlist> lstol = new List<lstorderlist>();

                            foreach (var ol in ollst)
                            {
                                if (orderlistno != ol.orderlistno)
                                {
                                    orderlistno = ol.orderlistno;
                                    lstorderlist objol = new lstorderlist();
                                    objol.patientvisitno = ol.patientvisitno;
                                    objol.orderlistno = ol.orderlistno;
                                    objol.departmentno = ol.departmentno;
                                    objol.departmentname = ol.departmentname;
                                    objol.departmentseqno = ol.departmentseqno;
                                    objol.samplename = ol.samplename;
                                    objol.barcodeno = ol.barcodeno;
                                    objol.serviceCode = ol.serviceCode;
                                    objol.servicetype = ol.servicetype;
                                    objol.serviceno = ol.serviceno;
                                    objol.servicename = ol.servicename;
                                    objol.serviceseqno = ol.serviceseqno;
                                    objol.resulttypeno = ol.resulttypeno;
                                    objol.risrerun = false;
                                    objol.risrecollect = false;
                                    objol.risrecheck = false;
                                    objol.isrecall = ol.isrecall;
                                    objol.isoutsource = ol.isoutsource;
                                    objol.isoutsourceattachment = ol.isoutsourceattachment;
                                    objol.isattachment = ol.isattachment;
                                    objol.isremarks = ol.isremarks;
                                    objol.istat = ol.istat;
                                    objol.iscontestinter = ol.iscontestinter;
                                    objol.groupinter = ol.groupinter;
                                    objol.ischecked = ol.ischecked;
                                    objol.isrerun = ol.isrerun;
                                    objol.isrecollect = ol.isrecollect;
                                    objol.isrecheck = ol.isrecheck;
                                    objol.isMultiEditor = ol.isMultiEditor;

                                    if (objol.isMultiEditor != null && objol.isMultiEditor == 1)
                                    {
                                        //multi editor - single test scenario - saved/drafted details shown in reportstatus, ICMR PatientId, SRF Number, abnormal, critical values
                                        requestresult reqtemplate = new requestresult();
                                        reqtemplate.pagecode = req.pagecode;
                                        reqtemplate.venueno = req.venueno;
                                        reqtemplate.venuebranchno = req.venuebranchno;
                                        reqtemplate.patientvisitno = req.patientvisitno;
                                        reqtemplate.serviceno = objol.serviceno;
                                        reqtemplate.deptno = req.deptno;
                                        reqtemplate.servicetype = objol.servicetype;

                                        objresulttemplate objtemplate = new objresulttemplate();
                                        objtemplate = GetResultTemplate(reqtemplate);

                                        if (objtemplate != null)
                                        {
                                            objol.isabnormal = objtemplate.isabnormal;
                                            objol.iscritical = objtemplate.iscritical;
                                            objol.icmrPatientId = objtemplate.icmrPatientId;
                                            objol.srfNumber = objtemplate.srfNumber;
                                            objol.reportstatus = objtemplate.reportstatus;
                                        }
                                    }

                                    if (ol.servicetype == "G")
                                    {
                                        objol.isgrouptd = true;
                                    }
                                    else if (ol.subtestname != "" && ol.servicetype != "G")
                                    {
                                        objol.isgrouptd = true;
                                    }

                                    if (objol.iscontestinter == false)
                                    {
                                        if (objol.groupinter == 2)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppTransFilePath = "TransFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                    ? objAppSettingResponse.ConfigValue : "";

                                            path = path + req.venueno.ToString() + "/G/InterNotes/" + objol.orderlistno + ".ym";
                                            if (File.Exists(path))
                                            {
                                                objol.internotes = File.ReadAllText(path);
                                            }
                                            else
                                            {
                                                objol.internotes = "";
                                            }
                                        }
                                        else if (objol.groupinter == 1)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppMasterFilePath = "MasterFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                    ? objAppSettingResponse.ConfigValue : "";

                                            path = path + req.venueno.ToString() + "/G/InterNotes/" + objol.serviceno + ".ym";
                                            if (File.Exists(path))
                                            {
                                                objol.internotes = File.ReadAllText(path);
                                            }
                                            else
                                            {
                                                objol.internotes = "";
                                            }
                                        }
                                        else
                                        {
                                            objol.internotes = "";
                                        }
                                    }
                                    else
                                    {
                                        objol.internotes = "";
                                    }
                                    var odlst = ollst.Where(o => o.orderlistno == ol.orderlistno).ToList();
                                    //odlst = odlst.OrderBy(t => t.tseqno).ThenBy(st => st.subtestno).ToList();
                                    odlst = odlst.OrderBy(t => t.tseqno).ThenBy(st => st.sseqno).ToList();//issue raised in PSH 
                                    List<lstorderdetail> lstod = new List<lstorderdetail>();
                                    foreach (var t in odlst)
                                    {
                                        lstorderdetail objod = new lstorderdetail();
                                        objod.id = id;
                                        id = id + 1;
                                        objod.orderlistno = ol.orderlistno;
                                        objod.orderdetailsno = t.orderdetailsno;
                                        objod.testtype = t.testtype;
                                        objod.testno = t.testno;
                                        objod.testname = t.testname;
                                        objod.tseqno = t.tseqno;
                                        objod.subtestno = t.subtestno;
                                        objod.subtestname = t.subtestname;
                                        objod.sseqno = t.sseqno;
                                        objod.resulttype = t.resulttype;
                                        objod.mastermethodno = t.mastermethodno;
                                        objod.masterunitno = t.masterunitno;
                                        objod.masterllcolumn = t.masterllcolumn;
                                        objod.masterhlcolumn = t.masterhlcolumn;
                                        objod.masterdisplayrr = t.masterdisplayrr;
                                        objod.crllcolumn = t.crllcolumn;
                                        objod.crhlcolumn = t.crhlcolumn;
                                        objod.minrange = t.minrange;
                                        objod.maxrange = t.maxrange;
                                        objod.isnonmandatory = t.isnonmandatory;
                                        objod.isdelta = t.isdelta;
                                        objod.istformula = t.istformula;
                                        objod.issformula = t.issformula;
                                        objod.decimalpoint = t.decimalpoint;
                                        objod.isroundoff = t.isroundoff;
                                        objod.isformulaparameter = t.isformulaparameter;
                                        objod.formulaserviceno = t.formulaserviceno;
                                        objod.formulaservicetype = t.formulaservicetype;
                                        objod.formulajson = JsonConvert.DeserializeObject<List<formulajson>>(t.formulajson);
                                        objod.formulaparameterjson = JsonConvert.DeserializeObject<List<formulaparameterjson>>(t.formulaparameterjson);
                                        objod.picklistjson = JsonConvert.DeserializeObject<List<picklistjson>>(t.picklistjson);
                                        objod.headerno = t.headerno;
                                        objod.isedit = t.isedit;
                                        objod.testinter = t.testinter;
                                        objod.methodno = t.methodno;
                                        objod.methodname = t.methodname;
                                        objod.unitno = t.unitno;
                                        objod.unitname = t.unitname;
                                        objod.llcolumn = t.llcolumn;
                                        objod.hlcolumn = t.hlcolumn;
                                        objod.displayrr = t.displayrr;
                                        objod.isMultiEditor = t.isMultiEditor;
                                        objod.statusName = t.statusName;

                                        if (t.isMultiEditor == 1)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppTransTemplateFilePath = "TransTemplateFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                    ? objAppSettingResponse.ConfigValue : "";

                                            path = path + req.venueno.ToString() + "/" + objod.orderlistno.ToString() + "/" + objod.testno.ToString() + "/" + objod.subtestno.ToString() + ".ym";

                                            if (File.Exists(path))
                                            {
                                                string content = File.ReadAllText(path);
                                                objod.result = content;
                                            }
                                            else
                                            {
                                                objod.result = "";
                                            }
                                        }
                                        else
                                        {
                                            objod.result = t.result;
                                            if (t.result != "" && objol.ischecked == false)
                                            {
                                                objol.ischecked = true;
                                            }
                                        }

                                        objod.formularesult = t.formularesult;
                                        objod.diresult = t.diresult;
                                        objod.resultflag = t.resultflag;
                                        objod.resultcomments = t.resultcomments;
                                        objod.risrerunod = false;
                                        objod.isrerunod = t.isrerunod;
                                        objod.isUploadOption = t.isUploadOption;
                                        objod.uploadedfile = t.uploadedFile;
                                        objod.approvalDoctor = t.approvalDoctor;

                                        if (objol.iscontestinter == true)
                                        {
                                            if (objod.testinter == 2)
                                            {
                                                objAppSettingResponse = new AppSettingResponse();
                                                string AppTransFilePath = "TransFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";

                                                path = path + req.venueno.ToString() + "/T/InterNotes/" + objod.orderdetailsno + ".ym";
                                                if (File.Exists(path))
                                                {
                                                    objod.internotes = File.ReadAllText(path);
                                                }
                                                else
                                                {
                                                    objod.internotes = "";
                                                }
                                            }
                                            else if (objod.testinter == 1)
                                            {
                                                objAppSettingResponse = new AppSettingResponse();
                                                string AppMasterFilePath = "MasterFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";

                                                path = path + req.venueno.ToString() + "/T/InterNotes/" + objod.testno + ".ym";
                                                if (File.Exists(path))
                                                {
                                                    objod.internotes = File.ReadAllText(path);
                                                }
                                                else
                                                {
                                                    objod.internotes = "";
                                                }
                                            }
                                            else
                                            {
                                                objod.internotes = "";
                                            }
                                        }
                                        else
                                        {
                                            objod.internotes = "";
                                        }
                                        lstod.Add(objod);
                                    }
                                    objol.lstorderdetail = lstod;
                                    lstol.Add(objol);
                                }
                            }
                            objv.lstorderlist = lstol;
                            lstv.Add(objv);
                        }
                    }
                    obj.lstvisit = lstv;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetResultExceptUserMapped - " + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }

        //merged concept
        public List<mergeresultresponse> GetMergedResult(mergeresultrequest req)
        {
            List<mergeresultresponse> lst = new List<mergeresultresponse>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _userno = new SqlParameter("userno", req.userNo);
                    var _patientno = new SqlParameter("patientno", req.patientno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientVisitNo);
                    var _testno = new SqlParameter("testno", req.testno);
                    var _subtestno = new SqlParameter("subtestno", req.subtestno);
                    var _nricno = new SqlParameter("nricNo", req.nricNo);

                    lst = context.GetMergedResults.FromSqlRaw(
                    "Execute dbo.pro_GetMergeResult @venueno, @venuebranchno, @userno, @patientno, @patientvisitno, @testno, @subtestno, @nricNo",
                    _venueno, _venuebranchno, _userno, _patientno, _patientvisitno, _testno, _subtestno, _nricno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetMergedResult" + req.patientno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return lst;
        }
        public savemergeresultresponse InsertMergedResult(savemergeresultrequest req)
        {
            savemergeresultresponse Obj = new savemergeresultresponse();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    string xmldata = string.Empty;
                    XDocument odxml = new XDocument();
                    XElement xodtbl = new XElement("odtbl");

                    foreach (var od in req.lstmergedresult)
                    {
                        if (od.resultType != "C" && od.resultType != "TE")
                        {
                            XElement xrow = new XElement("row",
                            new XElement("patientvisitno", od.patientVisitNo),
                            new XElement("orderlistno", od.orderListNo),
                            new XElement("orderdetailsno", od.orderDetailsNo),
                            new XElement("testtype", od.testType),
                            new XElement("testno", od.testNo),
                            new XElement("testname", od.testName),
                            new XElement("tseqno", od.tSeqNo),
                            new XElement("subtestno", od.subTestNo),
                            new XElement("subtestname", od.subTestName),
                            new XElement("sseqno", od.sSeqNo),
                            new XElement("methodno", od.methodNo),
                            new XElement("methodname", od.methodName),
                            new XElement("unitno", od.unitNo),
                            new XElement("unitname", od.unitName),
                            new XElement("llcolumn", od.lLColumn),
                            new XElement("hlcolumn", od.hLColumn),
                            new XElement("displayrr", od.displayRR),
                            new XElement("result", od.currResult),
                            new XElement("resultflag", od.resultflag),
                            new XElement("resulttype", od.resultType),
                            new XElement("headerno", od.headerNo)
                            );
                            xodtbl.Add(xrow);
                        }
                    }

                    odxml.Add(xodtbl);
                    xmldata = odxml.ToString();

                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _action = new SqlParameter("action", req.action);
                    var _odxml = new SqlParameter("odxml", xmldata);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _userno = new SqlParameter("userno", req.userNo);
                    
                    var lst = context.InsertMergedResults.FromSqlRaw(
                    "Execute dbo.pro_InsertMergedResult @pagecode, @action, @odxml, @venueno, @venuebranchno, @userno",
                    _pagecode, _action, _odxml, _venueno, _venuebranchno, _userno).ToList();

                    Obj.status = lst != null && lst.Count > 0 ? lst[0].status : 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.InsertMergedResult", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return Obj;
        }
        //
        public List<culturehistoryreponse> GetCultureHistory(culturehistoryrequest req)
        {
            List<culturehistoryreponse> lst = new List<culturehistoryreponse>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _userNo = new SqlParameter("userNo", req.userNo);
                    var _patientno = new SqlParameter("patientno", req.patientno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _nricNo = new SqlParameter("nricNo", req.nricNo);

                    lst = context.GetCultureHistory.FromSqlRaw(
                    "Execute dbo.pro_GetCultureHistory @venueno, @venuebranchno, @userNo, @patientno, @patientvisitno, @nricNo",
                    _venueno, _venuebranchno, _userNo, _patientno, _patientvisitno, _nricNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetCultureHistory", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }

        public async Task<objresult> GetAnalyserResult(analyserrequestresult req)
        {
            objresult obj = new objresult();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _deptno = new SqlParameter("deptno", req.deptno);
                    var _serviceno = new SqlParameter("serviceno", req.serviceno);
                    var _servicetype = new SqlParameter("servicetype", req.servicetype);
                    var _fromdate = new SqlParameter("fromDate", req.fromdate);
                    var _todate = new SqlParameter("toDate", req.todate);
                    var _type = new SqlParameter("type", req.gentype);
                    var _ismachinevalue = new SqlParameter("IsMachineValue", req.ismachinevalue);
                    var _machineId = new SqlParameter("machineId", req.machineId);
                    var _maindeptNo = new SqlParameter("maindeptNo", req.maindeptNo);
                    var _patientno = new SqlParameter("patientno", req.patientno);

                    var rtndblst = await context.GetAnalyserResult.FromSqlRaw(
                    "Execute dbo.pro_GetAnalyserResult " +
                    "@pagecode, @venueno, @venuebranchno, @userno, @patientvisitno, @deptno, @serviceno, @servicetype, @type, " +
                    "@fromdate, @todate, @ismachinevalue, @MachineId, @maindeptNo, @patientno",
                    _pagecode, _venueno, _venuebranchno, _userno, _patientvisitno, _deptno, _serviceno, _servicetype, _type,
                    _fromdate, _todate, _ismachinevalue, _machineId, _maindeptNo,_patientno).ToListAsync();

                    int patientvisitno = 0;
                    int orderlistno = 0;
                    int id = 0;

                    rtndblst = rtndblst.OrderBy(a => a.patientvisitno).ToList();
                    List<lstvisit> lstv = new List<lstvisit>();
                    foreach (var v in rtndblst)
                    {
                        if (patientvisitno != v.patientvisitno)
                        {
                            patientvisitno = v.patientvisitno;
                            lstvisit objv = new lstvisit();
                            objv.patientno = v.patientno;
                            objv.patientvisitno = v.patientvisitno;
                            objv.patientid = v.patientid;
                            objv.fullname = v.fullname;
                            objv.agetype = v.agetype;
                            objv.gender = v.gender;
                            objv.visitid = v.visitid;
                            objv.extenalvisitid = v.extenalvisitid;
                            objv.visitdttm = v.visitdttm;
                            objv.referraltype = v.referraltype;
                            objv.customername = v.customername;
                            objv.physicianname = v.physicianname;
                            objv.visstat = v.visstat;
                            objv.visremarks = v.visremarks;
                            objv.address = v.address;
                            objv.dob = v.dob;
                            objv.urntype = v.urntype;
                            objv.urnid = v.urnid;
                            objv.samplecollectedon = v.samplecollectedon;
                            objv.enteredon = v.enteredon;
                            objv.validatedon = v.validatedon;
                            objv.approvedon = v.approvedon;
                            objv.NotifyCount = v.notifyCount;
                            objv.venueBranchName = v.venueBranchName;
                            objv.nricNumber = v.nricNumber;
                            objv.isAbnormalAvail = v.isAbnormalAvail;
                            objv.rhNo = v.rhNo;
                            objv.taskdttm = v.taskdttm;
                            objv.isVipIndication = v.isVipIndication;
                            orderlistno = 0;
                            var ollst = rtndblst.Where(o => o.patientvisitno == v.patientvisitno).ToList();
                            ollst = ollst.OrderBy(d => d.departmentseqno).ThenBy(s => s.serviceseqno).ToList();
                            List<lstorderlist> lstol = new List<lstorderlist>();
                            foreach (var ol in ollst)
                            {
                                if (orderlistno != ol.orderlistno)
                                {
                                    orderlistno = ol.orderlistno;
                                    lstorderlist objol = new lstorderlist();
                                    objol.patientvisitno = ol.patientvisitno;
                                    objol.orderlistno = ol.orderlistno;
                                    objol.departmentno = ol.departmentno;
                                    objol.departmentname = ol.departmentname;
                                    objol.departmentseqno = ol.departmentseqno;
                                    objol.samplename = ol.samplename;
                                    objol.barcodeno = ol.barcodeno;
                                    objol.servicetype = ol.servicetype;
                                    objol.serviceno = ol.serviceno;
                                    objol.servicename = ol.servicename;
                                    objol.serviceseqno = ol.serviceseqno;
                                    objol.resulttypeno = ol.resulttypeno;
                                    objol.risrerun = false;
                                    objol.risrecollect = false;
                                    objol.risrecheck = false;
                                    objol.isrecall = ol.isrecall;
                                    objol.isoutsource = ol.isoutsource;
                                    objol.isoutsourceattachment = ol.isoutsourceattachment;
                                    objol.isattachment = ol.isattachment;
                                    objol.isremarks = ol.isremarks;
                                    objol.istat = ol.istat;
                                    objol.iscontestinter = ol.iscontestinter;
                                    objol.groupinter = ol.groupinter;
                                    objol.ischecked = false;// ol.ischecked;
                                    objol.isrerun = ol.isrerun;
                                    objol.isrecollect = ol.isrecollect;
                                    objol.isrecheck = ol.isrecheck;
                                    objol.isMultiEditor = ol.isMultiEditor;
                                    objol.isSecondReview = ol.isSecondReview;
                                    objol.isSecondReviewAvail = ol.isSecondReviewAvail;
                                    objol.IsPartialEntryMaster = ol.IsPartialEntryMaster;
                                    objol.IsPartialValidationMaster = ol.IsPartialValidationMaster;
                                    objol.IsPartialEntryTrans = ol.IsPartialEntryTrans;
                                    objol.IsPartialValidationTrans = ol.IsPartialValidationTrans;

                                    //non mapped department shown
                                    objol.isDeptAvail = ol.isDeptAvail;
                                    if (objol.isMultiEditor != null && objol.isMultiEditor == 1)
                                    {
                                        //multi editor - single test scenario - saved/drafted details shown in reportstatus, ICMR PatientId, SRF Number, abnormal, critical values
                                        requestresult reqtemplate = new requestresult();
                                        reqtemplate.pagecode = req.pagecode;
                                        reqtemplate.venueno = req.venueno;
                                        reqtemplate.venuebranchno = req.venuebranchno;
                                        reqtemplate.patientvisitno = req.patientvisitno;
                                        reqtemplate.serviceno = objol.serviceno;
                                        reqtemplate.deptno = req.deptno;
                                        reqtemplate.servicetype = objol.servicetype;
                                        objresulttemplate objtemplate = new objresulttemplate();
                                        objtemplate = GetResultTemplate(reqtemplate);
                                        if (objtemplate != null)
                                        {
                                            objol.isabnormal = objtemplate.isabnormal;
                                            objol.iscritical = objtemplate.iscritical;
                                            objol.icmrPatientId = objtemplate.icmrPatientId;
                                            objol.srfNumber = objtemplate.srfNumber;
                                            objol.reportstatus = objtemplate.reportstatus;
                                        }
                                    }

                                    if (ol.servicetype == "G")
                                    {
                                        objol.isgrouptd = true;
                                    }
                                    else if (ol.subtestname != "" && ol.servicetype != "G")
                                    {
                                        objol.isgrouptd = true;
                                    }

                                    if (objol.iscontestinter == false)
                                    {
                                        if (objol.groupinter == 2)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppTransFilePath = "TransFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                    ? objAppSettingResponse.ConfigValue : "";

                                            path = path + req.venueno.ToString() + "/G/InterNotes/" + objol.orderlistno + ".ym";
                                            if (File.Exists(path))
                                            {
                                                objol.internotes = File.ReadAllText(path);
                                            }
                                            else
                                            {
                                                objol.internotes = "";
                                            }
                                        }
                                        else if (objol.groupinter == 1)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppMasterFilePath = "MasterFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                    ? objAppSettingResponse.ConfigValue : "";

                                            path = path + req.venueno.ToString() + "/G/InterNotes/" + objol.serviceno + ".ym";
                                            if (File.Exists(path))
                                            {
                                                objol.internotes = File.ReadAllText(path);
                                            }
                                            else
                                            {
                                                objol.internotes = "";
                                            }
                                        }
                                        else
                                        {
                                            objol.internotes = "";
                                        }
                                    }
                                    else
                                    {
                                        objol.internotes = "";
                                    }

                                    var odlst = ollst.Where(o => o.orderlistno == ol.orderlistno).ToList();
                                    //odlst = odlst.OrderBy(t => t.tseqno).ThenBy(st => st.subtestno).ToList();
                                    odlst = odlst.OrderBy(t => t.tseqno).ThenBy(st => st.sseqno).ToList();//issue raised in PSH 
                                    List<lstorderdetail> lstod = new List<lstorderdetail>();
                                    foreach (var t in odlst)
                                    {
                                        lstorderdetail objod = new lstorderdetail();
                                        objod.id = id;
                                        id = id + 1;
                                        objod.orderlistno = ol.orderlistno;
                                        objod.orderdetailsno = t.orderdetailsno;
                                        objod.testtype = t.testtype;
                                        objod.testno = t.testno;
                                        objod.testname = t.testname;
                                        objod.tseqno = t.tseqno;
                                        objod.subtestno = t.subtestno;
                                        objod.subtestname = t.subtestname;
                                        objod.sseqno = t.sseqno;
                                        objod.resulttype = t.resulttype;
                                        objod.mastermethodno = t.mastermethodno;
                                        objod.masterunitno = t.masterunitno;
                                        objod.masterllcolumn = t.masterllcolumn;
                                        objod.masterhlcolumn = t.masterhlcolumn;
                                        objod.masterdisplayrr = t.masterdisplayrr;
                                        objod.crllcolumn = t.crllcolumn;
                                        objod.crhlcolumn = t.crhlcolumn;
                                        objod.minrange = t.minrange;
                                        objod.maxrange = t.maxrange;
                                        objod.isnonmandatory = t.isnonmandatory;
                                        objod.isdelta = t.isdelta;
                                        objod.istformula = t.istformula;
                                        objod.issformula = t.issformula;
                                        objod.decimalpoint = t.decimalpoint;
                                        objod.isroundoff = t.isroundoff;
                                        objod.isformulaparameter = t.isformulaparameter;
                                        objod.formulaserviceno = t.formulaserviceno;
                                        objod.formulaservicetype = t.formulaservicetype;
                                        objod.formulajson = JsonConvert.DeserializeObject<List<formulajson>>(t.formulajson);
                                        objod.formulaparameterjson = JsonConvert.DeserializeObject<List<formulaparameterjson>>(t.formulaparameterjson);
                                        objod.picklistjson = JsonConvert.DeserializeObject<List<picklistjson>>(t.picklistjson);
                                        objod.headerno = t.headerno;
                                        objod.isedit = t.isedit;
                                        objod.testinter = t.testinter;
                                        objod.methodno = t.methodno;
                                        objod.methodname = t.methodname;
                                        objod.unitno = t.unitno;
                                        objod.unitname = t.unitname;
                                        objod.llcolumn = t.llcolumn;
                                        objod.hlcolumn = t.hlcolumn;
                                        objod.displayrr = t.displayrr;
                                        objod.isMultiEditor = t.isMultiEditor;
                                        objod.statusName = t.statusName;
                                        objod.lesserValue = t.lesserValue;
                                        objod.greaterValue = t.greaterValue;
                                        objod.isExtraSubtestEnable = t.isExtraSubTestEnable;
                                        objod.isLogicNeeded = t.isLogicNeeded;
                                        objod.prevABORHResult = t.prevABORHResult;
                                        objod.logicneededjson = JsonConvert.DeserializeObject<List<logicConceptResponse>>(t.logicneededjson);
                                       
                                        if (t.isMultiEditor == 1)
                                        {
                                            objAppSettingResponse = new AppSettingResponse();
                                            string AppTransTemplateFilePath = "TransTemplateFilePath";
                                            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransTemplateFilePath);
                                            string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                    ? objAppSettingResponse.ConfigValue : "";

                                            path = path + req.venueno.ToString() + "/" + objod.orderlistno.ToString() + "/" + objod.testno.ToString() + "/" + objod.subtestno.ToString() + ".ym";

                                            if (File.Exists(path))
                                            {
                                                string content = File.ReadAllText(path);
                                                objod.result = content;
                                            }
                                            else
                                            {
                                                objod.result = "";
                                            }
                                        }
                                        else
                                        {
                                            objod.result = t.result;
                                            if (t.result != "" && objol.ischecked == false)
                                            {
                                                objol.ischecked = false;
                                            }
                                        }

                                        objod.formularesult = t.formularesult;
                                        objod.diresult = t.diresult;
                                        objod.resultflag = t.resultflag;
                                        objod.resultcomments = t.resultcomments;
                                        objod.risrerunod = false;
                                        objod.isrerunod = t.isrerunod;
                                        objod.isUploadOption = t.isUploadOption;
                                        objod.uploadedfile = t.uploadedFile;
                                        objod.approvalDoctor = t.approvalDoctor;
                                        objol.isnoresult = t.isnoresult;
                                        objod.noresult = t.isnoresult;
                                        if (objol.iscontestinter == true)
                                        {
                                            if (objod.testinter == 2)
                                            {
                                                objAppSettingResponse = new AppSettingResponse();
                                                string AppTransFilePath = "TransFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";

                                                path = path + req.venueno.ToString() + "/T/InterNotes/" + objod.orderdetailsno + ".ym";
                                                if (File.Exists(path))
                                                {
                                                    objod.internotes = File.ReadAllText(path);
                                                }
                                                else
                                                {
                                                    objod.internotes = "";
                                                }
                                            }
                                            else if (objod.testinter == 1)
                                            {
                                                objAppSettingResponse = new AppSettingResponse();
                                                string AppMasterFilePath = "MasterFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";

                                                path = path + req.venueno.ToString() + "/T/InterNotes/" + objod.testno + ".ym";
                                                if (File.Exists(path))
                                                {
                                                    objod.internotes = File.ReadAllText(path);
                                                }
                                                else
                                                {
                                                    objod.internotes = "";
                                                }
                                                //                                                
                                                objAppSettingResponse = new AppSettingResponse();
                                                AppMasterFilePath = "MasterFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                                string Fhpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";

                                                Fhpath = Fhpath + req.venueno.ToString() + "/T/InterNotes/" + objod.testno + "_H" + ".ym";
                                                if (File.Exists(Fhpath))
                                                {
                                                    objod.interNotesHigh = File.ReadAllText(Fhpath);
                                                }
                                                else
                                                {
                                                    objod.interNotesHigh = "";
                                                }
                                                //                                                
                                                objAppSettingResponse = new AppSettingResponse();
                                                AppMasterFilePath = "MasterFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppMasterFilePath);
                                                string Flpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";

                                                Flpath = Flpath + req.venueno.ToString() + "/T/InterNotes/" + objod.testno + "_L" + ".ym";
                                                if (File.Exists(Flpath))
                                                {
                                                    objod.interNotesLow = File.ReadAllText(Flpath);
                                                }
                                                else
                                                {
                                                    objod.interNotesLow = "";
                                                }
                                            }
                                            else
                                            {
                                                objod.internotes = "";
                                            }
                                        }
                                        else
                                        {
                                            objod.internotes = "";
                                        }

                                        objod.prevresult = t.prevresult;
                                        objod.prevresultrefrange = String.Empty;
                                        objod.prevresultdttm = String.Empty;
                                        string prevrefrange = t.prevresultrefrange;
                                        objod.prevresultdttm = prevrefrange != null ? prevrefrange.Split("$$")[1] : "";
                                        objod.prevresultrefrange = prevrefrange != null ? prevrefrange.Split("$$")[0] : "";
                                        //non mapped department shown
                                        objod.isDeptAvail = t.isDeptAvail;
                                        if (t.isDeptAvail > 1)
                                        {
                                            objol.ischecked = false;
                                        }
                                        lstod.Add(objod);
                                    }
                                    objol.lstorderdetail = lstod;
                                    lstol.Add(objol);
                                }
                            }
                            objv.lstorderlist = lstol;
                            lstv.Add(objv);
                        }
                    }
                    obj.lstvisit = lstv;
                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetAnalyserResult" + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }
        public async Task<resultrtn> InsertAnalyserResult(objresult req)
        {
            resultrtn obj = new resultrtn();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    XDocument odxml = new XDocument();
                    XElement xodtbl = new XElement("odtbl");
                    foreach (var vst in req.lstvisit)
                    {
                        int oldserviceno = 0;
                        foreach (var ol in vst.lstorderlist)
                        {
                            if (ol.ischecked == true || ol.risrerun == true || ol.risrecollect == true || ol.risrecheck == true || ol.isnoresult == true)
                            {
                                int oldtestno = 0;
                                foreach (var od in ol.lstorderdetail)
                                {
                                    if (od.resulttype != "CU" && od.resulttype != "TE")
                                    {
                                        XElement xrow = new XElement("row",
                                        new XElement("patientvisitno", ol.patientvisitno),
                                        new XElement("orderlistno", ol.orderlistno),
                                        new XElement("ischecked", ol.ischecked),
                                        new XElement("isrerun", ol.risrerun),
                                        new XElement("isrecollect", ol.risrecollect),
                                        new XElement("isrecheck", ol.risrecheck),
                                        new XElement("isnoresult", od.noresult),
                                        //new XElement("isoledit", ol.isoledit),
                                        new XElement("groupinter", ol.groupinter),
                                        new XElement("isattachment", ol.isattachment),
                                        new XElement("resulttypeno", ol.resulttypeno),
                                        new XElement("orderdetailsno", od.orderdetailsno),
                                        new XElement("testtype", od.testtype),
                                        new XElement("testno", od.testno),
                                        new XElement("testname", od.testname),
                                        new XElement("tseqno", od.tseqno),
                                        new XElement("subtestno", od.subtestno),
                                        new XElement("subtestname", od.subtestname),
                                        new XElement("sseqno", od.sseqno),
                                        new XElement("methodno", od.methodno),
                                        new XElement("methodname", od.methodname),
                                        new XElement("unitno", od.unitno),
                                        new XElement("unitname", od.unitname),
                                        new XElement("llcolumn", od.llcolumn),
                                        new XElement("hlcolumn", od.hlcolumn),
                                        new XElement("displayrr", od.displayrr),
                                        new XElement("result", od.result),
                                        new XElement("formularesult", od.formularesult),
                                        new XElement("diresult", od.diresult),
                                        new XElement("resultflag", od.resultflag),
                                        new XElement("resulttype", od.resulttype),
                                        new XElement("headerno", od.headerno),
                                        new XElement("resultcomments", od.resultcomments),
                                        new XElement("isedit", od.isedit),
                                        new XElement("testinter", od.testinter),
                                        new XElement("internotes", ""),
                                        new XElement("risrerunod", od.risrerunod),
                                        new XElement("uploadedpath", od.uploadedfile),
                                        new XElement("approvalDoctor", od.approvalDoctor),
                                        new XElement("isSecondReview", ol.isSecondReview),
                                        new XElement("isPartialEntryTrans", ol.IsPartialEntryTrans),
                                        new XElement("isPartialValidationTrans", ol.IsPartialValidationTrans)
                                        );
                                        xodtbl.Add(xrow);
                                        if (ol.iscontestinter == false)
                                        {
                                            if (ol.groupinter == 2 && oldserviceno != ol.serviceno)
                                            {
                                                oldserviceno = ol.serviceno;
                                                //                                                
                                                objAppSettingResponse = new AppSettingResponse();
                                                string AppTransFilePath = "TransFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";

                                                path = path + req.venueno.ToString() + "/G/InterNotes/";

                                                if (!Directory.Exists(path))
                                                {
                                                    Directory.CreateDirectory(path);
                                                }
                                                string createText = ol.internotes + Environment.NewLine;
                                                File.WriteAllText(path + ol.orderlistno + ".ym", createText);
                                            }
                                        }
                                        else if (ol.iscontestinter == true)
                                        {
                                            if (od.testinter == 2 && oldtestno != od.testno)
                                            {
                                                oldtestno = od.testno;
                                                //                                                
                                                objAppSettingResponse = new AppSettingResponse();
                                                string AppTransFilePath = "TransFilePath";
                                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppTransFilePath);
                                                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                        ? objAppSettingResponse.ConfigValue : "";

                                                path = path + req.venueno.ToString() + "/T/InterNotes/";

                                                if (!Directory.Exists(path))
                                                {
                                                    Directory.CreateDirectory(path);
                                                }
                                                var internotespath = od.resultflag == "H" ? od.interNotesHigh : od.resultflag == "L" ? od.interNotesLow : od.internotes;
                                                string createText = internotespath + Environment.NewLine;
                                                File.WriteAllText(path + od.orderdetailsno + ".ym", createText);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    odxml.Add(xodtbl);
                    req.odxml = odxml.ToString();

                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _action = new SqlParameter("action", req.action);
                    var _odxml = new SqlParameter("odxml", req.odxml);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);

                    var lst = await context.InsertAnalyserResult.FromSqlRaw(
                    "Execute dbo.pro_InsertAnalyserResult @pagecode, @action, @odxml, @venueno, @venuebranchno, @userno",
                    _pagecode, _action, _odxml, _venueno, _venuebranchno, _userno).ToListAsync();

                    obj = lst[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.InsertAnalyserResult", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }
        
        #region BulkResult Entry
        public BulkResultSaveResponse SaveBulkResultEtry(List<objbulkresult> req)
        {
            BulkResultSaveResponse obj = new BulkResultSaveResponse();
            try
            {
                string outxml = string.Empty;
                List<lstbulkresultdbl> lst = new List<lstbulkresultdbl>();
                lstbulkresultdbl objn = new lstbulkresultdbl();
                foreach (var data in req)
                {
                    foreach (var subdata in data.lstbulkresultdetails)
                    {
                        if (subdata.ischecked == true || subdata.isrecheck == true || subdata.isrerun == true || subdata.isrecollect == true || subdata.isnoresult == true)
                        {
                            objn = new lstbulkresultdbl();
                            objn.patientno = subdata.patientno;
                            objn.patientid = subdata.patientid;
                            objn.patientvisitno = subdata.patientvisitno;
                            objn.orderdetailsno = subdata.orderdetailsno;
                            objn.orderlistno = subdata.orderlistno;
                            objn.serviceno = data.serviceno;//order table record
                            objn.servicetype = data.servicetype;//order table record
                            objn.servicename = data.servicename;//order table record
                            objn.testno = subdata.testno;
                            objn.testtype = subdata.testtype;
                            objn.testname = subdata.testname;
                            objn.result = subdata.result;
                            objn.resulttype = subdata.resulttype;
                            objn.resulttypeno = subdata.resulttypeno;
                            objn.resultflag = subdata.resultflag;
                            objn.resultcomments = subdata.resultcomments?.ValidateEmpty();
                            objn.diresult = subdata.diresult;
                            objn.formularesult = subdata.formularesult;
                            objn.displayrr = subdata.displayrr;
                            objn.unitname = subdata.unit;
                            objn.unitno = subdata.unitno;
                            objn.ischecked = subdata.ischecked;
                            objn.isrecollect = subdata.isrecollect;
                            objn.isrerun = subdata.isrerun;
                            objn.isrecheck = subdata.isrecheck;
                            objn.llcolumn = subdata.llcolumn;
                            objn.hlcolumn = subdata.hlcolumn;
                            objn.crhlcolumn = subdata.crhlcolumn;
                            objn.crllcolumn = subdata.crllcolumn;
                            objn.groupinter = subdata.groupinter;
                            objn.isattachment = subdata.isattachment;
                            objn.tseqno = subdata.tseqno;
                            objn.subtestno = subdata.subtestno;
                            objn.subtestname = subdata.subtestname;
                            objn.sseqno = subdata.sseqno;
                            objn.methodno = subdata.methodno;
                            objn.methodname = subdata.methodname;
                            objn.unitno = subdata.unitno;
                            objn.unitname = subdata.unit;
                            objn.headerno = subdata.headerno;
                            objn.isedit = subdata.isedit;
                            objn.testinter = subdata.testinter;
                            objn.uploadedFile = subdata.uploadedFile;
                            objn.approvalDoctor = subdata.approvalDoctor;
                            objn.isnoresult = subdata.isnoresult;
                            objn.isSecondReview = subdata.isSecondReview;
                            objn.isLock = subdata.isLock;
                            lst.Add(objn);
                        }
                    }
                }
                CommonHelper commonUtility = new CommonHelper();
                outxml = commonUtility.ToXML(lst);
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req[0].pagecode);
                    var _action = new SqlParameter("action", req[0].action);
                    var _odxml = new SqlParameter("odxml", outxml);
                    var _venueno = new SqlParameter("venueno", req[0].venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req[0].venuebranchno);
                    var _userno = new SqlParameter("userno", req[0].userno);
                    
                    var lstexe = context.InsertBulkResult.FromSqlRaw(
                    "Execute dbo.pro_InsertBulkResultEntry @pagecode,@action,@odxml,@venueno,@venuebranchno,@userno",
                    _pagecode, _action, _odxml, _venueno, _venuebranchno, _userno).ToList();
                    
                    obj.outstatus = lstexe != null && lstexe.Count > 0 ? lstexe[0].outstatus : 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.SaveBulkResultEtry", ExceptionPriority.High, ApplicationType.REPOSITORY, req[0].venueno, req[0].venuebranchno, 0);
            }
            return obj;
        }
        public List<objbulkresult> GetBulkResultEtry(analyserrequestresult req)
        {
            List<objbulkresult> lst = new List<objbulkresult>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _deptno = new SqlParameter("deptno", req.deptno);
                    var _serviceno = new SqlParameter("serviceno", req.serviceno);
                    var _servicetype = new SqlParameter("servicetype", req.servicetype);
                    var _fromdate = new SqlParameter("fromDate", req.fromdate);
                    var _todate = new SqlParameter("toDate", req.todate);
                    var _type = new SqlParameter("type", req.gentype);
                    var _ismachinevalue = new SqlParameter("IsMachineValue", req.ismachinevalue);
                    var _machineId = new SqlParameter("machineId", req.machineId);
                    var _maindeptNo = new SqlParameter("maindeptNo", req.maindeptNo);
                    var _testfilterflag = new SqlParameter("testfilterflag", req.testfilterflag);
                    var _patientno = new SqlParameter("patientno", req.patientno);

                    var rtndblst = context.GetBulkEntryResult.FromSqlRaw(
                    "Execute dbo.pro_GetBulkResultEntry @pagecode,@venueno,@venuebranchno,@userno,@patientvisitno,@deptno,@serviceno,@servicetype,@type,@fromdate,@todate,@ismachinevalue,@MachineId,@maindeptNo,@testfilterflag,@patientno",
                    _pagecode, _venueno, _venuebranchno, _userno, _patientvisitno, _deptno, _serviceno, _servicetype, _type, _fromdate, _todate, _ismachinevalue, _machineId, _maindeptNo, _testfilterflag, _patientno).ToList();

                    if (rtndblst != null)
                    {
                        lst = new List<objbulkresult>();
                        objbulkresult obj = new objbulkresult();
                        rtndblst = rtndblst.OrderBy(a => a.patientvisitno).ToList();
                        objbulkresultdetails objsub = new objbulkresultdetails();
                        int servceno = 0;
                        string servicename = string.Empty;
                        string servcetype = string.Empty;

                        foreach (var item in rtndblst)
                        {
                            obj = new objbulkresult();
                            if (servceno != item.actualserviceno || servcetype != item.actualservicetype)
                            {
                                var serviceexists = lst.Where(d => d.serviceno == item.actualserviceno && d.servicetype == item.actualservicetype).ToList();
                                if (serviceexists != null && serviceexists.Count > 0)
                                {
                                }
                                else
                                {
                                    obj.pagecode = req.pagecode;
                                    obj.venueno = req.venueno;
                                    obj.venuebranchno = req.venuebranchno;
                                    obj.userno = req.userno;
                                    obj.lstbulkresultdetails = new List<objbulkresultdetails>();
                                    obj.servicetype = item.actualservicetype;
                                    obj.serviceno = item.actualserviceno;
                                    obj.servicename = item.actualservicename;
                                    obj.oTTestCode = item.oTTestCode;
                                    obj.IsPartialEntryMaster = item.IsPartialEntryMaster;
                                    obj.IsPartialValidationMaster = item.IsPartialValidationMaster;
                                    
                                    var lstout = rtndblst.Where(d => d.actualserviceno == item.actualserviceno && d.actualservicetype == item.actualservicetype).OrderBy(s => s.patientvisitno).ToList();
                                    
                                    if (lstout != null && lstout.Count > 0)
                                    {
                                        int sno = 1;
                                        foreach (var spcdata in lstout)
                                        {
                                            objsub = new objbulkresultdetails();
                                            objsub.rowno = sno;
                                            objsub.visitid = spcdata.visitid;
                                            objsub.patientid = spcdata.patientid;
                                            objsub.patientname = spcdata.fullname;
                                            objsub.patientage = spcdata.agetype;
                                            objsub.gender = spcdata.gender;
                                            objsub.patientno = spcdata.patientno;
                                            objsub.patientvisitno = spcdata.patientvisitno;
                                            objsub.orderdetailsno = spcdata.orderdetailsno;
                                            objsub.orderlistno = spcdata.orderlistno;
                                            objsub.departmentno = spcdata.departmentno;
                                            objsub.departmentseqno = spcdata.departmentseqno;
                                            objsub.tseqno = spcdata.tseqno;
                                            objsub.sseqno = spcdata.sseqno;
                                            objsub.testno = spcdata.testno;
                                            objsub.testname = spcdata.testname;
                                            objsub.testtype = spcdata.testtype;
                                            objsub.testCode = spcdata.testCode;
                                            objsub.subtestno = spcdata.subtestno;
                                            objsub.isnonmandatory = spcdata.isnonmandatory;
                                            objsub.subtestname = spcdata.subtestname;
                                            objsub.masterdisplayrr = spcdata.masterdisplayrr;
                                            objsub.masterllcolumn = spcdata.masterllcolumn;
                                            objsub.masterhlcolumn = spcdata.masterhlcolumn;
                                            objsub.displayrr = spcdata.displayrr;
                                            objsub.llcolumn = spcdata.llcolumn;
                                            objsub.hlcolumn = spcdata.hlcolumn;
                                            objsub.crllcolumn = spcdata.crllcolumn;
                                            objsub.crhlcolumn = spcdata.crhlcolumn;
                                            objsub.minrange = spcdata.minrange;
                                            objsub.maxrange = spcdata.maxrange;
                                            objsub.ischecked = spcdata.ischecked;
                                            objsub.unitno = spcdata.unitno;
                                            objsub.unit = spcdata.unitname;
                                            objsub.methodno = spcdata.methodno;
                                            objsub.methodname = spcdata.methodname;
                                            objsub.samplename = spcdata.samplename;
                                            objsub.result = spcdata.result;
                                            objsub.resultflag = spcdata.resultflag;
                                            objsub.resulttype = spcdata.resulttype;
                                            objsub.resulttypeno = spcdata.resulttypeno;
                                            objsub.resultcomments = spcdata.resultcomments;
                                            objsub.diresult = spcdata.diresult;
                                            objsub.isdelta = spcdata.isdelta;
                                            objsub.isformulaparameter = spcdata.isformulaparameter;
                                            objsub.istformula = spcdata.istformula;
                                            objsub.issformula = spcdata.issformula;
                                            objsub.formulaserviceno = spcdata.formulaserviceno;
                                            objsub.formulaservicetype = spcdata.formulaservicetype;
                                            objsub.formulajson = JsonConvert.DeserializeObject<List<formulajson>>(spcdata.formulajson);
                                            objsub.formulaparameterjson = JsonConvert.DeserializeObject<List<formulaparameterjson>>(spcdata.formulaparameterjson);
                                            objsub.picklistjson = JsonConvert.DeserializeObject<List<picklistjson>>(spcdata.picklistjson);
                                            objsub.isrecheck = spcdata.isrecheck;
                                            objsub.isrecollect = spcdata.isrecollect;
                                            objsub.isrerun = spcdata.isrerun;
                                            objsub.isnoresult = spcdata.isnoresult;
                                            objsub.lesserValue = spcdata.lesserValue;
                                            objsub.greaterValue = spcdata.greaterValue;
                                            objsub.prevDIResults = JsonConvert.DeserializeObject<List<DIResult>>(spcdata.prevDIResults);
                                            objsub.isOBNegative = spcdata.isOBNegative;
                                            objsub.isOBTest = spcdata.isOBTest;
                                            objsub.ObTestCode = spcdata.ObTestCode;
                                            objsub.ObPositiveCode = spcdata.ObPositiveCode;
                                            objsub.decimalpoint = spcdata.decimalpoint;
                                            objsub.isroundoff = spcdata.isroundoff;
                                            objsub.prevABORHResult = spcdata.prevABORHResult;
                                            objsub.IsPartialEntryTrans = spcdata.IsPartialEntryTrans;
                                            objsub.IsPartialValidationTrans = spcdata.IsPartialValidationTrans;

                                            obj.lstbulkresultdetails.Add(objsub);
                                            sno = sno + 1;
                                        }
                                    }
                                    lst.Add(obj);
                                }
                            }
                            servceno = item.actualserviceno;
                            servcetype = item.actualservicetype;
                            servicename = item.actualservicename;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetBulkResultEtry" + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return lst;
        }
        #endregion
        
        #region bulk result entry - culture
        public List<BulkCultureResultResponse> GetCultureBulkResultEtry(GetBulkCultureResultRequest req)
        {
            List<BulkCultureResultResponse> lst = new List<BulkCultureResultResponse>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _deptno = new SqlParameter("deptno", req.deptno);
                    var _serviceno = new SqlParameter("serviceno", req.serviceno);
                    var _servicetype = new SqlParameter("servicetype", req.servicetype);
                    var _fromdate = new SqlParameter("fromDate", req.fromdate);
                    var _todate = new SqlParameter("toDate", req.todate);
                    var _type = new SqlParameter("type", req.gentype);
                    var _ismachinevalue = new SqlParameter("IsMachineValue", req.ismachinevalue);
                    var _machineId = new SqlParameter("machineId", req.machineId);
                    var _maindeptNo = new SqlParameter("maindeptNo", req.maindeptNo);
                    var _patientno = new SqlParameter("patientno", req.patientno);
                    
                    lst = context.GetCultureBulkEntryResults.FromSqlRaw(
                    "Execute dbo.pro_GetBulkResultEntryCulture @pagecode,@venueno,@venuebranchno,@userno,@patientvisitno,@deptno,@serviceno,@servicetype,@type,@fromdate,@todate,@ismachinevalue,@MachineId,@maindeptNo,@patientno",
                    _pagecode, _venueno, _venuebranchno, _userno, _patientvisitno, _deptno, _serviceno, _servicetype, _type, _fromdate, _todate, _ismachinevalue, _machineId, _maindeptNo,_patientno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetCultureBulkResultEtry" + req.patientvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return lst;
        }
        public BulkCultureResultSaveResponse SaveCultureBulkResultEtry(SaveBulkCUltureResultRequest req)
        {
            BulkCultureResultSaveResponse obj = new BulkCultureResultSaveResponse();
            try
            {
                List<BulkCultureResultResponse> lstCultures = new List<BulkCultureResultResponse>();
                string outxml = string.Empty;
                CommonHelper commonUtility = new CommonHelper();
                if (req.lstCulture != null && req.lstCulture.Count > 0)
                {
                    outxml = commonUtility.ToXML(req.lstCulture);
                }
                else
                {
                    outxml = commonUtility.ToXML(lstCultures);
                }
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _action = new SqlParameter("action", req.action);
                    var _odxml = new SqlParameter("odxml", outxml);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    
                    var lstexe = context.InsertCultureBulkResult.FromSqlRaw(
                    "Execute dbo.pro_InsertCultureBulkResultEntry @pagecode, @action, @odxml, @venueno, @venuebranchno, @userno",
                    _pagecode, _action, _odxml, _venueno, _venuebranchno, _userno).ToList();
                    
                    obj.outstatus = lstexe != null && lstexe.Count > 0 ? lstexe[0].outstatus : 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.SaveCultureBulkResultEtry", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, 0);
            }
            return obj;
        }
        #endregion
        
        public async Task<ReportOutput> GetPatientImpressionReport(GetImpressionReportRequest RequestItem)
        {
            string DefaultConnection = string.Empty;
            DefaultConnection = _config.GetConnectionString(ConfigKeys.DefaultConnection);
            ReportRepository reportRepo = new ReportRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            ReportOutput obj = new ReportOutput();
            TblReportMaster tblReportMaster = new TblReportMaster();
            DataTable datable = null;
            
            try
            {
                List<PatientImpressionResponse> lst = new List<PatientImpressionResponse>();
                CommonFilterRequestDTO objinput = new CommonFilterRequestDTO();
                objinput.FromDate = RequestItem.objData.FromDate;
                objinput.ToDate = RequestItem.objData.ToDate;
                objinput.Type = RequestItem.objData.Type;
                objinput.serviceNo = RequestItem.objData.serviceNo;
                objinput.serviceType = RequestItem.objData.serviceType;
                objinput.departmentNo = RequestItem.objData.departmentNo;
                objinput.physicianNo = RequestItem.objData.physicianNo;
                objinput.CustomerNo = RequestItem.objData.CustomerNo;
                objinput.VenueNo = RequestItem.objData.VenueNo;
                objinput.VenueBranchNo = RequestItem.objData.VenueBranchNo;
                objinput.SearchKey = RequestItem.objData.SearchKey;
                objinput.routeNo = RequestItem.objData.routeNo;
                objinput.serviceType = RequestItem.objData.serviceType;
                objinput.userNo = RequestItem.objData.userNo;
                objinput.AnalyzerNo = RequestItem.objData.AnalyzerNo;//idtype (NRIC/FIN)
                objinput.FranchiseNo = RequestItem.objData.FranchiseNo;//Nationality
                objinput.isStat = RequestItem.objData.isStat;//for output report purpose, paging concept is not required
                objinput.searchType = RequestItem.objData.searchType;
                lst = GetPatientImpression(objinput);
                string lsttojson = "";
                
                if (lst != null && lst.Count > 0)
                {
                    lsttojson = JsonConvert.SerializeObject(lst);
                    datable = reportRepo.ManuallyConvertJsonToDataTable(lsttojson);
                    var _Dictionary = RequestItem.ReportParamitem.ToDictionary(x => x.key, x => x.value);

                    using (var context = new LIMSContext(DefaultConnection))
                    {
                        tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == RequestItem.ReportKey && x.VenueNo == RequestItem.VenueNo
                        && x.VenueBranchNo == RequestItem.VenueBranchNo).FirstOrDefault();
                        if (!Directory.Exists(tblReportMaster?.ExportPath))
                        {
                            Directory.CreateDirectory(tblReportMaster?.ExportPath);
                        }
                        ReportParamDto objitem = new ReportParamDto();
                        objitem.datatable = CommonExtension.DatableToDicionary(datable);
                        objitem.paramerter = _Dictionary;
                        objitem.ReportPath = tblReportMaster.ReportPath;
                        if (RequestItem.fileType == "excel")
                        {
                            objitem.ExportPath = tblReportMaster?.ExportPath + "x_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".xls";
                            objitem.ExportFormat = FileFormat.EXCEL;
                        }
                        else
                        {
                            objitem.ExportPath = tblReportMaster?.ExportPath + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                            objitem.ExportFormat = FileFormat.PDF;
                        }
                        string ReportParam = JsonConvert.SerializeObject(objitem);

                        objAppSettingResponse = new AppSettingResponse();
                        string AppReportServiceURL = "ReportServiceURL";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppReportServiceURL);
                        string ReportServiceURL = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                ? objAppSettingResponse.ConfigValue : "";
                        string filename = await ExportReportService.ExportPrint(ReportParam, ReportServiceURL);
                        obj.ExportURL = "";
                        obj.PatientExportFolderPath = objitem.ExportPath;
                        obj.PatientExportFile = tblReportMaster?.ExportURL + filename;
                    }
                }
                else
                {
                    lsttojson = "";
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetPatientImpressionReport", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return obj;
        }

        #region VisitMerge 
        public InsertVisitMergeResponse SaveVisitMerge(SaveResultforVisitMergeResponse req)
        {
            InsertVisitMergeResponse obj = new InsertVisitMergeResponse();
            try
            {
                List<BulkCultureResultResponse> lstCultures = new List<BulkCultureResultResponse>();
                string outxml = string.Empty;
                CommonHelper commonUtility = new CommonHelper();
                if (req != null && req.lstResultforVisitMerge != null && req.lstResultforVisitMerge.Count > 0)
                {
                    outxml = commonUtility.ToXML(req.lstResultforVisitMerge);
                }
                else
                {
                    outxml = commonUtility.ToXML(lstCultures);
                }
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _action = new SqlParameter("action", req.action);
                    var _odxml = new SqlParameter("odxml", outxml);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _visitno = new SqlParameter("visitno", req.visitno);
                    
                    var lstexe = context.SaveVisitMergeResponse.FromSqlRaw(
                    "Execute dbo.pro_InsertResultforVisitMerge @pagecode, @action, @odxml, @venueno, @venuebranchno, @userno, @visitno",
                    _pagecode, _action, _odxml, _venueno, _venuebranchno, _userno, _visitno).ToList();
                    
                    obj.OStatus = lstexe != null && lstexe.Count > 0 ? lstexe[0].OStatus : 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.SaveVisitMerge - " + req.visitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebno, 0);
            }
            return obj;
        }
        public List<GetResultforVisitMergeResponse> GetVisitMerge(VisitMergeRequest req)
        {
            List<GetResultforVisitMergeResponse> lstVisitMerge = new List<GetResultforVisitMergeResponse>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _fromtovisitflag = new SqlParameter("fromtovisitflag", req.fromtovisitflag);
                    var _flag = new SqlParameter("flag", req.flag);
                    var _fromvisitno = new SqlParameter("fromvisitno", req.fromvisitno);

                    var lstexe = context.GetVisitMergeRequest.FromSqlRaw(
                    "Execute dbo.pro_GetResultforVisitMerge @PageCode, @VenueNo, @VenueBranchNo, @UserNo, @PatientVisitNo, @FromToVisitFlag, @Flag, @FromVisitNo",
                    _pagecode, _venueno, _venuebranchno, _userno, _patientvisitno, _fromtovisitflag, _flag, _fromvisitno).ToList();

                    if (lstexe != null && lstexe.Count > 0)
                    {
                        for (int t = 0; t < lstexe.Count; t++)
                        {
                            GetResultforVisitMergeResponse obj = new GetResultforVisitMergeResponse();
                            obj.id = lstexe[t].id;
                            obj.fpatientname = lstexe[t].fpatientname;
                            obj.fage = lstexe[t].fage;
                            obj.fvisitid = lstexe[t].fvisitid;
                            obj.fvisitdttm = lstexe[t].fvisitdttm;
                            obj.freferraltypeno = lstexe[t].freferraltypeno;
                            obj.freferraltype = lstexe[t].freferraltype;
                            obj.fclientno = lstexe[t].fclientno;
                            obj.fphysicianno = lstexe[t].fphysicianno;
                            obj.fclientname = lstexe[t].fclientname;
                            obj.fphysicianname = lstexe[t].fphysicianname;
                            obj.fgrouppackno = lstexe[t].fgrouppackno;
                            obj.ftestno = lstexe[t].ftestno;
                            obj.fsubtestno = lstexe[t].fsubtestno;
                            obj.ftesttype = lstexe[t].ftesttype;
                            obj.fgrouppackname = lstexe[t].fgrouppackname;
                            obj.ftestname = lstexe[t].ftestname;
                            obj.fsubtestname = lstexe[t].fsubtestname;
                            obj.fpatientno = lstexe[t].fpatientno;
                            obj.fpatientvisitno = lstexe[t].fpatientvisitno;
                            obj.forderlistno = lstexe[t].forderlistno;
                            obj.forderdetailsno = lstexe[t].forderdetailsno;
                            obj.forderliststatus = lstexe[t].forderliststatus;
                            obj.forderliststatustext = lstexe[t].forderliststatustext;
                            obj.fresult = lstexe[t].fresult;
                            obj.fresultcomments = lstexe[t].fresultcomments;
                            obj.fresultflag = lstexe[t].fresultflag;
                            obj.fisVip = lstexe[t].fisVip;
                            obj.fresulttypeno = lstexe[t].fresulttypeno;
                            obj.fresulttype = lstexe[t].fresulttype;
                            obj.fpicklistjson = lstexe[t].fpicklistjson;

                            obj.fpicklistjsondata = new List<picklistjson>();
                            obj.fpicklistjsondata = JsonConvert.DeserializeObject<List<picklistjson>>(lstexe[t].fpicklistjson);

                            obj.fbarcode = lstexe[t].fbarcode;
                            obj.fserviceseqno = lstexe[t].fserviceseqno;
                            obj.ftestseqno = lstexe[t].ftestseqno;
                            obj.fsubtestseqno = lstexe[t].fsubtestseqno;
                            obj.fnricnumber = lstexe[t].fnricnumber;
                            obj.fdepartmentseqno = lstexe[t].fdepartmentseqno;
                            obj.fordertransactionno = lstexe[t].fordertransactionno;
                            obj.tpatientname = lstexe[t].tpatientname;
                            obj.tage = lstexe[t].tage;
                            obj.tvisitid = lstexe[t].tvisitid;
                            obj.tvisitdttm = lstexe[t].tvisitdttm;
                            obj.treferraltypeno = lstexe[t].treferraltypeno;
                            obj.treferraltype = lstexe[t].treferraltype;
                            obj.tclientno = lstexe[t].tclientno;

                            obj.tphysicianno = lstexe[t].tphysicianno;
                            obj.tclientname = lstexe[t].tclientname;
                            obj.tphysicianname = lstexe[t].tphysicianname;
                            obj.tgrouppackno = lstexe[t].tgrouppackno;
                            obj.ttestno = lstexe[t].ttestno;
                            obj.tsubtestno = lstexe[t].tsubtestno;
                            obj.ttesttype = lstexe[t].ttesttype;
                            obj.tgrouppackname = lstexe[t].tgrouppackname;
                            obj.ttestname = lstexe[t].ttestname;
                            obj.tsubtestname = lstexe[t].tsubtestname;
                            obj.tpatientno = lstexe[t].tpatientno;
                            obj.tpatientvisitno = lstexe[t].tpatientvisitno;
                            obj.torderlistno = lstexe[t].torderlistno;
                            obj.torderdetailsno = lstexe[t].torderdetailsno;
                            obj.torderliststatus = lstexe[t].torderliststatus;
                            obj.torderliststatustext = lstexe[t].torderliststatustext;
                            obj.tresult = lstexe[t].tresult;
                            obj.tresultcomments = lstexe[t].tresultcomments;
                            obj.tresultflag = lstexe[t].tresultflag;
                            obj.tisVip = lstexe[t].tisVip;
                            obj.tresulttypeno = lstexe[t].tresulttypeno;
                            obj.tresulttype = lstexe[t].tresulttype;
                            obj.tpicklistjson = lstexe[t].tpicklistjson;

                            obj.tpicklistjsondata = new List<picklistjson>();
                            obj.tpicklistjsondata = JsonConvert.DeserializeObject<List<picklistjson>>(lstexe[t].tpicklistjson);

                            obj.tbarcode = lstexe[t].tbarcode;
                            obj.tserviceseqno = lstexe[t].tserviceseqno;
                            obj.ttestseqno = lstexe[t].ttestseqno;
                            obj.tsubtestseqno = lstexe[t].tsubtestseqno;
                            obj.tnricnumber = lstexe[t].tnricnumber;
                            obj.tdepartmentseqno = lstexe[t].tdepartmentseqno;
                            obj.tordertransactionno = lstexe[t].tordertransactionno;
                            obj.ismerged = lstexe[t].ismerged;
                            obj.isfromvisitvalidated = lstexe[t].isfromvisitvalidated;
                            obj.istovisitvalidated = lstexe[t].istovisitvalidated;
                            obj.fromvisitvalidatemessage = lstexe[t].fromvisitvalidatemessage;
                            obj.tovisitvalidatemessage = lstexe[t].tovisitvalidatemessage;
                            lstVisitMerge.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetVisitMerge - " + req.fromvisitno.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, 0);
            }
            return lstVisitMerge;
        }
        #endregion

        #region infection control
        private async Task<int> PushMessageInfectionControl(int patientVisitNo, int venueno, int venuebranchno, int userno, string pagecode, string resulttypenos, string fullname)
        {
            int result = 0;
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            int resultype = resulttypenos != null && resulttypenos != "" ? Convert.ToInt32(resulttypenos) : 0;
            
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", patientVisitNo);
                    var _venueno = new SqlParameter("VenueNo", venueno);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", venuebranchno);
                    var _type = new SqlParameter("Type", resultype);
                    
                    var lst = context.CheckInvestigationReport.FromSqlRaw(
                    "Execute dbo.Pro_CheckInfectionControlAvail @PatientVisitNo,@VenueNo,@VenueBranchNo,@Type", 
                    _PatientVisitNo, _venueno, _venuebranchno, _type).ToList();
                    
                    if (lst != null && lst.Count > 0 && lst[0].status == 1)
                    {
                        if (lst[0].mailto != null && lst[0].mailto != "")
                        {
                            string[] maillst = lst[0].mailto.Split(',');
                            string visitid = lst[0].visitid != null && lst[0].visitid != "" ? lst[0].visitid : "";
                            if (maillst != null && maillst.Count() > 0)
                            {
                                for (int d = 0; d < maillst.Count(); d++)
                                {
                                    if (maillst[d] != null && maillst[d] != "")
                                    {
                                        string address = maillst[d].ToString();

                                        //mail contet push
                                        string ollst = lst[0].orderlistno != null && lst[0].orderlistno > 0 ? lst[0].orderlistno.ToString() : "";
                                        PatientReportDTO PatientItem = new PatientReportDTO();
                                        PatientReportRepository objRepository = new PatientReportRepository(_config);
                                        PatientItem.fullname = fullname.Replace(".", "");
                                        PatientItem.isheaderfooter = true;
                                        PatientItem.orderlistnos = ollst;
                                        PatientItem.pagecode = "PCPR";
                                        PatientItem.patientvisitno = patientVisitNo.ToString();
                                        PatientItem.process = 2;
                                        PatientItem.resulttypenos = resulttypenos;
                                        PatientItem.userno = userno;
                                        PatientItem.venueno = venueno;
                                        PatientItem.venuebranchno = venuebranchno;
                                        List<ReportOutput> data = await objRepository.PrintPatientReport(PatientItem);
                                        NotificationDto objDTO = new NotificationDto();
                                        CommonRepository objCommonRepository = new CommonRepository(_config);
                                        objDTO.Address = address;
                                        objDTO.MessageType = "Email";
                                        objDTO.TemplateKey = "Patient_Infection_Approve_Email";
                                        objDTO.VenueNo = venueno;
                                        objDTO.VenueBranchNo = venuebranchno;
                                        objDTO.UserNo = userno;
                                        objDTO.ScheduleTime = DateTime.Now;
                                        //
                                        Dictionary<string, string> objMessageItem = new Dictionary<string, string>();
                                        objMessageItem.Add("#PaitentName#", fullname);
                                        objMessageItem.Add("#VisitID#", visitid);
                                        objMessageItem.Add("#URL#", data[0].PatientExportFile);
                                        objDTO.MessageItem = objMessageItem;
                                        objDTO.IsAttachment = true;
                                        objDTO.PatientVisitNo = Convert.ToInt32(patientVisitNo);
                                        Dictionary<string, string> objAttachment = new Dictionary<string, string>();
                                        objAttachment.Add(fullname + ".pdf", data[0].PatientExportFile);
                                        objDTO.AttachmentItem = objAttachment;
                                        objCommonRepository.SendMessage(objDTO);
                                        result = 1;
                                    }
                                }
                                saveinfectioncontroldetrequest ob = new saveinfectioncontroldetrequest();
                                saveinfectioncontroldetresponse outs = new saveinfectioncontroldetresponse();
                                ob.PatientVisitNo = patientVisitNo; ;
                                ob.Type = resultype;
                                ob.VenueNo = venueno;
                                ob.VenueBranchNo = venuebranchno;
                                ob.UserNo = userno;
                                outs = InsertInfectionControlAvailDatas(ob);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.PushMessageInfectionControl" + patientVisitNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, venueno, venuebranchno, userno);
            }
            return result;
        }

        public saveinfectioncontroldetresponse InsertInfectionControlAvailDatas(saveinfectioncontroldetrequest req)
        {
            saveinfectioncontroldetresponse obj = new saveinfectioncontroldetresponse();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", req.PatientVisitNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", req.UserNo);
                    var _Type = new SqlParameter("Type", req.Type);

                    var lst = context.InsertInfectionControlAvailData.FromSqlRaw(
                    "Execute dbo.Pro_InserInfectionControlAvailData @PatientVisitNo,@VenueNo,@VenueBranchNo,@Type,@UserNo",
                    _PatientVisitNo, _VenueNo, _VenueBranchNo, _Type, _UserNo).ToList();
                    
                    if (lst != null && lst.Count > 0)
                    {
                        obj.OutStatus = lst[0].OutStatus;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.InsertInfectionControlAvailDatas", ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return obj;
        }
        #endregion
       
        #region LogicComments Get
        public List<logicCommentsRespose> GetLogicComments(logicCommentsRequest req)
        {
            List<logicCommentsRespose> lst = new List<logicCommentsRespose>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _logicName = new SqlParameter("LogicName", req.logicName);
                    var _serviceType = new SqlParameter("ServiceType", req.serviceType);
                    var _serviceNo = new SqlParameter("ServiceNo", req.serviceNo);
                    var _venueno = new SqlParameter("VenueNo", req.venueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", req.venueBranchNo);
                    var _userNo = new SqlParameter("UserNo", req.userNo);

                    lst = context.GetLogicComment.FromSqlRaw(
                    "Execute dbo.pro_GetLogicComments @LogicName,@ServiceType,@ServiceNo,@VenueNo,@VenueBranchNo,@UserNo",
                    _logicName, _serviceType, _serviceNo, _venueno, _venuebranchno, _userNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetLogicComments", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        #endregion
        
        #region Extra subtest flag based calculation for DC
        public extrasubtestflagbasedformularesponse GetExtrasubtestbasedformula(extrasubtestflagbasedformularequest req)
        {
            extrasubtestflagbasedformularesponse obj = new extrasubtestflagbasedformularesponse();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pageCode);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _userno = new SqlParameter("userno", req.userNo);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientVisitNo);
                    var _serviceno = new SqlParameter("serviceno", req.serviceNo);
                    var _servicetype = new SqlParameter("servicetype", req.serviceType);

                    var lst = context.GetExtrasubtestbasedformula.FromSqlRaw(
                    "Execute dbo.pro_GetIndividualTestFormulaJson @pagecode,@venueno,@venuebranchno,@userno,@patientvisitno,@serviceno,@servicetype",
                    _pagecode, _venueno, _venuebranchno, _userno, _patientvisitno, _serviceno, _servicetype).ToList();
                    
                    if (lst != null && lst.Count > 0)
                    {
                        obj.formulaParameterJson = lst[0].formulaParameterJson;
                        obj.formulaJson = lst[0].formulaJson;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetExtrasubtestbasedformula", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        #endregion

        public List<GetOldResultThroughDIResponse> GetOldResultThroughDIs(GetOldResultThroughDIRequest req)
        {
            List<GetOldResultThroughDIResponse> lst = new List<GetOldResultThroughDIResponse>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", req.PatientVisitNo);
                    var _OrderListNo = new SqlParameter("OrderListNo", req.OrderListNo);
                    var _OrderDetailsNo = new SqlParameter("OrderDetailsNo", req.OrderDetailsNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", req.UserNo);

                    lst = context.GetOldResultThroughDI.FromSqlRaw(
                    "Execute dbo.pro_GetOldResultThroughDI @PatientVisitNo,@OrderListNo,@OrderDetailsNo,@VenueBranchNo,@VenueNo,@UserNo",
                    _PatientVisitNo, _OrderListNo, _OrderDetailsNo, _VenueBranchNo, _VenueNo, _UserNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetOldResultThroughDIs", ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return lst;
        }

        public async Task<BulkResultSaveResponse> AutoApprovalBulkResult(List<objbulkresult> req)
        {
            req.Where(x =>
            {
                return true;
            }).ToList().ForEach(result =>
            {
                result.pagecode = "PCRA";
            });
            return await Task.Factory.StartNew(() => this.SaveBulkResultEtry(req));
        }
        
        public async Task<resultrtn> AutoApprovalResult(objresult originalReq, resultrtn req)
        {
            if (originalReq != null)
            {
                originalReq.pagecode = "PCRA";
                originalReq.lstvisit.Where(x => !x.isAbnormalAvail).ToList().ForEach(visit =>
                {
                    visit.validatedon = visit.enteredon;
                    visit.approvedon = visit.enteredon;

                    visit.lstorderlist.Where(x => !x.isabnormal).ToList().ForEach(order =>
                    {
                        order.lstorderdetail.ForEach(orderDetail =>
                        {
                            if (decimal.TryParse(orderDetail.llcolumn, out decimal llColumn) && decimal.TryParse(orderDetail.hlcolumn, out decimal hlColumn) && decimal.TryParse(orderDetail.result, out decimal result))
                            {
                                if (orderDetail.isDeltaApproval || orderDetail.isdelta)
                                {
                                    var range = orderDetail.deltaRange;
                                    var isPrsent = decimal.TryParse(orderDetail.prevresult, out decimal lastResult);
                                    var posdelta = result + (result * (range / 100));
                                    var negdelta = result - (result * (range / 100));
                                    if (result >= negdelta && result <= posdelta)
                                    {
                                        orderDetail.statusName = "Result Entered";
                                    }
                                }
                                else if (result >= llColumn && result <= hlColumn)
                                {
                                    orderDetail.statusName = "Result Entered";
                                }
                            }
                        });
                    });
                });
                return await this.InsertResult(originalReq);
            }
            return req;

        }

        //PBF Test Comment        
        public PBFTestResponse GetPBFAutoComment(PBFTestRequest req)
        {
            PBFTestResponse obj = new PBFTestResponse();            
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("patientvisitno", req.patientvisitno);                    
                    var _OrderDetailsNo = new SqlParameter("orderdetailsno", req.orderdetailsno);
                    var _VenueNo = new SqlParameter("venueno", req.venueno);
                    var _VenueBranchNo = new SqlParameter("venuebranchno", req.venuebranchno);                    

                   var lst = context.GetPBFAutoComments.FromSqlRaw(
                    "Execute dbo.pro_GetPBFAutoCommentResult @venueno,@venuebranchno,@orderdetailsno,@patientvisitno",
                    _VenueNo, _VenueBranchNo,_OrderDetailsNo, _PatientVisitNo).ToList();
                    
                    if (lst != null && lst.Count > 0) {
                        obj.status = lst[0].status;
                        obj.resultComment = lst[0].resultComment;
                    }
                    else
                    {
                        obj.status = 0;
                        obj.resultComment = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetPBFAutoComment", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, 0);
            }
            return obj;
        }

        public async Task<objUpdPartialEntryFlagResponse> UpdatePartialResultFlag(objUpdPartialEntryFlagRequest req)
        {
            objUpdPartialEntryFlagResponse objResponse = new objUpdPartialEntryFlagResponse();

            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PageCode = new SqlParameter("PageCode", req.pagecode);
                    var _PVNo = new SqlParameter("PVNo", req.visitNo);
                    var _OrdLstNo = new SqlParameter("OrdLstNo", req.orderListNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.venueno);
                    var _VenueBrNo = new SqlParameter("VenueBrNo", req.venuebranchno);
                    var _UserNo = new SqlParameter("UserNo", req.userno);
                    var _EntryFlag = new SqlParameter("EntryFlag", req.entryFlag);
                    var _ValidationFlag = new SqlParameter("ValidationFlag", req.validationFlag);

                    var lst = await context.InsertPartialResultFlag.FromSqlRaw(
                    "Execute dbo.pro_UpdatePartialResultFlag @PVNo, @OrdLstNo, @EntryFlag, @ValidationFlag, @PageCode, @VenueNo, @VenueBrNo, @UserNo",
                    _PVNo, _OrdLstNo, _EntryFlag, _ValidationFlag, _PageCode, _VenueNo, _VenueBrNo, _UserNo).ToListAsync();

                    objResponse = lst[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.UpdatePartialResultFlag - " + req.visitNo.ToString(), ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return objResponse;
        }
        #region Patient Result - Pending Test
        public List<PendingVisitDetailsRes> GetPendingVisitDetails(PendingVisitDetailsReq req)
        {
            List<PendingVisitDetailsRes> lst = new List<PendingVisitDetailsRes>();
            try
            {
                using (var context = new ResultContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req.viewvenuebranchno);
                    var _pageindex = new SqlParameter("pageindex", req.pageindex);
                    var _type = new SqlParameter("type", req.type);
                    var _fromdate = new SqlParameter("fromdate", req.fromdate);
                    var _todate = new SqlParameter("todate", req.todate);
                    var _patientno = new SqlParameter("patientno", req.patientno);
                    var _patientvisitno = new SqlParameter("patientvisitno", req.patientvisitno);
                    var _deptno = new SqlParameter("deptno", req.deptno);
                    var _serviceno = new SqlParameter("serviceno", req.serviceno);
                    var _servicetype = new SqlParameter("servicetype", req.servicetype);
                    var _refferraltypeno = new SqlParameter("refferraltypeno", req.refferraltypeno);
                    var _customerno = new SqlParameter("customerno", req.customerno);
                    var _physicianno = new SqlParameter("physicianno", req.physicianno);
                    var _orderstatus = new SqlParameter("orderstatus", req.orderstatus);
                    var _maindeptNo = new SqlParameter("maindeptNo", req.maindeptNo);
                    var _pageCount = new SqlParameter("pageCount", req.pageCount);
                    lst = context.PendingVisitDetailsLst.FromSqlRaw(
                        "Execute dbo.pro_GetPendingVisitDetails @pagecode,@venueno,@venuebranchno,@userno,@viewvenuebranchno,@pageindex,@type,@fromdate,@todate,@patientno,@patientvisitno,@deptno,@serviceno,@servicetype,@refferraltypeno,@customerno,@physicianno,@orderstatus,@maindeptNo,@pageCount",
                        _pagecode, _venueno, _venuebranchno, _userno, _viewvenuebranchno, _pageindex, _type, _fromdate, _todate, _patientno, _patientvisitno, _deptno, _serviceno, _servicetype, _refferraltypeno, _customerno, _physicianno, _orderstatus, _maindeptNo, _pageCount).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetPendingVisitDetails" + req.patientno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return lst;
        }
        #endregion
    }
}
