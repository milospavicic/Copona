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
            mojSalon = Projekat.Instance.Salon[0];
            tbNaziv.DataContext = mojSalon;
            tbAdresa.DataContext = mojSalon;
            tbBrojTelefona.DataContext = mojSalon;
            tbEmail.DataContext = mojSalon;
            tbWebAdresa.DataContext = mojSalon;
            tbBrojRacuna.DataContext = mojSalon;
            tbMaticniBroj.DataContext = mojSalon;
            tbPib.DataContext = mojSalon;
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
