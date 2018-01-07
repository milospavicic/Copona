using POP_SF39_2016_GUI.DAO;
using POP_SF39_2016_GUI.model;
using System.Collections.ObjectModel;

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
            TipoviNamestaja = TipNamestajaDAO.GetAll();
            Namestaji = NamestajDAO.GetAll();
            ProdateDU = ProdataDodatnaUslugaDAO.GetAll();
            Salon = SalonDAO.GetAll();
            Korisnici = KorisnikDAO.GetAll();
            NaAkciji = NaAkcijiDAO.GetAll();
            JediniceProdaje = JedinicaProdajeDAO.GetAll();
            DodatneUsluge = DodatnaUslugaDAO.GetAll();
            Prodaja = ProdajaDAO.GetAll();
            Akcija = AkcijaDAO.GetAll();

        }
    }
}
