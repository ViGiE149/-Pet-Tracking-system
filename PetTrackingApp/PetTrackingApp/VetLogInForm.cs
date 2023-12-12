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
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
        public VetLogInForm()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {

            if (!(string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtPssword.Text)))
            {
                try
                {
                 
                    DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                    // Use parameterized query to check credentials
                    int rowCount = dbHelper.ExecuteScalar("SELECT COUNT(*) FROM vetRegTable WHERE Work_ID = ? AND Password = ?",
                                                          new OleDbParameter("Work_ID", txtID.Text),
                                                          new OleDbParameter("Password", txtPssword.Text));

                    if (rowCount > 0)
                    {
                        // Update Logged_In table
                        dbHelper.ExecuteNonQuery("UPDATE Logged_In SET ID_Number = ? WHERE count = 1", new OleDbParameter("ID_Number", txtID.Text));

                        this.Hide();
                        vet page = new vet();
                        page.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid credentials!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Fill in all the spaces!");
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

        private void LogInPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
