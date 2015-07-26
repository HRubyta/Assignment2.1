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
    public partial class goals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortDirection"] = "ASC";
                Session["SortColumn"] = "GoalID";
                GetGoals();
            }
        }

        protected void GetGoals()
        {
            try
            {
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    var Goals = (from g in db.Goallogs
                                 select new { g.GoalID, g.GoalName, g.Description, g.GoalTime });

                    //append the current direction to the Sort Column
                    String Sort = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                    grdGoals.DataSource = Goals.AsQueryable().OrderBy(Sort).ToList();
                    grdGoals.DataBind();

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

        protected void grdGoals_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 GoalID = Convert.ToInt32(grdGoals.DataKeys[e.RowIndex].Values["GoalID"].ToString());

            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Goallog objG = (from g in db.Goallogs
                                where g.GoalID == GoalID
                                select g).FirstOrDefault();

                db.Goallogs.Remove(objG);
                db.SaveChanges();
            }

            //reload exercise table
            GetGoals();
        }

        protected void grdGoals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the page size and refresh grid
            grdGoals.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetGoals();
        }

        protected void grdGoals_Sorting(object sender, GridViewSortEventArgs e)
        {
            //set the global sort column to column clicked on by the user
            Session["SortColumn"] = e.SortExpression;
            GetGoals();

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

        protected void grdGoals_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdGoals.Columns.Count - 1; i++)
                    {
                        if (grdGoals.Columns[i].SortExpression == Session["SortColumn"].ToString())
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
            grdGoals.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetGoals();
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