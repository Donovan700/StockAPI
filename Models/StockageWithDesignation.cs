using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Models
{
    public class StockageWithDesignation
    {
        public string num_bon_liv { get; set; }
        public string num_produit { get; set; }
        public int quantite_entree { get; set; }
        public string design { get; set; }
    }
}