using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace httpsql
{
    public partial class Form2 : Form
    {
        public Form1 ParentForm { get; set; }
        public string rowID;
        public Form2(string id, string user_name, string user_email, string user_adress, string user_phone)
        {
            InitializeComponent();

            textBox1.Text = user_name;
            textBox2.Text = user_email;
            textBox3.Text = user_adress;
            textBox4.Text = user_phone;
            rowID = id;

        }

        public void button1_Click(object sender, EventArgs e)
        {
            var _cs = @"Server=mssql.sps-prosek.local;Database=cechma18;User ID=cechma18;Password=cechma18";
           
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE csharp_SQL SET user_name = @user_name, user_email = @user_email, user_adress = @user_adress, user_phone = @user_phone " + "WHERE id = @rowID", conn);
                cmd.Parameters.AddWithValue("@user_name", textBox1.Text);
                cmd.Parameters.AddWithValue("@user_email", textBox2.Text);
                cmd.Parameters.AddWithValue("@user_adress", textBox3.Text);
                cmd.Parameters.AddWithValue("@user_phone", textBox4.Text);
                cmd.Parameters.AddWithValue("@rowID", rowID);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            ParentForm.UpdateList();
            this.Close();
        }
    }
}
