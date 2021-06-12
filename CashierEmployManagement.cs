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
    public partial class CashierEmployManagement : Form
    {
        public CashierEmployManagement()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CashierMain obj = new CashierMain();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CashierLeaves obj = new CashierLeaves();
            this.Hide();
            obj.ShowDialog();
            this.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CashierEmployUpdate obj = new CashierEmployUpdate();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }
    }
}
