using POP_SF39_2016_GUI.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF39_2016.model
{
    public class Namestaj : INotifyPropertyChanged, ICloneable
    {
        private int id;
        private string naziv;
        private string sifra;
        private double cena;
        private int brKomada;
        private int? tipNamestajaId;
        private bool obrisan;
        private TipNamestaja tipNamestaja;
        public event PropertyChangedEventHandler PropertyChanged;
        bool naAkciji;
        private double akcijskaCena;
        private int tempProcenat;

        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }
        public string Sifra
        {
            get { return sifra; }
            set
            {
                sifra = value;
                OnPropertyChanged("Sifra");
            }
        }
        public TipNamestaja TipNamestaja
        {
            get
            {
                if (tipNamestaja == null)
                {
                    tipNamestaja = TipNamestaja.GetById(TipNamestajaId);
                }
                return tipNamestaja;
            }
            set
            {
                tipNamestaja = value;
                if(tipNamestaja!=null)
                    TipNamestajaId = tipNamestaja.Id;
                OnPropertyChanged("TipNamestaja");
            }
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public double Cena
        {
            get
            {
                return cena;
            }
            set
            {
                cena = value;
                AkcijskaCena = cena;
                OnPropertyChanged("Cena");
            }
        }
        public double AkcijskaCena
        {
            get
            {
                var tempPopust = NaAkcijiDAO.GetPopustForId(Id);
                if(tempPopust!=tempProcenat && tempPopust != 0 && tempProcenat != 0)
                    akcijskaCena = cena - ((cena * tempPopust)) / 100;
                if (tempPopust != 0 && naAkciji != true)
                {
                    akcijskaCena = cena - ((cena * tempPopust)) / 100;
                    naAkciji = true;
                    tempProcenat = tempPopust;
                }
                if(tempPopust!= 0 && naAkciji == true)
                {
                    return akcijskaCena;
                }
                return cena;
            }
            set
            {
                akcijskaCena = value - ((value * tempProcenat)) / 100;
                OnPropertyChanged("AkcijskaCena");
            }
        }

        public double CenaSaPdv
        {
            get { return AkcijskaCena + AkcijskaCena * ProdajaNamestaja.PDV; }
        }
        public int BrKomada
        {
            get { return brKomada; }
            set
            {
                brKomada = value;
                OnPropertyChanged("BrKomada");
            }
        }

        public int? TipNamestajaId
        {
            get { return tipNamestajaId; }
            set
            {
                tipNamestajaId = value;
                OnPropertyChanged("TipNamestajaId");
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
        
        public object Clone()
        {
            return new Namestaj()
            {
                Id = id,
                Naziv = naziv,
                Cena = cena,
                Sifra = sifra,
                Obrisan = obrisan,
                TipNamestaja = tipNamestaja,
                TipNamestajaId = tipNamestajaId,
                BrKomada = brKomada
            };
        }

        public override string ToString()
        {
            return $"{Naziv},{Cena},{TipNamestajaId}";
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public static Namestaj GetById(int? idProsledjen)
        {
            foreach (Namestaj namestaj in Projekat.Instance.Namestaji)
            {
                if (namestaj.id == idProsledjen)
                    return namestaj;
            }
            return null;
        }
    }
}
