using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockInformation
{
    class UserMethods
    {

        DataBaseConnection Connection = new DataBaseConnection();

        private User FillUserData(SQLiteDataReader reader)
        {
            try
            {
                User aranan = new User();
                aranan.ID = reader.GetInt32(0);
                aranan.Name = (reader["Name"] != null) ? reader["Name"].ToString() : "Default name";
                aranan.Surname = (reader["Surname"] != null) ? reader["Surname"].ToString() : "Default surname";
                aranan.Username = (reader["Username"] != null) ? reader["Username"].ToString() : "Default username";
                aranan.Password = (reader["Password"] != null) ? reader["Password"].ToString() : "Default password";
                aranan.Telephone = (reader["Telephone"] != null) ? reader["Telephone"].ToString() : "Default telephone";
                aranan.Mail = (reader["Mail"] != null) ? reader["Mail"].ToString() : "Default mail";
                aranan.StartDateofEmployment = (reader["StartDateofEmployment"] != null) ? reader["StartDateofEmployment"].ToString() : "Default Start date of employment ";
                aranan.Birthdate = (reader["Birthdate"] != null) ? reader["Birthdate"].ToString() : "Default birthdate";
                aranan.Address = (reader["Address"] != null) ? reader["Address"].ToString() : "Default address";
                aranan.UserRole = (reader["UserRole"] != null) ? (UserRoles)Enum.Parse(typeof(UserRoles), reader["UserRole"].ToString()) : UserRoles.None;
                return aranan;
            }
            catch
            {

            }
            return null;
        }



        public User selectUser(string username, string password)
        {
            User aranan = null;
            
            SQLiteConnection c = Connection.Connection;
            c.Open();

            using (SQLiteCommand fmd = c.CreateCommand())
            {
                try
                {
                    string cmdtext = @"SELECT * from Users where username ='" + username + "'and password = '" + password + "'"; ;
                    fmd.CommandText = cmdtext;
                    using (SQLiteDataReader reader = fmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            aranan = FillUserData(reader);
                        }

                    }
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
                c.Close();
                return aranan;
            }

        }

        public User GetUser(int ID)
        {
            User _user = null;
            Connection.Open();


            try
            {
                using (SQLiteCommand fmd = Connection.CreateCommand())
                {
                    string cmdtext = "Select * From Users";
                    if (ID != 0)
                        cmdtext += " where ID = " + ID.ToString();

                    fmd.CommandText = cmdtext;

                    using (SQLiteDataReader read = fmd.ExecuteReader())
                    {

                        while (read.Read())
                        {

                            _user = FillUserData(read);

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Connection.Close();
            return _user;
        }



        public List<User> SearchUser(string name)
        {
            List<User> search = new List<User>();
            Connection.Open();

            try
            {
                using (SQLiteCommand fmd = Connection.CreateCommand())
                {
                    string cmdtext = "Select * From Users";
                    if (name != "")
                        cmdtext += " where Name like '%" + name + "%' ";
                    fmd.CommandText = cmdtext;

                    using (SQLiteDataReader read = fmd.ExecuteReader())
                    {


                        while (read.Read())
                        {
                            User _user = FillUserData(read);
                            if (_user != null)
                            {
                                search.Add(_user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Connection.Close();
            return search;
        }

        public bool AddUser(User _user)
        {



            if (_user == null)
                return false;

            string ekle = "insert into Users(Name,Surname,Username,Password,Telephone,Mail,StartDateofEmployment,Birthdate,Address,UserRole)" +
                 "VALUES(@Name,@Surname,@Username,@Password,@Telephone,@Mail,@StartDateofEmployment,@Birthdate,@Address,@UserRole)";
            SQLiteCommand comm = new SQLiteCommand(ekle, Connection);
            try
            {
                Connection.Open();
                comm.Parameters.AddWithValue("@Name", _user.Name);
                comm.Parameters.AddWithValue("@Surname", _user.Surname);
                comm.Parameters.AddWithValue("@Username", _user.Username);
                comm.Parameters.AddWithValue("@Password", _user.Password);
                comm.Parameters.AddWithValue("@Telephone", _user.Telephone);
                comm.Parameters.AddWithValue("@Mail", _user.Mail);
                comm.Parameters.AddWithValue("@StartDateofEmployment", _user.StartDateofEmployment);
                comm.Parameters.AddWithValue("@Birthdate", _user.Birthdate);
                comm.Parameters.AddWithValue("@Address", _user.Address);
                comm.Parameters.AddWithValue("@UserRole", (int)_user.UserRole);
                object o = comm.ExecuteNonQuery();
                return (o != null);
            }

            catch (SQLiteException ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }


        }

        public bool UpdateUser(User _user)
        {



            if (_user == null)
                return false;


            string güncel = "Update Users SET Name = \'" + _user.Name + "\', Surname = \'" + _user.Surname + "\', Username = \'" + _user.Username + "\',Password = \'" + _user.Password +
                          "\', Telephone = \'" + _user.Telephone + "\',Mail = \'" + _user.Mail + "\',StartDateofEmployment = \'" + _user.StartDateofEmployment + "\',Birthdate = \'" + _user.Birthdate +
                          "\',Address = \'" + _user.Address + "\',UserRole = " + ((int)_user.UserRole).ToString() + " where ID =" + _user.ID.ToString();

            SQLiteCommand comm = new SQLiteCommand(güncel, Connection);
            try
            {
                Connection.Open();
                object o = comm.ExecuteNonQuery();
                return (o != null);


            }

            catch (SQLiteException ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        public bool DeleteUser(User _user)
        {


            if (_user == null)
                return false;
            return DeleteUser(_user.ID);
        }

        public bool DeleteUser(int userId)
        {


            if (userId <= 0)
                return false;

            string sil = " DELETE FROM Users WHERE ID=" + userId.ToString();
            SQLiteCommand comm = new SQLiteCommand(sil, Connection);
            try
            {
                Connection.Open();
                int result = comm.ExecuteNonQuery();
                return result > 0;
            }

            catch (SQLiteException ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

    }

}
    }
}
