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
    public partial class FormPrint : Form
    {
        public FormPrint()
        {
            InitializeComponent();
            LoadTable();
            LoadLabels();
        }

        private void LoadTable()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection refresh_connection = new MySqlConnection(MyConString);
            string refreshQuery = "SELECT * FROM temp_purchase_items";
            MySqlCommand commandDatabase = new MySqlCommand(refreshQuery, refresh_connection);
            DataTable dtable = new DataTable();
            refresh_connection.Open();


            MySqlDataReader mdr = commandDatabase.ExecuteReader();


            dtable.Load(mdr);


            refresh_connection.Close();
            dataGridView1.DataSource = dtable;


        }

        private void LoadLabels()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection1 = new MySqlConnection(MyConString);
            string total = "SELECT SUM(Total),SUM(Amount_Received),SUM(Balance) FROM temp_purchase_items";
            MySqlCommand commandDatabase1 = new MySqlCommand(total, connection1);

            connection1.Open();


            MySqlDataReader mdr1 = commandDatabase1.ExecuteReader();

            while (mdr1.Read())
            {
                payAmountLabel.Text = mdr1.GetValue(0).ToString();
                amountReceiveLabel.Text = mdr1.GetValue(1).ToString();
                balanceLabel.Text = mdr1.GetValue(2).ToString();
            }


            connection1.Close();
        }

        private void DeleteTempTable()
        {
            string MyConString = "datasource=localhost;port=3306;username=root;password=;database=retail_system";
            MySqlConnection connection1 = new MySqlConnection(MyConString);
            string total = "DELETE FROM temp_purchase_items";
            MySqlCommand commandDatabase1 = new MySqlCommand(total, connection1);

            connection1.Open();


            MySqlDataReader mdr1 = commandDatabase1.ExecuteReader();


            connection1.Close();
        }

        Bitmap bmp;

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            bmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            Graphics mg = Graphics.FromImage(bmp);
            mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            printPreviewDialog1.ShowDialog();

            DeleteTempTable();

            SalesManagement obj = new SalesManagement();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }
    }
}
