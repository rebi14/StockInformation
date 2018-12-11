using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlTypes;
using System.Windows.Forms;


namespace StockInformation
{
    public class DataBaseConnection
    {
        private SQLiteConnection _connect = null;
        public SQLiteConnection Connection
        {
            get
            {

                if (_connect == null)
                    _connect = new SQLiteConnection("Data Source = C:\\Development\\stockinformation\\StockInformation\\StockInformation\\bin\\Debug\\baran_db.db");
                return _connect;

            }
            private set
            {

            }
        }




       
 public DataBaseConnection()
        {

        }

    }
}
    
        

       
