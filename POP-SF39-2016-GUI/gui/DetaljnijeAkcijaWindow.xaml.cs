using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace POP_SF39_2016_GUI.gui
{
    public partial class DetaljnijeAkcijaWindow : MetroWindow
    {
        private Akcija trenutnaAkcija;
        public DetaljnijeAkcijaWindow(Akcija trenutnaAkcija)
        {
            InitializeComponent();
            this.trenutnaAkcija = trenutnaAkcija;
            dgNamestajNaAkciji.CanUserSortColumns = false;
            dgNamestajNaAkciji.CanUserAddRows = false;
            dgNamestajNaAkciji.CanUserDeleteRows = false;
            dgNamestajNaAkciji.IsReadOnly = true;
            dgNamestajNaAkciji.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            PopunjavanjePolja();
        }

        private void PopunjavanjePolja()
        {
            tbPocetniDatum.DataContext = trenutnaAkcija;
            tbKrajnjiDatum.DataContext = trenutnaAkcija;
            tbNaziv.DataContext = trenutnaAkcija;
            dgNamestajNaAkciji.ItemsSource = NaAkcijiDAO.GetAllNAForActionId(trenutnaAkcija.Id);
            this.Title += trenutnaAkcija.Naziv;
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
