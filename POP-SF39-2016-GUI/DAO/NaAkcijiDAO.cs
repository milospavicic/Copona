using POP_SF39_2016.model;
using POP_SF39_2016_GUI.model;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace POP_SF39_2016_GUI.DAO
{
    class NaAkcijiDAO
    {
        public static ObservableCollection<NaAkciji> GetAll()
        {
            try
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji predmeta akcije. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
                return null;
            }
            catch (SqlException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Isteklo je vreme za povezivanje sa bazom. " + ex.Message + "\nPokusajte ponovo pokrenuti program za koji trenutak.", "Upozorenje", MessageBoxButton.OK);
                Environment.Exit(0);
                return null;
            }
            catch
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri citanju iz baze. ", "Upozorenje", MessageBoxButton.OK);
                return null;
            }
        }
        public static NaAkciji Create(NaAkciji na)
        {
            try
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji predmeta akcije. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
                return null;
            }
            catch (SqlException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Isteklo je vreme za povezivanje sa bazom. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
                return null;
            }
            catch
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri citanju iz baze. ", "Upozorenje", MessageBoxButton.OK);
                return null;
            }
        }
        public static void Update(NaAkciji na)
        {
            try
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji predmeta akcije. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
                return ;
            }
            catch (SqlException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Isteklo je vreme za povezivanje sa bazom. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
                return ;
            }
            catch
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri citanju iz baze. ", "Upozorenje", MessageBoxButton.OK);
                return ;
            }
        }
        public static void Delete(NaAkciji na)
        {
            if (na != null)
            {
                na.Obrisan = true;
                Update(na);
            }
        }
        public static ObservableCollection<NaAkciji> GetAllNAForActionId(int IdAkcije)
        {
            try
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji predmeta akcije. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
                return null;
            }
            catch (SqlException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Isteklo je vreme za povezivanje sa bazom. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
                return null;
            }
            catch
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri citanju iz baze. ", "Upozorenje", MessageBoxButton.OK);
                return null;
            }
        }
        public static int GetPopustForId(int IdNamestaja)
        {
            try
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji predmeta akcije. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
                return 0;
            }
            catch (SqlException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Isteklo je vreme za povezivanje sa bazom. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
                return 0;
            }
            catch
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri citanju iz baze. ", "Upozorenje", MessageBoxButton.OK);
                return 0;
            }
        }
        public static NaAkciji GetForNamestajId(int index)
        {
            try
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji predmeta akcije. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
                return null;
            }
            catch (SqlException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Isteklo je vreme za povezivanje sa bazom. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
                return null;
            }
            catch
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri citanju iz baze. ", "Upozorenje", MessageBoxButton.OK);
                return null;
            }
        }
    }
}
