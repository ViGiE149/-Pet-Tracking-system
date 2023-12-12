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
    public partial class UserUpdate : Form
    {
        String OWNER = "";
        String owner = "";
         string  connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
        
        public UserUpdate()
        {
            InitializeComponent();

            try
            {
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);
                {
                    // Get ID_Number from Logged_In table
                    owner = dbHelper.ExecuteScalar("SELECT ID_Number FROM Logged_In;").ToString();

                    // Select specific data from owners table using the retrieved owner ID
                    string query = "SELECT ID_number, Name, Surname, Gender, Address, Contact_No, [Password] FROM owners WHERE ID_number = ?";
                    OleDbParameter parameter = new OleDbParameter("?", owner);

                    DataTable table = dbHelper.ExecuteDataTable(query, parameter);

                    dataGridView1.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtUsername.Text = row.Cells["Surname"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtContact.Text = row.Cells["Contact_no"].Value.ToString();
                txtPassword.Text = row.Cells["password"].Value.ToString();

            }

        }

      

        private void buttonUpdate_Click(object sender, EventArgs e)
        {


            try
            {
                // Use the DatabaseHelper class
               
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                // Retrieve OWNER from the database
                string querySelectOwner = "SELECT ID_Number FROM Logged_In";
                string OWNER = dbHelper.ExecuteScalar(querySelectOwner).ToString();

                // Update the owner information
                string queryUpdateOwner = "UPDATE owners SET Name=?, Surname=?, Address=?, Contact_No=?, [Password]=? WHERE ID_Number=?";
                OleDbParameter[] updateParameters =
                {
        new OleDbParameter("Name", txtName.Text),
        new OleDbParameter("Surname", txtUsername.Text),
        new OleDbParameter("Address", txtAddress.Text),
        new OleDbParameter("Contact_No", txtContact.Text),
        new OleDbParameter("Password", txtPassword.Text),
        new OleDbParameter("ID_Number", OWNER)
    };

                dbHelper.ExecuteNonQuery(queryUpdateOwner, updateParameters);

                MessageBox.Show("Updated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OwnerDesk ow = new OwnerDesk();
            ow.Show();
            this.Hide();
        }
    }
}
