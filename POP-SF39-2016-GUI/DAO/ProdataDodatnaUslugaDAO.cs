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
using System.Windows;

namespace POP_SF39_2016_GUI.DAO
{
    class ProdataDodatnaUslugaDAO
    {
        public static ObservableCollection<ProdataDU> GetAll()
        {
            try
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji prodatih dodatnih usluga. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
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
        public static ObservableCollection<ProdataDU> GetAllForId(int Id)
        {
            try
            {
                var dodatneUsluge = new ObservableCollection<ProdataDU>();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();

                    cmd.CommandText = "SELECT * FROM ProdataDodatnaUsluga WHERE ProdajaId=@ProdajaId";
                    cmd.Parameters.AddWithValue("ProdajaId", Id);
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji prodatih dodatnih usluga. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
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
        public static ProdataDU Create(ProdataDU tn)
        {
            try
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
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji prodatih dodatnih usluga. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
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
        public static void Update(ProdataDU jp)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    DataSet ds = new DataSet();


                    cmd.CommandText = "UPDATE ProdataDodatnaUsluga SET DodatnaUslugaId=@DodatnaUslugaId,ProdajaId=@ProdajaId,Obrisan=@Obrisan WHERE Id = @Id";
                    cmd.CommandText += " SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.AddWithValue("Id", jp.Id);
                    cmd.Parameters.AddWithValue("DodatnaUslugaId", jp.DodatnaUslugaId);
                    cmd.Parameters.AddWithValue("ProdajaId", jp.ProdajaId); ;
                    cmd.Parameters.AddWithValue("Obrisan", jp.Obrisan);

                    cmd.ExecuteNonQuery();
                }
                foreach (var prodataDU in Projekat.Instance.ProdateDU)
                {
                    if (prodataDU.Id == jp.Id)
                    {
                        prodataDU.DodatnaUslugaId = jp.DodatnaUslugaId;
                        prodataDU.ProdajaId = jp.ProdajaId;
                        prodataDU.Obrisan = jp.Obrisan;
                    }
                }
            }
            catch (TypeInitializationException ex)
            {
                MessageBoxResult poruka = MessageBox.Show("Doslo je do greske pri inicijalizaciji prodatih dodatnih usluga. " + ex.Message, "Upozorenje", MessageBoxButton.OK);
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
        public static void Delete(ProdataDU du)
        {
            if (du != null)
            {
                du.Obrisan = true;
                Update(du);
            }
        }
    }
}
