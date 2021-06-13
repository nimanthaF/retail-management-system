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
using System.IO;
using ZXing;
using System.Data.SqlClient;

namespace retail_system
{

    
    public partial class UserUpdate : Form
    {
        public UserUpdate()
        {
            InitializeComponent();
        }

       

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;

        private void button2_Click(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            
               
                try
                {
                    MySqlConnection databaseConnection = new MySqlConnection(MyConString);
                    databaseConnection.Open();

                   
                    MySqlCommand commandDatabase = new MySqlCommand("UPDATE employee  SET Name='" + txtName.Text + "',Designation='" + comboBox1.Text + "',Age='" + textAge.Text + "',Mobile='" + txtMobile.Text + "',Identity_Card='" + txtID.Text + "',EMail='" + txtMail.Text + "',Birthday='" + dateBirthday.Text + "',Address='" + richTextAddress.Text + "',Emergency='" + textEmergency.Text + "',Notes='" + richTextNotes.Text + "' WHERE Employee_ID='" + txtEmpID.Text+"' ;" , databaseConnection);
                  
                    
                    commandDatabase.ExecuteNonQuery();
                    databaseConnection.Close();

                    MessageBox.Show("Employee Successfully Updated!");
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
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
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void UserUpdate_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboDevice.Items.Add(filterInfo.Name);
            cboDevice.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode((Bitmap)pictureBox1.Image);
            if (result != null)
            {
                txtName.Text = result.ToString();
                timer1.Stop();
                if (captureDevice.IsRunning)
                    captureDevice.Stop();
            }
        }

        private void UserUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (captureDevice.IsRunning)
                captureDevice.Stop();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;


            MySqlDataReader mdr;
            connection.Open();
            string query = "Select Employee_ID,Designation,Age,Mobile,Identity_Card,EMail,Birthday,Address,Emergency,Notes,Image from employee WHERE Name='" + txtName.Text+"'";


            command = new MySqlCommand(query, connection);


            mdr = command.ExecuteReader();
            while (mdr.Read())
            {
                txtEmpID.Text = mdr.GetValue(0).ToString();
                comboBox1.Text = mdr.GetValue(1).ToString();
                textAge.Text = mdr.GetValue(2).ToString();
                txtMobile.Text = mdr.GetValue(3).ToString();
                txtID.Text = mdr.GetValue(4).ToString();
                txtMail.Text = mdr.GetValue(5).ToString();
                dateBirthday.Text = mdr.GetValue(6).ToString();
                richTextAddress.Text = mdr.GetValue(7).ToString();
                textEmergency.Text = mdr.GetValue(8).ToString();
                richTextNotes.Text = mdr.GetValue(9).ToString();
                pictureBox2.Image= ByteArrayToImage((byte[])(mdr.GetValue(10)));
            }
        }

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SysAdmin obj = new SysAdmin();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtEmpID.Text = "";
            txtName.Text = "";
            comboBox1.Text = "";
            textAge.Text = "";
            txtMobile.Text = "";
            txtID.Text = "";
            txtMail.Text = "";
            dateBirthday.Text = "";
            richTextAddress.Text = "";
            textEmergency.Text = "";
            richTextNotes.Text = "";
            pictureBox2.Image = null;
        }
    }
}
