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
                using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;"))
                {
                    con.Open();

                    using (OleDbCommand command = new OleDbCommand())
                    {
                        command.Connection = con;

                        // Get ID_Number from Logged_In table
                        command.CommandText = "SELECT ID_Number FROM Logged_In;";
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                owner = reader["ID_Number"].ToString();
                            }
                        }

                        con.Close();
                        con.Open();

                        // Select data from Pet_Transfer table for the current owner
                        command.CommandText = "SELECT * FROM Pet_Transfer WHERE current_ownerID = '" + owner + "';";
                        command.ExecuteNonQuery();

                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            dataGridView1.DataSource = table;
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
