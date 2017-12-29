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
    /// Interaction logic for DetaljnijeProdajaWindow.xaml
    /// </summary>
    public partial class DetaljnijeProdajaWindow : Window
    {
        public ProdajaNamestaja ProdajaIzabraniRed;
        public DetaljnijeProdajaWindow(ProdajaNamestaja ProdajaIzabraniRed)
        {
            InitializeComponent();
            this.ProdajaIzabraniRed = ProdajaIzabraniRed;
            tbKupac.IsReadOnly = true;
            tbBrojRacuna.IsReadOnly = true;
            tbDatum.IsReadOnly = true;
            tbUkupnaCena.IsReadOnly = true;
            dgRacun.IsReadOnly = true;
            PopuniPolja();
        }

        private void PopuniPolja()
        {
            tbKupac.DataContext = ProdajaIzabraniRed;
            tbBrojRacuna.DataContext = ProdajaIzabraniRed;
            tbDatum.DataContext = ProdajaIzabraniRed;
            tbUkupnaCena.DataContext = ProdajaIzabraniRed;
            List<Object> tempListJP = (List<Object>)JedinicaProdajeDAO.GetAllForId(ProdajaIzabraniRed.Id).ToList<Object>();
            List<Object> tempListDU = (List<Object>)ProdataDodatnaUslugaDAO.GetAllForId(ProdajaIzabraniRed.Id).ToList<Object>();
            var Korpa = tempListJP.Concat(tempListDU);
            dgRacun.ItemsSource = Korpa;

        }

        private void IgnoreDoubleclick(object sender, MouseButtonEventArgs e)
        {
            return;
        }
    }
}
