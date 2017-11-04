using POP_SF39_2016.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF39_2016.model
{
    public class Projekat
    {
        public static Projekat Instance { get; } = new Projekat();

        private List<Namestaj> listaNamestaja;

        public List<Namestaj> Namestaj
        {
            get
            {
                this.listaNamestaja = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
                return this.listaNamestaja;
            }
            set
            {
                this.listaNamestaja = value;
                GenericSerializer.Serialize<Namestaj>("namestaj.xml", listaNamestaja);
            }
        }

        private List<TipNamestaja> listaTipNamestaja;

        public List<TipNamestaja> TipNamestaja
        {
            get
            {
                this.listaTipNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tipnamestaja.xml");
                return this.listaTipNamestaja;
            }
            set
            {
                this.listaTipNamestaja = value;
                GenericSerializer.Serialize<TipNamestaja>("tipnamestaja.xml", listaTipNamestaja);
            }
        }
        private List<Korisnik> listaKorisnika;

        public List<Korisnik> Korisnik
        {
            get
            {
                this.listaKorisnika = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
                return this.listaKorisnika;
            }
            set
            {
                this.listaKorisnika = value;
                GenericSerializer.Serialize<Korisnik>("korisnici.xml", listaKorisnika);
            }
        }

    }
}
