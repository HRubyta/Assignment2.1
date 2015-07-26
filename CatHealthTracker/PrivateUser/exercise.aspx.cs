using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference to the db model
using CatHealthTracker.Models;
using System.Linq.Dynamic;

namespace CatHealthTracker
{
    public partial class exercise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortDirection"] = "ASC";
                Session["SortColumn"] = "ExerciseID";
                GetExercise();
            }
        }

        protected void GetExercise()
        {
            
            try
            {
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    var Exercise = (from r in db.Exerciselogs
                                    select new { r.ExerciseID, r.ExerciseType, r.Duration, r.CaloriesBurn, r.Daylist.Dayname });

                    //append the current direction to the Sort Column
                    String Sort = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                    grdExerciselog.DataSource = Exercise.AsQueryable().OrderBy(Sort).ToList();
                    grdExerciselog.DataBind();
                }
            }
            catch (NullReferenceException e)
            {
                Trace.Write("An error occured during update operation with Message: ", e.Message);
                Trace.Write("Stack Trace: ", e.StackTrace);

            }
            catch (Exception e)
            {
                Trace.Write("Database unavailable with Message: ", e.Message);
                Trace.Write("Stack Trace: ", e.StackTrace);

            }
        }

        protected void grdExerciselog_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 ExerciseID = Convert.ToInt32(grdExerciselog.DataKeys[e.RowIndex].Values["ExerciseID"].ToString());

            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Exerciselog objR = (from r in db.Exerciselogs
                                    where r.ExerciseID == ExerciseID
                                    select r).FirstOrDefault();

                db.Exerciselogs.Remove(objR);
                db.SaveChanges();
            }

            //reload exercise table
            GetExercise();
        }

        protected void grdExerciselog_PageIndexChanged(object sender, EventArgs e)
        {
            //set the page size and refresh grid
            grdExerciselog.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetExercise();
        }

        protected void grdExerciselog_Sorting(object sender, GridViewSortEventArgs e)
        {
            //set the global sort column to column clicked on by the user
            Session["SortColumn"] = e.SortExpression;
            GetExercise();

            //toggle the direction
            if (Session["SortDirection"].ToString() == "ASC")
            {
                Session["SortDirection"] = "DESC";
            }
            else
            {
                Session["SortDirection"] = "ASC";
            }
        }

        protected void grdExerciselog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdExerciselog.Columns.Count - 1; i++)
                    {
                        if(grdExerciselog.Columns[i].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "DESC")
                            {
                                SortImage.ImageUrl = "/images/desc.jpg";
                                SortImage.AlternateText = "Sort Descending";
                            }
                            else
                            {
                                SortImage.ImageUrl = "/images/asc.jpg";
                                SortImage.AlternateText = "Sort Ascending";
                            }

                            e.Row.Cells[i].Controls.Add(SortImage);
                        }
                    }
                }
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the global sort column to column clicked on by the user
            grdExerciselog.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetExercise();
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            // Handle specific exception.
            if (exc is HttpUnhandledException)
            {
                ErrorMsgTextBox.Visible = true;
                ErrorMsgTextBox.Text = "An error occurred on this page. Please verify your " +
                "information to resolve the issue.";
            }
            // Clear the error from the server.
            Server.ClearError();
        }
    }
}