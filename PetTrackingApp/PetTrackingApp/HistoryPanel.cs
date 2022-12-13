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
    public partial class HistoryPanel : Form
    {
        String owner = "";
        public HistoryPanel()
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

                command.CommandText = "select * FROM Pet_Transfer where current_ownerID ='" + owner +"';";
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

        
    }
}
