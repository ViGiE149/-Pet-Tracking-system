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
    public partial class StatusPanel : Form
    {
        public StatusPanel()
        {
            String owner = "";
            InitializeComponent();







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

                command.CommandText = "select pet_ID, name,status,statusByVet FROM PetS where Owner_ID ='" + owner + "';";
                command.ExecuteNonQuery();

                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView1.DataSource = table;

                con.Close();



            }
            catch (Exception EX)
            {
                MessageBox.Show("error " + EX);
            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            OwnerDesk page = new OwnerDesk();
            page.Show();
        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {




            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtName.Text = row.Cells["pet_ID"].Value.ToString();
                texBoxStatus.Text = row.Cells["status"].Value.ToString();
               

            }


        }

       

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
                con.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = con;

                if (!(texBoxStatus.Text == "" || txtName.Text== "'''''''''''''''''''"))
                {

                    command.CommandText = "UPDATE Pets SET status ='" + texBoxStatus.Text + "' WHERE Pet_ID = '" + txtName.Text + "'; ";
                    command.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("Updated successfully");
                }
                else
                {
                    MessageBox.Show("error! click and update status of a registered pet");
                }

            }
            catch (Exception EX)
            {
                MessageBox.Show("error " + EX);
            }

        
        }

      
      
    }
}
