using System;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using CrimeBusters.WebApp.Models.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoginModel = CrimeBusters.WebApp.Models.Login;
using CrimeBusters.WebApp.Models.Users;
using CrimeBusters.WebApp.Models.DAL;
using CrimeBusters.WebApp.Services;



namespace CrimeBusters.WebApp.Tests
{
    [TestClass]
    public class LoginTest
    {
        [TestInitialize]
        public void Initialize()
        {
            if (Membership.FindUsersByName("test.user2") != null)
            {
                Membership.DeleteUser("test.user2");
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            Membership.DeleteUser("test.user2");
            LoginDAO.DeleteUser("test.user2");
        }

        [TestMethod]
        public void TestCreateUser()
        {
            IUser newUser = new User
            {
                UserName = "test.user2",
                Password = "test123",
                FirstName = "FirstName Test",
                LastName = "LastName Test",
                Email = "test2@crimbusters.com"
            };

            LoginModel.Login login = new LoginModel.Login(newUser);
            MembershipCreateStatus createStatus = login.CreateUser(new TestContentLocator());
            Assert.AreEqual(MembershipCreateStatus.Success, createStatus,
                "User creation failure.");
        }

        [TestMethod]
        public void TestValidateUser()
        {
            IUser user = new User
            {
                UserName = "test.user",
                Password = "test123"
            };
            LoginModel.Login login = new LoginModel.Login(user);
            Assert.IsTrue(login.ValidateUser().Equals("success"));
        }
    }
}
