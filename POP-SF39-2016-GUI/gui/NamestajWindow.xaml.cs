using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System;
using System.Windows;

namespace POP_SF39_2016_GUI.gui
{
    public partial class NamestajWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        private Namestaj namestaj;
        private Operacija operacija;

        public NamestajWindow(Namestaj namestaj, Operacija operacija)
        {
            InitializeComponent();
            this.namestaj = namestaj;
            this.operacija = operacija;
            PopunjavanjePolja(namestaj);
        }
        public void PopunjavanjePolja(Namestaj namestaj)
        {
            cbTipNamestaja.ItemsSource = Projekat.Instance.TipoviNamestaja;
            if (operacija == Operacija.DODAVANJE)
                namestaj.TipNamestaja = Projekat.Instance.TipoviNamestaja[0];
            tbNaziv.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbBrojKomada.DataContext = namestaj;
            cbTipNamestaja.DataContext = namestaj;
        }
        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            try
            {
                double.Parse(tbBrojKomada.Text);

                double.Parse(tbCena.Text);
            }
            catch
            {
                MessageBoxResult poruka = MessageBox.Show("Polja moraju biti brojevi. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            if (cbTipNamestaja.SelectedItem == null)
            {
                MessageBoxResult poruka = MessageBox.Show("Polja ne smeju biti prazna. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    string sifraNamestaja = "";
                    sifraNamestaja += namestaj.Naziv.Substring(0, 2) + new Random().Next(1, 100) + namestaj.TipNamestaja.Naziv.Substring(0, 2);
                    namestaj.Sifra = sifraNamestaja.ToUpper();
                    NamestajDAO.Create(namestaj);
                    break;

                case Operacija.IZMENA:
                    NamestajDAO.Update(namestaj);
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
