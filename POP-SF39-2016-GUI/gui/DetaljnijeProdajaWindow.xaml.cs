using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace POP_SF39_2016_GUI.gui
{
    public partial class DetaljnijeProdajaWindow : MetroWindow
    {
        public ProdajaNamestaja ProdajaIzabraniRed;
        public DetaljnijeProdajaWindow(ProdajaNamestaja ProdajaIzabraniRed)
        {
            InitializeComponent();
            this.ProdajaIzabraniRed = ProdajaIzabraniRed;
            dgRacun.CanUserSortColumns = false;
            dgRacun.CanUserAddRows = false;
            dgRacun.CanUserDeleteRows = false;
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
            this.Title += ProdajaIzabraniRed.BrRacuna;

        }

        private void IgnoreDoubleclick(object sender, MouseButtonEventArgs e)
        {
            return;
        }
        private void Indexiranje(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()).ToString();
        }
    }
}
