using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace retail_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;
            MySqlDataReader mdr;

            connection.Open();
            string query = "Select * from users Where username='" + txtUsername.Text + "'and password='" + txtPassword.Text + "'";
            command = new MySqlCommand(query, connection);
            mdr = command.ExecuteReader();

            if (mdr.Read())
            {
               // MessageBox.Show("Login Successful!");
                Main main_obj = new Main();
                this.Hide();
                main_obj.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect Login Information! Try again.");
            }
            connection.Close();
        }
    }
}
