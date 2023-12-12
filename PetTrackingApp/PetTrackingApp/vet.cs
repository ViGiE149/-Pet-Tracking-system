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
    




    public partial class vet : Form
    {
        bool see = false;
        string x = "";
        string connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
            

        public vet()
        {
            InitializeComponent();
            panel5.Hide();
            // panelstatusCheck.Visible = true;
            panelstatusCheck.Hide();

            cc.Show();
            try
            {
                // Use the DatabaseHelper class
              
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                // Retrieve x from the database
                string querySelectX = "SELECT ID_Number FROM Logged_In";
               x = dbHelper.ExecuteScalar(querySelectX).ToString();

                // Retrieve owner information from the database
                string querySelectOwners = "SELECT ID_number, Name, Surname, Address, Contact_No, gender, [Password] FROM owners";
                DataTable table = dbHelper.ExecuteDataTable(querySelectOwners);

                // Display the result in dataGridView1
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(30, 30, 70);

            panelstatusCheck.Hide();
            cc.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String id = txtId.Text;


            if (id.Length == 13)
            {


                try
                {
                    // Use the DatabaseHelper class
                 
                    DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                    // Check if the person is registered
                    string querySelectOwner = "SELECT * FROM owners WHERE id_number=?";
                    OleDbParameter[] selectOwnerParameters =
                    {
        new OleDbParameter("id_number", txtId.Text)
    };

                    DataTable ownerTable = dbHelper.ExecuteDataTable(querySelectOwner, selectOwnerParameters);

                    if (ownerTable.Rows.Count > 0)
                    {
                        // Person is registered
                        Random rand = new Random();
                        int temp = rand.Next(10000, 100000);
                        txtPet.Text = "#" + Convert.ToString(temp);
                    }
                    else
                    {
                        MessageBox.Show("This person is not registered on the system!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

            }




        }

      

        private void button4_Click(object sender, EventArgs e)
        {

            if (!(txtId.Text == "" && txtPet.Text == ""))
            {
                try
                {
                    // Use the DatabaseHelper class
                  
                    DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                    // Check if the pet is already assigned
                    string queryCheckAssignment = "SELECT * FROM Assigned_Pet_IDs WHERE petID=? AND OwerID=?";
                    OleDbParameter[] checkAssignmentParameters =
                    {
            new OleDbParameter("petID", txtPet.Text),
            new OleDbParameter("OwerID", txtId.Text)
        };

                    DataTable assignmentTable = dbHelper.ExecuteDataTable(queryCheckAssignment, checkAssignmentParameters);

                    if (assignmentTable.Rows.Count > 0)
                    {
                        MessageBox.Show("This pet has already been assigned to you!");
                    }
                    else
                    {
                        // Assign the pet
                        dbHelper.ExecuteNonQuery("INSERT INTO Assigned_Pet_IDs (petID, OwerID) VALUES (?, ?)", checkAssignmentParameters);

                        MessageBox.Show("Pet ID has been assigned, they can now fully register the pet");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Owner ID and pet ID must be filled!!!");
            }





        }

        private void Check_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text == ""))
            {
                try
                {
                    // Use the DatabaseHelper class
                  
                    DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                    // Get owner information based on the pet ID
                    string querySelectOwner = "SELECT Owner_ID FROM Pets WHERE Pet_ID=?";
                    OleDbParameter[] selectOwnerParameters =
                    {
            new OleDbParameter("Pet_ID", textBox1.Text)
        };

                    string OWNER = dbHelper.ExecuteScalar(querySelectOwner, selectOwnerParameters).ToString();

                    // Display owner information
                    string querySelectOwnerDetails = "SELECT ID_number, Name, Surname, Gender, Address, Contact_No FROM owners WHERE ID_number=?";
                    OleDbParameter[] selectOwnerDetailsParameters =
                    {
            new OleDbParameter("ID_number", OWNER)
        };

                    DataTable ownerDetailsTable = dbHelper.ExecuteDataTable(querySelectOwnerDetails, selectOwnerDetailsParameters);

                    if (ownerDetailsTable.Rows.Count > 0)
                    {
                        DataRow row = ownerDetailsTable.Rows[0];
                        label12.Text = row["ID_number"].ToString();
                        label10.Text = row["Name"].ToString();
                        label11.Text = row["Surname"].ToString();
                        label13.Text = row["Gender"].ToString();
                        label9.Text = row["Address"].ToString();
                        label8.Text = row["Contact_No"].ToString();
                    }

                    // Get pet status information
                    string querySelectPetStatus = "SELECT status FROM Pets WHERE Pet_ID=?";
                    OleDbParameter[] selectPetStatusParameters =
                    {
            new OleDbParameter("Pet_ID", textBox1.Text)
        };

                    string pictureValue = dbHelper.ExecuteScalar(querySelectPetStatus, selectPetStatusParameters).ToString();

                    // Display pet status
                    if (pictureValue != null)
                    {
                        if (pictureValue.Equals("good"))
                        {
                            pictureBox1.Image = Properties.Resources.tick;
                            label14.Text = "Good";
                            panel1.BackColor = Color.FromArgb(120, Color.Green);
                        }
                        else if (pictureValue.Equals("missing"))
                        {
                            label14.Text = "Missing";
                            pictureBox1.Image = Properties.Resources.missing;
                            panel1.BackColor = Color.FromArgb(120, Color.Red);
                        }
                        else if (pictureValue.Equals("stolen"))
                        {
                            label14.Text = "Stolen";
                            pictureBox1.Image = Properties.Resources.stolen;
                            panel1.BackColor = Color.FromArgb(120, Color.Red);
                        }
                        else if (pictureValue.Equals("recovered"))
                        {
                            label14.Text = "Recovered";
                            pictureBox1.Image = Properties.Resources.recovered;
                            panel1.BackColor = Color.FromArgb(120, Color.Green);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Pet ID must be filled!!!");
            }

        }

        private void txtStatus_Click(object sender, EventArgs e)
        {
            cc.Show();
            panel5.Hide();
           // panelstatusCheck.Visible = true;
            panelstatusCheck.Show();
            

        }

      

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage hpage = new HomePage();
            hpage.Show();
        }

        private void LogOutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage hpage = new HomePage();
            hpage.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cc.Show();
         
            // panelstatusCheck.Visible = true;
            panelstatusCheck.Show();
            panel5.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox2.Text = row.Cells["ID_number"].Value.ToString();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                // Check if ID_Number exists in owners table
                int idNumberCount = dbHelper.ExecuteScalar("SELECT COUNT(*) FROM owners WHERE ID_Number = ?", new OleDbParameter("ID_Number", textBox2.Text));

                if (idNumberCount > 0)
                {
                    // ID_Number exists, fetch owner details
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT ID_number, Name, Surname, Address, Contact_No, gender, [Password] FROM owners WHERE ID_number = ?", dbHelper.GetConnection());
                    adapter.SelectCommand.Parameters.AddWithValue("ID_number", textBox2.Text);

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridView1.DataSource = table;
                }
                else
                {
                    MessageBox.Show("THIS ID IS NOT REGISTERED ON THE SYSTEM !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                using (OleDbConnection con = dbHelper.GetConnection())
                {
                    con.Open();

                    // Check if ID_Number exists in owners table
                    int ownerIdCount = dbHelper.ExecuteScalar("SELECT COUNT(*) FROM owners WHERE ID_Number = ?", new OleDbParameter("ID_Number", textBox2.Text));

                    if (ownerIdCount > 0)
                    {
                        // ID_Number exists, fetch data from PetS table
                        DataTable table = dbHelper.ExecuteDataTable("SELECT * FROM PetS WHERE Owner_ID = ?", new OleDbParameter("Owner_ID", textBox2.Text));

                        // Display data in dataGridView1
                        dataGridView1.DataSource = table;
                    }
                    else
                    {
                        MessageBox.Show("THIS ID IS NOT REGISTERED ON THE SYSTEM !");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex.Message);
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
               
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                // Check if Pet_ID exists in Pets table
                int petIdCount = dbHelper.ExecuteScalar("SELECT COUNT(*) FROM Pets WHERE Pet_ID = ?", new OleDbParameter("Pet_ID", textBox1.Text));

                if (petIdCount > 0)
                {
                    // Pet_ID exists, update statusByVet
                    dbHelper.ExecuteNonQuery("UPDATE Pets SET statusByVet = ? WHERE Pet_ID = ?", new OleDbParameter("statusByVet", textBox3.Text), new OleDbParameter("Pet_ID", textBox1.Text));
                    MessageBox.Show("Status updated!");
                }
                else
                {
                    MessageBox.Show("Pet ID not found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


        }
    }
}
