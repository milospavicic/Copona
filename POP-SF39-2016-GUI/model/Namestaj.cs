using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF39_2016.model
{
    public class Namestaj
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public string Sifra { get; set; }

        public double Cena { get; set; }

        public int BrKomada { get; set; }

        public int? AkcijaId { get; set; }

        public int? TipNamestajaId { get; set; }

        public bool Obrisan { get; set; }
    }
}
