using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SchoolService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in both code and config file together.
    public class UserService : IUserService
    {
        public DataSet SearchTeacher(string search)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            string Query = "SELECT * from tblUser WHERE (tblUser.UserType=2) and (tblUser.Id LIKE'%" + search + "%' OR tblUser.Name LIKE'%" + search + "%' OR tblUser.Gender LIKE'%" + search + "%' OR tblUser.DateofBirth LIKE'%" + search + "%' OR tblUser.Std LIKE'%" + search + "%') ORDER BY tblUser.Name ";

            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
        public DataSet SearchStudent(string search)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            string Query = "SELECT * from tblUser WHERE (tblUser.UserType=1) and (tblUser.Id LIKE'%" + search + "%' OR tblUser.Name LIKE'%" + search + "%' OR tblUser.Gender LIKE'%" + search + "%' OR tblUser.DateofBirth LIKE'%" + search + "%' OR tblUser.Std LIKE'%" + search + "%') ORDER BY tblUser.Name ";

            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
        public DataSet GetTeacher(int id)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                string Query = "SELECT * FROM tblUser WHERE Id=@Id and UserType=2";

                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                sda.SelectCommand.Parameters.AddWithValue("@Id", id);
                sda.Fill(ds);
            }
            catch (FaultException fex)
            {
                throw new FaultException<string>("Error:  " + fex);
            }
            return ds;
        }
        public DataSet GetTeachers()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                string Query = "SELECT * FROM tblUser WHERE UserType=2";

                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                sda.Fill(ds);
            }
            catch (FaultException fex)
            {
                throw new FaultException<string>("Error:  " + fex);
            }
            return ds;
        }
        public DataSet GetStudent(int id)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                string Query = "SELECT * FROM tblUser WHERE Id=@Id and UserType=1";

                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                sda.SelectCommand.Parameters.AddWithValue("@Id", id);
                sda.Fill(ds);
            }
            catch (FaultException fex)
            {
                throw new FaultException<string>("Error:  " + fex);
            }
            return ds;
        }
        public DataSet Search(string search)
        {
            /*
            SELECT * from tblUser WHERE tblUser.Name LIKE'%akshay%' OR tblUser.Gender LIKE'%akshay%' ORDER BY tblUser.Name
            */
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            
            string Query = "SELECT * from tblUser WHERE tblUser.Id LIKE'%" + search + "%' OR tblUser.Name LIKE'%" + search+ "%' OR tblUser.Gender LIKE'%" + search + "%' OR tblUser.DateofBirth LIKE'%" + search + "%' OR tblUser.Std LIKE'%" + search + "%' ORDER BY tblUser.Name";

            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataSet ds = new DataSet(); 
            sda.Fill(ds);
            return ds;
        }
        public DataSet GetStudnets()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                string Query = "SELECT * FROM tblUser WHERE UserType=1";

                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                sda.Fill(ds);
            }
            catch (FaultException fex)
            {
                throw new FaultException<string>("Error:  " + fex);
            }
            return ds;
        }
        public void UpdateUser(UserInfo user)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterId = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = user.ID
                };
                cmd.Parameters.Add(parameterId);

                SqlParameter parameterName = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = user.Name
                };
                cmd.Parameters.Add(parameterName);

                SqlParameter parameterGender = new SqlParameter
                {
                    ParameterName = "@Gender",
                    Value = user.Gender
                };
                cmd.Parameters.Add(parameterGender);

                SqlParameter parameterDateOfBirth = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = user.DOB
                };
                cmd.Parameters.Add(parameterDateOfBirth);

                SqlParameter parameterUserType = new SqlParameter
                {
                    ParameterName = "@UserType",
                    Value = user.Type
                };
                cmd.Parameters.Add(parameterUserType); 
                System.Diagnostics.Debug.WriteLine(user.Type + " -- -- --  " + user.Type.Equals("Student"));
                if (user.Type==UserType.Student)
                {
                    SqlParameter Std = new SqlParameter
                    {
                        ParameterName = "@Std",
                        Value = user.Std
                    };
                    cmd.Parameters.Add(Std);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("SomeText33333333333");
                    SqlParameter Sub = new SqlParameter
                    {
                        ParameterName = "@Sub",
                        Value = user.Sub,
                    };
                    cmd.Parameters.Add(Sub);

                }
                System.Diagnostics.Debug.WriteLine("SomeText 4444444444");
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand();
            string Query = "DELETE FROM tblUser Where Id=@UserID";
            cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@UserID", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public DataSet GetAllUsers()
        {
            SqlDataAdapter da =new SqlDataAdapter("select * from tblUser",@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            DataSet ds =new DataSet();
            da.Fill(ds, "tblUser");
            return ds;
        }

        public DataSet GetUser(int id)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                string Query = "SELECT * FROM tblUser WHERE Id=@Id";

                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                sda.SelectCommand.Parameters.AddWithValue("@Id", id);
                sda.Fill(ds);
            }
            catch (FaultException fex)
            {
                throw new FaultException<string>("Error:  " + fex);
            }
            return ds;
        }

        public void SaveUser(UserInfo user)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spSaveUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterId = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = user.ID
                };
                cmd.Parameters.Add(parameterId);

                SqlParameter parameterName = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = user.Name
                };
                cmd.Parameters.Add(parameterName);

                SqlParameter parameterGender = new SqlParameter
                {
                    ParameterName = "@Gender",
                    Value = user.Gender
                };
                cmd.Parameters.Add(parameterGender);

                SqlParameter parameterDateOfBirth = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = user.DOB
                };
                cmd.Parameters.Add(parameterDateOfBirth);

                SqlParameter parameterUserType = new SqlParameter
                {
                    ParameterName = "@UserType",
                    Value = user.Type
                };
                cmd.Parameters.Add(parameterUserType);

                if (user.Type == UserType.Student)
                {
                    SqlParameter Std = new SqlParameter
                    {
                        ParameterName = "@Std",
                        Value = user.Std
                    };
                    cmd.Parameters.Add(Std);
                }
                else
                {
                    SqlParameter Sub = new SqlParameter
                    {
                        ParameterName = "@Sub",
                        Value = user.Sub,
                    };
                    cmd.Parameters.Add(Sub);

                }
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
