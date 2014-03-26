﻿using System;
using System.Web;
using CrimeBusters.WebApp.Models.Util;

namespace CrimeBusters.WebApp.Models.Documents
{
    public interface IDocument
    {
        HttpPostedFile File { get; set; }
        String Url { get; set; }

        void Save(IContentLocator contentLocator);
    }
}
