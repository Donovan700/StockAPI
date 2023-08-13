using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Models
{
    public class Sortie
    {
        [Key]
        [StringLength(10)]
        public string num_facture { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateSortie { get; set; }
        [StringLength(25)]
        public string? nom_cli { get; set; }
        public List<Destockage> Destockages { get; set; }

        public List<Destockage> Products { get; } = new();         
    }
}