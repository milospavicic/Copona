using POP_SF39_2016.model;
using POP_SF39_2016_GUI.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF39_2016_GUI.model
{
    public class JedinicaProdaje : INotifyPropertyChanged
    {
        private int id;
        private int namestajId;
        private int prodajaId;
        private bool obrisan;
        private int kolicina;
        private Namestaj namestaj;
        public event PropertyChangedEventHandler PropertyChanged;
        private double cenaUkupno;
        private double cenaUkupnoPdv;
        private double cena;

        [XmlIgnore]
        public Namestaj Namestaj
        {
            get
            {
                if (namestaj == null)
                {
                    namestaj = NamestajDAO.GetById(NamestajId);
                }
                return namestaj;
            }
            set
            {
                namestaj = value;
                NamestajId = namestaj.Id;
                OnPropertyChanged("Namestaj");
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
        public int NamestajId
        {
            get { return namestajId; }
            set
            {
                namestajId = value;
                OnPropertyChanged("NamestajId");
            }
        }
        public int ProdajaId
        {
            get { return prodajaId; }
            set
            {
                prodajaId = value;
                OnPropertyChanged("ProdajaId");
            }
        }
        public int Kolicina
        {
            get { return kolicina; }
            set
            {
                kolicina = value;
                OnPropertyChanged("Kolicina");
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
        public string Naziv
        {
            get
            {
                return Namestaj.Naziv;
            }
        }
        [XmlIgnore]
        public double Cena
        {
            get
            {
                if (cena == 0)
                {
                    cena = Namestaj.AkcijskaCena;
                }
                return cena;
            }
            set
            {
                cena = value;
            }
        }
        public double CenaSaPdv
        {
            get
            {
                return Cena + Cena * ProdajaNamestaja.PDV;
            }
        }
        public double CenaUkupno
        {
            get
            {  
                if (cenaUkupno == 0)
                {
                    cenaUkupno = Cena * Kolicina;
                }
                return cenaUkupno;
            }
            set
            {
                cenaUkupno = value;
                OnPropertyChanged("CenaUkupno");
            }

        }
        public double CenaUkupnoPDV
        {
            get
            {
                if (cenaUkupnoPdv == 0)
                {
                    cenaUkupnoPdv = (Cena + Cena * ProdajaNamestaja.PDV) * Kolicina;
                }
                return cenaUkupnoPdv;
            }
            set
            {
                cenaUkupnoPdv = value;
                OnPropertyChanged("CenaUkupnoPDV");
            }
        }
    }
}
