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
    public class AllSortiesController : ControllerBase
    {
        private readonly DataContext _context;
        public AllSortiesController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllSorties()
        {
            try
            {
                var sqlQuery = @"
                    SELECT
                        p.num_produit AS num_produit,
                        p.design AS design,
                        s.num_facture AS num_facture,
                        s.DateSortie AS dateSortie,
                        s.nom_cli AS nom_client,
                        d.quantite_sortie AS quantite_sortie
                    FROM
                        Products p
                    JOIN
                        Destockages d ON p.num_produit = d.num_produit
                    JOIN
                        Sorties s ON d.num_facture = s.num_facture;
                ";

                var result = await _context.Orders.FromSqlRaw(sqlQuery).ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }       
    }
}