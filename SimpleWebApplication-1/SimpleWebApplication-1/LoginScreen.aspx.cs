using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimpleWebApplication_1
{
    public partial class LoginScreen : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["PageLoad"] = true;
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            var username = Login1.UserName.ToString();
            var password = Login1.Password.ToString();
            Session["UserName"] = Login1.UserName.ToString();
            Session["flow"] = "Admin";
            Session["library_flow"] = "Library";
            if (username.Equals("admin") && password.Equals("admin"))
            {
                Session["UserName_1"] = "Admin";
               
                Response.Redirect("~/Admin.aspx");
            }
            if(username.Equals("library") && password.Equals("library"))
            {
                Session["UserName_1"] = "Library";
                
                Response.Redirect("~/Librarian.aspx");
            }
            SqlConnection conn = new SqlConnection(connection);
            var cmd = "Select * From dbo.AdminTable";
            SqlCommand command = new SqlCommand(cmd,conn);
            conn.Open();
            SqlDataReader read = command.ExecuteReader();
           
            while (read.Read())
            {
                if (username.Equals(read["Name"].ToString().Trim()) && password.ToString().Equals(read["Password"].ToString().Trim()))
                   
                {
                    Session["Id"] = read["Id"].ToString().Trim();///added
                    Session["UserName_1"]= Login1.UserName.ToString(); 
                    
                    Response.Redirect("~/UserScreen.aspx");
                    
                }
            }
            Response.Write("Please register");

            conn.Close();

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RegisterScreen.aspx");
        }
        public int Count()
        {
            var count = 0;
            var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connection);
            var cmd = "Select * From dbo.UserTable";
            SqlCommand command = new SqlCommand(cmd, conn);
            conn.Open();
            SqlDataReader read = command.ExecuteReader();
            while (read.Read())
                count++;
            conn.Close();
            return count;
        }
    }
}