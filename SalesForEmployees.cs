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


            MySqlDataReader mdr_cash;



            connection.Open();
            string query = "Select * from users";


            command = new MySqlCommand(query, connection);


            mdr_cash = command.ExecuteReader();



            while (mdr_cash.Read())
            {
                string sName = mdr_cash.GetString("username");
                comboBox1.Items.Add(sName);
            }

            command.Cancel();
            mdr_cash.Close();
            connection.Close();
        }

        

        private void GetPurchaseRecords()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection refresh_connection = new MySqlConnection(MyConString);
            string refreshQuery = "SELECT * FROM purchase_item WHERE Cashier='"+comboBox1.Text+"'";
            MySqlCommand commandDatabase = new MySqlCommand(refreshQuery, refresh_connection);
            DataTable dtable = new DataTable();
            refresh_connection.Open();


            MySqlDataReader mdr = commandDatabase.ExecuteReader();


            dtable.Load(mdr);

            commandDatabase.Cancel();
            mdr.Close();
            refresh_connection.Close();
            dataGridView1.DataSource = dtable;

           
        }

        private void GetTotal()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection1 = new MySqlConnection(MyConString);
            string total = "SELECT SUM(Total) FROM purchase_item WHERE Cashier='" + comboBox1.Text + "'";
            MySqlCommand commandDatabase1 = new MySqlCommand(total, connection1);

            connection1.Open();


            MySqlDataReader mdr1 = commandDatabase1.ExecuteReader();
            Object result1 = commandDatabase1.ExecuteScalar();
            textBox5.Text = Convert.ToString(result1);

            commandDatabase1.Cancel();
            mdr1.Close();
            connection1.Close();
            
        }

        private void GetAmount()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection2 = new MySqlConnection(MyConString);
            string tot_amount = "SELECT SUM(Amount_Received) FROM purchase_item WHERE Cashier='" + comboBox1.Text + "'";
            MySqlCommand commandDatabase2 = new MySqlCommand(tot_amount, connection2);

            connection2.Open();


            MySqlDataReader mdr2 = commandDatabase2.ExecuteReader();
            Object result2 = commandDatabase2.ExecuteScalar();
            textBox4.Text = Convert.ToString(result2);

            commandDatabase2.Cancel();
            mdr2.Close();
            connection2.Close();

        }

        private void GetBalance()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=car_rent";
            MySqlConnection connection3 = new MySqlConnection(MyConString);
            string tot_balance = "SELECT SUM(Blance) FROM purchase_item WHERE Cashier='" + comboBox1.Text + "'";
            MySqlCommand commandDatabase3 = new MySqlCommand(tot_balance, connection3);

            connection3.Open();


            MySqlDataReader mdr3 = commandDatabase3.ExecuteReader();
            Object result3 = commandDatabase3.ExecuteScalar();
            textBox6.Text = Convert.ToString(result3);

            commandDatabase3.Cancel();
            mdr3.Close();
            connection3.Close();

        }



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        

        private void button2_Click(object sender, EventArgs e)
        {
           
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection1 = new MySqlConnection(MyConString);
            string total = "SELECT SUM(Total),SUM(Amount_Received),SUM(Balance) FROM purchase_item WHERE Cashier='" + comboBox1.Text + "'";
            MySqlCommand commandDatabase1 = new MySqlCommand(total, connection1);

            connection1.Open();


            MySqlDataReader mdr1 = commandDatabase1.ExecuteReader();

            while (mdr1.Read())
            {
                textBox5.Text = mdr1.GetValue(0).ToString();
                textBox4.Text = mdr1.GetValue(1).ToString();
                textBox6.Text = mdr1.GetValue(2).ToString();
            }
          

            connection1.Close();
            GetPurchaseRecords();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SalesForEmployees_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SalesManagement obj = new SalesManagement();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }
    }
}
