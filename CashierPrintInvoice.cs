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
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;


namespace retail_system
{
    public partial class CashierPrintInvoice : Form
    {
        public CashierPrintInvoice()
        {
            InitializeComponent();
            FillcomboSales();
            FillcomboCashier();
        }

        int tot_pay;

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;

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
            MySqlConnection refresh_connection = new MySqlConnection(MyConString);
            string refreshQuery = "SELECT * FROM temp_purchase_items WHERE Item_Name='" + textBox3.Text + "'";
            MySqlCommand commandDatabase = new MySqlCommand(refreshQuery, refresh_connection);
            DataTable dtable = new DataTable();
            refresh_connection.Open();


            MySqlDataReader mdr = commandDatabase.ExecuteReader();


            dtable.Load(mdr);


            refresh_connection.Close();
            dataGridView1.DataSource = dtable;


        }

        private void PurchaseItemRecords()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";



            string query = "INSERT INTO  purchase_item(Item_Name,No_of_Pieces,Price,Discount,Total,Amount_Received,Balance,Cashier) VALUES('" + textBox3.Text + "','" + txtNoPieces.Text + "','" + txtPrice.Text + "','" + comboBoxDiscount.Text + "','" + tot_pay + "','" + textBox2.Text + "','" + textBox4.Text + "','" + comboBox2.Text + "')";
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            BarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode((Bitmap)pictureBox1.Image);
            if (result != null)
            {
                textBox3.Text = result.ToString();
                timer2.Stop();
                if (captureDevice.IsRunning)
                    captureDevice.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode((Bitmap)pictureBox1.Image);
            if (result != null)
            {
                textBox1.Text = result.ToString();
                timer1.Stop();
                if (captureDevice.IsRunning)
                    captureDevice.Stop();
            }
        }

        private void CashierPrintInvoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (captureDevice.IsRunning)
                captureDevice.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame; ;
            captureDevice.Start();
            timer1.Start();
        }

        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame; ;
            captureDevice.Start();
            timer2.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CashierSalesManagement sales_obj = new CashierSalesManagement();
            this.Hide();
            sales_obj.ShowDialog();
            this.Close();
        }

        private void CashierPrintInvoice_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboDevice.Items.Add(filterInfo.Name);
            cboDevice.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;
            MySqlDataReader mdr;

            connection.Open();

            string selectQuery = "SELECT * FROM temp_purchase_items WHERE Item_Name = '" + textBox3.Text + "';";
            command = new MySqlCommand(selectQuery, connection);
            mdr = command.ExecuteReader();

            if (mdr.Read())
            {
                MessageBox.Show("Binding already added!");
            }
            else
            {
                int quantity = int.Parse(txtNoPieces.Text);
                int price = int.Parse(txtPrice.Text);

                int grand_total = 0;
                int grand_amount = 0;
                int grand_balance = 0;

                int total = quantity * price;
                int payment;

                if (comboBoxDiscount.SelectedIndex == 1)
                {
                    payment = total - ((total * 5) / 100);
                    tot_pay = payment;
                }
                else if (comboBoxDiscount.SelectedIndex == 2)
                {
                    payment = total - ((total * 10) / 100);
                    tot_pay = payment;
                }
                else
                {
                    payment = total;
                    tot_pay = payment;
                }

                string query = "INSERT INTO  temp_purchase_items(Item_Name,No_of_Piece,Price,Discount,Total,Amount_Received,Balance,Cashier) VALUES('" + textBox3.Text + "','" + txtNoPieces.Text + "','" + txtPrice.Text + "','" + comboBoxDiscount.Text + "','" + payment + "','" + textBox2.Text + "','" + textBox4.Text + "','" + comboBox2.Text + "')";
                MySqlConnection databaseConnection = new MySqlConnection(MyConString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    databaseConnection.Close();
                    LoadTable();

                    grand_total += payment;
                    grand_amount += int.Parse(textBox2.Text);
                    grand_balance += int.Parse(textBox4.Text);

                    payAmountLabel.Text = grand_total.ToString();
                    amountReceiveLabel.Text = grand_amount.ToString();
                    balanceLabel.Text = grand_balance.ToString();
                    PurchaseItemRecords();
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }

            }

            connection.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;


            MySqlDataReader mdr;
            connection.Open();
            string query = "Select Price from items WHERE Item_Name='" + textBox3.Text + "'";


            command = new MySqlCommand(query, connection);


            mdr = command.ExecuteReader();
            while (mdr.Read())
            {
                txtPrice.Text = mdr.GetValue(0).ToString();
            }

            connection.Close();
        }

        private void comboBoxDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            int quantity = int.Parse(txtNoPieces.Text);
            int price = int.Parse(txtPrice.Text);

            int total = quantity * price;
            int payment;

            if (comboBoxDiscount.SelectedIndex == 1)
            {
                payment = total - ((total * 5) / 100);
            }
            else if (comboBoxDiscount.SelectedIndex == 2)
            {
                payment = total - ((total * 10) / 100);
            }
            else
            {
                payment = total;
            }
            textBox5.Text = payment.ToString();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int payment = int.Parse(textBox5.Text);
            int amount_rec = int.Parse(textBox2.Text);
            int balance = amount_rec - payment;
            textBox4.Text = balance.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormPrint form_obj = new FormPrint();
            this.Hide();
            form_obj.ShowDialog();
            this.Close();
        }
    }
}
