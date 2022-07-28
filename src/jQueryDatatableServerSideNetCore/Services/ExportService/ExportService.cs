using jQueryDatatableServerSideNetCore.Models.DatabaseModels;
using jQueryDatatableServerSideNetCore.Services.CsvService;
using jQueryDatatableServerSideNetCore.Services.ExcelService;
using System.Text;

namespace jQueryDatatableServerSideNetCore.Services.ExportService
{
    public class ExportService : IExportService
    {
        private readonly IExcelService _excelService;
        private readonly ICsvService _csvService;

        public ExportService(IExcelService excelService, ICsvService csvService)
        {
            _excelService = excelService;
            _csvService = csvService;
        }

        public async Task<byte[]> ExportToExcel(List<TestRegister> registers)
        {
            return await _excelService.Write(registers);
        }

        public byte[] ExportToCsv(List<TestRegister> registers)
        {
            return _csvService.Write(registers);
        }

    }
}
