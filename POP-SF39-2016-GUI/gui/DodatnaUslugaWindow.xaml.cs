using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace POP_SF39_2016_GUI.gui
{
    public partial class DodatnaUslugaWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        private DodatnaUsluga dodatnaUsluga;
        private Operacija operacija;

        public DodatnaUslugaWindow(DodatnaUsluga dodatnaUsluga, Operacija operacija)
        {
            InitializeComponent();
            this.dodatnaUsluga = dodatnaUsluga;
            this.operacija = operacija;
            PopunjavanjePolja(dodatnaUsluga);
        }

        private void PopunjavanjePolja(DodatnaUsluga dodatnaUsluga)
        {
            tbNaziv.DataContext = dodatnaUsluga;
            tbCena.DataContext = dodatnaUsluga;
        }
        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            if (ForceValidation() == true)
                return;
            var listaDodatnihUsluga = Projekat.Instance.DodatneUsluge;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    DodatnaUslugaDAO.Create(dodatnaUsluga);
                    break;
                case Operacija.IZMENA:
                    DodatnaUslugaDAO.Update(dodatnaUsluga);
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
            BindingExpression be2 = tbCena.GetBindingExpression(TextBox.TextProperty);
            be2.UpdateSource();
            if (Validation.GetHasError(tbNaziv) == true || Validation.GetHasError(tbCena) == true)
            {
                return true;
            }
            return false;
        }
    }
}
