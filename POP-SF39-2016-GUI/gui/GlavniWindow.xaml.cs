using POP_SF39_2016.model;
using POP_SF39_2016.util;
using POP_SF39_2016_GUI.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        ICollectionView view;
        public Korisnik logovaniKorisnik { get; set; } = new Korisnik();

        public enum Opcija
        {
            NAMESTAJ,
            TIPNAMESTAJA,
            AKCIJA,
            DODATNAUSLUGA,
            KORISNIK,
            PRODAJA
        }
        public Opcija izabranaOpcija;
        public GlavniWindow(Korisnik logovaniKorisnik)
        {
            
            InitializeComponent();
            this.logovaniKorisnik = logovaniKorisnik;
            dgTabela.IsSynchronizedWithCurrentItem = true;
            dgTabela.IsReadOnly = true;
            dgTabela.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private bool obrisanFilter(object obj)
        {
            try
            {
                switch (izabranaOpcija)
                {
                    case Opcija.NAMESTAJ:
                        return !((Namestaj)obj).Obrisan;
                    case Opcija.TIPNAMESTAJA:
                        return !((TipNamestaja)obj).Obrisan;
                    case Opcija.KORISNIK:
                        return !((Korisnik)obj).Obrisan;
                    case Opcija.AKCIJA:
                        return !((Akcija)obj).Obrisan;
                    case Opcija.DODATNAUSLUGA:
                        return !((DodatnaUsluga)obj).Obrisan;
                    case Opcija.PRODAJA:
                        return !((ProdajaNamestaja)obj).Obrisan;
                }
            }
            catch
            {
                Console.WriteLine("PUCA PRETVARANJE>>>>>>>>>");
            }

            return false;
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
        }
        private void Prikaz()
        {
            dgTabela.Margin = new Thickness(10, 10, 10, 160);
            dgTabela.Visibility = Visibility.Visible;
            borderAddEditDelItem.Visibility = Visibility.Visible;
            if (logovaniKorisnik.TipKorisnika == TipKorisnika.Administrator)
            {
                btnDodaj.Visibility = Visibility.Visible;
                btnIzmeni.Visibility = Visibility.Visible;
                btnObrisi.Visibility = Visibility.Visible;
            }
            else
            {
                btnDodaj.Visibility = Visibility.Hidden;
                btnIzmeni.Visibility = Visibility.Hidden;
                btnObrisi.Visibility = Visibility.Hidden;
            }
        }
        //-------------------------------------------------------------------
        public void OsnovniPrikazTabela()
        {
            switch (izabranaOpcija)
            {
                case Opcija.NAMESTAJ:
                    view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaji);
                    view.Filter = obrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.TIPNAMESTAJA:
                    view = CollectionViewSource.GetDefaultView(Projekat.Instance.TipoviNamestaja);
                    view.Filter = obrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.KORISNIK:
                    view = CollectionViewSource.GetDefaultView(Projekat.Instance.Korisnici);
                    view.Filter = obrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.AKCIJA:
                    view = CollectionViewSource.GetDefaultView(Projekat.Instance.Akcija);
                    view.Filter = obrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.DODATNAUSLUGA:
                    view = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatneUsluge);
                    view.Filter = obrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.PRODAJA:
                    view = CollectionViewSource.GetDefaultView(Projekat.Instance.Prodaja);
                    view.Filter = obrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
            }
            return;
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
                btnProdaje.Visibility = Visibility.Visible;
            }
            else
            {
                btnNamestaj.Visibility = Visibility.Hidden;
                btnTipNamestaj.Visibility = Visibility.Hidden;
                btnKorisnici.Visibility = Visibility.Hidden;
                btnAkcije.Visibility = Visibility.Hidden;
                btnDodatneUsluge.Visibility = Visibility.Hidden;
                btnProdaje.Visibility = Visibility.Hidden;
            }
        }

        private void IzmenaPodataka(object sender, RoutedEventArgs e)
        {
            SkloniSve();
            borderCentarEdit.Visibility = Visibility.Visible;
            ObservableCollection<Korisnik> listaKorisnika = Projekat.Instance.Korisnici;
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
                ObservableCollection<Korisnik> listaKorisnika = Projekat.Instance.Korisnici;
                foreach (Korisnik korisnik in listaKorisnika)
                {
                    if (korisnik.Id == logovaniKorisnik.Id)
                    {
                        korisnik.Ime = tbIme.Text;
                        korisnik.Prezime = tbPrezime.Text;
                        korisnik.Lozinka = pbSifra.Password;
                    }
                }
                Projekat.Instance.Korisnici = listaKorisnika;
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
            Prikaz();
            OsnovniPrikazTabela();
        }
        public void PrikazTipovaNamestaja(object sender, RoutedEventArgs e)
        {
            izabranaOpcija = Opcija.TIPNAMESTAJA;
            SkloniSve();
            Prikaz();
            OsnovniPrikazTabela();
        }
        public void PrikazKorisnika(object sender, RoutedEventArgs e)
        {
            izabranaOpcija = Opcija.KORISNIK;
            SkloniSve();
            Prikaz();
            OsnovniPrikazTabela();
        }
        private void PrikazAkcija(object sender, RoutedEventArgs e)
        {
            izabranaOpcija = Opcija.AKCIJA;
            SkloniSve();
            Prikaz();
            OsnovniPrikazTabela();
        }
        private void PrikazDodatnihUsluga(object sender, RoutedEventArgs e)
        {
            izabranaOpcija = Opcija.DODATNAUSLUGA;
            SkloniSve();
            Prikaz();
            OsnovniPrikazTabela();
        }
        private void PrikazProdaja(object sender, RoutedEventArgs e)
        {
            izabranaOpcija = Opcija.PRODAJA;
            SkloniSve();
            Prikaz();
            OsnovniPrikazTabela();
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
                    var namestajProzor = new NamestajWindow(noviNamestaj, 0, NamestajWindow.Operacija.DODAVANJE);
                    namestajProzor.ShowDialog();
                    break;
                case Opcija.TIPNAMESTAJA:
                    var noviTipNamestaja = new TipNamestaja()
                    {
                        Naziv = ""
                    };
                    var tipNamestajaProzor = new TipNamestajaWindow(noviTipNamestaja, 0, TipNamestajaWindow.Operacija.DODAVANJE);
                    tipNamestajaProzor.ShowDialog();
                    break;
                case Opcija.KORISNIK:
                    var noviKorisnik = new Korisnik()
                    {
                        Ime = ""
                    };
                    var korisnikProzor = new KorisnikWindow(noviKorisnik, 0, KorisnikWindow.Operacija.DODAVANJE);
                    korisnikProzor.ShowDialog();
                    break;
                case Opcija.AKCIJA:
                    var novaAkcija = new Akcija()
                    {
                        Obrisan = false
                    };
                    var akcijaProzor = new AkcijaWindow(novaAkcija, 0, AkcijaWindow.Operacija.DODAVANJE);
                    akcijaProzor.ShowDialog();
                    break;
                case Opcija.DODATNAUSLUGA:
                    var novaDodatnaUsluga = new DodatnaUsluga()
                    {
                        Naziv = ""
                    };
                    var dodatnaUslugaProzor = new DodatnaUslugaWindow(novaDodatnaUsluga, 0, DodatnaUslugaWindow.Operacija.DODAVANJE);
                    dodatnaUslugaProzor.ShowDialog();
                    break;
                case Opcija.PRODAJA:
                    ProdajaProzor();
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
                    var namestajProzor = new NamestajWindow((Namestaj)noviNamestaj.Clone(),Projekat.Instance.Namestaji.IndexOf(noviNamestaj), NamestajWindow.Operacija.IZMENA);
                    namestajProzor.ShowDialog();
                    break;
                case Opcija.TIPNAMESTAJA:
                    var noviTipNamestaja = (TipNamestaja)dgTabela.SelectedItem;
                    var tipNamestajaProzor = new TipNamestajaWindow((TipNamestaja)noviTipNamestaja.Clone(), Projekat.Instance.TipoviNamestaja.IndexOf(noviTipNamestaja), TipNamestajaWindow.Operacija.IZMENA);
                    tipNamestajaProzor.ShowDialog();
                    break;
                case Opcija.KORISNIK:
                    var noviKorisnik = (Korisnik)dgTabela.SelectedItem;
                    var korisnikProzor = new KorisnikWindow((Korisnik)noviKorisnik.Clone(),Projekat.Instance.Korisnici.IndexOf(noviKorisnik), KorisnikWindow.Operacija.IZMENA);
                    korisnikProzor.ShowDialog();
                    break;
                case Opcija.AKCIJA:
                    var novaAkcija = (Akcija)dgTabela.SelectedItem;
                    var akcijaProzor = new AkcijaWindow((Akcija)novaAkcija.Clone(),Projekat.Instance.Akcija.IndexOf(novaAkcija), AkcijaWindow.Operacija.IZMENA);
                    akcijaProzor.ShowDialog();
                    break;
                case Opcija.DODATNAUSLUGA:
                    var novaDodatnaUsluga = (DodatnaUsluga)dgTabela.SelectedItem;
                    var dodatnaUslugaProzor = new DodatnaUslugaWindow((DodatnaUsluga)novaDodatnaUsluga.Clone(),Projekat.Instance.DodatneUsluge.IndexOf(novaDodatnaUsluga), DodatnaUslugaWindow.Operacija.IZMENA);
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

                    ObservableCollection<Namestaj> listaNamestaja = Projekat.Instance.Namestaji;
                    MessageBoxResult namestajMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (namestajMessage == MessageBoxResult.Yes)
                    {
                        NamestajDAO.Delete(izabraniNamestaj);
                    };
                    break;
                case Opcija.TIPNAMESTAJA:
                    var izabraniTipNamestaja = (TipNamestaja)dgTabela.SelectedItem;

                    ObservableCollection<TipNamestaja> listaTipaNamestaja = Projekat.Instance.TipoviNamestaja;
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

                    ObservableCollection<Korisnik> listaKorisnika = Projekat.Instance.Korisnici;
                    MessageBoxResult korisnikMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (korisnikMessage == MessageBoxResult.Yes)
                    {
                        foreach (Korisnik korisnik in listaKorisnika)
                            if (korisnik.Id == izabraniKorisnik.Id)
                                korisnik.Obrisan = true;
                        GenericSerializer.Serialize("korisnici.xml", listaKorisnika);
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
                        GenericSerializer.Serialize("akcije.xml", listaAkcija);
                    };
                    break;
                case Opcija.DODATNAUSLUGA:
                    var izabranaDodatnaUsluga = (DodatnaUsluga)dgTabela.SelectedItem;

                    ObservableCollection<DodatnaUsluga> listaDodatnihUsluga = Projekat.Instance.DodatneUsluge;
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
            view.Refresh();
        }

        private void dgTabela_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id")
            {
                e.Cancel = true;
            }
            switch (izabranaOpcija)
            {
                case Opcija.NAMESTAJ:
                    if ( (string)e.Column.Header == "TipNamestajaId" || (string)e.Column.Header == "CenaSaPdv" )
                    {
                        e.Cancel = true;
                    }
                    break;
                case Opcija.KORISNIK:
                    if ((string)e.Column.Header == "Lozinka")
                    {
                        e.Cancel = true;
                    }
                    break;
                case Opcija.AKCIJA:
                    if ((string)e.Column.Header == "NamestajId")
                    {
                        e.Cancel = true;
                    }
                    break;
                case Opcija.DODATNAUSLUGA:
                    if ((string)e.Column.Header == "CenaSaPdv" || (string)e.Column.Header == "CenaUkupno" || (string)e.Column.Header == "CenaUkupnoPDV" || (string)e.Column.Header == "Kolicina")
                    {
                        e.Cancel = true;
                    }
                    break;
                case Opcija.PRODAJA:
                    if ((string)e.Column.Header == "ListaJedinicaProdajeId" || (string)e.Column.Header == "DodatneUslugeId")
                    {
                        e.Cancel = true;
                    }
                    break;
            }
        }

        private void ProdajaBtnOnClick(object sender, RoutedEventArgs e)
        {
            ProdajaProzor();
        }
        private void ProdajaProzor()
        {
            var novaProdaja = new ProdajaNamestaja();
            var prodajaProzor = new ProdajaWindow(novaProdaja, 0, ProdajaWindow.Operacija.DODAVANJE);
            prodajaProzor.ShowDialog();
        }

        private void DetaljnijeOnClick(object sender, RoutedEventArgs e)
        {
            switch (izabranaOpcija)
            {
                case Opcija.AKCIJA:
                    var izabranaAkcija = (Akcija)dgTabela.SelectedItem;
                    var akcijaDetaljnijeProzor = new DetaljnijeAkcijaWindow(izabranaAkcija.Id);
                    akcijaDetaljnijeProzor.ShowDialog();
                    break;
            }
        }
    }
}