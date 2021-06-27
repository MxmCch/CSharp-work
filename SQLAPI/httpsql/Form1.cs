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
    public partial class Form1 : Form
    {
        public string[] cutters;
        public Form1()
        {
            InitializeComponent();
            textBox6.UseSystemPasswordChar = true;
            textBox6.PasswordChar = '\0';

            var task = Task.Run(() => GetData());
            task.Wait();

            UpdateList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Add button, adds values from textboxes and then clears them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            bool isEmptyInput = false;
            bool loginTrue = false;

            string[] userInputs =
            {
                textBox1.Text,
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox6.Text
            };

            foreach (string item in userInputs)
            {
                if (string.IsNullOrEmpty(item))
                {
                    isEmptyInput = true;
                }
            }

            if (textBox5.Text == "jmeno" && textBox6.Text == "heslo")
            {
                loginTrue = true;
            }
            else
            {
                MessageBox.Show("Login is wrong!");
                return;
            }
            
            loginTrue = true;

            if (!isEmptyInput && loginTrue)
            {
                // task pro asynchorni operace - pozadavek na http
                var task = Task.Run(() => Login(userInputs[0], userInputs[1], userInputs[2], userInputs[3]));
                task.Wait();

                UpdateList();
            }
            else
            {
                if (string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text))
                {
                    MessageBox.Show("Login is empty!");
                }
                else
                {
                    MessageBox.Show("One of the inputs is empty!");
                }
            }
        }

        /// <summary>
        /// Refreshes table with data
        /// </summary>
        public void UpdateList()
        {
            var _cs = @"Server=mssql.sps-prosek.local;Database=cechma18;User ID=cechma18;Password=cechma18";

            using var _con = new SqlConnection(_cs);
            _con.Open();


            var _sql = "SELECT * FROM [dbo].[csharp_SQL] ";
            using var _cmd = new SqlCommand(_sql, _con);


            listBox1.Items.Clear();
            SqlDataReader _read = _cmd.ExecuteReader();
            while (_read.Read())
            {
                string listLine = "";
                listLine += _read.GetValue(0).ToString() + " - ";
                listLine += _read.GetString(1).ToString() + " - ";
                listLine += _read.GetString(2).ToString() + " - ";
                listLine += _read.GetString(3).ToString() + " - ";
                listLine += _read.GetString(4).ToString();


                listBox1.Name = _read.GetValue(0).ToString();
                listBox1.Items.Add(listLine);

                this.Controls.Add(listBox1);
            }
            TextBoxClear();
        }

        /// <summary>
        /// just clears inputs
        /// </summary>
        private void TextBoxClear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        public void Login(string user, string mail, string adress, string phone)
        {

            var _cs = @"Server=mssql.sps-prosek.local;Database=cechma18;User ID=cechma18;Password=cechma18";

            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var cmd = new SqlCommand("insert into csharp_SQL values (@user_name, @user_email,@user_adress,@user_phone)", conn);
                cmd.Parameters.AddWithValue("@user_name", user);
                cmd.Parameters.AddWithValue("@user_email", mail);
                cmd.Parameters.AddWithValue("@user_adress", adress);
                cmd.Parameters.AddWithValue("@user_phone", phone);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        /// <summary>
        /// not sure if this does anything but too scared to remove it
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetData()
        {
            var userName = "seminar";
            var passwd = "prosek";
            var token = "wfnESsx0hKYyst0DOdQd8uSCVrLPQb7gwPJMeMfe5OxfFEsjo4";

            using (HttpClient http = new HttpClient())
            {
                var data = new List<KeyValuePair<string, string>>();
                data.Add(new KeyValuePair<string, string>("token", token));
                // odeslani pozadavku na url pres post
                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                HttpResponseMessage resp = await http.PostAsync(new Uri("https://cechma18.sps-prosek.cz/PHPAPI/get.php"), new FormUrlEncodedContent(data));
                string retstr = await resp.Content.ReadAsStringAsync();

                return retstr;
            }
        }

        public static string Base64Encode(string textToEncode)
        {
            byte[] textAsBytes = Encoding.UTF8.GetBytes(textToEncode);
            return Convert.ToBase64String(textAsBytes);
        }

        // trida pro zpravy, nas web vzdy vraci status a msg 
        public class JsonMsg
        {
            public int ID { get; set; }
            public string user_name { get; set; }
            public string user_email { get; set; }
            public string user_adress { get; set; }
            public string user_phone { get; set; }
        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string cutter = listBox1.Items[listBox1.SelectedIndex].ToString();
                cutters = cutter.Split(" - ");
                label7.Text = cutters[0];
            }
        }

        /// <summary>
        /// Edit button, shows new form for with updating insert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button2_Click(object sender, EventArgs e)
        {
            if (cutters == null || cutters.Length == 0)
            {
                return;
            }
            Form2 form2 = new Form2(cutters[0], cutters[1], cutters[2], cutters[3], cutters[4]);
            form2.ParentForm = this;
            form2.Show();
            UpdateList();
        }

        /// <summary>
        /// Deletes selected row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            var _cs = @"Server=mssql.sps-prosek.local;Database=cechma18;User ID=cechma18;Password=cechma18";

            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM csharp_SQL WHERE id = @rowID", conn);
                cmd.Parameters.AddWithValue("@rowID", cutters[0]);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            UpdateList();
        }
    }
}
