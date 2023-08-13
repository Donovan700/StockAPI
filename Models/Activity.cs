using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StockAPI.Models
{
    public class Activity
    {
        public string NumBon { get; set; }
        public DateTime DateActivite { get; set; }
        public string TypeActivite { get; set; }
        public string NomClientFournisseur { get; set; }        
    }
}