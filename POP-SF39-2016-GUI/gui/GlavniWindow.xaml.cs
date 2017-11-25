using POP_SF39_2016.model;
using POP_SF39_2016.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public void OsnovniPrikaz()
        {
            switch (izabranaOpcija)
            {
                case Opcija.NAMESTAJ:
                    dgTabela.ItemsSource = Projekat.Instance.Namestaj;
                    break;
                case Opcija.TIPNAMESTAJA:
                    dgTabela.ItemsSource = Projekat.Instance.TipNamestaja;
                    break;
                case Opcija.KORISNIK:
                    dgTabela.ItemsSource = Projekat.Instance.Korisnik;
                    break;
                case Opcija.AKCIJA:
                    dgTabela.ItemsSource = Projekat.Instance.Akcija;
                    break;
                case Opcija.DODATNAUSLUGA:
                    dgTabela.ItemsSource = Projekat.Instance.DodatnaUsluga;
                    break;
            }
        }
        private void PrikazNamestajaBasic(object sender, RoutedEventArgs e)
        {
            SkloniSve();
            dgTabela.Visibility = Visibility.Visible;
            dgTabela.Margin = new Thickness(10, 10, 10, 10);
            OsnovniPrikaz();
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
            ObservableCollection<Salon> listaSalona = Projekat.Instance.Salon;
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
            ObservableCollection<Korisnik> listaKorisnika = Projekat.Instance.Korisnik;
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
                ObservableCollection<Korisnik> listaKorisnika = Projekat.Instance.Korisnik;
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
            OsnovniPrikaz();
        }
        public void PrikazTipovaNamestaja(object sender, RoutedEventArgs e)
        {
            izabranaOpcija = Opcija.TIPNAMESTAJA;
            SkloniSve();
            AdminEdit();
            OsnovniPrikaz();
        }
        public void PrikazKorisnika(object sender, RoutedEventArgs e)
        {
            izabranaOpcija = Opcija.KORISNIK;
            SkloniSve();
            AdminEdit();
            OsnovniPrikaz();
        }
        private void PrikazAkcija(object sender, RoutedEventArgs e)
        {
            izabranaOpcija = Opcija.AKCIJA;
            SkloniSve();
            AdminEdit();
            OsnovniPrikaz();
        }
        private void PrikazDodatnihUsluga(object sender, RoutedEventArgs e)
        {
            izabranaOpcija = Opcija.DODATNAUSLUGA;
            SkloniSve();
            AdminEdit();
            OsnovniPrikaz();
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
                    break;
                case Opcija.TIPNAMESTAJA:
                    var noviTipNamestaja = new TipNamestaja()
                    {
                        Naziv = ""
                    };
                    var tipNamestajaProzor = new TipNamestajaWindow(noviTipNamestaja, TipNamestajaWindow.Operacija.DODAVANJE);
                    tipNamestajaProzor.ShowDialog();
                    break;
                case Opcija.KORISNIK:
                    var noviKorisnik = new Korisnik()
                    {
                        Ime = ""
                    };
                    var korisnikProzor = new KorisnikWindow(noviKorisnik, KorisnikWindow.Operacija.DODAVANJE);
                    korisnikProzor.ShowDialog();
                    break;
                case Opcija.AKCIJA:
                    var novaAkcija = new Akcija()
                    {
                        Popust = 0
                    };
                    var akcijaProzor = new AkcijaWindow(novaAkcija, AkcijaWindow.Operacija.DODAVANJE);
                    akcijaProzor.ShowDialog();
                    break;
                case Opcija.DODATNAUSLUGA:
                    var novaDodatnaUsluga = new DodatnaUsluga()
                    {
                        Naziv = ""
                    };
                    var dodatnaUslugaProzor = new DodatnaUslugaWindow(novaDodatnaUsluga, DodatnaUslugaWindow.Operacija.DODAVANJE);
                    dodatnaUslugaProzor.ShowDialog();
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
                    break;
                case Opcija.TIPNAMESTAJA:
                    var noviTipNamestaja = (TipNamestaja)dgTabela.SelectedItem;
                    var tipNamestajaProzor = new TipNamestajaWindow(noviTipNamestaja, TipNamestajaWindow.Operacija.IZMENA);
                    tipNamestajaProzor.ShowDialog();
                    break;
                case Opcija.KORISNIK:
                    var noviKorisnik = (Korisnik)dgTabela.SelectedItem;
                    var korisnikProzor = new KorisnikWindow(noviKorisnik, KorisnikWindow.Operacija.IZMENA);
                    korisnikProzor.ShowDialog();
                    break;
                case Opcija.AKCIJA:
                    var novaAkcija = (Akcija)dgTabela.SelectedItem;
                    var akcijaProzor = new AkcijaWindow(novaAkcija, AkcijaWindow.Operacija.IZMENA);
                    akcijaProzor.ShowDialog();
                    break;
                case Opcija.DODATNAUSLUGA:
                    var novaDodatnaUsluga = (DodatnaUsluga)dgTabela.SelectedItem;
                    var dodatnaUslugaProzor = new DodatnaUslugaWindow(novaDodatnaUsluga, DodatnaUslugaWindow.Operacija.IZMENA);
                    dodatnaUslugaProzor.ShowDialog();
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

                    ObservableCollection<Namestaj> listaNamestaja = Projekat.Instance.Namestaj;
                    MessageBoxResult namestajMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (namestajMessage == MessageBoxResult.Yes)
                    {
                        foreach (Namestaj namestaj in listaNamestaja)
                            if (namestaj.Id == izabraniNamestaj.Id)
                                namestaj.Obrisan = true;
                        GenericSerializer.Serialize("tipnamestaja.xml", listaNamestaja);
                    };
                    break;
                case Opcija.TIPNAMESTAJA:
                    var izabraniTipNamestaja = (TipNamestaja)dgTabela.SelectedItem;

                    ObservableCollection<TipNamestaja> listaTipaNamestaja = Projekat.Instance.TipNamestaja;
                    MessageBoxResult tipNamestajaMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (tipNamestajaMessage == MessageBoxResult.Yes)
                    {
                        foreach (TipNamestaja tipNamestaja in listaTipaNamestaja)
                            if (tipNamestaja.Id == izabraniTipNamestaja.Id)
                                tipNamestaja.Obrisan = true;
                        GenericSerializer.Serialize("tipnamestaja.xml",listaTipaNamestaja);
                    };
                    break;
                case Opcija.KORISNIK:
                    var izabraniKorisnik = (Korisnik)dgTabela.SelectedItem;

                    ObservableCollection<Korisnik> listaKorisnika = Projekat.Instance.Korisnik;
                    MessageBoxResult korisnikMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (korisnikMessage == MessageBoxResult.Yes)
                    {
                        foreach (Korisnik korisnik in listaKorisnika)
                            if (korisnik.Id == izabraniKorisnik.Id)
                                korisnik.Obrisan = true;
                        GenericSerializer.Serialize("tipnamestaja.xml", listaKorisnika);
                    };
                    break;
                case Opcija.AKCIJA:
                    var izabranaAkcija = (Akcija)dgTabela.SelectedItem;

                    ObservableCollection<Akcija> listaAkcija = Projekat.Instance.Akcija;
                    MessageBoxResult akcijaMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (akcijaMessage == MessageBoxResult.Yes)
                    {
                        foreach (Akcija akcija in listaAkcija)
                            if (akcija.Id == izabranaAkcija.Id)
                                akcija.Obrisan = true;
                        GenericSerializer.Serialize("tipnamestaja.xml", listaAkcija);
                    };
                    break;
                case Opcija.DODATNAUSLUGA:
                    var izabranaDodatnaUsluga = (DodatnaUsluga)dgTabela.SelectedItem;

                    ObservableCollection<DodatnaUsluga> listaDodatnihUsluga = Projekat.Instance.DodatnaUsluga;
                    MessageBoxResult dodatnaUslugaMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (dodatnaUslugaMessage == MessageBoxResult.Yes)
                    {
                        foreach (DodatnaUsluga dodatnaUsluga in listaDodatnihUsluga)
                            if (dodatnaUsluga.Id == izabranaDodatnaUsluga.Id)
                                dodatnaUsluga.Obrisan = true;
                        GenericSerializer.Serialize("dodatneusluge.xml", listaDodatnihUsluga);
                    };
                    break;
            }
        }
    }
}