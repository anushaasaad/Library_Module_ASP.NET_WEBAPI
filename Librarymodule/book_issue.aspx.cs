using DataAcess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Librarymodule
{
    public partial class book_issue : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44342/api/Bookissue");
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                if ((webResponse.StatusCode == HttpStatusCode.OK) && (webResponse.ContentLength > 0))
                {
                    var reader = new StreamReader(webResponse.GetResponseStream());
                    string s = reader.ReadToEnd();
                    GridView1.DataSource = (new JavaScriptSerializer()).Deserialize<List<Book_issue>>(s);
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
        //Go
        protected void Button1_Click(object sender, EventArgs e)
        {
            getBook();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIfBookExist())
            {

                if (checkIfIssueEntryExist())
                {
                    Response.Write("<script>alert('This Member already has this book');</script>");
                }
                else
                {
                    issueBook();
                }

            }
            else
            {
                Response.Write("<script>alert('Wrong Book ID or student ID');</script>");
            }
        }

        protected void Button4_Click1(object sender, EventArgs e)
        {
            if (checkIfBookExist())
            {

                if (checkIfIssueEntryExist())
                {
                    ReturnBook();
                }
                else
                {
                    Response.Write("<script>alert('This Entry does not exist');</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('Wrong Book ID or student ID');</script>");
            }
        }
        void issueanyBook()
        {
            Book_issue a = new Book_issue();

            a.book_id = TextBox1.Text.Trim();
            a.student_id = TextBox2.Text.Trim();
            a.date_issue = Convert.ToDateTime(TextBox5.Text.Trim());
            a.date_due = Convert.ToDateTime(TextBox6.Text.Trim());
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44342/api/Bookissue");
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
        bool checkbook()
        {
            Book_inventory b = new Book_inventory();
            b.book_id = TextBox1.Text.Trim();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"https://localhost:44342/api/book/{b.book_id}");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
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
                    if (result.Length > 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }

            catch
            {
                return false;
            }
        }


        bool checkIfStudentExist()
        {
            student s = new student();
            s.student_id = TextBox2.Text.Trim();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"https://localhost:44342/api/student/{s.student_id}");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    /*  string json = "{\"user\":\"test\"," +
                                    "\"password\":\"bla\"}";*/
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(s);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (result.Length > 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }

            }
            catch
            {
                return false;
            }


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
                    TextBox4.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('wrong ID');</script>");

                }
                SqlCommand cmd1 = new SqlCommand("SELECT * from dbo.student where student_id='" + TextBox2.Text.Trim() + "';", con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                if (dt1.Rows.Count >= 1)
                {
                    TextBox3.Text = dt1.Rows[0][1].ToString();
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
        bool checkIfIssueEntryExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from dbo.Book_issue WHERE student_id='" + TextBox2.Text.Trim() + "' AND book_id='" + TextBox1.Text.Trim() + "'", con);
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
                return false;
            }

        }

        void issueBook()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DateTime issue = Convert.ToDateTime(TextBox5.Text.Trim());
                DateTime due = Convert.ToDateTime(TextBox6.Text.Trim());
                DateTime today = DateTime.Today;
                if (issue ==  today && due > today)
                {

                    SqlCommand cmd = new SqlCommand("INSERT INTO Book_issue(student_id,book_id,date_issue,date_due) values(@student_id,@book_id,@date_issue,@date_due)", con);

                    cmd.Parameters.AddWithValue("@student_id", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@date_issue", TextBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@date_due", TextBox6.Text.Trim());

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("update dbo.Book_inventory set current_stock = current_stock-1 WHERE book_id='" + TextBox1.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();

                    con.Close();
                    Response.Write("<script>alert('Book Issued Successfully');</script>");

                    GridView1.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('errrrrrrr');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void returnbook()
        {
            String bookid = TextBox1.Text.Trim();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"https://localhost:44342/api/Bookissue/{bookid}");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "DELETE";



                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.ContentLength > 0)
                {
                    currentstock();
                }
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                }


                MessageBox.Show(bookid + " returned successfully.", "Employee Successfully added", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //Program.con.Close();
                GridView1.DataBind();
                clearForm();

            }
            catch (Exception ex)
            {
                MessageBox.Show("errorrr", ex.Message);
            }
        }
        void currentstock()
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("update dbo.Book_inventory set current_stock = current_stock+1 WHERE book_id='" + TextBox1.Text.Trim() + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Write("<script>alert('Book Returned Successfully');</script>");
            GridView1.DataBind();

            con.Close();
        }
        bool checkIfBookExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from dbo.Book_inventory WHERE book_id='" + TextBox1.Text.Trim() + "' AND current_stock >0", con);
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
                return false;
            }

        }
        void ReturnBook()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("Delete from dbo.Book_issue WHERE book_id='" + TextBox1.Text.Trim() + "' AND student_id='" + TextBox2.Text.Trim() + "'", con);
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {

                    cmd = new SqlCommand("update dbo.Book_inventory set current_stock = current_stock+1 WHERE book_id='" + TextBox1.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Write("<script>alert('Book Returned Successfully');</script>");
                    GridView1.DataBind();

                    con.Close();

                }
                else
                {
                    Response.Write("<script>alert('Error - Invalid details');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";


        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}