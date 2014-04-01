using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrimeBusters.WebApp.Models.Users;

namespace CrimeBusters.WebApp.Tests
{
    [TestClass]
    public class UserTest
    {
        private User _testUser;

        [TestInitialize]
        public void Initialize()
        {
            if (_testUser == null)
            {
                _testUser = User.GetUser("test.user");
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (!_testUser.FirstName.Equals("Test"))
            {
                _testUser.FirstName = "Test";
                _testUser.LastName = "User";
                _testUser.Gender = "M";
                _testUser.PhoneNumber = "+1234567890";
                _testUser.Address = "Urbana";
                _testUser.ZipCode = "51423";
                _testUser.UpdateProfile();
            }
        }

        [TestMethod]
        public void TestGetUserIsNotNull()
        {
            Assert.IsNotNull(_testUser);
        }

        [TestMethod]
        public void TestGetUserDetails()
        {
            Assert.IsTrue(_testUser.UserName.Equals("test.user"));
            Assert.IsTrue(_testUser.FirstName.Equals("Test"), "First name should be Test.");
            Assert.IsTrue(_testUser.LastName.Equals("User"), "Last name should be User.");
            Assert.IsTrue(_testUser.Gender.Equals("M"), "Gender should be M.");
            Assert.IsTrue(_testUser.Email.Equals("test@test.com"), "Email should be test@test.com.");
            Assert.IsTrue(_testUser.PhoneNumber.Equals("+1234567890"), "PhoneNumber should be +1234567890.");
            Assert.IsTrue(_testUser.Address.Equals("Urbana"), "Address should be Urbana.");
            Assert.IsTrue(_testUser.ZipCode.Equals("51423"), "Zipcode should be 51423.");
        }

        [TestMethod]
        public void TestUpdateProfile()
        {
            _testUser.FirstName = "Test Updated";
            _testUser.LastName = "User Updated";
            _testUser.Gender = "F";
            _testUser.PhoneNumber = "+12345678900";
            _testUser.Address = "Urbana Updated";
            _testUser.ZipCode = "51423 Updated";
            _testUser.UpdateProfile();

            User updatedUser = User.GetUser("test.user");
            Assert.IsTrue(updatedUser.FirstName.Equals("Test Updated"), "First name should be Test Updated.");
            Assert.IsTrue(updatedUser.LastName.Equals("User Updated"), "Last name should be User Updated.");
            Assert.IsTrue(updatedUser.Gender.Equals("F"), "Gender should be F.");
            Assert.IsTrue(updatedUser.PhoneNumber.Equals("+12345678900"), "PhoneNumber should be +12345678900.");
            Assert.IsTrue(updatedUser.Address.Equals("Urbana Updated"), "Address should be Urbana Updated.");
            Assert.IsTrue(updatedUser.ZipCode.Equals("51423 Updated"), "Zipcode should be 51423 Updated.");
        }
    }
}
