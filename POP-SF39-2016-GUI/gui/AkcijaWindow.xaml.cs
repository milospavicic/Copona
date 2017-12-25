using POP_SF39_2016.model;
using POP_SF39_2016.util;
using POP_SF39_2016_GUI.DAO;
using POP_SF39_2016_GUI.model;
using System;
using System.Collections;
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
    public partial class AkcijaWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public List<Namestaj> ListaNamestaja { get; set; } = new List<Namestaj>();
        private Akcija akcija;
        private Operacija operacija;
        private int index;

        public AkcijaWindow(Akcija akcija, int index, Operacija operacija)
        {
            InitializeComponent();
            this.akcija = akcija;
            this.operacija = operacija;
            this.index = index;
            dgNamestaj.AutoGenerateColumns = false;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            PopunjavanjePolja();
        }

        private void PopunjavanjePolja()
        {
            dpPocetniDatum.DataContext = akcija;
            dpKrajnjiDatum.DataContext = akcija;
            if (operacija == Operacija.DODAVANJE)
                dgNamestaj.ItemsSource = Projekat.Instance.Namestaji;
            else
            {
                tbPopust.Text = NaAkcijiDAO.GetPopust(akcija.Id).ToString();
                dgNamestaj.ItemsSource = NaAkcijiDAO.GetAllNamestajForActionId(akcija.Id);
                dgNamestaj.IsHitTestVisible = false;
                dgNamestaj.IsReadOnly = true;
                dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                dpPocetniDatum.IsHitTestVisible = false;

            }
        }

        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            if (dpPocetniDatum.DisplayDate > dpKrajnjiDatum.DisplayDate)
            {
                MessageBoxResult poruka = MessageBox.Show("Krajnji datum ne moze biti veci od pocetnog. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            var listaAkcija = Projekat.Instance.Akcija;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    AkcijaDAO.Create(akcija);
                    foreach (var tempNamestaj in ListaNamestaja)
                    {

                        var naAkciji = new NaAkciji()
                        {
                            IdAkcije = Projekat.Instance.Akcija.Count,
                            IdNamestaja = tempNamestaj.Id,
                            Popust = int.Parse(tbPopust.Text)
                        };
                        NaAkcijiDAO.Create(naAkciji);
                    }
                    break;
                case Operacija.IZMENA:
                    NaAkcijiDAO.SetPopust(akcija.Id, int.Parse(tbPopust.Text));
                    AkcijaDAO.Update(akcija);
                    break;
            }

            this.Close();
            
        }
        private void ZatvoriWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            ListaNamestaja.Add((Namestaj)dgNamestaj.SelectedItem);
        }

        private void OnUnChecked(object sender, RoutedEventArgs e)
        {
            ListaNamestaja.Remove((Namestaj)dgNamestaj.SelectedItem);
        }
    }
}
