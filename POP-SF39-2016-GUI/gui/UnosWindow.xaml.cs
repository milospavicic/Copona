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
    /// Interaction logic for UnosWindow.xaml
    /// </summary>
    public partial class UnosWindow : Window
    {
        public int PopustNamestaja { get; set; }
        public int Kolicina { get; set; }
        public enum Opcija
        {
            POPUST,
            KOLICINA
        }
        private Opcija opcija;
        private int max;
        private int vecUneto;
        public UnosWindow(Opcija opcija,int max, int vecUneto)
        {
            InitializeComponent();
            this.max = max;
            this.vecUneto = vecUneto;
            this.opcija = opcija;
            tbUnos.Focus();
            Popuni();
        }

        private void Popuni()
        {
            switch (opcija)
            {
                case Opcija.POPUST:
                    this.Title = "Unos procenta popusta.";
                    tbPoruka.Text = "Molimo vas unesite popust za izabrani namestaj.";
                    break;
                case Opcija.KOLICINA:
                    this.Title = "Unos kolicine namestaja.";
                    tbPoruka.Text = "Molimo vas unesite kolicinu izabranog namestaja.";
                    break;
            }
        }

        private void Izadji(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void UnetiPopust(object sender, RoutedEventArgs e)
        {

            switch (opcija)
            {
                case Opcija.POPUST:
                    int tempPopust = 0;
                    try { tempPopust = int.Parse(tbUnos.Text); }
                    catch
                    {
                        MessageBoxResult poruka = MessageBox.Show("Unos mora biti broj.", "Upozorenje!", MessageBoxButton.OK);
                        return;
                    }
                    if (tempPopust > 99 || tempPopust < 1)
                    {
                        MessageBoxResult poruka = MessageBox.Show("Popust moze biti samo pozitivan broj.", "Upozorenje!", MessageBoxButton.OK);
                        return;
                    }
                    PopustNamestaja = tempPopust;
                    this.DialogResult = true;
                    this.Close();
                    break;
                case Opcija.KOLICINA:
                    int tempKolicina = 0;
                    try { tempKolicina = int.Parse(tbUnos.Text); }
                    catch
                    {
                        MessageBoxResult poruka = MessageBox.Show("Unos mora biti broj.", "Upozorenje!", MessageBoxButton.OK);
                        return;
                    }
                    if (tempKolicina<1)
                    {
                        {
                            MessageBoxResult poruka = MessageBox.Show("Morate uneti pozitivan broj", "Upozorenje", MessageBoxButton.OK);
                            return;
                        }
                    }
                    if (tempKolicina + vecUneto > max)
                    {
                        {
                            MessageBoxResult poruka = MessageBox.Show("Nema dovoljno komada!", "Upozorenje", MessageBoxButton.OK);
                            return;
                        }
                    }
                    Kolicina = tempKolicina;
                    this.DialogResult = true;
                    this.Close();
                    break;

            }
        }
    }
}
