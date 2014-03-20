using System;
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
               ReportTypeEnum.ALERT,
               "Test message",
               "40.104669",
               "-88.242254",
               "",
               DateTime.UtcNow,
               new User("test.user"));
            _testReport.CreateReport();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _testReport = null;
            ReportsDAO.DeleteReportTest();
        }

        [TestMethod]
        public void TestCreateReport()
        {
            Report report = new Report(
                ReportTypeEnum.ALERT, 
                "Test message", 
                "40.104669", 
                "-88.242254", 
                "", 
                DateTime.UtcNow, 
                new User("test.user"));
            string result = report.CreateReport();
       
            Assert.IsTrue(result.Equals("success"), "The method should return success.");
        }

        [TestMethod]
        public void TestGetReports()
        {
            List<Report> reports = Report.GetReports();
            Assert.IsTrue(reports.Count > 0);
        }
    }
}
