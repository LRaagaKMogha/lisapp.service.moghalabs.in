using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    [NotMapped]
    public record UpsertStandardPatinetReport
    (
        int RowNo,
        Int64 RegistrationId,
        string? LabAccessionNo,
        string? PatientName,
        string? VisitId,
        string? NRICNumber,
        string? Gender,
        string? DOB,
        string? RegDTTM,
        string? ProductName,
        Int64? TestId,
        string? TestName,
        string? Result,
        string? DonorID,
        string? Status,
        bool? IsActive,
        Int64? ModifiedBy,
        DateTime? ModifiedDate,
        Int32? TotalRecords,
        Int32? PageIndex,
        string? InventoryDonationId,
        string? CheckedBy,
        string? ExpirationDateAndTime,
        string? Volume,
        string? InventoryAboOnLabel,
        Int64? NationalityId,
        Int64? RaceId,
        Int64? ResidenceStatusId,
        string? Nationality,
        string? Race,
        string? Residence,
        Int64? GenderId,
string UnitAttribute,
string CompatibilityResults,
string Remarks,
string CompatibilityValidTill,
string IssuedDateAndTime,
string PatientBloodGroup,
string PatientAntibodyScreen,
string PatientSpecialInstructions,
string ProductCode,
string Comments,
string PatientDOB,
string PatientSex,
string BarCode,
string TestShortName,
bool IsBarcodeAvail,
string SampleName,
string Tube, int UnitCount, int IsRedCell, int BSamInvenId, int BSamProductId,
int IsUploadAvail,
int IsTestUploadAvail
    );
}
