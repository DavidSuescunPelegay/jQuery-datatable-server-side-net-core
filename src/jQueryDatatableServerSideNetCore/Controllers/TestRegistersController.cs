using jQueryDatatableServerSideNetCore.Data;
using jQueryDatatableServerSideNetCore.Models.AuxiliaryModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using jQueryDatatableServerSideNetCore.Extensions;
using jQueryDatatableServerSideNetCore.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using RandomGen;

namespace jQueryDatatableServerSideNetCore.Controllers
{
    public class TestRegistersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestRegistersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TestRegisters
        public async Task<IActionResult> Index()
        {
            await SeedData();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody]DtParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = await _context.TestRegisters.ToListAsync();

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.FirstSurname != null && r.FirstSurname.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.SecondSurname != null && r.SecondSurname.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Street != null && r.Street.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Phone != null && r.Phone.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.ZipCode != null && r.ZipCode.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Country != null && r.Country.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Notes != null && r.Notes.ToUpper().Contains(searchBy.ToUpper()))
                    .ToList();
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc).ToList() : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc).ToList();

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var totalResultsCount = await _context.TestRegisters.CountAsync();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }

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
