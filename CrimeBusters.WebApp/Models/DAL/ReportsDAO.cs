using CrimeBusters.WebApp.Models.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CrimeBusters.WebApp.Models.DAL
{
    /// <summary>
    /// Contains all the data access logic for Reports module.
    /// </summary>
    public class ReportsDAO
    {
        public static void CreateReport(ReportTypeEnum reportTypeId, String message, 
            String latitude, String longitude, string resourceUrl, 
            String userName) 
        { 
            using (SqlConnection connection = ConnectionManager.GetConnection()) 
            {
                SqlCommand command = new SqlCommand("CreateReport", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ReportTypeId", reportTypeId);
                command.Parameters.AddWithValue("@Message", message);
                command.Parameters.AddWithValue("@Latitude", latitude);
                command.Parameters.AddWithValue("@Longitude", longitude);
                command.Parameters.AddWithValue("@ResourceUrl", resourceUrl);
                command.Parameters.AddWithValue("@UserName", userName);

                command.ExecuteNonQuery();
            }
        }

        public static SqlDataReader GetReports()
        {
            SqlConnection connection = ConnectionManager.GetConnection();
            SqlCommand command = new SqlCommand("GetReports", connection);
            command.CommandType = CommandType.StoredProcedure;

            return command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection);
        }

        public static void DeleteReport(int reportId)
        {
            using (SqlConnection connection = ConnectionManager.GetConnection())
            {
                SqlCommand command = new SqlCommand("DeleteReport", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ReportId", reportId);

                command.ExecuteNonQuery();
            }
        }

        public static void DeleteReportTest()
        {
            using (SqlConnection connection = ConnectionManager.GetConnection())
            {
                SqlCommand command = new SqlCommand("DeleteReportTest", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.ExecuteNonQuery();
            }
        }
    }
}