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
    public partial class UserUpdate : Form
    {
        String OWNER = "";
        String owner = "";
        public UserUpdate()
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

                command.CommandText = "SELECT ID_number, Name, Surname, Gender, Address, Contact_No, [Password] FROM owners Where ID_number ='" + owner + "';";
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

     

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtUsername.Text = row.Cells["Surname"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtContact.Text = row.Cells["Contact_no"].Value.ToString();
                txtPassword.Text = row.Cells["password"].Value.ToString();

            }

        }

      

        private void buttonUpdate_Click(object sender, EventArgs e)
        {


            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
            con.Open();

            try
            {


                OleDbCommand command = new OleDbCommand();
                command.Connection = con;


                //  MessageBox.Show("id as been assign they can now fully register there pet");
                command.CommandText = "SELECT ID_Number FROM Logged_In ";

                OleDbDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    OWNER = reader["ID_Number"].ToString();

                }
               
                con.Close();
                con.Open();

                command.CommandText = "UPDATE owners SET  Name ='"+txtName.Text+"', Surname = '"+txtUsername.Text+"', Address = '"+txtAddress.Text+ "', Contact_No = '"+txtContact.Text+ "', [Password] = '"+txtPassword.Text+"'  WHERE ID_Number = '"+OWNER+"';";


                



                command.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Updated successfully");

            }
            catch (Exception EX)
            {
                MessageBox.Show("error " + EX);
            }

            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OwnerDesk ow = new OwnerDesk();
            ow.Show();
            this.Hide();
        }
    }
}
