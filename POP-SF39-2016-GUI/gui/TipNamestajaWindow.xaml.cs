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

        public TipNamestajaWindow(TipNamestaja tipNamestaja, Operacija operacija)
        {
            InitializeComponent();
            this.tipNamestaja = tipNamestaja;
            this.operacija = operacija;
            PopunjavanjePolja(tipNamestaja);
        }
        public void PopunjavanjePolja(TipNamestaja tipNamestaja)
        {
            if (tipNamestaja.Naziv != "")
            {
                tbNaziv.Text = tipNamestaja.Naziv;
            }
            else
            {
                tbNaziv.Text = "";
            }
        }
        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {

            var listaTipaNamestaja = Projekat.Instance.TipNamestaja;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    var noviTipNamestaja = new TipNamestaja()
                    {
                        Id = listaTipaNamestaja.Count + 1,
                        Naziv = this.tbNaziv.Text,

                    };
                    listaTipaNamestaja.Add(noviTipNamestaja);
                    break;
                case Operacija.IZMENA:
                    foreach (TipNamestaja tn in listaTipaNamestaja)
                    {
                        if (tn.Id == tipNamestaja.Id)
                        {
                            tn.Naziv = this.tbNaziv.Text;
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
            Projekat.Instance.TipNamestaja = listaTipaNamestaja;
            this.Close();
        }
        private void ZatvoriWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
