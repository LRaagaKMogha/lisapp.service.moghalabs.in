using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Audit
{
    public class PropertyMapping
    {
        public string PropertyName { get; set; }
        public string ColumnName {  get; set; }
        public Func<object, object> MetadataFactory { get; set; }
        public Func<object, object, object> ValueSelector { get; set; }
       
        public string GetSerializedMetadataFactory(object propertyValue)
        {
            if(MetadataFactory == null)
            {
                return null;
            }
            var metadata = MetadataFactory(propertyValue);
            return JsonSerializer.Serialize(metadata);
        }
    }
}
