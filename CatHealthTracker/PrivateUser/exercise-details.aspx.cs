using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference to the db model
using CatHealthTracker.Models;

namespace CatHealthTracker
{
    public partial class exercise_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //if loading for the first time, file the daylist dropdown
                GetDaylist();

                //get the exercise editing
                if (!String.IsNullOrEmpty(Request.QueryString["ExerciseID"]))
                {
                    GetExercise();
                }
                
            }
        }

        protected void GetDaylist()
        {
            try
            {
                //connect to db via EF
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {

                    //get day name in daylist
                    var Daylist = from d in db.Daylists
                              orderby d.Dayname
                              select d;

                    //bind the dropdown list
                    ddlDays.DataSource = Daylist.ToList();
                    ddlDays.DataBind();

                    //add a default option to the dropdown after we fill it
                    ListItem DefaultTime = new ListItem("-Select-", "0");
                    ddlDays.Items.Insert(0, DefaultTime);
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Cannot load data. " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error passing data. " + e.InnerException.Message);
            }
            
        }

        protected void GetExercise()
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Int32 ExerciseID = Convert.ToInt32(Request.QueryString["ExerciseID"]);

                //look up foodlist
                Exerciselog exercise = (from r in db.Exerciselogs
                                        where r.ExerciseID == ExerciseID
                                        select r).FirstOrDefault();

                //pre-populate the form fields
                txtExercise.Text = exercise.ExerciseType;
                txtDuration.Text = exercise.Duration.ToString();
                txtCaloriesburn.Text = exercise.CaloriesBurn.ToString();
                ddlDays.SelectedValue = exercise.DayID.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect to SQL Server
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                //create a new exercise and fill properties
                Exerciselog objE = new Exerciselog();

                objE.ExerciseType = txtExercise.Text;
                objE.Duration = Convert.ToInt32(txtDuration.Text);
                objE.CaloriesBurn = Convert.ToInt32(txtCaloriesburn.Text);
                objE.DayID = Convert.ToInt32(ddlDays.SelectedValue);

                //save
                db.Exerciselogs.Add(objE);
                db.SaveChanges();

                //redirect
                Response.Redirect("exercise.aspx");

            }
        }
    }
}