using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;

namespace BloodBankManagement.Services.Downloads
{
    public interface IDownloadService
    {
        Task<ErrorOr<Dictionary<string, List<string>>>> GenerateTransfusedLinesForEDI(DateTime startDate, DateTime endDate);

    }
}