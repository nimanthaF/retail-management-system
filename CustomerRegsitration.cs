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
    public partial class CustomerRegsitration : Form
    {
        public CustomerRegsitration()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;
            MySqlDataReader mdr;

            connection.Open();

            string selectQuery = "SELECT * FROM customer WHERE CustomerID = '" + txtCustomID.Text + "';";
            command = new MySqlCommand(selectQuery, connection);
            mdr = command.ExecuteReader();

            if (mdr.Read())
            {
                MessageBox.Show("Customer already registered!");
            }
            else
            {
                string query = "INSERT INTO  customer(CustomerID,Name,Mobile,ID,Email,Birthday,Address) VALUES('" + txtCustomID.Text + "','" + txtName.Text + "','" + txtMobile.Text + "','" + txtID.Text + "','" + txtMail.Text + "','" + dateBirthday.Text + "','" + txtAddress.Text + "')";
                MySqlConnection databaseConnection = new MySqlConnection(MyConString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    databaseConnection.Close();
                    QRgenerate qr_obj = new QRgenerate();
                    qr_obj.ShowDialog();
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show("Customer Successfully Registered!");
            }

            connection.Close();
            
        }

        private void discardButton_Click(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;
            MySqlDataReader mdr;

            connection.Open();

            string selectQuery = "DELETE FROM customer WHERE CustomerID = '" + txtCustomID.Text + "';";
            command = new MySqlCommand(selectQuery, connection);
            mdr = command.ExecuteReader();

            MessageBox.Show("Customer Successfully Discarded!");
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SalesManagement sales_obj = new SalesManagement();
            this.Hide();
            sales_obj.ShowDialog();
            this.Close();
        }
    }
}
