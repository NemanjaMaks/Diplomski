using Diplomski.DataModel;
using System;
using System.Collections.Generic;
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

    }
}
