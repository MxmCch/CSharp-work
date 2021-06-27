
namespace DivadloForm
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioBought = new System.Windows.Forms.RadioButton();
            this.radioBroken = new System.Windows.Forms.RadioButton();
            this.radioOther = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioBought
            // 
            this.radioBought.AutoSize = true;
            this.radioBought.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radioBought.Location = new System.Drawing.Point(12, 31);
            this.radioBought.Name = "radioBought";
            this.radioBought.Size = new System.Drawing.Size(59, 17);
            this.radioBought.TabIndex = 0;
            this.radioBought.TabStop = true;
            this.radioBought.Text = "Bought";
            this.radioBought.UseVisualStyleBackColor = true;
            this.radioBought.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioBroken
            // 
            this.radioBroken.AutoSize = true;
            this.radioBroken.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radioBroken.Location = new System.Drawing.Point(12, 54);
            this.radioBroken.Name = "radioBroken";
            this.radioBroken.Size = new System.Drawing.Size(59, 17);
            this.radioBroken.TabIndex = 1;
            this.radioBroken.TabStop = true;
            this.radioBroken.Text = "Broken";
            this.radioBroken.UseVisualStyleBackColor = true;
            this.radioBroken.CheckedChanged += new System.EventHandler(this.radioBroken_CheckedChanged);
            // 
            // radioOther
            // 
            this.radioOther.AutoSize = true;
            this.radioOther.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radioOther.Location = new System.Drawing.Point(12, 77);
            this.radioOther.Name = "radioOther";
            this.radioOther.Size = new System.Drawing.Size(54, 17);
            this.radioOther.TabIndex = 2;
            this.radioOther.TabStop = true;
            this.radioOther.Text = "Other:";
            this.radioOther.UseVisualStyleBackColor = true;
            this.radioOther.CheckedChanged += new System.EventHandler(this.radioOther_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(72, 77);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(238, 99);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(235, 228);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(316, 228);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 228);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(81)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(403, 263);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.radioOther);
            this.Controls.Add(this.radioBroken);
            this.Controls.Add(this.radioBought);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioBought;
        private System.Windows.Forms.RadioButton radioBroken;
        private System.Windows.Forms.RadioButton radioOther;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}