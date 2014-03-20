using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrimeBusters.WebApp.Models.Users;

namespace CrimeBusters.WebApp.Tests
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestGetUserIsNotNull()
        {
            User testUser = User.GetUser("test.user");
            Assert.IsNotNull(testUser);
        }

        [TestMethod]
        public void TestGetUserDetails()
        {
            User testUser = User.GetUser("test.user");
            Assert.IsTrue(testUser.UserName.Equals("test.user"));
            Assert.IsTrue(testUser.FirstName.Equals("Test"), "First name should be Test.");
            Assert.IsTrue(testUser.LastName.Equals("User"), "Last name should be User.");
            Assert.IsTrue(testUser.Gender.Equals("M"), "Gender should be M.");
            Assert.IsTrue(testUser.Email.Equals("test@test.com"), "Email should be test@test.com.");
            Assert.IsTrue(testUser.PhoneNumber.Equals("+1234567890"), "PhoneNumber should be +1234567890.");
            Assert.IsTrue(testUser.Address.Equals("Urbana"), "Address should be Urbana.");
            Assert.IsTrue(testUser.ZipCode.Equals("51423"), "Zipcode should be 51423.");
        }
    }
}
