using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using POP_SF39_2016_GUI.model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace POP_SF39_2016_GUI.gui
{
    public partial class ProdajaWindow : MetroWindow
    {
        public ObservableCollection<Object> Korpa { get; set; } = new ObservableCollection<Object>();
        public ObservableCollection<DodatnaUsluga> ListaDU { get; set; } = new ObservableCollection<DodatnaUsluga>();
        ICollectionView view;
        bool prodajaUspesna = false;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public enum RadSa
        {
            NAMESTAJ,
            DODATNAUSLUGA
        }

        private ProdajaNamestaja prodajaNamestaja;
        private Operacija operacija;
        private RadSa radSa;

        public ProdajaWindow(ProdajaNamestaja prodajaNamestaja, Operacija operacija)
        {
            InitializeComponent();
            this.operacija = operacija;
            this.prodajaNamestaja = prodajaNamestaja;
            if (operacija == Operacija.DODAVANJE)
                this.Title += " - Dodavanje";
            else
                this.Title += " - Izmena";
            PopuniTabele();
        }

        private void NamestajSelected(object sender, RoutedEventArgs e)
        {
            var tab = sender as TabItem;
            if (tab != null)
            {
                radSa = RadSa.NAMESTAJ;
                dgProdajaDU.SelectedItem = null;
            }
        }
        private void DodatnaUslugaSelected(object sender, RoutedEventArgs e)
        {
            var tab = sender as TabItem;
            if (tab != null)
            {
                radSa = RadSa.DODATNAUSLUGA;
                dgProdajaN.SelectedItem = null;
            }
        }

        private bool obrisanFilterN(object obj)
        {
            return !((Namestaj)obj).Obrisan;
        }
        private bool obrisanFilterDU(object obj)
        {
            return !((DodatnaUsluga)obj).Obrisan;
        }
        private void PopuniTabele()
        {
            dgProdajaDU.SelectedIndex = 0;
            tbUkupnaCenaPdv.DataContext = prodajaNamestaja;
            tbUkupnaCena.DataContext = prodajaNamestaja;
            tbDatum.DataContext = prodajaNamestaja;
            tbKupac.DataContext = prodajaNamestaja;
            tbKupac.MaxLength = 30;
            dgRacun.ItemsSource = Korpa;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    prodajaNamestaja.DatumProdaje = DateTime.Today;
                    ListaDU = DodatnaUslugaDAO.GetAllNotSoldForId(0);
                    break;
                case Operacija.IZMENA:
                    var temp = prodajaNamestaja.UkupnaCena;
                    prodajaNamestaja.UkupnaCenaPdv = temp + temp * ProdajaNamestaja.PDV;
                    JedinicaProdajeDAO.GetAllForId(prodajaNamestaja.Id).ToList().ForEach(x => { Korpa.Add(x); });
                    ProdataDodatnaUslugaDAO.GetAllForId(prodajaNamestaja.Id).ToList().ForEach(x => { Korpa.Add(x); });
                    ListaDU = DodatnaUslugaDAO.GetAllNotSoldForId(prodajaNamestaja.Id);
                    break;
            }
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaji);
            view.Filter = obrisanFilterN;
            dgProdajaN.ItemsSource = view;
            dgProdajaN.SelectedIndex = 0;
            view = CollectionViewSource.GetDefaultView(ListaDU);
            view.Filter = obrisanFilterDU;
            dgProdajaDU.ItemsSource = view;
            //---------------------------------
            dgProdajaN.IsReadOnly = true;
            dgProdajaN.IsSynchronizedWithCurrentItem = true;
            dgProdajaN.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgProdajaN.CanUserSortColumns = false;
            dgProdajaN.CanUserAddRows = false;
            dgProdajaN.CanUserDeleteRows = false;
            //--
            dgProdajaDU.IsReadOnly = true;
            dgProdajaDU.IsSynchronizedWithCurrentItem = true;
            dgProdajaDU.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgProdajaDU.CanUserSortColumns = false;
            dgProdajaDU.CanUserAddRows = false;
            dgProdajaDU.CanUserDeleteRows = false;
            //--
            dgRacun.IsReadOnly = true;
            dgRacun.IsSynchronizedWithCurrentItem = true;
            dgRacun.CanUserSortColumns = false;
            dgRacun.CanUserAddRows = false;
            dgRacun.CanUserDeleteRows = false;
        }

        private void dgProdajaN_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "TipNamestajaId" || (string)e.Column.Header == "Cena" || (string)e.Column.Header == "BrKomadaX")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "AkcijskaCena")
            {
                e.Column.Header = "Cena";
            }
        }
        private void dgProdajaDU_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "Kolicina")
            {
                e.Cancel = true;
            }
        }

        private void btnProdajFinal_Click(object sender, RoutedEventArgs e)
        {
            if (ForceValidation() == true)
                return;
            if (Korpa.Count() != 0)
            {
                bool postojiJP = false;
                foreach (var item in Korpa)
                    if (item.GetType() == typeof(JedinicaProdaje))
                        postojiJP = true;

                if(postojiJP == false)
                {
                    ErrorMessagePrint("Ne mozete prodati samo dodatne usluge.", "Upozorenje");
                    return;
                }
            }
            else
            {
                ErrorMessagePrint("Korpa je prazna.", "Upozorenje");
                return;
            }
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    prodajaNamestaja.BrRacuna = (new Random().Next(1, int.MaxValue)).ToString();
                    var tempProdaja = ProdajaDAO.Create(prodajaNamestaja);
                    foreach (var item in Korpa)
                    {
                        if (item.GetType() == typeof(JedinicaProdaje))
                        {
                            var tempItem = (JedinicaProdaje)item;
                            foreach (Namestaj namestaj in Projekat.Instance.Namestaji)
                            {
                                if (namestaj.Id == tempItem.NamestajId)
                                {   
                                    NamestajDAO.Update(namestaj);
                                    break;
                                } 
                            }
                            tempItem.ProdajaId = tempProdaja.Id;
                            JedinicaProdajeDAO.Create(tempItem);
                        }
                        else
                        {
                            var tempItem = (ProdataDU)item;
                            tempItem.ProdajaId = tempProdaja.Id;
                            ProdataDodatnaUslugaDAO.Create(tempItem);
                        }
                    }
                    prodajaUspesna = true;
                    this.Close();
                    break;
                case Operacija.IZMENA:
                    ProdajaDAO.Update(prodajaNamestaja);
                    var listaJPZaBrisanje = JedinicaProdajeDAO.GetAllForId(prodajaNamestaja.Id);
                    var listaDUZaBrisanje = ProdataDodatnaUslugaDAO.GetAllForId(prodajaNamestaja.Id);
                    foreach (var item in Korpa)
                    {
                        ////////////////
                        if (item.GetType() == typeof(JedinicaProdaje))
                        {
                            var jpFromKorpa = (JedinicaProdaje)item;
                            bool postoji = false;
                            foreach (var jpFromProdajaEdit in JedinicaProdajeDAO.GetAllForId(prodajaNamestaja.Id))
                            {
                                if (jpFromProdajaEdit.Id == jpFromKorpa.Id)
                                {
                                    postoji = true;
                                    listaJPZaBrisanje.ToList().ForEach(x => { if (x.Id == jpFromProdajaEdit.Id) listaJPZaBrisanje.Remove(x); return; });
                                    //izbaci ga iz liste zato sto je vec postojao u prodaji. ALI PROVERI KOLICINU, ako je promenjena updejtuj namestaj.
                                    if (jpFromKorpa.Kolicina != jpFromProdajaEdit.Kolicina)
                                    {
                                        JedinicaProdajeDAO.Update(jpFromKorpa);
                                        NamestajDAO.Update(Namestaj.GetById(jpFromKorpa.NamestajId));
                                    }
                                    break;
                                }
                            }
                            if (postoji == false)
                            {
                                jpFromKorpa.ProdajaId = prodajaNamestaja.Id;
                                JedinicaProdajeDAO.Create(jpFromKorpa);
                                NamestajDAO.Update(Namestaj.GetById(jpFromKorpa.NamestajId));
                            }
                        }///////////
                        else
                        {
                            var duFromKorpa = (ProdataDU)item;
                            bool postoji = false;
                            foreach (ProdataDU duFromProdajaEdit in ProdataDodatnaUslugaDAO.GetAllForId(prodajaNamestaja.Id))
                            {
                                if (duFromProdajaEdit.Id == duFromKorpa.Id)
                                {
                                    postoji = true;
                                    listaDUZaBrisanje.ToList().ForEach(x => { if (x.Id == duFromProdajaEdit.Id) listaDUZaBrisanje.Remove(x); return; });
                                    break;
                                }
                            }
                            if (postoji == false)
                            {
                                duFromKorpa.ProdajaId = prodajaNamestaja.Id;
                                ProdataDodatnaUslugaDAO.Create(duFromKorpa);
                            }
                        }
                    }
                    foreach (JedinicaProdaje jpZaObrisati in listaJPZaBrisanje)
                    {
                        JedinicaProdajeDAO.Delete(jpZaObrisati);
                        var tempNamestaj = Namestaj.GetById(jpZaObrisati.NamestajId);
                        tempNamestaj.BrKomada += jpZaObrisati.Kolicina;
                        NamestajDAO.Update(tempNamestaj);
                    }
                    foreach (ProdataDU duZaObrisati in listaDUZaBrisanje)
                    {
                        ProdataDodatnaUslugaDAO.Delete(duZaObrisati);
                    }
                    prodajaUspesna = true;
                    this.Close();
                    break;
            }
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            double tempCena = 0;
            switch (radSa)
            {
                case RadSa.NAMESTAJ:
                    if (dgProdajaN.SelectedItem == null)
                    {
                        ErrorMessagePrint("Niste nista izabrali. ", "Upozorenje");
                        return;
                    }
                    Namestaj selektovaniNamestaj = (Namestaj)dgProdajaN.SelectedItem;
                    if (selektovaniNamestaj.BrKomada == 0)
                    {
                        ErrorMessagePrint("Namestaj je rasprodat.", "Upozorenje");
                        return;
                    }
                    bool postoji = false;
                    foreach (var item in Korpa)
                    {
                        if (item.GetType() == typeof(JedinicaProdaje))
                        {
                            var tempJP = (JedinicaProdaje)item;
                            if (tempJP.NamestajId == selektovaniNamestaj.Id)
                            {
                                var brKomadaUkupno = selektovaniNamestaj.BrKomada;

                                brKomadaUkupno = tempJP.Kolicina + selektovaniNamestaj.BrKomada;

                                var unosKolicine = new KolicinaWindow(brKomadaUkupno, tempJP.Kolicina);
                                unosKolicine.ShowDialog();
                                if (unosKolicine.DialogResult == true)
                                {
                                    tempJP.Kolicina += unosKolicine.Kolicina;
                                    selektovaniNamestaj.BrKomada -= unosKolicine.Kolicina;
                                    tempCena = selektovaniNamestaj.AkcijskaCena * unosKolicine.Kolicina;
                                    postoji = true;
                                    break;
                                }
                                else { return; }
                            }
                        }
                    }
                    if (postoji == false)
                    {
                        var unosKolicine = new KolicinaWindow(selektovaniNamestaj.BrKomada, 0);
                        unosKolicine.ShowDialog();
                        if (unosKolicine.DialogResult == true)
                        {
                            JedinicaProdaje jd = new JedinicaProdaje
                            {
                                NamestajId = selektovaniNamestaj.Id,
                                Kolicina = unosKolicine.Kolicina,
                            };
                            Korpa.Add(jd);
                            selektovaniNamestaj.BrKomada -= unosKolicine.Kolicina;
                            tempCena = selektovaniNamestaj.AkcijskaCena * unosKolicine.Kolicina;
                        }
                        else { return; }
                    }
                    prodajaNamestaja.UkupnaCena += tempCena;
                    prodajaNamestaja.UkupnaCenaPdv += tempCena + tempCena * ProdajaNamestaja.PDV;
                    return;
                case RadSa.DODATNAUSLUGA:
                    if (dgProdajaDU.SelectedItem == null)
                    {
                        ErrorMessagePrint("Niste nista izabrali. ", "Upozorenje");
                        return;
                    }
                    DodatnaUsluga selektovanaDodatnaUsluga = (DodatnaUsluga)dgProdajaDU.SelectedItem;
                    tempCena = selektovanaDodatnaUsluga.Cena;
                    prodajaNamestaja.UkupnaCena += tempCena;
                    prodajaNamestaja.UkupnaCenaPdv += tempCena + tempCena * ProdajaNamestaja.PDV;
                    var tempDU = new ProdataDU
                    {
                        DodatnaUslugaId = selektovanaDodatnaUsluga.Id,
                        Obrisan = false
                    };
                    Korpa.Add(tempDU);
                    ListaDU.Remove(selektovanaDodatnaUsluga);
                    return;
            }
        }

        private void btnIzbaci_Click(object sender, RoutedEventArgs e)
        {
            double tempCena = 0;
            if (dgRacun.SelectedItem == null)
            {
                ErrorMessagePrint("Niste nista izabrali. ", "Upozorenje");
                return;
            }
            if (dgRacun.SelectedItem.GetType() == typeof(JedinicaProdaje))
            {
                var itemSaRacuna = (JedinicaProdaje)dgRacun.SelectedItem;
                var unosKolicine = new KolicinaWindow(itemSaRacuna.Kolicina, 0);
                unosKolicine.ShowDialog();
                if (unosKolicine.DialogResult == true)
                {
                    int tempKolicina = unosKolicine.Kolicina;
                    if (tempKolicina == itemSaRacuna.Kolicina)
                    {
                        Korpa.Remove(itemSaRacuna);
                    }
                    else
                    {
                        itemSaRacuna.Kolicina = itemSaRacuna.Kolicina - tempKolicina;
                    }
                    var tempCenaJP = (tempKolicina * itemSaRacuna.Namestaj.AkcijskaCena);
                    prodajaNamestaja.UkupnaCena -= tempCenaJP;
                    prodajaNamestaja.UkupnaCenaPdv -= tempCenaJP + tempCenaJP * ProdajaNamestaja.PDV;
                    foreach(var item in Projekat.Instance.Namestaji)
                        if(itemSaRacuna.NamestajId == item.Id)
                            item.BrKomada += tempKolicina;
                }
                return;
            }
            if (dgRacun.SelectedItem.GetType() == typeof(ProdataDU))
            {
                var itemSaRacuna = (ProdataDU)dgRacun.SelectedItem;
                Korpa.RemoveAt(dgRacun.SelectedIndex);
                if (itemSaRacuna.Obrisan != true)
                    ListaDU.Add(DodatnaUslugaDAO.GetById(itemSaRacuna.DodatnaUslugaId));
                tempCena = itemSaRacuna.Cena;
                prodajaNamestaja.UkupnaCena -= tempCena;
                prodajaNamestaja.UkupnaCenaPdv -= tempCena + tempCena * ProdajaNamestaja.PDV;
            }
        }

        private bool ForceValidation()
        {
            BindingExpression be1 = tbKupac.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            if (Validation.GetHasError(tbKupac) == true)
            {
                return true;
            }
            return false;
        }
        private void Indexiranje(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()).ToString();
        }
        private void btnIzadji_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public async void ErrorMessagePrint(string message, string title)
        {
            await this.ShowMessageAsync(title, message);
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (prodajaUspesna == false && operacija == Operacija.DODAVANJE)
            {
                foreach (var item in Korpa)
                {
                    if (item.GetType() == typeof(JedinicaProdaje))
                    {
                        var tempJP = (JedinicaProdaje)item;
                        foreach (var tempNamestaj in Projekat.Instance.Namestaji)
                        {
                            if (tempJP.NamestajId == tempNamestaj.Id)
                            {
                                tempNamestaj.BrKomada += tempJP.Kolicina;
                            }
                        }
                    }
                }
            }
        }
    }
}
