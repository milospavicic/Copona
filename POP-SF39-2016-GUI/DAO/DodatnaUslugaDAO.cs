using POP_SF39_2016.model;
using POP_SF39_2016_GUI.gui;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace POP_SF39_2016_GUI.DAO
{
    class DodatnaUslugaDAO
    {
        public enum SortBy
        {
            Naziv_Opadajuce,
            Naziv_Rastuce,
            Cena_Opadajuce,
            Cena_Rastuce,
            Nesortirano
        }
        public static ObservableCollection<DodatnaUsluga> GetAll()
        {
            try
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji dodatnih usluga. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
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
        public static ObservableCollection<DodatnaUsluga> GetAllNotSoldForId(int Id)
        {
            try
            {
                var dodatneUsluge = new ObservableCollection<DodatnaUsluga>();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();

                    cmd.CommandText = "SELECT * FROM DodatnaUsluga WHERE Obrisan=0 AND Id NOT IN (SELECT DodatnaUslugaId FROM ProdataDodatnaUsluga WHERE Obrisan=0 AND ProdajaId=@ProdajaId)";
                    cmd.Parameters.AddWithValue("ProdajaId", Id);
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji dodatnih usluga. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
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
        public static DodatnaUsluga GetById(int Id)
        {
            try
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji dodatnih usluga. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
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
        public static DodatnaUsluga Create(DodatnaUsluga du)
        {
            try
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji dodatnih usluga. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
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
        public static void Update(DodatnaUsluga du)
        {
            try
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji dodatnih usluga. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
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
        public static void Delete(DodatnaUsluga du)
        {
            if (du != null)
            {
                du.Obrisan = true;
                Update(du);
            }
        }
        public static ObservableCollection<DodatnaUsluga> SearchAndOrSort(GlavniWindow.DoSearch doSearch, string parametar, SortBy sortBy)
        {
            try
            {
                var dodatneUsluge = new ObservableCollection<DodatnaUsluga>();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();

                    switch (doSearch)
                    {
                        case GlavniWindow.DoSearch.Other:
                            cmd.CommandText = "SELECT * FROM DodatnaUsluga WHERE Obrisan=0 AND Naziv LIKE @parametar";
                            cmd.Parameters.AddWithValue("parametar", "%" + parametar + "%");
                            break;
                        case GlavniWindow.DoSearch.No:
                            cmd.CommandText = "SELECT * FROM DodatnaUsluga WHERE Obrisan=0";
                            break;
                    }
                    switch (sortBy)
                    {
                        case SortBy.Naziv_Opadajuce:
                            cmd.CommandText += " ORDER BY Naziv DESC";
                            break;
                        case SortBy.Naziv_Rastuce:
                            cmd.CommandText += " ORDER BY Naziv ASC";
                            break;
                        case SortBy.Cena_Opadajuce:
                            cmd.CommandText += " ORDER BY Cena DESC";
                            break;
                        case SortBy.Cena_Rastuce:
                            cmd.CommandText += " ORDER BY Cena ASC";
                            break;
                    }

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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji dodatnih usluga. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
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
