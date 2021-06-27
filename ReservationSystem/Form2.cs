using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using System.IO;

namespace DivadloForm
{
    
    public partial class Form2 : Form
    {
        public string newHall;
        public Form2()
        {
            InitializeComponent();

        }
        public static bool isNumeric(string s)
        {
            return int.TryParse(s, out int n);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./database.db";

            if (textBox2.Text != null && textBox3.Text != null && isNumeric(textBox2.Text) == true && isNumeric(textBox3.Text) == true && hallName.Text != null )
            {
                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var insertCmd = connection.CreateCommand();
                        insertCmd.CommandText = "INSERT INTO Halls (name,row,column) VALUES($name,$row,$column)";
                        insertCmd.Parameters.AddWithValue("$name", hallName.Text);
                        insertCmd.Parameters.AddWithValue("$row", textBox2.Text);
                        insertCmd.Parameters.AddWithValue("$column", textBox3.Text);
                        insertCmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                }



                newHall = hallName.Text;
                this.Close();
            }
            else
            {
                string error = "0";

                if (isNumeric(textBox2.Text))
                {
                    error += "1";
                }
                if (isNumeric(textBox3.Text))
                {
                    error += "2";
                }
                if (textBox3.Text == null)
                {
                    error += "3";
                }
                if (hallName.Text == null)
                {
                    error += "4";
                }
                label3.Text = error;
            }

        }
    }
}
