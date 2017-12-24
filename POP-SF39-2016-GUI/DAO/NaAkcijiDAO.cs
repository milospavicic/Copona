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

    }
}
