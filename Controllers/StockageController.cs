using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Models;
using StockAPI.StructureJSON;

namespace StockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockageController : ControllerBase
    {
        private readonly DataContext _context;
        public StockageController(DataContext context)
        {
            _context = context;   
        }

        [HttpGet]
        public ActionResult<IEnumerable<StockageGet>> GetStockages()
        {
            try
            {
                var stockages = _context.Stockages
                    .Select(s => new StockageGet
                    {
                        num_produit = s.num_produit,
                        num_bon_liv = s.num_bon_liv,
                        quantite_entree = s.quantite_entree
                    })
                    .ToList();

                return Ok(stockages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<StockageGet>> GetStockage(string id)
        {
            var stockage = await _context.Stockages
                .Where(d => d.num_bon_liv == id)
                .Select(d => new StockageGet
                {
                    num_produit = d.num_produit,
                    num_bon_liv = d.num_bon_liv,
                    quantite_entree = d.quantite_entree
                })
                .FirstOrDefaultAsync();

            if (stockage == null)
            {
                return NotFound();
            }

            return Ok(stockage);
        }

        [HttpGet("StockageWithDesignation")]
        public async Task<ActionResult<IEnumerable<StockageWithDesignation>>> GetStockersWithDesignation()
        {
            string sqlQuery = "SELECT s.*, p.design FROM Stockages s, Products p WHERE s.num_produit = p.num_produit";

            List<StockageWithDesignation> stockagesWithDesignation = await _context.StockagesWithDesignation.FromSqlRaw(sqlQuery).ToListAsync();

            return stockagesWithDesignation;
        }
    }
}