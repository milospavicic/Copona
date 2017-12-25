using POP_SF39_2016.model;
using POP_SF39_2016.util;
using POP_SF39_2016_GUI.DAO;
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
    private int index;


    public NamestajWindow(Namestaj namestaj, int index, Operacija operacija)
    {
        InitializeComponent();
        this.namestaj = namestaj;
        this.operacija = operacija;
        this.index = index;
        PopunjavanjePolja(namestaj);
    }
    public void PopunjavanjePolja(Namestaj namestaj)
    {
        cbTipNamestaja.ItemsSource = Projekat.Instance.TipoviNamestaja;
        tbNaziv.DataContext = namestaj;
        tbSifra.DataContext = namestaj;
        tbCena.DataContext = namestaj;
        tbBrojKomada.DataContext = namestaj;
        cbTipNamestaja.DataContext = namestaj;   
    }
    private void SacuvajIzmene(object sender, RoutedEventArgs e)
    {
        try
        {
            int.Parse(tbBrojKomada.Text);
            int.Parse(tbCena.Text);
        }
        catch
        {
            MessageBoxResult poruka = MessageBox.Show("Polja moraju biti brojevi. ", "Upozorenje", MessageBoxButton.OK);
            return;
        }
        if (cbTipNamestaja.SelectedItem == null)
        {
            MessageBoxResult poruka = MessageBox.Show("Polja ne smeju biti prazna. ", "Upozorenje", MessageBoxButton.OK);
            return;
        }
        var listaNamestaja = Projekat.Instance.Namestaji;
        switch (operacija)
        {
            case Operacija.DODAVANJE:
                NamestajDAO.Create(namestaj);
                break;
                 
            case Operacija.IZMENA:
                NamestajDAO.Update(namestaj);
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
