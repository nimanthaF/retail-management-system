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
    }
}
