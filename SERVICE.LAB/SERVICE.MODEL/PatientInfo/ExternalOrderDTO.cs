using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class ExternalOrderDTO
    {
        public int Row_num { get; set; }
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string Barcodeno { get; set; }
        public string VisitID { get; set; }
        public string Sex { get; set; }
        public string AgeType { get; set; }
        public string DOB { get; set; }
        public string TestId { get; set; }
        public string TestName { get; set; }
        public bool IsReRun { get; set; }
    }
 
    public class ExternalResultDTO
    {
        public string MachineId { get; set; }
        public string BarcodeNo { get; set; }
        public string TestSubtesttNo { get; set; }
        public string Result { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string Base64 { get; set; }
        public string Comment { get; set; }
        public string Ivalue { get; set; }
        public string Hvalue { get; set; }
        public string Lvalue { get; set; }
        public bool IsReRun { get; set; }
        public int IsCalculationInput { get; set; } = 0;
    }
    public class ExternalResultResponseDTO
    {
        public int Status { get; set; }     
    }

    public class CreateTemplateResultDTO
    {
        public int orderlistno { get; set; }
        public int templateno { get; set; }
        public int serviceno { get; set; }
    }
    public class ExternalBulkResultDTO
    {
        public string MId { get; set; }
        public int VNo { get; set; }
        public int vbNo { get; set; }
        public List<BulkTestResultDTO> lsttest { get; set; }
    }
    public class BulkTestResultDTO
    {
        public string bno { get; set; }
        public string tsn { get; set; }
        public string rt { get; set; }
        public string base64 { get; set; }
        public string coment { get; set; }
        public string ival { get; set; }
        public string hval { get; set; }
        public string lval { get; set; }
        public bool isrerun { get; set; }
    }
    public class ExternalbulkResultResponseDTO
    {
        public string bno { get; set; }
        public string tsn { get; set; }
        public int status { get; set; }
    }
    //culture result
    public class ExternalBulkCultureResultDTO
    {
        public string Mid { get; set; }
        public string Bno { get; set; }
        public int Orderlsitno { get; set; }
        public int Vno { get; set; }
        public int Vbno { get; set; }
        public int Servno { get; set; }
        public string Samp { get; set; }
        public List<BulkCultureOrgResultDTO> lstCultureorg { get; set; }
    }
    public class BulkCultureOrgResultDTO
    {
        public int Ono { get; set; }
        public string Ocode { get; set; }
        public string Oname { get; set; }
        public string Base64 { get; set; }
        public List<BulkCultureDrugResultDTO> lstCulturedrug { get; set; }
    }
    public class BulkCultureDrugResultDTO
    {
        public int Dno { get; set; }
        public string Dcode { get; set; }
        public string Dname { get; set; }
        public string Dmic { get; set; }
        public string Dintrp { get; set; }
        public string DintrpVal { get; set; }
    }
    public class ExternalbulkCultureResponseDTO
    {
        public string bno { get; set; }
        public string tsn { get; set; }
        public int status { get; set; }
    }
    public class ExternalCultureResultResponseDTO
    {
        public int Status { get; set; }
    }
    public class ExternalApprovalResponseDTO
    {
        public int Status { get; set; }
    }
    public class ExternalResultCalculation
    {
        public int rowno { get; set; }
        public string BarcodeNo { get; set; }
        public int orderlistno { get; set; }
        public int orderdetailsno { get; set; }
        public string TestSubtesttNo { get; set; }
        public string testtype { get; set; }
        public int testno { get; set; }
        public int tseqno { get; set; }
        public int subtestno { get; set; }
        public int sseqno { get; set; }
        public string result { get; set; }
        public bool istformula { get; set; }
        public bool issformula { get; set; }
        public int decimalpoint { get; set; }
        public bool isroundoff { get; set; }
        public bool isformulaparameter { get; set; }
        public int formulaserviceno { get; set; }
        public string formulaservicetype { get; set; }
        public List<formulajson> formulajson { get; set; }
        public List<formulaparameterjson> formulaparameterjson { get; set; }
        public int age {  get; set; }
        public string ageType { get; set; }
        public string gender {  get; set; }
        public string Comment { get; set; }
        public int IsCalculationInput { get; set; } = 0;
    }
    public class CheckFormulaIsAvailable
    {
        public bool ReturnValue { get; set; }
    }
        public class LstExternalResultCalculation
    {
        public int rowno { get; set; }
        public string BarcodeNo { get; set; }
        public int orderlistno { get; set; }
        public int orderdetailsno { get; set; }
        public string TestSubtesttNo { get; set; }
        public string testtype { get; set; }
        public int testno { get; set; }
        public int tseqno { get; set; }
        public int subtestno { get; set; }
        public int sseqno { get; set; }
        public string result { get; set; }
        public bool istformula { get; set; }
        public bool issformula { get; set; }
        public int decimalpoint { get; set; }
        public bool isroundoff { get; set; }
        public bool isformulaparameter { get; set; }
        public int formulaserviceno { get; set; }
        public string formulaservicetype { get; set; }
        public string formulajson { get; set; }
        public string formulaparameterjson { get; set; }
        public int age { get; set; }
        public string ageType { get; set; }
        public string gender { get; set; }
        public string comment { get; set; }
    }
    public class ExternalResultCalculationRequest
    {
        public string MachineId { get; set; }
        public string BarcodeNo { get; set; }
        public string TestSubtesttNo { get; set; }
        public string Result { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    //
    //culture result - 5 organism
    public class ExternalDifBulkCultureResultNewDTO
    {
        public string Mid { get; set; }
        public string Bno { get; set; }
        public int Orderlsitno { get; set; }
        public int Vno { get; set; }
        public int Vbno { get; set; }
        public int Servno { get; set; }
        public string Samp { get; set; }
        public int Ono { get; set; }
        public string Ocode { get; set; }
        public string Oname { get; set; }
        public string Base64 { get; set; }
        public int Dno { get; set; }
        public string Dcode { get; set; }
        public string Dname { get; set; }
        public string Dmic { get; set; }
        public string Dintrp { get; set; }
        public string DintrpVal { get; set; }
        public string OrgType { get; set; }
    }
    //--5 organism changes
    //culture result - 4 Organism 
    public class ExternalDifBulkCultureResultDTO
    {
        public string Mid { get; set; }
        public string Bno { get; set; }
        public int Orderlsitno { get; set; }
        public int Vno { get; set; }
        public int Vbno { get; set; }
        public int Servno { get; set; }
        public string Samp { get; set; }
        public DifBulkCultureOrgResultDTO lstCultureorg1 { get; set; }
        public DifBulkCultureOrgResultDTO lstCultureorg2 { get; set; }
        public DifBulkCultureOrgResultDTO lstCultureorg3 { get; set; }
        public DifBulkCultureOrgResultDTO lstCultureorg4 { get; set; }
        public DifBulkCultureOrgResultDTO lstCultureorg5 { get; set; }
    }
    public class DifBulkCultureOrgResultDTO
    {
        public int Ono { get; set; }
        public string Ocode { get; set; }
        public string Oname { get; set; }
        public string Base64 { get; set; }
        public List<DifBulkCultureDrugResultDTO> lstCulturedrug { get; set; }
    }
    public class DifBulkCultureDrugResultDTO
    {
        public int Dno { get; set; }
        public string Dcode { get; set; }
        public string Dname { get; set; }
        public string Dmic { get; set; }
        public string Dintrp { get; set; }
        public string DintrpVal { get; set; }
    }
    public class ExternalDifbulkCultureResponseDTO
    {
        public string bno { get; set; }
        public string tsn { get; set; }
        public int status { get; set; }
    }    
    //
}
