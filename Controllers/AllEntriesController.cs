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
    public class AllEntriesController : ControllerBase
    {
        private readonly DataContext _context;
        public AllEntriesController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEntries()
        {
            try
            {
                var sqlQuery = @"
                    SELECT
                        p.num_produit AS num_produit,
                        p.design AS design,
                        e.num_bon_liv AS num_bon_liv,
                        e.DateEntree AS dateEntree,
                        e.nom_fournisseur AS nom_fournisseur,
                        s.quantite_entree AS quantite_entree
                    FROM
                        Products p
                    JOIN
                        Stockages s ON p.num_produit = s.num_produit
                    JOIN
                        Entrees e ON s.num_bon_liv = e.num_bon_liv;
                ";

                var result = await _context.Records.FromSqlRaw(sqlQuery).ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }       
    }
}