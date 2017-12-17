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
    /// <summary>
    /// Interaction logic for DodatnaUslugaWindow.xaml
    /// </summary>
    public partial class DodatnaUslugaWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        private DodatnaUsluga dodatnaUsluga;
        private Operacija operacija;
        private int index;

        public DodatnaUslugaWindow(DodatnaUsluga dodatnaUsluga, int index, Operacija operacija)
        {
            InitializeComponent();
            this.dodatnaUsluga = dodatnaUsluga;
            this.operacija = operacija;
            this.index = index;
            PopunjavanjePolja(dodatnaUsluga);
        }

        private void PopunjavanjePolja(DodatnaUsluga dodatnaUsluga)
        {
            tbNaziv.DataContext = dodatnaUsluga;
            tbCena.DataContext = dodatnaUsluga;
        }
        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            try
            {
                int.Parse(tbCena.Text);
            }
            catch
            {
                MessageBoxResult poruka = MessageBox.Show("Polja moraju biti brojevi. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            var listaDodatnihUsluga = Projekat.Instance.DodatneUsluge;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    dodatnaUsluga.Id = listaDodatnihUsluga.Count + 1;
                    listaDodatnihUsluga.Add(dodatnaUsluga);
                    break;
                case Operacija.IZMENA:
                    Projekat.Instance.DodatneUsluge[index] = dodatnaUsluga;
                    break;
            }
            GenericSerializer.Serialize("dodatneusluge.xml", listaDodatnihUsluga);
            this.Close();
        }
        private void ZatvoriWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
