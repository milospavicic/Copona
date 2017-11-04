using POP_SF39_2016.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF39_2016
{
    class Program
    {
        public static List<Namestaj> ListaNamestaja { get; set; }
        public static List<TipNamestaja> ListaTipoviNamestaja { get; set; } = new List<TipNamestaja>();
        public static List<Korisnik> ListaKorisnika { get; set; } = new List<Korisnik>();
        static void Main(string[] args)
        {
            var s1 = new Salon()
            {
                Id = 1,
                Naziv = "Forma FTNale",
                Adresa = "Trg Dositeja Obradovica 6",
                BrRacuna = "81515151-13141",
                Email = "123@ftn.com",
                MaticniBr = 5125151,
                Pib = 15151,
                BrojTelefona = "1235415151",
                WebAdresa = "http://TestSite.jeftino.com"
            };
            var tn1 = new TipNamestaja()
            {
                Id = 1,
                Naziv = "Krevet",
            };
            ListaTipoviNamestaja.Add(tn1);
            var tn2 = new TipNamestaja()
            {
                Id = 2,
                Naziv = "Stolica",
            };
            ListaTipoviNamestaja.Add(tn2);


            var k1 = new Korisnik()
            {
                Id = 1,
                Obrisan = false,
                Ime = "Milos",
                Prezime = "Pavicic",
                KorisnickoIme = "milosp",
                Lozinka = "1234",
                TipKorisnika = TipKorisnika.Prodavac,
            };
        }
    }
}
