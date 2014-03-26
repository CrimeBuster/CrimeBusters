using System.IO;
using System.Net;
using System.Runtime.Remoting.Channels;
using CrimeBusters.WebApp.Models.DAL;
using CrimeBusters.WebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CrimeBusters.WebApp.Models.Util;

namespace CrimeBusters.WebApp.Models.Login
{
    /// <summary>
    /// Contains the business logic for the Login module
    /// </summary>
    public class Login
    {
        public IUser User { get; set; }

        public Login() { }

        public Login(IUser user)
        {
            this.User = user;
        }

        /// <summary>
        /// Creates a user to the database with IsApproved = false.
        /// </summary>
        /// <param name="contentLocator">Whether TestContentLocator or WebContentLocator</param>
        /// <returns></returns>
        public MembershipCreateStatus CreateUser(IContentLocator contentLocator)
        {
            MembershipCreateStatus createStatus;

            MembershipUser newUser = Membership.CreateUser(
                this.User.UserName, 
                this.User.Password,
                this.User.Email, 
                "dummy question",
                "dummy answer", 
                false, 
                out createStatus);

            if (createStatus == MembershipCreateStatus.Success)
            {
                try
                {
                    CreateUserDetails();
                    SendVerificationEmail(newUser.ProviderUserKey.ToString(), contentLocator);
                }
                catch (Exception)
                {
                    return MembershipCreateStatus.UserRejected;
                }
            }
            return createStatus;
        }

        /// <summary>
        /// Sends an email to the user with verification link.
        /// </summary>
        private void SendVerificationEmail(String userId, IContentLocator contentLocator)
        {
            String filePath = contentLocator.GetPath("~/Content/documents/VerifyAccount.html");

            string emailContent =
                File.ReadAllText(filePath)
                    .Replace("%FirstName%", this.User.FirstName)
                    .Replace("%UserId%", userId);

            Email email = new Email
            {
                FromEmail = "admin@illinoiscrimebusters.com",
                FromName = "Crime Buster Admin",
                ToEmail = this.User.Email,
                Subject = "[Crime Busters] Please Verify Your Account",
                Body = emailContent,
                IsHighImportance = true
            };
            email.SendEmail();
        }

        /// <summary>
        /// Creates user details after creating a user. 
        /// </summary>
        private void CreateUserDetails()
        {
            LoginDAO.CreateUserDetails(this.User.UserName, this.User.FirstName, 
                this.User.LastName, this.User.Email);
        }

        /// <summary>
        /// Validates a user credential.
        /// </summary>
        /// <returns>true if the user credentials are valid</returns>
        public bool ValidateUser()
        {
            return Membership.ValidateUser(this.User.UserName, this.User.Password);
        }
    }
}