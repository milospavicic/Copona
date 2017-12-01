﻿using POP_SF39_2016.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF39_2016_GUI.model
{
    public class JedinicaProdaje : INotifyPropertyChanged, ICloneable
    {
        private int id;
        private int? namestajId;
        private bool obrisan;
        private int kolicina;
        private int? akcijaId;
        private Namestaj namestaj;
        public event PropertyChangedEventHandler PropertyChanged;

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
        public int? NamestajId
        {
            get { return namestajId; }
            set
            {
                namestajId = value;
                OnPropertyChanged("NamestajId");
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

        public int? AkcijaId
        {
            get { return akcijaId; }
            set
            {
                akcijaId = value;
                OnPropertyChanged("AkcijaId");
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
            throw new NotImplementedException();
        }
        public static JedinicaProdaje GetById(int? idProsledjen)
        {
            foreach (JedinicaProdaje jedProdaje in Projekat.Instance.JedinicaProdaje)
            {
                if (jedProdaje.id == idProsledjen)
                    return jedProdaje;
            }
            return null;
        }
    }
}