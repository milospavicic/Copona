using POP_SF39_2016.model;
using POP_SF39_2016_GUI.gui;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace POP_SF39_2016_GUI.DAO
{
    class AkcijaDAO
    {
        public enum SortBy
        {
            Naziv_Opadajuce,
            Naziv_Rastuce,
            PocetakAkcije_Opadajuce,
            PocetakAkcije_Rastuce,
            KrajAkcije_Opadajuce,
            KrajAkcije_Rastuce,
            Nesortirano
        }

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
                    a.Naziv = row["Naziv"].ToString();
                    a.PocetakAkcije = DateTime.Parse(row["DatumPocetak"].ToString());
                    a.KrajAkcije = DateTime.Parse(row["DatumKraj"].ToString());
                    a.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    listaAkcija.Add(a);
                }
            }
            return listaAkcija;
        }
        public static ObservableCollection<Akcija> Search(string parametarS, DateTime parametarDT, GlavniWindow.DoSearch doSearch)
        {
            var listaAkcija = new ObservableCollection<Akcija>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                switch (doSearch)
                {
                    case GlavniWindow.DoSearch.Other:
                        cmd.CommandText = "SELECT * FROM AKCIJA WHERE (IdAkcije IN (SELECT IdAkcije FROM NaAkciji WHERE Obrisan=0 AND IdNamestaja IN (SELECT Id FROM NAMESTAJ WHERE Naziv LIKE @parametarS)) OR Naziv LIKE @parametarS) AND Obrisan=0";
                        cmd.Parameters.AddWithValue("parametarS", "%" + parametarS.Trim() + "%");
                        break;
                    case GlavniWindow.DoSearch.Date:
                        cmd.CommandText = "SELECT * FROM Akcija WHERE Obrisan=0 AND (@parametarDT BETWEEN DatumPocetak AND DatumKraj)";
                        cmd.Parameters.AddWithValue("parametarDT", parametarDT);
                        break;
                }

                da.SelectCommand = cmd;
                da.Fill(ds, "Akcija"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Akcija"].Rows)
                {
                    var a = new Akcija();
                    a.Id = (int)row["IdAkcije"];
                    a.Naziv = row["Naziv"].ToString();
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


                cmd.CommandText = "INSERT INTO Akcija(Naziv,DatumPocetak,DatumKraj,Obrisan) VALUES (@Naziv,@DatumPocetak,@DatumKraj,@Obrisan)";
                cmd.CommandText += " Select SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("Naziv", nn.Naziv);
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


                cmd.CommandText = "UPDATE Akcija SET Naziv=@Naziv,DatumPocetak=@DatumPocetak,DatumKraj=@DatumKraj,Obrisan=@Obrisan WHERE IdAkcije = @IdAkcije";
                cmd.CommandText += " SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdAkcije", azu.Id);
                cmd.Parameters.AddWithValue("Naziv", azu.Naziv);
                cmd.Parameters.AddWithValue("DatumPocetak", azu.PocetakAkcije);
                cmd.Parameters.AddWithValue("DatumKraj", azu.KrajAkcije);
                cmd.Parameters.AddWithValue("Obrisan", azu.Obrisan);

                cmd.ExecuteNonQuery();
            }
            foreach (var akcija in Projekat.Instance.Akcija)
            {
                if (akcija.Id == azu.Id)
                {
                    akcija.Naziv = azu.Naziv;
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
        public static ObservableCollection<Akcija> Sort(SortBy sortBy)
        {
            var listaAkcija = new ObservableCollection<Akcija>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM Akcija WHERE Obrisan=0";
                switch (sortBy)
                {
                    case SortBy.Naziv_Opadajuce:
                        cmd.CommandText += " ORDER BY Naziv DESC";
                        break;
                    case SortBy.Naziv_Rastuce:
                        cmd.CommandText += " ORDER BY Naziv ASC";
                        break;
                    case SortBy.PocetakAkcije_Opadajuce:
                        cmd.CommandText += " ORDER BY DatumPocetak DESC";
                        break;
                    case SortBy.PocetakAkcije_Rastuce:
                        cmd.CommandText += " ORDER BY DatumPocetak ASC";
                        break;
                    case SortBy.KrajAkcije_Opadajuce:
                        cmd.CommandText += " ORDER BY DatumKraj DESC";
                        break;
                    case SortBy.KrajAkcije_Rastuce:
                        cmd.CommandText += " ORDER BY DatumKraj ASC";
                        break;
                }
                da.SelectCommand = cmd;
                da.Fill(ds, "Akcija"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Akcija"].Rows)
                {
                    var a = new Akcija();
                    a.Id = (int)row["IdAkcije"];
                    a.Naziv = row["Naziv"].ToString();
                    a.PocetakAkcije = DateTime.Parse(row["DatumPocetak"].ToString());
                    a.KrajAkcije = DateTime.Parse(row["DatumKraj"].ToString());
                    a.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    listaAkcija.Add(a);
                }
            }
            return listaAkcija;
        }
        public static ObservableCollection<Akcija> SearchAndOrSort(GlavniWindow.DoSearch doSearch, string parametarS, DateTime parametarDT, SortBy sortBy)
        {
            var listaAkcija = new ObservableCollection<Akcija>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                switch (doSearch)
                {
                    case GlavniWindow.DoSearch.Other:
                        cmd.CommandText = "SELECT * FROM AKCIJA WHERE (IdAkcije IN (SELECT IdAkcije FROM NaAkciji WHERE Obrisan=0 AND IdNamestaja IN (SELECT Id FROM NAMESTAJ WHERE Naziv LIKE @parametarS)) OR Naziv LIKE @parametarS) AND Obrisan=0";
                        cmd.Parameters.AddWithValue("parametarS", "%" + parametarS.Trim() + "%");
                        break;
                    case GlavniWindow.DoSearch.Date:
                        cmd.CommandText = "SELECT * FROM Akcija WHERE Obrisan=0 AND (@parametarDT BETWEEN DatumPocetak AND DatumKraj)";
                        cmd.Parameters.AddWithValue("parametarDT", parametarDT);
                        break;
                    case GlavniWindow.DoSearch.No:
                        cmd.CommandText = "SELECT * FROM Akcija WHERE Obrisan=0";
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
                    case SortBy.PocetakAkcije_Opadajuce:
                        cmd.CommandText += " ORDER BY DatumPocetak DESC";
                        break;
                    case SortBy.PocetakAkcije_Rastuce:
                        cmd.CommandText += " ORDER BY DatumPocetak ASC";
                        break;
                    case SortBy.KrajAkcije_Opadajuce:
                        cmd.CommandText += " ORDER BY DatumKraj DESC";
                        break;
                    case SortBy.KrajAkcije_Rastuce:
                        cmd.CommandText += " ORDER BY DatumKraj ASC";
                        break;
                }
                da.SelectCommand = cmd;
                da.Fill(ds, "Akcija"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Akcija"].Rows)
                {
                    var a = new Akcija();
                    a.Id = (int)row["IdAkcije"];
                    a.Naziv = row["Naziv"].ToString();
                    a.PocetakAkcije = DateTime.Parse(row["DatumPocetak"].ToString());
                    a.KrajAkcije = DateTime.Parse(row["DatumKraj"].ToString());
                    a.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    listaAkcija.Add(a);
                }
            }
            return listaAkcija;
        }
        public static void StillActiveButPastEndDate()
        {
            var listaAkcija = new ObservableCollection<Akcija>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();


                cmd.CommandText = "SELECT * FROM Akcija WHERE Obrisan=0 AND DatumKraj < @DatumKraj";
                cmd.Parameters.AddWithValue("DatumKraj",DateTime.Now);
                da.SelectCommand = cmd;
                da.Fill(ds, "Akcija"); //izvrsavanje upita

                foreach (DataRow row in ds.Tables["Akcija"].Rows)
                {
                    var a = new Akcija();
                    a.Id = (int)row["IdAkcije"];
                    a.Naziv = row["Naziv"].ToString();
                    a.PocetakAkcije = DateTime.Parse(row["DatumPocetak"].ToString());
                    a.KrajAkcije = DateTime.Parse(row["DatumKraj"].ToString());
                    a.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    listaAkcija.Add(a);
                }
            }
            if (listaAkcija.Count != 0)
            {
                foreach (var akcijaZaBrisanje in listaAkcija)
                {
                    foreach (var tempNa in NaAkcijiDAO.GetAllNAForActionId(akcijaZaBrisanje.Id))
                        NaAkcijiDAO.Delete(tempNa);
                    AkcijaDAO.Delete(akcijaZaBrisanje);
                }
            }
        }
    }
}

