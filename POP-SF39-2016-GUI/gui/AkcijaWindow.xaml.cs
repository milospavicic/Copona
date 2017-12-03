using POP_SF39_2016.model;
using POP_SF39_2016.util;
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
    public partial class AkcijaWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        private Akcija akcija;
        private Operacija operacija;
        private int index;

        public AkcijaWindow(Akcija akcija, int index, Operacija operacija)
        {
            InitializeComponent();
            this.akcija = akcija;
            this.operacija = operacija;
            this.index = index;
            
            PopunjavanjePolja();
        }

        private void PopunjavanjePolja()
        {
            dpPocetniDatum.DataContext = akcija;
            dpKrajnjiDatum.DataContext = akcija;
            tbPopust.DataContext = akcija;
            cbNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            cbNamestaj.DataContext = akcija;
        }

        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            if (dpPocetniDatum.DisplayDate > dpKrajnjiDatum.DisplayDate)
            {
                MessageBoxResult poruka = MessageBox.Show("Krajnji datum ne moze biti veci od pocetnog. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            if (cbNamestaj.SelectedItem == null)
            {
                MessageBoxResult poruka = MessageBox.Show("Polja ne smeju biti prazna. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            var listaAkcija = Projekat.Instance.Akcija;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    akcija.Id = listaAkcija.Count + 1;
                    listaAkcija.Add(akcija);
                    break;
                case Operacija.IZMENA:
                    Projekat.Instance.Akcija[index] = akcija;
                    break;
            }
            GenericSerializer.Serialize("akcije.xml",listaAkcija);
            this.Close();
            
        }
        private void ZatvoriWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
