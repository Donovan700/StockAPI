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
    public class DestockageController : ControllerBase
    {
        private readonly DataContext _context;
        public DestockageController(DataContext context)
        {
            _context = context;   
        }

        [HttpGet]
        public ActionResult<IEnumerable<DestockageGet>> GetDestockages()
        {
            try
            {
                var destockages = _context.Destockages
                    .Select(d => new DestockageGet
                    {
                        num_produit = d.num_produit,
                        num_facture = d.num_facture,
                        quantite_sortie = d.quantite_sortie
                    })
                    .ToList();

                return Ok(destockages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DestockageGet>> GetDestockage(string id)
        {
            var destockage = await _context.Destockages
                .Where(d => d.num_facture == id)
                .Select(d => new DestockageGet
                {
                    num_produit = d.num_produit,
                    num_facture = d.num_facture,
                    quantite_sortie = d.quantite_sortie
                })
                .FirstOrDefaultAsync();

            if (destockage == null)
            {
                return NotFound();
            }

            return Ok(destockage);
        }
    }
}