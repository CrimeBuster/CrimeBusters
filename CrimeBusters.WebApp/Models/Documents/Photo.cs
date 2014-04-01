using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Xml.Linq;
using CrimeBusters.WebApp.Models.Util;

namespace CrimeBusters.WebApp.Models.Documents
{
    public class Photo : IDocument
    {
        /// <summary>
        /// The actual file posted.
        /// </summary>
        public HttpPostedFile File { get; set; }
        /// <summary>
        /// Relative URL that we will save to the database.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Determines whether the HttpPostedFile uploaded is a valid file.
        /// </summary>
        public bool IsValidFile
        {
            get
            {
                string extension = Path.GetExtension(this.File.FileName);
                if (!String.IsNullOrEmpty(extension))
                {
                    switch (extension.ToLower())
                    {
                        case ".gif":
                        case ".png":
                        case ".jpg":
                        case ".jpeg":
                            return true;
                        default:
                            return false;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Saves the photo to the file system.
        /// </summary>
        public void Save(IContentLocator contentLocator)
        {
            if (!this.IsValidFile)
            {
                throw new Exception("Invalid file type.");
            }

            String filePath = contentLocator.GetPath(this.Url);
            this.File.SaveAs(filePath);
        }
    }
}