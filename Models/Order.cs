using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace StockAPI.Models
{
    public class Order
    {
        public string num_produit { get; set; }
        public string design { get; set; }
        public string num_facture { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateSortie { get; set; }
        public string nom_client { get; set; }
        public int quantite_sortie { get; set; }
    }
}