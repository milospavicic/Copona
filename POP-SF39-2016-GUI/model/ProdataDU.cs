using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF39_2016_GUI.model
{
    public class ProdataDU
    {
        public int Id { get; set; }
        public int ProdajaId { get; set; }
        public int DodatnaUslugaId { get; set; }
        public bool Obrisan { get; set; }
    }
}
