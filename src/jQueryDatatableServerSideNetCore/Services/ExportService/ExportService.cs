using jQueryDatatableServerSideNetCore.Models.DatabaseModels;
using jQueryDatatableServerSideNetCore.Services.CsvService;
using jQueryDatatableServerSideNetCore.Services.ExcelService;
using jQueryDatatableServerSideNetCore.Services.HtmlService;
using jQueryDatatableServerSideNetCore.Services.JsonService;

namespace jQueryDatatableServerSideNetCore.Services.ExportService
{
    public class ExportService : IExportService
    {
        private readonly IExcelService _excelService;
        private readonly ICsvService _csvService;
        private readonly IHtmlService _htmlService;
        private readonly IJsonService _jsonService;

        public ExportService(IExcelService excelService, ICsvService csvService, IHtmlService htmlService, IJsonService jsonService)
        {
            _excelService = excelService;
            _csvService = csvService;
            _htmlService = htmlService;
            _jsonService = jsonService;
        }

        public async Task<byte[]> ExportToExcel(List<TestRegister> registers)
        {
            return await _excelService.Write(registers);
        }

        public byte[] ExportToCsv(List<TestRegister> registers)
        {
            return _csvService.Write(registers);
        }

        public byte[] ExportToHtml(List<TestRegister> registers)
        {
            return _htmlService.Write(registers);
        }

        public byte[] ExportToJson(List<TestRegister> registers)
        {
            return _jsonService.Write(registers);
        }
    }
}
