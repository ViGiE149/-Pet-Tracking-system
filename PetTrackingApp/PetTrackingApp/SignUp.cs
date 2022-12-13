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

            string contact = txtContact.Text;
            String id = txtID.Text;
            Match regexID = Regex.Match(id, idPattern);
            Match regexNum = Regex.Match(contact, phoneNumPattern);

            if (!(txtID.Text == "" || txtName.Text == "" || txtSurname.Text == "" || txtAddress.Text == "" || txtContact.Text == "" || txtPassword.Text == "" || txtPasswordConfirm.Text == "" || radioButton1.Checked.Equals(false) && radioButton2.Checked.Equals(false)))
            {
                if (regexID.Success)
                {
                    if (regexNum.Success)
                {
                    try
                    {

                        if (txtPassword.Text == txtPasswordConfirm.Text)
                        {

                            try
                            {
                                OleDbConnection con = new OleDbConnection();
                                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
                                con.Open();

                                OleDbCommand command = new OleDbCommand();
                                command.Connection = con;



                                command.CommandText = "SELECT * FROM owners WHERE id_number ='" + txtID.Text + "';";

                                OleDbDataReader reader = command.ExecuteReader();



                                if (reader.Read())
                                {
                                    // con.Close();
                                    // con.Open();
                                    check = true;

                                }

                                con.Close();
                                con.Open();
                                if (check == false)
                                {

                                    command.CommandText = " INSERT INTO `owners` (`ID_number`, `Name`, `Surname`, `Gender`, `Address`, `Contact_No`, `Password`) VALUES( '" + txtID.Text + "','" + txtName.Text + "','" + txtSurname.Text + "', '" + gender + "','" + txtAddress.Text + "','" + txtContact.Text + "','" + txtPassword.Text + "'); ";
                                    command.ExecuteNonQuery();
                                    con.Close();
                                    MessageBox.Show("registed");
                                    this.Hide();
                                    new LogIn().Show();

                                }
                                else
                                {
                                    MessageBox.Show("this iD number is already reqistered");
                                }
                                con.Close();
                            }
                            catch (Exception EX)
                            {
                                MessageBox.Show("an error occoured" + EX);

                            }
                        }
                        else
                        {
                            MessageBox.Show("passwords are not the same");
                        }


                    }
                  
                    catch (FormatException)
                    {

                        MessageBox.Show("number format error id and numbe");
                    }
                   
                }
                else
                {
                    MessageBox.Show("invalid phone number");
                }
                  
            }
            else
            {
                    MessageBox.Show("invalid ID number");
            }


            }
            else
            {
                MessageBox.Show("Fill in all places");
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
