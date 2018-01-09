using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace POP_SF39_2016_GUI.gui
{
    public partial class KorisnikWindow : MetroWindow
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        private Korisnik korisnik;
        private Operacija operacija;
        private Korisnik logovaniKorisnik;

        public KorisnikWindow(Korisnik korisnik, Operacija operacija, Korisnik logovaniKorisnik)
        {
            InitializeComponent();
            this.korisnik = korisnik;
            this.operacija = operacija;
            this.logovaniKorisnik = logovaniKorisnik;
            PopunjavanjePolja(korisnik);

            if (operacija == Operacija.IZMENA)
            {
                tbKorisnickoIme.Focusable = false;
                tbKorisnickoIme.IsHitTestVisible = false;
            }
        }

        private void PopunjavanjePolja(Korisnik korisnik)
        {
            cbPozicija.ItemsSource = Enum.GetValues(typeof(TipKorisnika)).Cast<TipKorisnika>();
            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            cbPozicija.DataContext = korisnik;
            tbSifra.DataContext = korisnik;
            if (logovaniKorisnik.TipKorisnika == TipKorisnika.Prodavac || logovaniKorisnik.Id == korisnik.Id)
                cbPozicija.IsEnabled = false;
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
                    foreach(var vecPostojeciKorisnik in Projekat.Instance.Korisnici)
                        if (korisnik.KorisnickoIme == vecPostojeciKorisnik.KorisnickoIme)
                        {
                            ErrorMessagePrint("Vec postoji korisnik sa unetim korisnickim imenom.", "Upozorenje");
                            return;
                        }
                    KorisnikDAO.Create(korisnik);
                    break;
                case Operacija.IZMENA:
                    KorisnikDAO.Update(korisnik);
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
            BindingExpression be1 = tbIme.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            BindingExpression be2 = tbPrezime.GetBindingExpression(TextBox.TextProperty);
            be2.UpdateSource();
            BindingExpression be3 = tbKorisnickoIme.GetBindingExpression(TextBox.TextProperty);
            be3.UpdateSource();
            BindingExpression be4 = tbSifra.GetBindingExpression(TextBox.TextProperty);
            be4.UpdateSource();
            if (Validation.GetHasError(tbIme) == true || Validation.GetHasError(tbPrezime)==true || Validation.GetHasError(tbKorisnickoIme) == true || Validation.GetHasError(tbSifra) == true)
            {
                return true;
            }
            return false;
        }
        public async void ErrorMessagePrint(string message, string title)
        {
            await this.ShowMessageAsync(title, message);
        }
    }
}
