using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF39_2016.model
{
    public class Salon
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Adresa { get; set; }

        public string BrojTelefona { get; set; }

        public string Email { get; set; }

        public string WebAdresa { get; set; }

        public int Pib { get; set; }

        public int MaticniBr { get; set; }

        public string BrRacuna { get; set; }

        public bool Obrisan { get; set; }

    }
}
