using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Models
{
    public class Product
    {
        [Key]
        [StringLength(10)]
        public string num_produit { get; set; }
        [StringLength(15)]
        public string design { get; set; }
        [StringLength(150)]
        public string? description { get; set; }
        public int quantite { get; set; }
        [StringLength(100)]
        public string? image { get; set; }
        public List<Entree> Entry { get; set; }
        public List<Sortie> Exit { get; set; }

        public List<Stockage> Entrees { get; } = new();
        public List<Destockage> Sorties { get; } = new();        
    }
}