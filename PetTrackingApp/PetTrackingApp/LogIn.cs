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
    public partial class LogIn : Form
    {
        bool check = false;
        public LogIn()
        {
            InitializeComponent();
        }

      

        private void LoginBtn_Click(object sender, EventArgs e)
        {


            try
            {
                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
                con.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = con;

                command.CommandText = "SELECT * FROM owners WHERE id_number ='" + txtID.Text + "' AND password ='" + txtPassword.Text + "';";


                OleDbDataReader reader = command.ExecuteReader();
               
               if (reader.Read())
                {
                   
                    check = true;
              
                }
               else
                {
                    MessageBox.Show("invalid craditials!");
                }
                con.Close();
                con.Open();
                if (check)
                {
                   
                    command.CommandText = "UPDATE Logged_In SET ID_Number ='" + txtID.Text + "' where count='1';"; 
                    command.ExecuteNonQuery();

                    this.Hide();
                    OwnerDesk page = new OwnerDesk();
                    page.Show();

                }
                con.Close();
            }
            catch(Exception EX)
            {
                MessageBox.Show("an error occoured" +EX);

            }



        }

        private void Signup1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp page = new SignUp();
            page.Show();
        }

       

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {   
                passShow.BringToFront();
                txtPassword.UseSystemPasswordChar = false;
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
           
                passHide.BringToFront();
                txtPassword.UseSystemPasswordChar = true;
            
        }
    }
}
