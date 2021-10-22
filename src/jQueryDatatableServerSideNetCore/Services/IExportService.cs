using jQueryDatatableServerSideNetCore.Models.DatabaseModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace jQueryDatatableServerSideNetCore.Services
{
    public interface IExportService
    {
        Task<byte[]> ExportToExcel(List<TestRegister> registers);
    }
}
