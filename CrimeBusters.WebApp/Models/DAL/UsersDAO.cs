using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CrimeBusters.WebApp.Models.DAL
{
    /// <summary>
    ///  Contains all the data access logic for the Users module.
    /// </summary>
    public class UsersDAO
    {
        public static SqlDataReader GetUserInformation(string userName) {
            SqlConnection connection = ConnectionManager.GetConnection();
            SqlCommand command = new SqlCommand("GetUserInformation", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserName", userName);

            return command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection);
        }
    }
}