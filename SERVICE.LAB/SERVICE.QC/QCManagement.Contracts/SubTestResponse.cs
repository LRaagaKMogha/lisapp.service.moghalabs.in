using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    [NotMapped]
    public record SubTestResponse
    (
        int RowNo,
        int TestNo,
        string TestName,
        string DepartmentName,
        string SampleName,
        string ContainerName,
        int SubTestNo,
        string SubTestName,
        string MethodName,
        string UnitName,
        int STSequenceNo,
        bool IsActive,
        string ResultType,
        string? SubTestCode,
        DateTime? ModifiedOn
    );
}