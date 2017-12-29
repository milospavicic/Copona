using POP_SF39_2016.model;
using POP_SF39_2016_GUI.model;
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
    class NaAkcijiDAO
    {
        /***
        public static int Count()
        {
            int slIndex = 0;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT COUNT(*) as BrRedova FROM NaAkciji";
                da.SelectCommand = cmd;
                da.Fill(ds, "NaAkciji"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["NaAkciji"].Rows)
                {
                    slIndex = int.Parse(row["BrRedova"].ToString());
                }
            }
            return slIndex;
        }
        ***/
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


                cmd.CommandText = "UPDATE NaAkciji SET IdAkcije=@IdAkcije,IdNamestaja=@IdNamestaja,Obrisan=@Obrisan WHERE IdNaAkciji = @IdNaAkciji";
                cmd.CommandText += " SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdNaAkciji", na.IdNaAkciji);
                cmd.Parameters.AddWithValue("IdAkcije", na.IdAkcije);
                cmd.Parameters.AddWithValue("IdNamestaja", na.IdNamestaja);
                cmd.Parameters.AddWithValue("Obrisan", na.Obrisan);

                cmd.ExecuteNonQuery();
            }
            foreach (var naAkciji in Projekat.Instance.NaAkciji)
            {
                if (naAkciji.IdAkcije == na.IdNaAkciji)
                {
                    naAkciji.IdNamestaja = na.IdNamestaja;
                    naAkciji.IdAkcije = na.IdAkcije;
                    naAkciji.Obrisan = na.Obrisan;
                }
            }
        }
        public static void Delete(NaAkciji na)
        {
            na.Obrisan = true;
            Update(na);
        }
        public static ObservableCollection<Namestaj> GetAllNamestajForActionId(int IdAkcije)
        {
            var listaNamestaja = new ObservableCollection<Namestaj>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT IdNamestaja FROM NaAkciji WHERE Obrisan = 0 and IdAkcije = @IdAkcije";
                cmd.CommandText += " Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdAkcije", IdAkcije);

                da.SelectCommand = cmd;

                da.Fill(ds, "NaAkciji"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["NaAkciji"].Rows)
                {
                    var tempNamestaj =  Namestaj.GetById((int)row["IdNamestaja"]);
                    listaNamestaja.Add(tempNamestaj);
                }
            }
            return listaNamestaja;
        }

        public static int GetPopust(int IdAkcije)
        {
            int popust = 0;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT DISTINCT POPUST FROM NaAkciji WHERE Obrisan = 0 and IdAkcije = @IdAkcije";
                cmd.CommandText += " Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdAkcije", IdAkcije);

                da.SelectCommand = cmd;

                da.Fill(ds, "NaAkciji"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["NaAkciji"].Rows)
                {
                    popust = (int)row["Popust"];
                }
            }
            return popust;
        }
        public static void SetPopust(int IdAkcije,int Popust)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = "UPDATE NaAkciji SET Popust = @Popust WHERE Obrisan = 0 and IdAkcije = @IdAkcije";
                cmd.CommandText += " Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Popust", Popust);
                cmd.Parameters.AddWithValue("IdAkcije", IdAkcije);

                cmd.ExecuteNonQuery();
            }
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
