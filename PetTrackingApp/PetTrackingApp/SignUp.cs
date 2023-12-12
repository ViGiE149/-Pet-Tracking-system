using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;




namespace PetTrackingApp
{
   
    public partial class SignUp : Form
    {
      
        String gender = "";
 
        string phoneNumPattern = @"^((?:\+27|27)|0)(\d{2})-?(\d{3})-?(\d{4})$";
        string  idPattern = @"(?<Year>[0-9][0-9])(?<Month>([0][1-9])|([1][0-2]))(?<Day>([0-2][0-9])|([3][0-1]))(?<Gender>[0-9])(?<Series>[0-9]{3})(?<Citizenship>[0-9])(?<Uniform>[0-9])(?<Control>[0-9])";

      

         
        public SignUp()
        {
            InitializeComponent();
        }
       
      

        private void SignUpBtn_Click(object sender, EventArgs e)
        {

            try
            {
                if (new TextBox[] { txtID, txtName, txtSurname, txtAddress, txtContact, txtPassword, txtPasswordConfirm }
                    .Any(textBox => string.IsNullOrWhiteSpace(textBox.Text) || (radioButton1.Checked == false && radioButton2.Checked == false)))
                {
                    MessageBox.Show("Fill in all places");
                }
                else
                {
                    string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
                    DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                    // Check if ID is already registered
                    int count = dbHelper.ExecuteScalar("SELECT COUNT(*) FROM owners WHERE ID_number = @ID_number",
                                                       new OleDbParameter("@ID_number", txtID.Text));

                    if (count > 0)
                    {
                        MessageBox.Show("This ID number is already registered");
                    }
                    else
                    {
                        // Hash the password
                        string hashedPassword = shared.HashPassword(txtPassword.Text);

                        // Validate phone number
                        Match regexNum = Regex.Match(txtContact.Text, phoneNumPattern);

                        if (regexNum.Success)
                        {
                            // Insert data into the database using DatabaseHelper
                            dbHelper.ExecuteNonQuery("INSERT INTO owners (ID_number, Name, Surname, Gender, Address, Contact_No, Password) " +
                                                      "VALUES (@ID_number, @Name, @Surname, @Gender, @Address, @Contact_No, @Password)",
                                                      new OleDbParameter("@ID_number", txtID.Text),
                                                      new OleDbParameter("@Name", txtName.Text),
                                                      new OleDbParameter("@Surname", txtSurname.Text),
                                                      new OleDbParameter("@Gender", gender),
                                                      new OleDbParameter("@Address", txtAddress.Text),
                                                      new OleDbParameter("@Contact_No", txtContact.Text),
                                                      new OleDbParameter("@Password", hashedPassword));

                            MessageBox.Show("Registered");
                            this.Hide();
                            new LogIn().Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid phone number");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            // Function to hash the password using SHA-256
          

        }

      
        private void Login2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn page = new LogIn();
            page.Show();
        }

     

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "female";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gender = "male";
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            passHide.BringToFront();
            txtPassword.UseSystemPasswordChar = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            passHide1.BringToFront();
            txtPasswordConfirm.UseSystemPasswordChar = true;
        }

        private void hidePass_Click(object sender, EventArgs e)
        {

            passShow.BringToFront();
            txtPassword.UseSystemPasswordChar = false;
        }

        private void hidePass1_Click(object sender, EventArgs e)
        {

            passShow1.BringToFront();
            txtPasswordConfirm.UseSystemPasswordChar = false;
        }
    }
}
