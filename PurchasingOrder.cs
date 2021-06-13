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
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace retail_system
{
    public partial class PurchasingOrder : Form
    {
        public PurchasingOrder()
        {
            InitializeComponent();
            FillcomboCashier();
        }

        int total_pay = 0;
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;

        void FillcomboCashier()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;


            MySqlDataReader mdr;



            connection.Open();
            string query = "Select Name from employee WHERE Designation='Cashier'";


            command = new MySqlCommand(query, connection);


            mdr = command.ExecuteReader();



            while (mdr.Read())
            {
               // string sName = mdr.GetString("Name");
                //comboBox1.Items.Add(sName);

                comboBox1.Text = mdr.GetValue(0).ToString();
            }


            connection.Close();
        }

        private void LoadTable()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection refresh_connection = new MySqlConnection(MyConString);
            string refreshQuery = "SELECT * FROM purchasing_goods WHERE PO_No='" + txtPO.Text + "'";
            MySqlCommand commandDatabase = new MySqlCommand(refreshQuery, refresh_connection);
            DataTable dtable = new DataTable();
            refresh_connection.Open();


            MySqlDataReader mdr = commandDatabase.ExecuteReader();


            dtable.Load(mdr);


            refresh_connection.Close();
            dataGridView1.DataSource = dtable;


        }

        private void PO()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            


                string query = "INSERT INTO  purchasing_order(PO_No,Cashier,Supplier,Date,Required_Date,Remarks) VALUES('" + txtPO.Text + "','" + comboBox1.Text + "','" + txtSupName.Text + "','" + dateDate.Text + "','" + dateRequired.Text + "','" + richtxtRemarks.Text + "')";
                MySqlConnection databaseConnection = new MySqlConnection(MyConString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    databaseConnection.Close();
              

                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }

           
        }

        private void PurchasingOrder_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboDevice.Items.Add(filterInfo.Name);
            cboDevice.SelectedIndex = 0;
        }

        

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;


            MySqlDataReader mdr;
            connection.Open();
            string query = "Select supplier_name from suppliers WHERE supplier_id='" + txtSupID.Text + "'";


            command = new MySqlCommand(query, connection);


            mdr = command.ExecuteReader();
            while (mdr.Read())
            {
                txtSupName.Text = mdr.GetValue(0).ToString();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode((Bitmap)pictureBox2.Image);
            if (result != null)
            {
                textBox8.Text = result.ToString();
                timer1.Stop();
                if (captureDevice.IsRunning)
                    captureDevice.Stop();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame; ;
            captureDevice.Start();
            timer1.Start();
        }
        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox2.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void PurchasingOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (captureDevice.IsRunning)
                captureDevice.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;
            MySqlDataReader mdr;

            connection.Open();

            string selectQuery = "SELECT * FROM purchasing_goods WHERE PO_No = '" + txtPO.Text + "';";
            command = new MySqlCommand(selectQuery, connection);
            mdr = command.ExecuteReader();

            if (mdr.Read())
            {
                MessageBox.Show("Item already added!");
            }
            else
            {


                string query = "INSERT INTO  purchasing_goods(PO_No,Name,Price,Pieces,Total) VALUES('" + txtPO.Text + "','" + textBox8.Text + "','" + txtPrice.Text + "','" + txtNoPieces.Text + "','" + txtTot.Text + "')";
                MySqlConnection databaseConnection = new MySqlConnection(MyConString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    databaseConnection.Close();
                    LoadTable();
                    payAmountLabel.Text = total_pay.ToString();
                    PO();
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }

            }

            connection.Close();
        }

        private void payAmountLabel_Click(object sender, EventArgs e)
        {
            
            
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection1 = new MySqlConnection(MyConString);
            string total = "SELECT Price FROM items WHERE Item_Name='" + textBox8.Text + "'";
            MySqlCommand commandDatabase1 = new MySqlCommand(total, connection1);

            connection1.Open();


            MySqlDataReader mdr1 = commandDatabase1.ExecuteReader();

            while (mdr1.Read())
            {
                txtPrice.Text = mdr1.GetValue(0).ToString();

                
            }


            connection1.Close();
        }

        private void txtNoPieces_TextChanged(object sender, EventArgs e)
        {
            int price = int.Parse(txtPrice.Text);
            int pieces = int.Parse(txtNoPieces.Text);

            int tot = price * pieces;
            txtTot.Text = tot.ToString();
            total_pay = total_pay + tot;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GRN obj = new GRN();
            obj.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InventoryManagement obj = new InventoryManagement();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }
    }
}
