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
    public partial class TransfarePanel : Form
    {
        String owner = "";
        bool check = false;
        String newOwenr;
        String petID;
        bool petValid = false;
        bool idValid = false;

        public TransfarePanel()
        {
          
            InitializeComponent();

            try
            {

               OleDbConnection con = new OleDbConnection();
                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
                con.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = con;

                command.CommandText = "SELECT ID_Number FROM Logged_In;";
                OleDbDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    owner = reader["ID_Number"].ToString();

                }

               txtPrevious.Text = owner;
                con.Close();
               
            }
            catch (Exception EX)
            {
                MessageBox.Show("error " + EX);
            }

               

        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            OwnerDesk page = new OwnerDesk();
            page.Show();
        }

        private void petTransfareBtn_Click(object sender, EventArgs e)
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

                        if (string.IsNullOrEmpty(txtNewOwner.Text) || string.IsNullOrEmpty(txtPetID.Text))
                        {
                            MessageBox.Show("Error: Make sure new owner ID and pet ID are filled!");
                        }
                        else
                        {
                            // Check if pet ID exists
                            command.CommandText = "SELECT Pet_ID FROM Pets WHERE Pet_ID = ?";
                            command.Parameters.AddWithValue("?", txtPetID.Text);

                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    idValid = true;
                                }
                            }

                            con.Close();
                            con.Open();

                            // Check if pet ID is valid
                            command.CommandText = "SELECT Pet_ID FROM Pets WHERE Pet_ID = ?";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?", txtPetID.Text);

                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    petValid = true;
                                }
                            }

                            con.Close();
                            con.Open();

                            if (idValid)
                            {
                                if (petValid)
                                {
                                    // Check if the pet has already been transferred to the new owner
                                    command.CommandText = "SELECT * FROM Pet_Transfer WHERE current_ownerID = ? AND new_ownerID = ? AND pet_ID = ?";
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("?", txtPrevious.Text);
                                    command.Parameters.AddWithValue("?", txtNewOwner.Text);
                                    command.Parameters.AddWithValue("?", txtPetID.Text);

                                    using (OleDbDataReader reader = command.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            MessageBox.Show("This pet has already been transferred to this ID");
                                            check = false;
                                        }
                                        else
                                        {
                                            check = true;
                                        }
                                    }

                                    con.Close();

                                    if (check)
                                    {
                                        con.Open();

                                        // Insert into Pet_Transfer
                                        command.CommandText = "INSERT INTO Pet_Transfer (pet_ID, current_ownerID, new_ownerID, reason, date) VALUES (?, ?, ?, ?, ?)";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("?", txtPetID.Text);
                                        command.Parameters.AddWithValue("?", txtPrevious.Text);
                                        command.Parameters.AddWithValue("?", txtNewOwner.Text);
                                        command.Parameters.AddWithValue("?", txtReason.Text);
                                        command.Parameters.AddWithValue("?", txtPetDOB.Value.ToString());

                                        command.ExecuteNonQuery();

                                        // Update Pets table
                                        command.CommandText = "UPDATE Pets SET Owner_ID = ? WHERE Pet_ID = ?";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("?", txtNewOwner.Text);
                                        command.Parameters.AddWithValue("?", txtPetID.Text);

                                        command.ExecuteNonQuery();

                                        MessageBox.Show($"Pet {txtPetID.Text} successfully transferred from {txtPrevious.Text} to {txtNewOwner.Text}");
                                    }

                                    con.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Pet ID is invalid!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("You cannot transfer to an ID that is not registered on the system!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void txtReason_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
