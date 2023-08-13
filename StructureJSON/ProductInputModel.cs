using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.StructureJSON
{
    public class ProductInputModel
    {
        public string num_produit { get; set; }
        public string design { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }
}