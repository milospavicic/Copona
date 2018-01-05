using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System;
using System.Linq;
using System.Windows;

namespace POP_SF39_2016_GUI.gui
{
    public partial class KorisnikWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        private Korisnik korisnik;
        private Operacija operacija;
        private Korisnik logovaniKorisnik;

        public KorisnikWindow(Korisnik korisnik, Operacija operacija, Korisnik logovaniKorisnik)
        {
            InitializeComponent();
            this.korisnik = korisnik;
            this.operacija = operacija;
            this.logovaniKorisnik = logovaniKorisnik;
            PopunjavanjePolja(korisnik);

            if (operacija == Operacija.IZMENA)
            {
                tbKorisnickoIme.Focusable = false;
                tbKorisnickoIme.IsHitTestVisible = false;
            }
        }

        private void PopunjavanjePolja(Korisnik korisnik)
        {
            cbPozicija.ItemsSource = Enum.GetValues(typeof(TipKorisnika)).Cast<TipKorisnika>();
            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            cbPozicija.DataContext = korisnik;
            tbSifra.DataContext = korisnik;
            if (logovaniKorisnik.TipKorisnika == TipKorisnika.Prodavac || logovaniKorisnik.Id == korisnik.Id)
                cbPozicija.IsEnabled = false;
        }

        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    KorisnikDAO.Create(korisnik);
                    break;
                case Operacija.IZMENA:
                    KorisnikDAO.Update(korisnik);
                    break;
            }
            this.Close();
        }
        private void ZatvoriWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
