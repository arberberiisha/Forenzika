using Forenzika.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forenzika.Models
{
    public class Filtrimi
    {
        public Filtrimi()
        {
            rastet = new List<Rasti>();
        }

        public DateTime? fromDate { get; set; }
        
        public DateTime? toDate { get; set; }
        public int? kategoria { get; set; }
        public Guid hetuesi { get; set; }
        public List<Rasti> rastet { get; set; }
    }
}
