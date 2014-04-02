﻿using System;
using System.Web;
using System.Web.Script.Serialization;
using CrimeBusters.WebApp.Models.Users;

namespace CrimeBusters.WebApp.Services
{
    /// <summary>
    /// Updates the user profile.
    /// </summary>
    public class UpdateProfile : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            String jsonString;
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                User user = new User
                {
                    FirstName = request.QueryString["firstName"],
                    LastName = request.QueryString["lastName"],
                    Gender = request.QueryString["gender"],
                    PhoneNumber = request.QueryString["phoneNumber"],
                    Address = request.QueryString["address"],
                    ZipCode = request.QueryString["zipCode"],
                    UserName = request.QueryString["userName"]
                };
                user.UpdateProfile();

                jsonString = serializer.Serialize(new { result = "success" });
            }
            catch (Exception ex)
            {
                jsonString = serializer.Serialize(new { result = ex.Message });
            }
           
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