using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF39_2016.model
{
    public class Akcija : INotifyPropertyChanged, ICloneable
    {
        
        private int id;
        private DateTime pocetakAkcije;
        private DateTime krajAkcije;
        private Namestaj namestaj;
        private int? namestajId;
        private double popust;
        private bool obrisan;
        public Akcija()
        {
            pocetakAkcije = DateTime.Today;
            krajAkcije = DateTime.Today;
        }

        [XmlIgnore]
        public Namestaj Namestaj
        {
            get
            {
                if (namestaj == null)
                {
                    namestaj = Namestaj.GetById(NamestajId);
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

        public DateTime PocetakAkcije
        {
            get { return pocetakAkcije; }
            set
            {
                pocetakAkcije = value;
                OnPropertyChanged("PocetakAkcije");
            }
        }

        public DateTime KrajAkcije
        {
            get { return krajAkcije; }
            set
            {
                krajAkcije = value;
                OnPropertyChanged("KrajAkcije");
            }
        }

        public int? NamestajId
        {
            get { return namestajId; }
            set
            {
                namestajId = value;
                OnPropertyChanged("NamestajId");
            }
        }

        public double Popust
        {
            get { return popust; }
            set
            {
                popust = value;
                OnPropertyChanged("Popust");
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public object Clone()
        {
            return new Akcija()
            {
                Id = id,
                PocetakAkcije = pocetakAkcije,
                KrajAkcije = krajAkcije,
                NamestajId = namestajId,
                Namestaj = namestaj,
                Popust = popust,
                Obrisan = obrisan,
            };
            
        }

        public override string ToString()
        {
            return $"{PocetakAkcije},{KrajAkcije},{Popust},{NamestajId}";
        }
    }
}
