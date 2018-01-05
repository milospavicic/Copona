using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System.Windows;

namespace POP_SF39_2016_GUI.gui
{
    /// <summary>
    /// Interaction logic for TipNamestajaWindow.xaml
    /// </summary>
    public partial class TipNamestajaWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        private TipNamestaja tipNamestaja;
        private Operacija operacija;

        public TipNamestajaWindow(TipNamestaja tipNamestaja, Operacija operacija)
        {
            InitializeComponent();
            this.tipNamestaja = tipNamestaja;
            this.operacija = operacija;
            PopunjavanjePolja(tipNamestaja);
        }
        public void PopunjavanjePolja(TipNamestaja tipNamestaja)
        {
            tbNaziv.DataContext = tipNamestaja;
        }
        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {

            var listaTipaNamestaja = Projekat.Instance.TipoviNamestaja;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    TipNamestajaDAO.Create(tipNamestaja);
                    break;
                case Operacija.IZMENA:
                    TipNamestajaDAO.Update(tipNamestaja);
                    break;
            }
            this.Close();
        }
        private void ZatvoriWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
