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
    class ProdajaDAO
    {
        public static ObservableCollection<ProdajaNamestaja> GetAll()
        {
            var listaProdaja = new ObservableCollection<ProdajaNamestaja>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM ProdajaNamestaja WHERE Obrisan=0";
                da.SelectCommand = cmd;
                da.Fill(ds, "ProdajaNamestaja"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["ProdajaNamestaja"].Rows)
                {
                    var np = new ProdajaNamestaja();
                    np.Id = (int)row["Id"];
                    np.Kupac = row["Kupac"].ToString();
                    np.BrRacuna = row["BrRacuna"].ToString();
                    np.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                    np.UkupnaCena = double.Parse(row["UkupnaCena"].ToString());
                    np.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    listaProdaja.Add(np);
                }
            }
            return listaProdaja;
        }

        public static ProdajaNamestaja Create(ProdajaNamestaja npn)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();


                cmd.CommandText = "INSERT INTO ProdajaNamestaja(Kupac,BrRacuna,DatumProdaje,UkupnaCena,Obrisan) VALUES (@Kupac,@BrRacuna,@DatumProdaje,@UkupnaCena,@Obrisan)";
                cmd.CommandText += " Select SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Kupac", npn.Kupac);
                cmd.Parameters.AddWithValue("BrRacuna", npn.BrRacuna);
                cmd.Parameters.AddWithValue("DatumProdaje", npn.DatumProdaje);
                cmd.Parameters.AddWithValue("UkupnaCena", npn.UkupnaCena);
                cmd.Parameters.AddWithValue("Obrisan", npn.Obrisan);

                npn.Id = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava upit
            }
            Projekat.Instance.Prodaja.Add(npn);
            return npn;
        }
        
        public static void Update(ProdajaNamestaja npn)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                DataSet ds = new DataSet();


                cmd.CommandText = "UPDATE ProdajaNamestaja SET Kupac=@Kupac,BrRacuna=@BrRacuna,DatumProdaje=@DatumProdaje,UkupnaCena=@UkupnaCena,Obrisan=@Obrisan WHERE Id = @Id";
                cmd.CommandText += " SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Id", npn.Id);
                cmd.Parameters.AddWithValue("Kupac", npn.Kupac);
                cmd.Parameters.AddWithValue("BrRacuna", npn.BrRacuna);
                cmd.Parameters.AddWithValue("DatumProdaje", npn.DatumProdaje);
                cmd.Parameters.AddWithValue("UkupnaCena", npn.UkupnaCena);
                cmd.Parameters.AddWithValue("Obrisan", npn.Obrisan);

                cmd.ExecuteNonQuery();
            }
            foreach (var prodaja in Projekat.Instance.Prodaja)
            {
                if (prodaja.Id == npn.Id)
                {
                    prodaja.Kupac = npn.Kupac;
                    prodaja.BrRacuna = npn.BrRacuna;
                    prodaja.DatumProdaje = npn.DatumProdaje;
                    prodaja.UkupnaCena = npn.UkupnaCena;
                    prodaja.Obrisan = npn.Obrisan;
                }
            }
        }

        public static void Delete(ProdajaNamestaja prodaja)
        {
            prodaja.Obrisan = true;
            Update(prodaja);
        }
        
    }
}
