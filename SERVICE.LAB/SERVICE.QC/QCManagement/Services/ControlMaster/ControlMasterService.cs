using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Azure.Core;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using QC.Helpers;
using QCManagement.Contracts;
using QCManagement.Models;
using QCManagement.ServiceErrors;

namespace QCManagement.Services.ControlMaster
{
    public class ControlMasterService : IControlMasterService
    {
        private readonly QCDataContext dataContext;
        private readonly IConfiguration Configuration;
        public ControlMasterService(QCDataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.Configuration = configuration;
        }

        public async Task<ErrorOr<Models.ControlMaster>> CreateControlMaster(Models.ControlMaster controlMaster)
        {
            try
            {
                if (controlMaster != null)
                {
                    await dataContext.ControlMasters.AddAsync(controlMaster);
                    await dataContext.SaveChangesAsync();
                    return controlMaster;
                }
            }
            catch (Exception exp)
            {

            }
            return Errors.ControlMaster.NotFound;
        }

        public async Task<ErrorOr<Models.ControlMaster>> GetControlMaster(Int64 id)
        {
            var data = await dataContext.ControlMasters.FindAsync(id);
            if (data != null) return data;
            return Errors.ControlMaster.NotFound;
        }

        public async Task<ErrorOr<List<Models.ControlMaster>>> GetControlMasters(ControlMasterFilterRequest request)
        {
            try
            {
                var controlMasters = new List<string>();
                var distinctControlIdentifiers = new List<long>();
                Expression<Func<Models.ControlMaster, bool>> monitoringMethod = e => e.TestControlSamples.Any(row => row.QcMonitoringMethod == request.QcMonitoringMethod);
                Expression<Func<Models.ControlMaster, bool>> controlMaster = e => e.Identifier == request.ControlMasterId;
                Expression<Func<Models.ControlMaster, bool>> status = e => e.Status == "Captured";
                Expression<Func<Models.ControlMaster, bool>> controlMasterName = e => e.ControlName == request.controlMasterName;
                Expression<Func<Models.ControlMaster, bool>> activeStatus = e => e.IsActive == true;
                Expression<Func<Models.ControlMaster, bool>> inactiveStatus = e => e.IsActive == false;
                Expression<Func<Models.ControlMaster, bool>> controlMasterNames = e => controlMasters.Any(row => e.ControlName == row);
                Expression<Func<Models.ControlMaster, bool>> controlIDs = e => distinctControlIdentifiers.Any(y => e.Identifier == y);

                var query = dataContext.ControlMasters.Where(x => x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate);
                if (request.showAllActive && string.IsNullOrEmpty(request.ActiveStatus))
                    query = dataContext.ControlMasters.Where(x => x.IsActive);

                if (!string.IsNullOrEmpty(request.QcMonitoringMethod)) query = query.Where(monitoringMethod);
                if (!string.IsNullOrEmpty(request.controlMasterName)) query = query.Where(controlMasterName);
                if (request.ControlMasterId.GetValueOrDefault() > 0) query = query.Where(controlMaster);
                if (request.StrainCategoryId.GetValueOrDefault() > 0)
                {
                    controlMasters = dataContext.StrainMasters.Where(y => y.StrainCategoryId == request.StrainCategoryId).Select(y => y.StrainName).ToList();
                    query = query.Where(controlMasterNames);
                }
                query = query.Where(status);
                query = query.Where(x => x.IsAntibiotic == request.IsAntibiotic);
                if (!string.IsNullOrEmpty(request.ActiveStatus))
                {
                    if (request.ActiveStatus == "Active") query = query.Where(activeStatus);
                    if (request.ActiveStatus == "InActive") query = query.Where(inactiveStatus);
                }
                if(request.AnalyzerID.GetValueOrDefault() > 0)
                {
                    distinctControlIdentifiers = await dataContext.QCResultEntries.Where(z => z.AnalyzerID == request.AnalyzerID.GetValueOrDefault()).Select(x => x.ControlID).Distinct().ToListAsync(); 
                    query = query.Where(controlIDs);
                }
                var data = await query
                    .Include(x => x.TestControlSamples).AsSplitQuery().ToListAsync();
                if (data != null) return data;
                return new List<Models.ControlMaster>();
            }
            catch (Exception exp)
            {
                var exp1 = exp;
            }
            return new List<Models.ControlMaster>();
        }

        public async Task<ErrorOr<Models.ControlMaster>> UpdateControlMaster(Models.ControlMaster controlMaster)
        {
            var activeSampleIds = controlMaster.TestControlSamples.Select(x => x.Identifier).ToList();
            var controlMasterData = dataContext.ControlMasters.Include(x => x.TestControlSamples).FirstOrDefault(x => x.Identifier == controlMaster.Identifier);

            if (controlMaster != null && controlMasterData != null)
            {
                controlMasterData.ControlName = controlMaster.ControlName;
                controlMasterData.ControlType = controlMaster.ControlType;
                controlMasterData.LotNumber = controlMaster.LotNumber;
                controlMasterData.ManufacturerID = controlMaster.ManufacturerID;
                controlMasterData.DistributorID = controlMaster.DistributorID;
                controlMasterData.Notes = controlMaster.Notes;
                controlMasterData.Form = controlMaster.Form;
                controlMasterData.Level = controlMaster.Level;
                controlMasterData.ReagentID = controlMaster.ReagentID;
                controlMasterData.ExpirationDate = controlMaster.ExpirationDate;
                controlMasterData.IsQualitative = controlMaster.IsQualitative;
                controlMasterData.IsAntibiotic = controlMaster.IsAntibiotic;
                controlMasterData.IsActive = controlMaster.IsActive;
                controlMasterData.ModifiedBy = controlMaster.ModifiedBy;
                controlMasterData.ModifiedByUserName = controlMaster.ModifiedByUserName;
                controlMasterData.LastModifiedDateTime = controlMaster.LastModifiedDateTime;
                if (!string.IsNullOrEmpty(controlMaster.Status))
                {
                    controlMasterData.Status = controlMaster.Status;
                }
                controlMaster.TestControlSamples.Where(row => row.Identifier == 0).ToList().ForEach(test =>
                {
                    controlMasterData.TestControlSamples.Add(test);
                });
                controlMaster.TestControlSamples.Where(row => row.Identifier != 0).ToList().ForEach(test =>
                {
                    var itemToUpdate = controlMasterData.TestControlSamples.FirstOrDefault(x => x.Identifier == test.Identifier);
                    if (itemToUpdate != null)
                    {
                        itemToUpdate.TestID = test.TestID;
                        itemToUpdate.SubTestID = test.SubTestID;
                        itemToUpdate.QcMonitoringMethod = test.QcMonitoringMethod;
                        itemToUpdate.ControlLimit = test.ControlLimit;
                        itemToUpdate.ParameterName = test.ParameterName;
                        itemToUpdate.ControlID = test.ControlID;
                        itemToUpdate.TargetRangeMin = test.TargetRangeMin;
                        itemToUpdate.TargetRangeMax = test.TargetRangeMax;
                        itemToUpdate.SampleTypeId = test.SampleTypeId;
                        itemToUpdate.UoMId = test.UoMId;
                        itemToUpdate.UomText = test.UomText;
                        itemToUpdate.UoMId2 = test.UoMId2;
                        itemToUpdate.UomText2 = test.UomText2;
                        itemToUpdate.DecimalPlaces = test.DecimalPlaces;
                        itemToUpdate.Mean = test.Mean;
                        itemToUpdate.SD = test.SD;
                        itemToUpdate.Cv = test.Cv;
                        itemToUpdate.ModifiedBy = test.ModifiedBy;
                        itemToUpdate.ModifiedByUserName = test.ModifiedByUserName;
                        itemToUpdate.LastModifiedDateTime = test.LastModifiedDateTime;
                        itemToUpdate.IsSelected = test.IsSelected;
                        itemToUpdate.StartTime = test.StartTime;
                        itemToUpdate.EndTime = test.EndTime;
                    }
                });
                controlMasterData.TestControlSamples.Where(y => y.Identifier != 0 && !activeSampleIds.Any(z => z.Equals(y.Identifier))).ToList().ForEach(row =>
                {
                    controlMasterData.TestControlSamples.Remove(row);
                });
                dataContext.ControlMasters.Update(controlMasterData);
                await dataContext.SaveChangesAsync();
                return controlMaster;
            }
            return Errors.ControlMaster.NotFound;
        }

        public async Task<ErrorOr<Models.ControlMaster>> PrepareControlMaster(PrepareControlMasterRequest request)
        {
            var controlMaster = dataContext.ControlMasters.Find(request.Identifier);
            if (controlMaster != null)
            {
                controlMaster.PreparedBy = request.PreparedBy;
                controlMaster.PreparedByUserName = request.PreparedByUserName;
                controlMaster.PreparationDateTime = request.PreparationDateTime;
                controlMaster.StorageTemperature = request.StorageTemperature;
                controlMaster.AliquoteCount = request.AliquoteCount;
                controlMaster.LastModifiedDateTime = request.LastModifiedDateTime;
                controlMaster.ModifiedBy = request.ModifiedBy;
                controlMaster.ModifiedByUserName = request.ModifiedByUserName;
                controlMaster.Status = "Prepared";
                dataContext.ControlMasters.Update(controlMaster);
                await dataContext.SaveChangesAsync();
                return controlMaster;
            }
            return Errors.ControlMaster.NotFound;
        }
    }
}