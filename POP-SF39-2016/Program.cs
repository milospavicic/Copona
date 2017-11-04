using POP_SF39_2016.model;
using POP_SF39_2016.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF39_2016
{
    class Program
    {
        public static List<Namestaj> ListaNamestaja { get; set; }
        public static List<TipNamestaja> ListaTipoviNamestaja { get; set; } 
        public static List<Korisnik> ListaKorisnika { get; set; }
        public static List<Akcija> ListaAkcija { get; set; } = new List<Akcija>();
        public static List<DodatnaUsluga> ListaUsluga { get; set; } = new List<DodatnaUsluga>();
        public static List<Salon> ListaSalona { get; set; } = new List<Salon>();
        public List<Namestaj> newListNamestaja { get; set; } = new List<Namestaj>();
        static void Main(string[] args)
        {
            var s1 = new Salon()
            {
                Id = 1,
                Naziv = "Forma FTNale",
                Adresa = "Trg Dositeja Obradovica 6",
                BrRacuna = "81515151-13141",
                Email = "123@ftn.com",
                MaticniBr = 5125151,
                Pib = 15151,
                BrojTelefona = "1235415151",
                WebAdresa = "http://TestSite.jeftino.com"
            };
            ListaSalona.Add(s1);
            var a1 = new Akcija()
            {
                Id = 1,
                PocetakAkcije = new DateTime(2017, 1, 18),
                KrajAkcije = new DateTime(2017, 1, 21),
                NamestajId = 1,
                Popust = 10,
            };
            ListaAkcija.Add(a1);

            var du1 = new DodatnaUsluga()
            {
                Id = 1,
                Naziv = "Prevoz",
                Cena = 300,
            };
            ListaUsluga.Add(du1);

            //GenericSerializer.Serialize<Namestaj>("namestaj.xml", ListaNamestaja);
            //GenericSerializer.Serialize<TipNamestaja>("tipnamestaja.xml", ListaTipoviNamestaja);
            //GenericSerializer.Serialize<Korisnik>("korisnici.xml", ListaKorisnika);
            ListaKorisnika = Projekat.Instance.Korisnik;
            ListaNamestaja = Projekat.Instance.Namestaj;
            ListaTipoviNamestaja = Projekat.Instance.TipNamestaja;

            IspisGlavnogMenija();
        }
        //==============================================================================
        private static void IspisGlavnogMenija()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("GLAVNI MENI");
                Console.WriteLine("1.Rad sa namestajem");
                Console.WriteLine("2.Rad sa tipom namestaja");
                Console.WriteLine("3.Rad sa korisnicima");
                Console.WriteLine("4.Rad sa akcijama");
                Console.WriteLine("5.Rad sa dodatnim uslugama");
                Console.WriteLine("6.Rad sa salonima namestaja");
                Console.WriteLine("0.Izlaz");
                izbor = int.Parse(Console.ReadLine());
                // ZAVRSITI MENI
            } while (izbor < 0 || izbor > 6);

            switch (izbor)
            {
                case 1:
                    IspisiMeniNamestaja();
                    break;
                case 2:
                    IspisMeniTipNamestaja();
                    break;
                case 3:
                    IspisMeniKorisnici();
                    break;
                case 4:
                    IspisMeniAkcija();
                    break;
                case 5:
                    IspisMeniDodatneUsluge();
                    break;
                case 6:
                    IspisMeniSalon();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }


        private static void IspisiMeniNamestaja()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("MENI NAMESTAJA");
                Console.WriteLine("1. Izlistaj");
                Console.WriteLine("2. Dodaj novi namestaj");
                Console.WriteLine("3. Izmeni postojeci namestaj");
                Console.WriteLine("4. Obrisi postojeci");
                Console.WriteLine("0. Povratak na glavni meni");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    IzlistajNamestaj();
                    break;
                case 2:
                   DodajNoviNamestaj();
                    break;
                case 3:
                    IzmeniPostojeciNamestaj();
                    break;
                case 4:
                    ObrisiPostojeciNamestaj();
                    break;
                case 0:
                    IspisGlavnogMenija();
                    break;
                default:
                    break;
            }
        }
        private static void IspisMeniTipNamestaja()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("MENI TIP NAMESTAJA");
                Console.WriteLine("1. Izlistaj");
                Console.WriteLine("2. Dodaj novi tip namestaja");
                Console.WriteLine("3. Izmeni postojeci tip namestaja");
                Console.WriteLine("4. Obrisi postojeci tip namestaja");
                Console.WriteLine("0. Povratak na glavni meni");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    IzlistajTipoveNamestaja();
                    break;
                case 2:
                    DodajNoviTipNamestaja();
                    break;
                case 3:
                    IzmeniPostojeciTipNamestaja();
                    break;
                case 4:
                    ObrisiPostojeciTipNamestaja();
                    break;
                case 0:
                    IspisGlavnogMenija();
                    break;
                default:
                    break;
            }
        }
        private static void IspisMeniKorisnici()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("MENI KORISNICI");
                Console.WriteLine("1. Izlistaj");
                Console.WriteLine("2. Dodaj novog korisnika");
                Console.WriteLine("3. Izmeni postojeceg korisnika");
                Console.WriteLine("4. Obrisi postojeceg korisnika");
                Console.WriteLine("0. Povratak na glavni meni");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    IzlistajKorisnike();
                    break;
                case 2:
                    DodajNovogKorisnika();
                    break;
                case 3:
                    IzmeniPostojecegKorisnika();
                    break;
                case 4:
                    ObrisiPostojecegKorisnika();
                    break;
                case 0:
                    IspisGlavnogMenija();
                    break;
                default:
                    break;
            }
        }
        private static void IspisMeniAkcija()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("MENI AKCIJE");
                Console.WriteLine("1. Izlistaj");
                Console.WriteLine("2. Dodaj novu akciju");
                Console.WriteLine("3. Izmeni postojecu akciju");
                Console.WriteLine("4. Obrisi postojecu akciju");
                Console.WriteLine("0. Povratak na glavni meni");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    IzlistajAkcije();
                    break;
                case 2:
                    DodajNovuAkciju();
                    break;
                case 3:
                    IzmeniPostojecuAkciju();
                    break;
                case 4:
                    ObrisiPostojecuAkciju();
                    break;
                case 0:
                    IspisGlavnogMenija();
                    break;
                default:
                    break;
            }
        }
        private static void IspisMeniDodatneUsluge()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("MENI DODATNE USLUGE");
                Console.WriteLine("1. Izlistaj");
                Console.WriteLine("2. Dodaj novu uslugu");
                Console.WriteLine("3. Izmeni postojecu uslugu");
                Console.WriteLine("4. Obrisi postojecu uslugu");
                Console.WriteLine("0. Povratak na glavni meni");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    IzlistajUsluge();
                    break;
                case 2:
                    DodajNovuUslugu();
                    break;
                case 3:
                    IzmeniPostojecuUslugu();
                    break;
                case 4:
                    ObrisiPostojecuUslugu();
                    break;
                case 0:
                    IspisGlavnogMenija();
                    break;
                default:
                    break;
            }
        }
        private static void IspisMeniSalon()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("MENI SALONI NAMESTAJA");
                Console.WriteLine("1. Izlistaj");
                Console.WriteLine("2. Dodaj novi salon");
                Console.WriteLine("3. Izmeni postojeci salon");
                Console.WriteLine("4. Obrisi postojeci salon");
                Console.WriteLine("0. Povratak na glavni meni");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    IzlistajSalone();
                    break;
                case 2:
                    DodajNoviSalon();
                    break;
                case 3:
                    IzmeniPostojeciSalon();
                    break;
                case 4:
                    ObrisiPostojeciSalon();
                    break;
                case 0:
                    IspisGlavnogMenija();
                    break;
                default:
                    break;
            }
        }
        private static void IzlistajNamestaj()
        {
            Console.WriteLine("Izlistavanje namestaja.. \n");
            for (int i = 0; i < ListaNamestaja.Count; i++)
            {
                TipNamestaja trenutniTip = null;
                if (!ListaNamestaja[i].Obrisan)
                {
                    foreach (TipNamestaja tip in ListaTipoviNamestaja)
                        if (ListaNamestaja[i].TipNamestajaId == tip.Id)
                            trenutniTip = tip;
                    
                    Console.WriteLine($"{i + 1}.{ListaNamestaja[i].Naziv},cena:{ListaNamestaja[i].Cena},tip namestaja:{trenutniTip.Naziv}, akcija:{ListaNamestaja[i].AkcijaId}");
                }
            }
            Console.WriteLine("\nGotovo izlistavanje \n");
            IspisiMeniNamestaja();
        }
        private static void DodajNoviNamestaj()
        {
            TipNamestaja TrazeniTipNamestaja = null;
            Namestaj NoviNamestaj = new Namestaj();
            NoviNamestaj.Id = ListaNamestaja.Count + 1;
            Console.WriteLine("Unesite naziv namestaja");
            NoviNamestaj.Naziv = Console.ReadLine();
            Console.WriteLine("Unesite sifru namestaja");
            NoviNamestaj.Sifra = Console.ReadLine();
            Console.WriteLine("Unesite cenu");
            NoviNamestaj.Cena = double.Parse(Console.ReadLine());
            do
            {
                Console.WriteLine("Unesite tip namestaja");
                string UnetiTip = Console.ReadLine();

                foreach (TipNamestaja Tip in ListaTipoviNamestaja)
                {
                    if (UnetiTip.Equals(Tip.Naziv))
                    {
                        TrazeniTipNamestaja = Tip;
                    }
                }
            } while (TrazeniTipNamestaja == null);
            NoviNamestaj.TipNamestajaId = TrazeniTipNamestaja.Id;
            ListaNamestaja.Add(NoviNamestaj);
            Projekat.Instance.Namestaj = ListaNamestaja;
            IspisiMeniNamestaja();
        }
        private static void IzmeniPostojeciNamestaj()
        {
            Namestaj namestajZaIzmenu = null;
            do
            {
                Console.WriteLine("Unesite ime namestaja koji zelite da izmenite");
                string nazivTrazenogNamestaja = Console.ReadLine();
                foreach (Namestaj trenutniNamestaj in ListaNamestaja)
                {
                    if (trenutniNamestaj.Naziv.Equals(nazivTrazenogNamestaja))
                    {
                        namestajZaIzmenu = trenutniNamestaj;
                    }
                }
            } while (namestajZaIzmenu == null);

            int izbor = 0;
            do
            {
                Console.WriteLine("Sta zelite da izmenite?");
                Console.WriteLine("1. Naziv");
                Console.WriteLine("2. Sifra");
                Console.WriteLine("3. Cena");
                Console.WriteLine("4. Tip namestaja");
                Console.WriteLine("0. Povratak na glavni meni");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    string novNaziv = null;
                    do
                    {
                        Console.WriteLine("Unesite nov naziv");
                        novNaziv = Console.ReadLine();
                    } while (novNaziv == null || novNaziv == "" || novNaziv.Equals(namestajZaIzmenu.Naziv));
                    namestajZaIzmenu.Naziv = novNaziv;
                    break;

                case 2:
                    string novaSifra = null;
                    do
                    {
                        Console.WriteLine("Unesite novu sifru");
                        novaSifra = Console.ReadLine();
                    } while (novaSifra == null || novaSifra == "" || novaSifra.Equals(namestajZaIzmenu.Sifra));
                    namestajZaIzmenu.Sifra = novaSifra;
                    break;

                case 3:
                    double novaCena = 0;
                    do
                    {
                        Console.WriteLine("Unesite novu cenu");
                        novaCena = double.Parse(Console.ReadLine());
                    } while (novaCena <= 0 || novaCena == namestajZaIzmenu.Cena);
                    namestajZaIzmenu.Cena = novaCena;
                    break;

                case 4:
                    TipNamestaja noviTip = null;
                    do
                    {
                        Console.WriteLine("Unesite novi tip namestaja");
                        string unetiTip = Console.ReadLine();
                        foreach (TipNamestaja tip in ListaTipoviNamestaja)
                        {
                            if (unetiTip.Equals(tip.Naziv))
                                noviTip = tip;
                        }
                    } while (noviTip == null || noviTip.Id == namestajZaIzmenu.TipNamestajaId);
                    namestajZaIzmenu.TipNamestajaId = noviTip.Id;
                    break;

                case 0:
                    break;

                default:
                    break;
            }
            Projekat.Instance.Namestaj = ListaNamestaja;
            IspisiMeniNamestaja();
        }
        private static void ObrisiPostojeciNamestaj()
        {
            Namestaj namestajZaBrisanje = null;
            do
            {
                Console.WriteLine("Unesite naziv namestaja");
                string unetiNaziv = Console.ReadLine();
                foreach (Namestaj trenutniNamestaj in ListaNamestaja)
                {
                    if (trenutniNamestaj.Naziv.Equals(unetiNaziv) & trenutniNamestaj.Obrisan == false)
                        namestajZaBrisanje = trenutniNamestaj;
                }
            } while (namestajZaBrisanje == null);
            Console.WriteLine("Brisanje uspesno");
            namestajZaBrisanje.Obrisan = true;
            Projekat.Instance.Namestaj = ListaNamestaja;
            IspisiMeniNamestaja();
        }
        //==============================================================================
        private static void IzlistajTipoveNamestaja()
        {
            
            Console.WriteLine("Izlistavanje tipova namestaja");
            int index = 1;
            foreach (TipNamestaja trenutniTip in ListaTipoviNamestaja)
            {
                if (!trenutniTip.Obrisan)
                    Console.WriteLine(index + ". " + trenutniTip.Naziv);
                index++;
            }
            Console.WriteLine("Gotovo izlistavanje \n");
            IspisMeniTipNamestaja();
        }
        private static void DodajNoviTipNamestaja()
        {
            TipNamestaja NoviTipNamestaja = new TipNamestaja();
            NoviTipNamestaja.Id = ListaTipoviNamestaja.Count + 1;
            Console.WriteLine("Unesite naziv tipa namestaja");
            NoviTipNamestaja.Naziv = Console.ReadLine();
            NoviTipNamestaja.Obrisan = false;
            ListaTipoviNamestaja.Add(NoviTipNamestaja);
            IspisMeniTipNamestaja();
        }
        private static void IzmeniPostojeciTipNamestaja()
        {
            TipNamestaja tipNamestajaZaIzmenu = null;
            do
            {
                Console.WriteLine("Unesite naziv tipa namestaja za izmenu");
                string unetiNaziv = Console.ReadLine();
                foreach (TipNamestaja trenutniTip in ListaTipoviNamestaja)
                    if (unetiNaziv.Equals(trenutniTip.Naziv))
                        tipNamestajaZaIzmenu = trenutniTip;
            } while (tipNamestajaZaIzmenu == null);
            int izbor = 0;
            do
            {
                Console.WriteLine("Sta zelite da izmenite?");
                Console.WriteLine("1. Naziv");
                Console.WriteLine("0. Povratak na meni tipa namestaja");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 1);
            switch (izbor)
            {
                case 1:
                    string novNaziv = null;
                    do
                    {
                        Console.WriteLine("Unesite nov naziv");
                        novNaziv = Console.ReadLine();
                    } while (novNaziv == null || novNaziv == "" || novNaziv.Equals(tipNamestajaZaIzmenu.Naziv));
                    tipNamestajaZaIzmenu.Naziv = novNaziv;
                    IspisMeniTipNamestaja();
                    break;
            }
        }
        private static void ObrisiPostojeciTipNamestaja()
        {
            TipNamestaja tipNamestajaZaBrisanje = null;
            do
            {
                Console.WriteLine("Unesite naziv tipa namestaja");
                string unetiNaziv = Console.ReadLine();
                foreach (TipNamestaja trenutniTip in ListaTipoviNamestaja)
                {
                    if (trenutniTip.Naziv.Equals(unetiNaziv) & trenutniTip.Obrisan == false)
                        tipNamestajaZaBrisanje = trenutniTip;
                }
            } while (tipNamestajaZaBrisanje == null);
            Console.WriteLine("Brisanje uspesno");
            tipNamestajaZaBrisanje.Obrisan = true;
            IspisMeniTipNamestaja();
        }
        //==============================================================================
        private static void IzlistajKorisnike()
        {
            Console.WriteLine("Izlistavanje korisnika ");
            int index = 1;
            foreach (Korisnik trenutniKorisnik in ListaKorisnika)
            {
                if (!trenutniKorisnik.Obrisan)
                    Console.WriteLine(index + ". " + trenutniKorisnik.Ime + " " + trenutniKorisnik.Prezime + ", " + trenutniKorisnik.TipKorisnika);
                index++;
            }
            Console.WriteLine("Gotovo izlistavanje \n");
            IspisMeniKorisnici();
        }
        private static void DodajNovogKorisnika()
        {
            Korisnik noviKorisnik = new Korisnik();
            noviKorisnik.Id = ListaKorisnika.Count + 1;
            Console.WriteLine("Unesite ime korisnika");
            noviKorisnik.Ime = Console.ReadLine();
            Console.WriteLine("Unesite prezime korisnika");
            noviKorisnik.Prezime = Console.ReadLine();
            Console.WriteLine("Unesite korisnicko ime korisnika");
            noviKorisnik.KorisnickoIme = Console.ReadLine();
            Console.WriteLine("Unesite lozinku korisnika");
            noviKorisnik.Lozinka = Console.ReadLine();
            int tipKorisnika = 0;
            do
            {
                Console.WriteLine("Unesite tip korisnika \n1. Administrator \n2. Prodavac");
                tipKorisnika = int.Parse(Console.ReadLine());
            } while (tipKorisnika < 0 || tipKorisnika > 2);
            switch (tipKorisnika)
            {
                case 1:
                    noviKorisnik.TipKorisnika = TipKorisnika.Administrator;
                    break;
                case 2:
                    noviKorisnik.TipKorisnika = TipKorisnika.Prodavac;
                    break;
            }
            noviKorisnik.Obrisan = false;
            Console.WriteLine("Novi korisnik je dodat");
            ListaKorisnika.Add(noviKorisnik);
            IspisMeniKorisnici();
        }
        private static void IzmeniPostojecegKorisnika()
        {
            Korisnik korisnikZaIzmenu = null;
            do
            {
                Console.WriteLine("Unesite korisnicko ime korisnika za izmenu");
                string unetoIme = Console.ReadLine();
                foreach (Korisnik trenutniKorisnik in ListaKorisnika)
                    if (trenutniKorisnik.KorisnickoIme.Equals(unetoIme))
                        korisnikZaIzmenu = trenutniKorisnik;
            } while (korisnikZaIzmenu == null);
            int izbor = 0;
            do
            {
                Console.WriteLine("Sta zelite da izmenite?");
                Console.WriteLine("1. Ime");
                Console.WriteLine("2. Prezime");
                Console.WriteLine("3. Lozinku");
                Console.WriteLine("4. Tip korisnika");
                Console.WriteLine("0. Povratak na meni korisnika");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    string novoIme = null;
                    do
                    {
                        Console.WriteLine("Unesite novo ime");
                        novoIme = Console.ReadLine();
                    } while (novoIme == null || novoIme == "" || novoIme.Equals(korisnikZaIzmenu.Ime));
                    korisnikZaIzmenu.Ime = novoIme;
                    IspisMeniKorisnici();
                    break;

                case 2:
                    string novoPrezime = null;
                    do
                    {
                        Console.WriteLine("Unesite novo prezime");
                        novoPrezime = Console.ReadLine();
                    } while (novoPrezime == null || novoPrezime == "" || novoPrezime.Equals(korisnikZaIzmenu.Prezime));
                    korisnikZaIzmenu.Prezime = novoPrezime;
                    IspisMeniKorisnici();
                    break;

                case 3:
                    string novaLozinka = null;
                    do
                    {
                        Console.WriteLine("Unesite novu lozinku");
                        novaLozinka = Console.ReadLine();
                    } while (novaLozinka == null || novaLozinka == "" || novaLozinka.Equals(korisnikZaIzmenu.Lozinka));
                    korisnikZaIzmenu.Lozinka = novaLozinka;
                    IspisMeniKorisnici();
                    break;

                case 4:
                    int tipKorisnika = 0;
                    do
                    {
                        Console.WriteLine("Unesite tip korisnika \n1. Administrator \n2. Prodavac");
                        tipKorisnika = int.Parse(Console.ReadLine());
                    } while (tipKorisnika < 0 || tipKorisnika > 2);
                    switch (tipKorisnika)
                    {
                        case 1:
                            korisnikZaIzmenu.TipKorisnika = TipKorisnika.Administrator;
                            break;
                        case 2:
                            korisnikZaIzmenu.TipKorisnika = TipKorisnika.Prodavac;
                            break;
                    }
                    IspisMeniKorisnici();
                    break;

                case 0:
                    IspisMeniKorisnici();
                    break;
            }
        }
        private static void ObrisiPostojecegKorisnika()
        {
            Korisnik korisnikZaBrisanje = null;
            do
            {
                Console.WriteLine("Unesite korisnicko ime korisnika za brisanje");
                string unetoIme = Console.ReadLine();
                foreach (Korisnik trenutniKorisnik in ListaKorisnika)
                    if (trenutniKorisnik.KorisnickoIme.Equals(unetoIme))
                        korisnikZaBrisanje = trenutniKorisnik;
            } while (korisnikZaBrisanje == null);
            korisnikZaBrisanje.Obrisan = true;
            Console.WriteLine("Korisnik je obrisan \n");
            IspisMeniKorisnici();
        }
        //==============================================================================
        private static void Login()
        {
            bool korisnikLogin = false;
            int pokusaj = 0;
            do
            {
                Console.WriteLine("Unesite vase korisnicko ime");
                string korisnickoIme = Console.ReadLine();
                Console.WriteLine("Unesite vasu lozinku");
                string sifra = Console.ReadLine();
                foreach (Korisnik trenutniKorisnik in ListaKorisnika)
                    if (trenutniKorisnik.KorisnickoIme == korisnickoIme & trenutniKorisnik.Lozinka == sifra && trenutniKorisnik.Obrisan != true)
                        korisnikLogin = true;
                pokusaj++;
                if (pokusaj >= 3)
                    Environment.Exit(0);


            } while (korisnikLogin == false);
            Console.WriteLine();
        }
        //==============================================================================
        private static void IzlistajAkcije()
        {
            int index = 1;
            foreach(Akcija akcija in ListaAkcija)
            {
                Namestaj namestajNaAkciji = null;
                if (!akcija.Obrisan)
                {
                    foreach (Namestaj namestaj in ListaNamestaja)
                        if (akcija.NamestajId == namestaj.Id)
                            namestajNaAkciji = namestaj;
                     Console.WriteLine(index + ". Pocetak: " +akcija.PocetakAkcije.Date+ ", Kraj: "+akcija.KrajAkcije.Date + ", Za :"+namestajNaAkciji.Naziv);
                }
                index++;
            }
            IspisMeniAkcija();
        }
        private static void DodajNovuAkciju()
        {
            Akcija novaAkcija = new Akcija();
            novaAkcija.Id = ListaAkcija.Count + 1;
            bool testDate =false;
            bool compareDate = false;
            do
            {
                try
                {
                    Console.WriteLine("Unesite pocetni datum (YYYY/MM/DD)");
                    DateTime pocetniDatum = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Unesite zavrsni datum (YYYY/MM/DD)");
                    DateTime zavrsniDatum = DateTime.Parse(Console.ReadLine());
                    if (zavrsniDatum < pocetniDatum)
                    {
                        compareDate = true;
                        throw new Exception();
                    }
                    testDate = true;
                    novaAkcija.PocetakAkcije = pocetniDatum;
                    novaAkcija.KrajAkcije = zavrsniDatum;
                }
                catch
                {
                    if (compareDate == true)
                        Console.WriteLine("Krajnji datum ne moze biti manji od pocetnog. \n");
                    else
                    Console.WriteLine("Pogresan format datuma. \n");
                    
                }
            } while (testDate == false);

            Namestaj namestajZaAkciju = null;
            do
            {
                Console.WriteLine("Unesite naziv namestaja koji je na akciji");
                string unetiNamestaj = Console.ReadLine();
                foreach (Namestaj namestaj in ListaNamestaja)
                {
                    if (unetiNamestaj == namestaj.Naziv)
                        if (namestaj.AkcijaId != null)
                            Console.WriteLine("Taj namestaj je vec na akciji");
                        else
                        {
                            namestaj.AkcijaId = novaAkcija.Id;
                            Projekat.Instance.Namestaj = ListaNamestaja;
                            namestajZaAkciju = namestaj;
                        }
                }
            } while (namestajZaAkciju==null);
            novaAkcija.NamestajId = namestajZaAkciju.Id;
            Console.WriteLine("Unesite koliki je popust");
            novaAkcija.Popust = double.Parse(Console.ReadLine());
            ListaAkcija.Add(novaAkcija);
            IspisMeniAkcija();
        }
        public static void IzmeniPostojecuAkciju()
        {
            foreach (Akcija akcija in ListaAkcija)
            {
                Namestaj namestajNaAkciji = null;
                if (!akcija.Obrisan)
                {
                    foreach (Namestaj namestaj in ListaNamestaja)
                        if (akcija.NamestajId == namestaj.Id)
                            namestajNaAkciji = namestaj;
                    Console.WriteLine(akcija.Id + ". Pocetak: " + akcija.PocetakAkcije.Date + ", Kraj: " + akcija.KrajAkcije.Date + ", Za :" + namestajNaAkciji.Naziv);
                }
            }
            Console.WriteLine("Unesite id za izmenu");
            int unetiID = int.Parse(Console.ReadLine());
            Akcija akcijaZaIzmenu = null;
            do
            {
                foreach (Akcija akcija in ListaAkcija)
                {
                    if (unetiID == akcija.Id)
                        akcijaZaIzmenu = akcija;
                }
            } while (akcijaZaIzmenu == null);
            Console.WriteLine("Unesite sta zelite da izmenite\n1. Pocetni Datum\n2. Zavrsni Datum\n3. Namestaj koji je na akciji\n4. Popust %");
            int unos = int.Parse(Console.ReadLine());
            switch (unos)
            {
                case 1:
                    bool testDate = false;
                    
                    do
                    {
                        bool compareDate = false;
                        try
                        {
                            Console.WriteLine("Unesite pocetni datum (YYYY/MM/DD)");
                            DateTime pocetniDatum = DateTime.Parse(Console.ReadLine());
                            if (akcijaZaIzmenu.KrajAkcije < pocetniDatum)
                            {
                                compareDate = true;
                                throw new Exception();
                            }
                            testDate = true;
                            akcijaZaIzmenu.PocetakAkcije = pocetniDatum;
                        }
                        catch
                        {
                            if (compareDate == true)
                                Console.WriteLine("Krajnji datum ne moze biti manji od pocetnog. \n");
                            else
                                Console.WriteLine("Pogresan format datuma. \n");
                        }

                    } while (testDate==false);
                    
                    IspisMeniAkcija();
                    break;
                case 2:
                    bool testDate1 = false;
                    do
                    {
                        bool compareDate = false;
                        try
                        {
                            Console.WriteLine("Unesite zavrsni datum (YYYY/MM/DD)");
                            DateTime zavrsniDatum = DateTime.Parse(Console.ReadLine());
                            if (zavrsniDatum < akcijaZaIzmenu.PocetakAkcije)
                            {
                                compareDate = true;
                                throw new Exception();
                            }
                            testDate1 = true;
                            akcijaZaIzmenu.KrajAkcije = zavrsniDatum;
                        }
                        catch
                        {
                            if (compareDate == true)
                                Console.WriteLine("Krajnji datum ne moze biti manji od pocetnog. \n");
                            else
                                Console.WriteLine("Pogresan format datuma. \n");

                        }

                    } while (testDate1 == false);
                    IspisMeniAkcija();
                    break;
                case 3:
                    Namestaj namestajZaAkciju = null;
                    do
                    {
                        Console.WriteLine("Unesite naziv namestaja");
                        string unetiNamestaj = Console.ReadLine();
                        foreach (Namestaj namestaj in ListaNamestaja)
                        {
                            if (unetiNamestaj == namestaj.Naziv)
                                if (namestaj.AkcijaId != null)
                                    Console.WriteLine("Taj namestaj je vec na akciji");
                                else
                                {
                                    namestaj.AkcijaId = akcijaZaIzmenu.Id;
                                    namestajZaAkciju = namestaj;
                                }
                        }
                    } while (namestajZaAkciju == null);
                    akcijaZaIzmenu.NamestajId = namestajZaAkciju.Id;
                    IspisMeniAkcija();
                    break;
                case 4:
                    Console.WriteLine("Unesite koliki je popust");
                    double popust = double.Parse(Console.ReadLine());
                    akcijaZaIzmenu.Popust = popust;
                    IspisMeniAkcija();
                    break;
            }
        }

        private static void ObrisiPostojecuAkciju()
        {
            foreach (Akcija akcija in ListaAkcija)
            {
                Namestaj namestajNaAkciji = null;
                if (!akcija.Obrisan)
                {
                    foreach (Namestaj namestaj in ListaNamestaja)
                        if (akcija.NamestajId == namestaj.Id)
                            namestajNaAkciji = namestaj;
                    Console.WriteLine(akcija.Id + ". Pocetak: " + akcija.PocetakAkcije.Date + ", Kraj: " + akcija.KrajAkcije.Date + ", Za :" + namestajNaAkciji.Naziv);
                }
            }
            Console.WriteLine("Unesite id za brisanje");
            int unetiID = int.Parse(Console.ReadLine());
            Akcija akcijaZaBrisanje = null;
            do
            {
                foreach (Akcija akcija in ListaAkcija)
                {
                    if (unetiID == akcija.Id)
                        akcijaZaBrisanje = akcija;
                }
            } while (akcijaZaBrisanje == null);
            akcijaZaBrisanje.Obrisan = true;
            IspisMeniAkcija();
        }
        //==============================================================================
        private static void IzlistajUsluge()
        {
            int index = 1;
            foreach(DodatnaUsluga dodatnaUsluga in ListaUsluga)
            {
                if (!dodatnaUsluga.Obrisan)
                    Console.WriteLine(index + ". Naziv: " + dodatnaUsluga.Naziv+", Cena: "+dodatnaUsluga.Cena);
                index++;
            }
            IspisMeniDodatneUsluge();
        }
        private static void DodajNovuUslugu()
        {
            DodatnaUsluga novaUsluga = new DodatnaUsluga();
            novaUsluga.Id = ListaUsluga.Count + 1;
            Console.WriteLine("Unesite naziv nove usluge");
            novaUsluga.Naziv = Console.ReadLine();
            Console.WriteLine("Unesite cenu usluge");
            novaUsluga.Cena = double.Parse(Console.ReadLine());
            ListaUsluga.Add(novaUsluga);
            IspisMeniDodatneUsluge();
        }
        private static void IzmeniPostojecuUslugu()
        {
            DodatnaUsluga uslugaZaIzmenu=null;
            do
            {
                Console.WriteLine("Unesite naziv usluge koju zelite da izmenite");
                string unetaUsluga = Console.ReadLine();
                foreach (DodatnaUsluga dodatnaUsluga in ListaUsluga)
                    if (dodatnaUsluga.Naziv == unetaUsluga)
                        uslugaZaIzmenu = dodatnaUsluga;
            } while (uslugaZaIzmenu == null);
            Console.WriteLine("Sta zelite da izmenite\n1. Naziv\n2. Cena");
            int opcija = int.Parse(Console.ReadLine());
            switch (opcija)
            {
                case 1:
                    Console.WriteLine("Unesite novi naziv");
                    string noviNaziv = Console.ReadLine();
                    uslugaZaIzmenu.Naziv = noviNaziv;
                    IspisMeniDodatneUsluge();
                    break;
                case 2:
                    Console.WriteLine("Unesite novu cenu");
                    double novaCena = double.Parse(Console.ReadLine());
                    uslugaZaIzmenu.Cena = novaCena;
                    IspisMeniDodatneUsluge();
                    break;
            }
        }
        private static void ObrisiPostojecuUslugu()
        {
            DodatnaUsluga uslugaZaBrisanje = null;
            do
            {
                Console.WriteLine("Unesite naziv usluge koju zelite da obrisete");
                string unetaUsluga = Console.ReadLine();
                foreach (DodatnaUsluga dodatnaUsluga in ListaUsluga)
                    if (dodatnaUsluga.Naziv == unetaUsluga)
                        uslugaZaBrisanje = dodatnaUsluga;
            } while (uslugaZaBrisanje == null);
            uslugaZaBrisanje.Obrisan = true;
            IspisMeniDodatneUsluge();
        }
        //==============================================================================

        private static void IzlistajSalone()
        {
            int index = 1;
            foreach (Salon salon in ListaSalona)
            {
                if (!salon.Obrisan)
                    Console.WriteLine(index + ". Naziv: " + salon.Naziv + ", Adresa: " + salon.Adresa + ", Br Telefona: " + salon.BrojTelefona + ", Email: " + salon.Email);
                index++;
            }
            IspisMeniSalon();
        }
        private static void DodajNoviSalon()
        {
            Salon noviSalon = new Salon();
            noviSalon.Id = ListaSalona.Count + 1;
            Console.WriteLine("Unesite naziv salona");
            noviSalon.Naziv = Console.ReadLine();
            Console.WriteLine("Unesite adresu salona");
            noviSalon.Adresa = Console.ReadLine();
            Console.WriteLine("Unesite broj telefona salona");
            noviSalon.BrojTelefona = Console.ReadLine();
            Console.WriteLine("Unesite email salona");
            noviSalon.Email = Console.ReadLine();
            Console.WriteLine("Unesite web adresu salona");
            noviSalon.WebAdresa = Console.ReadLine();
            Console.WriteLine("Unesite broj racuna salona");
            noviSalon.BrRacuna = Console.ReadLine();
            Console.WriteLine("Unesite pib salona");
            noviSalon.Pib = int.Parse(Console.ReadLine());
            Console.WriteLine("Unesite maticni broj salona");
            noviSalon.MaticniBr = int.Parse(Console.ReadLine());
            ListaSalona.Add(noviSalon);
            IspisMeniSalon();
        }
        private static void IzmeniPostojeciSalon()
        {
            Salon salonZaIzmenu = null;
            do
            {
                Console.WriteLine("Unesite ime salona za izmenu");
                string unetiSalon = Console.ReadLine();
                foreach (Salon salon in ListaSalona)
                    if (unetiSalon == salon.Naziv)
                        salonZaIzmenu = salon;
            } while (salonZaIzmenu == null);
            Console.WriteLine("Sta zelite da izmenite?");
            Console.WriteLine("1. Naziv\n2. Adresa\n3. Broj Telefona\n4. Email\n5. Web Adresa\n6. Broj Racuna\n7. PIB\n8. Maticni broj \n");
            int opcija = int.Parse(Console.ReadLine());
            switch (opcija)
            {
                case 1:
                    Console.WriteLine("Unesite nov naziv");
                    salonZaIzmenu.Naziv = Console.ReadLine();
                    IspisMeniSalon();
                    break;

                case 2:
                    Console.WriteLine("Unesite novu adresu");
                    salonZaIzmenu.Adresa = Console.ReadLine();
                    IspisMeniSalon();
                    break;

                case 3:
                    Console.WriteLine("Unesite nov broj telefona");
                    salonZaIzmenu.BrojTelefona = Console.ReadLine();
                    IspisMeniSalon();
                    break;

                case 4:
                    Console.WriteLine("Unesite email");
                    salonZaIzmenu.Email = Console.ReadLine();
                    IspisMeniSalon();
                    break;

                case 5:
                    Console.WriteLine("Unesite novu web adresu");
                    salonZaIzmenu.WebAdresa = Console.ReadLine();
                    IspisMeniSalon();
                    break;

                case 6:
                    Console.WriteLine("Unesite nov broj racuna");
                    salonZaIzmenu.BrRacuna = Console.ReadLine();
                    IspisMeniSalon();
                    break;

                case 7:
                    Console.WriteLine("Unesite nov pib");
                    salonZaIzmenu.Pib = int.Parse(Console.ReadLine());
                    IspisMeniSalon();
                    break;

                case 8:
                    Console.WriteLine("Unesite nov maticni broj");
                    salonZaIzmenu.MaticniBr = int.Parse(Console.ReadLine());
                    IspisMeniSalon();
                    break;

            }
        }
        private static void ObrisiPostojeciSalon()
        {
            Salon salonZaBrisanje = null;
            do
            {
                Console.WriteLine("Unesite ime salona za izmenu");
                string unetiSalon = Console.ReadLine();
                foreach (Salon salon in ListaSalona)
                    if (unetiSalon == salon.Naziv)
                        salonZaBrisanje = salon;
            } while (salonZaBrisanje == null);
            salonZaBrisanje.Obrisan = true;
            IspisMeniSalon();
        }
        //==============================================================================
    }
}
