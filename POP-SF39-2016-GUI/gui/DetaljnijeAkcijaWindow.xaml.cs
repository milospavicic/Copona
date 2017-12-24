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
        int idAkcije;
        public DetaljnijeAkcijaWindow(int idAkcije)
        {
            InitializeComponent();
            this.idAkcije = idAkcije;
            tbPopust.IsReadOnly = true;
            dgNamestajNaAkciji.IsHitTestVisible = false;
            dgNamestajNaAkciji.IsReadOnly = true;
            dgNamestajNaAkciji.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            PopunjavanjePolja();
            
        }

        private void PopunjavanjePolja()
        {

            tbPopust.Text = NaAkcijiDAO.GetPopust(idAkcije).ToString();
            dgNamestajNaAkciji.ItemsSource = NaAkcijiDAO.GetAllNamestajForActionId(idAkcije);

        }

        private void PrikazivanjeKolona(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "TipNamestajaId" || (string)e.Column.Header == "CenaSaPdv")
            {
                e.Cancel = true;
            }
        }
    }
}
