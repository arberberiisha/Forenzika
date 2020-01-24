using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forenzika.Models
{
    public class RastiViewModel
    {
        public int ID { get; set; }
        public string EmriRastit { get; set; }
        public string Vendi { get; set; }
        public string Pershkrimi { get; set; }
        public int Viktimi { get; set; }
        public int IAkuzuari { get; set; }
        public int Kategoria { get; set; }
        public DateTime Data { get; set; }
    }
}
