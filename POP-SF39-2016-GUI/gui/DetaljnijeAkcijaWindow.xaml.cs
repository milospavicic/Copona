using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
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
    /// Interaction logic for DetaljnijeAkcijaWindow.xaml
    /// </summary>
    public partial class DetaljnijeAkcijaWindow : Window
    {
        private Akcija trenutnaAkcija;
        public DetaljnijeAkcijaWindow(Akcija trenutnaAkcija)
        {
            InitializeComponent();
            this.trenutnaAkcija = trenutnaAkcija;
            tbPocetniDatum.DataContext = trenutnaAkcija;
            tbKrajnjiDatum.DataContext = trenutnaAkcija;
            dgNamestajNaAkciji.IsReadOnly = true;
            dgNamestajNaAkciji.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            PopunjavanjePolja();
            
        }

        private void PopunjavanjePolja()
        {
            dgNamestajNaAkciji.ItemsSource = NaAkcijiDAO.GetAllNAForActionId(trenutnaAkcija.Id);
        }

        private void PrikazivanjeKolona(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "IdNaAkciji" || (string)e.Column.Header == "IdNamestaja" || (string)e.Column.Header == "IdAkcije" || (string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Popust")
            {
                e.Column.Width = 50;
            }
            if ((string)e.Column.Header == "Cena")
            {
                e.Column.Width = 70;
            }
        }
        private void IgnoreDoubleclick(object sender, MouseButtonEventArgs e)
        {
            return;
        }

        private void Indexiranje(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()).ToString();
        }
    }
}
