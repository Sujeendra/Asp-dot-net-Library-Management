using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimpleWebApplication_1
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                if (Session["UserName"] == null && Session["UserName_1"]==null)
                {
                    Response.Redirect("~/LoginScreen.aspx");
                }
                if ((Session["UserName"].ToString()).Equals((Session["UserName_1"]).ToString()))
                {
                    Response.Redirect("~/UserScreen.aspx");
                }
                if (Session["UserName"].Equals("library"))
                {
                    Response.Redirect("~/Librarian.aspx");
                }
                Session["flow"] = "admin";

            }
           

        }
        protected void EventSelect(object sender, EventArgs e)
        {
           
            
                Response.Write("Successfully Authorised user: " + GridView1.SelectedRow.Cells[2].Text + " " + GridView1.SelectedRow.Cells[3].Text);
                var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
                string cmd = "Insert Into dbo.AdminTable values(@Id,@Name,@Password)";
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand command = new SqlCommand(cmd, conn);
                var cnt = CountCalc();
                command.Parameters.AddWithValue("@Name", GridView1.SelectedRow.Cells[2].Text);
                command.Parameters.AddWithValue("@Password", GridView1.SelectedRow.Cells[3].Text);
                command.Parameters.AddWithValue("@Id", cnt + 1);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            //}
            //else
            //{
            //    Response.Write("User is registered already");
            //}
        }
        //public int check()

        //{
        //    var conn = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
        //    SqlConnection connection = new SqlConnection(conn);
        //    SqlCommand check_User_Name = new SqlCommand(string.Format("SELECT Id FROM dbo.AdminTable WHERE Name='{0}'", Session["UserName_1"]), connection);
        //    connection.Open();
        //    //int UserExist = (int)check_User_Name.ExecuteScalar();
        //    SqlDataReader read = check_User_Name.ExecuteReader();
        //    int UserExist = 0;
        //    while (read.Read())
        //    {
        //        UserExist = Convert.ToInt32(read["Id"]);
        //    }
        //    connection.Close();
        //    return UserExist;
        //}
        public int CountCalc()
        {
            var count = 0;
            var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connection);
            var cmd = "Select * From dbo.AdminTable";
            SqlCommand command = new SqlCommand(cmd, conn);
            conn.Open();
            SqlDataReader read = command.ExecuteReader();
            while (read.Read())
                count++;
            conn.Close();
            return count;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Clear();
           // Session.Abandon();
            Response.Redirect("~/LoginScreen.aspx");
        }
    }
}