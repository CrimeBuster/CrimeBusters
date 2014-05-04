using System.Configuration;
using System.Data.SqlClient;

namespace CrimeBusters.WebApp.Models.DAL
{
    public class ConnectionManager
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cbConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }
    }
}