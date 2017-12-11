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
        //public List<Object> korpa { get; set; } = new List<object>();
        public double ukupnaCena { get; set; }
        //public double ukupnaCenaSaPDV { get; set; }
        ICollectionView view;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        private ProdajaNamestaja prodajaNamestaja;
        private Operacija operacija;
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
            dgProdatoN.IsReadOnly = true;
            dgProdatoN.IsSynchronizedWithCurrentItem = true;
            dgProdatoN.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgProdatoDU.IsReadOnly = true;
            dgProdatoDU.IsSynchronizedWithCurrentItem = true;
            dgProdatoDU.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            PopuniTabele();
        }
        private void OnTabSelected(object sender, RoutedEventArgs e)
        {
            var tab = sender as TabItem;
            if (tab != null)
            {
                tbUkupnaCena.Text = ukupnaCena.ToString();
                tbDatum.Text = DateTime.Now.ToShortDateString();
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
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatnaUsluga);
            view.Filter = obrisanFilterDU;
            dgProdajaDU.ItemsSource = view;
            dgProdajaDU.SelectedIndex = 0;
            dgProdatoN.ItemsSource = listaNamestajaZaProdaju;
            dgProdatoDU.ItemsSource = listaDUZaProdaju;
            tbUkupnaCena.Text = ukupnaCena.ToString();
        }
        private void ProdajNOnClick(object sender, RoutedEventArgs e)
        {
            if (dgProdajaN.SelectedItem == null)
            {
                MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            Namestaj selektovaniNamestaj = (Namestaj)dgProdajaN.SelectedItem;
            int kolicina = 0;
            try
            {
                if(tbKolicina.Text=="")
                    throw new Exception("Morate uneti pozitivan broj!");
                kolicina =Int32.Parse(tbKolicina.Text);
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
                Id = listaJedProdaje.Count()+1,
                NamestajId = selektovaniNamestaj.Id,
                Kolicina = kolicina,
            };
            listaJedProdaje.Add(jd);
            ukupnaCena += (selektovaniNamestaj.Cena*kolicina);
            listaNamestajaZaProdaju.Add(jd);
        }

        private void ProdajDUOnClick(object sender, RoutedEventArgs e)
        {
            if (dgProdajaDU.SelectedItem == null)
            {
                MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            DodatnaUsluga selektovanaDodatnaUsluga = (DodatnaUsluga)dgProdajaDU.SelectedItem;
            ukupnaCena += selektovanaDodatnaUsluga.Cena;
            listaDUZaProdaju.Add(selektovanaDodatnaUsluga);
        }

        private void IzbaciNOnClick(object sender, RoutedEventArgs e)
        {
            if (dgProdatoN.SelectedItem == null)
            {
                MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            JedinicaProdaje namestajZaIzbaciti =(JedinicaProdaje) dgProdatoN.SelectedItem;
            int kolicina = 0;
            try
            {
                if (tbKolicina.Text == "")
                    throw new Exception("Morate uneti pozitivan broj!");
                kolicina = Int32.Parse(tbKolicina.Text);
                if (kolicina <= 0)
                    throw new Exception("Morate uneti pozitivan broj!");
                if (kolicina > namestajZaIzbaciti.Kolicina)
                    throw new Exception("Nema dovoljno komada!");
            }
            catch (Exception ex)
            {
                MessageBoxResult poruka = MessageBox.Show(ex.Message, "Upozorenje", MessageBoxButton.OK);
                return;
            }
            if(kolicina == namestajZaIzbaciti.Kolicina)
            {
                listaNamestajaZaProdaju.Remove(namestajZaIzbaciti);
                
            }
            else
            {
                namestajZaIzbaciti.Kolicina = namestajZaIzbaciti.Kolicina - kolicina;
            }
            ukupnaCena -= (kolicina * namestajZaIzbaciti.Namestaj.Cena);
        }
        private void IzbaciDUOnClick(object sender, RoutedEventArgs e)
        {
            if (dgProdatoDU.SelectedItem == null)
            {
                MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            DodatnaUsluga duZaIzbaciti = (DodatnaUsluga)dgProdatoDU.SelectedItem;
            listaDUZaProdaju.RemoveAt(dgProdatoDU.SelectedIndex);
            ukupnaCena -= duZaIzbaciti.Cena;


        }
        private void dgProdajaN_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "TipNamestajaId")
            {
                e.Cancel = true;
            }
        }

        private void dgProdatoN_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "NamestajId")
            {
                e.Cancel = true;
            }
        }
        private void dgProdajaDU_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id")
            {
                e.Cancel = true;
            }
        }
        private void dgProdatoDU_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id")
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
            if (listaNamestajaZaProdaju.Count()==0)
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
    }
}
