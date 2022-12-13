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
    




    public partial class vet : Form
    {
        bool see = false;
        bool saw = false;
        String x = "";
        String idx = "";
        String namex = "";
        String surnamex = "";
        String addressx = "";
        String contactx = "";
        String pictureValue = "";
        string OWNER = "";
        String black = "";
        bool seex = false;
        public vet()
        {
            InitializeComponent();
            panel5.Hide();
            // panelstatusCheck.Visible = true;
            panelstatusCheck.Hide();

            cc.Show();
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

                    x = reader["ID_Number"].ToString();

                }

                con.Close();
                con.Open();

                command.CommandText = "SELECT ID_number, Name , Surname , Address, Contact_No,gender, [Password] FROM owners;";
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

      

        private void button3_Click_1(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(30, 30, 70);

            panelstatusCheck.Hide();
            cc.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String id = txtId.Text;


            if (id.Length == 13)
            {


                int month = Convert.ToInt32(id.Substring(2, 2));
                if (month < 13 && month > 0)
                {


                    int day = Convert.ToInt32(id.Substring(4, 2));
                    if (day < 32 && day > 0)
                    {
                        int genderr = Convert.ToInt32(id.Substring(6, 4));
                        if (genderr >= 0000 && genderr <= 9999)
                        {
                            int citizenship = Convert.ToInt32(id.Substring(10, 1));
                            if (citizenship >= 0 && citizenship <= 1)
                            {

                                int randomA = Convert.ToInt32(id.Substring(11, 1));
                                if (randomA < 10 && randomA >= 0)
                                {



                                    int[] array ={ Convert.ToInt32(id.Substring(0,1)), Convert.ToInt32(id.Substring(2,1)),  Convert.ToInt32(id.Substring(4,1)),
              Convert.ToInt32(id.Substring(6,1)), Convert.ToInt32(id.Substring(8,1)), Convert.ToInt32(id.Substring(10,1))};


                                    int[] array1 ={ Convert.ToInt32(id.Substring(1,1)), Convert.ToInt32(id.Substring(3,1)),  Convert.ToInt32(id.Substring(5,1)),
                Convert.ToInt32(id.Substring(7,1)), Convert.ToInt32(id.Substring(9,1)), Convert.ToInt32(id.Substring(11,1))};


                                    int sum = array[0] + array[1] + array[2] + array[3] + array[4] + array[5];
                                    String allAsOne = array1[0] + "" + array1[1] + "" + array1[2] + "" + array1[3] + "" + array1[4] + "" + array1[5];

                                    int doubleSUm = Convert.ToInt32(allAsOne) * 2;   /////to Int

                                    String makeDoubleSumString = Convert.ToString((doubleSUm));  ////toString

                                    int[] SumSD = new int[makeDoubleSumString.Length];
                                    int sumEven = 0;

                                    for (int i = 0; i < SumSD.Length; i++)
                                    {
                                        SumSD[i] = Convert.ToInt32(makeDoubleSumString.Substring(i, 1));
                                        sumEven += SumSD[i];
                                        if (makeDoubleSumString.Length == i + 1)
                                        {

                                           

                                            int add = sum + sumEven;

                                            int lastAdd = Convert.ToString(add).Length;
                                            int IDcheckSumValue = 10 - Convert.ToInt32(Convert.ToString(add).Substring(lastAdd - 1));

                                            int x = Convert.ToInt32(id.Substring(id.Length - 1));

                                            if (x == IDcheckSumValue)
                                            {
                                                MessageBox.Show(" ID is valid");



                                                    try
                                                    {
                                                        OleDbConnection con = new OleDbConnection();
                                                        con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
                                                        con.Open();

                                                        OleDbCommand command = new OleDbCommand();
                                                        command.Connection = con;

                                                        command.CommandText = "SELECT * FROM owners WHERE id_number ='" + txtId.Text + "';";


                                                        OleDbDataReader reader = command.ExecuteReader();

                                                        if (reader.Read())
                                                        {


                                                          Random rand = new Random();

                                                          int temp = rand.Next(10000, 100000);

                                                           txtPet.Text = "#" + Convert.ToString(temp);
 


                                                    }
                                                    else
                                                        {
                                                          MessageBox.Show("This person is not registered on the system!");


                                                        }

                                                        con.Close();
                                                        con.Open();
                                                    
                                                    }
                                                    catch (Exception EX)
                                                    {
                                                        MessageBox.Show("An error occoured" + EX);

                                                    }
                                              


                                            }
                                            else
                                            {
                                                MessageBox.Show(" ID is invalid!!!");
                                            }

                                        }
                                    }

                                }
                                else
                                {
                                    MessageBox.Show(randomA + " ID is invalid!!!");
                                }

                            }
                            else
                            {
                                MessageBox.Show(citizenship + " ID is invalid!!!");
                            }
                        }
                        else
                        {
                            MessageBox.Show(genderr + "  " + "ID is invalid!!!");
                        }

                    }
                    else
                    {
                        MessageBox.Show(day + "  " + "ID is invalid!!!");
                    }
                }
                else
                {
                    MessageBox.Show(month + "  " + "ID is invalid!!!");
                }



            }
            else
            {
                MessageBox.Show("ID number must be 13 digits!");

            }




        }

      

        private void button4_Click(object sender, EventArgs e)
        {

            if (!(txtId.Text == "" && txtPet.Text == ""))
            {
                try
                {
                    OleDbConnection con = new OleDbConnection();
                    con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
                    con.Open();

                    OleDbCommand command = new OleDbCommand();
                    command.Connection = con;
                   


                  //  MessageBox.Show("id as been assign they can now fully register there pet");
                   command.CommandText = "SELECT * FROM Assigned_Pet_IDs where petID ='" + txtPet.Text + "' and OwerID ='" + txtId.Text + "';";


                    OleDbDataReader reader = command.ExecuteReader();
                      
                    if (reader.Read())
                    {


                        MessageBox.Show("This pet has already been assigned to you !");


                    }
                    else
                        {
                        con.Close();
                        con.Open();

                        MessageBox.Show("Pet ID has been assigned, they can now fully register the pet");
                        command.CommandText = "SELECT * FROM Assigned_Pet_IDs where petID ='" + txtPet.Text + "' and OwerID='" + txtId.Text + "';";
                        command.ExecuteNonQuery();

                        command.CommandText = "INSERT INTO `Assigned_Pet_IDs` (`petID`, `OwerID`) VALUES ('" + txtPet.Text + "','" + txtId.Text + "')";
                        command.ExecuteNonQuery();
                    }

                    con.Close();
                 

                }
                catch (Exception EX)
                {
                    MessageBox.Show("An error occoured" + EX);

                }
            }
            else
            {
                MessageBox.Show("Owner ID and pet ID must be filled!!!");
            }





        }

        private void Check_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text == ""))
            {
               
                try
                {
                    OleDbConnection con = new OleDbConnection();
                    con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
                    con.Open();

                    OleDbCommand command = new OleDbCommand();
                    command.Connection = con;




                    //  MessageBox.Show("id as been assign they can now fully register there pet");
                    command.CommandText = "SELECT  * FROM Pets WHERE Pet_ID = '"+textBox1.Text+"' ";
                  
                    OleDbDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {

                        OWNER = reader["Owner_ID"].ToString();
                  
                    }
                    
                    con.Close();
                    con.Open();

                    //  MessageBox.Show("id as been assign they can now fully register there pet");
                    command.CommandText = "SELECT ID_number, Name, Surname, Gender, Address, Contact_No FROM owners WHERE ID_number = '" + OWNER+" '";


                    reader = command.ExecuteReader();

                    while(reader.Read())
                    {

                        label12.Text = reader["ID_number"].ToString();
                        label10.Text = reader["Name"].ToString();
                        label11.Text = reader["Surname"].ToString();
                        label13.Text = reader["Gender"].ToString();
                        label9.Text = reader["Address"].ToString();
                        label8.Text = reader["Contact_No"].ToString();



                    }

                    con.Close();
                    con.Open();

                    command.CommandText = "SELECT status FROM Pets WHERE pet_ID ='"+textBox1.Text+ "' ";

                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {

                        pictureValue = reader["status"].ToString();
                        

                    }
                   ////////
                    if (pictureValue.Equals("good"))
                    {
                        pictureBox1.Image= Properties.Resources.tick;
                        label14.Text = "Good";
                        panel1.BackColor = Color.FromArgb(120, Color.Green);
                    }
                    else if(pictureValue.Equals("missing"))
                    {
                        label14.Text = "Missing";
                        pictureBox1.Image = Properties.Resources.missing;
                        panel1.BackColor = Color.FromArgb(120, Color.Red);
                    }
                    else if (pictureValue.Equals("stolen"))
                    {
                        label14.Text = "Stolen";
                        pictureBox1.Image = Properties.Resources.stolen;
                        panel1.BackColor = Color.FromArgb(120, Color.Red);
                    }
                    else if (pictureValue.Equals("recovered"))
                    {
                        label14.Text = "Recovered";
                        pictureBox1.Image = Properties.Resources.recovered;
                        panel1.BackColor = Color.FromArgb(120, Color.Green);
                    }
                    con.Close();
                    

                }
                catch (Exception EX)
                {
                    MessageBox.Show("An error occoured" + EX);

                }
            }
            else
            {
                MessageBox.Show("Pet ID must be filled!!!");
            }

        }

        private void txtStatus_Click(object sender, EventArgs e)
        {
            cc.Show();
            panel5.Hide();
           // panelstatusCheck.Visible = true;
            panelstatusCheck.Show();
            

        }

      

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage hpage = new HomePage();
            hpage.Show();
        }

        private void LogOutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage hpage = new HomePage();
            hpage.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cc.Show();
         
            // panelstatusCheck.Visible = true;
            panelstatusCheck.Show();
            panel5.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox2.Text = row.Cells["ID_number"].Value.ToString();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
                con.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = con;



                command.CommandText = "SELECT ID_Number From owners WHERE ID_Number = '"+textBox2.Text+"';";
                OleDbDataReader reader = command.ExecuteReader();

               while (reader.Read())
                {

                    see = true;
                   
                }

                con.Close();
                con.Open();

               




                if (see==true)
                {


                    command.CommandText = "SELECT ID_number, Name , Surname , Address, Contact_No,gender, [Password] FROM owners where Id_number ='" + x + "';";
                    command.ExecuteNonQuery();

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridView1.DataSource = table;

                    con.Close();
                }
                else
                {
                    MessageBox.Show("THIS ID IS NOT REGISTERED ON THE SYSTEM !");
                }


            }
            catch (Exception EX)
            {
                MessageBox.Show("error " + EX);
            }


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {

                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
                con.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = con;




                command.CommandText = "SELECT ID_Number From owners WHERE ID_Number = '" + textBox2.Text + "';";
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    saw = true;

                }

                con.Close();
                con.Open();






                if (saw == true)
                {


                    command.CommandText = "select * FROM PetS where Owner_ID ='" + textBox2.Text + "';";
                    command.ExecuteNonQuery();

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridView1.DataSource = table;

                    con.Close();
                }
                else
                {
                    MessageBox.Show("THIS ID IS NOT REGISTERED ON THE SYSTEM !");
                }


                con.Close();



              


            }
            catch (Exception EX)
            {
                MessageBox.Show("error " + EX);
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            try
            {
                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database.accdb;  Persist Security Info = False; ";
                con.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = con;

                command.CommandText = "SELECT * from Pets WHERE Pet_ID = '" + textBox1.Text + "';";


                OleDbDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    seex = true;

                }
               
                con.Close();
                con.Open();
                if (seex)
                {

                    command.CommandText = "UPDATE Pets SET statusByVet = '"+textBox3.Text+ "' where Pet_ID = '" + textBox1.Text + "';";
                    command.ExecuteNonQuery();

                    MessageBox.Show("status updated!");

                }
                con.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show("an error occoured" + EX);

            }

        }
    }
}
