using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    [NotMapped]
    public record TestResponse
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
        DateTime? ModifiedOn
    );
}