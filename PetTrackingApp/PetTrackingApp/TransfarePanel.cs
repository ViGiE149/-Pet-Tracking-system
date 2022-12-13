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
    public partial class TransfarePanel : Form
    {
        String owner = "";
        bool check = false;
        String newOwenr;
        String petID;
        bool petValid = false;
        bool idValid = false;

        public TransfarePanel()
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

               txtPrevious.Text = owner;
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

        private void petTransfareBtn_Click(object sender, EventArgs e)
        {
          

            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
            con.Open();

            OleDbCommand command = new OleDbCommand();
            command.Connection = con;

            if (txtNewOwner.Text == "" || txtPetID.Text == "")
            {
                MessageBox.Show("error make sure new owner id and pat id are filled!");
            }
            else
            {





                command.CommandText = "SELECT Pet_ID FROM Pets WHERE Pet_ID = '" + txtPetID.Text + "';";

                OleDbDataReader reader = command.ExecuteReader();



                while (reader.Read())
                {

                   idValid = true;
                }

                con.Close();
                con.Open();


                command.CommandText = "SELECT Pet_ID FROM Pets WHERE Pet_ID = '" + txtPetID.Text + "';";

                 reader = command.ExecuteReader();

                

                if (reader.Read())
                {

                    petValid = true;
                }

                con.Close();
                con.Open();


                if (idValid==true)
                {
                    if (petValid==true)
                    {

                        command.CommandText = "SELECT * FROM Pet_Transfer WHERE current_ownerID = '" + txtPrevious.Text + "'   AND   new_ownerID ='" + txtNewOwner.Text + "' AND pet_ID = '" + txtPetID.Text + "';";


                        reader = command.ExecuteReader();


                        if (reader.Read())
                        {
                            MessageBox.Show("this pet has already been tranferred to this ID");
                            check = false;

                        }
                        else
                        {
                            check = true;
                        }

                        con.Close();

                        if (check == true)
                        {
                            con.Open();
                            command.CommandText = "INSERT INTO `Pet_Transfer` ( `pet_ID`,`current_ownerID`, `new_ownerID`, `reason`, `date`) VALUES ('" + txtPetID.Text + "','" + txtPrevious.Text + "','" + txtNewOwner.Text + "','" + txtReason.Text + "','" + txtPetDOB.Value.ToString() + "')";
                            command.ExecuteNonQuery();



                            command.CommandText = "UPDATE Pets SET Owner_ID  = '" + txtNewOwner.Text + "'   WHERE Pet_ID = '" + txtPetID.Text + "'; ";
                            command.ExecuteNonQuery();

                            MessageBox.Show("pet " + txtPetID.Text + " successfully transfered from "+ txtPrevious.Text+" to " + txtNewOwner.Text);
                        }
                        con.Close();
                    }
                    else
                    { MessageBox.Show("pet  ID invalid!!!"); } }
                else { MessageBox.Show("you cannot trenfer to an ID that is not registered on the system!!!"); }

            }
        }

        
    }
}
