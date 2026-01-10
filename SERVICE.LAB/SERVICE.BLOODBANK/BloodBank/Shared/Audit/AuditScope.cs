using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Audit
{
    public class AuditScope<TDto> : IDisposable where TDto: class
    {
        private readonly IEnumerable<TDto> _dtos;
        private Dictionary<string, TDto> _enumerations;
        private Dictionary<string, Dictionary<string, object>> _oldValues = new Dictionary<string, Dictionary<string, object>>();
        public DtoToTableMapping<TDto> Mapping;
        private readonly IAuditService _auditService;
        private bool _disposed = false;
        private List<string> _entityIds = new List<string>();
        public bool IsRollBack { get; set; }
        public string VisitNo { get; set; }
        public string LabAccessionNo { get; set; }
        public string ContextId { get; set; }
        public Object AdditionalData { get; set; }
        public AuditScope(TDto dto, IAuditService auditService, string profileName = "", params string[] otherInputs) : this("", dto, auditService, profileName, otherInputs)
        {

        }
        public AuditScope(IEnumerable<TDto> dto, IAuditService auditService, string profileName = "", params string[] otherInputs) : this(new List<string>(), dto, auditService, profileName, otherInputs)
        {

        }
        public AuditScope(string entityId, TDto dto, IAuditService auditService, string profileName = "", params string[] otherInputs) : this(new List<string> { entityId }, new List<TDto> { dto }, auditService, profileName, otherInputs)
        {

        }

        public bool IsSpecificGenericType(object obj, Type genericType)
        {
            var objType = obj.GetType();
            if (!objType.IsGenericType) return false;
            return objType.GetGenericTypeDefinition() == genericType;
        }
        public AuditScope(Dictionary<string, TDto> dtos, IAuditService auditService, string profileName = "", params string[] otherInputs)
        {
            _enumerations = dtos;
            _dtos = new List<TDto>();
            _auditService = auditService;
            Mapping = DtoMappingRegistry.GetMappingFor<TDto>(profileName);
            if (otherInputs != null)
            {
                var index = 0;
                foreach (string input in otherInputs)
                {
                    if (index == 0 && !string.IsNullOrEmpty(input)) Mapping.UserAction = input;
                    if (index == 1 && !string.IsNullOrEmpty(input)) VisitNo = input;
                    if (index == 2 && !string.IsNullOrEmpty(input)) LabAccessionNo = input;
                    index++;
                }
            }
        }
        public AuditScope(List<string> entityIds, IEnumerable<TDto> dtos, IAuditService auditService, string profileName = "", params string[] otherInputs)
        {
            _entityIds = entityIds.Where(x => x != "").ToList();
            _dtos = dtos;
            _auditService = auditService;
            Mapping = DtoMappingRegistry.GetMappingFor<TDto>(profileName);
            if (otherInputs != null)
            {
                var index = 0;
                foreach (string input in otherInputs)
                {
                    if (index == 0 && !string.IsNullOrEmpty(input)) Mapping.UserAction = input;
                    if (index == 1 && !string.IsNullOrEmpty(input)) VisitNo = input;
                    if (index == 2 && !string.IsNullOrEmpty(input)) LabAccessionNo = input;
                    index++;
                }
            }
            var entityIdValues = _entityIds.Count > 0 ? _entityIds : _dtos.Select(dto => string.IsNullOrEmpty(Mapping.Query) ? GetEntityId(dto) : Mapping.EntityIdSelector(dto)).ToList();
            _oldValues = entityIdValues.Any(e => !string.IsNullOrEmpty(e) && e != "0") ? GetEntityValuesForList(entityIdValues) : new Dictionary<string, Dictionary<string, object>>();
        }

        private string GetEntityId(TDto dto)
        {
            var entityIdProperty = Mapping.EntityIdProperty;
            string entityId = typeof(TDto).GetProperty(entityIdProperty)?.GetValue(dto)?.ToString() ?? "";
            return entityId;
        }

        private Dictionary<string, Dictionary<string, object>> GetEntityValuesForList(IEnumerable<string> entityIds)
        {
            var dbEntityId = Mapping.PropertyMappings.Values.Any(m => m.PropertyName == Mapping.EntityIdProperty) ? Mapping.PropertyMappings.Values.First(y => y.PropertyName == Mapping.EntityIdProperty).ColumnName : Mapping.EntityIdProperty;
            var idMappingQueryText = Mapping.PropertyMappings.Values.Any(m => m.ColumnName == dbEntityId) ? "" : ("," + dbEntityId);
            var queryInput = new { EntityIds = entityIds } as Object;
            var query = $@"
                SELECT {string.Join(",", Mapping.PropertyMappings.Values.Select(m => m.ColumnName).Where(m => !string.IsNullOrEmpty(m)))} {idMappingQueryText}
                 FROM {Mapping.TableName} WHERE {dbEntityId} IN @EntityIds";
            if (!string.IsNullOrEmpty(Mapping.Query))
            {
                queryInput = Mapping.InputSelector(_dtos.ToList());
                query = Mapping.Query;
            }
            var dictionary = _auditService.Query(query, dbEntityId, queryInput);
            return dictionary;
        }


        public void Dispose()
        {
            if (_disposed)
                return;
            try
            {
                if (!IsRollBack)
                {
                    if (_dtos.Count() > 0)
                        LogChanges(_dtos);
                    else
                    {
                        foreach (KeyValuePair<string, TDto> kvp in _enumerations)
                        {
                            var dtoToLog = kvp.Value as TDto;
                            LogChanges(new List<TDto>() { dtoToLog }, kvp.Key);
                        }
                    }
                }

            }
            finally
            {
                _disposed = true;
            }
        }

        private void LogChanges(IEnumerable<TDto> dtos, string visitNo = "")
        {
            var index = 0;
            foreach (var dto in dtos)
            {
                var entityId = _entityIds.Count > 0 ? _entityIds[index] : (!string.IsNullOrEmpty(Mapping.Query) ? Mapping.EntityIdSelector(dto) : GetEntityId(dto));
                if (!string.IsNullOrEmpty(visitNo)) entityId = visitNo.ToString();
                var newValues = new Dictionary<string, object>();
                var propertyMetadata = new Dictionary<string, string>();
                var auditEntries = new List<AuditLogEntry>();
                var oldValues = new Dictionary<string, object>();
                if (_oldValues.ContainsKey(entityId)) oldValues = _oldValues[entityId];
                foreach (var property in typeof(TDto).GetProperties())
                {
                    var propertyName = property.Name;
                    if (Mapping.PropertyMappings.ContainsKey(propertyName))
                    {
                        var newValue = property.GetValue(dto);
                        newValues[propertyName] = newValue;
                    }
                }

                //Compare and log changes for each DTO 
                foreach (var propertyMapping in Mapping.PropertyMappings.Values)
                {
                    var propertyName = string.IsNullOrEmpty(propertyMapping.ColumnName) ? propertyMapping.PropertyName.ToLower() : propertyMapping.ColumnName.ToLower();
                    var oldValue = oldValues.ContainsKey(propertyName) && !string.IsNullOrEmpty(oldValues[propertyName]?.ToString()) ? oldValues[propertyName]?.ToString() : "";
                    var newValue = "";
                    if (newValues.ContainsKey(propertyMapping.PropertyName) && newValues[propertyMapping.PropertyName] != null)
                    {
                        if (propertyMapping.ValueSelector != null)
                        {
                            var transformedData = propertyMapping.ValueSelector(dto!, AdditionalData);
                            if (transformedData is string) newValue = (string)transformedData;
                            else newValue = JsonConvert.SerializeObject(transformedData);
                        }
                            
                        else if (newValues[propertyMapping.PropertyName] is int[] intArray)
                            newValue = string.Join(",", intArray);
                        else if (newValues[propertyMapping.PropertyName] is string[] stringArray)
                            newValue = string.Join(",", stringArray);
                        else if (newValues[propertyMapping.PropertyName] is long[] longArray)
                            newValue = string.Join(",", longArray);
                        else
                            newValue = newValues[propertyMapping.PropertyName]?.ToString() ?? oldValue;
                    }
                    else
                    {
                        newValue = oldValue;
                    }
                    newValues[propertyMapping.PropertyName] = newValue;
                    var metadataJson = propertyMapping.GetSerializedMetadataFactory((dynamic)newValue);
                    if (metadataJson != null)
                    {
                        propertyMetadata[propertyMapping.PropertyName] = metadataJson;
                    }
                        
                    if (!Equals(oldValue, newValue) && oldValues.Count > 0)
                    {
                        auditEntries.Add(new AuditLogEntry
                        {
                            EntityId = entityId,
                            TableName = Mapping.TableName,
                            ColumnName = !string.IsNullOrEmpty(propertyMapping.ColumnName) ? propertyMapping.ColumnName : propertyMapping.PropertyName,
                            OldValue = oldValue?.ToString(),
                            NewValue = newValue?.ToString(),
                            MetadataJson = metadataJson,
                            ModifiedOn = DateTime.Now,
                            UserAction = Mapping.UserAction,
                            VisitNo = Mapping.UseEntityIdAsVisitNo ? entityId : VisitNo,
                            LabAccessionNo = LabAccessionNo,
                            SubMenuCode = Mapping.SubMenuCode,
                            ContextId = ContextId
                        });

                    }
                } //end of propertymapping looping

                //create summary entry 
                newValues = Mapping.EnrichSummary(newValues, _auditService.User);
                var combinedPropertyValuesJson = newValues.Count > 0 ? JsonConvert.SerializeObject(newValues) : null;
                var combinedMetadataJson = propertyMetadata.Count > 0 ? JsonConvert.SerializeObject(propertyMetadata) : null;

                if (auditEntries.Count > 0 || string.IsNullOrEmpty(entityId) || entityId == "0" || oldValues.Count == 0)
                {
                    if(Mapping.UseNewContextIdForEachEntry)
                    {
                        ContextId = Guid.NewGuid().ToString();
                    }
                    auditEntries.Add(new AuditLogEntry()
                    {
                        EntityId = entityId,
                        TableName = Mapping.TableName,
                        ColumnName = "Combined",
                        OldValue = "",
                        NewValue = combinedPropertyValuesJson,
                        MetadataJson = combinedMetadataJson,
                        ModifiedOn = DateTime.Now,
                        UserAction = Mapping.UserAction,
                        VisitNo = Mapping.UseEntityIdAsVisitNo ? entityId : VisitNo,
                        LabAccessionNo = LabAccessionNo,
                        SubMenuCode = Mapping.SubMenuCode,
                        ContextId = ContextId
                    });
                }

                _auditService.LogChanges(auditEntries);

                index++;
            }//for each dto loop end.
        }
    }
}
