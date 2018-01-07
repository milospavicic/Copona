using POP_SF39_2016.model;
using POP_SF39_2016_GUI.model;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace POP_SF39_2016_GUI.DAO
{
    class NaAkcijiDAO
    {
        public static ObservableCollection<NaAkciji> GetAll()
        {
            var listaNaAkcija = new ObservableCollection<NaAkciji>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM NaAkciji WHERE Obrisan = 0";
                da.SelectCommand = cmd;
                da.Fill(ds, "NaAkciji"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["NaAkciji"].Rows)
                {
                    var a = new NaAkciji();
                    a.IdAkcije = (int)row["IdAkcije"];
                    a.IdNaAkciji = (int)row["IdNaAkciji"];
                    a.IdNamestaja = (int)row["IdNamestaja"];
                    a.Popust = (int)row["Popust"];

                    listaNaAkcija.Add(a);
                }
            }
            return listaNaAkcija;
        }

        public static NaAkciji Create(NaAkciji na)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();


                cmd.CommandText = "INSERT INTO NaAkciji(IdNamestaja,IdAkcije,Popust,Obrisan) VALUES (@IdNamestaja,@IdAkcije,@Popust,@Obrisan)";
                cmd.CommandText += " Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdNamestaja", na.IdNamestaja);
                cmd.Parameters.AddWithValue("IdAkcije", na.IdAkcije);
                cmd.Parameters.AddWithValue("Popust", na.Popust);
                cmd.Parameters.AddWithValue("Obrisan", na.Obrisan);

                na.IdNaAkciji = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava upit
            }
            Projekat.Instance.NaAkciji.Add(na);
            return na;
        }
        public static void Update(NaAkciji na)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                DataSet ds = new DataSet();


                cmd.CommandText = "UPDATE NaAkciji SET IdAkcije=@IdAkcije,IdNamestaja=@IdNamestaja,Popust=@Popust,Obrisan=@Obrisan WHERE IdNaAkciji = @IdNaAkciji";
                cmd.CommandText += " SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdNaAkciji", na.IdNaAkciji);
                cmd.Parameters.AddWithValue("IdAkcije", na.IdAkcije);
                cmd.Parameters.AddWithValue("IdNamestaja", na.IdNamestaja);
                cmd.Parameters.AddWithValue("Popust", na.Popust);
                cmd.Parameters.AddWithValue("Obrisan", na.Obrisan);

                cmd.ExecuteNonQuery();
            }
            foreach (var naAkciji in Projekat.Instance.NaAkciji)
            {
                if (naAkciji.IdNaAkciji == na.IdNaAkciji)
                {
                    naAkciji.IdNamestaja = na.IdNamestaja;
                    naAkciji.IdAkcije = na.IdAkcije;
                    naAkciji.Popust = na.Popust;
                    naAkciji.Obrisan = na.Obrisan;
                }
            }
        }
        public static void Delete(NaAkciji na)
        {
            na.Obrisan = true;
            Update(na);
        }
        public static ObservableCollection<NaAkciji> GetAllNAForActionId(int IdAkcije)
        {
            var listaNaAkcija = new ObservableCollection<NaAkciji>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM NaAkciji WHERE Obrisan = 0 and IdAkcije = @IdAkcije";
                cmd.CommandText += " Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdAkcije", IdAkcije);

                da.SelectCommand = cmd;

                da.Fill(ds, "NaAkciji"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["NaAkciji"].Rows)
                {
                    var a = new NaAkciji();
                    a.IdAkcije = (int)row["IdAkcije"];
                    a.IdNaAkciji = (int)row["IdNaAkciji"];
                    a.IdNamestaja = (int)row["IdNamestaja"];
                    a.Popust = (int)row["Popust"];

                    listaNaAkcija.Add(a);
                }
            }
            return listaNaAkcija;
        }

        public static int GetPopustForId(int IdNamestaja)
        {
            int popust = 0;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                //SELECT* FROM NaAkciji na INNER JOIN Akcija a ON na.IdAkcije = a.IdAkcije WHERE na.Obrisan = 0 AND na.IdAkcije = 1 AND DatumKraj<= @DatumKraj
                cmd.CommandText = "SELECT POPUST FROM NaAkciji na INNER JOIN Akcija a ON na.IdAkcije = a.IdAkcije WHERE na.Obrisan = 0 and na.IdNamestaja = @IdNamestaja  AND DatumKraj >= @DatumKraj";
                cmd.CommandText += " Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdNamestaja", IdNamestaja);
                cmd.Parameters.AddWithValue("DatumKraj", DateTime.Today);

                da.SelectCommand = cmd;

                da.Fill(ds, "NaAkciji"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["NaAkciji"].Rows)
                {
                    popust = (int)row["Popust"];
                }
            }
            return popust;
        }
        public static NaAkciji GetForNamestajId(int index)
        {
            var tempNaAkciji = new NaAkciji();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                cmd.CommandText = "SELECT * FROM NaAkciji WHERE IdNamestaja=@IdNamestaja";
                cmd.CommandText += " Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdNamestaja", index);
                da.SelectCommand = cmd;
                da.Fill(ds, "NaAkciji"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["NaAkciji"].Rows)
                {
                    var a = new NaAkciji();
                    a.IdAkcije = (int)row["IdAkcije"];
                    a.IdNaAkciji = (int)row["IdNaAkciji"];
                    a.IdNamestaja = (int)row["IdNamestaja"];
                    a.Popust = (int)row["Popust"];

                    tempNaAkciji = a;
                }
            }
            return tempNaAkciji;
        }
    }
}
