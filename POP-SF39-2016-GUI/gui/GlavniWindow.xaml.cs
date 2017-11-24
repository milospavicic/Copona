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
    public partial class GlavniWindow : Window
    {
        public Korisnik logovaniKorisnik { get; set; } = new Korisnik();

        public enum Opcija
        {
            NAMESTAJ,
            TIPNAMESTAJA,
            AKCIJA,
            DODATNAUSLUGA,
            KORISNIK
        }
        public Opcija izabranaOpcija;
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
            dgTabela.Visibility = Visibility.Hidden;
            //dgTabela.Items.Clear();
        }
        private void AdminEdit()
        {
            dgTabela.Margin = new Thickness(10, 10, 10, 145);
            dgTabela.Visibility = Visibility.Visible;
            borderAddEditDelItem.Visibility = Visibility.Visible;
        }
        //-------------------------------------------------------------------
        public void OsveziPrikazNamestaja()
        {
            dgTabela.ItemsSource = Projekat.Instance.Namestaj;
        }
        public void OsveziPrikazTipaNamestaja()
        {
            dgTabela.ItemsSource = Projekat.Instance.TipNamestaja;
        }
        public void OsveziPrikazKorisnika()
        {
            dgTabela.ItemsSource = Projekat.Instance.Korisnik;
        }
        public void OsveziPrikazAkcija()
        {
            dgTabela.ItemsSource = Projekat.Instance.Akcija;
        }
        private void PrikazNamestajaBasic(object sender, RoutedEventArgs e)
        {
            SkloniSve();
            dgTabela.Visibility = Visibility.Visible;
            dgTabela.Margin = new Thickness(10, 10, 10, 10);
            OsveziPrikazNamestaja();
        }
        //-------------------------------------------------------------------
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
            izabranaOpcija = Opcija.NAMESTAJ;
            SkloniSve();
            AdminEdit();
            OsveziPrikazNamestaja();
        }
        public void PrikazTipovaNamestaja(object sender, RoutedEventArgs e)
        {
            izabranaOpcija = Opcija.TIPNAMESTAJA;
            SkloniSve();
            AdminEdit();
            OsveziPrikazTipaNamestaja();
        }
        public void PrikazKorisnika(object sender, RoutedEventArgs e)
        {
            izabranaOpcija = Opcija.KORISNIK;
            SkloniSve();
            AdminEdit();
            OsveziPrikazKorisnika();
        }
        private void PrikazAkcija(object sender, RoutedEventArgs e)
        {
            izabranaOpcija = Opcija.AKCIJA;
            SkloniSve();
            AdminEdit();
            OsveziPrikazAkcija();
        }

        private void DodajItem(object sender, RoutedEventArgs e)
        {
            switch (izabranaOpcija)
            {
                case Opcija.NAMESTAJ:
                    var noviNamestaj = new Namestaj()
                    {
                        Naziv = ""
                    };

                    var namestajProzor = new NamestajWindow(noviNamestaj, NamestajWindow.Operacija.DODAVANJE);
                    namestajProzor.ShowDialog();
                    OsveziPrikazNamestaja();
                    break;
                case Opcija.TIPNAMESTAJA:
                    var noviTipNamestaja = new TipNamestaja()
                    {
                        Naziv = ""
                    };
                    var tipNamestajaProzor = new TipNamestajaWindow(noviTipNamestaja, TipNamestajaWindow.Operacija.DODAVANJE);
                    tipNamestajaProzor.ShowDialog();
                    OsveziPrikazTipaNamestaja();
                    break;
                case Opcija.KORISNIK:
                    var noviKorisnik = new Korisnik()
                    {
                        Ime = ""
                    };
                    var korisnikProzor = new KorisnikWindow(noviKorisnik, KorisnikWindow.Operacija.DODAVANJE);
                    korisnikProzor.ShowDialog();
                    OsveziPrikazKorisnika();
                    break;
                case Opcija.AKCIJA:
                    var novaAkcija = new Akcija()
                    {
                        Popust = 0
                    };
                    var akcijaProzor = new AkcijaWindow(novaAkcija, AkcijaWindow.Operacija.DODAVANJE);
                    akcijaProzor.ShowDialog();
                    OsveziPrikazAkcija();
                    break;

            }
        }
        private void IzmeniItem(object sender, RoutedEventArgs e)
        {
            if (dgTabela.SelectedItem == null)
            {
                MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            switch (izabranaOpcija)
            {
                case Opcija.NAMESTAJ:
                    var noviNamestaj = (Namestaj)dgTabela.SelectedItem;

                    var namestajProzor = new NamestajWindow(noviNamestaj, NamestajWindow.Operacija.IZMENA);
                    namestajProzor.ShowDialog();
                    OsveziPrikazNamestaja();
                    break;
                case Opcija.TIPNAMESTAJA:
                    var noviTipNamestaja = (TipNamestaja)dgTabela.SelectedItem;
                    var tipNamestajaProzor = new TipNamestajaWindow(noviTipNamestaja, TipNamestajaWindow.Operacija.IZMENA);
                    tipNamestajaProzor.ShowDialog();
                    OsveziPrikazTipaNamestaja();
                    break;
                case Opcija.KORISNIK:
                    var noviKorisnik = (Korisnik)dgTabela.SelectedItem;
                    var korisnikProzor = new KorisnikWindow(noviKorisnik, KorisnikWindow.Operacija.IZMENA);
                    korisnikProzor.ShowDialog();
                    OsveziPrikazKorisnika();
                    break;
                case Opcija.AKCIJA:
                    var novaAkcija = (Akcija)dgTabela.SelectedItem;
                    var akcijaProzor = new AkcijaWindow(novaAkcija, AkcijaWindow.Operacija.IZMENA);
                    akcijaProzor.ShowDialog();
                    OsveziPrikazAkcija();
                    break;
            }
        }
        private void ObrisiItem(object sender, RoutedEventArgs e)
        {
            if (dgTabela.SelectedItem == null)
            {
                MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            switch (izabranaOpcija)
            {
                case Opcija.NAMESTAJ:
                    var izabraniNamestaj = (Namestaj)dgTabela.SelectedItem;

                    List<Namestaj> listaNamestaja = Projekat.Instance.Namestaj;
                    MessageBoxResult namestajMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (namestajMessage == MessageBoxResult.Yes)
                    {
                        foreach (Namestaj namestaj in listaNamestaja)
                            if (namestaj.Id == izabraniNamestaj.Id)
                                namestaj.Obrisan = true;
                        Projekat.Instance.Namestaj = listaNamestaja;
                        OsveziPrikazNamestaja();
                    };
                    break;
                case Opcija.TIPNAMESTAJA:
                    var izabraniTipNamestaja = (TipNamestaja)dgTabela.SelectedItem;

                    List<TipNamestaja> listaTipaNamestaja = Projekat.Instance.TipNamestaja;
                    MessageBoxResult tipNamestajaMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (tipNamestajaMessage == MessageBoxResult.Yes)
                    {
                        foreach (TipNamestaja tipNamestaja in listaTipaNamestaja)
                            if (tipNamestaja.Id == izabraniTipNamestaja.Id)
                                tipNamestaja.Obrisan = true;
                        Projekat.Instance.TipNamestaja = listaTipaNamestaja;
                        OsveziPrikazTipaNamestaja();
                    };
                    break;
                case Opcija.KORISNIK:
                    var izabraniKorisnik = (Korisnik)dgTabela.SelectedItem;

                    List<Korisnik> listaKorisnika = Projekat.Instance.Korisnik;
                    MessageBoxResult korisnikMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (korisnikMessage == MessageBoxResult.Yes)
                    {
                        foreach (Korisnik korisnik in listaKorisnika)
                            if (korisnik.Id == izabraniKorisnik.Id)
                                korisnik.Obrisan = true;
                        Projekat.Instance.Korisnik = listaKorisnika;
                        OsveziPrikazKorisnika();
                    };
                    break;
                case Opcija.AKCIJA:
                    var izabranaAkcija = (Akcija)dgTabela.SelectedItem;

                    List<Akcija> listaAkcija = Projekat.Instance.Akcija;
                    MessageBoxResult akcijaMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (akcijaMessage == MessageBoxResult.Yes)
                    {
                        foreach (Akcija akcija in listaAkcija)
                            if (akcija.Id == izabranaAkcija.Id)
                                akcija.Obrisan = true;
                        Projekat.Instance.Akcija = listaAkcija;
                        OsveziPrikazAkcija();
                    };
                    break;
            }
        }
    }
}