﻿using CrimeBusters.WebApp.Models.Report;
using CrimeBusters.WebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

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
                int reportTypeId = Int16.Parse(request.QueryString["reportTypeId"]);
                String message = request.QueryString["message"];
                String latitude = request.QueryString["latitude"];
                String longitude = request.QueryString["longitude"];
                String resourceUrl = request.QueryString["resourceUrl"];
                String userName = request.QueryString["userName"];

                User user = new User(userName);
                Report report = new Report((ReportTypeEnum)reportTypeId, message, 
                    latitude, longitude, resourceUrl, user);

                jsonString = serializer.Serialize(new { result = report.CreateReport() });
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