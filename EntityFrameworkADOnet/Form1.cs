using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkADOnet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductDAL _productDAL = new ProductDAL();


        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _productDAL.GetAll2();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDAL.Add(new Product
            {
                Name = tbAddName.Text,
                UnitPrice = Convert.ToDecimal(tbAddPrice.Text),
                StockAmount = Convert.ToInt32(tbAddStock.Text)
            });
            dataGridView1.DataSource = _productDAL.GetAll2();

            MessageBox.Show("Product Added");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbUpdateName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            tbUpdatePrice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            tbUpdateStock.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Product product = new Product
            {
                Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value),
                Name = tbUpdateName.Text,
                UnitPrice = Convert.ToDecimal(tbUpdatePrice.Text),
                StockAmount = Convert.ToInt32(tbUpdateStock.Text)
            };
            _productDAL.Update(product);
            dataGridView1.DataSource = _productDAL.GetAll2();

            MessageBox.Show("Updated");

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            _productDAL.Delete(id);
            dataGridView1.DataSource = _productDAL.GetAll2();
            MessageBox.Show("Deleted");
        }
    }
}
