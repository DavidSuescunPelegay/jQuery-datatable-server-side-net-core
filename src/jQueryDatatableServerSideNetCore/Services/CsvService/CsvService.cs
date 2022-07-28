using jQueryDatatableServerSideNetCore.Data;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace jQueryDatatableServerSideNetCore.Services.CsvService
{
    public class CsvService : ICsvService
    {
        public byte[] Write<T>(IList<T> list, bool includeHeader = true)
        {
            StringBuilder sb = new StringBuilder();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            if (includeHeader)
            {
                sb.AppendLine(CreateCsvHeaderLine(properties));
            }

            foreach (var item in list)
            {
                sb.AppendLine(CreateCsvLine(item, properties));
            }

            return Encoding.ASCII.GetBytes(sb.ToString());
        }

        public string CreateCsvHeaderLine(PropertyInfo[] properties)
        {
            List<string> propertyValues = new List<string>();

            foreach (var prop in properties)
            {
                string value = prop.Name;

                var attribute = prop.GetCustomAttribute(typeof(DisplayAttribute));
                if (attribute != null)
                {
                    value = (attribute as DisplayAttribute).Name;
                }

                CreateCsvStringItem(propertyValues, value);
            }

            return CreateCsvLine(propertyValues);
        }

        public string CreateCsvLine<T>(T item, PropertyInfo[] properties)
        {
            List<string> propertyValues = new List<string>();

            foreach (var prop in properties)
            {
                string stringformatString = string.Empty;
                object value = prop.GetValue(item, null);

                if (prop.PropertyType == typeof(string))
                {
                    CreateCsvStringItem(propertyValues, value);
                }
                else if (prop.PropertyType == typeof(string[]))
                {
                    CreateCsvStringArrayItem(propertyValues, value);
                }
                else if (prop.PropertyType == typeof(List<string>))
                {
                    CreateCsvStringListItem(propertyValues, value);
                }
                else
                {
                    CreateCsvItem(propertyValues, value);
                }
            }

            return CreateCsvLine(propertyValues);
        }

        public string CreateCsvLine(IList<string> list)
        {
            return string.Join(ExportFormat.CsvDelimiter, list);
        }

        public void CreateCsvItem(List<string> propertyValues, object value)
        {
            if (value != null)
            {
                propertyValues.Add(value.ToString());
            }
            else
            {
                propertyValues.Add(string.Empty);
            }
        }

        public void CreateCsvStringListItem(List<string> propertyValues, object value)
        {
            string formatString = "\"{0}\"";
            if (value != null)
            {
                value = CreateCsvLine((List<string>)value);
                propertyValues.Add(string.Format(formatString, ProcessStringEscapeSequence(value)));
            }
            else
            {
                propertyValues.Add(string.Empty);
            }
        }

        public void CreateCsvStringArrayItem(List<string> propertyValues, object value)
        {
            string formatString = "\"{0}\"";
            if (value != null)
            {
                value = CreateCsvLine(((string[])value).ToList());
                propertyValues.Add(string.Format(formatString, ProcessStringEscapeSequence(value)));
            }
            else
            {
                propertyValues.Add(string.Empty);
            }
        }

        public void CreateCsvStringItem(List<string> propertyValues, object value)
        {
            string formatString = "\"{0}\"";
            if (value != null)
            {
                propertyValues.Add(string.Format(formatString, ProcessStringEscapeSequence(value)));
            }
            else
            {
                propertyValues.Add(string.Empty);
            }
        }

        public string ProcessStringEscapeSequence(object value)
        {
            return value.ToString().Replace("\"", "\"\"");
        }
    }
}
