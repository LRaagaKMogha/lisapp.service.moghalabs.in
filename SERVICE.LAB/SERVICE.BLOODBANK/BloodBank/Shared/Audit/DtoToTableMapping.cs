using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Audit
{
    public abstract class DtoToTableMapping<TDto>
    {
        public string TableName { get; set; }
        public bool IsAutoMap { get; set; } = true;
        public List<string> IgnoredArchitecturalProperties { get; set; } = new List<string> {  "CreatedOn", "CreatedBy", "ModifiedOn", "ModifiedBy", "VenueNo", "VenueBranchNo", "LastModifiedDateTime", "ModifiedByUserName" };
        public string EntityIdProperty { get; set; }
        public string  Query { get; set; }
        public string UserAction {  get; set; } 
        public string SubMenuCode {  get; set; }
        public bool UseEntityIdAsVisitNo {  get; set; }
        public bool UseNewContextIdForEachEntry { get; set;} 
        public Func<List<TDto>, object> InputSelector { get; set; }
        public Func<TDto, string> EntityIdSelector {  get; set; }
        public Dictionary<string, PropertyMapping> PropertyMappings { get; set; } = new Dictionary<string, PropertyMapping>();

        public List<Expression<Func<TDto, object>>> IgnoreProperties { get; set; } = new List<Expression<Func<TDto, object>>>();

        public abstract void SetUp();
        public virtual Dictionary<string, object> EnrichSummary(Dictionary<string, object> input, User user)
        {
            input.Add("Modified By", user.UserName);
            input.Add("Modified At", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            foreach(KeyValuePair<string, object> kvp in input)
            {
                if(kvp.Value != null && kvp.Value is string)
                {
                    DateTime parsedDate;
                    bool isValidDate = DateTime.TryParseExact((string)kvp.Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate);
                    if(isValidDate)
                    {
                        input[kvp.Key] = parsedDate.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                }
            }
            return input;
        }
        public DtoToTableMapping()
        {
            this.SetUp();
            if (this.IsAutoMap)
                this.AutoMap();
        }

        public void QueryMapping(string query, Func<List<TDto>, object> dtos, Func<TDto, string> entityId)
        {
            Query = query;
            InputSelector = dtos;
            EntityIdSelector = entityId;
        }

        public void ReplaceLabels(Dictionary<string, string> input, Dictionary<string, object> newValues)
        {
            foreach(KeyValuePair<string, string> kvp in input)
            {
                if (newValues.ContainsKey(kvp.Key))
                {
                    var value = newValues[kvp.Key];
                    newValues.Remove(kvp.Key);
                    newValues.Add(kvp.Value, value);
                }
            }
        }
        private void AutoMap()
        {
            var properties = typeof(TDto).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (IgnoreProperties.Any(exp => IsPropertyIgnored(exp, property.Name))
                    || IgnoredArchitecturalProperties.Any(a => a.ToLower() == property.Name.ToLower()))
                {
                    continue;
                }

                if (!PropertyMappings.ContainsKey(property.Name))
                {
                    PropertyMappings[property.Name] = new PropertyMapping
                    {
                        PropertyName = property.Name,
                        ColumnName = property.Name
                    }; ;
                }

            }
        }

        private bool IsPropertyIgnored(Expression<Func<TDto, object>> ignoreExpression, string propertyName)
        {
            if (ignoreExpression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name == propertyName;
            }
            if (ignoreExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression memberOperand)
            {
                return memberOperand.Member.Name == propertyName;
            }
            return false;
        }
        public void AddPropertyExtended<TProperty>(
            Expression<Func<TDto, TProperty>> propertyExpression,
            string columnName,
            Func<object, object> metadataFactory = null,
            Func<TDto, object, object> valueSelector = null)
        {
            var propertyName = GetPropertyName(propertyExpression);
            var propertyMapping = new PropertyMapping
            {
                PropertyName = propertyName,
                ColumnName = columnName,
                MetadataFactory = metadataFactory != null
                ? (value => metadataFactory((object)value))
                : null,
                ValueSelector = valueSelector != null
                ? ((value, value2) => valueSelector((TDto)value, value2))
                : null
            };
            PropertyMappings[propertyName] = propertyMapping;

        }
            public void AddProperty<TProperty>(
            Expression<Func<TDto, TProperty>> propertyExpression,
            string columnName,
            Func<object, object> metadataFactory = null,
            Func<TDto, object> valueSelector = null)
        {
            var propertyName = GetPropertyName(propertyExpression);
            var propertyMapping = new PropertyMapping
            {
                PropertyName = propertyName,
                ColumnName = columnName,
                MetadataFactory = metadataFactory != null
                ? (value => metadataFactory((object)value))
                : null,
                ValueSelector = valueSelector != null
                ? ((value, value2) => valueSelector((TDto)value))
                : null
            };
            PropertyMappings[propertyName] = propertyMapping;
        }

        private string GetPropertyName<TProperty>(Expression<Func<TDto, TProperty>> propertyExpression)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression memberOperand)
            {
                return memberOperand.Member.Name;
            }
            throw new ArgumentException("Invalid property expression");
        }
    }
}
