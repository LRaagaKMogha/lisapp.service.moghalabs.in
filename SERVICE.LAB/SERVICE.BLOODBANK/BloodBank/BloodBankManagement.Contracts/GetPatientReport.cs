using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record GetPatientReport
    (
        Int64 RowNo,
        int Sno,
        Int64 RegistrationId,
        string? PatientRecord,
string? Labno,
string? MRONo,
string? Name,
string? Sex,
string? DOB,
string? Sample1,
string? Sample1Date,
string? Sample2,
string? Sample2Date,
string? AntibodyScreen,
string? Remarks,
string? LastABSC,
string? LastAbscDesc,
string? LastPOS,
string? LastPOSDesc,
string? PatientComment,
string? KnowAntibodies,
string? SpecialReq,
string? ValiditySplReq,
string? AdditionalReq,
string? TransfusionReaction,
string? DonationId,
string? ProductCode,
string? PhenoType,
string? DonationBarcodeId,
string? Barcode2,
string? BloodGroup, 
string? Quantity,
string? ExpiryDate, 
string? CompatibleResult, 
string? Volume, 
string? Sample1BldGrp, 
string? Sample1BldGrpType, 
string? Sample2BldGrp, 
string? Sample2BldGrpType,
string? RejectedComments

    );
   
}