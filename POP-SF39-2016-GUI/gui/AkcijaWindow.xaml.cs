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
    public partial class AkcijaWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        private Akcija akcija;
        private Operacija operacija;

        public AkcijaWindow(Akcija akcija, Operacija operacija)
        {
            InitializeComponent();
            this.akcija = akcija;
            this.operacija = operacija;
            
            PopunjavanjePolja();
        }

        private void PopunjavanjePolja()
        {
            foreach(Namestaj namestaj in Projekat.Instance.Namestaj)
            {
                cbNamestaj.Items.Add(namestaj.Naziv);
            }
            if (operacija == Operacija.IZMENA)
            {
                dpPocetniDatum.Text = akcija.PocetakAkcije.ToString();
                dpKrajnjiDatum.Text = akcija.KrajAkcije.ToString();
                tbPopust.Text =  akcija.Popust.ToString();
                cbNamestaj.SelectedIndex = ((int)akcija.NamestajId-1);
            }
            else
            {
                dpPocetniDatum.DisplayDateStart = DateTime.Now.Date;
                dpKrajnjiDatum.DisplayDateStart = DateTime.Now.Date;
                tbPopust.Text = "0";
                cbNamestaj.SelectedIndex = 0;
            }
        }

        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            if (dpPocetniDatum.DisplayDate > dpKrajnjiDatum.DisplayDate)
            {
                MessageBoxResult poruka = MessageBox.Show("Krajnji datum ne moze biti veci od pocetnog. ", "Upozorenje", MessageBoxButton.OK);
                return;
            }

            var listaAkcija = Projekat.Instance.Akcija;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    var novAkcija = new Akcija()
                    {
                        Id = listaAkcija.Count + 1,
                        PocetakAkcije = (DateTime)dpPocetniDatum.SelectedDate,
                        KrajAkcije = (DateTime)dpKrajnjiDatum.SelectedDate,
                        Popust = double.Parse(tbPopust.Text),
                        NamestajId = cbNamestaj.SelectedIndex +1
                    };
                    listaAkcija.Add(novAkcija);
                    break;
                case Operacija.IZMENA:
                    foreach (Akcija a in listaAkcija)
                    {
                        if (a.Id == akcija.Id)
                        {
                            a.PocetakAkcije = (DateTime)this.dpPocetniDatum.SelectedDate;
                            a.KrajAkcije = (DateTime)this.dpKrajnjiDatum.SelectedDate;
                            a.Popust = double.Parse(this.tbPopust.Text);
                            a.NamestajId = cbNamestaj.SelectedIndex + 1;
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
            Projekat.Instance.Akcija = listaAkcija;
            this.Close();
            
        }
        private void ZatvoriWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
