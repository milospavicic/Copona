using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System.Windows;
using MahApps.Metro.Controls;

namespace POP_SF39_2016_GUI.gui
{
    public partial class SalonWindow : MetroWindow
    {
        public Korisnik logovaniKorisnik;
        public Salon mojSalon;
        public SalonWindow(Korisnik logovaniKorisnik)
        {
            InitializeComponent();
            this.logovaniKorisnik = logovaniKorisnik;
            PopuniPolja();
        }

        private void PopuniPolja()
        {
            mojSalon =(Salon) Projekat.Instance.Salon[0].Clone();
            tbNaziv.DataContext = mojSalon;
            tbNaziv.MaxLength = 30;
            tbAdresa.DataContext = mojSalon;
            tbAdresa.MaxLength = 60;
            tbBrojTelefona.DataContext = mojSalon;
            tbBrojTelefona.MaxLength = 30;
            tbEmail.DataContext = mojSalon;
            tbEmail.MaxLength = 30;
            tbWebAdresa.DataContext = mojSalon;
            tbWebAdresa.MaxLength = 60;
            tbBrojRacuna.DataContext = mojSalon;
            tbBrojRacuna.MaxLength = 30;
            tbMaticniBroj.DataContext = mojSalon;
            tbMaticniBroj.MaxLength = 9;
            tbPib.DataContext = mojSalon;
            tbPib.MaxLength = 9;
            if (logovaniKorisnik.TipKorisnika != TipKorisnika.Administrator)
                btnSnimi.IsEnabled = false;
        }

        private void Izadji(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SnimiPromene(object sender, RoutedEventArgs e)
        {
            SalonDAO.Update(mojSalon);
            this.Close();
        }
    }
}
