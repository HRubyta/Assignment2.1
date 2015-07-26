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
    public partial class foodlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortDirection"] = "ASC";
                Session["SortColumn"] = "FoodID";
                GetFoodlist();
            }
        }

        protected void GetFoodlist()
        {
            try
            {
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    var Foodlist = (from l in db.Foodlists
                                    select new { l.FoodID, l.FoodType, l.FoodBrand, l.Notes });

                    //append the current direction to the Sort Column
                    String Sort = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                    grdFoodlist.DataSource = Foodlist.AsQueryable().OrderBy(Sort).ToList();
                    grdFoodlist.DataBind();

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

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the global sort column to column clicked on by the user
            grdFoodlist.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetFoodlist();
        }

        protected void grdFoodlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //get the ID that will be deletin
            Int32 FoodID = Convert.ToInt32(grdFoodlist.DataKeys[e.RowIndex].Values["FoodID"].ToString());

            //look into db the same ID to be deleted
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Foodlist objL = (from l in db.Foodlists
                                 where l.FoodID == FoodID
                                 select l).FirstOrDefault();

                db.Foodlists.Remove(objL);
                db.SaveChanges();
            }

            //Reload the new foodlist table
            GetFoodlist();
        }

        protected void grdFoodlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the page index and refresh the grid
            grdFoodlist.PageSize = e.NewPageIndex;
            GetFoodlist();
        }

        protected void grdFoodlist_Sorting(object sender, GridViewSortEventArgs e)
        {
            //set the global sort column to column clicked on by the user
            Session["SortColumn"] = e.SortExpression;
            GetFoodlist();

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

        protected void grdFoodlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdFoodlist.Columns.Count -1; i++)
                    {
                        if (grdFoodlist.Columns[i].SortExpression == Session["SortColumn"].ToString())
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