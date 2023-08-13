using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Models
{
    public class Record
    {
        public string num_produit { get; set; }
        public string design { get; set; }
        public string num_bon_liv { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateEntree { get; set; }
        public string nom_fournisseur { get; set; }
        public int quantite_entree { get; set; }
    }
}