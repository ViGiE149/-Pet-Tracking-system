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
    public partial class petWindow : Form
    {
        string gender = "";
        String ower = "";
        String owner = "";
        String name = "";

        public petWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            OwnerDesk page = new OwnerDesk();
            page.Show();
        }



        private void petAdd_Click(object sender, EventArgs e)
        {
            if (!(txtPetName.Text.Equals("") || txtPetID.Text.Equals("") || txtPetColor.Text.Equals("") || txtTypeofPet.Text.Equals("") || malePet.Checked == false && femalePet.Checked == false))
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

                        ower = reader["ID_Number"].ToString();

                    }

                    con.Close();
                    con.Open();

                    command.CommandText = "INSERT INTO `Pets` (`Pet_ID`, `Owner_ID`,`Name`, `Pet_Type`, `Breed`, `Colour`, `Gender`, `Date_Of_Birth`, `status`)" +
                    "VALUES('" + txtPetID.Text + "', '" + ower + "', '" + txtPetName.Text + "', '" + txtTypeofPet.Text + "', '" + txtPetBreed.Text + "', '" + txtPetColor.Text + "', '" + gender + "', '" + txtPetDOB.Value.ToString() + "', 'good');";
                    command.ExecuteNonQuery();


                    command.CommandText = "DELETE FROM `Assigned_Pet_IDs` WHERE `petID` = '" + txtPetID.Text + "' AND `OwerID` = '" + ower + "' ";
                    command.ExecuteNonQuery();


                    con.Close();
                    MessageBox.Show("pet registerd successfully!");
                    txtPetName.Text = "";
                    txtTypeofPet.Text = "";
                    txtPetColor.Text = "";

                    txtPetBreed.Text = "";
                    gender = "";
                    txtPetID.Text = "";
                    femalePet.Checked = false;
                    malePet.Checked = false;
                }
                catch
                {
                    MessageBox.Show("an error occored!");
                }
              
        }
            else { MessageBox.Show("fill in all the spaces!"); }


        }

        private void button3_Click(object sender, EventArgs e)
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




               //  command.CommandText = "SELECT * FROM Asigned_Pet_IDs where OwerID = '" + owner + "';";
              //  command.ExecuteNonQuery();



                command.CommandText = "SELECT * FROM Assigned_Pet_IDs where OwerID = '" + owner + "';";
                reader = command.ExecuteReader();

                if (reader.Read())
                {

                    name = reader["petID"].ToString();
                    MessageBox.Show(name);
                }
               
                txtPetID.Text = name;
                //txtId.Text = owner;
                con.Close();
               

            }
            catch (Exception EX)
            {
                MessageBox.Show("error " + EX);
            }


        }

        private void malePet_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Male";
          
        }

        private void femalePet_CheckedChanged(object sender, EventArgs e)
        {
            gender = "female";
        }

        private void petExit_Click(object sender, EventArgs e)
        {
            txtPetBreed.Text = "";
            txtPetColor.Text = "";
            txtTypeofPet.Text = "";
            txtPetID.Text = "";
            txtPetName.Text = "";
        }
    }
}
