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
    public partial class CashierMain : Form
    {
        public CashierMain()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CashierSalesManagement obj = new CashierSalesManagement();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CashierEmployManagement obj = new CashierEmployManagement();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }
    }
}
