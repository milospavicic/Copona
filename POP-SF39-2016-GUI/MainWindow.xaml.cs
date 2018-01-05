using POP_SF39_2016.model;
using POP_SF39_2016_GUI.gui;
using System;
using System.Windows;

namespace POP_SF39_2016_GUI
{
    public partial class MainWindow : Window
    {
        public Korisnik logovaniKorisnik { get; set; } = new Korisnik();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult poruka = MessageBox.Show("Da li ste sigurni?", "Izlazak", MessageBoxButton.YesNo);
            if (poruka == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            };
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tbKorisnickoIme.Text.Equals("") || passboxSifra.Password.Equals(""))
            {

                MessageBoxResult poruka = MessageBox.Show("Polja ne smeju biti prazna. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            foreach(Korisnik korisnik in Projekat.Instance.Korisnici)
            {
                if(korisnik.KorisnickoIme.Equals(tbKorisnickoIme.Text) && korisnik.Lozinka.Equals(passboxSifra.Password))
                {
                    this.Hide();
                    logovaniKorisnik = korisnik;
                    GlavniWindow noviGlavniWindow = new GlavniWindow(logovaniKorisnik);
                    noviGlavniWindow.Show();
                    break;
                }
            }
        }
    }
}
