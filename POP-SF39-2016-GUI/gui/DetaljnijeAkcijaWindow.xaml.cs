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
            tbPopust.IsReadOnly = true;
            dgNamestajNaAkciji.IsReadOnly = true;
            dgNamestajNaAkciji.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            PopunjavanjePolja();
            
        }

        private void PopunjavanjePolja()
        {

            tbPopust.Text = NaAkcijiDAO.GetPopust(trenutnaAkcija.Id).ToString();
            dgNamestajNaAkciji.ItemsSource = NaAkcijiDAO.GetAllNamestajForActionId(trenutnaAkcija.Id);

        }

        private void PrikazivanjeKolona(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "TipNamestajaId" || (string)e.Column.Header == "CenaSaPdv")
            {
                e.Cancel = true;
            }
        }
        private void IgnoreDoubleclick(object sender, MouseButtonEventArgs e)
        {
            return;
        }
    }
}
