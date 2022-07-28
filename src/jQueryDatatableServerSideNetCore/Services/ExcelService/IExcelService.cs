using System.Reflection;

namespace jQueryDatatableServerSideNetCore.Services.ExcelService
{
    public interface IExcelService
    {
        Task<byte[]> Write<T>(IList<T> registers);
    }
}
