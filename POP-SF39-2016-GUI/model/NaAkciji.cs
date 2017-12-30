using POP_SF39_2016_GUI.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF39_2016_GUI.model
{
    public class NaAkciji
    {
        public int IdNaAkciji { get; set; }
        public int IdAkcije { get; set; }
        public int IdNamestaja { get; set; }
        public bool Obrisan { get; set; }
        public string Naziv { get {return NamestajDAO.GetById(IdNamestaja).Naziv; }}
        public string Sifra { get { return NamestajDAO.GetById(IdNamestaja).Sifra; } }
        public double Cena { get { return NamestajDAO.GetById(IdNamestaja).Cena; } }
        public int Popust { get; set; }
        public double CenaSaPopustom
        {
            get
            {
                var tempCena = Cena;
                return tempCena - (tempCena*Popust)/100;
            }
        }
    }
}
