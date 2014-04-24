using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using CrimeBusters.WebApp.Models.Util;

namespace CrimeBusters.WebApp.Models.Documents
{
    public class Audio : IDocument
    {
        public HttpPostedFile File { get; set; }
        public string Url { get; set; }

        public bool IsValidFile
        {
            get
            {
                string extension = Path.GetExtension(this.File.FileName);
                if (!String.IsNullOrEmpty(extension))
                {
                    switch (extension.ToLower())
                    {
                        case ".mp3":
                        case ".wav":
                            return true;
                        default:
                            return false;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Saves the Audio to the file system
        /// </summary>
        public void Save(IContentLocator contentLocator)
        {
            if (!this.IsValidFile)
            {
                throw new Exception("Invalid file type. Can only accept mp3 and wav extensions.");
            }

            String filePath = contentLocator.GetPath(this.Url);
            this.File.SaveAs(filePath);
        }
    }
}