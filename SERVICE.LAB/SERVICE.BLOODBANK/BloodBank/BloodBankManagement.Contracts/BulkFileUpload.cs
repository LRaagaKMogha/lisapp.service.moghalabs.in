using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record BulkFileUpload
    (
string? ActualFileName,
string? ManualFileName,
string? FileBinaryData,
string? FileType,
string? FilePath,
string? ExternalVisitID,
int? PatientVisitNo,
int VenueNo,
int VenueBranchNo,
string? ActualBinaryData
    );
}