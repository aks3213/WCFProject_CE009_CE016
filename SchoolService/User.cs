using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SchoolService
{
    [MessageContract(IsWrapped =true,WrapperName ="UserRequestObject",WrapperNamespace ="http://MySchool.com/User")]
    public class UserRequest
    {
        [MessageHeader(Namespace = "http://MySchool.com/User")]
        public string LicenceKey { get; set; }
        [MessageBodyMember]
        public int UserId { get; set; }
    }
    [MessageContract(IsWrapped = true,
                     WrapperName = "UserInfoObject",
                     WrapperNamespace = "http://MySchool.com/User")]
    public class UserInfo
    {
        public UserInfo()
        {
        }
        public UserInfo(User user)
        {
            this.ID = user.Id;
            this.Name = user.Name;
            this.Gender = user.Gender;
            this.DOB = user.DateOfBirth;
            this.Type = user.Type;
            if (this.Type == UserType.Teacher)
            {
                this.Sub = ((Teacher)user).Sub;
            }
            else
            {
                this.Std = ((Student)user).Std;
            }
        }

        [MessageBodyMember(Order = 1, Namespace = "http://MySchool.com/User")]
        public int ID { get; set; }
        [MessageBodyMember(Order = 2, Namespace = "http://MySchool.com/User")]
        public string Name { get; set; }
        [MessageBodyMember(Order = 3, Namespace = "http://MySchool.com/User")]
        public string Gender { get; set; }
        [MessageBodyMember(Order = 4, Namespace = "http://MySchool.com/User")]
        public DateTime DOB { get; set; }
        [MessageBodyMember(Order = 5, Namespace = "http://MySchool.com/User")]
        public UserType Type { get; set; }
        [MessageBodyMember(Order = 6, Namespace = "http://MySchool.com/User")]
        public string Sub { get; set; }
        [MessageBodyMember(Order = 7, Namespace = "http://MySchool.com/User")]
        public int Std { get; set; }
    }

    [KnownType(typeof(Student))]
    [KnownType(typeof(Teacher))]
    [DataContract(Namespace = "http://akshay.com/User")]
    public class User
    {
        private string _name;
        private string _gender;
        private DateTime _dateOfBirth;

        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [DataMember(Order = 3)]
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        [DataMember(Order = 4)]
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }
        [DataMember(Order = 5)]
        public UserType Type { get; set; }
    }

    public enum UserType
    {
        Student=1,
        Teacher=2
    }
}
