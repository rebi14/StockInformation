using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockInformation
{
    class ProductDOAImplementation : ProductDAO
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

        public List<Product> SearchProduct(string ProductName, string Brand)
        {
            throw new NotImplementedException();
        }
    }
}









