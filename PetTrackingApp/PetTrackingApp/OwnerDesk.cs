using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.OleDb;

namespace PetTrackingApp
{
   
    public partial class OwnerDesk : Form
    {
        String owner = "";
        String name = "";
        public OwnerDesk()
        {
            InitializeComponent();
            // Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            pictureBox6.BackColor = Color.FromArgb(0, 0, 0, 0);
            pictureBox3.BackColor = Color.FromArgb(0, 0, 0, 0);
            pictureBox2.BackColor = Color.FromArgb(00, 0, 0, 0);
            pictureBox5.BackColor = Color.FromArgb(0, 0, 0, 0);


        }

       

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            TransfarePanel page = new TransfarePanel();
            page.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            petWindow page = new petWindow();
            page.Show();
        }

        private void Closebtn_Click(object sender, EventArgs e)
        {
          
            Application.Exit();
        }

      
        private void StatusBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            StatusPanel page = new StatusPanel();
            page.Show();
        }

        private void TransfareBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            TransfarePanel page = new TransfarePanel();
            page.Show();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdatePanel page = new UpdatePanel();
            page.Show();
        }

        private void HistoryBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            HistoryPanel page = new HistoryPanel();
            page.Show();
        }

        private void LogOutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage hpage = new HomePage();
            hpage.Show();
        }

        private void StatusBtn_Leave(object sender, EventArgs e)
        {
            StatusBtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void TransfareBtn_Leave(object sender, EventArgs e)
        {
           TransfareBtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void AddNewBtn_Leave(object sender, EventArgs e)
        {
           AddNewBtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void DeleteBtn_Leave(object sender, EventArgs e)
        {
            UpdateBtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void HistoryBtn_Leave(object sender, EventArgs e)
        {
            HistoryBtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void LogOutBtn_Leave(object sender, EventArgs e)
        {
            LogOutBtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

            this.Hide();
            HomePage hpage = new HomePage();
            hpage.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            StatusPanel page = new StatusPanel();
            page.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            petWindow page = new petWindow();
            page.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            this.Hide();
            UpdatePanel page = new UpdatePanel();
            page.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            HistoryPanel page = new HistoryPanel();
            page.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {     
            try
            {

                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
                con.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = con;



                command.CommandText = "SELECT ID_Number FROM Logged_In;";
                OleDbDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    owner = reader["ID_Number"].ToString();

                }

                con.Close();
                con.Open();

                command.CommandText = "SELECT * FROM owners where ID_number = '" + owner+"';";
                reader = command.ExecuteReader();

                if (reader.Read())
                {

                    name = reader["Name"].ToString();
                   
                }
                txtUsername.Text = name;
                txtId.Text = owner;
                con.Close();


            }
            catch (Exception EX)
            {
                MessageBox.Show("error " + EX);
            }
            timer1.Stop();

        }

        private void OwnerDesk_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserUpdate user = new UserUpdate();
            user.Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            UserUpdate user = new UserUpdate();
            user.Show();
        }
    }
}
