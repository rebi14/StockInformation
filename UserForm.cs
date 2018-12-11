using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SQLite;
using System.Data.Common;
using System.Data.SqlClient;

namespace StockInformation
{

    public partial class USER : Form
    {

        private int activeUserId = -1;

        public Users users;

        public USER()
        {
            InitializeComponent();
            Text = Program.Kullanıcı.Name + " " + Program.Kullanıcı.Surname;
            this.ActiveControl = name;

        }

        UserMethods Search = new UserMethods(); 


        private void AddButon_Click_1(object sender, EventArgs e) //CLEAR button
        {
            savebutton.Text = "ADD";
            activeUserId = -1;
            ClearEdits();
        }

        private void ClearEdits()
        {
            name.Text = "";
            surname.Text = "";

            UsernameBox4.Text = "";
            Password.Text = "";
            maskedTextBox1.Text = "";
            MailBox.Text = "";
            maskedTextBox2.Text = "";
            maskedTextBox3.Text = "";
            AddressBox.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void FillGrid(DataBaseConnection _db)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Search.SearchUser("");
            dataGridView1.Columns[0].Visible = false;
        }

        private void PopulateUser(User user)
        {
            name.Text = user.Name;
            surname.Text = user.Surname;
            UsernameBox4.Text = user.Username;
            Password.Text = user.Password;
            maskedTextBox1.Text = user.Telephone;
            MailBox.Text = user.Mail;
            maskedTextBox2.Text = user.StartDateofEmployment;
            maskedTextBox3.Text = user.Birthdate;
            AddressBox.Text = user.Address;

            int itemIndex = comboBox1.Items.IndexOf(user.UserRole.ToString());
            comboBox1.SelectedIndex = itemIndex;
        }

        private void button2_Click(object sender, EventArgs e)//Save (UPDATE) button
        {


            if (activeUserId > 0)
            {
                User _user = new User();
                _user.Name = name.Text;
                _user.Surname = surname.Text;
                _user.Username = UsernameBox4.Text;
                _user.Password = Password.Text;
                _user.Telephone = maskedTextBox1.Text;
                _user.Mail = MailBox.Text;
                _user.StartDateofEmployment = maskedTextBox2.Text;
                _user.Birthdate = maskedTextBox3.Text;
                _user.Address = AddressBox.Text;
                _user.UserRole = (UserRoles)Enum.Parse(typeof(UserRoles), comboBox1.Text);
                _user.ID = activeUserId;
                DataBaseConnection _db = new DataBaseConnection();

                _db.UpdateUser(_user);

                FillGrid(_db);
                int rowIndex = findRowIndex(activeUserId);
                if (rowIndex >= 0)
                    dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[1];


            }
            else
            {
                try
                {
                    DataBaseConnection _db = new DataBaseConnection();
                    User _user = new User();
                    _user.Name = name.Text;
                    _user.Surname = surname.Text;
                    _user.Username = UsernameBox4.Text;
                    _user.Password = Password.Text;
                    _user.Telephone = maskedTextBox1.Text;
                    _user.Mail = MailBox.Text;
                    _user.StartDateofEmployment = maskedTextBox2.Text;
                    _user.Birthdate = maskedTextBox3.Text;
                    _user.Address = AddressBox.Text;
                    _user.UserRole = (UserRoles)Enum.Parse(typeof(UserRoles), comboBox1.Text);
                    _db.AddUser(_user);
                    FillGrid(_db);
                    MessageBox.Show("Kayıt Başarıyla Eklendi");
                    name.Clear();
                    surname.Clear();
                    UsernameBox4.Clear();
                    Password.Clear();
                    maskedTextBox1.Clear();
                    MailBox.Clear();
                    maskedTextBox2.Clear();
                    maskedTextBox3.Clear();
                    AddressBox.Clear();

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Kayıt Eklenemedi" + ex.Message);

                }
            }

        }

        List<User> searchlist = null;

        private void button3_Click(object sender, EventArgs e)// search button
        {
            try
            {
                DataBaseConnection _db = new DataBaseConnection();
                searchlist = _db.SearchUser(UserSearchBox.Text);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = searchlist;
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.SelectedRows.Count == 0 && dataGridView1.SelectedCells.Count == 0)
                    return;
                User _user = null;
                int rowindex = -1;
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    rowindex = dataGridView1.SelectedRows[0].Index;
                    _user = dataGridView1.SelectedRows[0].DataBoundItem as User;
                }
                else
                {
                    rowindex = dataGridView1.SelectedCells[0].RowIndex;
                    _user = dataGridView1.Rows[rowindex].DataBoundItem as User;
                }
                DataBaseConnection _db = new DataBaseConnection();


                if (_db.DeleteUser(_user))
                {
                    MessageBox.Show("Kayıt silindi");
                    searchlist.RemoveAt(rowindex);
                    dataGridView1.ClearSelection();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = searchlist;

                }

                else
                    MessageBox.Show("kayıt silinemedi");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;
        }






        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (dataGridView1.SelectedCells.Count > 0)
            {
                savebutton.Text = "SAVE";
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                int userId = (int)selectedRow.Cells["ID"].Value;

                activeUserId = userId;

                DataBaseConnection _db = new DataBaseConnection();

                PopulateUser(_db.GetUser(userId));

            }


        }

        private int findRowIndex(int ID)
        {

            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(ID.ToString()))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            return rowIndex;

        }

        private void Product_Click(object sender, EventArgs e)
        {
            ProductForm secondform = new ProductForm();
            secondform.Show();


        }




    }
}









