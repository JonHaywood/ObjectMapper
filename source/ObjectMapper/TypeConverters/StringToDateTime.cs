using System;
using System.Globalization;

namespace ObjectMapper.TypeConverters
{
    public class StringToDateTime : TypeConverter<String, DateTime>
    {
        private readonly string[] dateFormats;

        public StringToDateTime(params string[] dateFormats)
        {
            this.dateFormats = dateFormats;
        }

        protected override DateTime ConvertSource(string source)
        {
            DateTime destination;
            DateTime.TryParseExact(source, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out destination);
            return destination;
        }
    }
}
