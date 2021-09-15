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

                    SqlDataReader reader = command.ExecuteReader();
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

            return null;
        }

        public static List<Dezurstvo> GetDezurstva(int id_korisnika)
        {
            List<Dezurstvo> dezurstva = new List<Dezurstvo>();
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select d.id, i.nazivRoka, d.predmet, d.vreme, d.sala from Dezurstvo d
                                join IspitniRok i on d.id_roka = i.id
                                where d.id_korisnika = @id_korisnika
                                and i.aktivan = 1;";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Dezurstvo dezurstvo = new Dezurstvo();
                        dezurstvo.Id = reader.GetInt32(0);
                        dezurstvo.Rok = reader.GetString(1);
                        dezurstvo.Predmet = reader.GetString(2);
                        dezurstvo.Vreme = reader.GetDateTime(3);
                        dezurstvo.Sala = reader.GetString(4);

                        dezurstvo.Id_korisnika = id_korisnika;

                        dezurstva.Add(dezurstvo);
                    }
                }
            }

            return dezurstva;
        }

        public static Preference GetPreference(int id_korisnika)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = "select * from preference where id_korisnika = @id_korisnika";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    SqlDataReader reader = command.ExecuteReader();
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

        

    }
}
