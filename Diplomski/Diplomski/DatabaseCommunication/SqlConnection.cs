using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DatabaseCommunication
{
    public class SqlConnection
    {

        private const string ConnectionString = @"Server=DESKTOP-KK6BPK9\SQLSERVER19;Database=Diplomski;User Id=sa;Password=sifra123;";
        private static System.Data.SqlClient.SqlConnection connection = null;

        public static System.Data.SqlClient.SqlConnection GetConnection()
        {
            connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

    }
}
