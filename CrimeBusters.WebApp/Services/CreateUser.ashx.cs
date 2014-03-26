using CrimeBusters.WebApp.Models.Login;
using CrimeBusters.WebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using CrimeBusters.WebApp.Models.Util;

namespace CrimeBusters.WebApp.Services
{
    /// <summary>
    /// Summary description for CreateUser
    /// </summary>
    public class CreateUser : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            String jsonString = String.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Login login = new Login(new User { 
                UserName = request.QueryString["userName"],
                Password = request.QueryString["password"],
                FirstName = request.QueryString["firstName"],
                LastName = request.QueryString["lastName"],
                Email = request.QueryString["email"]
            });
            login.ContentLocator = new WebContentLocator();

            MembershipCreateStatus createStatus = login.CreateUser();
            jsonString = serializer.Serialize(new { result = createStatus.ToString() });

            response.Write(jsonString);
            response.ContentType = "application/json";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}