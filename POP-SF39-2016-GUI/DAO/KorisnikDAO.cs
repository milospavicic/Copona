using POP_SF39_2016.model;
using POP_SF39_2016_GUI.gui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF39_2016_GUI.DAO
{
    class KorisnikDAO
    {
        public enum SortBy
        {
            Ime_Opadajuce,
            Ime_Rastuce,
            Prezime_Opadajuce,
            Prezime_Rastuce,
            KorisnickoIme_Opadajuce,
            KorisnickoIme_Rastuce,
            TipKorisnika_Opadajuce,
            TipKorisnika_Rastuce,
            Nesortirano,
        }
        public static ObservableCollection<Korisnik> GetAll()
        {
            var korisnici = new ObservableCollection<Korisnik>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM Korisnik WHERE Obrisan=0";
                da.SelectCommand = cmd;
                da.Fill(ds, "Korisnik"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Korisnik"].Rows)
                {
                    var nk = new Korisnik();
                    nk.Id = (int)row["Id"];
                    nk.Ime = row["Ime"].ToString();
                    nk.Prezime = row["Prezime"].ToString();
                    nk.KorisnickoIme = row["KorisnickoIme"].ToString();
                    nk.Lozinka = row["Lozinka"].ToString();
                    if ((int)row["TipKorisnika"] == 1)
                        nk.TipKorisnika = TipKorisnika.Administrator;
                    else
                        nk.TipKorisnika = TipKorisnika.Prodavac;
                    nk.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    korisnici.Add(nk);
                }
            }
            return korisnici;
        }

        public static Korisnik GetById(int Id)
        {
            var korisnik = new Korisnik();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                cmd.CommandText = "SELECT * FROM Korisnik WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", Id);
                da.SelectCommand = cmd;
                da.Fill(ds, "Korisnik"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Korisnik"].Rows)
                {

                    korisnik.Id = (int)row["Id"];
                    korisnik.Ime = row["Ime"].ToString();
                    korisnik.Prezime = row["Prezime"].ToString();
                    korisnik.KorisnickoIme = row["KorisnickoIme"].ToString();
                    korisnik.Lozinka = row["Lozinka"].ToString();
                    if ((int)row["TipKorisnika"] == 1)
                        korisnik.TipKorisnika = TipKorisnika.Administrator;
                    else
                        korisnik.TipKorisnika = TipKorisnika.Prodavac;
                    korisnik.Obrisan = bool.Parse(row["Obrisan"].ToString());
                }

            }
            return korisnik;
        }

        public static Korisnik Create(Korisnik nk)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();


                cmd.CommandText = "INSERT INTO Korisnik(Ime,Prezime,KorisnickoIme,Lozinka,TipKorisnika,Obrisan) VALUES (@Ime,@Prezime,@KorisnickoIme,@Lozinka,@TipKorisnika,@Obrisan)";
                cmd.CommandText += " Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Ime", nk.Ime);
                cmd.Parameters.AddWithValue("Prezime", nk.Prezime);
                cmd.Parameters.AddWithValue("KorisnickoIme", nk.KorisnickoIme);
                cmd.Parameters.AddWithValue("Lozinka", nk.Lozinka);
                if (nk.TipKorisnika == TipKorisnika.Administrator)
                    cmd.Parameters.AddWithValue("TipKorisnika", 1);
                else
                    cmd.Parameters.AddWithValue("TipKorisnika", 2);
                cmd.Parameters.AddWithValue("Obrisan", nk.Obrisan);

                nk.Id = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava upit
            }
            Projekat.Instance.Korisnici.Add(nk);
            return nk;
        }

        public static void Update(Korisnik kzu)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                DataSet ds = new DataSet();


                cmd.CommandText = "UPDATE Korisnik SET Ime=@Ime,Prezime=@Prezime,KorisnickoIme=@KorisnickoIme,Lozinka=@Lozinka,TipKorisnika=@TipKorisnika,Obrisan=@Obrisan WHERE Id = @Id";
                cmd.CommandText += " SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Id", kzu.Id);
                cmd.Parameters.AddWithValue("Ime", kzu.Ime);
                cmd.Parameters.AddWithValue("Prezime", kzu.Prezime);
                cmd.Parameters.AddWithValue("KorisnickoIme", kzu.KorisnickoIme);
                cmd.Parameters.AddWithValue("Lozinka", kzu.Lozinka);
                if (kzu.TipKorisnika == TipKorisnika.Administrator)
                    cmd.Parameters.AddWithValue("TipKorisnika", 1);
                else
                    cmd.Parameters.AddWithValue("TipKorisnika", 2);
                cmd.Parameters.AddWithValue("Obrisan", kzu.Obrisan);

                cmd.ExecuteNonQuery();
            }
            foreach (var korisnik in Projekat.Instance.Korisnici)
            {
                if (korisnik.Id == kzu.Id)
                {
                    korisnik.Ime = kzu.Ime;
                    korisnik.Prezime = kzu.Prezime;
                    korisnik.KorisnickoIme = kzu.KorisnickoIme;
                    korisnik.Lozinka = kzu.Lozinka;
                    korisnik.TipKorisnika = kzu.TipKorisnika;
                    korisnik.Obrisan = kzu.Obrisan;
                }
            }
        }

        public static void Delete(Korisnik nk)
        {
            nk.Obrisan = true;
            Update(nk);
        }
        public static ObservableCollection<Korisnik> Search(string parametar)
        {
            var korisnici = new ObservableCollection<Korisnik>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM Korisnik WHERE Obrisan=0 AND (Ime LIKE @parametar OR Prezime LIKE @parametar OR KorisnickoIme LIKE @parametar";
                cmd.Parameters.AddWithValue("parametar", "%" + parametar + "%");
                da.SelectCommand = cmd;
                da.Fill(ds, "Korisnik"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Korisnik"].Rows)
                {
                    var nk = new Korisnik();
                    nk.Id = (int)row["Id"];
                    nk.Ime = row["Ime"].ToString();
                    nk.Prezime = row["Prezime"].ToString();
                    nk.KorisnickoIme = row["KorisnickoIme"].ToString();
                    nk.Lozinka = row["Lozinka"].ToString();
                    if ((int)row["TipKorisnika"] == 1)
                        nk.TipKorisnika = TipKorisnika.Administrator;
                    else
                        nk.TipKorisnika = TipKorisnika.Prodavac;
                    nk.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    korisnici.Add(nk);
                }
            }
            return korisnici;
        }
        public static ObservableCollection<Korisnik> Sort(SortBy sortBy)
        {
            var korisnici = new ObservableCollection<Korisnik>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                cmd.CommandText = "SELECT * FROM Korisnik WHERE Obrisan=0";
                switch (sortBy)
                {
                    case SortBy.Ime_Opadajuce:
                        cmd.CommandText += " ORDER BY Ime DESC";
                        break;
                    case SortBy.Ime_Rastuce:
                        cmd.CommandText += " ORDER BY Ime ASC";
                        break;
                    case SortBy.Prezime_Opadajuce:
                        cmd.CommandText += " ORDER BY Prezime DESC";
                        break;
                    case SortBy.Prezime_Rastuce:
                        cmd.CommandText += " ORDER BY Prezime ASC";
                        break;
                    case SortBy.KorisnickoIme_Opadajuce:
                        cmd.CommandText += " ORDER BY KorisnickoIme DESC";
                        break;
                    case SortBy.KorisnickoIme_Rastuce:
                        cmd.CommandText += " ORDER BY KorisnickoIme ASC";
                        break;
                    case SortBy.TipKorisnika_Opadajuce:
                        cmd.CommandText += " ORDER BY TipKorisnika DESC";
                        break;
                    case SortBy.TipKorisnika_Rastuce:
                        cmd.CommandText += " ORDER BY TipKorisnika ASC";
                        break;
                }
                da.SelectCommand = cmd;
                da.Fill(ds, "Korisnik"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Korisnik"].Rows)
                {
                    var nk = new Korisnik();
                    nk.Id = (int)row["Id"];
                    nk.Ime = row["Ime"].ToString();
                    nk.Prezime = row["Prezime"].ToString();
                    nk.KorisnickoIme = row["KorisnickoIme"].ToString();
                    nk.Lozinka = row["Lozinka"].ToString();
                    if ((int)row["TipKorisnika"] == 1)
                        nk.TipKorisnika = TipKorisnika.Administrator;
                    else
                        nk.TipKorisnika = TipKorisnika.Prodavac;
                    nk.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    korisnici.Add(nk);
                }
            }
            return korisnici;
        }
        public static ObservableCollection<Korisnik> SearchAndOrSort(GlavniWindow.DoSearch doSearch, string parametar, SortBy sortBy)
        {
            var korisnici = new ObservableCollection<Korisnik>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                switch (doSearch)
                {
                    case GlavniWindow.DoSearch.Other:
                        cmd.CommandText = "SELECT * FROM Korisnik WHERE Obrisan=0 AND (Ime LIKE @parametar OR Prezime LIKE @parametar OR KorisnickoIme LIKE @parametar)";
                        cmd.Parameters.AddWithValue("parametar", "%" + parametar + "%");
                        break;
                    case GlavniWindow.DoSearch.No:
                        cmd.CommandText = "SELECT * FROM Korisnik WHERE Obrisan=0";
                        break;
                }

                switch (sortBy)
                {
                    case SortBy.Ime_Opadajuce:
                        cmd.CommandText += " ORDER BY Ime DESC";
                        break;
                    case SortBy.Ime_Rastuce:
                        cmd.CommandText += " ORDER BY Ime ASC";
                        break;
                    case SortBy.Prezime_Opadajuce:
                        cmd.CommandText += " ORDER BY Prezime DESC";
                        break;
                    case SortBy.Prezime_Rastuce:
                        cmd.CommandText += " ORDER BY Prezime ASC";
                        break;
                    case SortBy.KorisnickoIme_Opadajuce:
                        cmd.CommandText += " ORDER BY KorisnickoIme DESC";
                        break;
                    case SortBy.KorisnickoIme_Rastuce:
                        cmd.CommandText += " ORDER BY KorisnickoIme ASC";
                        break;
                    case SortBy.TipKorisnika_Opadajuce:
                        cmd.CommandText += " ORDER BY TipKorisnika DESC";
                        break;
                    case SortBy.TipKorisnika_Rastuce:
                        cmd.CommandText += " ORDER BY TipKorisnika ASC";
                        break;
                }
                da.SelectCommand = cmd;
                da.Fill(ds, "Korisnik"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Korisnik"].Rows)
                {
                    var nk = new Korisnik();
                    nk.Id = (int)row["Id"];
                    nk.Ime = row["Ime"].ToString();
                    nk.Prezime = row["Prezime"].ToString();
                    nk.KorisnickoIme = row["KorisnickoIme"].ToString();
                    nk.Lozinka = row["Lozinka"].ToString();
                    if ((int)row["TipKorisnika"] == 1)
                        nk.TipKorisnika = TipKorisnika.Administrator;
                    else
                        nk.TipKorisnika = TipKorisnika.Prodavac;
                    nk.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    korisnici.Add(nk);
                }
            }
            return korisnici;
        }
    }
}
