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
    public class ManyEntriesController : ControllerBase
    {

        private readonly DataContext _context;
        public ManyEntriesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddEntryWithStockages([FromBody] EntryWithStockagesInput input)
        {
            try
            {
                var stockages = new List<Stockage>();

                foreach (var stockageInput in input.Stockages)
                {
                    var stockage = new Stockage
                    {
                        num_produit = stockageInput.num_produit,
                        num_bon_liv = stockageInput.num_bon_liv,
                        quantite_entree = stockageInput.quantite_entree
                    };

                    stockages.Add(stockage);
                }

                if(stockages.Count != 0){
                    var newEntry = new Entree
                    {
                        num_bon_liv = input.num_bon_liv,
                        DateEntree = input.DateEntree,
                        nom_fournisseur = input.fournisseur,
                        Stockages = stockages
                    };

                    foreach (var stockage in stockages)
                    {
                        var product = await _context.Products.FirstOrDefaultAsync(p => p.num_produit == stockage.num_produit);
                        if (product != null)
                        {
                            product.quantite += stockage.quantite_entree;
                        }
                    }

                    _context.Entrees.Add(newEntry);
                    await _context.SaveChangesAsync();

                    return Ok("Entry and Stockages added successfully.");
                }else{
                    return BadRequest("Nothing to Add");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}

