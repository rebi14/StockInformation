using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockInformation
{
    class ProductDOASQLite : ProductDAO
    {
        public Product FillProductData(DbDataReader reader)
        {
            Product aranan = new Product();
            aranan.ID = reader.GetInt32(0);
            aranan.BillingNumber = (reader["BillingNumber"] != null) ? reader["BillingNumber"].ToString() : "Default BillingNumber";
            aranan.CurrentSupplier = (reader["CurrentSupplier"] != null) ? reader["CurrentSupplier"].ToString() : "Default CurrentSupplier";
            aranan.ProductName = (reader["ProductName"] != null) ? reader["ProductName"].ToString() : "Default ProductName";
            aranan.State = (reader["State"] != null) ? reader["State"].ToString() : "Default State";
            aranan.Type = (reader["Type"] != null) ? reader["Type"].ToString() : "Default Type";
            aranan.Brand = (reader["Brand"] != null) ? reader["Brand"].ToString() : "Default Brand ";

            if (reader["Total"].ToString() == "")
            {
                aranan.Total = 0;
            }
            else
            {

                aranan.Total = reader.GetInt32(reader.GetOrdinal("Total"));
            }
            aranan.Explanation = (reader["Explanation"] != null) ? reader["Explanation"].ToString() : "Default Explanation";
            return aranan;
        }

        public List<Product> SearchProduct(DataBaseConnection conn, string ProductName, string Brand)
        {
            List<Product> search = new List<Product>();

            conn.Connection.Open();


                using (SQLiteCommand fmd = conn.Connection.CreateCommand())
                {
                    string cmdtext = "";
                    if (ProductName == "" && Brand == "")
                    {
                        cmdtext = "Select * From Products";
                    }
                    else
                    {
                        cmdtext += cmdtext = "Select * From Products where";

                        Boolean addAnd = false;
                        if (ProductName != "")
                        {

                            cmdtext += " ProductName like '" + ProductName + "%'";
                            addAnd = true;
                        }

                        if (Brand != "")
                        {
                            if (addAnd)
                                cmdtext += " AND ";
                            cmdtext += " Brand like '" + Brand + "%'";

                        }
                    }



                    fmd.CommandText = cmdtext;
                    using (SQLiteDataReader read = fmd.ExecuteReader())
                    {


                        while (read.Read())
                        {
                            Product _product = FillProductData(read);
                            if (_product != null)
                            {
                                search.Add(_product);
                            }
                        }
                    }
                }
     
            
            conn.Connection.Close();
            return search;
        }
    }
}









