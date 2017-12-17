using POP_SF39_2016.model;
using POP_SF39_2016.util;
using POP_SF39_2016_GUI.DAO;
using POP_SF39_2016_GUI.gui;
using POP_SF39_2016_GUI.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP_SF39_2016_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Korisnik logovaniKorisnik { get; set; } = new Korisnik();
        public MainWindow()
        {
            InitializeComponent();
            /***
            var du = DodatnaUslugaDAO.GetById(2);
            Console.WriteLine(du.Naziv);

            //DodatnaUslugaDAO.Delete(du);
            
            du.Obrisan = false;
            DodatnaUslugaDAO.Update(du);
            //-------------
            
            var newDU = new DodatnaUsluga()
            {
                Naziv = "Garancija",
                Cena = 5000,
                Obrisan = false
            };
            DodatnaUslugaDAO.Create(newDU);
            ***/


            /***
            var namestaj = Namestaj.GetById(1);
            namestaj.Naziv = namestaj.Naziv+"321";
            NamestajDAO.Update(namestaj);
            var tnamestaj = TipNamestaja.GetById(1);
            Console.WriteLine(tnamestaj.Naziv);
            tnamestaj.Naziv = "Polica";
            TipNamestajaDAO.Update(tnamestaj);
            
            var noviNamestaj = new Namestaj()
            {
                TipNamestajaId = 2,
                Naziv = "test",
                Sifra = "test",
                BrKomada = 15,
                Cena = 3000.2,
                Obrisan = false
            };
            NamestajDAO.Create(noviNamestaj);
            var noviTipNamestaj = new TipNamestaja()
            {
                Naziv = "test",
                Obrisan = false
            };
            TipNamestajaDAO.Create(noviTipNamestaj);
            ***/
            /***
            var namestaji = NamestajDAO.GetAll();
            foreach(Namestaj namestajj in namestaji)
            {
                Console.WriteLine(namestajj.Naziv);
            }
            ***/

            /***
            List<JedinicaProdaje> lista = Projekat.Instance.JedinicaProdaje.ToList();
            List<int> listaId = lista.Select(predmet => predmet.Id).ToList();
            ObservableCollection<ProdajaNamestaja> lista2 = new ObservableCollection<ProdajaNamestaja>();
            var p1 = new ProdajaNamestaja()
            {
                Id = 1,
                ListaJedinicaProdajeId = listaId,
                Kupac = "pero",
                BrRacuna = "234432",
                DatumProdaje = DateTime.Today,
                UkupnaCena = 515151,
            };
            lista2.Add(p1);
            GenericSerializer.Serialize<ProdajaNamestaja>("prodajenamestaja.xml", lista2);
            ***/
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
                if(korisnik.KorisnickoIme.Equals(tbKorisnickoIme.Text) || korisnik.Lozinka.Equals(passboxSifra.Password))
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
