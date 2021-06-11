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

namespace retail_system
{
    public partial class UserRegistration : Form
    {
        public UserRegistration()
        {
            InitializeComponent();
            
        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;


        private void UserRegistration_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboDevice.Items.Add(filterInfo.Name);
            cboDevice.SelectedIndex = 0;
        }

        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame; ;
            captureDevice.Start();
        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void UserRegistration_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (captureDevice.IsRunning==true)
                captureDevice.Stop();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox1.Image;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command;
            MySqlDataReader mdr;

            connection.Open();

            string selectQuery = "SELECT * FROM employee WHERE Name = '" + txtName.Text + "';";
            command = new MySqlCommand(selectQuery, connection);
            mdr = command.ExecuteReader();

            if (mdr.Read())
            {
                MessageBox.Show("Employee already registered!");
            }
            else
            {                
                try
                {
                    MySqlConnection databaseConnection = new MySqlConnection(MyConString);
                    databaseConnection.Open();

                   // string query = "INSERT INTO  employee(Employee_ID,Name,Designation,Age,Mobile,Identity_Card,EMail,Birthday,Address,Emergency,Notes,Image) VALUES('" + txtEmpID.Text + "','" + txtName.Text + "','" + comboBox1.Text + "','" + textAge.Text + "','" + txtMobile.Text + "','" + txtID.Text + "','" + txtMail.Text + "','" + dateBirthday.Text + "','" + richTextAddress.Text + "''" + textEmergency.Text + "','" + richTextNotes.Text + "',@pic)";
                    MySqlCommand commandDatabase = new MySqlCommand("INSERT INTO  employee(Employee_ID,Name,Designation,Age,Mobile,Identity_Card,EMail,Birthday,Address,Emergency,Notes,Image) VALUES('" + txtEmpID.Text + "','" + txtName.Text + "','" + comboBox1.Text + "','" + textAge.Text + "','" + txtMobile.Text + "','" + txtID.Text + "','" + txtMail.Text + "','" + dateBirthday.Text + "','" + richTextAddress.Text + "','" + textEmergency.Text + "','" + richTextNotes.Text + "',@Pic)", databaseConnection);
                    MemoryStream stream = new MemoryStream();
                    pictureBox2.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] pic = stream.ToArray();
                    commandDatabase.Parameters.AddWithValue("Pic", pic);
                    // MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    commandDatabase.ExecuteNonQuery();
                    databaseConnection.Close();
                    
                    MessageBox.Show("Employee Successfully Registered!");
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }
                
            }

            connection.Close();
        }
    }
}
