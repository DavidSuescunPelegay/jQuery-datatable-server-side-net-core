using jQueryDatatableServerSideNetCore.Models.DatabaseModels;
using jQueryDatatableServerSideNetCore.Services.CsvService;
using jQueryDatatableServerSideNetCore.Services.ExcelService;
using jQueryDatatableServerSideNetCore.Services.HtmlService;
using jQueryDatatableServerSideNetCore.Services.JsonService;
using jQueryDatatableServerSideNetCore.Services.XmlService;
using jQueryDatatableServerSideNetCore.Services.YamlService;
using System.Text;

namespace jQueryDatatableServerSideNetCore.Services.ExportService
{
    public class ExportService<TModel> : IExportService<TModel> where TModel : class
    {
        private readonly IExcelService _excelService;
        private readonly ICsvService _csvService;
        private readonly IHtmlService _htmlService;
        private readonly IJsonService _jsonService;
        private readonly IXmlService _xmlService;
        private readonly IYamlService _yamlService;

        public ExportService(IExcelService excelService, ICsvService csvService, IHtmlService htmlService, IJsonService jsonService, IXmlService xmlService, IYamlService yamlService)
        {
            _excelService = excelService;
            _csvService = csvService;
            _htmlService = htmlService;
            _jsonService = jsonService;
            _xmlService = xmlService;
            _yamlService = yamlService;
        }

        public async Task<byte[]> ExportToExcel(List<TModel> registers)
        {
            return await _excelService.Write(registers);
        }

        public byte[] ExportToCsv(List<TModel> registers)
        {
            return _csvService.Write(registers);
        }

        public byte[] ExportToHtml(List<TModel> registers)
        {
            return _htmlService.Write(registers);
        }

        public byte[] ExportToJson(List<TModel> registers)
        {
            return _jsonService.Write(registers);
        }

        public byte[] ExportToXml(List<TModel> registers)
        {
            return _xmlService.Write(registers);
        }

        public byte[] ExportToYaml(List<TModel> registers)
        {
            return _yamlService.Write(registers);
        }
    }
}
