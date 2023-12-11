using System;
using System.Data.OleDb;
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
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";
                DatabaseHelper dbHelper = new DatabaseHelper(connectionString);

                int count = dbHelper.ExecuteScalar("SELECT COUNT(*) FROM owners WHERE id_number = ? AND password = ?",
                                                   new OleDbParameter("?", txtID.Text),
                                                   new OleDbParameter("?", txtPassword.Text));

                if (count > 0)
                {
                    // Update Logged_In table
                    dbHelper.ExecuteNonQuery("UPDATE Logged_In SET ID_Number = ? WHERE count = 1", new OleDbParameter("?", txtID.Text));

                    this.Hide();
                    new OwnerDesk().Show();
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

        private void hidePass_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
