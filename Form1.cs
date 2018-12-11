using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace StockInformation
{
    public partial class login : Form
    {
        List<User> _kullanıcılar = new List<User>();
        
        public login()
        {
            InitializeComponent();
        }
        
       

        private void button1_Click(object sender, EventArgs e)
        {


            
            DataBaseConnection conn = new DataBaseConnection();
            User arananuser = null;
            arananuser=conn.selectUser(username.Text, password.Text);

          
           
            if (arananuser != null)
            {
                Program.Kullanıcı = arananuser;
                //MessageBox.Show("Merhaba " + arananuser.Name + " " + arananuser.Surname);
            }
            else
            {
                MessageBox.Show("Kullanıcı bulunamadı.");
                return;
            }
            USER form2 = new USER();
            form2.Show();
            this.Hide();
        }

      
    }
}