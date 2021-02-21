using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWeb.pages
{
    public partial class Teachers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = true;
            Panel4.Visible = false;
            if (TextBox1.Text == null)
            {
                Label1.Text = "Please enter credentials";
            }
            else
            {
                ClientWeb.UserService.UserServiceClient proxy = new ClientWeb.UserService.UserServiceClient();
                DataSet ds = proxy.SearchTeacher(TextBox1.Text);
                DataTable dt = ds.Tables[0];
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
        }

        protected void AllTeachers_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            ClientWeb.UserService.UserServiceClient proxy = new ClientWeb.UserService.UserServiceClient();
            DataSet ds = proxy.GetTeachers();
            DataTable dt = ds.Tables[0];
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Label1.Text = "Teachers retrived successfully!!";
        }

        protected void AddTeacher_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = true;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Label1.Text = "Enter credentials";
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = true;
        }

        protected void GetUser_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = true;
            UserService.IUserService client = new UserService.UserServiceClient();
            DataSet ds = client.GetTeacher(Convert.ToInt32(TextBox8.Text));

            if (ds.Tables[0].Rows.Count > 0)
            {
                TextBox2.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                TextBox3.Text = ds.Tables[0].Rows[0]["Gender"].ToString();
                TextBox4.Text = ds.Tables[0].Rows[0]["DateofBirth"].ToString();
                TextBox6.Text = ds.Tables[0].Rows[0]["Sub"].ToString();
            }
            else
            {
                Label1.Text = "No teacher with this ID ,Enter valid!";
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = true;
            UserService.IUserService client = new UserService.UserServiceClient();
            UserService.UserInfo user = new UserService.UserInfo();
            user.Type = UserService.UserType.Teacher;
            user.Sub =TextBox6.Text;
            user.ID = Convert.ToInt32(TextBox8.Text);
            user.Name = TextBox2.Text;
            user.Gender = TextBox3.Text;
            String Text = "22/11/2009";

            user.DOB = DateTime.ParseExact(Text, "dd/MM/yyyy", null);
            client.UpdateUser(user);
            Label1.Text = "Teacher updated successfully!!";
        }

        protected void DeleteTeacher_Click(object sender, EventArgs e)
        {

            UserService.IUserService client = new UserService.UserServiceClient();

            client.DeleteUser(Convert.ToInt32(TextBox8.Text));
            Label1.Text = "Deleted successfully";
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            UserService.IUserService client = new UserService.UserServiceClient();
            UserService.UserInfo user = new UserService.UserInfo();
            user.Type = UserService.UserType.Teacher;
            user.Sub = Sub.Text;
            user.ID = Convert.ToInt32(Id.Text);
            user.Name = Name.Text;
            user.Gender = Gender.Text;
            user.DOB = Convert.ToDateTime(DOB.Text);

            client.SaveUser(user);
            Label1.Text = "Teachers saved successfully!!";
        }
    }
}