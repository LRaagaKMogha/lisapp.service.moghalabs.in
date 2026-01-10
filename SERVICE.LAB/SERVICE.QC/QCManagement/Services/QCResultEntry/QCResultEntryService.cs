using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QC.Helpers;
using QCManagement.Contracts;
using QCManagement.ServiceErrors;

namespace QCManagement.Services.QCResultEntry
{
    public class QCResultEntryService : IQCResultEntryService
    {
        private readonly QCDataContext dataContext;

        public QCResultEntryService(QCDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        private int GetNextSequenceNumber()
        {
            var p = new SqlParameter("@result", System.Data.SqlDbType.Int);
            p.Direction = System.Data.ParameterDirection.Output;
            dataContext.Database.ExecuteSqlRaw("set @result = NEXT VALUE FOR MicroQcResultEntrySequence", p);
            var nextVal = (int)p.Value;
            return nextVal;
        }
        public async Task<ErrorOr<Models.QCResultEntry>> CreateQCResultEntry(Models.QCResultEntry qcResultEntry)
        {
            if (qcResultEntry != null)
            {

                await dataContext.QCResultEntries.AddAsync(qcResultEntry);
                await dataContext.SaveChangesAsync();
                return qcResultEntry;
            }
            return Errors.QCResultEntry.NotFound;
        }

        public async Task<ErrorOr<List<Models.QCResultEntry>>> GetQCResultEntries(QCResultEntryFilterRequest request)
        {
            try
            {
                Expression<Func<Models.QCResultEntry, bool>> monitoringMethod = e => e.QcMonitoringMethod == request.QcMonitoringMethod;
                Expression<Func<Models.QCResultEntry, bool>> controlID = e => e.ControlID == request.ControlID;
                Expression<Func<Models.QCResultEntry, bool>> controlIDs = e => request.ControlIDs.Any(y => y == e.ControlID);
                Expression<Func<Models.QCResultEntry, bool>> analyzerID = e => e.AnalyzerID == request.AnalyzerID;
                Expression<Func<Models.QCResultEntry, bool>> testID = e => e.TestID == request.TestID;
                Expression<Func<Models.QCResultEntry, bool>> testControlSamplesID = e => e.TestControlSamplesID == request.TestControlSamplesID;
                Expression<Func<Models.QCResultEntry, bool>> activeStatus = e => e.Status == request.ActiveStatus;

                var query = dataContext.QCResultEntries.Where(x => x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate && x.IsMicroQc == request.IsMicroQc && x.IsAntibiotic == request.IsAntibiotic);
                if(request.IsFilterBasedOnTestDate)
                {
                    query = dataContext.QCResultEntries.Where(x => x.TestDate >= request.StartDate && x.TestDate <= request.EndDate && x.IsMicroQc == request.IsMicroQc && x.IsAntibiotic == request.IsAntibiotic);
                }
                if (request.showAllActive && string.IsNullOrEmpty(request.ActiveStatus))
                    query = dataContext.QCResultEntries.AsQueryable();
                if (!string.IsNullOrEmpty(request.QcMonitoringMethod)) query = query.Where(monitoringMethod);
                if (request.ControlID != 0) query = query.Where(controlID);
                if (request.ControlIDs != null && request.ControlIDs.Count > 0) query = query.Where(controlIDs);
                if (request.AnalyzerID != 0) query = query.Where(analyzerID);
                if (request.TestID != 0) query = query.Where(testID);
                if (request.TestControlSamplesID > 0) query = query.Where(testControlSamplesID);
                if (!string.IsNullOrEmpty(request.ActiveStatus)) query = query.Where(activeStatus);

                var data = await query.ToListAsync();
                if (data != null) return data;
            }
            catch (Exception exp)
            {

            }
            return new List<Models.QCResultEntry>();
        }

        public async Task<ErrorOr<Models.QCResultEntry>> GetQCResultEntry(Int64 id)
        {

            var data = await dataContext.QCResultEntries.FindAsync(id);
            if (data != null) return data;
            return Errors.QCResultEntry.NotFound;
        }

        public async Task<ErrorOr<List<Models.QCResultEntry>>> UpdateQCResultEntry(List<Models.QCResultEntry> qcResultEntries, string type)
        {
            var batchId = 0;
            if (qcResultEntries.Any(x => x.Identifier == 0 && x.IsMicroQc && (string.IsNullOrEmpty(x.BatchId) || x.BatchId == "0")))
            {
                batchId = GetNextSequenceNumber();
                qcResultEntries.ForEach(row => row.BatchId = batchId.ToString());
            }


            if (qcResultEntries != null)
            {
                try
                {
                    var testControlRelatedEntries = dataContext.QCResultEntries.Where(x => x.ControlID == qcResultEntries.First().ControlID).ToList();
                    if(qcResultEntries.Any(y => y.IsMicroQc))
                    {
                        testControlRelatedEntries = dataContext.QCResultEntries.Where(x => x.BatchId == qcResultEntries.First().BatchId).ToList();
                    }
                    qcResultEntries.Where(x => x.Identifier != 0).ToList().ForEach(row =>
                    {
                        var itemToUpdate = testControlRelatedEntries.Find(r => r.Identifier == row.Identifier);
                        if (itemToUpdate == null) return;
                        itemToUpdate.AnalyzerID = row.AnalyzerID;
                        itemToUpdate.TestID = row.TestID;
                        itemToUpdate.ParameterName = row.ParameterName;
                        itemToUpdate.TestDate = row.TestDate;
                        itemToUpdate.ObservedValue = row.ObservedValue;
                        itemToUpdate.IsActive = row.IsActive;
                        itemToUpdate.Status = row.Status;
                        itemToUpdate.IsMicroQc = row.IsMicroQc;
                        itemToUpdate.IsAntibiotic = row.IsAntibiotic;
                        itemToUpdate.LastModifiedDateTime = row.LastModifiedDateTime;
                        itemToUpdate.TestControlSamplesID = row.TestControlSamplesID;
                        //itemToUpdate.ModifiedByUserName = row.ModifiedByUserName;
                        itemToUpdate.Comments = row.Comments;
                        dataContext.QCResultEntries.Update(itemToUpdate);
                    });
                    dataContext.QCResultEntries.AddRange(qcResultEntries.Where(x => x.Identifier == 0).ToList());
                    if (type == "ESTABLISHMENT")
                    {
                        var itemsToRemove = testControlRelatedEntries.Where(id => !qcResultEntries.Any(p => p.Identifier == id.Identifier) && id.TestControlSamplesID != 0).ToList();
                        dataContext.QCResultEntries.RemoveRange(itemsToRemove);
                    }
                    await dataContext.SaveChangesAsync();
                    return qcResultEntries;
                }
                catch (Exception exp)
                {

                }

            }
            return Errors.QCResultEntry.NotFound;
        }
    }
}