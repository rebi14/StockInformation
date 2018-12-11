using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockInformation
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }


        List<Product> searchproduct = null;


        private void Search_Click(object sender, EventArgs e)
        {
            try
            {
                DataBaseConnection _db = new DataBaseConnection();

                searchproduct = _db.SearchProduct(ProductNameBox.Text, ProductBrandBox.Text);
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = searchproduct;
                dataGridView2.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

     }

}
