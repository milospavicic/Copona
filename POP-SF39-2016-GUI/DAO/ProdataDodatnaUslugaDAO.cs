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
    class ProdataDodatnaUslugaDAO
    {
        public static ObservableCollection<ProdataDU> GetAll()
        {
            var dodatneUsluge = new ObservableCollection<ProdataDU>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM ProdataDodatnaUsluga WHERE Obrisan=0";
                da.SelectCommand = cmd;
                da.Fill(ds, "ProdataDodatnaUsluga"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["ProdataDodatnaUsluga"].Rows)
                {
                    var du = new ProdataDU();
                    du.Id = (int)row["Id"];
                    du.ProdajaId = (int)row["ProdajaId"];
                    du.DodatnaUslugaId = (int)row["DodatnaUslugaId"];
                    du.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    dodatneUsluge.Add(du);
                }
            }
            return dodatneUsluge;
        }
        /**
        public static TipNamestaja GetById(int Id)
        {
            var tipNamestaja = new TipNamestaja();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                cmd.CommandText = "SELECT * FROM TipNamestaja WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", Id);
                da.SelectCommand = cmd;
                da.Fill(ds, "TipNamestaja"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["TipNamestaja"].Rows)
                {
                    tipNamestaja.Id = (int)row["Id"];
                    tipNamestaja.Naziv = row["Naziv"].ToString();
                    tipNamestaja.Obrisan = bool.Parse(row["Obrisan"].ToString());
                }

            }
            return tipNamestaja;
        }
        **/
        public static ProdataDU Create(ProdataDU tn)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();


                cmd.CommandText = "INSERT INTO ProdataDodatnaUsluga(ProdajaId,DodatnaUslugaId,Obrisan) VALUES (@ProdajaId,@DodatnaUslugaId,@Obrisan)";
                cmd.CommandText += "Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("ProdajaId", tn.ProdajaId);
                cmd.Parameters.AddWithValue("DodatnaUslugaId", tn.DodatnaUslugaId);
                cmd.Parameters.AddWithValue("Obrisan", tn.Obrisan);

                tn.Id = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava upit
            }
            Projekat.Instance.ProdateDU.Add(tn);
            return tn;
        }
    }
}
