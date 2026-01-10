using DEV.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEV.Model
{

    public partial class requestsearchresultvisit
    {
        public string pagecode { get; set; }
        public int viewvenuebranchno { get; set; }
        public int searchby { get; set; }
        public string searchtext { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
    }
    public partial class lstsearchresultvisit
    {
        public int patientvisitno { get; set; }
        public string displaytext { get; set; }
        public string searchdisplaytext { get; set; }
        public int patientno { get; set; }
    }
    public partial class requestresultvisit
    {
        public Int16 maindeptNo { get; set; }
        public string pagecode { get; set; }
        public int viewvenuebranchno { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int pageindex { get; set; }
        public string type { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int deptno { get; set; }
        public int serviceno { get; set; }
        public string servicetype { get; set; }
        public bool ismachinevalue { get; set; }
        public int refferalType { get; set; }
        public int customerNo { get; set; }
        public int physicianNo { get; set; }
        public int patientvisitno { get; set; }
        public int templateno { get; set; }
        public int routeNo { get; set; }
        public bool isReRunFilter { get; set; }
        public bool isRecheckFilter { get; set; }
        public bool isRecollectFilter { get; set; }
        public bool isRecallFilter { get; set; }
        public Int16 machineId { get; set; }
        public Int16 testStatusFilterNo { get; set; }
        public bool isSTATFilter { get; set; }
        public bool isTATFilter { get; set; }
        public int companyNo { get; set; }
        public string multiDeptNo { get; set; }
        public int patientno { get; set; }
        public string multiFieldsSearch { get; set; }
    }
    public partial class lstresultvisitdbl
    {
        public int rowno { get; set; }
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public string rhNo { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agetype { get; set; }
        public string gender { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public string taskdttm { get; set; }
        public string referraltype { get; set; }
        public string customername { get; set; }
        public string physicianname { get; set; }
        public bool visStat { get; set; }
        public bool visAbnormal { get; set; }
        public bool visCritical { get; set; }
        public bool visTAT { get; set; }
        public bool visRemarks { get; set; }
        public bool visCPRemarks { get; set; }
        public int totalRecords { get; set; }
        public int orderlistno { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicename { get; set; }
        public int resulttypeno { get; set; }
        public int orderliststatus { get; set; }
        public string orderliststatustext { get; set; }
        public string barcodeno { get; set; }
        public bool isMachineValue { get; set; }
        public bool isRecheck { get; set; }
        public bool isRecollect { get; set; }
        public bool isRecall { get; set; }
        public bool isReRun { get; set; }
        public bool isOutSource { get; set; }
        public bool isAbnormal { get; set; }
        public bool isCritical { get; set; }
        public bool isTAT { get; set; }
        public bool isRemarks { get; set; }
        public bool isCPRemarks { get; set; }
        public int tATFlag { get; set; }
        public string departmentName { get; set; }
        public int availStatus { get; set; }
        public string venueBranchName { get; set; }
        public string nricnumber { get; set; }
        public bool isVipIndication { get; set; }
        public bool isSecondReviewAvail { get; set; }
    }
    public partial class lstresultvisit
    {
        public Int16 maindeptNo { get; set; }
        public int rowno { get; set; }
        public int patientno { get; set; }
        public string rhNo { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agetype { get; set; }
        public string gender { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public string taskdttm { get; set; }
        public string referraltype { get; set; }
        public string customername { get; set; }
        public string physicianname { get; set; }
        public bool visStat { get; set; }
        public bool visAbnormal { get; set; }
        public bool visCritical { get; set; }
        public bool visTAT { get; set; }
        public bool visRemarks { get; set; }
        public bool visCPRemarks { get; set; }
        public int totalRecords { get; set; }
        public List<lstservice> lstservice { get; set; }
        public string venueBranchName { get; set; }
        public string nricnumber { get; set; }
        public bool isVipIndication { get; set; }
        public bool isSecondReviewAvail { get; set; }
    }
    public partial class lstservice
    {
        public int patientvisitno { get; set; }
        public int orderlistno { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicename { get; set; }
        public int resulttypeno { get; set; }
        public int orderliststatus { get; set; }
        public string orderliststatustext { get; set; }
        public string barcodeno { get; set; }
        public bool isMachineValue { get; set; }
        public bool isRecheck { get; set; }
        public bool isRecollect { get; set; }
        public bool isRecall { get; set; }
        public bool isReRun { get; set; }
        public bool isOutSource { get; set; }
        public bool isAbnormal { get; set; }
        public bool isCritical { get; set; }
        public bool isTAT { get; set; }
        public bool isRemarks { get; set; }
        public bool isCPRemarks { get; set; }
        public int tATFlag { get; set; }
        public string departmentName { get; set; }
        public int availStatus { get; set; }
        public bool isVipIndication { get; set; }
    }

    public partial class requestdeltaresult
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int patientno { get; set; }
        public int testno { get; set; }
        public int subtestno { get; set; }
        public string mrdnumber { get; set; }
        public string nricnumber { get; set; }
    }

    public partial class deltaresult
    {
        public int rowno { get; set; }
        public int patientno { get; set; }
        public int testno { get; set; }
        public int subtestno { get; set; }
        public string testname { get; set; }
        public string resultvalue { get; set; }
        public string resultdate { get; set; }
        public string testCode { get; set; }
        public string visitId { get; set; }
        public string resultComments { get; set; }
        public string unitName { get; set; }
    }

    public partial class requestresult
    {
        public string pagecode { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int patientvisitno { get; set; }
        public int deptno { get; set; }
        public int serviceno { get; set; }
        public string servicetype { get; set; }
        public int type { get; set; }
        public int CCUnitNo { get; set; }
        public int patientno { get; set; }
        public int viewvenuebranchno { get; set; }

    }
    public partial class lstresultdbl
    {
        public int rowno { get; set; }
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agetype { get; set; }
        public string gender { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public string referraltype { get; set; }
        public string customername { get; set; }
        public string physicianname { get; set; }
        public bool visstat { get; set; }
        public bool visremarks { get; set; }
        public string address { get; set; }
        public string dob { get; set; }
        public string urntype { get; set; }
        public string urnid { get; set; }
        public string samplecollectedon { get; set; }
        public string enteredon { get; set; }
        public string validatedon { get; set; }
        public string approvedon { get; set; }
        //--
        public int orderlistno { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int departmentseqno { get; set; }
        public string samplename { get; set; }
        public string barcodeno { get; set; }
        public string serviceCode { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicename { get; set; }
        public int serviceseqno { get; set; }
        public int resulttypeno { get; set; }
        public bool isrerun { get; set; }
        public bool isrecollect { get; set; }
        public bool isrecheck { get; set; }
        public bool isrecall { get; set; }
        public bool isoutsource { get; set; }
        public bool isoutsourceattachment { get; set; }
        public bool isattachment { get; set; }
        public bool isremarks { get; set; }
        public bool istat { get; set; }
        public bool iscontestinter { get; set; }
        public int groupinter { get; set; }
        public bool ischecked { get; set; }
        //-- 
        public int orderdetailsno { get; set; }
        public string testtype { get; set; }
        public int testno { get; set; }
        public string testname { get; set; }
        public int tseqno { get; set; }
        public int subtestno { get; set; }
        public string subtestname { get; set; }
        public int sseqno { get; set; }
        public string resulttype { get; set; }
        public int mastermethodno { get; set; }
        public int masterunitno { get; set; }
        public string masterllcolumn { get; set; }
        public string masterhlcolumn { get; set; }
        public string masterdisplayrr { get; set; }
        public string crllcolumn { get; set; }
        public string crhlcolumn { get; set; }
        public string minrange { get; set; }
        public string maxrange { get; set; }
        public bool isnonmandatory { get; set; }
        public bool isdelta { get; set; }
        public bool istformula { get; set; }
        public bool issformula { get; set; }
        public int decimalpoint { get; set; }
        public bool isroundoff { get; set; }
        public bool isformulaparameter { get; set; }
        public int formulaserviceno { get; set; }
        public string formulaservicetype { get; set; }
        public string formulajson { get; set; }
        public string formulaparameterjson { get; set; }
        public string picklistjson { get; set; }
        public int headerno { get; set; }
        public bool isedit { get; set; }
        public int testinter { get; set; }
        //
        public int methodno { get; set; }
        public string methodname { get; set; }
        public int unitno { get; set; }
        public string unitname { get; set; }
        public string llcolumn { get; set; }
        public string hlcolumn { get; set; }
        public string displayrr { get; set; }
        public string result { get; set; }
        public string formularesult { get; set; }
        public string diresult { get; set; }
        public string resultflag { get; set; }
        public string resultcomments { get; set; }
        public string internotes { get; set; }
        public bool isrerunod { get; set; }
        public int notifyCount { get; set; }
        public bool isUploadOption { get; set; }
        public string uploadedFile { get; set; }
        public int isMultiEditor { get; set; }
        public Int16 approvalDoctor { get; set; }
        public string venueBranchName { get; set; }
        public string statusName { get; set; }
        public bool isnoresult { get; set; }
        public string nricNumber { get; set; }
        public bool isSecondReview { get; set; }
        public bool isSecondReviewAvail { get; set; }
        public decimal lesserValue { get; set; }
        public decimal greaterValue { get; set; }
        public string prevresult { get; set; }
        public string prevresultrefrange { get; set; }
        public string prevresultdttm { get; set; }
        public bool isAbnormalAvail { get; set; }
        public Int16 isDeptAvail { get; set; }
        public bool isSensitiveData { get; set; }
        public int isNotify { get; set; }
        public string calcprevresult { get; set; }
        public decimal calcprevresultper { get; set; }
        public decimal calcprevresultdif { get; set; }
        public bool isVipIndication { get; set; }
        public bool isLock { get; set; }
        public int snomedId { get; set; }
        public bool isWBC { get; set; }
        public bool isDC { get; set; }
        public int isSubTestDeptNotMapd { get; set; }
        public bool isExtraSubTest { get; set; }
        public bool isPCVTest { get; set; }
        public bool isHGBAvail { get; set; }
        public bool isHGBTest { get; set; }
        public bool isPTTAvail { get; set; }
        public string pTTRestrictedValue { get; set; }
        public string hGBMessage { get; set; }
        public string pTTMessage { get; set; }
        public string validFromRange { get; set; }
        public string validToRange { get; set; }
        public bool isPCVValidation { get; set; }
        public bool isPttRestrictedValueExists { get; set; }
        public string ivalue { get; set; }
        public string hvalue { get; set; }
        public string lvalue { get; set; }
        public bool isIHLValueAvail { get; set; }
        public bool isDeltaApproval { get; set; }
        public decimal deltaRange { get; set; }
        public bool isDeltaApprovalRestriction { get; set; }
        public string approvedprevresult { get; set; }
        public bool isInfectionControl { get; set; }
        public bool isLogicNeeded { get; set; }
        public string logicneededjson { get; set; }
        public bool isExtraSubTestEnable { get; set; }
        public bool isprevdiresultavail { get; set; }
        public bool IsOtherDeptSrvAvailble { get; set; }
        public string rhNo { get; set; }
        public string taskdttm { get; set; }
        public bool isPediatricSample { get; set; }
        public string groupMultiSampleComments { get; set; }
        public string dIComment { get; set; }
        public bool isIndRerun { get; set; }
        public bool isMCHC { get; set; }
        public bool isBlast { get; set; }
        public bool isAMC { get; set; }
        public string blasDTTM { get; set; }
        public string oDTestCode { get; set; }
        public string oTTestCode { get; set; }
        public bool isOBNegative { get; set; }
        public bool isOBTest { get; set; }
        public string allergyInfo { get; set; }
        public int subtestDeptNo { get; set; }
        public string ObTestCode { get; set; }
        public string ObPositiveCode { get; set; }
        public bool isPermutation { get; set; }
        public string permutationCode { get; set; }
        public string permutationCodeType { get; set; }
        public string prevDIResults { get; set; }
        public bool isPBFTest { get; set; }
        public bool IsPartialEntryMaster { get; set; }
        public bool IsPartialValidationMaster { get; set; }
        public bool IsPartialEntryTrans { get; set; }
        public bool IsPartialValidationTrans { get; set; }
        public int resultAckType { get; set; }
        public string prevABORHResult { get; set; }
        public bool isAbnormalRemove { get; set; }
    }
    public partial class objresult
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public string pagecode { get; set; }
        public string action { get; set; }
        public List<lstvisit> lstvisit { get; set; }
        public string odxml { get; set; }
    }
    public partial class lstvisit
    {
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agetype { get; set; }
        public string gender { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public string referraltype { get; set; }
        public string customername { get; set; }
        public string physicianname { get; set; }
        public bool visstat { get; set; }
        public bool visremarks { get; set; }

        public string address { get; set; }
        public string dob { get; set; }
        public string urntype { get; set; }
        public string urnid { get; set; }
        public string samplecollectedon { get; set; }
        public string enteredon { get; set; }
        public string validatedon { get; set; }
        public string approvedon { get; set; }
        public List<lstorderlist> lstorderlist { get; set; }
        public int NotifyCount { get; set; }
        public string venueBranchName { get; set; }
        public string nricNumber { get; set; }
        public bool isAbnormalAvail { get; set; }
        public bool isVipIndication { get; set; }
        public bool IsOtherDeptSrvAvailble { get; set; }
        public string rhNo { get; set; }
        public string taskdttm { get; set; }
        public string allergyInfo { get; set; } 
    }
    public partial class lstorderlist
    {
        public int patientvisitno { get; set; }
        public int orderlistno { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int departmentseqno { get; set; }
        public string samplename { get; set; }
        public string barcodeno { get; set; }
        public string serviceCode { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicename { get; set; }
        public int serviceseqno { get; set; }
        public int resulttypeno { get; set; }
        public bool risrerun { get; set; }
        public bool risrecollect { get; set; }
        public bool risrecheck { get; set; }
        public bool isrecall { get; set; }
        public bool isoutsource { get; set; }
        public bool isoutsourceattachment { get; set; }
        public bool isattachment { get; set; }
        public bool isremarks { get; set; }
        public bool istat { get; set; }
        public bool iscontestinter { get; set; }
        public int groupinter { get; set; }
        public string internotes { get; set; }
        public bool ischecked { get; set; }
        public bool isrerun { get; set; }
        public bool isrecollect { get; set; }
        public bool isrecheck { get; set; }
        public bool isgrouptd { get; set; }
        public List<lstorderdetail> lstorderdetail { get; set; }
        public int isMultiEditor { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public string icmrPatientId { get; set; }
        public string srfNumber { get; set; }
        public int reportstatus { get; set; }
        public bool isnoresult { get; set; }
        public bool isSecondReview { get; set; }
        public bool isSecondReviewAvail { get; set; }
        public Int16 isDeptAvail { get; set; }
        public int isNotify { get; set; }
        public bool isLock { get; set; }
        public bool isVipIndication { get; set; }
        public bool isLockChanged { get; set; }
        public int snomedId { get; set; }
        public bool isWBC { get; set; }
        public bool isDC { get; set; }
        public int isSubTestDeptNotMapd { get; set; }
        public string ivalue { get; set; }
        public string hvalue { get; set; }
        public string lvalue { get; set; }
        public bool isIHLValueAvail { get; set; }
        public bool isInfectionControl { get; set; }
        public bool isPediatricSample { get; set; }
        public string groupMultiSampleComments { get; set; }
        public string dIComment { get; set; }
        public string oTTestCode { get; set; }
        public bool isOBNegative { get; set; }
        public bool isOBTest { get; set; }
        public bool isPermutation { get; set; }
        public string permutationCode { get; set; }
        public string permutationCodeType { get; set; }
        public bool IsPartialEntryMaster { get; set; }
        public bool IsPartialValidationMaster { get; set; }
        public bool IsPartialEntryTrans { get; set; }
        public bool IsPartialValidationTrans { get; set; }
        public bool isUploadOption { get; set; }
    }
    public partial class lstorderdetail
    {
        public int id { get; set; }
        public int orderlistno { get; set; }
        public int orderdetailsno { get; set; }
        public string testtype { get; set; }
        public int testno { get; set; }
        public string testname { get; set; }
        public int tseqno { get; set; }
        public int subtestno { get; set; }
        public string subtestname { get; set; }
        public int sseqno { get; set; }
        public string resulttype { get; set; }
        public int mastermethodno { get; set; }
        public int masterunitno { get; set; }
        public string masterllcolumn { get; set; }
        public string masterhlcolumn { get; set; }
        public string masterdisplayrr { get; set; }
        public string crllcolumn { get; set; }
        public string crhlcolumn { get; set; }
        public string displaycrrr { get; set; }
        public string minrange { get; set; }
        public string maxrange { get; set; }
        public string displaymmrr { get; set; }
        public bool isnonmandatory { get; set; }
        public bool isdelta { get; set; }
        public bool istformula { get; set; }
        public bool issformula { get; set; }
        public int decimalpoint { get; set; }
        public bool isroundoff { get; set; }
        public bool isformulaparameter { get; set; }
        public int formulaserviceno { get; set; }
        public string formulaservicetype { get; set; }
        public List<formulajson> formulajson { get; set; }
        public List<formulaparameterjson> formulaparameterjson { get; set; }
        public List<picklistjson> picklistjson { get; set; }
        public int headerno { get; set; }
        public bool isedit { get; set; }
        public int testinter { get; set; }

        public int methodno { get; set; }
        public string methodname { get; set; }
        public int unitno { get; set; }
        public string unitname { get; set; }
        public string llcolumn { get; set; }
        public string hlcolumn { get; set; }
        public string displayrr { get; set; }
        public string result { get; set; }
        public string formularesult { get; set; }
        public string diresult { get; set; }
        public string resultflag { get; set; }
        public string resultcomments { get; set; }
        public string internotes { get; set; }
        public bool risrerunod { get; set; }
        public bool isrerunod { get; set; }
        public bool isUploadOption { get; set; }
        public string uploadedfile { get; set; }
        public int isMultiEditor { get; set; }
        public Int16 approvalDoctor { get; set; }
        public string statusName { get; set; }
        public string interNotesHigh { get; set; }
        public string interNotesLow { get; set; }
        public bool noresult { get; set; }
        public bool isSecondReview { get; set; }
        public decimal lesserValue { get; set; }
        public decimal greaterValue { get; set; }
        public string prevresult { get; set; }
        public string prevresultrefrange { get; set; }
        public string prevresultdttm { get; set; }
        public Int16 isDeptAvail { get; set; }
        public bool isSensitiveData { get; set; }
        public int isNotify { get; set; }
        public string calcprevresult { get; set; }
        public decimal calcprevresultper { get; set; }
        public decimal calcprevresultdif { get; set; }
        public bool isVipIndication { get; set; }
        public int snomedId { get; set; }
        public bool isWBC { get; set; }
        public bool isDC { get; set; }
        public int isSubTestDeptNotMapd { get; set; }
        public bool isExtraSubTest { get; set; }
        public bool isPCVTest { get; set; }
        public bool isHGBAvail { get; set; }
        public bool isHGBTest { get; set; }
        public bool isPTTAvail { get; set; }
        public string pTTRestrictedValue { get; set; }
        public string hGBMessage { get; set; }
        public string pTTMessage { get; set; }
        public string validFromRange { get; set; }
        public string validToRange { get; set; }
        public bool isPCVValidation { get; set; }
        public bool isPttRestrictedValueExists { get; set; }
        public string ivalue { get; set; }
        public string hvalue { get; set; }
        public string lvalue { get; set; }
        public bool isIHLValueAvail { get; set; }
        public bool isDeltaApproval { get; set; }
        public decimal deltaRange { get; set; }
        public bool isDeltaApprovalRestriction { get; set; }
        public string approvedprevresult { get; set; }
        public bool isLogicNeeded { get; set; }
        public List<logicConceptResponse> logicneededjson { get; set; }
        public bool isExtraSubtestEnable { get; set; }
        public bool isprevdiresultavail { get; set; }
        public bool isIndRerun { get; set; }
        public bool isMCHC { get; set; }
        public bool isBlast { get; set; }
        public bool isAMC { get; set; }
        public string blasDTTM { get; set; }
        public string oDTestCode { get; set; }
        public bool isOBNegative { get; set; }
        public bool isOBTest { get; set; }
        public int subtestDeptNo { get; set; }
        public string ObTestCode { get; set; }
        public string ObPositiveCode { get; set; }
        public bool isPermutation { get; set; }
        public string permutationCode { get; set; }
        public string permutationCodeType { get; set; }
        public List<DIResult> prevDIResults { get; set; }
        public bool isPBFTest { get; set; }
        public int resultAckType { get; set; }
        public string prevABORHResult { get; set; }
        public bool ischecked { get; set; }
        public bool isAbnormalRemove { get; set; }
    }

    public partial class lsthistorydbl
    {
        public int rowno { get; set; }
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agetype { get; set; }
        public string gender { get; set; }
        public string visitid { get; set; }
        public string visitdttm { get; set; }
        public string referraltype { get; set; }
        public string customername { get; set; }
        public string physicianname { get; set; }
        //--
        public int orderlistno { get; set; }
        public string departmentname { get; set; }
        public int departmentseqno { get; set; }
        public string samplename { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicename { get; set; }
        public int serviceseqno { get; set; }
        public int resulttypeno { get; set; }
        public int orderdetailsno { get; set; }
        public string testtype { get; set; }
        public int testno { get; set; }
        public string testname { get; set; }
        public int tseqno { get; set; }
        public int subtestno { get; set; }
        public string subtestname { get; set; }
        public int sseqno { get; set; }
        public string resulttype { get; set; }
        public string methodname { get; set; }
        public string unitname { get; set; }
        public string displayrr { get; set; }
        public string result { get; set; }
        public string resultflag { get; set; }
        public string resultcomments { get; set; }
        public string resultcommentsflag { get; set; }
    }

    public partial class resultrtn
    {
        public int patientvisitno { get; set; }
        public int isMultiEditor { get; set; }
        public int multieditorcount { get; set; }
        public int RsltAmendNo { get; set; }
        public string RsltAmendCode { get; set; }
        public List<RecallTestDetailsResponse> lstTestDetails { get; set; }
    }
    public partial class recallResponse
    {
        public int patientvisitno { get; set; }
        public int isMultiEditor { get; set; }
        public int multieditorcount { get; set; }
        public int RsltAmendNo { get; set; }
        public string RsltAmendCode { get; set; }
        public List<RecallTestDetailsResponse> lstTestDetails { get; set; }
    }
    public partial class recallDataResponse
    {
        public int patientvisitno { get; set; }
        public int isMultiEditor { get; set; }
        public int multieditorcount { get; set; }
        public int RsltAmendNo { get; set; }
        public string RsltAmendCode { get; set; }
        public string? lstTestDetails { get; set; }
    }
    public partial class RecallTestDetailsResponse
    {
        public int orderListNo { get; set; }
        public int testNo { get; set; }
        public int subTestNo { get; set; }
        public int resultTypeNo { get; set; }
        public string resultType { get; set; }
    }
    public partial class formulajson
    {
        public int parameterserviceno { get; set; }
        public string parameterservicetype { get; set; }
        public int calculateseqno { get; set; }
        public decimal value { get; set; }
        public string foperator { get; set; }
    }

    public partial class formulaparameterjson
    {
        public int serviceno { get; set; }
        public string servicetype { get; set; }
    }
    public partial class picklistjson
    {
        public string pickvalue { get; set; }
        public bool isdefault { get; set; }
        public bool isabnormal { get; set; }
        public string comments { get; set; }
    }

    public partial class CustomerMsgDetails
    {
        public int rowno { get; set; }
        public string Address { get; set; }
        public string MessageType { get; set; }
        public string FullName { get; set; }
        public string VisitID { get; set; }
        public string EmbedURL { get; set; }
        public bool Isembed { get; set; }
        public decimal DueAmount { get; set; }
    }

    //MB
    public partial class objresultmbdbl
    {
        public int rowno { get; set; }
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agetype { get; set; }
        public string gender { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public string referraltype { get; set; }
        public string customername { get; set; }
        public string physicianname { get; set; }
        public bool visstat { get; set; }
        public bool visremarks { get; set; }

        public string address { get; set; }
        public string dob { get; set; }
        public string urntype { get; set; }
        public string urnid { get; set; }
        public string samplecollectedon { get; set; }
        public string enteredon { get; set; }
        public string validatedon { get; set; }
        public string approvedon { get; set; }
        //--
        public int orderlistno { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int departmentseqno { get; set; }
        public string samplename { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicename { get; set; }
        public int serviceseqno { get; set; }
        public string barcodeno { get; set; }
        public bool risrerun { get; set; }
        public bool risrecollect { get; set; }
        public bool risrecheck { get; set; }
        public bool isrecall { get; set; }
        public bool isoutsource { get; set; }
        public bool isoutsourceattachment { get; set; }
        public bool isattachment { get; set; }
        public bool oisremarks { get; set; }
        public bool oistat { get; set; }
        public bool isoledit { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public bool ischecked { get; set; }
        //-- 
        public int reportstatus { get; set; }
        public int resultstatus { get; set; }
        public int resultpattern { get; set; }
        public string patterndescription { get; set; }
        public int colonycount { get; set; }
        public string wetpreparation { get; set; }
        public string gramstainjson { get; set; }
        public string gramstainbottlejson { get; set; }
        public string gramstaintext { get; set; }
        public string gramstainbottletext { get; set; }
        public string comments { get; set; }
        public string mbdrugjson { get; set; }
        public Int16 approvalDoctor { get; set; }
        public string venueBranchName { get; set; }
        public string nricnumber { get; set; }
        public bool isSensitiveData { get; set; }
        public bool isSecondReviewAvail { get; set; }
        public bool isSecondReview { get; set; }
        public bool isLock { get; set; }
        public int snomedId { get; set; }
        public bool isInfectionControl { get; set; }
        public int SrcOfSpecimenNo { get; set; }
        public string SrcOfSpecimenDesc { get; set; }
        public string SrcOfSpecimenOthers { get; set; }
        public bool isNoMicroOrgSeen { get; set; }
        public int CCUnitNo { get; set; }
        public string UnitText { get; set; }
        public string AllergyInfo { get; set; }
        public string ColonyCountText { get; set; }
    }

    public partial class lstgramstainmb
    {
        public int gsno { get; set; }
        public int gsvno { get; set; }
    }


    public partial class lstgramstainmbbottle
    {
        public int gsbno { get; set; }
        public int gsvbno { get; set; }
    }

    public partial class lstmbdrugjson
    {
        public int organismtypeno { get; set; }
        public int organismno { get; set; }
        public string organismmccode { get; set; }
        public string organismname { get; set; }
        public int osequenceno { get; set; }
        public string notes { get; set; }
        public int antibioticno { get; set; }
        public string antibioticmccode { get; set; }
        public string antibioticname { get; set; }
        public int asequenceno { get; set; }
        public int interp { get; set; }
        public bool isshow { get; set; }
        public string interpvalue { get; set; }
        public string interptype { get; set; }
        public int resultpattern { get; set; }
        public string resultpatterntext { get; set; }
        public int colonycount { get; set; }

        public string colonycounttext { get; set; }
        public string orgbasednotes { get; set; }
        public int orgno { get; set; }
        public bool isInterface { get; set; }
        public string orgType { get; set; }
    }

    public partial class objresultmb
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public string pagecode { get; set; }
        public string action { get; set; }
        //--
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agetype { get; set; }
        public string gender { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public string referraltype { get; set; }
        public string customername { get; set; }
        public string physicianname { get; set; }
        public bool visstat { get; set; }
        public bool visremarks { get; set; }

        public string address { get; set; }
        public string dob { get; set; }
        public string urntype { get; set; }
        public string urnid { get; set; }
        public string samplecollectedon { get; set; }
        public string enteredon { get; set; }
        public string validatedon { get; set; }
        public string approvedon { get; set; }
        //--
        public int orderlistno { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int departmentseqno { get; set; }
        public string samplename { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicename { get; set; }
        public int serviceseqno { get; set; }
        public string barcodeno { get; set; }
        public bool risrerun { get; set; }
        public bool risrecollect { get; set; }
        public bool risrecheck { get; set; }
        public bool isrecall { get; set; }
        public bool isoutsource { get; set; }
        public bool isoutsourceattachment { get; set; }
        public bool isattachment { get; set; }
        public bool oisremarks { get; set; }
        public bool oistat { get; set; }
        public bool isoledit { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public bool ischecked { get; set; }
        public bool isrerun { get; set; }
        public bool isrecollect { get; set; }
        public bool isrecheck { get; set; }
        //-- 
        public int reportstatus { get; set; }
        public int resultstatus { get; set; }
        public int resultpattern { get; set; }
        public string patterndescription { get; set; }
        public int colonycount { get; set; }
        public string wetpreparation { get; set; }
        public List<lstgramstainmb> lstgramstainmb { get; set; }
        public List<lstgramstainmbbottle> lstgramstainmbbottle { get; set; }
        public string gramstaintext { get; set; }
        public string gramstainbottletext { get; set; }
        public string comments { get; set; }
        public List<lstorg> lstorg { get; set; }
        public string organtixml { get; set; }
        public Int16 approvalDoctor { get; set; }
        public string venueBranchName { get; set; }
        public bool ismbnoresult { get; set; }
        public string nricnumber { get; set; }
        public bool isSensitiveData { get; set; }
        public bool isSecondReviewAvail { get; set; }
        public bool isSecondReview { get; set; }
        public bool isLock { get; set; }
        public int snomedId { get; set; }
        public bool isInfectionControl { get; set; }
        public int SrcOfSpecimenNo { get; set; }
        public string SrcOfSpecimenDesc { get; set; }
        public string SrcOfSpecimenOthers { get; set; }
        public bool isNoMicroOrgSeen { get; set; }
        public int CCUnitNo { get; set; }
        public string UnitText { get; set; }
        public string AllergyInfo { get; set; }
        public string ColonyCountText { get; set; }
    }

    public partial class lstorg
    {
        public int organismtypeno { get; set; }
        public int organismno { get; set; }
        public string organismmccode { get; set; }
        public string organismname { get; set; }
        public int sequenceno { get; set; }
        public string notes { get; set; }
        public string interptype { get; set; }
        public int resultpattern { get; set; }
        public string resultpatterntext { get; set; }
        public int colonycount { get; set; }
        public string colonycounttext { get; set; }
        public string orgbasednotes { get; set; }
        public List<lstdrug> lstdrug { get; set; }
        public int orgno { get; set; }
        public bool isInterface { get; set; }
    }
    public partial class lstdrug
    {
        public int organismno { get; set; }
        public int antibioticno { get; set; }
        public string antibioticmccode { get; set; }
        public string antibioticname { get; set; }
        public int sequenceno { get; set; }
        public int interp { get; set; }
        public string interpvalue { get; set; }
        public string interprange { get; set; }
        public bool isshow { get; set; }
        public string orgType { get; set; }
    }

    public partial class objresulttemplatedbl
    {
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agetype { get; set; }
        public string gender { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public string referraltype { get; set; }
        public string customername { get; set; }
        public string physicianname { get; set; }
        public bool visstat { get; set; }
        public bool visremarks { get; set; }

        public string address { get; set; }
        public string dob { get; set; }
        public string urntype { get; set; }
        public string urnid { get; set; }
        public string samplecollectedon { get; set; }
        public string enteredon { get; set; }
        public string validatedon { get; set; }
        public string approvedon { get; set; }
        //--
        public int orderlistno { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int departmentseqno { get; set; }
        public string samplename { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicename { get; set; }
        public int serviceseqno { get; set; }
        public string barcodeno { get; set; }
        public bool risrerun { get; set; }
        public bool risrecollect { get; set; }
        public bool risrecheck { get; set; }
        public bool isrecall { get; set; }
        public bool isoutsource { get; set; }
        public bool isoutsourceattachment { get; set; }
        public bool isattachment { get; set; }
        public bool oisremarks { get; set; }
        public bool oistat { get; set; }
        public bool isoledit { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public bool ischecked { get; set; }
        //-- 
        public string icmrPatientId { get; set; }
        public string srfNumber { get; set; }
        public int templateno { get; set; }
        public string result { get; set; }
        public int reportstatus { get; set; }
        public string venueBranchName { get; set; }
        public string nricnumber { get; set; }
        public bool isSensitiveData { get; set; }
        public bool isSecondReviewAvail { get; set; }
        public bool isSecondReview { get; set; }
        public bool isLock { get; set; }
        public int snomedId { get; set; }
        public bool isAbnormalAvail { get; set; }
        public int tissueAudit { get; set; }
        public bool isInfectionControl { get; set; }
        public string malignantCase { get; set; }
        public int defaultPathologist { get; set; }
    }

    public partial class objresulttemplate
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public string pagecode { get; set; }
        public string action { get; set; }
        //--
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agetype { get; set; }
        public string gender { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public string referraltype { get; set; }
        public string customername { get; set; }
        public string physicianname { get; set; }
        public bool visstat { get; set; }
        public bool visremarks { get; set; }
        public string address { get; set; }
        public string dob { get; set; }
        public string urntype { get; set; }
        public string urnid { get; set; }
        public string samplecollectedon { get; set; }
        public string enteredon { get; set; }
        public string validatedon { get; set; }
        public string approvedon { get; set; }
        //--
        public int orderlistno { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int departmentseqno { get; set; }
        public string samplename { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicename { get; set; }
        public int serviceseqno { get; set; }
        public string barcodeno { get; set; }
        public bool risrerun { get; set; }
        public bool risrecollect { get; set; }
        public bool risrecheck { get; set; }
        public bool isrecall { get; set; }
        public bool isoutsource { get; set; }
        public bool ismachinevalue { get; set; }
        public bool isoutsourceattachment { get; set; }
        public bool isattachment { get; set; }
        public bool oisremarks { get; set; }
        public bool oistat { get; set; }
        public bool isoledit { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public bool ischecked { get; set; }
        public bool isrerun { get; set; }
        public bool isrecollect { get; set; }
        public bool isrecheck { get; set; }
        //-- 
        public int templateno { get; set; }
        public string result { get; set; }
        public string icmrPatientId { get; set; }
        public string srfNumber { get; set; }
        public int reportstatus { get; set; }
        public string colorCode { get; set; }
        public int isMultiEditor { get; set; }
        public int orderdetailsno { get; set; }
        public int multieditorcount { get; set; }
        public int subtestno { get; set; }
        public Int16 approvalDoctor { get; set; }
        public string venueBranchName { get; set; }
        public bool istmpnoresult { get; set; }
        public string nricnumber { get; set; }
        public bool isSensitiveData { get; set; }
        public bool isSecondReviewAvail { get; set; }
        public bool isLock { get; set; }
        public bool isSecondReview { get; set; }
        public int snomedId { get; set; }
        public bool isAbnormalAvail { get; set; }
        public int tissueAudit { get; set; }
        public bool isInfectionControl { get; set; }
        public string malignantCase { get; set; }
        public bool IsPartialEntryMaster { get; set; }
        public bool IsPartialValidationMaster { get; set; }
        public bool IsPartialEntryTrans { get; set; }
        public bool IsPartialValidationTrans { get; set; }
        public int defaultPathologist { get; set; }
    }


    public partial class lstrecalldbl
    {
        public int rowno { get; set; }
        public int patientVisitNo { get; set; }
        public string patientID { get; set; }
        public string fullName { get; set; }
        public string ageType { get; set; }
        public string gender { get; set; }
        public string visitID { get; set; }
        public string extenalVisitID { get; set; }
        public string barcodeNo { get; set; }
        public string visitDTTM { get; set; }
        public string referredBy { get; set; }
        public int orderListNo { get; set; }
        public string serviceType { get; set; }
        public int serviceNo { get; set; }
        public string serviceName { get; set; }
        public int orderListStatus { get; set; }
        public string orderListStatusText { get; set; }
        public string venueBranchName { get; set; }
    }
    public partial class objrecall
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int patientVisitNo { get; set; }
        public string patientID { get; set; }
        public string fullName { get; set; }
        public string ageType { get; set; }
        public string gender { get; set; }
        public string visitID { get; set; }
        public string extenalVisitID { get; set; }
        public string visitDTTM { get; set; }
        public string referredBy { get; set; }
        public List<lstRecallServicves> lstRecallServicves { get; set; }
        public string venueBranchName { get; set; }
    }

    public partial class lstRecallServicves
    {
        public bool isChecked { get; set; }
        public int patientVisitNo { get; set; }
        public int orderListNo { get; set; }
        public string barcodeNo { get; set; }
        public string serviceType { get; set; }
        public int serviceNo { get; set; }
        public string serviceName { get; set; }
        public int orderListStatus { get; set; }
        public string orderListStatusText { get; set; }
        public string comments { get; set; }
    }

    public partial class objbulkresultdbl
    {
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agetype { get; set; }
        public string gender { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public string referraltype { get; set; }
        public string customername { get; set; }
        public string physicianname { get; set; }
        public bool visstat { get; set; }
        public bool visremarks { get; set; }

        public string address { get; set; }
        public string dob { get; set; }
        public string urntype { get; set; }
        public string urnid { get; set; }
        public string samplecollectedon { get; set; }
        public string enteredon { get; set; }
        public string validatedon { get; set; }
        public string approvedon { get; set; }
        //--
        public int orderlistno { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int departmentseqno { get; set; }
        public string samplename { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicename { get; set; }
        public int serviceseqno { get; set; }
        public string barcodeno { get; set; }
        public bool risrerun { get; set; }
        public bool risrecollect { get; set; }
        public bool risrecheck { get; set; }
        public bool isrecall { get; set; }
        public bool isoutsource { get; set; }
        public bool ismachinevalue { get; set; }
        public bool isoutsourceattachment { get; set; }
        public bool isattachment { get; set; }
        public bool oisremarks { get; set; }
        public bool oistat { get; set; }
        public bool isoledit { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public bool ischecked { get; set; }
        //-- 
        public string icmrPatientId { get; set; }
        public string srfNumber { get; set; }
        public int templateno { get; set; }
        public string result { get; set; }
        public int reportstatus { get; set; }
        public string colorCode { get; set; }
        public bool IsPartialEntryMaster { get; set; }
        public bool IsPartialValidationMaster { get; set; }
        public bool IsPartialEntryTrans { get; set; }
        public bool IsPartialValidationTrans { get; set; }
    }

    public partial class objbulkresulttemplate
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public string pagecode { get; set; }
        public string action { get; set; }
        public List<objresulttemplate> lstbulkresult { get; set; }
    }

    public class orgtypeantibiotic
    {
        public bool isshow { get; set; }
        public int antibioticno { get; set; }
        public string antibioticmccode { get; set; }
        public string antibioticname { get; set; }
        public int sequenceno { get; set; }
        public int? sensitiveFrom { get; set; }
        public int? sensitiveTo { get; set; }
        public int? intermediateFrom { get; set; }
        public int? intermediateTo { get; set; }
        public int? resistantFrom { get; set; }
        public int? resistantTo { get; set; }
        public string interprange { get; set; }
    }

    public class covidWorkOrderreq
    {
        public string pagecode { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public string type { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public int refferraltypeno { get; set; }
        public int customerno { get; set; }
        public int physicianno { get; set; }
        public bool isCompleted { get; set; }
        public int orderstatus { get; set; }
        public int routeNo { get; set; }
    }

    public class covidWorkOrder
    {
        public string pagecode { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public List<covidresult> lstcovidresult { get; set; }
    }

    public class covidresult
    {
        public int PatientVisitNo { get; set; }
        public int OrderListNo { get; set; }
        public string VisitID { get; set; }
        public string FullName { get; set; }
        public string visitDTTM { get; set; }
        public string ReferredBy { get; set; }
        public string ICMRID { get; set; }
        public string SRFID { get; set; }
    }
    public class BulkDocumentUpload
    {
        public string ActualFileName { get; set; }
        public string ManualFileName { get; set; }
        public string FileBinaryData { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string ExternalVisitID { get; set; }
        public int PatientVisitNo { get; set; }
        public int ServiceNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string ActualBinaryData { get; set; }
        public string ConfigValuePath { get; set; }
        public string InsertPath { get; set; }
        public int UserNo { get; set; }
    }
    public partial class ApprovalDoctorRequest
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int deptNo { get; set; }

    }
    public partial class ApprovalDoctorResponse
    {
        public int ApprovalUserNo { get; set; }
        public string ApprovalUserName { get; set; }
        public int DepartmentNo { get; set; }
        public int RowNo { get; set; }
    }
    public partial class PatientDataImpressionResponse
    {
        public int PatientVisitNo { get; set; }
        public int OrderListNo { get; set; }
        public Int64 Row_Num { get; set; }
    }
    public class PatientImpressionResponse
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_num { get; set; }
        public Int64 Sno { get; set; }
        public int PatientNo { get; set; }
        public string PrimaryId { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public int VisitNo { get; set; }
        public string VisitId { get; set; }
        public string RegistrationDate { get; set; }
        public string RefferedBy { get; set; }
        public int RefferedByNo { get; set; }
        public string ServiceName { get; set; }
        public int OrderListNo { get; set; }
        public string fromDate { get; set; }
        public string todate { get; set; }
        public string malignantCase { get; set; }
        public string doctorName { get; set; }
        public string raceName { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public int refferalType { get; set; }

    }
    //merging concept
    public class mergeresultrequest
    {
        public int patientVisitNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int patientno { get; set; }
        public int testno { get; set; }
        public int subtestno { get; set; }
        public string nricNo { get; set; }
    }
    public class mergeresultresponse
    {
        public int rowNo { get; set; }
        public int orderDetailsNo { get; set; }
        public int patientVisitNo { get; set; }
        public string visitID { get; set; }
        public string patientID { get; set; }
        public string uRNID { get; set; }
        public string uRNType { get; set; }
        public int orderListNo { get; set; }
        public char testType { get; set; }
        public int testNo { get; set; }
        public string testName { get; set; }
        public int subTestNo { get; set; }
        public string subTestName { get; set; }
        public string currResult { get; set; }
        public string prevResult { get; set; }
        public int tSeqNo { get; set; }
        public int sSeqNo { get; set; }
        public int methodNo { get; set; }
        public string methodName { get; set; }
        public int unitNo { get; set; }
        public string unitName { get; set; }
        public string lLColumn { get; set; }
        public string hLColumn { get; set; }
        public string displayRR { get; set; }
        public string maxRange { get; set; }
        public string minRange { get; set; }
        public string cRLLColumn { get; set; }
        public string cRHLColumn { get; set; }
        public string resultType { get; set; }
        public string resultflag { get; set; }
        public int headerNo { get; set; }
    }
    public class savemergeresultrequest
    {
        public string pagecode { get; set; }
        public string action { get; set; }
        public List<mergeresultresponse> lstmergedresult { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
    }
    public class savemergeresultresponse
    {
        public int status { get; set; }
    }
    //
    public class culturehistoryrequest
    {
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public string nricNo { get; set; }
    }
    public class culturehistoryreponse
    {
        public int rowNo { get; set; }
        public string visitID { get; set; }
        public string visitDTTM { get; set; }
        public int patientVisitNo { get; set; }
        public int orderListNo { get; set; }
        public string testName { get; set; }
        public int testNo { get; set; }
        public string testType { get; set; }
        public int resultTypeNo { get; set; }
    }
    public partial class analyserrequestresult
    {
        public string pagecode { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int patientvisitno { get; set; }
        public int deptno { get; set; }
        public int serviceno { get; set; }
        public string servicetype { get; set; }
        public int type { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string gentype { get; set; }
        public bool ismachinevalue { get; set; }
        public Int16 machineId { get; set; }
        public Int16 maindeptNo { get; set; }
        public Int16 testfilterflag { get; set; }
        public int patientno { get; set; }
    }
    public partial class objbulkresult
    {
        public int rowno { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public string pagecode { get; set; }
        public string action { get; set; }
        public int serviceno { get; set; }
        public string servicetype { get; set; }
        public string servicename { get; set; }
        public string oTTestCode { get; set; }
        public List<objbulkresultdetails> lstbulkresultdetails { get; set; }
        public bool IsPartialEntryMaster { get; set; }
        public bool IsPartialValidationMaster { get; set; }
    }
    public class objbulkresultdetails
    {
        public int sno { get; set; }
        public string labaccessionno { get; set; }
        public string patientname { get; set; }
        public string patientage { get; set; }
        public string gender { get; set; }
        public string result { get; set; }
        public string resulttype { get; set; }
        public string referrencerange { get; set; }
        public string unit { get; set; }
        public bool isrecheck { get; set; }
        public bool isrerun { get; set; }
        public bool isrecollect { get; set; }
        public string patientid { get; set; }
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public int orderlistno { get; set; }
        public int orderdetailsno { get; set; }
        public int testno { get; set; }
        public string testtype { get; set; }
        public string testname { get; set; }
        public string testCode { get; set; }
        public int subtestno { get; set; }
        public string subtestname { get; set; }
        public bool isdelta { get; set; }
        public int unitno { get; set; }
        public bool istformula { get; set; }
        public bool issformula { get; set; }
        public bool isformulaparameter { get; set; }
        public int formulaserviceno { get; set; }
        public string formulaservicetype { get; set; }
        public List<formulajson> formulajson { get; set; }
        public List<formulaparameterjson> formulaparameterjson { get; set; }
        public List<picklistjson> picklistjson { get; set; }
        public string formularesult { get; }
        public string masterllcolumn { get; set; }
        public string masterhlcolumn { get; set; }
        public string masterdisplayrr { get; set; }
        public string crllcolumn { get; set; }
        public string crhlcolumn { get; set; }
        public string displaycrrr { get; set; }
        public string minrange { get; set; }
        public string maxrange { get; set; }
        public string displaymmrr { get; set; }
        public bool isnonmandatory { get; set; }
        public int decimalpoint { get; set; }
        public bool isroundoff { get; set; }
        public int methodno { get; set; }
        public string methodname { get; set; }
        public string llcolumn { get; set; }
        public string hlcolumn { get; set; }
        public string displayrr { get; set; }
        public string diresult { get; set; }
        public string resultflag { get; set; }
        public string resultcomments { get; set; }
        public string internotes { get; set; }
        public bool risrerunod { get; set; }
        public bool isrerunod { get; set; }
        public decimal lesserValue { get; set; }
        public decimal greaterValue { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int departmentseqno { get; set; }
        public string samplename { get; set; }
        public bool ischecked { get; set; }
        public int resulttypeno { get; set; }
        public string visitid { get; set; }
        public int tseqno { get; set; }
        public int sseqno { get; set; }
        public int rowno { get; set; }
        public bool isnoresult { get; set; }
        public int groupinter { get; set; }
        public bool isattachment { get; set; }
        public int headerno { get; set; }
        public bool isedit { get; set; }
        public int testinter { get; set; }
        public string uploadedFile { get; set; }
        public Int16 approvalDoctor { get; set; }
        public bool isSecondReview { get; set; }
        public bool isLock { get; set; }
        public List<DIResult> prevDIResults { get; set; }
        public bool isOBNegative { get; set; }
        public bool isOBTest { get; set; }
        public string ObTestCode { get; set; }
        public string ObPositiveCode { get; set; }
        public bool IsPartialEntryTrans { get; set; }
        public bool IsPartialValidationTrans { get; set; }
        public string prevABORHResult { get; set; }
    }
    public partial class lstbulkresultdbl
    {
        public int rowno { get; set; }
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agetype { get; set; }
        public string gender { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public string referraltype { get; set; }
        public string customername { get; set; }
        public string physicianname { get; set; }
        public bool visstat { get; set; }
        public bool visremarks { get; set; }

        public string address { get; set; }
        public string dob { get; set; }
        public string urntype { get; set; }
        public string urnid { get; set; }
        public string samplecollectedon { get; set; }
        public string enteredon { get; set; }
        public string validatedon { get; set; }
        public string approvedon { get; set; }
        //--
        public int orderlistno { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int departmentseqno { get; set; }
        public string samplename { get; set; }
        public string barcodeno { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicename { get; set; }
        public int serviceseqno { get; set; }
        public int resulttypeno { get; set; }
        public bool isrerun { get; set; }
        public bool isrecollect { get; set; }
        public bool isrecheck { get; set; }
        public bool isrecall { get; set; }
        public bool isoutsource { get; set; }
        public bool isoutsourceattachment { get; set; }
        public bool isattachment { get; set; }
        public bool isremarks { get; set; }
        public bool istat { get; set; }
        public bool iscontestinter { get; set; }
        public int groupinter { get; set; }
        public bool ischecked { get; set; }
        //-- 
        public int orderdetailsno { get; set; }
        public string testtype { get; set; }
        public int testno { get; set; }
        public string testname { get; set; }
        public string testCode { get; set; }
        public int tseqno { get; set; }
        public int subtestno { get; set; }
        public string subtestname { get; set; }
        public int sseqno { get; set; }
        public string resulttype { get; set; }
        public int mastermethodno { get; set; }
        public int masterunitno { get; set; }
        public string masterllcolumn { get; set; }
        public string masterhlcolumn { get; set; }
        public string masterdisplayrr { get; set; }
        public string crllcolumn { get; set; }
        public string crhlcolumn { get; set; }
        public string minrange { get; set; }
        public string maxrange { get; set; }
        public bool isnonmandatory { get; set; }
        public bool isdelta { get; set; }
        public bool istformula { get; set; }
        public bool issformula { get; set; }
        public int decimalpoint { get; set; }
        public bool isroundoff { get; set; }
        public bool isformulaparameter { get; set; }
        public int formulaserviceno { get; set; }
        public string formulaservicetype { get; set; }
        public string formulajson { get; set; }
        public string formulaparameterjson { get; set; }
        public string picklistjson { get; set; }
        public int headerno { get; set; }
        public bool isedit { get; set; }
        public int testinter { get; set; }
        //
        public int methodno { get; set; }
        public string methodname { get; set; }
        public int unitno { get; set; }
        public string unitname { get; set; }
        public string llcolumn { get; set; }
        public string hlcolumn { get; set; }
        public string displayrr { get; set; }
        public string result { get; set; }
        public string formularesult { get; set; }
        public string diresult { get; set; }
        public string resultflag { get; set; }
        public string resultcomments { get; set; }
        public string internotes { get; set; }
        public bool isrerunod { get; set; }
        public int notifyCount { get; set; }
        public bool isUploadOption { get; set; }
        public string uploadedFile { get; set; }
        public int isMultiEditor { get; set; }
        public Int16 approvalDoctor { get; set; }
        public string venueBranchName { get; set; }
        public string statusName { get; set; }
        public bool isnoresult { get; set; }
        public string nricNumber { get; set; }
        public bool isSecondReview { get; set; }
        public bool isSecondReviewAvail { get; set; }
        public decimal lesserValue { get; set; }
        public decimal greaterValue { get; set; }
        public string prevresult { get; set; }
        public string prevresultrefrange { get; set; }
        public string prevresultdttm { get; set; }
        public bool isAbnormalAvail { get; set; }
        public Int16 isDeptAvail { get; set; }
        public bool isSensitiveData { get; set; }
        public int isNotify { get; set; }
        public string calcprevresult { get; set; }
        public decimal calcprevresultper { get; set; }
        public decimal calcprevresultdif { get; set; }
        public int groupno { get; set; }
        public int packageno { get; set; }
        public int indvserviceno { get; set; }
        public int actualserviceno { get; set; }
        public string actualservicetype { get; set; }
        public string actualservicename { get; set; }
        public bool isLock { get; set; }
        public string oTTestCode { get; set; }
        public string prevDIResults { get; set; }
        public bool isOBNegative { get; set; }
        public bool isOBTest { get; set; }
        public string ObTestCode { get; set; }
        public string ObPositiveCode { get; set; }
        public bool IsPartialEntryMaster { get; set; }
        public bool IsPartialValidationMaster { get; set; }
        public bool IsPartialEntryTrans { get; set; }
        public bool IsPartialValidationTrans { get; set; }
        public string prevABORHResult { get; set; }
    }
    public class BulkResultSaveResponse
    {
        public int outstatus { get; set; }
    }
    public class GetBulkCultureResultRequest
    {
        public string pagecode { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int patientvisitno { get; set; }
        public int deptno { get; set; }
        public int serviceno { get; set; }
        public string servicetype { get; set; }
        public int type { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string gentype { get; set; }
        public bool ismachinevalue { get; set; }
        public Int16 machineId { get; set; }
        public Int16 maindeptNo { get; set; }
        public int patientno { get; set; }
    }
    public class SaveBulkCUltureResultRequest
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public string pagecode { get; set; }
        public string action { get; set; }
        public List<BulkCultureResultResponse> lstCulture { get; set; }
    }
    public class BulkCultureResultResponse
    {
        public int rowno { get; set; }
        public int patientno { get; set; }
        public string visitid { get; set; }
        public string visitdttm { get; set; }
        public string patientname { get; set; }
        public string agetype { get; set; }
        public string gender { get; set; }
        public string collectedon { get; set; }
        public string enteredon { get; set; }
        public int patientvisitno { get; set; }
        public int orderno { get; set; }
        public int orderlistno { get; set; }
        public int orderliststatus { get; set; }
        public string orderliststatustext { get; set; }
        public string testname { get; set; }
        public int testno { get; set; }
        public string testtype { get; set; }
        public int sampleno { get; set; }
        public string samplename { get; set; }
        public int reportstatus { get; set; }
        public string reportstatustext { get; set; }
        public int resultstatus { get; set; }
        public string resultstatustext { get; set; }
        public int resultpattern { get; set; }
        public string resultpatterntext { get; set; }
        public int colonycount { get; set; }
        public string colonycounttext { get; set; }
        public bool isabnormalavail { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public int resulttypeno { get; set; }
        public bool isnabl { get; set; }
        public bool ischecked { get; set; }
        public int approvalDoctor { get; set; }
        public int prevreportstatus { get; set; }
        public string allergyInfo { get;set;}
    }
    public class BulkCultureResultSaveResponse
    {
        public int outstatus { get; set; }
    }
    public class VisitMergeRequest
    {
        public string pagecode { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int patientvisitno { get; set; }
        public Int16 fromtovisitflag { get; set; }
        public string flag { get; set; }
        public int fromvisitno { get; set; }
    }
    public class SaveResultforVisitMergeResponse
    {
        public int venueno { get; set; }
        public int venuebno { get; set; }
        public int userno { get; set; }
        public string pagecode { get; set; }
        public int visitno { get; set; }
        public string action { get; set; }
        public List<GetResultforVisitMergeResponse> lstResultforVisitMerge { get; set; }
    }
    public class GetResultforVisitMergeResponse
    {
        public int id { get; set; }
        public string fpatientname { get; set; }
        public string fage { get; set; }
        public string fvisitid { get; set; }
        public string fvisitdttm { get; set; }
        public int freferraltypeno { get; set; }
        public string freferraltype { get; set; }
        public int fclientno { get; set; }
        public int fphysicianno { get; set; }
        public string fclientname { get; set; }
        public string fphysicianname { get; set; }
        public int fgrouppackno { get; set; }
        public int ftestno { get; set; }
        public int fsubtestno { get; set; }
        public string ftesttype { get; set; }
        public string fgrouppackname { get; set; }
        public string ftestname { get; set; }
        public string fsubtestname { get; set; }
        public int fpatientno { get; set; }
        public int fpatientvisitno { get; set; }
        public int forderlistno { get; set; }
        public int forderdetailsno { get; set; }
        public int forderliststatus { get; set; }
        public string forderliststatustext { get; set; }
        public string fresult { get; set; }
        public int fresulttypeno { get; set; }
        public string fresulttype { get; set; }
        public string fpicklistjson { get; set; }
        public List<picklistjson> fpicklistjsondata { get; set; }
        public string fbarcode { get; set; }
        public int fserviceseqno { get; set; }
        public int ftestseqno { get; set; }
        public int fsubtestseqno { get; set; }
        public string fnricnumber { get; set; }
        public int fdepartmentseqno { get; set; }
        public int fordertransactionno { get; set; }
        public string fresultcomments { get; set; }
        public string fresultflag { get; set; }
        public bool fisVip { get; set; }
        public string tpatientname { get; set; }
        public string tage { get; set; }
        public string tvisitid { get; set; }
        public string tvisitdttm { get; set; }
        public int treferraltypeno { get; set; }
        public string treferraltype { get; set; }
        public int tclientno { get; set; }
        public int tphysicianno { get; set; }
        public string tclientname { get; set; }
        public string tphysicianname { get; set; }
        public int tgrouppackno { get; set; }
        public int ttestno { get; set; }
        public int tsubtestno { get; set; }
        public string ttesttype { get; set; }
        public string tgrouppackname { get; set; }
        public string ttestname { get; set; }
        public string tsubtestname { get; set; }
        public int tpatientno { get; set; }
        public int tpatientvisitno { get; set; }
        public int torderlistno { get; set; }
        public int torderdetailsno { get; set; }
        public int torderliststatus { get; set; }
        public string torderliststatustext { get; set; }
        public string tresult { get; set; }
        public int tresulttypeno { get; set; }
        public string tresulttype { get; set; }
        public string tpicklistjson { get; set; }
        public List<picklistjson> tpicklistjsondata { get; set; }
        public string tbarcode { get; set; }
        public int tserviceseqno { get; set; }
        public int ttestseqno { get; set; }
        public int tsubtestseqno { get; set; }
        public string tnricnumber { get; set; }
        public int tdepartmentseqno { get; set; }
        public int tordertransactionno { get; set; }
        public string tresultcomments { get; set; }
        public string tresultflag {  get; set; }
        public bool tisVip { get; set; }
        public bool ismerged { get; set; }
        public string fromvisitvalidatemessage { get; set; }
        public bool isfromvisitvalidated { get; set; }
        public string tovisitvalidatemessage { get; set; }
        public bool istovisitvalidated { get; set; }
    }
    public class ResultforVisitMergeResponse
    {
        public int id { get; set; }
        public string fpatientname { get; set; }
        public string fage { get; set; }
        public string fvisitid { get; set; }
        public string fvisitdttm { get; set; }
        public int freferraltypeno { get; set; }
        public string freferraltype { get; set; }
        public int fclientno { get; set; }
        public int fphysicianno { get; set; }
        public string fclientname { get; set; }
        public string fphysicianname { get; set; }
        public int fgrouppackno { get; set; }
        public int ftestno { get; set; }
        public int fsubtestno { get; set; }
        public string ftesttype { get; set; }
        public string fgrouppackname { get; set; }
        public string ftestname { get; set; }
        public string fsubtestname { get; set; }
        public int fpatientno { get; set; }
        public int fpatientvisitno { get; set; }
        public int forderlistno { get; set; }
        public int forderdetailsno { get; set; }
        public int forderliststatus { get; set; }
        public string forderliststatustext { get; set; }
        public string fresult { get; set; }
        public int fresulttypeno { get; set; }
        public string fresulttype { get; set; }
        public string fpicklistjson { get; set; }
        public string fbarcode { get; set; }
        public int fserviceseqno { get; set; }
        public int ftestseqno { get; set; }
        public int fsubtestseqno { get; set; }
        public string fnricnumber { get; set; }
        public int fdepartmentseqno { get; set; }
        public int fordertransactionno { get; set; }
        public string fresultcomments { get; set; }
        public string fresultflag { get; set; }
        public bool fisVip { get; set; }
        public string tpatientname { get; set; }
        public string tage { get; set; }
        public string tvisitid { get; set; }
        public string tvisitdttm { get; set; }
        public int treferraltypeno { get; set; }
        public string treferraltype { get; set; }
        public int tclientno { get; set; }
        public int tphysicianno { get; set; }
        public string tclientname { get; set; }
        public string tphysicianname { get; set; }
        public int tgrouppackno { get; set; }
        public int ttestno { get; set; }
        public int tsubtestno { get; set; }
        public string ttesttype { get; set; }
        public string tgrouppackname { get; set; }
        public string ttestname { get; set; }
        public string tsubtestname { get; set; }
        public int tpatientno { get; set; }
        public int tpatientvisitno { get; set; }
        public int torderlistno { get; set; }
        public int torderdetailsno { get; set; }
        public int torderliststatus { get; set; }
        public string torderliststatustext { get; set; }
        public string tresult { get; set; }
        public int tresulttypeno { get; set; }
        public string tresulttype { get; set; }
        public string tpicklistjson { get; set; }
        public string tbarcode { get; set; }
        public int tserviceseqno { get; set; }
        public int ttestseqno { get; set; }
        public int tsubtestseqno { get; set; }
        public string tnricnumber { get; set; }
        public int tdepartmentseqno { get; set; }
        public int tordertransactionno { get; set; }
        public string tresultcomments { get; set; }
        public string tresultflag { get; set; }
        public bool tisVip { get; set; }
        public bool ismerged { get; set; }
        public string fromvisitvalidatemessage { get; set; }
        public bool isfromvisitvalidated { get; set; }
        public string tovisitvalidatemessage { get; set; }
        public bool istovisitvalidated { get; set; }
    }
    public class InsertVisitMergeResponse
    {
        public int OStatus { get; set; }
    }
    public partial class logicConceptResponse
    {
        public int logicNeededServiceNo { get; set; }
        public string logicNeededServiceType { get; set; }
        public string additionalExpression { get; set; }
        public int additionalParamService { get; set; }
        public string additionalParamServiceType { get; set; }
        public int requiredServiceNo { get; set; }
        public string requiredServiceNoType { get; set; }
        public string restrictResultComment { get; set; }
        public string restrictResultValue { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int secondRequiredServiceNo { get; set; }
        public string secondRequiredServiceNoType { get; set; }
    }
    public partial class InvestigationAvailResponse
    {
        public int status { get; set; }
        public int orderlistno { get; set; }
        public string mailto { get; set; }
        public string visitid { get; set; }
    }
    public class logicCommentsRequest
    {
        public int venueNo { get; set; }
        public string logicName { get; set; }
        public string serviceType { get; set; }
        public int serviceNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
    }
    public class logicCommentsRespose
    {
        public int LogicCommentsId { get; set; }
        public string LogicComments { get; set; }
    }
    //Dc - IsExtra subtest flag based calculation
    public partial class extrasubtestflagbasedformularequest
    {
        public int venueNo { get; set; }
        public string pageCode { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int patientVisitNo { get; set; }
        public int serviceNo { get; set; }
        public string serviceType { get; set; }
    }
    public partial class extrasubtestflagbasedformularesponse
    {
        public int id { get; set; }
        public string formulaJson { get; set; }
        public string formulaParameterJson { get; set; }
    }
    public class saveinfectioncontroldetrequest
    {
        public int PatientVisitNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int Type { get; set; }
        public int UserNo { get; set; }
    }
    public class saveinfectioncontroldetresponse
    {
        public int OutStatus { get; set; }
    }
    public class GetOldResultThroughDIRequest
    {
        public int PatientVisitNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int OrderListNo { get; set; }
        public int OrderDetailsNo { get; set; }
        public int UserNo { get; set; }
    }
    public class GetOldResultThroughDIResponse
    {
        public int rowno { get; set; }
        public int patientvisitno { get; set; }
        public int orderlistno { get; set; }
        public int orderdetailsno { get; set; }
        public string testtype { get; set; }
        public int testno { get; set; }
        public int subtestno { get; set; }
        public string subtestname { get; set; }
        public string testname { get; set; }
        public string result { get; set; }
        public string diresult { get; set; }
    }
    public class DIResult
    {
        public int id { get; set; }
        public string result { get; set; }
        public string date { get; set; }
    }
    public class PBFTestRequest
    {
        public int id { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int orderdetailsno { get; set; }
        public int patientvisitno { get; set; }        
    }
    public class PBFTestResponse
    {
        public int status { get; set; }
        public string resultComment { get; set; }        
    }
    public partial class objUpdPartialEntryFlagRequest
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public string pagecode { get; set; }
        public int visitNo { get; set; }
        public int orderListNo { get; set; }
        public bool entryFlag { get; set; }
        public bool validationFlag { get; set; }
    }
    public partial class objUpdPartialEntryFlagResponse
    {
        public int patientvisitno { get; set; }
    }
    public class TemplatePatientResultInsertRequest
    {
        public Int64 PtRsltTmplContId { get; set; }
        public int PatientVisitNo { get; set; }
        public int OrderListNo { get; set; }
        public int TestNo { get; set; }
        public Int16 TemplateNo { get; set; }
        public string? PageAction { get; set; }
        public string? PageCode { get; set; }
        public string? Result { get; set; }
        public Int16 VenueNo { get; set; }
        public Int16 BranchNo { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int16 CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Int16 ModifiedBy { get; set; }
    }
    [Keyless]
    public class TemplatePatientResultResponse
    {
        public Int64 PtRsltTmplContId { get; set; }
        public int PatientVisitNo { get; set; }
        public int OrderListNo { get; set; }
        public int TestNo { get; set; }
        public Int16 TemplateNo { get; set; }
        public string? PageCode { get; set; }
        public string? Result { get; set; }
    }
    public partial class PendingVisitDetailsReq
    {
        public int venueno { get; set; }
        public string pagecode { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int viewvenuebranchno { get; set; }
        public int pageindex { get; set; }
        public string? type { get; set; }
        public string? fromdate { get; set; }
        public string? todate { get; set; }
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public int deptno { get; set; }
        public int serviceno { get; set; }
        public int refferraltypeno { get; set; }
        public int customerno { get; set; }
        public int physicianno { get; set; }
        public int orderstatus { get; set; }
        public Int16 maindeptNo { get; set; }
        public string? servicetype { get; set; }
        public int pageCount { get; set; }

    }
    public partial class PendingVisitDetailsRes
    {
        public int TotalVisitCount { get; set; }
        public int TotalRecords {  get; set; }
        public int PageIndex { get; set; }
        public int RowNo { get; set; }
        public string VenueBranchName { get; set; }
        public string VisitDttm { get; set; }
        public string FullName { get; set; }
        public string RefName { get; set; }
        public string DepartmentName { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public string BarcodeNo { get; set; }
        public string ServiceStatus { get; set; }
        public string VisitId { get; set; }
        public int PatientNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string PatientId { get; set; }
        public int DepartmentNo { get; set; }
        public Int16 MainDeptNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int RefferralTypeNo { get; set; }
        public string Colorcode { get; set; }
     
    }
    public partial class HeaderFooterRestrictionReq
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int patientvisitno { get; set; }
        public string notifytype { get; set; }
    }
    public partial class HeaderFooterRestrictionRes
    {
        public int id { get; set; }
        public int isHeaderAvail { get; set; }
        public int isFooterAvail { get; set; }
    }
}
