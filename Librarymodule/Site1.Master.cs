using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Librarymodule
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["student_id"] != null)
                {
                    if (Session["role"].Equals("user"))
                    {
                        LinkButton1.Visible = false;
                        LinkButton2.Visible = false;

                        LinkButton3.Visible = true;
                        LinkButton4.Text = Session["student_id"].ToString();
                        LinkButton4.Visible = true;
                        LinkButton5.Visible = false;
                        LinkButton6.Visible = false;
                        LinkButton7.Visible = false;

                    }

                } else if (Session["admin_id"] != null) {

                    if (Session["role"].Equals("admin"))
                    {
                        LinkButton1.Visible = false;
                        LinkButton2.Visible = false;

                        LinkButton3.Visible = true;
                        LinkButton4.Text = "Hello Admin";
                        LinkButton6.Visible = false;
                        LinkButton4.Visible = true;
                        LinkButton5.Visible = true;
                        LinkButton7.Visible = true;


                    }
                }
                else
                {

                    LinkButton1.Visible = true;
                    LinkButton2.Visible = true;
                    LinkButton3.Visible = false;
                    LinkButton4.Visible = false;
                    LinkButton5.Visible = false;
                    LinkButton6.Visible = true;
                    LinkButton7.Visible = false;




                }

            }
            catch(Exception ex)
            {

            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("book_lists.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["student_id"] = "";
            LinkButton1.Visible = true;
            LinkButton2.Visible = true;
            LinkButton3.Visible = false;
            LinkButton4.Visible = false;
            LinkButton6.Visible = true;
            LinkButton5.Visible = false;
            LinkButton7.Visible = false;



        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin_login.aspx");

        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Response.Redirect("book_inventory.aspx");

        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("authormanage.aspx");

        }
    }
}