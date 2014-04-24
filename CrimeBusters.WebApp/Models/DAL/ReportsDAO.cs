using System.Collections.Generic;
using System.Security.Policy;
using CrimeBusters.WebApp.Models.Report;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CrimeBusters.WebApp.Models.DAL
{
    /// <summary>
    /// Contains all the data access logic for Reports module.
    /// </summary>
    public class ReportsDAO
    {
        public static void CreateReport(ReportTypeEnum reportTypeId, String message, 
            String latitude, String longitude, String location, DateTime dateReported,
            String userName, String photo1Url, String photo2Url, String photo3Url, 
            String videoUrl, String audioUrl) 
        { 
            using (SqlConnection connection = ConnectionManager.GetConnection()) 
            {
                SqlCommand command = new SqlCommand("CreateReport", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ReportTypeId", reportTypeId);
                command.Parameters.AddWithValue("@Message", message);
                command.Parameters.AddWithValue("@Latitude", latitude);
                command.Parameters.AddWithValue("@Longitude", longitude);
                command.Parameters.AddWithValue("@Location", location);
                command.Parameters.AddWithValue("@DateReported", dateReported);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Photo1", photo1Url);
                command.Parameters.AddWithValue("@Photo2", photo2Url);
                command.Parameters.AddWithValue("@Photo3", photo3Url);
                command.Parameters.AddWithValue("@Video", videoUrl);
                command.Parameters.AddWithValue("@Audio", audioUrl);

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