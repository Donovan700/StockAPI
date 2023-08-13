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
    public class ManySortiesController : ControllerBase
    {
        private readonly DataContext _context;
        public ManySortiesController(DataContext context){
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddSortieWithDestockages([FromBody] SortieWithDestockagesInput input){
            try
            {
                var destockages = new List<Destockage>();
                var destockages_failed = new List<Destockage>();
                int count = 0;

                foreach(var destockageInput in input.Destockages)
                {
                    var prod = await _context.Products.FirstOrDefaultAsync(p => p.num_produit == destockageInput.num_produit);

                    if(prod != null)
                    {
                        if(prod.quantite > 0 && prod.quantite > destockageInput.quantite_sortie)
                        {
                            var destockage = new Destockage
                            {
                                num_produit = destockageInput.num_produit,
                                num_facture = destockageInput.num_facture,
                                quantite_sortie = destockageInput.quantite_sortie
                            };

                            destockages.Add(destockage); 

                        }else
                        {
                            count++;
                            var destock_fail = new Destockage
                            {
                                num_produit = destockageInput.num_produit,
                                num_facture = destockageInput.num_facture,
                                quantite_sortie = destockageInput.quantite_sortie                                
                            };

                            destockages_failed.Add(destock_fail);

                        }
                    }
                }
                if(destockages.Count != 0){
                    var newSortie = new Sortie
                        {
                            num_facture = input.num_facture,
                            DateSortie = input.DateSortie,
                            nom_cli = input.client,
                            Destockages = destockages
                        };

                    foreach (var destockage in destockages)
                    {
                        var product = _context.Products.FirstOrDefault(p => p.num_produit == destockage.num_produit);
                        if(product != null){
                            product.quantite -= destockage.quantite_sortie;
                        }
                    }


                    _context.Sorties.Add(newSortie);
                    await _context.SaveChangesAsync();


                    if(count != 0)
                    {
                        string[] excludedProduct = new string[count];
                        int index = 0;
                        foreach(var df in destockages_failed)
                        {
                            excludedProduct[index] = df.num_produit;
                            index++;
                        }
                        string response = $"Sortie and destockages added successfully exept for products: {string.Join(",", excludedProduct)}";

                        return Ok(new { Message = response});
                    }else
                    {
                        return Ok("Sortie and Destockages added successfully");
                    }
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