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
    public partial class UpdatePanel : Form
    {
        string owner="";
        string  connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
        public UpdatePanel()
        {
            InitializeComponent();

            try
            {
                string owner = GetOwnerID();
                DataTable petsTable = GetPetsForOwner(owner);

                dataGridView1.DataSource = petsTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private string GetOwnerID()
        {
       
            try
            {
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);


                    owner = dbHelper.ExecuteScalar("SELECT ID_Number FROM Logged_In;").ToString();
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving owner ID: " + ex.Message);
            }
            return owner;
        }

        private DataTable GetPetsForOwner(string owner)
        {
            DataTable petsTable = new DataTable();
            try
            {
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);
                {
                    string query = "SELECT * FROM Pets WHERE Owner_ID = ?";
                    OleDbParameter parameter = new OleDbParameter("?", owner);
                    petsTable = dbHelper.ExecuteDataTable(query, parameter);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving pets: " + ex.Message);
            }
            return petsTable;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            OwnerDesk page = new OwnerDesk();
            page.Show();
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);
                {
                    // Use parameterized query to update Pets table
                    string query = "UPDATE Pets SET Name = ?, Colour = ? WHERE Pet_ID = ?";
                    OleDbParameter[] parameters = new OleDbParameter[]
                    {
            new OleDbParameter("?", txtPetName.Text),
            new OleDbParameter("?", txtPetColor.Text),
            new OleDbParameter("?", txtPetID.Text)
                    };

                    dbHelper.ExecuteNonQuery(query, parameters);

                    MessageBox.Show("Updated successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
             

                    DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                    // DELETE * FROM PetS WHERE Pet_ID = @PetID
                    dbHelper.ExecuteNonQuery("DELETE FROM PetS WHERE Pet_ID = @PetID",
                                            new OleDbParameter("@PetID", txtPetID.Text));

                    MessageBox.Show("Deleted successfully");
           
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

             if(e.RowIndex>=0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtPetID.Text = row.Cells["pet_ID"].Value.ToString();
                txtPetName.Text = row.Cells["name"].Value.ToString();
                txtPetColor.Text = row.Cells["Colour"].Value.ToString();

            }



        }

        private void UpdatePanel_Load(object sender, EventArgs e)
        {

        }
    }
}
