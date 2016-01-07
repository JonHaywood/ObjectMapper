using System;
using ObjectMapper.TypeConverters;

namespace ObjectMapper.MappingRules.TypeConverters
{
    public class StringToGuid : TypeConverter<String, Guid>
    {
        protected override Guid ConvertSource(string source)
        {
            Guid guid;
            Guid.TryParse(source, out guid);
            return guid;
        }
    }
}
