using POP_SF39_2016_GUI.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF39_2016.model
{
    public class ProdajaNamestaja : INotifyPropertyChanged, ICloneable
    {
        public const double PDV = 0.02;
        private int id;
        private bool obrisan;
        private List<int> listaJedinicaProdajeId;
        private DateTime datumProdaje;
        private string kupac;
        private string brRacuna;
        private List<int> dodatneUslugeId;
        private double ukupnaCena;
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        

        public List<int> ListaJedinicaProdajeId
        {
            get { return listaJedinicaProdajeId; }
            set
            {
                listaJedinicaProdajeId = value;
                OnPropertyChanged("ListaJedinicaProdajeId");

            }
        }
        public DateTime DatumProdaje
        {
            get { return datumProdaje; }
            set
            {
                datumProdaje = value;
                OnPropertyChanged("DatumProdaje");
            }
        }
        public string Kupac
        {
            get { return kupac; }
            set
            {
                kupac = value;
                OnPropertyChanged("Kupac");
            }
        }
        public string BrRacuna
        {
            get { return brRacuna; }
            set
            {
                brRacuna = value;
                OnPropertyChanged("BrRacuna");
            }
        }
        public List<int> DodatneUslugeId
        {
            get { return dodatneUslugeId; }
            set
            {
                dodatneUslugeId = value;
                OnPropertyChanged("DodatneUsluge");
            }
        }
        public double UkupnaCena
        {
            get { return ukupnaCena; }
            set
            {
                ukupnaCena = value;
                OnPropertyChanged("UkupnaCena");
            }
        }

        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            return new ProdajaNamestaja
            {
                Id = id,
                Kupac = kupac,
                DatumProdaje = datumProdaje,
                BrRacuna = brRacuna,
                DodatneUslugeId = dodatneUslugeId,
                ListaJedinicaProdajeId = listaJedinicaProdajeId,
                UkupnaCena = ukupnaCena,
                Obrisan = obrisan
            };
        }
    }
}
