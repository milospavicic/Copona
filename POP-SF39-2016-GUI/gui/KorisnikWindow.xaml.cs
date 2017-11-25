using POP_SF39_2016.model;
using POP_SF39_2016.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        public KorisnikWindow(Korisnik korisnik, Operacija operacija)
        {
            InitializeComponent();
            this.korisnik = korisnik;
            this.operacija = operacija;
            PopunjavanjePolja(korisnik);
            
            if (operacija == Operacija.IZMENA) {
                tbKorisnickoIme.Focusable = false;
                tbKorisnickoIme.IsHitTestVisible = false;
                pbSifra.Focusable = false; // Nema bindinga = nema promene?
                pbSifra.IsHitTestVisible = false;
            }  
        }

        private void PopunjavanjePolja(Korisnik korisnik)
        {
            cbPozicija.Items.Add(TipKorisnika.Prodavac);
            cbPozicija.Items.Add(TipKorisnika.Administrator);

            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            cbPozicija.DataContext = korisnik;

            pbSifra.Password = korisnik.Lozinka;

        }

        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            var listaKorisnika = Projekat.Instance.Korisnik;
            korisnik.Lozinka = this.pbSifra.Password; // NEMA BINDINGA ZA PWBOX
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    korisnik.Id = listaKorisnika.Count + 1;
                    listaKorisnika.Add(korisnik);
                    break;
            }
            GenericSerializer.Serialize("korisnici.xml", listaKorisnika);
            this.Close();
        }
        private void ZatvoriWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
