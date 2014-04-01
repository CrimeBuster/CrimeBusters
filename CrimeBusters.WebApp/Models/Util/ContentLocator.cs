using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace CrimeBusters.WebApp.Models.Util
{
    public class WebContentLocator : IContentLocator
    {
        public string GetPath(string relativePath)
        {
            return HttpContext.Current.Server.MapPath(relativePath);
        }
    }

    public class TestContentLocator : IContentLocator
    {
        string _contentRoot;
        public TestContentLocator()
        {
            _contentRoot = ConfigurationManager.AppSettings["ContentRoot"];
        }

        public string GetPath(string relativePath)
        {
            return Path.Combine(_contentRoot, relativePath.Replace("~/", String.Empty));
        }
    }

    public interface IContentLocator
    {
        string GetPath(string relativePath);
    }
}