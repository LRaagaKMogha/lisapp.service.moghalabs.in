using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QCManagement.Contracts;

namespace QCManagement.Services.QCResultEntry
{
    public interface IQCResultEntryService
    {
        Task<ErrorOr<Models.QCResultEntry>> CreateQCResultEntry(Models.QCResultEntry qcResultEntry);
        Task<ErrorOr<List<Models.QCResultEntry>>> UpdateQCResultEntry(List<Models.QCResultEntry> qcResultEntry, string Type);
        Task<ErrorOr<Models.QCResultEntry>> GetQCResultEntry(Int64 id);
        Task<ErrorOr<List<Models.QCResultEntry>>> GetQCResultEntries(QCResultEntryFilterRequest request);

    }
}