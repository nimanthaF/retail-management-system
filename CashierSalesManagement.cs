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
    public partial class CashierSalesManagement : Form
    {
        public CashierSalesManagement()
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

        private void button2_Click(object sender, EventArgs e)
        {
            CashierPrintInvoice obj = new CashierPrintInvoice();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CashierCustomerRegistration obj = new CashierCustomerRegistration();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CashierSalesForEmployees obj = new CashierSalesForEmployees();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }
    }
}
