using System.Reflection;

namespace jQueryDatatableServerSideNetCore.Services.CsvService
{
    public interface ICsvService
    {
        byte[] Write<T>(IList<T> list, bool includeHeader = true);

        string CreateCsvHeaderLine(PropertyInfo[] properties);

        string CreateCsvLine<T>(T item, PropertyInfo[] properties);

        string CreateCsvLine(IList<string> list);

        void CreateCsvItem(List<string> propertyValues, object value);

        void CreateCsvStringListItem(List<string> propertyValues, object value);

        void CreateCsvStringArrayItem(List<string> propertyValues, object value);

        void CreateCsvStringItem(List<string> propertyValues, object value);

        string ProcessStringEscapeSequence(object value);
    }
}
