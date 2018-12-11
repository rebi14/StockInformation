using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlTypes;
using System.Data.Common;


namespace StockInformation
{
    public interface ProductDAO
    {
        Product FillProductData(DbDataReader reader);
        List<Product> SearchProduct(string ProductName, string Brand);
    }
}