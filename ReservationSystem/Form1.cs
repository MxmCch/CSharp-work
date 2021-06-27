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
    public partial class Form1 : Form
    {
        private static string hallID = "";
        public Form1()
        {
            InitializeComponent();
            this.Text = "Divadlo Reservation";

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./database.db";
            if (!File.Exists("database.db"))
            {
                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    //Create table
                    var tableCmd = connection.CreateCommand();
                    tableCmd.CommandText = "CREATE TABLE Reservation (id INTEGER PRIMARY KEY AUTOINCREMENT,reason CHAR(50) NOT NULL ,seat CHAR(50) NOT NULL,hall CHAR(50) NOT NULL);";
                    tableCmd.ExecuteNonQuery();
                }
                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    //Create table
                    var tableCmd = connection.CreateCommand();
                    tableCmd.CommandText = "CREATE TABLE Halls (id INTEGER PRIMARY KEY AUTOINCREMENT,name CHAR(50) NOT NULL ,row CHAR(50) NOT NULL, column CHAR(50) NOT NULL);";
                    tableCmd.ExecuteNonQuery();
                }
            }

            //refreshne list se jmeny hal, jsem linej
            RefreshList();


            //animace na zacatku
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 3000;
            timer.Tick += timer_enable;
            timer.Start();
            nadpisPanel.Size = new Size(this.Width, 594);

            void timer_enable(object sender, System.EventArgs e)
            {
                timer.Stop();
                int vyska = 594;
                int shrink = 1;
                int bonus = 100;
                while (vyska >= 115)
                {
                    vyska -= shrink / 200;
                    shrink += bonus / 50;
                    bonus += 2;
                    nadpisPanel.Size = new Size(this.Width, vyska);
                    Application.DoEvents();
                }
            }
        }

        private void react(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            bool changeCheck = false;
            using (var form3 = new Form3(btn.Name, hallID))
            {
                form3.Text = "Seat number: " + btn.Name;
                var result = form3.ShowDialog();
                if (result == DialogResult.OK)
                {
                    changeCheck = form3.somethingChanged;
                    label9.Text = form3.seatReason;
                }
            }

            if (btn.BackColor == Color.Red && changeCheck == true)
            {
                btn.BackColor = Color.Green;
            }
            else if (btn.BackColor == Color.Green && changeCheck == true)
            {
                btn.BackColor = Color.Red;
            }
            label4.Text = btn.Name;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            //after closing new opened window the code continues after
            form2.ShowDialog();
            //public newHall, is a return string that returns when dialog is closed and is set
            if (String.IsNullOrEmpty(form2.newHall) != true)
            {
                listBox1.Items.Add(form2.newHall);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./database.db";


            hallID = listBox1.Items[listBox1.SelectedIndex].ToString();

            int row = 0;
            int column = 0;
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"SELECT * FROM Halls WHERE name = $name";
                selectCmd.Parameters.AddWithValue("$name", hallID);
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hallID = reader["id"].ToString();
                        row = Int32.Parse(reader["row"].ToString());
                        column = Int32.Parse(reader["column"].ToString());
                    }
                }
            }

            string reservedSeat = "";
            connectionStringBuilder.DataSource = "./database.db";
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"SELECT * FROM Reservation WHERE hall = $name";
                selectCmd.Parameters.AddWithValue("$name", hallID);
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservedSeat += $"{reader["seat"]},";
                    }
                }
            }
            string[] reservedSeatList = reservedSeat.Split(',');

            int reset = 0;
            int top = 10;
            int left = 10;

            for (int i = 0; i < (column*row); i++)
            {
                if (reset != column-1)
                {
                    Button button = new Button();
                    button.Left = left;
                    button.Top = top;
                    button.Text = (i + 1).ToString();
                    button.Size = new Size(47, 41);
                    button.Name = (i + 1).ToString();
                    button.BackColor = Color.Green;
                    foreach (var item in reservedSeatList)
                    {
                        if (item == (i+1).ToString())
                        {
                            button.BackColor = Color.Red;
                        }

                    }
                    button.Click += new EventHandler(this.react);
                    panel1.Controls.Add(button); // here
                    left += button.Width + 2;
                    reset++;
                }
                else
                {
                    Button button = new Button();
                    button.Left = left;
                    button.Top = top;
                    button.Text = (i + 1).ToString();
                    button.Size = new Size(47, 41);
                    button.Name = (i + 1).ToString();
                    button.BackColor = Color.Green;
                    foreach (var item in reservedSeatList)
                    {
                        if (item == (i + 1).ToString())
                        {
                            button.BackColor = Color.Red;
                        }

                    }
                    button.Click += new EventHandler(this.react);
                    panel1.Controls.Add(button); // here
                    top += button.Height + 2;
                    left = 10;
                    reset = 0;
                }
            }

        }

        private void RefreshList()
        {
            listBox1.Items.Clear();
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./database.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"SELECT * FROM Halls";
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader["name"]);

                    }
                }

            }
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void hallName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string remove = listBox1.SelectedItem.ToString();

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./database.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "DELETE FROM Halls WHERE name = $hall";
                selectCmd.Parameters.AddWithValue("$hall", remove);
                selectCmd.ExecuteNonQuery();
            }
            RefreshList();
            MessageBox.Show("Hall has been removed!");
        }
    }
}
