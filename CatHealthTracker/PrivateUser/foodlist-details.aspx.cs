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
    public partial class foodlist_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if have id look up the selected record
            if (!String.IsNullOrEmpty(Request.QueryString["FoodID"]))
            {
                GetFood();
            }
        }

        protected void GetFood()
        {
            try
            {
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    Int32 FoodID = Convert.ToInt32(Request.QueryString["FoodID"]);

                    //look up foodlist
                    Foodlist food = (from f in db.Foodlists
                                 where f.FoodID == FoodID
                                 select f).FirstOrDefault();

                    //pre-populate the form fields
                    txtFood.Text = food.FoodType;
                    txtBrand.Text = food.FoodBrand;
                    txtNotes.Text = food.Notes;
                
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                //create new foodlist in memory
                Foodlist food = new Foodlist();

                //check url
                if(!String.IsNullOrEmpty(Request.QueryString["FoodID"]))
                {
                    Int32 FoodID = Convert.ToInt32(Request.QueryString["FoodID"]);

                    food = (from f in db.Foodlists
                            where f.FoodID == FoodID
                            select f).FirstOrDefault();
                }
                //fill new properties of the new foodlist
                food.FoodType = txtFood.Text;
                food.FoodBrand = txtBrand.Text;
                food.Notes = txtNotes.Text;
                //save the new foodlist
                if (String.IsNullOrEmpty(Request.QueryString["FoodID"]))
                {
                    db.Foodlists.Add(food);
                }
                db.Foodlists.Add(food);
                db.SaveChanges();

                //redirect to foodlist page
                Response.Redirect("foodlist.aspx");
            }
            
        }
    }
}