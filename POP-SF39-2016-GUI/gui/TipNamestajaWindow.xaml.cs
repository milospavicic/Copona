using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MahApps.Metro.Controls;

namespace POP_SF39_2016_GUI.gui
{
    public partial class TipNamestajaWindow : MetroWindow
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
            tbNaziv.MaxLength = 100;
            if (operacija == Operacija.DODAVANJE)
                this.Title += " - Dodavanje";
            else
                this.Title += " - Izmena";
        }
        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            if (ForceValidation() == true)
                return;
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
        private bool ForceValidation()
        {
            BindingExpression be1 = tbNaziv.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();

            if (Validation.GetHasError(tbNaziv) == true)
            {
                return true;
            }
            return false;
        }
    }
}
