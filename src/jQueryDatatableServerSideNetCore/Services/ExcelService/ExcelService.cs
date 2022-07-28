using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace jQueryDatatableServerSideNetCore.Services.ExcelService
{
    public class ExcelService : IExcelService
    {
        public async Task<byte[]> Write<T>(IList<T> registers)
        {
            var registersTotalRows = registers.Count;

            using (var excelPackage = new ExcelPackage())
            {
                var excelWorksheet = excelPackage.Workbook.Worksheets.Add("TestRegisters");

                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties();

                for (var i = 0; i < properties.Length; i++)
                {
                    string value = properties[i].Name;

                    var attribute = properties[i].GetCustomAttribute(typeof(DisplayAttribute));
                    if (attribute != null)
                    {
                        value = (attribute as DisplayAttribute).Name;
                    }

                    excelWorksheet.Cells[1, i + 1].Value = value;
                }

                int index = 0;
                for (int row = 2; row <= registersTotalRows + 1; row++)
                {
                    for (var i = 0; i < properties.Length; i++)
                    {
                        var value = properties[i].GetValue(registers[index], null);

                        Type propertyType = properties[i].PropertyType;
                        TypeCode typeCode = Type.GetTypeCode(propertyType);

                        var columnIndex = i + 1;

                        switch (typeCode)
                        {
                            case TypeCode.String:
                                excelWorksheet.Cells[row, columnIndex].Value = value;
                                break;

                            case TypeCode.Int32:
                            case TypeCode.Double:
                            case TypeCode.Decimal:
                            case TypeCode.Single:
                                excelWorksheet.Cells[row, columnIndex].Value = value?.ToString();
                                break;

                            case TypeCode.Boolean:
                                excelWorksheet.Cells[row, columnIndex].Value = ((bool)value) ? "Yes" : "No";
                                break;

                            case TypeCode.DateTime:
                                excelWorksheet.Cells[row, columnIndex].Value = ((DateTime)value).ToString("dd/MM/yyyy HH:mm:ss");
                                break;

                            default:
                                excelWorksheet.Cells[row, columnIndex].Value = string.Empty;
                                break;
                        }
                    }

                    index++;
                }
                excelWorksheet.Cells.AutoFitColumns();

                var excelTable = excelWorksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: registersTotalRows, toColumn: properties.Length), "TestRegisters");
                excelTable.ShowHeader = true;

                var excelRange = excelWorksheet.Cells[1, 1, excelWorksheet.Dimension.End.Row, excelWorksheet.Dimension.End.Column];
                excelRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                excelTable.TableStyle = TableStyles.Light21;
                excelTable.ShowTotal = false;

                return await excelPackage.GetAsByteArrayAsync();
            }
        }
    }
}
