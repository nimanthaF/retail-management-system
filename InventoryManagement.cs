using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace retail_system
{
    public partial class InventoryManagement : Form
    {
        public InventoryManagement()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ItemData obj = new ItemData();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SupplierData obj = new SupplierData();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PurchasingOrder obj = new PurchasingOrder();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Main obj = new Main();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }
    }
}
