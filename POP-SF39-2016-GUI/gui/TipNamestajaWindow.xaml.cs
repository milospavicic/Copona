using POP_SF39_2016.model;
using POP_SF39_2016.util;
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
    /// Interaction logic for TipNamestajaWindow.xaml
    /// </summary>
    public partial class TipNamestajaWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        private TipNamestaja tipNamestaja;
        private Operacija operacija;
        private int index;

        public TipNamestajaWindow(TipNamestaja tipNamestaja, int index, Operacija operacija)
        {
            InitializeComponent();
            this.tipNamestaja = tipNamestaja;
            this.operacija = operacija;
            this.index = index;
            PopunjavanjePolja(tipNamestaja);
        }
        public void PopunjavanjePolja(TipNamestaja tipNamestaja)
        {
            tbNaziv.DataContext = tipNamestaja;
        }
        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {

            var listaTipaNamestaja = Projekat.Instance.TipoviNamestaja;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    TipNamestajaDAO.Create(tipNamestaja);
                    break;
                case Operacija.IZMENA:
                    TipNamestajaDAO.Update(tipNamestaja);
                    break;
            }
            this.Close();
        }
        private void ZatvoriWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
