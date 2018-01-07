using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using MahApps.Metro.Controls;

namespace POP_SF39_2016_GUI.gui
{
    public partial class GlavniWindow : MetroWindow
    {
        ICollectionView view;
        public Korisnik LogovaniKorisnik { get; set; } = new Korisnik();
        public bool Pretrazeno { get; set; } = false;
        public bool Sortirano { get; set; } = false;
        public ObservableCollection<Namestaj> ListaNamestaja = Projekat.Instance.Namestaji;

        public enum Opcija
        {
            NAMESTAJ,
            TIPNAMESTAJA,
            AKCIJA,
            DODATNAUSLUGA,
            KORISNIK,
            PRODAJA
        }
        public enum DoSearch
        {
            Date,
            Other,
            No
        }
        public Opcija izabranaOpcija;
        public GlavniWindow(Korisnik logovaniKorisnik)
        {
            
            InitializeComponent();
            this.LogovaniKorisnik = logovaniKorisnik;
            AkcijaDAO.StillActiveButPastEndDate();
            dgTabela.IsSynchronizedWithCurrentItem = true;
            dgTabela.IsReadOnly = true;
            dgTabela.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgTabela.CanUserSortColumns = false;
        }

        private bool ObrisanFilter(object obj)
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
                return false;
            }
            catch
            {
                return false;
            }
        }
#region Kompletan Prikaz
        private void SkloniSve()
        {
            borderAddEditDelItem.Visibility = Visibility.Hidden;
            dgTabela.Visibility = Visibility.Hidden;
        }
        private void Prikaz()
        {
            dgTabela.Margin = new Thickness(10, 10, 10, 160);
            dgTabela.Visibility = Visibility.Visible;
            borderAddEditDelItem.Visibility = Visibility.Visible;
            if (LogovaniKorisnik.TipKorisnika == TipKorisnika.Administrator)
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
       
        public void OsnovniPrikazTabela()
        {
            cbDatum.IsEnabled = false;
            cbDatum.IsChecked = false;
            btnDetaljnije.IsEnabled = false;
            Pretrazeno = false;
            Sortirano = false;
            switch (izabranaOpcija)
            {
                case Opcija.NAMESTAJ:
                    cbZaSort.ItemsSource = Enum.GetValues(typeof(NamestajDAO.SortBy));
                    cbZaSort.SelectedItem = NamestajDAO.SortBy.Nesortirano;
                    view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaji);
                    view.Filter = ObrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.TIPNAMESTAJA:
                    cbZaSort.ItemsSource = Enum.GetValues(typeof(TipNamestajaDAO.SortBy));
                    cbZaSort.SelectedItem = TipNamestajaDAO.SortBy.Nesortirano;
                    view = CollectionViewSource.GetDefaultView(Projekat.Instance.TipoviNamestaja);
                    view.Filter = ObrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.KORISNIK:
                    cbZaSort.ItemsSource = Enum.GetValues(typeof(KorisnikDAO.SortBy));
                    cbZaSort.SelectedItem = KorisnikDAO.SortBy.Nesortirano;
                    view = CollectionViewSource.GetDefaultView(Projekat.Instance.Korisnici);
                    view.Filter = ObrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.AKCIJA:
                    cbZaSort.ItemsSource = Enum.GetValues(typeof(AkcijaDAO.SortBy));
                    cbZaSort.SelectedItem = AkcijaDAO.SortBy.Nesortirano;
                    view = CollectionViewSource.GetDefaultView(Projekat.Instance.Akcija);
                    view.Filter = ObrisanFilter;
                    dgTabela.ItemsSource = view;
                    cbDatum.IsEnabled = true;
                    btnDetaljnije.IsEnabled = true;
                    break;
                case Opcija.DODATNAUSLUGA:
                    cbZaSort.ItemsSource = Enum.GetValues(typeof(DodatnaUslugaDAO.SortBy));
                    cbZaSort.SelectedItem = DodatnaUslugaDAO.SortBy.Nesortirano;
                    view = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatneUsluge);
                    view.Filter = ObrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.PRODAJA:
                    cbZaSort.ItemsSource = Enum.GetValues(typeof(ProdajaDAO.SortBy));
                    cbZaSort.SelectedItem = ProdajaDAO.SortBy.Nesortirano;
                    view = CollectionViewSource.GetDefaultView(Projekat.Instance.Prodaja);
                    view.Filter = ObrisanFilter;
                    dgTabela.ItemsSource = view;
                    cbDatum.IsEnabled = true;
                    btnDetaljnije.IsEnabled = true;
                    break;
            }
            return;
        }

        private void PrikaziSakrij(object sender, RoutedEventArgs e)
        {

            if (btnNamestaj.Visibility == Visibility.Hidden)
            {
                btnNamestaj.Visibility = Visibility.Visible;
                btnTipNamestaj.Visibility = Visibility.Visible;
                btnAkcije.Visibility = Visibility.Visible;
                btnDodatneUsluge.Visibility = Visibility.Visible;
                btnProdaje.Visibility = Visibility.Visible;
                if(LogovaniKorisnik.TipKorisnika == TipKorisnika.Administrator)
                    btnKorisnici.Visibility = Visibility.Visible;

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
#endregion
#region Dodavanje
        private void DodajItem(object sender, RoutedEventArgs e)
        {
            switch (izabranaOpcija)
            {
                case Opcija.NAMESTAJ:
                    var noviNamestaj = new Namestaj();
                    var namestajProzor = new NamestajWindow(noviNamestaj, NamestajWindow.Operacija.DODAVANJE);
                    namestajProzor.ShowDialog();
                    break;
                case Opcija.TIPNAMESTAJA:
                    var noviTipNamestaja = new TipNamestaja();
                    var tipNamestajaProzor = new TipNamestajaWindow(noviTipNamestaja, TipNamestajaWindow.Operacija.DODAVANJE);
                    tipNamestajaProzor.ShowDialog();
                    break;
                case Opcija.KORISNIK:
                    var noviKorisnik = new Korisnik();
                    var korisnikProzor = new KorisnikWindow(noviKorisnik, KorisnikWindow.Operacija.DODAVANJE, LogovaniKorisnik);
                    korisnikProzor.ShowDialog();
                    break;
                case Opcija.AKCIJA:
                    var novaAkcija = new Akcija();
                    var akcijaProzor = new AkcijaWindow(novaAkcija, AkcijaWindow.Operacija.DODAVANJE);
                    akcijaProzor.ShowDialog();
                    break;
                case Opcija.DODATNAUSLUGA:
                    var novaDodatnaUsluga = new DodatnaUsluga();
                    var dodatnaUslugaProzor = new DodatnaUslugaWindow(novaDodatnaUsluga, DodatnaUslugaWindow.Operacija.DODAVANJE);
                    dodatnaUslugaProzor.ShowDialog();
                    break;
                case Opcija.PRODAJA:
                    ProdajaProzor();
                    break;
            }
            SearchAndOrSort(null,null);
        }
        #endregion
#region Izmena
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
                    var namestajProzor = new NamestajWindow((Namestaj)noviNamestaj.Clone(), NamestajWindow.Operacija.IZMENA);
                    namestajProzor.ShowDialog();
                    break;
                case Opcija.TIPNAMESTAJA:
                    var noviTipNamestaja = (TipNamestaja)dgTabela.SelectedItem;
                    var tipNamestajaProzor = new TipNamestajaWindow((TipNamestaja)noviTipNamestaja.Clone(), TipNamestajaWindow.Operacija.IZMENA);
                    tipNamestajaProzor.ShowDialog();
                    break;
                case Opcija.KORISNIK:
                    var noviKorisnik = (Korisnik)dgTabela.SelectedItem;
                    var korisnikProzor = new KorisnikWindow((Korisnik)noviKorisnik.Clone(), KorisnikWindow.Operacija.IZMENA, LogovaniKorisnik);
                    korisnikProzor.ShowDialog();
                    break;
                case Opcija.AKCIJA:
                    var novaAkcija = (Akcija)dgTabela.SelectedItem;
                    var akcijaProzor = new AkcijaWindow((Akcija)novaAkcija.Clone(), AkcijaWindow.Operacija.IZMENA);
                    akcijaProzor.ShowDialog();
                    break;
                case Opcija.DODATNAUSLUGA:
                    var novaDodatnaUsluga = (DodatnaUsluga)dgTabela.SelectedItem;
                    var dodatnaUslugaProzor = new DodatnaUslugaWindow((DodatnaUsluga)novaDodatnaUsluga.Clone(), DodatnaUslugaWindow.Operacija.IZMENA);
                    dodatnaUslugaProzor.ShowDialog();
                    break;
                case Opcija.PRODAJA:
                    var novaProdaja = (ProdajaNamestaja)dgTabela.SelectedItem;
                    var prodajaWindow = new ProdajaWindow((ProdajaNamestaja)novaProdaja.Clone(), ProdajaWindow.Operacija.IZMENA);
                    prodajaWindow.ShowDialog();
                    break;
            }
            SearchAndOrSort(null, null);
        }
#endregion
#region Brisanje
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

                    MessageBoxResult namestajMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (namestajMessage == MessageBoxResult.Yes)
                    {
                        NamestajDAO.Delete(izabraniNamestaj);
                    };
                    break;
                case Opcija.TIPNAMESTAJA:
                    var izabraniTipNamestaja = (TipNamestaja)dgTabela.SelectedItem;
                    if (izabraniTipNamestaja.Id == 1)
                    {
                        MessageBoxResult poruka = MessageBox.Show("Ovaj tip se ne moze obrisati!", "Upozorenje", MessageBoxButton.OK);
                        return;
                    }
                    MessageBoxResult tipNamestajaMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (tipNamestajaMessage == MessageBoxResult.Yes)
                    {
                        var tempListaZaBrisanje = NamestajDAO.GetAllForTipId(izabraniTipNamestaja.Id);
                        foreach(var item in tempListaZaBrisanje)
                        {
                            item.TipNamestaja = TipNamestajaDAO.GetById(1);
                            NamestajDAO.Update(item);
                        }
                        TipNamestajaDAO.Delete(izabraniTipNamestaja);
                    };
                    break;
                case Opcija.KORISNIK:
                    var izabraniKorisnik = (Korisnik)dgTabela.SelectedItem;
                    
                    MessageBoxResult korisnikMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (korisnikMessage == MessageBoxResult.Yes)
                    {
                        KorisnikDAO.Delete(izabraniKorisnik);
                    };
                    break;
                case Opcija.AKCIJA:
                    var izabranaAkcija = (Akcija)dgTabela.SelectedItem;

                    MessageBoxResult akcijaMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (akcijaMessage == MessageBoxResult.Yes)
                    {
                        var listaZaBrisanje = NaAkcijiDAO.GetAllNAForActionId(izabranaAkcija.Id);
                        foreach(var item in listaZaBrisanje)
                        {
                            NaAkcijiDAO.Delete(item);
                        }
                        AkcijaDAO.Delete(izabranaAkcija);
                    };
                    break;
                case Opcija.DODATNAUSLUGA:
                    var izabranaDodatnaUsluga = (DodatnaUsluga)dgTabela.SelectedItem;

                    MessageBoxResult dodatnaUslugaMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (dodatnaUslugaMessage == MessageBoxResult.Yes)
                    {
                        DodatnaUslugaDAO.Delete(izabranaDodatnaUsluga);
                    };
                    break;
                case Opcija.PRODAJA:
                    var izabranaProdaja = (ProdajaNamestaja)dgTabela.SelectedItem;

                    MessageBoxResult prodajaMessage = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);
                    if (prodajaMessage == MessageBoxResult.Yes)
                    {
                        var listaZaBrisanje = JedinicaProdajeDAO.GetAllForId(izabranaProdaja.Id);
                        foreach (var item in listaZaBrisanje)
                        {
                            var tempNamestaj = NamestajDAO.GetById(item.NamestajId);
                            tempNamestaj.BrKomada += item.Kolicina;
                            NamestajDAO.Update(tempNamestaj);
                            JedinicaProdajeDAO.Delete(item);
                        }
                        ProdajaDAO.Delete(izabranaProdaja);
                    };
                    break;
            }
            view.Refresh();
        }
#endregion
        private void dgTabela_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id")
            {
                e.Cancel = true;
            }
            switch (izabranaOpcija)
            {
                case Opcija.NAMESTAJ:
                    if ( (string)e.Column.Header == "TipNamestajaId" || (string)e.Column.Header == "CenaSaPdv" || (string)e.Column.Header == "Cena")
                    {
                        e.Cancel = true;
                    }
                    if((string)e.Column.Header == "AkcijskaCena")
                    {
                        e.Column.Header = "Cena";
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
                    if ((string)e.Column.Header == "PocetakAkcije" || (string)e.Column.Header == "KrajAkcije")
                    {
                        e.Column.Header += " (MM/DD/YYYY)";
                    }
                    break;
                case Opcija.DODATNAUSLUGA:
                    if ((string)e.Column.Header == "CenaSaPdv" || (string)e.Column.Header == "CenaUkupno" || (string)e.Column.Header == "CenaUkupnoPDV" || (string)e.Column.Header == "Kolicina")
                    {
                        e.Cancel = true;
                    }
                    break;
                case Opcija.PRODAJA:
                    if ((string)e.Column.Header == "ListaJedinicaProdajeId" || (string)e.Column.Header == "DodatneUslugeId" || (string)e.Column.Header == "UkupnaCenaPdv")
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
            var prodajaProzor = new ProdajaWindow(novaProdaja, ProdajaWindow.Operacija.DODAVANJE);
            prodajaProzor.ShowDialog();
        }

        private void DetaljnijeOnClick(object sender, RoutedEventArgs e)
        {
            switch (izabranaOpcija)
            {
                case Opcija.AKCIJA:
                    var izabranaAkcija = (Akcija)dgTabela.SelectedItem;
                    var akcijaDetaljnijeProzor = new DetaljnijeAkcijaWindow(izabranaAkcija);
                    akcijaDetaljnijeProzor.ShowDialog();
                    break;
                case Opcija.PRODAJA:
                    var izabranaProdaja = (ProdajaNamestaja)dgTabela.SelectedItem;
                    var prodajaDetaljnijeProzor = new DetaljnijeProdajaWindow(izabranaProdaja);
                    prodajaDetaljnijeProzor.ShowDialog();
                    break;
            }
        }

        private void Oznaci(object sender, RoutedEventArgs e)
        {
            dpParametar.Visibility = Visibility.Visible;
            tbParametar.Visibility = Visibility.Hidden;
            tbParametar.Text = "";
        }

        private void SkiniOznaku(object sender, RoutedEventArgs e)
        {
            tbParametar.Visibility = Visibility.Visible;
            dpParametar.Visibility = Visibility.Hidden;
        }
        public void SearchAndOrSort(object sender, RoutedEventArgs e)
        {
            DoSearch doSearch; 
            DateTime datumZaPretragu = DateTime.Today;
            if (cbDatum.IsChecked == true)
            {
                doSearch = DoSearch.Date;
                try
                { 
                datumZaPretragu =(DateTime)dpParametar.SelectedDate;
                }
                catch
                {
                    MessageBoxResult poruka = MessageBox.Show("Neodgovarajuci format datuma.\nPokusajte DD/MM/YYYY format.", "Upozorenje", MessageBoxButton.OK);
                    return;
                }
            }
            else
            {
                if (tbParametar.Text == "")
                    doSearch = DoSearch.No;
                else
                    doSearch = DoSearch.Other;

            }
            switch (izabranaOpcija)
            {
                case Opcija.NAMESTAJ:
                    if (cbZaSort.SelectedValue==null)
                        view = CollectionViewSource.GetDefaultView(NamestajDAO.SearchAndOrSort(doSearch, tbParametar.Text, NamestajDAO.SortBy.Nesortirano));
                    else
                        view = CollectionViewSource.GetDefaultView(NamestajDAO.SearchAndOrSort(doSearch, tbParametar.Text, (NamestajDAO.SortBy)cbZaSort.SelectedValue));
                    view.Filter = ObrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.TIPNAMESTAJA:
                    if (cbZaSort.SelectedValue == null)
                        view = CollectionViewSource.GetDefaultView(TipNamestajaDAO.SearchAndOrSort(doSearch, tbParametar.Text, TipNamestajaDAO.SortBy.Nesortirano));
                    else
                        view = CollectionViewSource.GetDefaultView(TipNamestajaDAO.SearchAndOrSort(doSearch, tbParametar.Text, (TipNamestajaDAO.SortBy)cbZaSort.SelectedValue));
                    view.Filter = ObrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.KORISNIK:
                    if (cbZaSort.SelectedValue == null)
                        view = CollectionViewSource.GetDefaultView(KorisnikDAO.SearchAndOrSort(doSearch, tbParametar.Text, KorisnikDAO.SortBy.Nesortirano));
                    else
                        view = CollectionViewSource.GetDefaultView(KorisnikDAO.SearchAndOrSort(doSearch, tbParametar.Text, (KorisnikDAO.SortBy)cbZaSort.SelectedValue));
                    view.Filter = ObrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.DODATNAUSLUGA:
                    if (cbZaSort.SelectedValue == null)
                        view = CollectionViewSource.GetDefaultView(DodatnaUslugaDAO.SearchAndOrSort(doSearch, tbParametar.Text, DodatnaUslugaDAO.SortBy.Nesortirano));
                    else
                        view = CollectionViewSource.GetDefaultView(DodatnaUslugaDAO.SearchAndOrSort(doSearch, tbParametar.Text, (DodatnaUslugaDAO.SortBy)cbZaSort.SelectedValue));
                    view.Filter = ObrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.AKCIJA:
                    if (cbZaSort.SelectedValue == null)
                        view = CollectionViewSource.GetDefaultView(AkcijaDAO.SearchAndOrSort(doSearch, tbParametar.Text, datumZaPretragu, AkcijaDAO.SortBy.Nesortirano));
                    else
                        view = CollectionViewSource.GetDefaultView(AkcijaDAO.SearchAndOrSort(doSearch, tbParametar.Text, datumZaPretragu, (AkcijaDAO.SortBy)cbZaSort.SelectedValue));
                    view.Filter = ObrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
                case Opcija.PRODAJA:
                    if (cbZaSort.SelectedValue == null)
                        view = CollectionViewSource.GetDefaultView(ProdajaDAO.SearchAndOrSort(doSearch, tbParametar.Text, datumZaPretragu, ProdajaDAO.SortBy.Nesortirano));
                    else
                        view = CollectionViewSource.GetDefaultView(ProdajaDAO.SearchAndOrSort(doSearch, tbParametar.Text, datumZaPretragu, (ProdajaDAO.SortBy)cbZaSort.SelectedValue));
                    view.Filter = ObrisanFilter;
                    dgTabela.ItemsSource = view;
                    break;
            }
            if (doSearch != DoSearch.No)
                Pretrazeno = true;
            if (cbZaSort.SelectedValue != null)
                Sortirano = true;
        }
        public void SalonEditView(object sender, RoutedEventArgs e)
        {
            var salonProzor = new SalonWindow(LogovaniKorisnik);
            salonProzor.ShowDialog();
        }
        private void IzmenaPodataka(object sender, RoutedEventArgs e)
        {
            var korisnikProzor = new KorisnikWindow(LogovaniKorisnik, KorisnikWindow.Operacija.IZMENA, LogovaniKorisnik);
            korisnikProzor.ShowDialog();
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
            this.Close();
        }
        private void Izadji(object sender, EventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Da li ste sigurni?", "Izlazak", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            };
        }
    }
}