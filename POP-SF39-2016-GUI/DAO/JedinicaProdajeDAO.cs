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
    class JedinicaProdajeDAO
    {
        public static ObservableCollection<JedinicaProdaje> GetAll()
        {
            var jedProdaje = new ObservableCollection<JedinicaProdaje>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM JedinicaProdaje WHERE Obrisan=0";
                da.SelectCommand = cmd;
                da.Fill(ds, "JedinicaProdaje"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["JedinicaProdaje"].Rows)
                {
                    var njp = new JedinicaProdaje();
                    njp.Id = (int)row["Id"];
                    njp.ProdajaId = int.Parse(row["ProdajaId"].ToString());
                    njp.NamestajId = int.Parse(row["NamestajId"].ToString());
                    njp.Kolicina = int.Parse(row["Kolicina"].ToString());
                    njp.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    jedProdaje.Add(njp);
                }
            }
            return jedProdaje;
        }
        public static List<Namestaj> GetAllForId(int Id)
        {
            var listaNamestaja = new List<Namestaj>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                cmd.CommandText = "SELECT NamestajId FROM JedinicaProdaje WHERE Obrisan=0 and ProdajaId=@ProdajaId";
                cmd.CommandText += " Select SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("ProdajaId", Id);
                da.SelectCommand = cmd;
                da.Fill(ds, "JedinicaProdaje"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["JedinicaProdaje"].Rows)
                {
                    Namestaj tempNamestaj = Namestaj.GetById(int.Parse(row["NamestajId"].ToString()));

                    listaNamestaja.Add(tempNamestaj);
                }
            }
            return listaNamestaja;
        }

        public static JedinicaProdaje Create(JedinicaProdaje njp)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();


                cmd.CommandText = "INSERT INTO JedinicaProdaje(ProdajaId,NamestajId,Kolicina,Obrisan) VALUES (@ProdajaId,@NamestajId,@Kolicina,@Obrisan)";
                cmd.CommandText += " Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("ProdajaId", njp.ProdajaId);
                cmd.Parameters.AddWithValue("NamestajId", njp.NamestajId);
                cmd.Parameters.AddWithValue("Kolicina", njp.Kolicina);
                cmd.Parameters.AddWithValue("Obrisan", njp.Obrisan);

                njp.Id = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava upit
            }
            Projekat.Instance.JediniceProdaje.Add(njp);
            return njp;
        }
    }
}
