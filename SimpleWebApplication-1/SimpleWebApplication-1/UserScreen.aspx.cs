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
    public partial class UserScreen : System.Web.UI.Page
    {
       public int Checked;
        public string data;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Response.Write("Hi " + Session["Username_1"]);
            if (!Page.IsPostBack)
            {
                
                if (Session["UserName"] == null && Session["UserName_1"] == null)
                {
                    Response.Redirect("~/LoginScreen.aspx");
                }
                if (Session["flow"].Equals("admin"))
                {
                    Response.Redirect("~/Admin.aspx");
                }
                if (Session["library_flow"].Equals("library"))
                {
                    Response.Redirect("~/library.aspx");
                }
                refreshData("select * from dbo.BookTable order by Id");
                update(string.Format("Select Pp.Id,Bt.UniqueId,Bt.Name,Bt.Author,Pp.[Date of Issue] From PenaltyPage Pp Right outer Join UserBookTable Bt On Pp.Id = Bt.UniqueId  Where Bt.Id={0} order by Bt.UniqueId", Session["Id"]));

            }
            //if (Page.IsPostBack)
            //{
            //    if (!Convert.ToBoolean(Session["PageLoad"]))
            //    {
            //        Response.Redirect("~/LoginScreen.aspx");
            //    }
            //    else
            //    {
            //        Session["PageLoad"] = false;
            //    }
            //}

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue.Equals("Id"))
            {
                string id = TextBox1.Text;
                refreshData(string.Format("select * from dbo.BookTable where Id={0} order by Id", id));
            }
            else if (RadioButtonList1.SelectedValue.Equals("Name"))
            {
                string name = TextBox1.Text;
                refreshData(string.Format("select * from dbo.BookTable where Name Like '{0}%' order by Id", name));
            }
            else if(RadioButtonList1.SelectedValue.Equals("Author"))
            {
                string author = TextBox1.Text;
                refreshData(string.Format("select * from dbo.BookTable where Author Like '{0}%' order by Id", author));
                
            }
            else
            {
                refreshData("select * from dbo.BookTable order by Id");
            }
        }
        public void refreshData(string command)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True");
                SqlCommand cmd = new SqlCommand(command, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            catch(Exception e)
            {
                Response.Write(e.Message);
            }
            
         
        }
       protected void IssueEvent(object sender, EventArgs e)
         {
            try
            {
                Checked = Int32.Parse(GridView1.SelectedRow.Cells[4].Text);
                Session["data"] = GridView1.SelectedRow.Cells[1].Text;
                Session["Name"] = GridView1.SelectedRow.Cells[2].Text;
                Session["Author"] = GridView1.SelectedRow.Cells[3].Text;
                Session["count"] = Int32.Parse(GridView1.SelectedRow.Cells[5].Text);
                Checked = 1;
                var cn = Convert.ToInt32(Session["count"]);
                Decrement(string.Format("Update dbo.BookTable Set BookCount={0} Where Name='{1}'", cn - 1, Session["Name"]));
                data = Session["data"].ToString();
                var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
                string cmd = string.Format("Update dbo.BookTable Set Checked={0} Where Id={1}", Checked, data);
                string c = "Insert Into dbo.UserBookTable values(@Id,@UniqueId,@Name,@Author,@Checked)";//new inserted
                SqlConnection conn = new SqlConnection(connection);
                SqlConnection conn1 = new SqlConnection(connection);
                SqlCommand command = new SqlCommand(cmd, conn);
                SqlCommand com = new SqlCommand(c, conn1);
                command.Parameters.AddWithValue("@Checked", Checked);//
                com.Parameters.AddWithValue("@Name", Session["Name"]);//
                com.Parameters.AddWithValue("@Id", Session["Id"]);//
                com.Parameters.AddWithValue("@Author", Session["Author"]);//
                com.Parameters.AddWithValue("@Checked", Checked);//
                com.Parameters.AddWithValue("@UniqueId", index());
                conn1.Open();
                conn.Open();
                com.ExecuteNonQuery();
                command.ExecuteNonQuery();
                conn1.Close();
                conn.Close();
                if ((cn - 1) == 0)
                {
                    delete(string.Format("Delete From dbo.BookTable Where Name='{0}'", Session["Name"]));
                }
                refreshData("select * from dbo.BookTable order by Id");
                var issue = DateTime.Now.ToString();
                Session["issue"] = issue;
                penalty(string.Format("Update PenaltyPage Set [Date of Issue]='{0}' where Id={1}",issue, index()-1));
                update(string.Format("Select Pp.Id,Bt.UniqueId,Bt.Name,Bt.Author,Pp.[Date of Issue] From PenaltyPage Pp Right outer Join UserBookTable Bt On Pp.Id = Bt.UniqueId  Where Bt.Id={0} order by UniqueId", Session["Id"]));
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }



        }

        public string fetchDate()
        {
            var conn = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand check_User_Name = new SqlCommand(string.Format("Select [Date of Issue] From dbo.PenaltyPage Where Id={0}",Session["Uid"]), connection);
            connection.Open();
            string Date = check_User_Name.ExecuteScalar().ToString();
            connection.Close();
            return Date;
        }

       
        public void update(string commd)
        {
            SqlConnection con = new SqlConnection("Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(commd, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            GridView2.DataSource = ds;
            GridView2.DataBind();
            

        }
        public void Increment(string cmd)
        {
            var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand command = new SqlCommand(cmd, conn);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void Decrement(string cmd)
        {
            var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand command = new SqlCommand(cmd, conn);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void delete(string cmd)
        {
            var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand command = new SqlCommand(cmd, conn);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void add(string cmd)
        {
            var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand command = new SqlCommand(cmd, conn);
            command.Parameters.AddWithValue("@Name", Session["Name1"]);

            command.Parameters.AddWithValue("@Id", Count());
            command.Parameters.AddWithValue("@Author", Session["Author1"]);
            command.Parameters.AddWithValue("@BookCount", check()+1);
            command.Parameters.AddWithValue("@Checked", 0);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();


        }
        protected void ReturnEvent(object sender, EventArgs e)
        {
            Session["Name1"] = GridView2.SelectedRow.Cells[3].Text;
            Session["Author1"] = GridView2.SelectedRow.Cells[4].Text;
            Session["Uid"]= GridView2.SelectedRow.Cells[2].Text;
            delete(string.Format("Delete From dbo.UserBookTable Where UniqueId='{0}'", Session["Uid"]));
            if (check() == 0)
            {
                add("Insert Into dbo.BookTable values(@Id,@Name,@Author,@Checked,@BookCount)");

            }
            else
            {
                var count = check();
                Increment(string.Format("Update dbo.BookTable Set BookCount={0} Where Name='{1}'",count+1,Session["Name1"]));
            }
            refreshData("select * from dbo.BookTable order by Id");
            var Return = DateTime.Now.ToString();
            string popop =( (DateTime.Parse(Return).Subtract(Convert.ToDateTime(Convert.ToDateTime(fetchDate()))).Minutes) * 10).ToString();
            string message = string.Format("Penalty is : {0}",popop);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            penalty(string.Format("Update PenaltyPage Set [Date of Return]='{0}' where Id={1}", Return , Session["Uid"]));
            update(string.Format("Select Pp.Id,Bt.UniqueId,Bt.Name,Bt.Author,Pp.[Date of Issue] From PenaltyPage Pp Right outer Join UserBookTable Bt On Pp.Id = Bt.UniqueId  Where Bt.Id={0} order by UniqueId", Session["Id"]));

        }
        public int check()

        {
            var conn = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand check_User_Name = new SqlCommand(string.Format("SELECT BookCount FROM dbo.BookTable WHERE Name='{0}'",Session["Name1"]),connection);
            connection.Open();
            //int UserExist = (int)check_User_Name.ExecuteScalar();
            SqlDataReader read = check_User_Name.ExecuteReader();
            int UserExist=0;
            while (read.Read())
            {
                UserExist = Convert.ToInt32(read["BookCount"]);
            }
            connection.Close();
            return UserExist;
        }
        public int Count()
        {
            var conn = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand check_User_Name = new SqlCommand("Select Count(*) From dbo.BookTable",connection);
            connection.Open();
            int UserExist = (int)check_User_Name.ExecuteScalar();
            connection.Close();
            return UserExist+1;

        }
        public int index()
        {
            var count = 0;
            var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connection);
            var cmd = "Select * From dbo.UserBookTable";
            SqlCommand command = new SqlCommand(cmd, conn);
            conn.Open();
            SqlDataReader read = command.ExecuteReader();
            while (read.Read())
                count++;
            conn.Close();
            return count+1;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/LoginScreen.aspx");
        }
        public void penalty(string commd)
        {
             
            var connection = "Data Source=AURBLRLT-439;Initial Catalog=library;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand command = new SqlCommand(commd, conn);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}