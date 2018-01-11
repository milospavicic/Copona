using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MahApps.Metro.Controls;

namespace POP_SF39_2016_GUI.gui
{
    public partial class NamestajWindow : MetroWindow
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
            tbNaziv.MaxLength = 100;
            tbCena.DataContext = namestaj;
            tbCena.MaxLength = 9;
            tbBrojKomada.DataContext = namestaj;
            tbBrojKomada.MaxLength = 9;
            cbTipNamestaja.DataContext = namestaj;
            if (operacija == Operacija.DODAVANJE)
                this.Title += " - Dodavanje";
            else
                this.Title += " - Izmena";
        }
        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            if (ForceValidation() == true)
            {
                return;
            }
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    string sifraNamestaja = "";
                    if(namestaj.Naziv.Length>=2)
                        sifraNamestaja += namestaj.Naziv.Substring(0, 2);
                    sifraNamestaja += new Random().Next(1, 1000);
                    sifraNamestaja += namestaj.TipNamestaja.Naziv.Substring(0, 2);
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
        private bool ForceValidation()
        {
            BindingExpression be1 = tbNaziv.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            BindingExpression be2 = tbCena.GetBindingExpression(TextBox.TextProperty);
            be2.UpdateSource();
            BindingExpression be3 = tbBrojKomada.GetBindingExpression(TextBox.TextProperty);
            be3.UpdateSource();
            if (Validation.GetHasError(tbNaziv) == true || Validation.GetHasError(tbCena) == true || Validation.GetHasError(tbBrojKomada) == true)
            {
                return true;
            }
            return false;
        }
    }
}
