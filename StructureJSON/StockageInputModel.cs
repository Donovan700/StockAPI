using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.StructureJSON
{
    public class StockageInputModel
    {
        public string num_produit { get; set; }
        public string num_bon_liv { get; set; }
        public int quantite_entree { get; set; }
    }
}