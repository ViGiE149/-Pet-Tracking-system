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
    public partial class VetLogInForm : Form
    {
        vet page = new vet();
        bool check = false;
        public VetLogInForm()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {



            if(!(txtID.Text.Equals("")  && txtPssword.Text.Equals("")))
            {


                try
                {
                    OleDbConnection con = new OleDbConnection();
                    con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
                    con.Open();

                    OleDbCommand command = new OleDbCommand();
                    command.Connection = con;

                    command.CommandText = "SELECT Work_ID , Password FROM vetRegTable WHERE work_Id = '" + txtID.Text + "' AND password ='" + txtPssword.Text + "';";


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
                        vet page = new vet();
                        page.Show();

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
                MessageBox.Show("fill in all the the spaces!");
            }
               
            
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void passHide_Click(object sender, EventArgs e)
        {
            passShow.BringToFront();
            txtPssword.UseSystemPasswordChar = false;
        }
        
        private void passShow_Click(object sender, EventArgs e)
        {

            passHide.BringToFront();
            txtPssword.UseSystemPasswordChar = true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            VetSignUp page = new VetSignUp();
            page.Show();
            this.Hide();
        }
    }
}
