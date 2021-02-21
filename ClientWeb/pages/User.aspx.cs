using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWeb
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
        }
        protected void btnGetUser_Click(object sender, EventArgs e)
        {
            Panel2.Visible = true;
            if (TextBox1.Text != "")
            {
                UserService.IUserService client = new UserService.UserServiceClient();
                DataSet ds = client.GetUser(Convert.ToInt32(TextBox1.Text));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    TextBox2.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    TextBox3.Text = ds.Tables[0].Rows[0]["Gender"].ToString();
                    TextBox4.Text = ds.Tables[0].Rows[0]["DateofBirth"].ToString();
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["UserType"])==1)
                    {
                        tr1.Visible = true;
                        tr2.Visible = false;
                        Label2.Text = "Student";
                        TextBox6.Text= ds.Tables[0].Rows[0]["Std"].ToString();
                    }
                    else
                    {

                        tr1.Visible = false;
                        tr2.Visible = true;
                        Label2.Text = "Teacher";
                        TextBox7.Text = ds.Tables[0].Rows[0]["Sub"].ToString();
                    }
                }
                else
                {
                    Label1.Text = "No user with this ID ,Enter valid!";
                }

            }
            else
            {
                Label1.Text = "Please Enter User ID !";
            }
        }

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            if (ddlUserType.SelectedValue == "-1")
            {
                trSub.Visible = false;
                trStd.Visible = false;
            }
            else if (ddlUserType.SelectedValue == "1")
            {
                trStd.Visible = true;
                trSub.Visible = false;
            }
            else
            {
                trStd.Visible = false;
                trSub.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            UserService.IUserService client = new UserService.UserServiceClient();
            UserService.UserInfo user = new UserService.UserInfo();
            if (ddlUserType.SelectedValue == "-1")
            {
                lblMessage.Text = "Please select User Type";
            }
            else
            {
                if (((UserService.UserType)Convert.ToInt32(ddlUserType.SelectedValue)) == UserService.UserType.Student)
                {
                    user.Type = UserService.UserType.Student;
                    user.Std = Convert.ToInt32(txtStd.Text);
                }
                else
                {
                    user.Type = UserService.UserType.Teacher; 
                    user.Sub = (txtSubject.Text).ToString();
                }
                user.ID = Convert.ToInt32(txtID.Text);
                user.Name = txtName.Text;
                user.Gender = txtGender.Text;
                user.DOB = Convert.ToDateTime(txtDateOfBirth.Text);

                client.SaveUser(user);
                lblMessage.Text = "User saved successfully!!";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            Panel1.Visible = true;
            Panel3.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = true;
            Panel3.Visible = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = true;
            ClientWeb.UserService.UserServiceClient proxy = new ClientWeb.UserService.UserServiceClient();
            DataSet ds = proxy.GetAllUsers();
            DataTable dt=ds.Tables[0];
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            UserService.IUserService client = new UserService.UserServiceClient();

            client.DeleteUser(Convert.ToInt32(TextBox1.Text));
            Label1.Text = "Deleted successfully";
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Panel2.Visible = true;
            UserService.IUserService client = new UserService.UserServiceClient();
            UserService.UserInfo user = new UserService.UserInfo();
            if (Label2.Text.Equals("Student"))
            {
                user.Type = UserService.UserType.Student;
                user.Std = Convert.ToInt32(TextBox6.Text);
            }
            else
            {
                user.Type = UserService.UserType.Teacher;
                user.Sub = (TextBox7.Text).ToString();
            }
            user.ID = Convert.ToInt32(TextBox1.Text);
            user.Name = TextBox2.Text;
            user.Gender = TextBox3.Text;
            //user.DOB = Convert.ToDateTime(TextBox4.Text);
            // DateTime CreatdDate = DateTime.ParseExact(TextBox4.Text,"dd-MM-yyyy HH:mm:ss",
            //   System.Globalization.CultureInfo.InvariantCulture,DateTimeStyles.AssumeLocal);        
            //user.DOB =DateTime.ParseExact(TextBox4.Text, "dd/MM/yyyy", new CultureInfo("en-US")); ;
            String Text = "22/11/2009";

            user.DOB = DateTime.ParseExact(Text, "dd/MM/yyyy", null);
            client.UpdateUser(user);
            lblMessage.Text = "User saved successfully!!";
        }
    }
}