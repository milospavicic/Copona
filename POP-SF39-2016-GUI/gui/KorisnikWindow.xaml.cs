using POP_SF39_2016.model;
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
                pbSifra.Focusable = false;
                pbSifra.IsHitTestVisible = false;
            }
            
        }

        private void PopunjavanjePolja(Korisnik korisnik)
        {
            cbPozicija.Items.Add(TipKorisnika.Prodavac);
            cbPozicija.Items.Add(TipKorisnika.Administrator);
            if (korisnik.Ime != "")
            {
                tbIme.Text = korisnik.Ime;
                tbPrezime.Text = korisnik.Prezime;
                tbKorisnickoIme.Text = korisnik.KorisnickoIme;
                pbSifra.Password = korisnik.Lozinka;
                cbPozicija.SelectedItem = korisnik.TipKorisnika;
            }
            else
            {
                tbIme.Text = "";
                tbPrezime.Text = "";
                tbKorisnickoIme.Text = "";
                pbSifra.Password = "";

                cbPozicija.SelectedItem = TipKorisnika.Prodavac;
            }
        }

        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {

            var listaKorisnika = Projekat.Instance.Korisnik;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    var noviKorisnik = new Korisnik()
                    {
                        Id = listaKorisnika.Count + 1,
                        Ime = this.tbIme.Text,
                        Prezime = this.tbPrezime.Text,
                        KorisnickoIme = this.tbKorisnickoIme.Text,
                        Lozinka = this.pbSifra.Password,
                        TipKorisnika = (TipKorisnika)this.cbPozicija.SelectedItem,
                    };
                    listaKorisnika.Add(noviKorisnik);
                    break;
                case Operacija.IZMENA:
                    foreach (Korisnik k in listaKorisnika)
                    {
                        if (k.Id == korisnik.Id)
                        {
                            k.Ime = this.tbIme.Text;
                            k.Prezime = this.tbPrezime.Text;
                            k.KorisnickoIme = this.tbKorisnickoIme.Text;
                            k.Lozinka = this.pbSifra.Password;
                            k.TipKorisnika = (TipKorisnika)this.cbPozicija.SelectedItem;
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
            Projekat.Instance.Korisnik = listaKorisnika;
            this.Close();
        }
            private void ZatvoriWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
