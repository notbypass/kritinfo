using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace KritInfo
{
    public partial class add_change : Form
    {
        public string modeS = "";
        int item;
        void setMode(string mode)
        {
            if (mode == "add")
            {
                AddButton.Text = "Добавить";
            }
            else if (mode == "change")
            {
                AddButton.Text = "Изменить";
                string Info = "select id,FIO, Groupstudent,Godvipuska,Phone,Region,Specialnost from k_info where id =" + item.ToString() + ";";
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmInfo = new MySqlCommand(Info, conn);
                MySqlDataReader inRead;
                cmInfo.CommandTimeout = 60;
                try
                {
                    conn.Open();
                    inRead = cmInfo.ExecuteReader();
                    if (inRead.HasRows)
                    {
                        while (inRead.Read())
                        {
                            nameBox.Text = inRead.GetString(0);
                            textBox1.Text = inRead.GetString(1);
                            textBox2.Text = inRead.GetString(2);
                            textBox3.Text = inRead.GetString(3);
                            textBox4.Text = inRead.GetString(4);
                            textBox5.Text = inRead.GetString(5);
                            textBox6.Text = inRead.GetString(6);


                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void getNames(ComboBox Box)
        {
            string query = "select FIO from k_info;";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            MySqlDataReader rd;
            cmDB.CommandTimeout = 60;
            try
            {
                conn.Open();
                rd = cmDB.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        string row = rd.GetString(0);
                        Box.Items.Add(row);
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public add_change(string mode, int id)
        {
            InitializeComponent();
       
            modeS = mode;
            item = id;
            setMode(mode);
            
        }


   

        private void AddButton_Click_1(object sender, EventArgs e)
        {

            if (modeS == "add")
            {
                
                string query = "insert into k_info(id,FIO,Groupstudent,Godvipuska,Phone,Region,Specialnost) values('" + nameBox.Text + "', '" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "');";
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmDB = new MySqlCommand(query, conn);
                cmDB.CommandTimeout = 60;
                try
                {
                    conn.Open();
                    MySqlDataReader rd = cmDB.ExecuteReader();
                    conn.Close();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (modeS == "change")
            {
               
                string query = "update k_info set FIO ='" + textBox1.Text + "',Groupstudent ='" + textBox2.Text + "',Godvipuska = '" + textBox3.Text + "',Phone = '" + textBox4.Text + "', Region ='" + textBox5.Text + "', Specialnost ='" + textBox6.Text + "' where id =" + item.ToString() + ";";
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmDB = new MySqlCommand(query, conn);
                cmDB.CommandTimeout = 60;
                try
                {
                    conn.Open();
                    MySqlDataReader rd = cmDB.ExecuteReader();
                    conn.Close();

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }
    }
}
