using POP_SF39_2016.model;
using POP_SF39_2016.util;
using POP_SF39_2016_GUI.DAO;
using POP_SF39_2016_GUI.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
   
    public partial class ProdajaWindow : Window
    {
        public ObservableCollection<Object> Korpa { get; set; } = new ObservableCollection<Object>();
        ICollectionView view;
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
        private int index;

        public ProdajaWindow(ProdajaNamestaja prodajaNamestaja,int index,Operacija operacija)
        {
            InitializeComponent();
            this.operacija = operacija;
            this.prodajaNamestaja = prodajaNamestaja;
            this.index = index;
            dgProdajaN.IsReadOnly = true;
            dgProdajaN.IsSynchronizedWithCurrentItem = true;
            dgProdajaN.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgProdajaDU.IsReadOnly = true;
            dgProdajaDU.IsSynchronizedWithCurrentItem = true;
            dgProdajaDU.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgRacun.IsReadOnly = true;
            dgRacun.IsSynchronizedWithCurrentItem = true;
            PopuniTabele();
        }

        private void NamestajSelected(object sender, RoutedEventArgs e)
        {
            var tab = sender as TabItem;
            if (tab != null)
            {
                radSa = RadSa.NAMESTAJ;
                lbKolicina.Visibility = Visibility.Visible;
                tbKolicina.Visibility = Visibility.Visible;
                Console.WriteLine("NAM");
            }
        }
        private void DodatnaUslugaSelected(object sender, RoutedEventArgs e)
        {
            var tab = sender as TabItem;
            if (tab != null)
            {
                radSa = RadSa.DODATNAUSLUGA;
                tbKolicina.Text = "";
                lbKolicina.Visibility = Visibility.Hidden;
                tbKolicina.Visibility = Visibility.Hidden;
                Console.WriteLine("DU");
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
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaji);
            view.Filter = obrisanFilterN;
            dgProdajaN.ItemsSource = view;
            dgProdajaN.SelectedIndex = 0;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatneUsluge);
            view.Filter = obrisanFilterDU;
            dgProdajaDU.ItemsSource = view;
            dgProdajaDU.SelectedIndex = 0;
            tbUkupnaCenaPdv.DataContext = prodajaNamestaja;
            tbUkupnaCena.DataContext = prodajaNamestaja;
            tbDatum.DataContext = prodajaNamestaja;
            tbKupac.DataContext = prodajaNamestaja;
            tbBrojRacuna.DataContext = prodajaNamestaja;
            dgRacun.ItemsSource = Korpa;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    prodajaNamestaja.DatumProdaje = DateTime.Today;
                    break;
                case Operacija.IZMENA:
                    var temp = prodajaNamestaja.UkupnaCena;
                    prodajaNamestaja.UkupnaCenaPdv =temp+ temp*ProdajaNamestaja.PDV;
                    //ObservableCollection<Object> tempListJP = JedinicaProdajeDAO.GetAllForId(prodajaNamestaja.Id);
                    //ObservableCollection<Object> tempListDU = ProdataDodatnaUslugaDAO.GetAllForId(prodajaNamestaja.Id);
                    //Korpa = (ObservableCollection<Object>) JedinicaProdajeDAO.GetAllForId(prodajaNamestaja.Id).ToList<Object>();
                    JedinicaProdajeDAO.GetAllForId(prodajaNamestaja.Id).ToList().ForEach(x => { Korpa.Add(x); });
                    ProdataDodatnaUslugaDAO.GetAllForId(prodajaNamestaja.Id).ToList().ForEach(x => { Korpa.Add(x); });
                    //ObservableCollection<Object> myCollection = JedinicaProdajeDAO.GetAllForId(prodajaNamestaja.Id);

                    break;
            }
            

        }
        private void dgProdajaN_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "TipNamestajaId")
            {
                e.Cancel = true;
            }
        }
        private void dgProdajaDU_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "Kolicina")
            {
                e.Cancel = true;
            }
        }

        private void btnIzadji_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnProdajFinal_Click(object sender, RoutedEventArgs e)
        {
            if (tbKupac.Text=="" || tbBrojRacuna.Text=="")
            {
                MessageBoxResult poruka = MessageBox.Show("Polja moraju biti popunjena. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            if (Korpa.Count()==0)
            {
                MessageBoxResult poruka = MessageBox.Show("Korpa je prazna.", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    var tempProdaja = ProdajaDAO.Create(prodajaNamestaja);
                    var listaNamestaja = Projekat.Instance.Namestaji;
                    foreach (var item in Korpa)
                    {
                        if (item.GetType() == typeof(JedinicaProdaje))
                        {
                            var tempItem = (JedinicaProdaje)item;
                            foreach (Namestaj namestaj in listaNamestaja)
                            {
                                if (namestaj.Id == tempItem.NamestajId)
                                {
                                    namestaj.BrKomada -= tempItem.Kolicina;
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
                    var listaProdaja = Projekat.Instance.Prodaja;
                    
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
                            var tempItem = (JedinicaProdaje)item;
                            bool postoji = false;
                            foreach (JedinicaProdaje tempN in JedinicaProdajeDAO.GetAllForId(prodajaNamestaja.Id))
                            {
                                if (tempN.Id == tempItem.Id)
                                {
                                    postoji = true;
                                    listaJPZaBrisanje.ToList().ForEach(x => { if (x.Id == tempN.Id) listaJPZaBrisanje.Remove(x);return;});
                                    if (tempItem.Kolicina != tempN.Kolicina)
                                    {
                                        int temp = tempItem.Kolicina - tempN.Kolicina;
                                        JedinicaProdajeDAO.Update(tempItem);
                                        Namestaj.GetById(tempItem.NamestajId).BrKomada -= temp;

                                    }
                                    break;
                                }
                            }
                            if (postoji == false)
                            {
                                tempItem.ProdajaId = prodajaNamestaja.Id;
                                JedinicaProdajeDAO.Create(tempItem);
                                Namestaj.GetById(tempItem.NamestajId).BrKomada -= tempItem.Kolicina;
                            }
                        }///////////
                        else
                        {
                            var tempItem = (ProdataDU)item;
                            bool postoji = false;
                            foreach (ProdataDU tempDU in ProdataDodatnaUslugaDAO.GetAllForId(prodajaNamestaja.Id))
                            {
                                if (tempDU.Id == tempItem.Id)
                                {
                                    postoji = true;
                                    listaDUZaBrisanje.ToList().ForEach(x => { if (x.Id == tempDU.Id) listaDUZaBrisanje.Remove(x); return; });
                                    break;
                                }
                            }
                            if (postoji == false)
                            {
                                tempItem.ProdajaId = prodajaNamestaja.Id;
                                ProdataDodatnaUslugaDAO.Create(tempItem);
                            }
                        }
                    }
                    foreach(JedinicaProdaje jpZaObrisati in listaJPZaBrisanje)
                    {
                        JedinicaProdajeDAO.Delete(jpZaObrisati);
                        Namestaj.GetById(jpZaObrisati.NamestajId).BrKomada += jpZaObrisati.Kolicina;
                    }
                    foreach(ProdataDU duZaObrisati in listaDUZaBrisanje)
                    {
                        ProdataDodatnaUslugaDAO.Delete(duZaObrisati);
                    }
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
                        MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                        return;
                    }
                    Namestaj selektovaniNamestaj = (Namestaj)dgProdajaN.SelectedItem;
                    int kolicina = 0;
                    try
                    {
                        if (tbKolicina.Text == "")
                            throw new Exception("Morate uneti pozitivan broj!");
                        kolicina = Int32.Parse(tbKolicina.Text);
                        if (kolicina <= 0)
                            throw new Exception("Morate uneti pozitivan broj!");
                        if (kolicina > selektovaniNamestaj.BrKomada)
                            throw new Exception("Nema dovoljno komada!");
                    }
                    catch (Exception ex)
                    {
                        MessageBoxResult poruka = MessageBox.Show(ex.Message, "Upozorenje", MessageBoxButton.OK);
                        return;
                    }
                    bool postoji = false;
                    foreach(var item in Korpa)
                    {
                        if (item.GetType() == typeof(JedinicaProdaje))
                        {
                            var tempJP = (JedinicaProdaje)item;
                            if (tempJP.NamestajId == ((Namestaj)dgProdajaN.SelectedItem).Id)
                            {
                                tempJP.Kolicina += kolicina;
                                var tempCenaJP = tempJP.Kolicina * tempJP.Cena;
                                tempJP.CenaUkupno = tempCenaJP;
                                tempJP.CenaUkupnoPdv = tempCenaJP + tempCenaJP * ProdajaNamestaja.PDV;
                                postoji = true;
                            }
                        }

                    }
                    if (postoji == false)
                    {
                        JedinicaProdaje jd = new JedinicaProdaje
                        {
                            NamestajId = selektovaniNamestaj.Id,
                            Kolicina = kolicina,
                        };
                        Korpa.Add(jd);
                    }
                    tempCena = selektovaniNamestaj.Cena * kolicina;
                    prodajaNamestaja.UkupnaCena += tempCena;
                    prodajaNamestaja.UkupnaCenaPdv += tempCena + tempCena*ProdajaNamestaja.PDV;
                    return;
                case RadSa.DODATNAUSLUGA:
                    if (dgProdajaDU.SelectedItem == null)
                    {
                        MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                        return;
                    }
                    DodatnaUsluga selektovanaDodatnaUsluga = (DodatnaUsluga)dgProdajaDU.SelectedItem;
                    tempCena = selektovanaDodatnaUsluga.Cena ;
                    prodajaNamestaja.UkupnaCena += tempCena;
                    prodajaNamestaja.UkupnaCenaPdv += tempCena + tempCena * ProdajaNamestaja.PDV;
                    var tempDU = new ProdataDU
                    {
                        DodatnaUslugaId = selektovanaDodatnaUsluga.Id,
                        Obrisan = false
                    };
                    Korpa.Add(tempDU);
                    return;
            }
        }

        private void btnIzbaci_Click(object sender, RoutedEventArgs e)
        {
            double tempCena = 0;
            if (dgRacun.SelectedItem == null)
            {
                MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            try
            { JedinicaProdaje itemSaRacuna = (JedinicaProdaje)dgRacun.SelectedItem;
                int kolicina = 0;
                try
                {
                    if (tbKolicina.Text == "")
                        throw new Exception("Morate uneti pozitivan broj!");
                    kolicina = Int32.Parse(tbKolicina.Text);
                    if (kolicina <= 0)
                        throw new Exception("Morate uneti pozitivan broj!");
                    if (kolicina > itemSaRacuna.Kolicina)
                        throw new Exception("Nema dovoljno komada!");
                }
                catch (Exception ex)
                {
                    MessageBoxResult poruka = MessageBox.Show(ex.Message, "Upozorenje", MessageBoxButton.OK);
                    return;
                }
                if (kolicina == itemSaRacuna.Kolicina)
                {
                    Korpa.Remove(itemSaRacuna);
                }
                else
                {
                    itemSaRacuna.Kolicina = itemSaRacuna.Kolicina - kolicina;
                    var tempCenaForRow = itemSaRacuna.Kolicina * itemSaRacuna.Cena;
                    itemSaRacuna.CenaUkupno = tempCenaForRow;
                    itemSaRacuna.CenaUkupnoPdv = tempCenaForRow + tempCenaForRow * ProdajaNamestaja.PDV;
                }
                var tempCenaJP = (kolicina * itemSaRacuna.Namestaj.Cena);
                prodajaNamestaja.UkupnaCena -= tempCenaJP;
                prodajaNamestaja.UkupnaCenaPdv -= tempCenaJP + tempCenaJP * ProdajaNamestaja.PDV;
                return;
            } catch { Console.WriteLine("nije namestaj"); }
            try
            {
                var itemSaRacuna = (ProdataDU)dgRacun.SelectedItem;
                Korpa.RemoveAt(dgRacun.SelectedIndex);
                tempCena = itemSaRacuna.Cena;
                prodajaNamestaja.UkupnaCena -= tempCena;
                prodajaNamestaja.UkupnaCenaPdv -= tempCena + tempCena * ProdajaNamestaja.PDV;
            } catch { Console.WriteLine("nije DU"); return; }
        }
    }
}
