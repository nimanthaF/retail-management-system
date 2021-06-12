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
    public partial class GRN : Form
    {
        public GRN()
        {
            InitializeComponent();
            LoadTable();
            GetPORecords();
        }

        private void LoadTable()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection refresh_connection = new MySqlConnection(MyConString);
            string refreshQuery = "SELECT * FROM purchasing_goods";
            MySqlCommand commandDatabase = new MySqlCommand(refreshQuery, refresh_connection);
            DataTable dtable = new DataTable();
            refresh_connection.Open();


            MySqlDataReader mdr = commandDatabase.ExecuteReader();


            dtable.Load(mdr);


            refresh_connection.Close();
            dataGridView1.DataSource = dtable;


        }

        private void GetPORecords()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection1 = new MySqlConnection(MyConString);
            string total = "SELECT PO_No,Supplier,Date,Required_Date FROM purchasing_order ";
            MySqlCommand commandDatabase1 = new MySqlCommand(total, connection1);

            connection1.Open();


            MySqlDataReader mdr1 = commandDatabase1.ExecuteReader();

            while (mdr1.Read())
            {
                labelPO.Text = mdr1.GetValue(0).ToString();
                labelSupplier.Text = mdr1.GetValue(1).ToString();
                labelDate.Text = mdr1.GetValue(2).ToString();
                labelRequired.Text = mdr1.GetValue(3).ToString();
            }


            connection1.Close();
        }

        private void GRN_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        Bitmap bmp;
        private void button3_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            bmp = new Bitmap(this.Size.Width, 730, g);
            Graphics mg = Graphics.FromImage(bmp);
            mg.CopyFromScreen(this.Location.X, this.Location.Y, 0,0, this.Size);
            printPreviewDialog1.ShowDialog();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }
    }
}
