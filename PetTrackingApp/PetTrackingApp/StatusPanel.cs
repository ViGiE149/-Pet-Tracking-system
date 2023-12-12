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
    public partial class StatusPanel : Form
    {
        String owner = "";
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
        public StatusPanel()
        {
          
            InitializeComponent();


            try
            {
          

              
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                // Get ID_Number from Logged_In table
                owner = dbHelper.ExecuteScalar("SELECT ID_Number FROM Logged_In;").ToString();

                // Select pet data from PetS table for the current owner
                string query = "SELECT pet_ID, name, status, statusByVet FROM PetS WHERE Owner_ID = @Owner_ID";
                DataTable dataTable = dbHelper.ExecuteDataTable(query, new OleDbParameter("@Owner_ID", owner));

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

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {




            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtName.Text = row.Cells["pet_ID"].Value.ToString();
                texBoxStatus.Text = row.Cells["status"].Value.ToString();
            }


        }

       

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);
                dbHelper.GetConnection().Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = dbHelper.GetConnection();

                if (!string.IsNullOrWhiteSpace(texBoxStatus.Text) && !string.IsNullOrWhiteSpace(txtName.Text))
                {
                    command.CommandText = "UPDATE Pets SET status = '" + texBoxStatus.Text + "' WHERE Pet_ID = '" + txtName.Text + "'; ";
                    command.ExecuteNonQuery();

                    MessageBox.Show("Updated successfully");
                }
                else
                {
                    MessageBox.Show("Error! Click and update the status of a registered pet.");
                }

                dbHelper.GetConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void StatusPanel_Load(object sender, EventArgs e)
        {

        }
    }
}
