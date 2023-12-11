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
        public StatusPanel()
        {
            String owner = "";
            InitializeComponent();







            try
            {
                using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;"))
                {
                    con.Open();

                    using (OleDbCommand command = new OleDbCommand())
                    {
                        command.Connection = con;

                        // Get ID_Number from Logged_In table
                        command.CommandText = "SELECT ID_Number FROM Logged_In;";
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                owner = reader["ID_Number"].ToString();
                            }
                        }

                        con.Close();
                        con.Open();

                        // Select pet data from PetS table for the current owner
                        command.CommandText = "SELECT pet_ID, name, status, statusByVet FROM PetS WHERE Owner_ID = '" + owner + "';";
                        command.ExecuteNonQuery();

                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            dataGridView1.DataSource = table;
                        }

                        con.Close();
                    }
                }
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
                using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;"))
                {
                    con.Open();

                    using (OleDbCommand command = new OleDbCommand())
                    {
                        command.Connection = con;

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
                    }
                }
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
