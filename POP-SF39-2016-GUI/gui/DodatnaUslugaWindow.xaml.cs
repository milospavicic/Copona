using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System.Windows;

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
    }
}
