using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Audit
{
    public static class DtoMappingRegistry
    {
        private static readonly Dictionary<Tuple<object, string>, Type> _mappings = new Dictionary<Tuple<object, string>, Type>();
        public static void RegisterMappingsFromAssemblies(params Assembly[] assemblies)
        {
            var mappingTypes = assemblies.SelectMany(x => x.GetTypes())
                .Where(t => t.GetCustomAttribute<DtoMappingAttribute>() != null);
            foreach (var mappingType in mappingTypes)
            {
                var attribute = mappingType.GetCustomAttribute<DtoMappingAttribute>();  
                if (attribute != null) 
                { 
                    var instance = Activator.CreateInstance(mappingType);
                    _mappings[Tuple.Create(instance!, attribute.Profile ?? "")] = attribute.DtoType;
                }
            }
        }

        public static DtoToTableMapping<TDto> GetMappingFor<TDto>(string profileName)
        {
            var mapping = _mappings.FirstOrDefault(m => m.Value.Equals(typeof(TDto)) && m.Key.Item2 == profileName);
            if(mapping.Value != null)
            {
                return (DtoToTableMapping<TDto>)mapping.Key.Item1;
            }
            throw new InvalidOperationException($"No mapping found for DTO Type {typeof(TDto).Name}");
        }
    }
}
