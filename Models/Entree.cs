using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StockAPI.StructureJSON;

namespace StockAPI.Models
{
    public class Entree
    {
        [Key]
        [StringLength(10)]
        public string num_bon_liv { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateEntree { get; set; }
        [StringLength(25)]
        public string? nom_fournisseur { get; set; }
        public List<Stockage> Stockages { get; set; }

        public List<Stockage> Products { get; } = new();         
    }
}