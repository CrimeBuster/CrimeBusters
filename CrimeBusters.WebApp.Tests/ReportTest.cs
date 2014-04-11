using System;
using System.Web;
using CrimeBusters.WebApp.Models.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrimeBusters.WebApp.Models.Report;
using CrimeBusters.WebApp.Models.Users;
using CrimeBusters.WebApp.Models.DAL;
using System.Collections.Generic;

namespace CrimeBusters.WebApp.Tests
{
    [TestClass]
    public class ReportTest
    {
        private Report _testReport;

        [TestInitialize]
        public void Initialize()
        {
            _testReport = new Report(
               ReportTypeEnum.HighPriority,
               "Test message",
               "40.104669",
               "-88.242254",
               "University of Illinois Campus",
               DateTime.UtcNow,
               new User("test.user"));
            _testReport.CreateReport(null, null);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _testReport = null;
            ReportsDAO.DeleteReportTest();
        }

        [TestMethod]
        public void TestCreateReportWithEmptyMessage()
        {
            Report report = new Report(
                ReportTypeEnum.HighPriority,
                "",
                "40.104669",
                "-88.242254",
                "University of Illinois Campus",
                DateTime.UtcNow,
                new User("test.user"));
            string result = report.CreateReport(null, null);

            Assert.IsTrue(result.Equals("success"), result);
        }

        [TestMethod()]
        public void TestCreateReportWithNullMessage()
        {
            Report report = new Report(
                ReportTypeEnum.HighPriority,
                null,
                "40.104669",
                "-88.242254",
                "University of Illinois Campus",
                DateTime.UtcNow,
                new User("test.user"));
            string result = report.CreateReport(null, null);

            Assert.IsTrue(result.Equals("success"), result);
        }

        [TestMethod]
        public void TestCreateReportWithNoFile()
        {
            Report report = new Report(
                ReportTypeEnum.HighPriority, 
                "Test message", 
                "40.104669", 
                "-88.242254", 
                "University of Illinois Campus",
                DateTime.UtcNow,
                new User("test.user"));
            string result = report.CreateReport(null, null);
       
            Assert.IsTrue(result.Equals("success"), result);
        }

        [TestMethod]
        public void TestCreateReportWithNoLatitude()
        {
            Report report = new Report(
                ReportTypeEnum.HighPriority,
                "Test message",
                 null,
                 "-88.242254",
                "University of Illinois Campus",
                DateTime.UtcNow,
                new User("test.user"));
            string result = report.CreateReport(null, null);

            Assert.IsFalse(result.Equals("success"), result);
        }

        [TestMethod]
        public void TestCreateReportWithNoLongitutde()
        {
            Report report = new Report(
                ReportTypeEnum.HighPriority,
                "Test message",
                 "40.104669",
                 null,
                "University of Illinois Campus",
                DateTime.UtcNow,
                new User("test.user"));
            string result = report.CreateReport(null, null);

            Assert.IsFalse(result.Equals("success"), result);
        }

        [TestMethod]
        public void TestCreateReportWithNoUser()
        {
            Report report = new Report(
                ReportTypeEnum.HighPriority,
                "Test message",
                 "40.104669",
                 "-88.242254",
                "University of Illinois Campus",
                DateTime.UtcNow,
                null);
            string result = report.CreateReport(null, null);

            Assert.IsFalse(result.Equals("success"), result);
        }


        [TestMethod]
        public void TestGetReports()
        {
            List<Report> reports = Report.GetReports();
            Assert.IsTrue(reports.Count > 0);
        }
    }
}
