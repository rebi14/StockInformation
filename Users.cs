using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StockInformation
{
    public class Users
    {
        public Users()
        {
            List<User> UserList = new List<User>();
        }

        [XmlArray("Users"), XmlArrayItem(typeof(User), ElementName = "User")]
        public List<User> UserList { get; set; }
    }
}
