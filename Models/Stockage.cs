using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Models
{
    public class Stockage
    {

        [StringLength(10)]
        public string num_produit { get; set; }
        public string num_bon_liv { get; set; }
        public int quantite_entree { get; set; }
        public Product Products { get; set; }
        public Entree Entrees { get; set; }
    }
}