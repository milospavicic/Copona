using POP_SF39_2016.model;
using POP_SF39_2016_GUI.model;
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
   
    public partial class ProdajaWindow : Window
    {
        public ObservableCollection<JedinicaProdaje> listaNamestajaZaProdaju { get; set; } = new ObservableCollection<JedinicaProdaje>();
        //visual uvek trazi save?! wtf
        public List<DodatnaUsluga> listaDUZaProdaju { get; set; } = new List<DodatnaUsluga>();
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
            dgProdajaDU.IsReadOnly = true;
            dgProdajaDU.IsSynchronizedWithCurrentItem = true;
            dgProdatoN.IsReadOnly = true;
            dgProdatoN.IsSynchronizedWithCurrentItem = true;
            dgProdatoN.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            PopuniTabele();
        }
        private bool obrisanFilter(object obj)
        {
            return !((Namestaj)obj).Obrisan;
        }
        private void PopuniTabele()
        {   //view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
            //view.Filter = obrisanFilter;
            //dgProdajaN.ItemsSource = view;
            dgProdajaN.ItemsSource = Projekat.Instance.Namestaj;
            dgProdajaN.SelectedIndex = 0;
            dgProdajaDU.ItemsSource = Projekat.Instance.DodatnaUsluga;
            dgProdajaDU.SelectedIndex = 0;
            dgProdatoN.ItemsSource = listaNamestajaZaProdaju;
            dgProdatoN.DataContext = listaNamestajaZaProdaju;
        }
        private void ProdajNOnClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(dgProdajaN.SelectedItem);
            if(dgProdajaN.SelectedItem == null)
            {
                MessageBoxResult poruka = MessageBox.Show("Niste nista izabrali. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            Namestaj selektovaniNamestaj = (Namestaj)dgProdajaN.SelectedItem;
            JedinicaProdaje jd = new JedinicaProdaje
            {
                NamestajId = selektovaniNamestaj.Id,
                Kolicina = 1,
            };
            listaNamestajaZaProdaju.Add(jd);
            return;
        }

        private void dgProdatoN_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "NamestajId")
            {
                e.Cancel = true;
            }
        }

    }
}
