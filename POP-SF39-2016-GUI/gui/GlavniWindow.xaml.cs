using POP_SF39_2016.model;
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
    /// Interaction logic for GlavniWindow.xaml
    /// </summary>
    public partial class GlavniWindow : Window
    {
        public Korisnik logovaniKorisnik { get; set; } = new Korisnik();
        public GlavniWindow(Korisnik logovaniKorisnik)
        {
            this.logovaniKorisnik = logovaniKorisnik;
            InitializeComponent();
            if (logovaniKorisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                btnAdmin.Visibility = Visibility.Hidden;
            }
        }
          private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MessageBoxResult r = MessageBox.Show("Da li ste sigurni?", "Izlazak", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            };
        }

        private void PrikazNamestaja(object sender, RoutedEventArgs e)
        {
            borderCentarInfo.Visibility = Visibility.Hidden;
            borderCentarEdit.Visibility = Visibility.Hidden;
            lbNamestaj.Visibility = Visibility.Visible;
            lbNamestaj.Items.Clear();
            List<Namestaj> listaNamestaja = Projekat.Instance.Namestaj;
            foreach (Namestaj namestaj in listaNamestaja)
                lbNamestaj.Items.Add(namestaj.Naziv);
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Da li ste sigurni?", "Izlazak", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            };
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Da li ste sigurni?", "Izlazak", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                var MainWindow = new MainWindow();
                MainWindow.Show();  
                this.Hide();
            };
        }

        private void IspisInformmacija(object sender, RoutedEventArgs e)
        {
            
            lbNamestaj.Visibility = Visibility.Hidden;
            borderCentarEdit.Visibility = Visibility.Hidden;
            borderCentarInfo.Visibility = Visibility.Visible;
            string ispis = "";
            string pocetniRazmak = String.Concat(Enumerable.Repeat("\n", 5));
            List<Salon> listaSalona = Projekat.Instance.Salon;
            Salon mojSalon = listaSalona[0];
            ispis += "Naziv: " + mojSalon.Naziv + "\n";
            ispis += "Adresa: " + mojSalon.Adresa + "\n";
            ispis += "Broj telefona: " + mojSalon.BrojTelefona + "\n";
            ispis += "Email: " + mojSalon.Email + "\n";
            ispis += "Web adresa: " + mojSalon.WebAdresa + "\n";
            ispis += "Maticni broj: " + mojSalon.MaticniBr + "\n";
            ispis += "Broj Racuna: " + mojSalon.BrRacuna + "\n";
            ispis += "Pib: " + mojSalon.Pib + "\n";

            tbOSalonu.Text = pocetniRazmak + ispis;
        }

        private void IzmenaPodataka(object sender, RoutedEventArgs e)
        {
            lbNamestaj.Visibility = Visibility.Hidden;
            borderCentarInfo.Visibility = Visibility.Hidden;
            borderCentarEdit.Visibility = Visibility.Visible;
            tbIme.Text = logovaniKorisnik.Ime;
            tbPrezime.Text = logovaniKorisnik.Prezime;
            tbKorisnickoIme.Text = logovaniKorisnik.KorisnickoIme;
            tbKorisnickoIme.IsReadOnly = true;
            pbSifra.Password = logovaniKorisnik.Lozinka;
            if (logovaniKorisnik.TipKorisnika == TipKorisnika.Administrator)
            {
                tbPozicija.Text = "Administrator";
            }
            else
            {
                tbPozicija.Text = "Prodavac";
            }
            tbPozicija.IsReadOnly = true;
        }

        private void IzadjiEdit(object sender, RoutedEventArgs e)
        {
            borderCentarEdit.Visibility = Visibility.Hidden;
        }
    }
}
