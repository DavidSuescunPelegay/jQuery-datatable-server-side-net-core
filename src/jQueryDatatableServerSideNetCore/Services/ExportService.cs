using jQueryDatatableServerSideNetCore.Models.DatabaseModels;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jQueryDatatableServerSideNetCore.Services
{
    public class ExportService : IExportService
    {
        public async Task<byte[]> ExportToExcel(List<TestRegister> registers)
        {
            var registersTotalRows = registers.Count;

            using (var excelPackage = new ExcelPackage())
            {
                var excelWorksheet = excelPackage.Workbook.Worksheets.Add("TestRegisters");
                excelWorksheet.Cells[2, 1].Value = "Id";
                excelWorksheet.Cells[2, 2].Value = "Name";
                excelWorksheet.Cells[2, 3].Value = "FirstSurname";
                excelWorksheet.Cells[2, 4].Value = "SecondSurname";
                excelWorksheet.Cells[2, 5].Value = "Street";
                excelWorksheet.Cells[2, 6].Value = "Phone";
                excelWorksheet.Cells[2, 7].Value = "ZipCode";
                excelWorksheet.Cells[2, 8].Value = "Country";
                excelWorksheet.Cells[2, 9].Value = "Notes";
                excelWorksheet.Cells[2, 10].Value = "CreationDate";

                int index = 0;
                for (int row = 3; row <= registersTotalRows + 2; row++)
                {
                    excelWorksheet.Cells[row, 1].Value = registers[index].Id;
                    excelWorksheet.Cells[row, 2].Value = registers[index].Name;
                    excelWorksheet.Cells[row, 3].Value = registers[index].FirstSurname;
                    excelWorksheet.Cells[row, 4].Value = registers[index].SecondSurname;
                    excelWorksheet.Cells[row, 5].Value = registers[index].Street;
                    excelWorksheet.Cells[row, 6].Value = registers[index].Phone;
                    excelWorksheet.Cells[row, 7].Value = registers[index].ZipCode;
                    excelWorksheet.Cells[row, 8].Value = registers[index].Country;
                    excelWorksheet.Cells[row, 9].Value = registers[index].Notes;
                    excelWorksheet.Cells[row, 10].Value = registers[index].CreationDate.ToString("dd/MM/yyyy HH:mm:ss");

                    index++;
                }
                excelWorksheet.Cells.AutoFitColumns();

                var excelTable = excelWorksheet.Tables.Add(new ExcelAddressBase(fromRow: 2, fromCol: 1, toRow: registersTotalRows + 2, toColumn: 10), "TestRegisters");
                excelTable.ShowHeader = true;

                var excelRange = excelWorksheet.Cells[1, 1, excelWorksheet.Dimension.End.Row, excelWorksheet.Dimension.End.Column];
                excelRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                excelTable.TableStyle = TableStyles.Light21;
                excelTable.ShowTotal = true;

                return await excelPackage.GetAsByteArrayAsync();
            }
        }
    }
}
