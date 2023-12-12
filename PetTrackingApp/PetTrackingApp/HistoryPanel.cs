using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetTrackingApp
{
    public partial class HistoryPanel : Form
    {
      
        public HistoryPanel()
        {
            InitializeComponent();

            try
            {
                string owner;

                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                // Get ID_Number from Logged_In table
                owner = dbHelper.ExecuteScalar("SELECT ID_Number FROM Logged_In;").ToString();

                // Select data from Pet_Transfer table for the current owner
                DataTable dataTable = dbHelper.ExecuteDataTable("SELECT * FROM Pet_Transfer WHERE current_ownerID = @owner",
                    new OleDbParameter("@owner", owner));

                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            OwnerDesk page = new OwnerDesk();
            page.Show();
        }

        
    }
}
