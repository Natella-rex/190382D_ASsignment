using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _190382D_ASsignment
{
    public partial class XSSDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_xssdisplay.Text = Request.QueryString["Comment"];
        }
    }
}