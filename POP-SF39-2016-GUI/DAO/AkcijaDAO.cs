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
    class AkcijaDAO
    {

        public static ObservableCollection<Akcija> GetAll()
        {
            var listaAkcija = new ObservableCollection<Akcija>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM Akcija WHERE Obrisan=0";
                da.SelectCommand = cmd;
                da.Fill(ds, "Akcija"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Akcija"].Rows)
                {
                    var a = new Akcija();
                    a.Id = (int)row["IdAkcije"];
                    a.PocetakAkcije = DateTime.Parse(row["DatumPocetak"].ToString());
                    a.KrajAkcije = DateTime.Parse(row["DatumKraj"].ToString());
                    a.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    listaAkcija.Add(a);
                }
            }
            return listaAkcija;
        }
        public static Akcija Create(Akcija nn)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();


                cmd.CommandText = "INSERT INTO Akcija(DatumPocetak,DatumKraj,Obrisan) VALUES (@DatumPocetak,@DatumKraj,@Obrisan)";
                cmd.CommandText += " Select SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("DatumPocetak", nn.PocetakAkcije);
                cmd.Parameters.AddWithValue("DatumKraj", nn.KrajAkcije);
                cmd.Parameters.AddWithValue("Obrisan", nn.Obrisan);

                nn.Id = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava upit
            }
            Projekat.Instance.Akcija.Add(nn);
            return nn;
        }
        public static void Update(Akcija azu)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                DataSet ds = new DataSet();


                cmd.CommandText = "UPDATE Akcija SET DatumPocetak=@DatumPocetak,DatumKraj=@DatumKraj,Obrisan=@Obrisan WHERE IdAkcije = @IdAkcije";
                cmd.CommandText += " SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdAkcije", azu.Id);
                cmd.Parameters.AddWithValue("DatumPocetak", azu.PocetakAkcije);
                cmd.Parameters.AddWithValue("DatumKraj", azu.KrajAkcije);
                cmd.Parameters.AddWithValue("Obrisan", azu.Obrisan);

                cmd.ExecuteNonQuery();
            }
            foreach (var akcija in Projekat.Instance.Akcija)
            {
                if (akcija.Id == azu.Id)
                {
                    akcija.PocetakAkcije = azu.PocetakAkcije;
                    akcija.KrajAkcije = azu.KrajAkcije;
                    akcija.Obrisan = azu.Obrisan;
                }
            }
        }

        public static void Delete(Akcija akcija)
        {
            akcija.Obrisan = true;
            Update(akcija);
        }
    }
}

