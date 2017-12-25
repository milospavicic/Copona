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
    class SalonDAO
    {
        public static ObservableCollection<Salon> GetAll()
        {
            var tempSalonList = new ObservableCollection<Salon>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM Salon WHERE Obrisan=0";
                da.SelectCommand = cmd;
                da.Fill(ds, "Salon"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Salon"].Rows)
                {
                    var tempSalon = new Salon();
                    tempSalon.Id = (int)row["IdSalona"];
                    tempSalon.Naziv = row["Naziv"].ToString();
                    tempSalon.Adresa = row["Adresa"].ToString();
                    tempSalon.BrojTelefona = row["BrojTelefona"].ToString();
                    tempSalon.Email = row["Email"].ToString();
                    tempSalon.WebAdresa = row["WebAdresa"].ToString();
                    tempSalon.BrRacuna = row["BrRacuna"].ToString();
                    tempSalon.Pib = (int)row["Pib"];
                    tempSalon.MaticniBr = (int)row["MaticniBr"];

                    tempSalon.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    tempSalonList.Add(tempSalon);
                }
            }
            return tempSalonList;
        }
        public static void Update(Salon szu)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                DataSet ds = new DataSet();


                cmd.CommandText = "UPDATE Salon SET Naziv=@Naziv,Adresa=@Adresa,BrojTelefona=@BrojTelefona,Email=@Email,WebAdresa=@WebAdresa,BrRacuna=@BrRacuna,Pib=@Pib,MaticniBr=@MaticniBr,Obrisan=@Obrisan WHERE IdSalona = @IdSalona";
                cmd.CommandText += " SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdSalona", szu.Id);
                cmd.Parameters.AddWithValue("Naziv", szu.Naziv);
                cmd.Parameters.AddWithValue("Adresa", szu.Adresa);
                cmd.Parameters.AddWithValue("BrojTelefona", szu.BrojTelefona);
                cmd.Parameters.AddWithValue("Email", szu.Email);
                cmd.Parameters.AddWithValue("WebAdresa", szu.WebAdresa);
                cmd.Parameters.AddWithValue("BrRacuna", szu.BrRacuna);
                cmd.Parameters.AddWithValue("Pib", szu.Pib);
                cmd.Parameters.AddWithValue("MaticniBr", szu.MaticniBr);
                cmd.Parameters.AddWithValue("Obrisan", szu.Obrisan);

                cmd.ExecuteNonQuery();
            }
            foreach (var salon in Projekat.Instance.Salon)
            {
                if (salon.Id == szu.Id)
                {
                    salon.Naziv = szu.Naziv;
                    salon.Adresa = szu.Adresa;
                    salon.BrojTelefona = szu.BrojTelefona;
                    salon.Email = szu.Email;
                    salon.WebAdresa = szu.WebAdresa;
                    salon.BrRacuna = szu.BrRacuna;
                    salon.Pib = szu.Pib;
                    salon.MaticniBr = szu.MaticniBr;
                    salon.Obrisan = szu.Obrisan;
                }
            }
        }
        
    }
}
