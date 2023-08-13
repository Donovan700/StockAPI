using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.StructureJSON
{
    public class DestockageGet
    {
        public string num_produit { get; set; }
        public string num_facture { get; set; }
        public int quantite_sortie { get; set; }
    }
}