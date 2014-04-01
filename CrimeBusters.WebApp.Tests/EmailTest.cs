using System;
using CrimeBusters.WebApp.Models.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrimeBusters.WebApp.Tests
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public void TestSendEmail()
        {
            Email email = new Email
            {
                FromEmail = "admin@illinoiscrimebusters.com",
                FromName = "Crime Buster Admin",
                ToEmail = "chris.ababan@gmail.com",
                Subject = "Test email",
                Body = "<h1>Hello World</h1>",
                IsHighImportance = true
            };
            string sendingStatus = email.SendEmail();
            Assert.IsTrue(sendingStatus.Equals("success"), sendingStatus);
        }
    }
}
