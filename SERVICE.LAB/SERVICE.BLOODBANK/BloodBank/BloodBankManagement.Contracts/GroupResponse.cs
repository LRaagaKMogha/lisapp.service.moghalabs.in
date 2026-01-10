using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    [NotMapped]
    public record GroupResponse
    (
        int RowNo,
        int TestNo,
        string TestShortName,
        string TestName,
        string TestDisplayName,
        string DepartmentName,
        string MethodName,
        string SampleName,
        string ContainerName,
        string UnitName,
        int TsequenceNo,
        string ResultType,
        decimal Rate,
        bool IsActive,
        Int32 TotalRecords,
        Int32 PageIndex,
        DateTime? ModifiedOn,
        string TestCode,
        int GroupNo, 
        string GroupName, 
        decimal GroupRate, 
        string GroupCode, 
        string GroupDisplayName,
        string GroupShortName
    );
}