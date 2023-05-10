using jQueryDatatableServerSideNetCore.Data;
using jQueryDatatableServerSideNetCore.Extensions;
using jQueryDatatableServerSideNetCore.Models.AuxiliaryModels;
using jQueryDatatableServerSideNetCore.Models.DatabaseModels;
using jQueryDatatableServerSideNetCore.Services.ExportService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RandomGen;

namespace jQueryDatatableServerSideNetCore.Controllers
{
    /// <summary>
    ///   <br />
    /// </summary>
    [Route("[controller]")]
    public class TestRegistersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IExportService _exportService;

        /// <summary>Initializes a new instance of the <see cref="TestRegistersController" /> class.</summary>
        /// <param name="context">The context.</param>
        /// <param name="exportService">The export service.</param>
        public TestRegistersController(ApplicationDbContext context, IExportService exportService)
        {
            _context = context;
            _exportService = exportService;
        }

        // GET: TestRegisters
        /// <summary>Indexes this instance.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await SeedData();

            return View();
        }

        /// <summary>Loads the table.</summary>
        /// <param name="dtParameters">The dt parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost("LoadTable")]
        public async Task<IActionResult> LoadTable([FromBody] DtParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;

            // if we have an empty search then just order the results by Id ascending
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }

            var result = _context.TestRegisters.AsQueryable();

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.FirstSurname != null && r.FirstSurname.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.SecondSurname != null && r.SecondSurname.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Street != null && r.Street.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Phone != null && r.Phone.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.ZipCode != null && r.ZipCode.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Country != null && r.Country.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Notes != null && r.Notes.ToUpper().Contains(searchBy.ToUpper()));
            }

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = await result.CountAsync();
            var totalResultsCount = await _context.TestRegisters.CountAsync();

            return Json(new DtResult<TestRegister>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = await result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToListAsync()
            });
        }

        /// <summary>Exports the table.</summary>
        /// <param name="format">The format.</param>
        /// <param name="dtParametersJson">The dt parameters json.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost("ExportTable")]
        public async Task<IActionResult> ExportTable([FromQuery] string format, [FromForm] string dtParametersJson)
        {
            var dtParameters = new DtParameters();
            if (!string.IsNullOrEmpty(dtParametersJson))
            {
                dtParameters = JsonConvert.DeserializeObject<DtParameters>(dtParametersJson);
            }

            var searchBy = dtParameters.Search?.Value;

            // if we have an empty search then just order the results by Id ascending
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }

            var result = _context.TestRegisters.AsQueryable();

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.FirstSurname != null && r.FirstSurname.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.SecondSurname != null && r.SecondSurname.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Street != null && r.Street.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Phone != null && r.Phone.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.ZipCode != null && r.ZipCode.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Country != null && r.Country.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Notes != null && r.Notes.ToUpper().Contains(searchBy.ToUpper()));
            }

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            var resultList = await result.ToListAsync();

            switch (format)
            {
                case ExportFormat.Excel:
                    return File(
                        await _exportService.ExportToExcel(resultList),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "data.xlsx");

                case ExportFormat.Csv:
                    return File(_exportService.ExportToCsv(resultList),
                        "application/csv",
                        "data.csv");

                case ExportFormat.Html:
                    return File(_exportService.ExportToHtml(resultList),
                        "application/csv",
                        "data.html");

                case ExportFormat.Json:
                    return File(_exportService.ExportToJson(resultList),
                        "application/json",
                        "data.json");

                case ExportFormat.Xml:
                    return File(_exportService.ExportToXml(resultList),
                        "application/xml",
                        "data.xml");

                case ExportFormat.Yaml:
                    return File(_exportService.ExportToYaml(resultList),
                        "application/yaml",
                        "data.yaml");
            }

            return null;
        }

        /// <summary>Seeds the data.</summary>
        [NonAction]
        public async Task SeedData()
        {
            if (!_context.TestRegisters.Any())
            {
                for (var i = 0; i < 1000; i++)
                {
                    await _context.TestRegisters.AddAsync(new TestRegister
                    {
                        Name = i % 2 == 0 ? Gen.Random.Names.Male()() : Gen.Random.Names.Female()(),
                        FirstSurname = Gen.Random.Names.Surname()(),
                        SecondSurname = Gen.Random.Names.Surname()(),
                        Street = Gen.Random.Names.Full()(),
                        Phone = Gen.Random.PhoneNumbers.WithRandomFormat()(),
                        ZipCode = Gen.Random.Numbers.Integers(10000, 99999)().ToString(),
                        Country = Gen.Random.Countries()(),
                        Notes = Gen.Random.Text.Short()(),
                        CreationDate = Gen.Random.Time.Dates(DateTime.Now.AddYears(-100), DateTime.Now)()
                    });
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
