using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using StockAPI.Data;
using StockAPI.Models;

namespace StockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatMonthController : ControllerBase
    {
        private readonly DataContext _context;

        public StatMonthController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatisticsByMonthAsync()
        {
            try
            {
                var entriesStatistics = await _context.Entrees
                    .GroupBy(e => new { Month = e.DateEntree.Month, Year = e.DateEntree.Year })
                    .Select(g => new StatisticsResult
                    {
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        NbrEntree = g.Count()
                    })
                    .ToListAsync();

                var exitsStatistics = await _context.Sorties
                    .GroupBy(s => new { Month = s.DateSortie.Month, Year = s.DateSortie.Year })
                    .Select(g => new StatisticsResult
                    {
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        NbrSortie = g.Count()
                    })
                    .ToListAsync();

                var combinedStatistics = entriesStatistics
                    .Concat(exitsStatistics)
                    .GroupBy(s => new { s.Month, s.Year })
                    .Select(g => new StatisticsResult
                    {
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        NbrEntree = g.Sum(s => s.NbrEntree),
                        NbrSortie = g.Sum(s => s.NbrSortie)
                    })
                    .OrderBy(s => s.Year)
                    .ThenBy(s => s.Month)
                    .ToList();

                return Ok(combinedStatistics);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
