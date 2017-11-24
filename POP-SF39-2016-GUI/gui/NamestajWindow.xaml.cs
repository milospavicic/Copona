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
    public partial class NamestajWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        private Namestaj namestaj;
        private Operacija operacija;

        public NamestajWindow(Namestaj namestaj, Operacija operacija)
        {
            InitializeComponent();
            this.namestaj = namestaj;
            this.operacija = operacija;
            PopunjavanjePolja(namestaj);
        }
        public void PopunjavanjePolja(Namestaj namestaj)
        {
            List<TipNamestaja> listaTipaNamestaja = Projekat.Instance.TipNamestaja;
            foreach(TipNamestaja tipNamestaja in listaTipaNamestaja)
            {
                cbTipNamestaja.Items.Add(tipNamestaja);
            }
            if (namestaj.Naziv != "")
            {
                tbNaziv.Text = namestaj.Naziv;
                tbSifra.Text = namestaj.Sifra;
                tbCena.Text = namestaj.Cena.ToString();
                tbBrojKomada.Text = namestaj.BrKomada.ToString();
                cbTipNamestaja.SelectedIndex = (int)namestaj.TipNamestajaId;
            }
            else
            {
                tbNaziv.Text = "";
                tbSifra.Text = "";
                tbCena.Text = "0";
                tbBrojKomada.Text = "0";
                cbTipNamestaja.SelectedIndex = 0;
            }         
        }
        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {

            var listaNamestaja = Projekat.Instance.Namestaj;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    var noviNamestaj = new Namestaj()
                    {
                        Id = listaNamestaja.Count + 1,
                        Naziv = this.tbNaziv.Text,
                        Sifra = this.tbSifra.Text,
                        Cena = double.Parse(this.tbCena.Text),
                        BrKomada = int.Parse(this.tbBrojKomada.Text),
                        TipNamestajaId = cbTipNamestaja.SelectedIndex
                    };
                    listaNamestaja.Add(noviNamestaj);
                    break;
                case Operacija.IZMENA:
                    foreach (Namestaj n in listaNamestaja)
                    {
                        if (n.Id == namestaj.Id)
                        {
                            n.Naziv = this.tbNaziv.Text;
                            n.Sifra = this.tbSifra.Text;
                            n.Cena = double.Parse(this.tbCena.Text);
                            n.BrKomada = int.Parse(this.tbBrojKomada.Text);
                            n.TipNamestajaId = cbTipNamestaja.SelectedIndex;
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
            Projekat.Instance.Namestaj = listaNamestaja;
            this.Close();
        }
        private void ZatvoriWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
