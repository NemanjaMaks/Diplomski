using Diplomski.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DatabaseHelper
{
    public class SqlQueryHelper
    {

        public static User Login(string username, string password)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = "select * from korisnik where korisnickoIme = @username and lozinka = @password";
                using(SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user = new User();
                            user.Id = reader.GetInt32(0);
                            user.Name = reader.GetString(1);
                            user.LastName = reader.GetString(2);
                            user.Username = reader.GetString(3);
                            user.Password = reader.GetString(4);
                            user.IsAdmin = reader.GetBoolean(5);

                            return user;
                        }
                    }
                }
            }

            return null;
        }

        public static ListaDezurstva GetMojaDezurstva(int id_korisnika)
        {
            ListaDezurstva dezurstva = new ListaDezurstva();
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select d.id, i.nazivRoka, d.predmet, d.vreme, d.sala from Dezurstvo d
                                join IspitniRok i on d.id_roka = i.id
                                where d.id_korisnika = @id_korisnika
                                and i.aktivan = 1;";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dezurstvo dezurstvo = new Dezurstvo();
                            dezurstvo.id = reader.GetInt32(0);
                            dezurstvo.Rok = reader.GetString(1);
                            dezurstvo.Predmet = reader.GetString(2);
                            dezurstvo.Vreme = reader.GetDateTime(3);
                            dezurstvo.Sala = reader.GetString(4);

                            dezurstvo.id_korisnika = id_korisnika;

                            dezurstva.Add(dezurstvo);
                        }
                    } 
                }
            }

            return dezurstva;
        }

        public static ListaDezurstva GetDezurstva(int id_korisnika = -1)
        {
            ListaDezurstva dezurstva = new ListaDezurstva();
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select d.id, i.nazivRoka, d.predmet, d.vreme, d.sala, d.id_korisnika, k.ime + ' ' + k.prezime 
                                from Dezurstvo d
                                join IspitniRok i on d.id_roka = i.id
                                join Korisnik k on k.id = d.id_korisnika
                                where d.id_korisnika <> @id_korisnika
                                and i.aktivan = 1;";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dezurstvo dezurstvo = new Dezurstvo();
                            dezurstvo.id = reader.GetInt32(0);
                            dezurstvo.Rok = reader.GetString(1);
                            dezurstvo.Predmet = reader.GetString(2);
                            dezurstvo.Vreme = reader.GetDateTime(3);
                            dezurstvo.Sala = reader.GetString(4);
                            dezurstvo.id_korisnika = reader.GetInt32(5);
                            dezurstvo.ImeKorisnika = reader.GetString(6);

                            dezurstva.Add(dezurstvo);
                        }
                    }
                }
            }

            return dezurstva;
        }

        public static Dezurstvo GetDezurstvo(int id_dezurstva)
        {
            Dezurstvo dezurstvo = null;
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select d.id, i.nazivRoka, d.predmet, d.vreme, d.sala, d.id_korisnika from Dezurstvo d
                                join IspitniRok i on d.id_roka = i.id
                                where d.id = @id_dezurstva;";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_dezurstva", id_dezurstva);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dezurstvo = new Dezurstvo();
                            dezurstvo.id = reader.GetInt32(0);
                            dezurstvo.Rok = reader.GetString(1);
                            dezurstvo.Predmet = reader.GetString(2);
                            dezurstvo.Vreme = reader.GetDateTime(3);
                            dezurstvo.Sala = reader.GetString(4);
                            dezurstvo.id_korisnika = reader.GetInt32(5);
                        }
                    }
                }
            }

            return dezurstvo;
        }

        public static Preference GetPreference(int id_korisnika)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = "select * from preference where id_korisnika = @id_korisnika";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Preference preference = new Preference();
                            preference.Id = reader.GetInt32(0);
                            preference.Id_korisnika = reader.GetInt32(1);
                            preference.BezPauze = reader.GetBoolean(2);
                            preference.Uvece = reader.GetBoolean(3);
                            preference.Vikend = reader.GetBoolean(4);

                            return preference;
                        }
                    }  
                }
            }

            //Ako nije pronasao preference za korisnika, dodaj nove i njih vrati
            CreatePreference(id_korisnika);
            return GetPreference(id_korisnika);
        }

        public static void ChangePreference(Preference preference)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"update preference 
                        set vikend = @vikend,
                            uvece = @uvece,
                            bezPauze = @bezPauze
                        where id = @id";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id", preference.Id);
                    command.Parameters.AddWithValue("vikend", preference.Vikend);
                    command.Parameters.AddWithValue("uvece", preference.Uvece);
                    command.Parameters.AddWithValue("bezPauze", preference.BezPauze);

                    command.ExecuteNonQuery();
                }
            }
        }

        private static void CreatePreference(int id_korisnika) 
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = "insert into preference (id_korisnika) values(@id_korisnika)";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<Zahtev> GetPoslateZahteve(int id_korisnika)
        {
            List<Zahtev> zahtevi = new List<Zahtev>();
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select z.id, z.korisnik_od, z.korisnik_za, k.ime + ' ' + k.prezime as ime, z.status, z.id_dezurstva_od, z.id_dezurstva_za
                                from Zahtevi z
                                join Dezurstvo d on z.id_dezurstva_za = d.id
                                join Korisnik k on k.id = z.korisnik_za
                                where z.korisnik_od = @id_korisnika and status <> 'zatvoren';";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int korisnik_od = reader.GetInt32(1);
                            int korisnik_za = reader.GetInt32(2);
                            string ime_korisnika = reader.GetString(3);
                            string status = reader.GetString(4);
                            int id_dezurstva_od = reader.GetInt32(5);
                            int id_dezurstva_za = reader.GetInt32(6);
                            Zahtev zahtev = new Zahtev(id, korisnik_od, korisnik_za, ime_korisnika, status, id_dezurstva_od, id_dezurstva_za);

                            zahtevi.Add(zahtev);
                        }
                    }
                }
            }

            return zahtevi;
        }

        public static List<Zahtev> GetPrimljeneZahteve(int id_korisnika)
        {
            List<Zahtev> zahtevi = new List<Zahtev>();
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select z.id, z.korisnik_od, z.korisnik_za, k.ime + ' ' + k.prezime as ime, z.status, z.id_dezurstva_od, z.id_dezurstva_za
                                from Zahtevi z
                                join Dezurstvo d on z.id_dezurstva_za = d.id
                                join Korisnik k on k.id = z.korisnik_od
                                where z.korisnik_za = @id_korisnika and status = 'poslat';";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int korisnik_od = reader.GetInt32(1);
                            int korisnik_za = reader.GetInt32(2);
                            string ime_korisnika = reader.GetString(3);
                            string status = reader.GetString(4);
                            int id_dezurstva_od = reader.GetInt32(5);
                            int id_dezurstva_za = reader.GetInt32(6);
                            Zahtev zahtev = new Zahtev(id, korisnik_od, korisnik_za, ime_korisnika, status, id_dezurstva_od, id_dezurstva_za);

                            zahtevi.Add(zahtev);
                        }
                    }
                }
            }

            return zahtevi;
        }

        public static void RequestZamena(int id_dezurstva_za, int id_dezurstva_od, int id_korisnika_za, int id_korisnika_od)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"insert into zahtevi (korisnik_od, korisnik_za, status, id_dezurstva_od, id_dezurstva_za)
                                values(@id_korisnika_od, @id_korisnika_za, 'poslat', @id_dezurstva_od, @id_dezurstva_za)";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika_od", id_korisnika_od);
                    command.Parameters.AddWithValue("id_korisnika_za", id_korisnika_za);
                    command.Parameters.AddWithValue("id_dezurstva_od", id_dezurstva_od);
                    command.Parameters.AddWithValue("id_dezurstva_za", id_dezurstva_za);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void ChangeZahtevStatus(string status, int id)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"update zahtevi 
                                set status = @status
                                where id = @id";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("status", status);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Zameni(int id_dezurstva_za, int id_dezurstva_od, int id_korisnika_za, int id_korisnika_od)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"update dezurstvo 
                                set id_korisnika = @id_korisnika
                                where id = @id";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id", id_dezurstva_za);
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika_od);

                    command.ExecuteNonQuery();
                }
            }

            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"update dezurstvo 
                                set id_korisnika = @id_korisnika
                                where id = @id";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id", id_dezurstva_od);
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika_za);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static PreferenceStatistika GetPreferenceStatistika()
        {
            string sql = @"select 'Rad vikendom', 'Da', (select count(*) from Preference where vikend = 1)
                            union all
                            select 'Rad vikendom', 'Ne',(select count(*) from Preference where vikend = 0)
                            union all
                            select 'Deo dana', 'Prva_polovina_dana',(select count(*) from Preference where uvece = 0)
                            union all
                            select 'Deo dana', 'Druga_polovina_dana',(select count(*) from Preference where uvece = 1)
                            union all
                            select 'Pauza', 'Bez_pauze',(select count(*) from Preference where bezPauze = 1)
                            union all
                            select 'Pauza', 'Sa_pauzom',(select count(*) from Preference where bezPauze = 0);";

            PreferenceStatistika statistika = new PreferenceStatistika();
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string key = reader.GetString(0);
                            string name = reader.GetString(1);
                            int val = reader.GetInt32(2);

                            if (statistika.ContainsKey(key))
                            {
                                statistika[key].Add(new NameValuePair { Name = name, Value = val });
                            }
                            else
                            {
                                statistika.Add(key, new List<NameValuePair>() { new NameValuePair { Name = name, Value = val } });
                            }
                        }
                    }
                }
            }

            return statistika;
        }

    }
}
