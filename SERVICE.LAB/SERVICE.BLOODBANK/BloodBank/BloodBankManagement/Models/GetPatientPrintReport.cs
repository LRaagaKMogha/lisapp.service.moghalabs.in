using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using ErrorOr;

namespace BloodBankManagement.Models
{
    public class GetPatientPrintReport
    {
public Int64 RowNo{get;set;}
public int Sno{get;set;}
        public Int64 RegistrationId { get;set;}
public string? PatientRecord{get;set;}
public string? Labno{get;set;}
public string? MRONo{get;set;}
public string? Name{get;set;}
public string? Sex{get;set;}
public string? DOB{get;set;}
public string? Sample1{get;set;}
public string? Sample1Date{get;set;}
public string? Sample2{get;set;}
public string? Sample2Date{get;set;}
public string? AntibodyScreen{get;set;}
public string? Remarks{get;set;}
public string? LastABSC{get;set;}
public string? LastAbscDesc{get;set;}
public string? LastPOS{get;set;}
public string? LastPOSDesc{get;set;}
public string? PatientComment{get;set;}
public string? KnowAntibodies{get;set;}
public string? SpecialReq{get;set;}
public string? ValiditySplReq{get;set;}
public string? AdditionalReq{get;set;}
public string? TransfusionReaction{get;set;}
public string? DonationId{get;set;}
public string? ProductCode{get;set;}
public string? PhenoType{get;set;}
public string? DonationBarcodeId{get;set;}
public string? Barcode2{get;set;}
public string? BloodGroup{get;set;}
        public string? Quantity { get; set; }
        public string? ExpiryDate { get; set; }
        public string? CompatibleResult { get; set; }
        public string? Volume { get; set; }
        public string? Sample1BldGrp { get; set; }
        public string? Sample1BldGrpType { get; set; }
        public string? Sample2BldGrp { get; set; }
        public string? Sample2BldGrpType { get; set; }
        public string? RejectedComments { get; set; }
        public GetPatientPrintReport()
        {

        }

        public GetPatientPrintReport(Int64 rowNo, int sno,Int64 registrationId, string? patientRecord, string? labno, string? mRONo, string? name, string? sex, string? dOB, string? sample1, string? sample1Date, string? sample2, string? sample2Date,
string? antibodyScreen, string? remarks, string? lastABSC, string? lastAbscDesc, string? lastPOS, string? lastPOSDesc, string? patientComment, string? knowAntibodies, string? specialReq, string? validitySplReq, string? additionalReq,
string? transfusionReaction, string? donationId, string? productCode, string? phenoType, string? donationBarcodeId, string? barcode2, string? bloodGroup, string? quantity,
string? expiryDate, string? compatibleResult, string? volume, string? sample1BldGrp, string? sample1BldGrpType, string? sample2BldGrp, string? sample2BldGrpType, string? rejectedComments)
        {
            RowNo = rowNo;
            Sno = sno;
            RegistrationId = registrationId;
            PatientRecord = patientRecord;
            Labno = labno;
            MRONo = mRONo;
            Name = name;
            Sex = sex;
            DOB = dOB;
            Sample1 = sample1;
            Sample1Date = sample1Date;
            Sample2 = sample2;
            Sample2Date = sample2Date;
            AntibodyScreen = antibodyScreen;
            Remarks = remarks;
            LastABSC = lastABSC;
            LastAbscDesc = lastAbscDesc;
            LastPOS = lastPOS;
            LastPOSDesc = lastPOSDesc;
            PatientComment = patientComment;
            KnowAntibodies = knowAntibodies;
            SpecialReq = specialReq;
            ValiditySplReq = validitySplReq;
            AdditionalReq = additionalReq;
            TransfusionReaction = transfusionReaction;
            DonationId = donationId;
            ProductCode = productCode;
            PhenoType = phenoType;
            DonationBarcodeId = donationBarcodeId;
            Barcode2 = barcode2;
            BloodGroup = bloodGroup;
            Quantity = quantity;
            ExpiryDate = expiryDate;
            CompatibleResult = compatibleResult;
            Volume = volume;
            Sample1BldGrp = sample1BldGrp;
            Sample1BldGrpType = sample1BldGrpType;
            Sample2BldGrp = sample2BldGrp;
            Sample2BldGrpType = sample2BldGrpType;
            RejectedComments = rejectedComments;
        }
        public static ErrorOr<GetPatientPrintReport> Create(Int64 rowNo, int sno,Int64 registrationId, string? patientRecord, string? labno, string? mRONo, string? name, string? sex, string? dOB, 
            string? sample1, string? sample1Date, string? sample2, string? sample2Date,string? antibodyScreen, string? remarks, string? lastABSC, string? lastAbscDesc, 
            string? lastPOS, string? lastPOSDesc, string? patientComment, string? knowAntibodies, string? specialReq, string? validitySplReq, string? additionalReq,
string? transfusionReaction, string? donationId, string? productCode, string? phenoType, string? donationBarcodeId, string? barcode2, string? bloodGroup, string? quantity, 
string? expiryDate, string? compatibleResult, string? volume, string? sample1BldGrp, string? sample1BldGrpType, string? sample2BldGrp, string? sample2BldGrpType,string? rejectedComments
            )
        {
            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }
            return new GetPatientPrintReport(rowNo,sno,registrationId,patientRecord,labno,mRONo,name,sex,dOB,sample1,sample1Date,sample2,sample2Date,antibodyScreen,remarks,lastABSC,lastAbscDesc,
                lastPOS,lastPOSDesc,patientComment,knowAntibodies,specialReq,validitySplReq,additionalReq,transfusionReaction,donationId,productCode,phenoType,donationBarcodeId, barcode2, bloodGroup,
                quantity, expiryDate, compatibleResult, volume, sample1BldGrp,sample1BldGrpType,sample2BldGrp,sample2BldGrpType, rejectedComments
                );
        }
        public static ErrorOr<GetPatientPrintReport> From(GetPatientReport request)
        {
            return Create(request.RowNo,request.Sno,request.RegistrationId, request.PatientRecord,request.Labno,request.MRONo,request.Name,request.Sex,request.DOB,request.Sample1,
                request.Sample1Date,request.Sample2,request.Sample2Date,request.AntibodyScreen,request.Remarks,request.LastABSC,request.LastAbscDesc,request.LastPOS,request.LastPOSDesc,
                request.PatientComment,request.KnowAntibodies,request.SpecialReq,request.ValiditySplReq,request.AdditionalReq,request.TransfusionReaction,request.DonationId,request.ProductCode,
                request.PhenoType,request.DonationBarcodeId,request.Barcode2,request.BloodGroup,request.Quantity,request.ExpiryDate,request.CompatibleResult,request.Volume,request.Sample1BldGrp,
                request.Sample1BldGrpType,request.Sample2BldGrp,request.Sample2BldGrpType,request.RejectedComments);
        }
    }
}