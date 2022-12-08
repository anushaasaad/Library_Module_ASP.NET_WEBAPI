using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Librarymodule
{
    public partial class admin_login : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT admin_id FROM dbo.admin WHERE admin_id = '" + TextBox1.Text.Trim() + "' AND password = '" + TextBox1.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //Response.Write("<script>alert('" + dr.GetValue(1).ToString() + "');</script>");
                        Session["admin_id"] = dr.GetValue(0);
                        Session["role"] = "admin";
                        Response.Redirect("homepage.aspx");
                        // Response.Redirect("homepage.aspx");

                    }
                }
                else
                {
                    Response.Write("<script>alert('invalid credentials');</script>");

                }
            }
            catch (Exception ex)
            {

            }
            //Response.Write("<script>alert('Button works');</script>");
        }

    }
 }
