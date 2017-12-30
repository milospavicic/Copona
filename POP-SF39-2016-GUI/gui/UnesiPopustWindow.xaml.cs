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
    /// Interaction logic for UnesiPopustWindow.xaml
    /// </summary>
    public partial class UnesiPopustWindow : Window
    {
        public int PopustNamestaja { get; set; }
        public UnesiPopustWindow()
        {
            InitializeComponent();
        }

        private void Izadji(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void UnetiPopust(object sender, RoutedEventArgs e)
        {
            int tempPopust = 0;
            try { tempPopust = int.Parse(tbPopust.Text); }
            catch
            {
                MessageBoxResult poruka = MessageBox.Show("Unos mora biti broj.", "Upozorenje!", MessageBoxButton.OK);
                return;
            }
            if(tempPopust>99 || tempPopust < 1)
            {
                MessageBoxResult poruka = MessageBox.Show("Popust moze biti samo pozitivan broj.", "Upozorenje!", MessageBoxButton.OK);
                return;
            }
            PopustNamestaja = tempPopust;
            this.DialogResult = true;
            this.Close();
        }
    }
}
