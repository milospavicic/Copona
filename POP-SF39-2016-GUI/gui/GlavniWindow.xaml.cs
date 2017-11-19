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
    /// <summary>
    /// Interaction logic for GlavniWindow.xaml
    /// </summary>
    public partial class GlavniWindow : Window
    {
        public Korisnik logovaniKorisnik { get; set; } = new Korisnik();
        public int Opcija { get; set; } = 0;
        public GlavniWindow(Korisnik logovaniKorisnik)
        {
            InitializeComponent();
            this.logovaniKorisnik = logovaniKorisnik;
            if (logovaniKorisnik.TipKorisnika == TipKorisnika.Prodavac)
                btnAdmin.Visibility = Visibility.Hidden;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Da li ste sigurni?", "Izlazak", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                // PROBLEM - ZATVARA I KAD KLIKNES NE/ NACI ALTERNATIVU ZA WINDOW_CLOSING
                Environment.Exit(0);
            };
        }
        //-------------------------------------------------------------------
        private void SkloniSve()
        {
            borderCentarInfo.Visibility = Visibility.Hidden;
            borderCentarEdit.Visibility = Visibility.Hidden;
            borderAddEditDelItem.Visibility = Visibility.Hidden;
            lbNamestaj.Visibility = Visibility.Hidden;
            lbNamestaj.Items.Clear();
        }
        private void AdminEdit()
        {
            lbNamestaj.Margin = new Thickness(10, 10, 10, 155);
            lbNamestaj.Visibility = Visibility.Visible;
            borderAddEditDelItem.Visibility = Visibility.Visible;
        }
        //-------------------------------------------------------------------
        public void OsveziPrikazNamestaja()
        {
            lbNamestaj.Items.Clear();
            List<Namestaj> listaNamestaja = Projekat.Instance.Namestaj;
            foreach (Namestaj namestaj in listaNamestaja)
                if (namestaj.Obrisan == false)
                    lbNamestaj.Items.Add(namestaj);
        }
        public void OsveziPrikazTipaNamestaja()
        {
            lbNamestaj.Items.Clear();
            List<TipNamestaja> listaTipaNamestaja = Projekat.Instance.TipNamestaja;
            foreach (TipNamestaja tipNamestaj in listaTipaNamestaja)
                if (tipNamestaj.Obrisan == false)
                    lbNamestaj.Items.Add(tipNamestaj);
        }
        private void PrikazNamestajaBasic(object sender, RoutedEventArgs e)
        {
            SkloniSve();
            lbNamestaj.Visibility = Visibility.Visible;
            lbNamestaj.Margin = new Thickness(10, 10, 10, 10);
            OsveziPrikazNamestaja();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Da li ste sigurni?", "Izlazak", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                var MainWindow = new MainWindow();
                MainWindow.Show();
                this.Hide();
            };
        }
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Da li ste sigurni?", "Izlazak", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            };
        }

        private void IspisInformmacija(object sender, RoutedEventArgs e)
        {
            SkloniSve();
            borderCentarInfo.Visibility = Visibility.Visible;
            string ispis = "";
            List<Salon> listaSalona = Projekat.Instance.Salon;
            Salon mojSalon = listaSalona[0];
            ispis += "Naziv: " + mojSalon.Naziv + "\n";
            ispis += "Adresa: " + mojSalon.Adresa + "\n";
            ispis += "Broj telefona: " + mojSalon.BrojTelefona + "\n";
            ispis += "Email: " + mojSalon.Email + "\n";
            ispis += "Web adresa: " + mojSalon.WebAdresa + "\n";
            ispis += "Maticni broj: " + mojSalon.MaticniBr + "\n";
            ispis += "Broj Racuna: " + mojSalon.BrRacuna + "\n";
            ispis += "Pib: " + mojSalon.Pib + "\n";

            tbOSalonu.Text = ispis;
        }

        private void PrikaziSakrij(object sender, RoutedEventArgs e)
        {

            if (btnNamestaj.Visibility == Visibility.Hidden)
            {
                btnNamestaj.Visibility = Visibility.Visible;
                btnTipNamestaj.Visibility = Visibility.Visible;
                btnKorisnici.Visibility = Visibility.Visible;
                btnAkcije.Visibility = Visibility.Visible;
                btnDodatneUsluge.Visibility = Visibility.Visible;
            }
            else
            {
                btnNamestaj.Visibility = Visibility.Hidden;
                btnTipNamestaj.Visibility = Visibility.Hidden;
                btnKorisnici.Visibility = Visibility.Hidden;
                btnAkcije.Visibility = Visibility.Hidden;
                btnDodatneUsluge.Visibility = Visibility.Hidden;
            }
        }

        private void IzmenaPodataka(object sender, RoutedEventArgs e)
        {
            SkloniSve();
            borderCentarEdit.Visibility = Visibility.Visible;
            List<Korisnik> listaKorisnika = Projekat.Instance.Korisnik;
            foreach (Korisnik korisnik in listaKorisnika)
            {
                if (korisnik.Id == logovaniKorisnik.Id)
                {
                    tbIme.Text = korisnik.Ime;
                    tbPrezime.Text = korisnik.Prezime;
                    tbKorisnickoIme.Text = korisnik.KorisnickoIme;
                    tbKorisnickoIme.IsReadOnly = true;
                    pbSifra.Password = korisnik.Lozinka;
                    if (korisnik.TipKorisnika == TipKorisnika.Administrator)
                    {
                        tbPozicija.Text = "Administrator";
                    }
                    else
                    {
                        tbPozicija.Text = "Prodavac";
                    }
                    tbPozicija.IsReadOnly = true;
                }
            }
        }

        private void SnimiPodatkeKorisnika(object sender, RoutedEventArgs e)
        {
            if (tbIme.Text == "" || tbPrezime.Text == "" || pbSifra.Password == "")
            {
                MessageBoxResult poruka = MessageBox.Show("Polja ne smeju biti prazna. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }

            MessageBoxResult r = MessageBox.Show("Da li ste sigurni?", "Izlazak", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                List<Korisnik> listaKorisnika = Projekat.Instance.Korisnik;
                foreach (Korisnik korisnik in listaKorisnika)
                {
                    if (korisnik.Id == logovaniKorisnik.Id)
                    {
                        korisnik.Ime = tbIme.Text;
                        korisnik.Prezime = tbPrezime.Text;
                        korisnik.Lozinka = pbSifra.Password;
                    }
                }
                Projekat.Instance.Korisnik = listaKorisnika;
                borderCentarEdit.Visibility = Visibility.Hidden;
            };
        }
        private void IzadjiEdit(object sender, RoutedEventArgs e)
        {
            borderCentarEdit.Visibility = Visibility.Hidden;
        }
        public void PrikazNamestaja(object sender, RoutedEventArgs e)
        {
            Opcija = 1;
            SkloniSve();
            AdminEdit();
            OsveziPrikazNamestaja();
        }
        public void PrikazTipovaNamestaja(object sender, RoutedEventArgs e)
        {
            Opcija = 2;
            SkloniSve();
            AdminEdit();
            OsveziPrikazTipaNamestaja();
        }

        private void DodajItem(object sender, RoutedEventArgs e)
        {
            switch (Opcija)
            {
                case 1:
                    var noviNamestaj = new Namestaj()
                    {
                        Naziv = ""
                    };

                    var namestajProzor = new NamestajWindow(noviNamestaj, NamestajWindow.Operacija.DODAVANJE);
                    namestajProzor.ShowDialog();
                    OsveziPrikazNamestaja();
                    break;
                case 2:
                    var noviTipNamestaja = new TipNamestaja()
                    {
                        Naziv = ""
                    };
                    //var tipNamestajaProzor = new TipNamestaja(noviTipNamestaja, TipNamestajaWindow.Operacija.DODAVANJE);
                    //tipNamestajaProzor.showDialog();
                    OsveziPrikazTipaNamestaja();
                    break;
            }
        }
        private void IzmeniItem(object sender, RoutedEventArgs e)
        {
            if (lbNamestaj.SelectedItem == null)
            {
                MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            switch (Opcija)
            {
                case 1:
                    var noviNamestaj = (Namestaj)lbNamestaj.SelectedItem;

                    var namestajProzor = new NamestajWindow(noviNamestaj, NamestajWindow.Operacija.IZMENA);
                    namestajProzor.ShowDialog();
                    OsveziPrikazNamestaja();
                    break;
            }
        }
        private void ObrisiItem(object sender, RoutedEventArgs e)
        {
            if (lbNamestaj.SelectedItem == null)
            {
                MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            switch (Opcija)
            {
                case 1:
                    var izabraniNamestaj = (Namestaj)lbNamestaj.SelectedItem;

                    List<Namestaj> ListaNamestaja = Projekat.Instance.Namestaj;
                    MessageBoxResult r = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (r == MessageBoxResult.Yes)
                    {
                        foreach (Namestaj namestaj in ListaNamestaja)
                            if (namestaj.Id == izabraniNamestaj.Id)
                                namestaj.Obrisan = true;
                        Projekat.Instance.Namestaj = ListaNamestaja;
                        OsveziPrikazNamestaja();
                    };
                    break;
            }
        }
    }
}