using POP_SF39_2016.model;
using POP_SF39_2016.util;
using POP_SF39_2016_GUI.DAO;
using POP_SF39_2016_GUI.model;
using System;
using System.Collections;
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
        public ObservableCollection<Namestaj> ListaNamestaja { get; set; } = new ObservableCollection<Namestaj>();
        public ObservableCollection<Namestaj> ListaNaAkciji { get; set; } = new ObservableCollection<Namestaj>();
        public ObservableCollection<NaAkciji> ListaNA { get; set; } = new ObservableCollection<NaAkciji>();

        private Akcija akcija;
        private Operacija operacija;
        private int index;
        private int popustNA;

        public AkcijaWindow(Akcija akcija, int index, Operacija operacija)
        {
            InitializeComponent();
            this.akcija = akcija;
            this.operacija = operacija;
            this.index = index;
            dgNamestaj.AutoGenerateColumns = false;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            PopunjavanjePolja();
        }

        private void PopunjavanjePolja()
        {
            dpPocetniDatum.DataContext = akcija;
            dpKrajnjiDatum.DataContext = akcija;
            ListaNamestaja = NamestajDAO.GetAllNamestajNotOnAction();
            if (operacija == Operacija.DODAVANJE)
            {
                
                dgNamestaj.ItemsSource = ListaNamestaja;
                dgZaAkciju.ItemsSource = ListaNA;
            } 
            else
            {
                tbPopust.Text = NaAkcijiDAO.GetPopust(akcija.Id).ToString();
                ListaNaAkciji = NaAkcijiDAO.GetAllNamestajForActionId(akcija.Id);
                dgNamestaj.ItemsSource = ListaNamestaja;
                dgZaAkciju.ItemsSource = ListaNaAkciji;
                //dgZaAkciju.IsHitTestVisible = false;
                dgZaAkciju.IsReadOnly = true;
                dgZaAkciju.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                dpPocetniDatum.IsHitTestVisible = false;
                
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
                    var novaAkcija = AkcijaDAO.Create(akcija);
                    foreach (var tempNamestaj in ListaNaAkciji)
                    {
                        var naAkciji = new NaAkciji()
                        {
                            IdAkcije = novaAkcija.Id,
                            IdNamestaja = tempNamestaj.Id,
                            Popust = int.Parse(tbPopust.Text)
                        };
                        NaAkcijiDAO.Create(naAkciji);
                    }
                    break;
                case Operacija.IZMENA:
                    NaAkcijiDAO.SetPopust(akcija.Id, int.Parse(tbPopust.Text));
                    AkcijaDAO.Update(akcija);
                    var ListaZaBrisanje = NaAkcijiDAO.GetAllNamestajForActionId(akcija.Id);
                    foreach (Namestaj tempNamestaj in ListaNaAkciji)
                    {
                        bool postoji = false;
                        foreach(Namestaj tempN in NaAkcijiDAO.GetAllNamestajForActionId(akcija.Id))
                        {
                            if(tempN.Id == tempNamestaj.Id)
                            {
                                postoji = true;
                                ListaZaBrisanje.Remove(tempN);
                                break;
                            }
                        }
                        if (postoji == false)
                        {
                            
                            var naAkciji = new NaAkciji()
                            {
                                IdAkcije = akcija.Id,
                                IdNamestaja = tempNamestaj.Id,
                                Popust = int.Parse(tbPopust.Text)
                            };
                            NaAkcijiDAO.Create(naAkciji);
                        }
                    }
                    foreach (Namestaj tempN in ListaZaBrisanje)
                    {
                        var tempNaAkciji = NaAkcijiDAO.GetForNamestajId(tempN.Id);
                        NaAkcijiDAO.Delete(tempNaAkciji);
                    }

                    break;
            }

            this.Close();
            
        }
        private void ZatvoriWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodajUListu(object sender, RoutedEventArgs e)
        {
            if (popustNA>=1 && popustNA<=99)
            {
                var tempNaAkciji = new NaAkciji
                {
                    IdNamestaja = ((Namestaj)dgNamestaj.SelectedItem).Id,
                    Popust = popustNA,
                };
                ListaNA.Add(tempNaAkciji);
            }
            else
            {
                MessageBoxResult poruka = MessageBox.Show("'Popust' polje mora biti popunjeno.", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            //ListaNaAkciji.Add((Namestaj)dgNamestaj.SelectedItem);
            ListaNamestaja.Remove((Namestaj)dgNamestaj.SelectedItem);
            popustNA = 0;
        }

        private void IzbaciIzAkcije(object sender, RoutedEventArgs e)
        {
            var tempNA = (NaAkciji)dgZaAkciju.SelectedItem;
            ListaNA.Remove(tempNA);
            var tempN = NamestajDAO.GetById(tempNA.IdNamestaja);
            ListaNamestaja.Add(tempN);
        }

        private void PopustProcenat(object sender, RoutedEventArgs e)
        {

            var tb = (TextBox)sender;
            Console.WriteLine(tb.Text);
            try { popustNA = int.Parse(tb.Text); } catch { popustNA=0;}
            
            //DataGridRow row = dgNamestaj.ItemContainerGenerator.ContainerFromIndex
            //    (dgNamestaj.SelectedIndex) as DataGridRow;
            //var i = 5; /// Specify your column index here.
            ////EDIT
            //TextBox ele = ((ContentPresenter)(dgNamestaj.Columns[i].GetCellContent(row))).Content as TextBox;
            //Console.WriteLine(ele.Text);

            //Console.WriteLine(((Namestaj)dgNamestaj.SelectedItem).);
        }
        
    }
}
