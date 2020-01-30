using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimpleWebApplication_1
{
    public partial class Librarian : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null && Session["UserName_1"] == null)
            {
                Response.Redirect("~/LoginScreen.aspx");
            }
            if ((Session["UserName"].ToString()).Equals((Session["UserName_1"]).ToString()))
            {
                Response.Redirect("~/UserScreen.aspx");
            }
            if (Session["UserName"].Equals("admin"))
            {
                Response.Redirect("~/Admin.aspx");
            }
            bind();
            Session["library_flow"] = "library";



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            var cnt = Count();
            if (check() == 0)
            {
                try
                {
                    string cmd = "Insert Into dbo.BookTable values(@Id,@Name,@Author,@Checked,@BookCount)";
                    SqlConnection conn = new SqlConnection(connection);
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@Name", TextBox3.Text);
                    command.Parameters.AddWithValue("@Author", TextBox1.Text);
                    command.Parameters.AddWithValue("@Id", cnt + 1);
                    command.Parameters.AddWithValue("@Checked", 0);
                    command.Parameters.AddWithValue("@BookCount", Convert.ToInt32(TextBox4.Text));
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                bind();
                TextBox1.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";

            }
            else
            {
                var count = check();
                UserScreen obj = new UserScreen();
                var c = Convert.ToInt32(TextBox4.Text) + count;
                obj.Increment(string.Format("Update dbo.BookTable Set BookCount={0} Where Name='{1}'",c ,TextBox3.Text));
                bind();
            }
            

        }
        public int Count()
        {
            var count = 0;
            var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connection);
            var cmd = "Select * From dbo.BookTable";
            SqlCommand command = new SqlCommand(cmd, conn);
            conn.Open();
            SqlDataReader read = command.ExecuteReader();
            while (read.Read())
                count++;
            conn.Close();
            return count;
        }
        public void bind()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("select * from dbo.BookTable", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/LoginScreen.aspx");
        }
        public int check()

        {
            var conn = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand check_User_Name = new SqlCommand(string.Format("SELECT BookCount FROM dbo.BookTable WHERE Name='{0}'",TextBox3.Text), connection);
            connection.Open();
            //int UserExist = (int)check_User_Name.ExecuteScalar();
            SqlDataReader read = check_User_Name.ExecuteReader();
            int UserExist = 0;
            while (read.Read())
            {
                UserExist = Convert.ToInt32(read["BookCount"]);
            }
            connection.Close();
            return UserExist;
        }
        protected void DeleteEvent(object sender, EventArgs e)
        {
            UserScreen obj = new UserScreen();
            try
            {
                obj.delete(string.Format("Delete From dbo.BookTable Where Name='{0}'", GridView1.SelectedRow.Cells[2].Text));

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            bind();
        }


    }
}