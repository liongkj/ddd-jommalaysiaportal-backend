using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JomMalaysia.Framework.Helper
{
    public class CustomDateTimeConverter : DateTimeConverterBase
    {
        public static string[] DefaultInputFormats = new[] {
        "yyyyMMdd", "yyyy/MM/dd", "dd/MM/yyyy", "dd-MM-yyyy",
        "yyyyMMddHHmmss", "yyyy/MM/dd HH:mm:ss", "dd/MM/yyyy HH:mm:ss", "dd-MM-yyyy HH:mm:ss"
    };
        public static string DefaultOutputFormat = "yyyyMMdd";
        public static bool DefaultEvaluateEmptyStringAsNull = true;

        private string[] InputFormats = DefaultInputFormats;
        private string OutputFormat = DefaultOutputFormat;
        private bool EvaluateEmptyStringAsNull = DefaultEvaluateEmptyStringAsNull;

        public CustomDateTimeConverter()
        {
        }

        public CustomDateTimeConverter(string[] inputFormats, string outputFormat, bool evaluateEmptyStringAsNull = true)
        {
            if (inputFormats != null) InputFormats = inputFormats;
            if (outputFormat != null) OutputFormat = outputFormat;
            EvaluateEmptyStringAsNull = evaluateEmptyStringAsNull;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string v = (reader.Value != null) ? reader.Value.ToString() : null;
            try
            {
                // The following line grants Nullable DateTime support. We will return (DateTime?)null if the Json property is null.
                if (String.IsNullOrEmpty(v) && Nullable.GetUnderlyingType(objectType) != null)
                {
                    // If EvaluateEmptyStringAsNull is true an empty string will be treated as null, 
                    // otherwise we'll let DateTime.ParseExactwill throw an exception in a couple lines.
                    if (v == null || EvaluateEmptyStringAsNull) return null;
                }
                return DateTime.ParseExact(v, InputFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch (Exception e)
            {
                throw new NotSupportedException(String.Format("ERROR: Input value '{0}' is not parseable using the following supported formats: {1}", v, string.Join(",", InputFormats)));
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString(OutputFormat));
        }
    }
}