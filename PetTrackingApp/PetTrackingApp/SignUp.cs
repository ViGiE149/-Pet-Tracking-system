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



namespace PetTrackingApp
{
   
    public partial class SignUp : Form
    {
      
        String gender = "";
        bool check = false;
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

                    using (OleDbConnection con = new OleDbConnection(connectionString))
                    {
                        con.Open();

                        using (OleDbCommand command = new OleDbCommand())
                        {
                            command.Connection = con;

                            // Check if ID is already registered
                            command.CommandText = "SELECT * FROM owners WHERE id_number = ?";
                            command.Parameters.AddWithValue("?", txtID.Text);

                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    MessageBox.Show("This ID number is already registered");
                                }
                                else
                                {
                                    // Insert new record
                                    if (txtPassword.Text == txtPasswordConfirm.Text)
                                    {
                                        // Validate phone number
                                        Match regexNum = Regex.Match(txtContact.Text, phoneNumPattern);

                                        if (regexNum.Success)
                                        {
                                            // Insert data into the database
                                            command.CommandText = "INSERT INTO owners (ID_number, Name, Surname, Gender, Address, Contact_No, Password) VALUES (?, ?, ?, ?, ?, ?, ?)";
                                            command.Parameters.AddWithValue("?", txtID.Text);
                                            command.Parameters.AddWithValue("?", txtName.Text);
                                            command.Parameters.AddWithValue("?", txtSurname.Text);
                                            command.Parameters.AddWithValue("?", gender);
                                            command.Parameters.AddWithValue("?", txtAddress.Text);
                                            command.Parameters.AddWithValue("?", txtContact.Text);
                                            command.Parameters.AddWithValue("?", txtPassword.Text);

                                            command.ExecuteNonQuery();

                                            MessageBox.Show("Registered");
                                            this.Hide();
                                            new LogIn().Show();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Invalid phone number");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Passwords are not the same");
                                    }
                                }
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
