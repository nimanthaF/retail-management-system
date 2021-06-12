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
    public partial class PerfromanceEvaluation : Form
    {
        public PerfromanceEvaluation()
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
            string query = "Select * from employee";


            command = new MySqlCommand(query, connection);


            mdr = command.ExecuteReader();



            while (mdr.Read())
            {
                string sName = mdr.GetString("Name");
                comboBox1.Items.Add(sName);
            }


            connection.Close();
        }

        private void LoadTable()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection refresh_connection = new MySqlConnection(MyConString);
            string refreshQuery = "SELECT * FROM purchase_item WHERE Cashier='" + comboBox1.Text + "'";
            MySqlCommand commandDatabase = new MySqlCommand(refreshQuery, refresh_connection);
            DataTable dtable = new DataTable();
            refresh_connection.Open();


            MySqlDataReader mdr = commandDatabase.ExecuteReader();


            dtable.Load(mdr);


            refresh_connection.Close();
            dataGridView1.DataSource = dtable;


        }

        private void PerfromanceEvaluation_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;


            MySqlDataReader mdr;
            connection.Open();
            string query = "Select Employee_ID,Designation,Age,Mobile,Identity_Card,EMail,Birthday,Address,Emergency,Notes from employee WHERE Name='" + comboBox1.Text + "'";


            command = new MySqlCommand(query, connection);


            mdr = command.ExecuteReader();
            while (mdr.Read())
            {
                txtEmpID.Text = mdr.GetValue(0).ToString();
                txtDesination.Text = mdr.GetValue(1).ToString();
                textAge.Text = mdr.GetValue(2).ToString();
                txtMobile.Text = mdr.GetValue(3).ToString();
                txtID.Text = mdr.GetValue(4).ToString();
                txtMail.Text = mdr.GetValue(5).ToString();
                dateBirthday.Text = mdr.GetValue(6).ToString();
                richTextAddress.Text = mdr.GetValue(7).ToString();
                textEmergency.Text = mdr.GetValue(8).ToString();
                richTextNotes.Text = mdr.GetValue(9).ToString();
                LoadTable();
            }
            
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SysAdmin obj = new SysAdmin();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }
    }
}
