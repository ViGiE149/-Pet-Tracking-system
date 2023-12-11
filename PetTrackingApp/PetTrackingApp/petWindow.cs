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
    public partial class petWindow : Form
    {
        string gender = "";
        String ower = "";
        String owner = "";
        String name = "";

        public petWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            OwnerDesk page = new OwnerDesk();
            page.Show();
        }



        private void petAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(txtPetName.Text) || string.IsNullOrEmpty(txtPetID.Text) || string.IsNullOrEmpty(txtPetColor.Text) || string.IsNullOrEmpty(txtTypeofPet.Text) || (malePet.Checked == false && femalePet.Checked == false)))
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

                            // Insert pet into Pets table
                            command.CommandText = "INSERT INTO Pets (Pet_ID, Owner_ID, Name, Pet_Type, Breed, Colour, Gender, Date_Of_Birth, status)" +
                                                  "VALUES (?, ?, ?, ?, ?, ?, ?, ?, 'good');";
                            command.Parameters.AddWithValue("?", txtPetID.Text);
                            command.Parameters.AddWithValue("?", owner);
                            command.Parameters.AddWithValue("?", txtPetName.Text);
                            command.Parameters.AddWithValue("?", txtTypeofPet.Text);
                            command.Parameters.AddWithValue("?", txtPetBreed.Text);
                            command.Parameters.AddWithValue("?", txtPetColor.Text);
                            command.Parameters.AddWithValue("?", gender);
                            command.Parameters.AddWithValue("?", txtPetDOB.Value.ToString());

                            command.ExecuteNonQuery();

                            // Remove assigned pet ID from Assigned_Pet_IDs table
                            command.CommandText = "DELETE FROM Assigned_Pet_IDs WHERE petID = ? AND OwerID = ?";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?", txtPetID.Text);
                            command.Parameters.AddWithValue("?", owner);

                            command.ExecuteNonQuery();

                            con.Close();
                            MessageBox.Show("Pet registered successfully!");
                            // Clear form fields
                            txtPetName.Text = "";
                            txtTypeofPet.Text = "";
                            txtPetColor.Text = "";
                            txtPetBreed.Text = "";
                            gender = "";
                            txtPetID.Text = "";
                            femalePet.Checked = false;
                            malePet.Checked = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Fill in all the spaces!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


        }

        private void button3_Click(object sender, EventArgs e)
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

                        // Get petID from Assigned_Pet_IDs table based on owner ID
                        command.CommandText = "SELECT TOP 1 petID FROM Assigned_Pet_IDs WHERE OwerID = ?;";
                        command.Parameters.AddWithValue("?", owner);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                name = reader["petID"].ToString();
                                MessageBox.Show(name);
                            }
                        }

                        txtPetID.Text = name;
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void malePet_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Male";
          
        }

        private void femalePet_CheckedChanged(object sender, EventArgs e)
        {
            gender = "female";
        }

        private void petExit_Click(object sender, EventArgs e)
        {
            txtPetBreed.Text = "";
            txtPetColor.Text = "";
            txtTypeofPet.Text = "";
            txtPetID.Text = "";
            txtPetName.Text = "";
        }
    }
}
