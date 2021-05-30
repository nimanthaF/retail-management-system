using MySql.Data.MySqlClient;
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
    public partial class SalesForEmployees : Form
    {
        public SalesForEmployees()
        {
            InitializeComponent();
            FillcomboCashier();
            
        }

        void FillcomboCashier()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;


            MySqlDataReader mdr;



            connection.Open();
            string query = "Select * from users";


            command = new MySqlCommand(query, connection);


            mdr = command.ExecuteReader();



            while (mdr.Read())
            {
                string sName = mdr.GetString("username");
                comboBox1.Items.Add(sName);
            }


            connection.Close();
        }

        void FillTotal()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;

            connection.Open();
            string query = "SELECT SUM(CAST(Total AS UNSIGNED)) FROM purchase_item";


            command = new MySqlCommand(query, connection);


            Object result = command.ExecuteScalar();

            textBox5.Text = Convert.ToString(result);

            connection.Close();
        }



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;
            MySqlDataReader mdr;

            connection.Open();

            string selectQuery = "SELECT * FROM purchase_item  WHERE ItemCode = '" + txtItemCode.Text + "';";
            command = new MySqlCommand(selectQuery, connection);
            mdr = command.ExecuteReader();

            if (mdr.Read())
            {
                MessageBox.Show("This item has already added!");
            }
            else
            {
                int quantity = int.Parse(txtNoPieces.Text);
                int price = int.Parse(txtPrice.Text);

                int tot = quantity * price;
                int tot_payment;

                if (comboBoxDiscount.SelectedIndex == 1)
                {
                    tot_payment = tot - ((tot * 5) / 100);
                }else if(comboBoxDiscount.SelectedIndex == 2)
                {
                    tot_payment = tot - ((tot * 10) / 100);
                }
                else
                {
                    tot_payment = tot;
                }

                string query = "INSERT INTO  purchase_item(ItemCode,Item_Name,No_of_Pieces,Price,Discount,Total,CustomerID) VALUES('" + txtItemCode.Text + "','" + txtItemName.Text + "','" + txtNoPieces.Text + "','" + txtPrice.Text + "','" + comboBoxDiscount.Text + "','"+tot_payment+"','" + txtCustomerID.Text + "')";
                MySqlConnection databaseConnection = new MySqlConnection(MyConString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    databaseConnection.Close();
                    FillTotal();

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

        private void button2_Click(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;
            MySqlDataReader mdr;

            connection.Open();

            string selectQuery = "SELECT SUM(total) FROM purchase_item  WHERE ItemCode = '" + txtItemCode.Text + "';";
            command = new MySqlCommand(selectQuery, connection);
            mdr = command.ExecuteReader();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SalesForEmployees_Load(object sender, EventArgs e)
        {

        }
    }
}
