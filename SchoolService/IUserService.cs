using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SchoolService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        DataSet SearchTeacher(string input);
        [OperationContract]
        DataSet GetTeacher(int id);
        [OperationContract]
        DataSet GetTeachers();
        [OperationContract]
        DataSet GetStudent(int id);
        [OperationContract]
        DataSet SearchStudent(string input);
        [OperationContract]
        DataSet Search(string input);
        [OperationContract]
        DataSet GetStudnets();
        [OperationContract]
        void UpdateUser(UserInfo User);
        [OperationContract]
        void DeleteUser(int id);
        [OperationContract]
        DataSet GetUser(int id);
        [OperationContract]
        void SaveUser(UserInfo User);

        [OperationContract]
        DataSet GetAllUsers();
    }
}
