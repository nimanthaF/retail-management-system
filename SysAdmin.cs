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
    public partial class SysAdmin : Form
    {
        public SysAdmin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserRegistration obj = new UserRegistration();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserUpdate obj = new UserUpdate();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }
    }
}
