using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatHealthTracker
{
    public partial class main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //calculate the weight from pound to kg
            Double weight = Math.Round(Convert.ToDouble(txtWeight.Text) / 2.2);

            //display the weight in kg
            txtMass.Text = weight.ToString();

            //calculate the calories
            Double cal = (Math.Round(Convert.ToDouble(txtMass.Text)) * 30) + 70;
            Double finalCal = cal * 0.8;

            //display the total calories in a day
            txtCalorie.Text = finalCal.ToString();
        }
    }
}