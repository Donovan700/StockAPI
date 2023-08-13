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
    public class ActivityController : ControllerBase
    {
        private readonly DataContext _context;
        public ActivityController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("activities")]
        public IActionResult GetActivities()
        {
            string sqlQuery = @"SELECT numBon, dateActivite, typeActivite, nomClientFournisseur
            FROM (
                SELECT num_bon_liv AS numBon, DateEntree AS dateActivite, 'Entree' AS typeActivite, nom_fournisseur AS nomClientFournisseur
                FROM Entrees
                UNION ALL
                SELECT num_facture AS numBon, DateSortie AS dateActivite, 'Sortie' AS typeActivite, nom_cli AS nomClientFournisseur
                FROM Sorties
            ) AS activites_combinees
            ORDER BY dateActivite DESC
            LIMIT 5;";

            var activities = _context.Activities.FromSqlRaw(sqlQuery).ToList();

            return Ok(activities);
        }
    }
}