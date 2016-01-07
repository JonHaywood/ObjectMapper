using System;
using System.Globalization;
using ObjectMapper.MappingRules.TypeConverters;

namespace ObjectMapper.TypeConverters
{
    public class DateTimeToString : TypeConverter<DateTime, String>
    {
        private readonly string dateFormat;

        public DateTimeToString(string dateFormat = null)
        {
            this.dateFormat = dateFormat;
        }

        protected override string ConvertSource(DateTime source)
        {
            return string.IsNullOrEmpty(dateFormat)
                ? source.ToString(CultureInfo.InvariantCulture)
                : source.ToString(dateFormat);
        }
    }
}
