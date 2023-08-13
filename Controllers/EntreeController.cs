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
    public class EntreeController : ControllerBase
    {
        private readonly DataContext _context;
        public EntreeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Entree>>> GetEntree(){
            return Ok(await _context.Entrees.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Entree>>> GetEntree(string id){
            var entree = await _context.Entrees.FindAsync(id);
            if(entree == null){
                return BadRequest("Entree not found");
            }
            return Ok(entree);
        }
    }
}