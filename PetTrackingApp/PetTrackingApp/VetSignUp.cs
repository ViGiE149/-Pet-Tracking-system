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

    public partial class VetSignUp : Form
    {
        bool check = false;
        String gender = "";
        public VetSignUp()
        {
            InitializeComponent();
        }

        private void VetSignUp_Load(object sender, EventArgs e)
        {

        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login2_Click(object sender, EventArgs e)
        {
            VetLogInForm log = new VetLogInForm();
            log.Show();
            this.Hide();
        }

        private void SignUp2_Click(object sender, EventArgs e)
        {
            VetSignUp log = new VetSignUp();
            log.Show();
            this.Hide();
        }

        private void SignUpBtn_Click(object sender, EventArgs e)
        {
            string contact = txtContact.Text;
            string phoneNumPattern = @"^((?:\+27|27)|0)(\d{2})-?(\d{3})-?(\d{4})$";
            Match regexNum = Regex.Match(contact, phoneNumPattern);

            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSurname.Text) || string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtAddress.Text) || string.IsNullOrWhiteSpace(contact) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtPasswordConfirm.Text) || (!radioButton1.Checked && !radioButton2.Checked))
            {
                MessageBox.Show("Fill in all details");
            }
            else
            {
                if (regexNum.Success)
                {
                    if (txtPassword.Text.Equals(txtPasswordConfirm.Text))
                    {
                        try
                        {
                            int m = Convert.ToInt32(txtContact.Text);

                            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
                            DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                            // Check if ID number is already registered
                            int idCount = dbHelper.ExecuteScalar("SELECT COUNT(*) FROM vetRegTable WHERE Work_ID = ?", new OleDbParameter("Work_ID", txtID.Text));

                            if (idCount == 0)
                            {
                                // Insert new vet record
                                dbHelper.ExecuteNonQuery("INSERT INTO vetRegTable (Work_ID, name_, Surname, Address, Contact_No, Gender, Password) VALUES (?, ?, ?, ?, ?, ?, ?)",
                                    new OleDbParameter("Work_ID", txtID.Text),
                                    new OleDbParameter("name_", txtName.Text),
                                    new OleDbParameter("Surname", txtSurname.Text),
                                    new OleDbParameter("Address", txtAddress.Text),
                                    new OleDbParameter("Gender", gender),
                                    new OleDbParameter("Contact_No", txtContact.Text),
                                    new OleDbParameter("Password", txtPassword.Text));

                                MessageBox.Show("Registered successfully");
                                this.Hide();
                                new VetLogInForm().Show();
                            }
                            else
                            {
                                MessageBox.Show("This ID number is already registered");
                            }
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Contact number must be digits only");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Passwords do not match");
                    }
                }
                else
                {
                    MessageBox.Show("Contact number must be 10 digits and start with a 0");
                }
            }


        }


        private void txtContact_TextChanged(object sender, EventArgs e)
        {

        }

        private void passHide_Click(object sender, EventArgs e)
        {
            passShow.BringToFront();
            txtPassword.UseSystemPasswordChar = false;
        }

        private void passHide1_Click(object sender, EventArgs e)
        {
            passShow1.BringToFront();
            txtPasswordConfirm.UseSystemPasswordChar = false;
        }

        private void passShow_Click(object sender, EventArgs e)
        {
            passHide.BringToFront();
            txtPassword.UseSystemPasswordChar = true;
        }

        private void passShow1_Click(object sender, EventArgs e)
        {
            passHide1.BringToFront();
            txtPasswordConfirm.UseSystemPasswordChar = true;
        }
    }
}

