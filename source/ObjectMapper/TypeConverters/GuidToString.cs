using System;

namespace ObjectMapper.TypeConverters
{
    public class GuidToString : TypeConverter<Guid, String>
    {
        protected override string ConvertSource(Guid source)
        {
            return source.ToString();
        }
    }
}
