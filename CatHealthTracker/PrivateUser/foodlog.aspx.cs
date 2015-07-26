using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference our db model
using CatHealthTracker.Models;
using System.Linq.Dynamic;

namespace CatHealthTracker
{
    public partial class foodlog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session["SortDirection"] = "ASC";
                Session["SortColumn"] = "LogID";
                GetFoodlog();
            }
        }

        protected void GetFoodlog()
        {
            try
            {
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    var Foodlog = (from f in db.Foodlogs
                                   select new { f.LogID, f.FoodName, f.Daylist.Dayname, f.Calories, f.Foodlist.FoodType });

                    //append the current direction to the Sort Column
                    String Sort = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                    grdFoodlog.DataSource = Foodlog.AsQueryable().OrderBy(Sort).ToList();
                    grdFoodlog.DataBind();
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

        protected void grdFoodlog_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //get the ID that will be deletin
            Int32 LogID = Convert.ToInt32(grdFoodlog.DataKeys[e.RowIndex].Values["LogID"].ToString());

            //look into db the same ID to be deleted
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Foodlog objF = (from f in db.Foodlogs
                                 where f.LogID == LogID
                                 select f).FirstOrDefault();

                db.Foodlogs.Remove(objF);
                db.SaveChanges();
            }

            //Reload the new foodlist table
            GetFoodlog();
        }

        protected void grdFoodlog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the page index and refresh the grid
            grdFoodlog.PageSize = e.NewPageIndex;
            GetFoodlog();
        }

        protected void grdFoodlog_Sorting(object sender, GridViewSortEventArgs e)
        {
            //set the global sort column to column clicked on by the user
            Session["SortColumn"] = e.SortExpression;
            GetFoodlog();

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

        protected void grdFoodlog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdFoodlog.Columns.Count - 1; i++)
                    {
                        if (grdFoodlog.Columns[i].SortExpression == Session["SortColumn"].ToString())
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
            grdFoodlog.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetFoodlog();
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