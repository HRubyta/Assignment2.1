using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference 
using Microsoft.Owin.Security;



namespace CatHealthTracker
{
    public partial class site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //set up nav bar when log in and log out
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                plhPrivate.Visible = true;
                plhPublic.Visible = false;
            }
            else
            {
                plhPrivate.Visible = false;
                plhPublic.Visible = true;
            }
        }
    }
}