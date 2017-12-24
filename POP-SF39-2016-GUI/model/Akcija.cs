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
        private bool obrisan;
        public Akcija()
        {
            pocetakAkcije = DateTime.Today;
            krajAkcije = DateTime.Today;
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
                Obrisan = obrisan,
            };
            
        }

        public override string ToString()
        {
            return $"{PocetakAkcije},{KrajAkcije}";
        }
    }
}
