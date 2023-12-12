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
                    string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
                    DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                    // Get owner ID from Logged_In table
                    owner = dbHelper.ExecuteScalar("SELECT ID_Number FROM Logged_In;").ToString();

                    // Insert pet into Pets table
                    dbHelper.ExecuteNonQuery("INSERT INTO Pets (Pet_ID, Owner_ID, Name, Pet_Type, Breed, Colour, Gender, Date_Of_Birth, status)" +
                                            "VALUES (@Pet_ID, @Owner_ID, @Name, @Pet_Type, @Breed, @Colour, @Gender, @Date_Of_Birth, 'good');",
                                            new OleDbParameter("@Pet_ID", txtPetID.Text),
                                            new OleDbParameter("@Owner_ID", owner),
                                            new OleDbParameter("@Name", txtPetName.Text),
                                            new OleDbParameter("@Pet_Type", txtTypeofPet.Text),
                                            new OleDbParameter("@Breed", txtPetBreed.Text),
                                            new OleDbParameter("@Colour", txtPetColor.Text),
                                            new OleDbParameter("@Gender", gender),
                                            new OleDbParameter("@Date_Of_Birth", txtPetDOB.Value.ToString()));

                    // Remove assigned pet ID from Assigned_Pet_IDs table
                    dbHelper.ExecuteNonQuery("DELETE FROM Assigned_Pet_IDs WHERE petID = @Pet_ID AND OwerID = @Owner_ID",
                                            new OleDbParameter("@Pet_ID", txtPetID.Text),
                                            new OleDbParameter("@Owner_ID", owner));

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
               

                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                // Get owner ID from Logged_In table
                owner = dbHelper.ExecuteScalar("SELECT ID_Number FROM Logged_In;").ToString();

                // Get petID from Assigned_Pet_IDs table based on owner ID
                name = dbHelper.ExecuteScalar("SELECT TOP 1 petID FROM Assigned_Pet_IDs WHERE OwerID = @Owner_ID;",
                                              new OleDbParameter("@Owner_ID", owner)).ToString();

                txtPetID.Text = name;

                MessageBox.Show(name);
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
