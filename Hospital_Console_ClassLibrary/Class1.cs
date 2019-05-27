using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Hospital_Console_ClassLibrary
{
    [Serializable]
    public class User
    {
        
        public string name { get; set; }
        public string sname { get; set; }
        public long socialID { get; set; }
        public string login { get; set; }
        public string password { get; set;}
        public int daysWorked = 0;

        public User(string _name, string _sname, long _socialID, string _login, string _password)
        {
            name = _name;
            sname = _sname;
            socialID = _socialID;
            login = _login;
            password = _password;
            
        }


    }
    [Serializable]
    public class Doctor : User
    {
        public int WHZ { get; set; }
         public string Spec { get; set; }
        

        public Doctor(string _name, string _sname, long _socialID, string _login, string _password, int _WHZ, string _Spec) : base(_name, _sname, _socialID, _login, _password)
        {
            WHZ = _WHZ;
            Spec = _Spec;
        }
    }
    [Serializable]
    public class Nurse : User
    {
        public Nurse(string _name, string _sname, long _socialID, string _login, string _password) : base(_name, _sname, _socialID, _login, _password)
        {
            
        }
    }
    [Serializable]
    public class Admin : User
    {
         
        public Admin(string _name, string _sname, long _socialID, string _login, string _password) : base(_name, _sname, _socialID, _login, _password)
        {

        }

        
       
    }

    [Serializable]
    public class Hospital
    {
        public List<Nurse> Nurses { get; set; } = new List<Nurse>();
        public List<Admin> Admins { get; set; } = new List<Admin>();
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public List<Nurse> NursesTimetable { get; set; } = new List<Nurse>();
        public List<Doctor> monthd { get; set; } = new List<Doctor>();
        public List<Doctor>[] monthd2 = new List<Doctor>[31];

        //public string[] NursesTimetable = new string[0];
        //public string[] DoctorsTimetable = new string[0];

    }

        


}
