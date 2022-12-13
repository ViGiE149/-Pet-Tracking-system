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
    public partial class UpdatePanel : Form
    {
        String owner="";
        public UpdatePanel()
        {
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

                command.CommandText = "select * FROM PetS where Owner_ID ='"+owner +"';";
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            OwnerDesk page = new OwnerDesk();
            page.Show();
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



                command.CommandText = "UPDATE Pets SET name ='" + txtPetName.Text + "', Colour = '" + txtPetColor.Text + "'   WHERE Pet_ID = '" + txtPetID.Text + "'; ";
                command.ExecuteNonQuery();
               
                con.Close();

                MessageBox.Show("Updated successfully");

            }
            catch(Exception EX)
            {
                MessageBox.Show("error " + EX);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
            con.Open();

            try
            {
                

                OleDbCommand command = new OleDbCommand();
                command.Connection = con;

                command.CommandText = "DELETE * FROM PetS WHERE Pet_ID = '"+txtPetID.Text+"'; ";
                command.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Deleted successfully");
 
            }
            catch (Exception EX)
            {
                MessageBox.Show("error " + EX);
            }

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

             if(e.RowIndex>=0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtPetID.Text = row.Cells["pet_ID"].Value.ToString();
                txtPetName.Text = row.Cells["name"].Value.ToString();
                txtPetColor.Text = row.Cells["Colour"].Value.ToString();

            }



        }
    }
}
