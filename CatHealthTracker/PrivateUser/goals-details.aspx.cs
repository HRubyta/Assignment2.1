using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference to the db models
using CatHealthTracker.Models;

namespace CatHealthTracker
{
    public partial class goals_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if have id look up the selected record
            if (!String.IsNullOrEmpty(Request.QueryString["FoodID"]))
            {
                GetGoals();
            }
        }

        protected void GetGoals()
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Int32 GoalID = Convert.ToInt32(Request.QueryString["GoalID"]);

                //look up foodlist
                Goallog goal = (from g in db.Goallogs
                                 where g.GoalID == GoalID
                                 select g).FirstOrDefault();

                //pre-populate the form fields
                txtGoals.Text = goal.GoalName;
                txtDescription.Text = goal.Description;
                txtGoaltime.Text = goal.GoalTime.ToString();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                //create new goals log in memory
                Goallog goal = new Goallog();

                //check url
                if (!String.IsNullOrEmpty(Request.QueryString["GoalID"]))
                {
                    Int32 GoalID = Convert.ToInt32(Request.QueryString["GoalID"]);

                    goal = (from g in db.Goallogs
                            where g.GoalID == GoalID
                            select g).FirstOrDefault();
                }
                //fill new properties of the new goals log
                goal.GoalName = txtGoals.Text;
                goal.Description = txtDescription.Text;
                goal.GoalTime = Convert.ToInt32(txtGoaltime.Text);

                //save the new goals log
                if (String.IsNullOrEmpty(Request.QueryString["GoalID"]))
                {
                    db.Goallogs.Add(goal);
                }
                db.Goallogs.Add(goal);
                db.SaveChanges();

                //redirect to foodlist page
                Response.Redirect("goals.aspx");
            }
            
        }
    }
}