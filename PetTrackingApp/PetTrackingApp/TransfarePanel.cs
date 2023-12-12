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
        string owner = "";
    
      string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";

        public TransfarePanel()
        {
          
            InitializeComponent();

            try
            {
             

              
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                // SELECT ID_Number FROM Logged_In
                owner = dbHelper.ExecuteScalar("SELECT ID_Number FROM Logged_In;").ToString();

                txtPrevious.Text = owner;
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

        private void petTransfareBtn_Click(object sender, EventArgs e)
        {
            try
            {
                bool idValid = false;
                bool petValid = false;
                bool check = false;

              
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                // Check if pet ID exists
                idValid = dbHelper.ExecuteScalar("SELECT COUNT(*) FROM Pets WHERE Pet_ID = @Pet_ID",
                                                 new OleDbParameter("@Pet_ID", txtPetID.Text)).ToString() == "1";

                if (idValid)
                {
                    // Check if pet ID is valid
                    petValid = dbHelper.ExecuteScalar("SELECT COUNT(*) FROM Pets WHERE Pet_ID = @Pet_ID",
                                                      new OleDbParameter("@Pet_ID", txtPetID.Text)).ToString() == "1";

                    if (petValid)
                    {
                        // Check if the pet has already been transferred to the new owner
                        int transferCount = dbHelper.ExecuteScalar("SELECT COUNT(*) FROM Pet_Transfer WHERE current_ownerID = @CurrentOwnerID AND new_ownerID = @NewOwnerID AND pet_ID = @PetID",
                                                                  new OleDbParameter("@CurrentOwnerID", txtPrevious.Text),
                                                                  new OleDbParameter("@NewOwnerID", txtNewOwner.Text),
                                                                  new OleDbParameter("@PetID", txtPetID.Text));

                        if (transferCount > 0)
                        {
                            MessageBox.Show("This pet has already been transferred to this ID");
                            check = false;
                        }
                        else
                        {
                            check = true;
                        }

                        if (check)
                        {
                            // Insert into Pet_Transfer
                            dbHelper.ExecuteNonQuery("INSERT INTO Pet_Transfer (pet_ID, current_ownerID, new_ownerID, reason, date) VALUES (@PetID, @CurrentOwnerID, @NewOwnerID, @Reason, @Date)",
                                                      new OleDbParameter("@PetID", txtPetID.Text),
                                                      new OleDbParameter("@CurrentOwnerID", txtPrevious.Text),
                                                      new OleDbParameter("@NewOwnerID", txtNewOwner.Text),
                                                      new OleDbParameter("@Reason", txtReason.Text),
                                                      new OleDbParameter("@Date", txtPetDOB.Value.ToString()));

                            // Update Pets table
                            dbHelper.ExecuteNonQuery("UPDATE Pets SET Owner_ID = @NewOwnerID WHERE Pet_ID = @PetID",
                                                      new OleDbParameter("@NewOwnerID", txtNewOwner.Text),
                                                      new OleDbParameter("@PetID", txtPetID.Text));

                            MessageBox.Show($"Pet {txtPetID.Text} successfully transferred from {txtPrevious.Text} to {txtNewOwner.Text}");
                        }
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
