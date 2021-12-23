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
    public partial class Form1 : Form
    {
        
        void get_Info(ListView List)
        {
            string query = "select*from k_info";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.CommandTimeout = 60;
            try
            {
                conn.Open();
                MySqlDataReader rd = cmDB.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        string[] row = { rd.GetString(0), rd.GetString(1), rd.GetString(2), rd.GetString(3), rd.GetString(4), rd.GetString(5), rd.GetString(6) };
                        var listItem = new ListViewItem(row);
                        List.Items.Add(listItem);
                    }
                }
                List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Form1()
        {
            InitializeComponent();
            get_Info(list);

        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            list.Items.Clear();
            get_Info(list);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            list.Items.Clear();
            string query = "delete from k_info where Id = " + list.Items[list.SelectedIndices[0]].Text + ";";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.CommandTimeout = 60;
            try
            {
                conn.Open();
                MySqlDataReader rd = cmDB.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            add_change Win = new add_change("add", 0);
            Win.Show();
            list.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            add_change Win = new add_change("change", Convert.ToInt32(Convert.ToString(list.Items[list.SelectedIndices[0]].Text)));
            Win.Show();
            list.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SearchInfo Win = new SearchInfo();
            Win.Show();
            list.Items.Clear();
        }
    }
    }

