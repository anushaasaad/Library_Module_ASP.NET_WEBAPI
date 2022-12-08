using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using DataAcess;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using Bogus;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;
using System.Drawing;

namespace Librarymodule
{
    public partial class authormanage : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44342/api/author");
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                if ((webResponse.StatusCode == HttpStatusCode.OK) && (webResponse.ContentLength > 0))
                {
                    var reader = new StreamReader(webResponse.GetResponseStream());
                    string s = reader.ReadToEnd();
                    GridView1.DataSource = (new JavaScriptSerializer()).Deserialize<List<Author>>(s);
                    GridView1.DataBind();
                }
                else
                {
                    MessageBox.Show(string.Format("Status code == {0}", webResponse.StatusCode));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




            //ADD BUTTON
            protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkAuthorexist())
            {
                Response.Write("<script>alert('Author with ID already exists');</script>");

            }
            else
            {
                addauthor();
                
            }

        }

       

        //UPDATE BUTTON
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkAuthorexist())
            {
                updateAuthor();
            }
            else
            {
                Response.Write("<script>alert('Author with ID already doesnot exist');</script>");

            }
        }
        //DELETE BUTTON
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkAuthorexist())
            {
                deleteanyauthor();
            }
            else
            {
                Response.Write("<script>alert('Author with ID already exists');</script>");

            }

        }
        //GO BUTTON
        protected void Button1_Click(object sender, EventArgs e)
        {
            getAuthor();

        }
        void getAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.Author where author_id='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('wrong ID');</script>");

                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>unsuccesful</script>");

            }
        }
        /*void deleteAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Author WHERE author_id = '" + TextBox1.Text.Trim() + "';", con);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script> succesful</script>");
                clearForm();
                GridView1.DataBind();


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('wrong text');</script>");

            }
        }
        void updatenewAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE dbo.Author SET author_name=@a1 WHERE author_id='" + TextBox1.Text.Trim() + "';", con);
                cmd.Parameters.AddWithValue("@a1", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script> succesful</script>");
                clearForm();
                GridView1.DataBind();


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('wrong text');</script>");

            }
        }
        void addNewauthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Author(author_id,author_name) VALUES (@a1,@a2)", con);
                cmd.Parameters.AddWithValue("@a1", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@a2", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                clearForm();
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('wrong text');</script>");

            }
        }*/
        void deleteanyauthor()
        {
            String authorid = TextBox1.Text.Trim();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"https://localhost:44342/api/author/{authorid}");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "DELETE";



                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                }


                MessageBox.Show(authorid + " deleted successfully.", "Employee Successfully added", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //Program.con.Close();
                GridView1.DataBind();
                clearForm();

            }
            catch (Exception ex)
            {
                MessageBox.Show("errorrr", ex.Message);
            }
        }
        void updateAuthor()
        {
            Author a = new Author();

            a.author_id = TextBox1.Text.Trim();
            a.author_name = TextBox2.Text.Trim();


            if (a.author_name.Length < 1 || a.author_id.Length < 1)
            {
                MessageBox.Show("All information is required. Please enter all information.", "All information is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create($"https://localhost:44342/api/author/{a.author_id}");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "PUT";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        /*  string json = "{\"user\":\"test\"," +
                                        "\"password\":\"bla\"}";*/
                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(a);

                        streamWriter.Write(json);
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                    }
                    MessageBox.Show("Updated Employee info.", "Employee Successfully updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show("errorrr", ex.Message);
                }

                clearForm();
            }
        }
       void addauthor()
        {
            Author a = new Author();

            a.author_id = TextBox1.Text.Trim();
            a.author_name = TextBox2.Text.Trim();
        

            if (a.author_name.Length < 1 || a.author_id.Length < 1 )
            {
                MessageBox.Show("All information is required. Please enter all information.", "All information is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44342/api/author");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(a);

                        streamWriter.Write(json);
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                    }
                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show("errorrr", ex.Message);
                }
                clearForm();


            }
        }
        private void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }

        //Author exists
        bool checkAuthorexist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.Author where author_id='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script> succesful</script>");
                return false;
            }
        }
       
        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

    }
}
