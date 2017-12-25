using POP_SF39_2016.util;
using POP_SF39_2016_GUI.DAO;
using POP_SF39_2016_GUI.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF39_2016.model
{
    public class Projekat
    {
        public static Projekat Instance { get; private set; } = new Projekat();
        public ObservableCollection<TipNamestaja> TipoviNamestaja { get; set; }
        public ObservableCollection<Namestaj> Namestaji { get; set; }
        public ObservableCollection<Korisnik> Korisnici { get; set; }
        public ObservableCollection<Salon> Salon { get; set; }
        public ObservableCollection<Akcija> Akcija { get; set; }
        public ObservableCollection<DodatnaUsluga> DodatneUsluge { get; set; }
        public ObservableCollection<ProdajaNamestaja> Prodaja { get; set; }
        public ObservableCollection<JedinicaProdaje> JediniceProdaje { get; set; }
        public ObservableCollection<NaAkciji> NaAkciji { get; set; }
        public ObservableCollection<ProdataDU> ProdateDU { get; set; }
        private Projekat()
        {

            //Namestaj = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
            //TipNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tipnamestaja.xml");
            //Korisnici = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
            //Akcija = GenericSerializer.Deserialize<Akcija>("akcije.xml");
            //DodatneUsluge = GenericSerializer.Deserialize<DodatnaUsluga>("dodatneusluge.xml");
            //JedinicaProdaje = GenericSerializer.Deserialize<JedinicaProdaje>("jediniceprodaje.xml");
            //Salon = GenericSerializer.Deserialize<Salon>("salon.xml");
            //Prodaja = GenericSerializer.Deserialize<ProdajaNamestaja>("prodajenamestaja.xml");
            ProdateDU = ProdataDodatnaUslugaDAO.GetAll();
            Prodaja = ProdajaDAO.GetAll();
            Salon = SalonDAO.GetAll();
            Korisnici = KorisnikDAO.GetAll();
            NaAkciji = NaAkcijiDAO.GetAll();
            JediniceProdaje = JedinicaProdajeDAO.GetAll();
            Akcija = AkcijaDAO.GetAll();
            DodatneUsluge = DodatnaUslugaDAO.GetAll();
            TipoviNamestaja = TipNamestajaDAO.GetAll();
            Namestaji = NamestajDAO.GetAll();
        }


        /***
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

        private List<Akcija> listaAkcija;

        public List<Akcija> Akcija
        {
            get
            {
                this.listaAkcija = GenericSerializer.Deserialize<Akcija>("akcije.xml");
                return this.listaAkcija;
            }
            set
            {
                this.listaAkcija = value;
                GenericSerializer.Serialize<Akcija>("akcije.xml", listaAkcija);
            }
        }

        private List<DodatnaUsluga> listaDodatnihUsluga;

        public List<DodatnaUsluga> DodatnaUsluga
        {
            get
            {
                this.listaDodatnihUsluga = GenericSerializer.Deserialize<DodatnaUsluga>("dodatneusluge.xml");
                return this.listaDodatnihUsluga;
            }
            set
            {
                this.listaDodatnihUsluga = value;
                GenericSerializer.Serialize<DodatnaUsluga>("dodatneusluge.xml", listaDodatnihUsluga);
            }
        }

        private List<ProdajaNamestaja> listaProdajaNamestaja;

        public List<ProdajaNamestaja> ProdajaNamestaja
        {
            get
            {
                this.listaProdajaNamestaja = GenericSerializer.Deserialize<ProdajaNamestaja>("prodaje.xml");
                return this.listaProdajaNamestaja;
            }
            set
            {
                this.listaProdajaNamestaja = value;
                GenericSerializer.Serialize<ProdajaNamestaja>("prodaje.xml", listaProdajaNamestaja);
            }
        }
        private List<Salon> listaSalona;

        public List<Salon> Salon
        {
            get
            {
                this.listaSalona = GenericSerializer.Deserialize<Salon>("salon.xml");
                return this.listaSalona;
            }
            set
            {
                this.listaSalona = value;
                GenericSerializer.Serialize<Salon>("salon.xml", listaSalona);

            }
        }
       ***/
    }
}
