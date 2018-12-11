
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace StockInformation
{
    public enum UserRoles
    {
        Admin,
        Writer,
        Reader,
        None
    }
    
    public class User
    {
        public int ID { get; set; }
       
        public string Name { get; set; } 
        public string Surname { get; set; }
        public string Username { get; set;}
        public string Telephone { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string StartDateofEmployment { get; set; }
        public string Birthdate { get; set; }
        public string Address { get; set; }
        public UserRoles UserRole { get; set; }

    }
}
