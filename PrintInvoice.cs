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
    public partial class PrintInvoice : Form
    {
        public PrintInvoice()
        {
            InitializeComponent();
            FillcomboSales();
            FillcomboCashier();
            LoadTable();
        }

        void FillcomboSales()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;


            MySqlDataReader mdr;



            connection.Open();
            string query = "Select * from suppliers";

   
            command = new MySqlCommand(query, connection);

      
            mdr = command.ExecuteReader();
           
         

            while (mdr.Read())
            {
                string sName = mdr.GetString("supplier_name");
                comboBox1.Items.Add(sName);
            }

 
            connection.Close();
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
                comboBox2.Items.Add(sName);
            }


            connection.Close();
        }

        private void LoadTable()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;
            MySqlDataReader mdr;


            string query = "Select ItemCode,Item_Name,No_of_Pieces,Price,Discount,Total from purchase_item WHERE CustomerID='" + textBox1.Text+ "'";
            command = new MySqlCommand(query, connection);

            DataTable dtable = new DataTable();
            connection.Open();

            mdr = command.ExecuteReader();

            while (mdr.Read())
            {
                dtable.Load(mdr);
            }


            connection.Close();
            dataGridView1.DataSource = dtable;
        }

        

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                   
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SalesManagement sales_obj = new SalesManagement();
            this.Hide();
            sales_obj.ShowDialog();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;
            MySqlDataReader mdr;

            connection.Open();
            string query = "Select Name from customer";
            command = new MySqlCommand(query, connection);

            mdr = command.ExecuteReader();

            while (mdr.Read())
            {
                textBox2.Text = mdr.GetValue(0).ToString();
            }
            connection.Close();
            LoadTable();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
