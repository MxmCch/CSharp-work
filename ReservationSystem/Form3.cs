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
    public partial class Form3 : Form
    {
        private string seatNum;
        private string hallID;
        private string oldHallID;
        private string oldSeatNum;
        private string oldReason;
        private int radioChecked;
        public bool somethingChanged;
        public string seatReason;
        public Form3(string abc, string abcd)
        {
            InitializeComponent();
            seatNum = abc;
            hallID = abcd;
            oldHallID = "";
            oldSeatNum = "";
            oldReason = "";
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;


            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./database.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"SELECT * FROM Reservation WHERE hall = $name AND seat = $seat";
                selectCmd.Parameters.AddWithValue("$name", hallID);
                selectCmd.Parameters.AddWithValue("$seat", seatNum);
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oldHallID = reader["hall"].ToString();
                        oldSeatNum = reader["seat"].ToString();
                        oldReason = reader["reason"].ToString();
                    }
                }
            }
            Console.WriteLine("hala" + oldHallID);
            Console.WriteLine("sedadlo" + oldSeatNum);
            Console.WriteLine("duvod" + oldReason);
            if (!String.IsNullOrWhiteSpace(oldReason))
            {
                if (oldReason == "Bought")
                {
                    radioBought.Checked = true;
                }
                else if (oldReason == "Broken")
                {
                    radioBroken.Checked = true;
                }
                else
                {
                    radioOther.Checked = true;
                    textBox1.Text = oldReason;
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.somethingChanged = false;
            if (oldReason != "Bought" && oldReason != "Broken")
            {
                this.seatReason = "Other";
            }
            else
            {
                this.seatReason = oldReason;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string reason = "";
            if (radioChecked == 1)
            {
                reason = "Bought";
            }
            else if (radioChecked == 2)
            {
                reason = "Broken";
            }
            else if (radioChecked == 3)
            {
                reason = textBox1.Text;
            }
            else
            {
                reason = null;
            }

            if (String.IsNullOrEmpty(oldSeatNum))
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder();
                connectionStringBuilder.DataSource = "./database.db";

                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var insertCmd = connection.CreateCommand();
                        insertCmd.CommandText = "INSERT INTO Reservation (reason,seat,hall) VALUES($reason,$seat,$hall)";
                        insertCmd.Parameters.AddWithValue("$reason", reason);
                        insertCmd.Parameters.AddWithValue("$seat", seatNum);
                        insertCmd.Parameters.AddWithValue("$hall", hallID);
                        insertCmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                }
                this.somethingChanged = true;
                if (oldReason != "Bought" && oldReason != "Broken")
                {
                    this.seatReason = "Other";
                }
                else
                {
                    this.seatReason = oldReason;
                }
                this.DialogResult = DialogResult.OK;
                MessageBox.Show("Reservation has been made!");
                this.Close();
            }
            else
            {
                //Update

                var connectionStringBuilder = new SqliteConnectionStringBuilder();
                connectionStringBuilder.DataSource = "./database.db";

                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var insertCmd = connection.CreateCommand();
                        insertCmd.CommandText = "UPDATE Reservation SET reason = @reason, seat = @seat, hall = @hall";
                        insertCmd.Parameters.AddWithValue("@reason", reason);
                        insertCmd.Parameters.AddWithValue("@seat", seatNum);
                        insertCmd.Parameters.AddWithValue("@hall", hallID);
                        insertCmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                }
                this.somethingChanged = false;
                if (oldReason != "Bought" && oldReason != "Broken")
                {
                    this.seatReason = "Other";
                }
                else
                {
                    this.seatReason = oldReason;
                }
                this.DialogResult = DialogResult.OK;
                MessageBox.Show("Reservation has been updated!");
                this.Close();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioChecked = 1;
            textBox1.ReadOnly = true;
            textBox1.BackColor = Color.Gray;
        }
        private void radioBroken_CheckedChanged(object sender, EventArgs e)
        {
            radioChecked = 2;
            textBox1.ReadOnly = true;
            textBox1.BackColor = Color.Gray;
        }

        private void radioOther_CheckedChanged(object sender, EventArgs e)
        {
            radioChecked = 3;
            textBox1.ReadOnly = false;
            textBox1.BackColor = Color.White;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./database.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "DELETE FROM Reservation WHERE hall = $hall AND seat = $seat";
                selectCmd.Parameters.AddWithValue("$hall", hallID);
                selectCmd.Parameters.AddWithValue("$seat", seatNum);
                selectCmd.ExecuteNonQuery();
            }
            this.somethingChanged = true;
            this.seatReason = "Avaliable";
            this.DialogResult = DialogResult.OK;
            MessageBox.Show("This reservation had been deleted!");
            this.Close();
        }
    }
}
