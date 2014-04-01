using CrimeBusters.WebApp.Models.DAL;
using CrimeBusters.WebApp.Models.Report;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CrimeBusters.WebApp
{
    public partial class ReportsDump : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataReader reader = null;
            try
            {
                reader = ReportsDAO.GetReports();
                GridView1.DataSource = reader;
                GridView1.DataBind();
            }
            catch (Exception)
            {

            }
            finally 
            {
                reader.Close();
            }
        }
    }
}