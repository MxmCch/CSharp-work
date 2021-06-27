using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
        Pen p = new Pen(Color.Red, 3);
        SolidBrush sb = new SolidBrush(Color.Red);
        public string shapeBtn;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Obrazce";
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.helpProvider1.SetHelpString(button1,
                "Maxim Čech - " + "Zde si vyberte tvar"
                );
            this.helpProvider2.SetHelpString(textBox,
                "Maxim Čech - " + "Zde zadejte parametry"
                );
            this.helpProvider3.SetHelpString(panel1,
                "Maxim Čech - " + "Zde se zobrazuji tvary"
                );
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        //funkce ktera enabluje textBoxy aby se do nich dalo psat podle toho jaky tvar to je
        public void textEnabler(bool text0, bool text1, bool text2, bool text3, bool text4)
        {
            panel1.Refresh();
            textBox.Enabled = text0;
            textBox1.Enabled = text1;
            textBox2.Enabled = text2;
            textBox3.Enabled = text3;
            textBox4.Enabled = text4;
            textBox.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            labelReset();
        }
        public void labelReset()
        {
            label8.Text = "";
            label9.Text = "";
        }
        public class Tvar
        {
            protected string barva;
            protected string tloustka;
            protected bool vypln;
            protected float obsah;
            protected float obvod;
            protected string nejdelsi;
            private string tvar;

            public string tvarp
            {
                get { return tvar; }
            }

            public Tvar(string barva, float tloustka, bool vypln)
            {
                this.barva = barva;
                this.tloustka = tloustka.ToString();
                this.vypln = vypln;

                Zadej_Tvar();
            }

            public string Vrat_Barvu()
            {
                return this.barva;
            }

            public float Vrat_Obsah()
            {
                return this.obsah;
            }

            public float Vrat_Obvod()
            {
                return this.obvod;
            }

            public virtual string Vrat_Stranu(float[] strany)
            {
                string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                if (this is Kruh)
                {
                    this.nejdelsi = "nema stranu";
                    return this.nejdelsi;
                }
                float x = 0f;
                float nejvetsi = 0f;
                foreach (var item in strany)
                {
                    if (item >= nejvetsi)
                    {
                        nejvetsi = item;
                    }
                }
                foreach (var item in strany)
                {
                    if (item >= nejvetsi)
                    {
                        this.nejdelsi += alphabet[Int32.Parse(x.ToString())];
                    }
                    x++;
                }
                return this.nejdelsi;
            }
            public virtual string Vrat_Tloustku()
            {
                return this.tloustka;
            }

             private void Zadej_Tvar()
            {
                if (this is Ctverec)
                {
                    this.tvar = "ctverec";
                }

                if (this is Obdelnik)
                {
                    this.tvar = "obdelnik";
                }

                if (this is Kruh)
                {
                    this.tvar = "kruh";
                }

                if (this is Trojuhelnik)
                {
                    this.tvar = "trojuhelnik";
                }
            }
        }

        public class Ctverec : Tvar
        {
            private float strana;

            public Ctverec(string barva, float tloustka, bool vypln, float strana) : base(barva, tloustka, vypln)
            {
                this.strana = strana;

                Vypocti_Obsah();
                Vypocti_Obvod();
            }

            private void Vypocti_Obsah()
            {
                this.obsah = strana * strana;
            }

            private void Vypocti_Obvod()
            {
                this.obvod = 4 * strana;
            }
        }

        public class Obdelnik : Tvar
        {
            private float strana_a;
            private float strana_b;

            public Obdelnik(string barva, float tloustka, bool vypln, float strana_a, float strana_b) : base(barva, tloustka, vypln)
            {
                this.strana_a = strana_a;
                this.strana_b = strana_b;

                Vypocti_Obsah();
                Vypocti_Obvod();
            }
            private void Vypocti_Obsah()
            {
                this.obsah = strana_a * strana_b;
            }

            private void Vypocti_Obvod()
            {
                this.obvod = (2 * strana_a) + (2 * strana_b);
            }
        }

        public class Kruh : Tvar
        {
            private float polomer;

            public Kruh(string barva, float tloustka, bool vypln, float polomer) : base(barva, tloustka, vypln)
            {
                this.polomer = polomer;

                Vypocti_Obsah();
                Vypocti_Obvod();
            }
            private void Vypocti_Obsah()
            {
                this.obsah = (float)Math.PI * (float)Math.Pow(polomer, 2);
            }

            // Math.PI a Math.Pow vraci double, proto pretypvani na float - atribut obsah je float (vypocet neni presny kvuli pretypovani)
            private void Vypocti_Obvod()
            {
                this.obvod = 2 * (float)Math.PI * polomer;
            }

            // nahradi metodu Vrat_Tloustku() z objektu Tvar -> "override", ve objektu tvar musi byt Vrat_Tloustku oznaceno "virtual"
            public override string Vrat_Tloustku()
            {
                return "neexsituje tloustka pro tento tvar";
            }
        }
        // abc123
        public class Trojuhelnik : Tvar
        {
            private float strana_a;
            private float strana_b;
            private float strana_c;
            private float vyska_a;
            public Trojuhelnik(string barva, float tloustka, bool vypln, float strana_a, float strana_b, float strana_c, float vyska_a) : base(barva, tloustka, vypln)
            {
                this.strana_a = strana_a;
                this.strana_b = strana_b;
                this.strana_c = strana_c;
                this.vyska_a = vyska_a;

                Vypocti_Obsah();
                Vypocti_Obvod();
            }
            private void Vypocti_Obsah()
            {
                this.obsah = (strana_a / 2) * vyska_a;
            }

            private void Vypocti_Obvod()
            {
                this.obvod = strana_a + strana_b + strana_c;
            }
        }
        //end OOP
        private void button1_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click_1(object sender, EventArgs e)
        { 
        }

        //funkce ktera meni velikost tvaru aby se vesel do panelu
        public float sizeLimiter(bool width, bool height, float currentSize)
        {
            if (width == true)
            {
                while (currentSize >= panel1.Width)
                {
                    currentSize = currentSize / 3 * 2;
                }
            }
            else if (height == true)
            {
                while (currentSize >= panel1.Height)
                {
                    currentSize = currentSize / 3 * 2;
                }
            }
            return currentSize;
        }

        private void TextChange()
        {
            string comboSelected = shapeBtn;
            if (comboSelected.ToString() == "Čtverec")
            {
                if (IsDigitsOnly(textBox.Text) && !string.IsNullOrEmpty(textBox.Text))
                {
                    panel1.Refresh();
                    panel1.Controls.Clear();

                    float strana_a = float.Parse(textBox.Text);
                    float strana_a_text = strana_a;

                    Label labelX = new Label();
                    labelX.Text = strana_a.ToString();

                    Tvar c = new Ctverec("null", 0, false, strana_a);
                    label8.Text = c.Vrat_Obvod().ToString();
                    label9.Text = c.Vrat_Obsah().ToString();

                    strana_a = sizeLimiter(true, false, strana_a);

                    float sirka = (panel1.Width) / 2 - strana_a / 2;
                    float vyska = (panel1.Height) / 2 - strana_a / 2;
                    float sirkaPlus = (panel1.Width) / 2 + strana_a / 2;
                    if (strana_a_text >= 580)
                    {
                        sirkaPlus = 330;
                    }
                    float vyskaPlus = (panel1.Height) / 2 + 8;

                    labelX.Left = (int)sirkaPlus;
                    labelX.Top = (int)vyskaPlus;
                    panel1.Controls.Add(labelX);


                    RectangleF shape = new RectangleF(sirka, vyska, strana_a, strana_a);
                    Graphics g = panel1.CreateGraphics();
                    g.FillRectangle(sb, shape);
                }
            }
            else if (comboSelected.ToString() == "Obdelník")
            {
                if (IsDigitsOnly(textBox.Text) && !string.IsNullOrEmpty(textBox.Text) && IsDigitsOnly(textBox1.Text) && !string.IsNullOrEmpty(textBox1.Text))
                {
                    panel1.Refresh();
                    panel1.Controls.Clear();

                    float strana_a = float.Parse(textBox.Text);
                    float strana_b = float.Parse(textBox1.Text);
                    float strana_a_text = strana_a;
                    float strana_b_text = strana_b;

                    //text lokace start
                    Label labelX = new Label();
                    labelX.Text = strana_a.ToString();
                    Label labelY = new Label();
                    labelY.Text = strana_b.ToString();
                    //text lokace end

                    Tvar c = new Obdelnik("null", 0, false, strana_a, strana_b);
                    label8.Text = c.Vrat_Obvod().ToString();
                    label9.Text = c.Vrat_Obsah().ToString();


                    strana_a = sizeLimiter(true, false, strana_a);
                    strana_b = sizeLimiter(false, true, strana_b);

                    float sirka = (panel1.Width) / 2 - strana_a / 2;
                    float vyska = (panel1.Height) / 2 - strana_b / 2;

                    //text lokace start
                    float sirkaPlus = (panel1.Width) / 2 + strana_a / 2;
                    if (strana_a_text >= 580)
                    {
                        sirkaPlus = 330;
                    }
                    float vyskaPlus = (panel1.Height) / 2 + 8;

                    float sirkaPlusY = (panel1.Width) / 2 + strana_b / 2;
                    if (strana_b_text >= 580)
                    {
                        sirkaPlusY = 400;
                    }
                    float vyskaPlusY = (panel1.Height) / 2 + 8;

                    labelX.Left = (int)sirkaPlus;
                    labelX.Top = (int)vyskaPlus;
                    panel1.Controls.Add(labelX);

                    labelY.Left = (int)vyskaPlusY;
                    labelY.Top = (int)sirkaPlusY;
                    panel1.Controls.Add(labelY);
                    //text lokace end

                    RectangleF shape = new RectangleF(sirka, vyska, strana_a, strana_b);
                    Graphics g = panel1.CreateGraphics();
                    g.FillRectangle(sb, shape);
                }
            }
            else if (comboSelected.ToString() == "Trojúhelník")
            {
                if (IsDigitsOnly(textBox.Text) && !string.IsNullOrEmpty(textBox.Text) && IsDigitsOnly(textBox1.Text) && !string.IsNullOrEmpty(textBox1.Text) && IsDigitsOnly(textBox2.Text) && !string.IsNullOrEmpty(textBox2.Text) && IsDigitsOnly(textBox3.Text) && !string.IsNullOrEmpty(textBox3.Text))
                {
                    panel1.Refresh();
                    panel1.Controls.Clear();

                    float strana_a = float.Parse(textBox.Text);
                    float strana_b = float.Parse(textBox1.Text);
                    float strana_c = float.Parse(textBox2.Text);
                    float vyska_a = float.Parse(textBox3.Text);

                    //text lokace start
                    Label labelW = new Label();
                    labelW.Text = strana_a.ToString();
                    Label labelX = new Label();
                    labelX.Text = strana_b.ToString();
                    Label labelY = new Label();
                    labelY.Text = strana_c.ToString();
                    Label labelZ = new Label();
                    labelZ.Text = vyska_a.ToString();
                    //text lokace end

                    Tvar c = new Trojuhelnik("null", 0, false, strana_a, strana_b, strana_c, vyska_a);
                    label8.Text = c.Vrat_Obvod().ToString();
                    label9.Text = c.Vrat_Obsah().ToString();

                    float sirka = (panel1.Width) / 2 - strana_a / 2;
                    float vyska = (panel1.Height) / 2 - vyska_a / 2;
                    float vyskaPlus = (panel1.Height) / 2 + vyska_a / 2;

                    float w = GetRatio_(strana_a, strana_b, true); //a
                    float x = GetRatio_(strana_a, strana_b, false); //b1
                    float y = GetRatio_(strana_b, strana_c, true); //b2
                    float z = GetRatio_(strana_b, strana_c, false); //c

                    float A = solveProportion(w, x, y, z, "A");
                    float B = solveProportion(w, x, y, z, "B");
                    float C = solveProportion(w, x, y, z, "C");


                    float x_nahore = 0;
                    float x_navic = 0;
                    if (C >= B)
                    {
                        float jedna_cast = strana_a / A;
                        x_nahore = strana_a / 2 + ((C - B) * jedna_cast);
                    }
                    else if (B >= C)
                    {
                        float jedna_cast = strana_a / A;
                        x_nahore = strana_a / 2 - ((B - C) * jedna_cast);
                        if (x_nahore < 0)
                        {
                            x_navic = x_nahore * -1;
                            x_nahore += x_navic;
                        }
                    }



                    //text lokace start
                    float kaka = ((0 + x_navic + sirka) + (0 + strana_a + x_navic + sirka)) / 2;
                    float kuku = ((0 + x_navic + sirka) + (0 + strana_a + x_navic + sirka)) / 2;
                    labelW.Left = (int)kaka;
                    float vyskaPlusW = vyskaPlus;
                    if (vyskaPlus >= 400)
                    {
                        vyskaPlusW = 400;
                    }
                    labelW.Top = (int)vyskaPlusW;
                    panel1.Controls.Add(labelW);


                    float xaxa = (vyska + vyskaPlus) / 2;
                    float xuxu = ((x_nahore + sirka) + (0 + strana_a + x_navic + sirka)) / 2 ;
                    labelX.Left = (int)xaxa;
                    labelX.Top = (int)(xuxu/1.1f);
                    panel1.Controls.Add(labelX);

                    /*
                    labelY.Left = (int)sirkaPlus;
                    labelY.Top = (int)vyskaPlus;
                    panel1.Controls.Add(labelY);

                    labelZ.Left = (int)vyskaPlusY;
                    labelZ.Top = (int)sirkaPlusY;
                    panel1.Controls.Add(labelZ);*/
                    //text lokace end


                    strana_a = sizeLimiter(true, false, Int32.Parse(strana_a.ToString()));
                    vyska_a = sizeLimiter(false, true, Int32.Parse(vyska_a.ToString()));

                    Graphics g = panel1.CreateGraphics();
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    PointF[] pts = new PointF[] {
                        new PointF(x_nahore + sirka, vyska)/*nahore*/,
                        new PointF(0+x_navic + sirka, vyskaPlus)/*levo*/,
                        new PointF(0+strana_a + x_navic + sirka, vyskaPlus)/*pravo*/ 
                    };
                    g.FillPolygon(new SolidBrush(Color.Red), pts);
                    GraphicsPath gp = new GraphicsPath();
                    gp.AddPolygon(pts);
                }
            }
            else if (comboSelected.ToString() == "Kruh")
            {
                if (IsDigitsOnly(textBox4.Text) && !string.IsNullOrEmpty(textBox4.Text))
                {
                    panel1.Refresh();
                    panel1.Controls.Clear();
                    Graphics g = panel1.CreateGraphics();
                    float polomer = float.Parse(textBox4.Text.ToString());

                    
                    Label labelX = new Label();
                    labelX.Text = polomer.ToString();

                    float sirkaPlus = (panel1.Width) / 2 + polomer / 2;
                    if (sirkaPlus >= 380)
                    {
                        sirkaPlus = 380;
                    }
                    float vyskaPlus = (panel1.Height) / 2 + 8;

                    labelX.Left = (int)sirkaPlus;
                    labelX.Top = (int)vyskaPlus;
                    panel1.Controls.Add(labelX);

                    Tvar c = new Kruh("null", 0, false, polomer);
                    label8.Text = c.Vrat_Obvod().ToString();
                    label9.Text = c.Vrat_Obsah().ToString();

                    polomer = sizeLimiter(true, false, polomer);
                    polomer = sizeLimiter(false, true, polomer);

                    g.FillEllipse(sb, panel1.Width / 2 - polomer / 2, panel1.Height / 2 - polomer / 2, polomer, polomer);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        public float GCD_(float a, float b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }
            if (a == 0)
            {
                return b;
            }
            else
            {
                return a;
            }
        }
        public float GetRatio_(float a, float b, bool a_b)
        {
            var gcd_ = GCD_(a, b);
            if (a_b == true)
            {
                return a / gcd_;
            }
            else
            {
                return b / gcd_;
            }
        }
        public float __gcd(float a, float b)
        {
            return b == 0 ? a : __gcd(b, a % b);
        }
        public float solveProportion(float a, float b1,float b2, float c, string strana)
        {
            float A = a * b2;
            float B = b1 * b2;
            float C = b1 * c;

            // To print the given proportion
            // in simplest form.
            float gcd = __gcd(__gcd(A, B), C);

            if (strana == "B")
            {
                return (B / gcd);
            }
            else if (strana == "C")
            {
                return (C / gcd);
            }
            else if (strana == "A")
            {
                return (A / gcd);
            }
            return A;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextChange();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextChange();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextChange();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextChange();
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            TextChange();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            shapeBtn = "Čtverec";
            textEnabler(true, false, false, false, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            shapeBtn = "Obdelník";
            textEnabler(true, true, false, false, false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            shapeBtn = "Trojúhelník";
            textEnabler(true, true, true, true, false);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            shapeBtn = "Kruh";
            textEnabler(false, false, false, false, true);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
