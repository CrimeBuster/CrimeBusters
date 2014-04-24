﻿using System.IO;
using System.Web.UI.WebControls;
using CrimeBusters.WebApp.Models.DAL;
using CrimeBusters.WebApp.Models.Documents;
using CrimeBusters.WebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CrimeBusters.WebApp.Models.Util;

namespace CrimeBusters.WebApp.Models.Report
{
    /// <summary>
    /// Contains the business logic for the Report module.
    /// </summary>
    public class Report
    {
        private String _reportType;
        private List<IDocument> _media = new List<IDocument>();
        private List<String> _urlList = new List<string>();
 
        public int ReportId { get; set; }
        public ReportTypeEnum ReportTypeId { get; set; }
        public String ReportType 
        {
            get 
            {
                if (String.IsNullOrEmpty(_reportType))
                {
                    return Enum.GetName(typeof(ReportTypeEnum), this.ReportTypeId);
                }
                return _reportType;
            }
            set
            {
                _reportType = value;
            }
        }
        public String MarkerImage { get; set; }
        public String Message { get; set; }
        public String Latitude { get; set; }
        public String Longitude { get; set; }
        public String Location { get; set; }
        public DateTime DateReported { get; set; }
        public string TimeStampString
        {
            get
            {
                return Convert.ToString(this.DateReported,
                            System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        public IUser User { get; set; }
        public List<IDocument> Media 
        {
            get { return _media; }
        }
        public List<String> UrlList
        {
            get { return _urlList; }
        }

        /// <summary>
        /// We do not differentiate a certain media when we dump the values in the database. 
        /// Since we only need the MediaUrl when we retrieve the values in the database, 
        /// </summary>
        public List<String> MediaUrl { get; set; }
        public Report() { }
        public Report(int reportId) 
        {
            this.ReportId = reportId;
        }
        public Report(ReportTypeEnum reportTypeId, String message, 
            String latitude, String longitude, String location,
            DateTime dateReported, IUser user) 
        {
            this.ReportTypeId = reportTypeId;
            this.Message = message;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Location = location;
            this.DateReported = dateReported;
            this.User = user;
        }

        /// <summary>
        /// Creates a report that will be saved to the database.
        /// </summary>
        /// <returns>success for successful insert, else will return the error message.</returns>
        public string CreateReport(IContentLocator contentLocator) 
        {
            foreach (var document in Media.Where(document => document != null))
            {
                AddUrlList(document.Url);
                document.Save(contentLocator);
            }

            try
            {
                ReportsDAO.CreateReport(ReportTypeId, Message, 
                    Latitude, Longitude, Location, DateReported, 
                    User.UserName, UrlList);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Gets a list of reports from the database.
        /// </summary>
        /// <returns>The list of Report object.</returns>
        public static List<Report> GetReports()
        {
            SqlDataReader reader = ReportsDAO.GetReports();
            List<Report> reports = new List<Report>();

            try
            {
                int oReportId = reader.GetOrdinal("ReportId");
                int oReportType = reader.GetOrdinal("ReportType");
                int oMarkerImage = reader.GetOrdinal("MarkerImage");
                int oMessage = reader.GetOrdinal("Message");
                int oLatitude = reader.GetOrdinal("Latitude");
                int oLongitude = reader.GetOrdinal("Longitude");
                int oTimeStamp = reader.GetOrdinal("TimeStamp");
                int oUserName = reader.GetOrdinal("UserName");
                int oFirstName = reader.GetOrdinal("FirstName");
                int oLastName = reader.GetOrdinal("LastName");
                int oGender = reader.GetOrdinal("Gender");
                int oEmail = reader.GetOrdinal("Email");
                int oPhoneNumber = reader.GetOrdinal("PhoneNumber");
                int oAddress = reader.GetOrdinal("Address");
                int oZipCode = reader.GetOrdinal("ZipCode");

                while (reader.Read())
                {
                    Report report = new Report
                    {
                        ReportId = Convert.ToInt32(reader[oReportId]),
                        ReportType = reader[oReportType].ToString(),
                        MarkerImage = reader[oMarkerImage].ToString(),
                        Message = reader[oMessage].ToString(),
                        Latitude = reader[oLatitude].ToString(),
                        Longitude = reader[oLongitude].ToString(),
                        DateReported = Convert.ToDateTime(reader[oTimeStamp]),
                        User = new User 
                        {
                            UserName = reader[oUserName].ToString(),
                            FirstName = reader[oFirstName].ToString(),
                            LastName = reader[oLastName].ToString(),
                            Gender = reader[oGender].ToString(),
                            Email = reader[oEmail].ToString(),
                            PhoneNumber = reader[oPhoneNumber].ToString(),
                            Address = reader[oAddress].ToString(),
                            ZipCode = reader[oZipCode].ToString()
                        }
                    };

                    for (int i = 1; i <= 5; i++)
                    {
                        String mediaUrl = reader["Media" + i].ToString();
                        if (!String.IsNullOrEmpty(mediaUrl))
                        {
                            report.AddUrlList(mediaUrl);
                        }
                    }
                    reports.Add(report);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                reader.Close();
            }
            return reports;
        }

        /// <summary>
        /// Deletes a report from the database.
        /// </summary>
        /// <returns>true if the deletion is successful.</returns>
        public bool DeleteReport()
        {
            try
            {
                ReportsDAO.DeleteReport(this.ReportId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a media to the list of media associated to a report.
        /// </summary>
        /// <param name="document">Document that implements the IDocument interface.</param>
        public void AddMedia(IDocument document)
        {
            _media.Add(document);
        }

        public void AddUrlList(String url)
        {
            _urlList.Add(url);
        }
    }
}