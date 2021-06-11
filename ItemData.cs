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
    public partial class ItemData : Form
    {
        public ItemData()
        {
            InitializeComponent();
        }

        private void LoadTable()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection refresh_connection = new MySqlConnection(MyConString);
            string refreshQuery = "SELECT * FROM items WHERE Item_Name='" + txtItemName.Text + "'";
            MySqlCommand commandDatabase = new MySqlCommand(refreshQuery, refresh_connection);
            DataTable dtable = new DataTable();
            refresh_connection.Open();


            MySqlDataReader mdr = commandDatabase.ExecuteReader();


            dtable.Load(mdr);


            refresh_connection.Close();
            dataGridView1.DataSource = dtable;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;
            MySqlDataReader mdr;

            connection.Open();

            string selectQuery = "SELECT * FROM items WHERE Item_Name = '" + txtItemName.Text + "';";
            command = new MySqlCommand(selectQuery, connection);
            mdr = command.ExecuteReader();

            if (mdr.Read())
            {
                MessageBox.Show("Binding already added!");
            }
            else
            {
                

                string query = "INSERT INTO  items(Item_Name,Price,Pieces) VALUES('" + txtItemName.Text + "','" + txtPrice.Text + "','" + txtPieces.Text + "')";
                MySqlConnection databaseConnection = new MySqlConnection(MyConString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    databaseConnection.Close();
                    LoadTable();

                   
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }

            }

            connection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            QRgenerate obj = new QRgenerate();
            obj.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InventoryManagement obj = new InventoryManagement();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }

        private void ItemData_Load(object sender, EventArgs e)
        {

        }
    }
}
