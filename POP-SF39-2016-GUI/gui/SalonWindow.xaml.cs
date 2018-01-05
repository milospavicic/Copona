using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
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
    /// Interaction logic for SalonWindow.xaml
    /// </summary>
    public partial class SalonWindow : Window
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
