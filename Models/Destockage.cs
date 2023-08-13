using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Models
{
    public class Destockage
    {
        [StringLength(10)]
        public string num_produit { get; set; }
        public string num_facture { get; set; }
        public int quantite_sortie { get; set; }
        public Product Products { get; set; }
        public Sortie Sorties { get; set; }
    }
}