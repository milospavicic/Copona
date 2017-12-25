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
    class DodatnaUslugaDAO
    {
        public static ObservableCollection<DodatnaUsluga> GetAll()
        {
            var dodatneUsluge = new ObservableCollection<DodatnaUsluga>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM DodatnaUsluga WHERE Obrisan=0";
                da.SelectCommand = cmd;
                da.Fill(ds, "DodatnaUsluga"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["DodatnaUsluga"].Rows)
                {
                    var du = new DodatnaUsluga();
                    du.Id = (int)row["Id"];
                    du.Naziv = row["Naziv"].ToString();
                    du.Cena = double.Parse(row["Cena"].ToString());
                    du.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    dodatneUsluge.Add(du);
                }
            }
            return dodatneUsluge;
        }
        
        public static DodatnaUsluga GetById(int Id)
        {
            var dodatnaUsluga = new DodatnaUsluga();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                cmd.CommandText = "SELECT * FROM DodatnaUsluga WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", Id);
                da.SelectCommand = cmd;
                da.Fill(ds, "DodatnaUsluga"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["DodatnaUsluga"].Rows)
                {

                    dodatnaUsluga.Id = (int)row["Id"];
                    dodatnaUsluga.Naziv = row["Naziv"].ToString();
                    dodatnaUsluga.Cena = double.Parse(row["Cena"].ToString());
                    dodatnaUsluga.Obrisan = bool.Parse(row["Obrisan"].ToString());
                }

            }
            return dodatnaUsluga;
        }
        
        public static DodatnaUsluga Create(DodatnaUsluga du)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();


                cmd.CommandText = "INSERT INTO DodatnaUsluga(Naziv,Cena,Obrisan) VALUES (@Naziv,@Cena,@Obrisan)";
                cmd.CommandText += "Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Naziv", du.Naziv);
                cmd.Parameters.AddWithValue("Cena", du.Cena);
                cmd.Parameters.AddWithValue("Obrisan", du.Obrisan);

                du.Id = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava upit
            }
            Projekat.Instance.DodatneUsluge.Add(du);
            return du;
        }

        public static void Update(DodatnaUsluga du)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                DataSet ds = new DataSet();


                cmd.CommandText = "UPDATE DodatnaUsluga SET Naziv=@Naziv,Cena=@Cena,Obrisan=@Obrisan WHERE Id = @Id";
                cmd.CommandText += " SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Id", du.Id);
                cmd.Parameters.AddWithValue("Naziv", du.Naziv);
                cmd.Parameters.AddWithValue("Cena", du.Cena);
                cmd.Parameters.AddWithValue("Obrisan", du.Obrisan);

                cmd.ExecuteNonQuery();
            }
            foreach (var dodatnaUsluga in Projekat.Instance.DodatneUsluge)
            {
                if (dodatnaUsluga.Id == du.Id)
                {
                    dodatnaUsluga.Naziv = du.Naziv;
                    dodatnaUsluga.Cena = du.Cena;
                    dodatnaUsluga.Obrisan = du.Obrisan;
                }
            }
        }

        public static void Delete(DodatnaUsluga du)
        {
            du.Obrisan = true;
            Update(du);
        }
    }
}
