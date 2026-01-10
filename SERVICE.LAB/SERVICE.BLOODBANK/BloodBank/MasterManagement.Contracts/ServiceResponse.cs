using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterManagement.Contracts
{
    public record ServiceResponse<T> (
        string Message,
        string StatusCode, 
        T Data
    );
}