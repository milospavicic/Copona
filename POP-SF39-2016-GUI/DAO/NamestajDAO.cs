using POP_SF39_2016.model;
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
    class NamestajDAO
    {
        public static ObservableCollection<Namestaj> GetAll()
        {
            var listaNamestaja = new ObservableCollection<Namestaj>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM Namestaj WHERE Obrisan=0";
                da.SelectCommand = cmd;
                da.Fill(ds, "Namestaj"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    var n = new Namestaj();
                    n.Id = (int)row["Id"];
                    n.TipNamestajaId = (int?)row["TipNamestajaId"];
                    n.Naziv = row["Naziv"].ToString();
                    n.Sifra = row["Sifra"].ToString();
                    n.Cena = double.Parse(row["Cena"].ToString());
                    n.BrKomada = (int)row["Kolicina"];

                    n.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    listaNamestaja.Add(n);
                }
            }
            return listaNamestaja;
        }

        public static Namestaj GetById(int Id)
        {
            var namestaj = new Namestaj();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM Namestaj WHERE Obrisan=0 AND Id = @Id";
                cmd.Parameters.AddWithValue("Id", Id);
                da.SelectCommand = cmd;
                da.Fill(ds, "Namestaj"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    var n = new Namestaj();
                    n.Id = (int)row["Id"];
                    n.TipNamestajaId = (int?)row["TipNamestajaId"];
                    n.Naziv = row["Naziv"].ToString();
                    n.Sifra = row["Sifra"].ToString();
                    n.Cena = double.Parse(row["Cena"].ToString());
                    n.BrKomada = (int)row["Kolicina"];

                    n.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    namestaj = n;
                }
            }
            return namestaj;
        }

        public static Namestaj Create(Namestaj nn)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();


                cmd.CommandText = "INSERT INTO Namestaj(TipNamestajaId,Naziv,Sifra,Kolicina,Cena,Obrisan) VALUES (@TipNamestajaId,@Naziv,@Sifra,@Kolicina,@Cena,@Obrisan)";
                cmd.CommandText += " Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("TipNamestajaId", nn.TipNamestajaId);
                cmd.Parameters.AddWithValue("Naziv", nn.Naziv);
                cmd.Parameters.AddWithValue("Sifra", nn.Sifra);
                cmd.Parameters.AddWithValue("Kolicina", nn.BrKomada);
                cmd.Parameters.AddWithValue("Cena", nn.Cena);
                cmd.Parameters.AddWithValue("Obrisan", nn.Obrisan);

                nn.Id = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava upit
            }
            Projekat.Instance.Namestaji.Add(nn);
            return nn;
        }
        
        public static void Update(Namestaj nzu)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                DataSet ds = new DataSet();


                cmd.CommandText = "UPDATE Namestaj SET TipNamestajaId=@TipNamestajaId,Naziv=@Naziv,Sifra=@Sifra,Kolicina=@Kolicina,Cena=@Cena,Obrisan=@Obrisan WHERE Id = @Id";
                cmd.CommandText += " SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Id", nzu.Id);
                cmd.Parameters.AddWithValue("TipNamestajaId", nzu.TipNamestajaId);
                cmd.Parameters.AddWithValue("Naziv", nzu.Naziv);
                cmd.Parameters.AddWithValue("Sifra", nzu.Sifra);
                cmd.Parameters.AddWithValue("Kolicina", nzu.BrKomada);
                cmd.Parameters.AddWithValue("Cena", nzu.Cena);
                cmd.Parameters.AddWithValue("Obrisan", nzu.Obrisan);

                cmd.ExecuteNonQuery();
            }
            foreach (var namestaj in Projekat.Instance.Namestaji)
            {
                if (namestaj.Id == nzu.Id)
                {
                    namestaj.TipNamestajaId = nzu.TipNamestajaId;
                    namestaj.Naziv = nzu.Naziv;
                    namestaj.Sifra = nzu.Sifra;
                    namestaj.Cena = nzu.Cena;
                    namestaj.BrKomada = nzu.BrKomada;
                    namestaj.Obrisan = nzu.Obrisan;
                }
            }
        }

        public static void Delete(Namestaj namestaj)
        {
            namestaj.Obrisan = true;
            Update(namestaj);
        }

        public static ObservableCollection<Namestaj> GetAllNamestajNotOnAction()
        {
            var listaNamestaja = new ObservableCollection<Namestaj>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT Id FROM Namestaj WHERE Id not in (SELECT IdNamestaja FROM NaAkciji WHERE obrisan=0)";
                cmd.CommandText += " Select SCOPE_IDENTITY();";

                da.SelectCommand = cmd;

                da.Fill(ds, "Namestaj"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    var tempNamestaj = Namestaj.GetById((int)row["Id"]);
                    listaNamestaja.Add(tempNamestaj);
                }
            }
            return listaNamestaja;
        }
    }
}
