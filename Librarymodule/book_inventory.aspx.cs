using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using DataAcess;

namespace Librarymodule
{
    public partial class book_inventory : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44342/api/book");
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                if ((webResponse.StatusCode == HttpStatusCode.OK) && (webResponse.ContentLength > 0))
                {
                    var reader = new StreamReader(webResponse.GetResponseStream());
                    string s = reader.ReadToEnd();
                    GridView1.DataSource = (new JavaScriptSerializer()).Deserialize<List<Book_inventory>>(s);
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
        //ADD
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkbookexist())
            {
                Response.Write("<script>alert('book with ID already exists');</script>");

            }
            else
            {
                addbook();
            }
        }

        //update
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkbookexist())
            {
                updateanybook();

            }
            else
            {
                Response.Write("<script>alert('errrr');</script>");
            }
        }
        //delete
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkbookexist())
            {
                deletebook();
            }
            else
            {
                Response.Write("<script>alert('Author with ID already exists');</script>");

            }

        }
        //Go
        protected void Button1_Click(object sender, EventArgs e)
        {
            getBook();
        }
        void getBook()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.Book_inventory where book_id='" + TextBox1.Text.Trim() + "';", con);
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

        bool checkbookexist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.Book_inventory where book_id='" + TextBox1.Text.Trim() + "';", con);
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
                Response.Write("<script> unsuccesful</script>");
                return false;
            }
        }
        void addanyBook()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string filepath = "~/book_inventory/books1.png";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                filepath = "~/book_inventory/" + filename;

                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Book_inventory(book_id,book_name,genre,edition,actual_stock,language,book_cost,pages,description,current_stock,img_link,author_name) VALUES(@book_id,@book_name,@genre,@edition,@actual_stock,@language,@book_cost,@pages,@description,@current_stock,@img_link,@author_name)", con);
                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@author_name", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@description", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@pages", TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@book_cost", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@current_stock", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@img_link", filepath);


                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script> succesful</script>");
                clearForm();
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>'wrong text'</script>");

            }
        } 

        void deletebook()
        {
            String bookid = TextBox1.Text.Trim();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"https://localhost:44342/api/book/{bookid}");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "DELETE";



                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                }


                MessageBox.Show(bookid + " deleted successfully.", "Employee Successfully added", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //Program.con.Close();
                GridView1.DataBind();
                clearForm();

            }
            catch (Exception ex)
            {
                MessageBox.Show("errorrr", ex.Message);
            }
        }
        void updatebook()
        {
            Book_inventory b = new Book_inventory();
           
            b.book_id = TextBox1.Text.Trim();
            b.book_name = TextBox2.Text.Trim();
            b.description = TextBox6.Text.Trim();
            b.book_cost = TextBox10.Text.Trim();


            if (b.book_name.Length < 1 || b.book_id.Length < 1)
            {
                MessageBox.Show("All information is required. Please enter all information.", "All information is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create($"https://localhost:44342/api/book/{b.book_id}");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "PUT";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        /*  string json = "{\"user\":\"test\"," +
                                        "\"password\":\"bla\"}";*/
                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(b);

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

        void addbook()
        {
            Book_inventory b = new Book_inventory();
            b.book_id = TextBox1.Text.Trim();
            b.book_name = TextBox2.Text.Trim();
            b.description = TextBox6.Text.Trim();
            b.book_cost = TextBox10.Text.Trim();
            b.genre = DropDownList3.SelectedItem.Value;
            b.edition = TextBox9.Text.Trim();
            b.actual_stock = TextBox3.Text.Trim();
            b.language = DropDownList1.SelectedItem.Value;
            b.author_name = TextBox7.Text.Trim();
            b.pages = TextBox11.Text.Trim();
            b.current_stock = TextBox4.Text.Trim();


            if (b.book_name.Length < 1 || b.book_id.Length < 1)
            {
                MessageBox.Show("All information is required. Please enter all information.", "All information is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44342/api/book");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(b);

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



            }
            clearForm();
        
    }
        private void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox9.Text = "";
            TextBox6.Text = "";
            TextBox11.Text = "";
            TextBox10.Text = "";
            TextBox4.Text = "";
            

        }

       
        protected void TextBox7_TextChanged1(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void updateanybook()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE dbo.Book_inventory SET book_name=@a1 WHERE book_id='" + TextBox1.Text.Trim() + "';", con);
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

        }
}