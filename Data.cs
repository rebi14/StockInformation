using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace StockInformation
{
    
    
        public class Data
        {
            public static void SaveData(object obj, string filename)
            {
                XmlSerializer sr = new XmlSerializer(obj.GetType());
                TextWriter writer = new StreamWriter(filename);
                sr.Serialize(writer, obj);
                writer.Close();
            }

        public static Users LoadData(string filename)
        {
            Users users;
            XmlSerializer sr = new XmlSerializer(typeof(Users));

            using (FileStream stream = File.OpenRead(filename))
            {
               users = (Users)sr.Deserialize(stream);
            }
            return users;
        }


        public static void SaveUserData(User user, string filename)
        {
            XmlTextWriter yaz = new XmlTextWriter(filename, System.Text.UTF8Encoding.UTF8);
            
            yaz.Formatting = Formatting.Indented;
            
           
        }
      }
    }

