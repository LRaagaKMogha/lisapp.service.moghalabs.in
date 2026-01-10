using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using ErrorOr;

namespace BloodBankManagement.Models
{
    public class StandardPatinetReport
    {
        public int RowNo { get; set; }
        public Int64 RegistrationId { get; set; }
        public string? LabAccessionNo { get; set; }
        public string? PatientName { get; set; }
        public string? VisitId { get; set; }
        public string? NRICNumber { get; set; }
        public string? Gender { get; set; }
        public string? DOB { get; set; }
        public string? RegDTTM { get; set; }
        public string? ProductName { get; set; }
        public Int64? TestId { get; set; }
        public string? TestName { get; set; }
        public string? Result { get; set; }
        public string? DonorID { get; set; }
        public string? Status { get; set; }
        public bool? IsActive { get; set; }
        public Int64? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Int32? TotalRecords { get; set; }
        public Int32? PageIndex { get; set; }
        public string? InventoryDonationId { get; set; }
        public string? CheckedBy { get; set; }
        public string? ExpirationDateAndTime { get; set; }
        public string? Volume { get; set; }
        public string? InventoryAboOnLabel { get; set; }
        public Int64? NationalityId { get; set; }
        public Int64? RaceId { get; set; }
        public Int64? ResidenceStatusId { get; set; }
        public string? Nationality { get; set; }
        public string? Race { get; set; }
        public string? Residence { get; set; }
        public Int64? GenderId { get; set; }
        public string UnitAttribute { get; set; }
        public string CompatibilityResults { get; set; }
        public string Remarks { get; set; }
        public string CompatibilityValidTill { get; set; }
        public string IssuedDateAndTime { get; set; }
        public string PatientBloodGroup { get; set; }
        public string PatientAntibodyScreen { get; set; }
        public string PatientSpecialInstructions { get; set; }
        public string ProductCode { get; set; }
        public string Comments { get; set; }
        public string PatientDOB { get; set; }
        public string PatientSex { get; set; }
        public string BarCode { get; set; }
        public string TestShortName { get; set; }
        public bool IsBarcodeAvail { get; set; }
        public string SampleName { get; set; }
        public string Tube { get; set; }
        public int UnitCount { get; set; }
        public int IsRedCell { get; set; }
        public int BSamInvenId { get; set; }
        public int BSamProductId { get; set; }
        public int IsUploadAvail { get; set; }
        public int IsTestUploadAvail { get; set; }
        public string CaseOrVisitNumber { get; set; }
        public string referraltype { get; set; }
        public GetPatientPrintReport objReportPrint { get; set; }

        public StandardPatinetReport()
        {

        }

        public StandardPatinetReport(int rowNo, Int64 registrationId, string? labAccessionNo, string? patientName, string? visitId, string? nRICNumber, string? gender, string? dOB, string? regDTTM, string? productName, Int64? testId, string? testName,string? result, string? donorID, string? status, bool? isActive, Int64? modifiedBy, DateTime? modifiedDate, Int32? totalRecords, Int32? pageIndex, string? inventoryDonationId, string? checkedBy, string? expirationDateAndTime, string? volume, string? inventoryAboOnLabel, Int64? nationalityId, Int64? raceId, Int64? residenceStatusId, string? nationality, string? race, string? residence, Int64? genderId,
            string unitAttribute, string compatibilityResults, string remarks, string compatibilityValidTill, string issuedDateAndTime, string patientBloodGroup, string patientAntibodyScreen, string patientSpecialInstructions,string? productCode,string? comments, string? patientDOB, string? patientSex, string? barCode, string? testShortName, bool isBarcodeAvail, string? sampleName, string? tube,int unitCount,int isRedCell,int bSamInvenId,int bSamProductId,int isUploadAvail,int isTestUploadAvail)
        {
            RowNo = rowNo;
            RegistrationId = registrationId;
            LabAccessionNo = labAccessionNo;
           PatientName = patientName;
            VisitId = visitId;
            NRICNumber = nRICNumber;
            Gender = gender;
            DOB = dOB;
            RegDTTM = regDTTM;
            ProductName = productName;
            TestId = testId;
            TestName= testName;
            Result = result;
            DonorID = donorID;
            Status = status;
            IsActive = isActive;
            ModifiedBy = modifiedBy;
            ModifiedDate = modifiedDate;
            TotalRecords = totalRecords;
            PageIndex = pageIndex;
            InventoryDonationId = inventoryDonationId;
            CheckedBy = checkedBy;
            ExpirationDateAndTime = expirationDateAndTime;
            Volume = volume;
            InventoryAboOnLabel = inventoryAboOnLabel;
            NationalityId= nationalityId;
            RaceId = raceId;
            ResidenceStatusId = residenceStatusId;
            Nationality = nationality;
            Residence = residence;
            GenderId= genderId;
            UnitAttribute = unitAttribute;
            CompatibilityResults = compatibilityResults;
            Remarks = remarks;
            CompatibilityValidTill = compatibilityValidTill;
            IssuedDateAndTime= issuedDateAndTime;
            PatientBloodGroup=patientBloodGroup;
            PatientAntibodyScreen = patientAntibodyScreen;
            PatientSpecialInstructions = patientSpecialInstructions;
            ProductCode = productCode;
            Comments = comments;
            PatientDOB = patientDOB;
            PatientSex = patientSex;
            BarCode = barCode;
            TestShortName = testShortName;
            IsBarcodeAvail= isBarcodeAvail;
            SampleName = sampleName;
            Tube = tube;
            UnitCount = unitCount;
            IsRedCell = isRedCell;
            BSamInvenId = bSamInvenId;
            BSamProductId = bSamProductId;
            IsUploadAvail = isUploadAvail;
            IsTestUploadAvail = isTestUploadAvail;
        }
        public static ErrorOr<StandardPatinetReport> Create(
            int rowNo, Int64 registrationId, string? labAccessionNo, 
            string? patientName, string? visitId, string? nRICNumber, 
            string? gender, string? dOB, string? regDTTM, string? productName, 
            Int64? testId, string? testName, string? result, string? donorID, 
            string? status, bool? isActive, Int64? modifiedBy, DateTime? modifiedDate, 
            Int32? totalRecords, Int32? pageIndex, string? inventoryDonationId, 
            string? checkedBy, string? expirationDateAndTime, string? volume, 
            string? inventoryAboOnLabel, Int64? nationalityId, Int64? raceId, 
            Int64? residenceStatusId, string? nationality, string? race, 
            string? residence, Int64? genderId, string unitAttribute, 
            string compatibilityResults, string remarks, 
            string compatibilityValidTill, string issuedDateAndTime,
            string patientBloodGroup, string patientAntibodyScreen, string patientSpecialInstructions,string productCode,string comments,string patientDOB,string patientSex,string barCode,string testShortName,bool isBarcodeAvail,string sampleName,string tube, int unitCount, int isRedCell, int bSamInvenId, int bSamProductId,int isUploadAvail,int isTestUploadAvail
            )
        {
            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }
            return new StandardPatinetReport(rowNo,registrationId,labAccessionNo,patientName,visitId,nRICNumber,gender,dOB,regDTTM,productName,testId,testName,result,donorID,status,isActive,modifiedBy,
                modifiedDate,totalRecords,pageIndex,inventoryDonationId,checkedBy,expirationDateAndTime,volume,inventoryAboOnLabel,nationalityId,raceId,residenceStatusId,nationality,race,residence,genderId
                ,unitAttribute,compatibilityResults,remarks,compatibilityValidTill,issuedDateAndTime,patientBloodGroup,patientAntibodyScreen,patientSpecialInstructions, productCode,comments,patientDOB, patientSex,barCode,testShortName,isBarcodeAvail,sampleName,tube, unitCount, isRedCell, bSamInvenId, bSamProductId, isUploadAvail, isTestUploadAvail
                );
        }
        public static ErrorOr<StandardPatinetReport> From(UpsertStandardPatinetReport request)
        {
            return Create(request.RowNo,request.RegistrationId,request.LabAccessionNo,request.PatientName,request.VisitId,request.NRICNumber,request.Gender,request.DOB,request.RegDTTM,
                request.ProductName,request.TestId,request.TestName,request.Result,request.DonorID,request.Status,request.IsActive,request.ModifiedBy,request.ModifiedDate,request.TotalRecords,request.PageIndex,
                request.InventoryDonationId,request.CheckedBy,request.ExpirationDateAndTime,request.Volume,request.InventoryAboOnLabel,request.NationalityId,request.RaceId,request.ResidenceStatusId,
                request.Nationality,request.Race,request.Residence,request.GenderId,request.UnitAttribute,request.CompatibilityResults,request.Remarks,request.CompatibilityValidTill,request.IssuedDateAndTime,
                request.PatientBloodGroup,request.PatientAntibodyScreen,request.PatientSpecialInstructions,request.ProductCode,request.Comments,request.PatientDOB,request.PatientSex,request.BarCode,request.TestShortName,request.IsBarcodeAvail,request.SampleName,request.Tube,request.UnitCount,request.IsRedCell,request.BSamInvenId,request.BSamProductId,request.IsUploadAvail,request.IsTestUploadAvail);
        }
    }
}