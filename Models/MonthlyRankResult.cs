using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Models
{
    public class MonthlyRankResult
    {
        public string num_produit { get; set; }
        public string design { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int quantite { get; set; }
        public int TotalSorties { get; set; }
        public int Month { get; set; }
    }
}