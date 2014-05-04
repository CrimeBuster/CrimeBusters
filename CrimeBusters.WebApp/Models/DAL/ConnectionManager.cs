using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CrimeBusters.WebApp.Models.DAL
{
    public class ConnectionManager
    {
        /// <summary>
        /// Opens and Returns the SQL Database Connection as configured on server
        /// </summary>
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cbConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }
    }
}