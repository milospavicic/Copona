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
    class ProdajaDAO
    {
        public enum SortBy
        {
            Kupac_Opadajuce,
            Kupac_Rastuce,
            BrRacuna_Opadajuce,
            BrRacuna_Rastuce,
            DatumProdaje_Opadajuce,
            DatumProdaje_Rastuce,
            UkupnaCena_Opadajuce,
            UkupnaCena_Rastuce,
            Nesortirano
        }

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

        public static ObservableCollection<ProdajaNamestaja> Search(string parametarS, DateTime parametarDT, GlavniWindow.DoSearch tipPretrage)
        {
            var listaProdaja = new ObservableCollection<ProdajaNamestaja>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                switch (tipPretrage)
                {
                    case GlavniWindow.DoSearch.Date:
                        cmd.CommandText = "SELECT * FROM ProdajaNamestaja WHERE Obrisan=0 AND DatumProdaje = @parametarDT";
                        cmd.Parameters.AddWithValue("parametarDT", parametarDT);
                        break;
                    case GlavniWindow.DoSearch.Other:
                        cmd.CommandText = "SELECT * FROM ProdajaNamestaja WHERE (Id IN(SELECT ProdajaId FROM JedinicaProdaje WHERE Obrisan=0 AND NamestajId IN (SELECT Id FROM Namestaj WHERE Naziv LIKE @parametarS))OR Kupac LIKE @parametarS OR BrRacuna LIKE @parametarS) AND Obrisan=0";
                        cmd.Parameters.AddWithValue("parametarS","%" + parametarS + "%");
                        break;
                }
                
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
        public static ObservableCollection<ProdajaNamestaja> Sort(SortBy sortBy)
        {
            var listaProdaja = new ObservableCollection<ProdajaNamestaja>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                cmd.CommandText = "SELECT * FROM ProdajaNamestaja WHERE Obrisan=0";
                switch (sortBy)
                {
                    case SortBy.Kupac_Opadajuce:
                        cmd.CommandText += " ORDER BY Kupac DESC";
                        break;
                    case SortBy.Kupac_Rastuce:
                        cmd.CommandText += " ORDER BY Kupac ASC";
                        break;
                    case SortBy.BrRacuna_Opadajuce:
                        cmd.CommandText += " ORDER BY BrRacuna DESC";
                        break;
                    case SortBy.BrRacuna_Rastuce:
                        cmd.CommandText += " ORDER BY BrRacuna ASC";
                        break;
                    case SortBy.DatumProdaje_Opadajuce:
                        cmd.CommandText += " ORDER BY DatumProdaje DESC";
                        break;
                    case SortBy.DatumProdaje_Rastuce:
                        cmd.CommandText += " ORDER BY DatumProdaje ASC";
                        break;
                    case SortBy.UkupnaCena_Opadajuce:
                        cmd.CommandText += " ORDER BY UkupnaCena DESC";
                        break;
                    case SortBy.UkupnaCena_Rastuce:
                        cmd.CommandText += " ORDER BY UkupnaCena ASC";
                        break;
                }

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
        public static ObservableCollection<ProdajaNamestaja> SearchAndOrSort(GlavniWindow.DoSearch doSearch, string parametarS, DateTime parametarDT, SortBy sortBy)
        {
            var listaProdaja = new ObservableCollection<ProdajaNamestaja>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                switch (doSearch)
                {
                    case GlavniWindow.DoSearch.Date:
                        cmd.CommandText = "SELECT * FROM ProdajaNamestaja WHERE Obrisan=0 AND DatumProdaje = @parametarDT";
                        cmd.Parameters.AddWithValue("parametarDT", parametarDT);
                        break;
                    case GlavniWindow.DoSearch.Other:
                        cmd.CommandText = "SELECT * FROM ProdajaNamestaja WHERE (Id IN(SELECT ProdajaId FROM JedinicaProdaje WHERE Obrisan=0 AND NamestajId IN (SELECT Id FROM Namestaj WHERE Naziv LIKE @parametarS))OR Kupac LIKE @parametarS OR BrRacuna LIKE @parametarS) AND Obrisan=0";
                        cmd.Parameters.AddWithValue("parametarS", "%" + parametarS + "%");
                        break;
                    case GlavniWindow.DoSearch.No:
                        cmd.CommandText = "SELECT * FROM ProdajaNamestaja WHERE Obrisan=0";
                        break;
                }
                switch (sortBy)
                {
                    case SortBy.Kupac_Opadajuce:
                        cmd.CommandText += " ORDER BY Kupac DESC";
                        break;
                    case SortBy.Kupac_Rastuce:
                        cmd.CommandText += " ORDER BY Kupac ASC";
                        break;
                    case SortBy.BrRacuna_Opadajuce:
                        cmd.CommandText += " ORDER BY BrRacuna DESC";
                        break;
                    case SortBy.BrRacuna_Rastuce:
                        cmd.CommandText += " ORDER BY BrRacuna ASC";
                        break;
                    case SortBy.DatumProdaje_Opadajuce:
                        cmd.CommandText += " ORDER BY DatumProdaje DESC";
                        break;
                    case SortBy.DatumProdaje_Rastuce:
                        cmd.CommandText += " ORDER BY DatumProdaje ASC";
                        break;
                    case SortBy.UkupnaCena_Opadajuce:
                        cmd.CommandText += " ORDER BY UkupnaCena DESC";
                        break;
                    case SortBy.UkupnaCena_Rastuce:
                        cmd.CommandText += " ORDER BY UkupnaCena ASC";
                        break;
                }

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
    }
}
