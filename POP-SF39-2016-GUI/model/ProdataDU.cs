using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF39_2016_GUI.model
{
    public class ProdataDU
    {
        private DodatnaUsluga dodatnaUsluga;
        public int Id { get; set; }
        public int ProdajaId { get; set; }
        public int DodatnaUslugaId { get; set; }
        public bool Obrisan { get; set; }

        public DodatnaUsluga DodatnaUsluga
        {
            get
            {
                if (dodatnaUsluga == null)
                {
                    dodatnaUsluga = DodatnaUslugaDAO.GetById(DodatnaUslugaId);
                }
                return dodatnaUsluga;
            }
            set
            {
                dodatnaUsluga = value;
                DodatnaUslugaId = dodatnaUsluga.Id;
            }
        }
        public string Kolicina
        {
            get
            {
                return "/";
            }
        }
        public string Naziv
        {
            get
            {
                return DodatnaUsluga.Naziv;
            }
        }
        public double Cena
        {
            get
            {
                return DodatnaUsluga.Cena;
            }
        }
        public double CenaSaPdv
        {
            get
            {
                return DodatnaUsluga.Cena + DodatnaUsluga.Cena * ProdajaNamestaja.PDV;
            }
        }
        public double CenaUkupno
        {
            get
            {
                return DodatnaUsluga.Cena;
            }
        }
        public double CenaUkupnoPDV
        {
            get
            {
                return DodatnaUsluga.Cena + DodatnaUsluga.Cena * ProdajaNamestaja.PDV;
            }
        }
    }
}
