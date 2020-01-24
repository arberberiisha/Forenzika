using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forenzika.Data
{
    public class Rasti
    {
        public int ID { get; set; }
        public string EmriRastit { get; set; }
        public string Vendi { get; set; }
        public string Pershkrimi { get; set; }
        public Personi Viktimi { get; set; }
        public Personi IAkuzuari { get; set; }
        public Kategoria Kategoria { get; set; }
        public IdentityUser Hetuesi { get; set; }
        public DateTime Data { get; set; }
        public ICollection<Foto> Fotot { get; set; }
    }
}
