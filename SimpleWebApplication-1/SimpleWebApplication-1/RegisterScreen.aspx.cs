using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimpleWebApplication_1
{
    public partial class RegisterScreen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var email = TextBox3.Text;
            var password = TextBox2.Text;
            var user = TextBox1.Text;
            Console.WriteLine(email + " " + password + " " + user);
            try
            {
                var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
                var obj = new LoginScreen();
                var cnt=obj.Count();
                string cmd = "Insert Into dbo.UserTable values(@Id,@Name,@Password,@email)";
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand command = new SqlCommand(cmd, conn);
                command.Parameters.AddWithValue("@Name", user);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@Id", cnt+1);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                Label3.Text = "Error";
            }
            Response.Redirect("~/LoginScreen.aspx");
        }
    }
}