using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CrimeBusters.WebApp
{
    public partial class Util : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
            // Creates Role
            //Roles.CreateRole("User");

            // Add user to a certain role.
            //Roles.AddUserToRole("police", "Police");
        }
    }
}