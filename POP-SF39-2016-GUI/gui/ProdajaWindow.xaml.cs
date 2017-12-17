using POP_SF39_2016.model;
using POP_SF39_2016.util;
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
        public ObservableCollection<JedinicaProdaje> listaNamestajaZaProdaju { get; set; } = new ObservableCollection<JedinicaProdaje>();
        public ObservableCollection<DodatnaUsluga> listaDUZaProdaju { get; set; } = new ObservableCollection<DodatnaUsluga>();
        public ObservableCollection<JedinicaProdaje> listaJedProdaje { get; set; } = Projekat.Instance.JedinicaProdaje;
        // lista za indexiranje jedProdaje..
        public ObservableCollection<Object> korpa { get; set; } = new ObservableCollection<object>();
        public double ukupnaCena { get; set; }
        public double ukupnaCenaSaPDV { get; set; }
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
        private void PodaciSelected(object sender, RoutedEventArgs e)
        {
            var tab = sender as TabItem;
            if (tab != null)
            {
                tbUkupnaCena.Text = ukupnaCena.ToString();
                tbDatum.Text = DateTime.Now.ToShortDateString();
            }
        }
        private void NamestajSelected(object sender, RoutedEventArgs e)
        {
            var tab = sender as TabItem;
            if (tab != null)
            {
                radSa = RadSa.NAMESTAJ;
                tbKolicina.IsReadOnly = false;
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
                tbKolicina.IsReadOnly = true;
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
        {   view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaji);
            view.Filter = obrisanFilterN;
            dgProdajaN.ItemsSource = view;
            dgProdajaN.SelectedIndex = 0;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatneUsluge);
            view.Filter = obrisanFilterDU;
            dgProdajaDU.ItemsSource = view;
            dgProdajaDU.SelectedIndex = 0;
            //dgProdatoN.ItemsSource = listaNamestajaZaProdaju;
            //dgProdatoDU.ItemsSource = listaDUZaProdaju;
            tbUkupnaCena.Text = ukupnaCena.ToString();
            dgRacun.ItemsSource = korpa;
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
            if (korpa.Count()==0)
            {
                MessageBoxResult poruka = MessageBox.Show("Korpa je prazna.", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            
            List<int> listaJedId = new List<int>();
            foreach (JedinicaProdaje jedProdaje in listaNamestajaZaProdaju)
            {
                listaJedId.Add(jedProdaje.Id);
            }
            List<int> listaDUId = new List<int>();
            if (listaDUZaProdaju.Count!=0)
            {
                
                foreach (DodatnaUsluga dodatnaUsluga in listaDUZaProdaju)
                {
                    listaDUId.Add(dodatnaUsluga.Id);
                }
            }
            var novaProdaja = new ProdajaNamestaja
            {
                BrRacuna = tbBrojRacuna.Text,
                Kupac = tbKupac.Text,
                DatumProdaje = DateTime.Today,
                ListaJedinicaProdajeId = listaJedId,
                DodatneUslugeId = listaDUId,
                Id = Projekat.Instance.Prodaja.Count() + 1,
                UkupnaCena = ukupnaCena
            };
            var listaProdaja = Projekat.Instance.Prodaja;
            listaProdaja.Add(novaProdaja);
            GenericSerializer.Serialize("prodajenamestaja.xml", listaProdaja);
            GenericSerializer.Serialize("jediniceprodaje.xml", listaJedProdaje);
            var listaNamestajaZaOduzeti = Projekat.Instance.Namestaji;

            foreach (JedinicaProdaje jedProdaje in listaNamestajaZaProdaju)
            {
                Console.WriteLine("test");
                //ovaj deo ne radi kako treba.. pise manje u fajl
                foreach (Namestaj namestaj in listaNamestajaZaOduzeti)
                {
                    if (namestaj.Id == jedProdaje.NamestajId)
                    {
                        namestaj.BrKomada -= jedProdaje.Kolicina;
                    }
                }
            }

            GenericSerializer.Serialize("namestaj.xml", listaNamestajaZaOduzeti);
            this.Close(); 

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
                    JedinicaProdaje jd = new JedinicaProdaje
                    {
                        Id = listaJedProdaje.Count() + 1,
                        NamestajId = selektovaniNamestaj.Id,
                        Kolicina = kolicina,
                    };
                    listaJedProdaje.Add(jd);
                    tempCena = selektovaniNamestaj.Cena * kolicina;
                    ukupnaCena += tempCena;
                    ukupnaCenaSaPDV += tempCena + tempCena * ProdajaNamestaja.PDV;
                    listaNamestajaZaProdaju.Add(jd);
                    korpa.Add(jd);

                    return;
                case RadSa.DODATNAUSLUGA:
                    if (dgProdajaDU.SelectedItem == null)
                    {
                        MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                        return;
                    }
                    DodatnaUsluga selektovanaDodatnaUsluga = (DodatnaUsluga)dgProdajaDU.SelectedItem;
                    tempCena = selektovanaDodatnaUsluga.Cena ;
                    ukupnaCena += tempCena;
                    ukupnaCenaSaPDV += tempCena + tempCena * ProdajaNamestaja.PDV;
                    listaDUZaProdaju.Add(selektovanaDodatnaUsluga);
                    korpa.Add(selektovanaDodatnaUsluga);
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
                    korpa.Remove(itemSaRacuna);
                }
                else
                {
                    itemSaRacuna.Kolicina = itemSaRacuna.Kolicina - kolicina;
                }
                ukupnaCena -= (kolicina * itemSaRacuna.Namestaj.Cena);
                return;
            } catch { Console.WriteLine("nije namestaj"); }
            try
            {
                DodatnaUsluga itemSaRacuna = (DodatnaUsluga)dgRacun.SelectedItem;
                korpa.RemoveAt(dgRacun.SelectedIndex);
                tempCena = itemSaRacuna.Cena;
                ukupnaCena -= tempCena;
                ukupnaCenaSaPDV -= tempCena * ProdajaNamestaja.PDV;
            } catch { Console.WriteLine("nije DU"); return; }
        }
    }
}
