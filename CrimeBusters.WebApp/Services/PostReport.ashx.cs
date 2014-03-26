using CrimeBusters.WebApp.Models.Report;
using CrimeBusters.WebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using CrimeBusters.WebApp.Models.Util;

namespace CrimeBusters.WebApp.Services
{
    /// <summary>
    /// Saves the user report from the Android to our database.
    /// </summary>
    public class PostReport : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            String jsonString = String.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                HttpPostedFile photo = request.Files["photo"];
                String latitude = request.Form["lat"];
                String longitude = request.Form["lng"];
                String description = request.Form["desc"];
                DateTime timeStamp = Convert.ToDateTime(request.Form["timeStamp"]);
                String userName = request.Form["userName"];
                int reportTypeId = Int16.Parse(request.Form["reportTypeId"]);

                User user = new User(userName);
                Report report = new Report((ReportTypeEnum)reportTypeId, description,
                    latitude, longitude, timeStamp, user);

                jsonString = serializer.Serialize(
                    new { result = report.CreateReport(photo, new WebContentLocator()) });
            }
            catch (Exception ex)
            {
                jsonString = serializer.Serialize(new { result = ex.Message });
            }
            finally
            {
                response.Write(jsonString);
                response.ContentType = "application/json";
            }
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