using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Models;

namespace StockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatEveryMonth3rdController : ControllerBase
    {
    private readonly DataContext _context;

    public StatEveryMonth3rdController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<MonthlyRankResult>>> GetMonthlyRank()
    {
        var sqlQuery = @"
        WITH MonthlyRank AS (
        SELECT p.num_produit, p.design, p.description, p.image, p.quantite,
            COUNT(ds.num_produit) AS TotalSorties, 
            strftime('%m', s.DateSortie) AS [Month],
            ROW_NUMBER() OVER (PARTITION BY strftime('%m', s.DateSortie) ORDER BY COUNT(ds.num_produit) DESC) AS Rank
        FROM Products p 
        LEFT JOIN Destockages ds ON p.num_produit = ds.num_produit 
        LEFT JOIN Sorties s ON ds.num_facture = s.num_facture
        GROUP BY p.num_produit, p.design, p.description, p.image, p.image, strftime('%m', s.DateSortie)
        )
        SELECT num_produit, design,description, image, quantite, TotalSorties, [Month]
        FROM MonthlyRank
        WHERE Rank <= 3 AND TotalSorties > 0
        ORDER BY [Month], TotalSorties DESC;";

        var monthlyRankResults = await _context.MonthlyRankResults.FromSqlRaw(sqlQuery).ToListAsync();

        return monthlyRankResults;
    }        
    }
}