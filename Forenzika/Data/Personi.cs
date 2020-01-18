using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forenzika.Data
{
    public class Personi
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public int NumriPersonal { get; set; }
        public string Adresa { get; set; }
        public DateTime DataLindjes { get; set; }
    }

}
