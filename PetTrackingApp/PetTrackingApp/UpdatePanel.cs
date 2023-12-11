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
        String owner="";
        public UpdatePanel()
        {
            InitializeComponent();

            try
            {
                using (OleDbConnection con = new OleDbConnection())
                {
                    con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
                    con.Open();

                    using (OleDbCommand command = new OleDbCommand())
                    {
                        command.Connection = con;

                        // Get owner ID from Logged_In table
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

                        // Retrieve pets for the specific owner using parameterized query
                        command.CommandText = "SELECT * FROM Pets WHERE Owner_ID = ?";
                        command.Parameters.AddWithValue("?", owner);

                        command.ExecuteNonQuery();

                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        dataGridView1.DataSource = table;

                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

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
                using (OleDbConnection con = new OleDbConnection())
                {
                    con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
                    con.Open();

                    using (OleDbCommand command = new OleDbCommand())
                    {
                        command.Connection = con;

                        // Use parameterized query to update Pets table
                        command.CommandText = "UPDATE Pets SET Name = ?, Colour = ? WHERE Pet_ID = ?";
                        command.Parameters.AddWithValue("?", txtPetName.Text);
                        command.Parameters.AddWithValue("?", txtPetColor.Text);
                        command.Parameters.AddWithValue("?", txtPetID.Text);

                        command.ExecuteNonQuery();

                        con.Close();

                        MessageBox.Show("Updated successfully");
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
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
            con.Open();

            try
            {
                

                OleDbCommand command = new OleDbCommand();
                command.Connection = con;

                command.CommandText = "DELETE * FROM PetS WHERE Pet_ID = '"+txtPetID.Text+"'; ";
                command.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Deleted successfully");
 
            }
            catch (Exception EX)
            {
                MessageBox.Show("error " + EX);
            }

            con.Close();
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
    }
}
