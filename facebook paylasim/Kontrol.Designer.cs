namespace facebook_paylasim
{
    partial class Kontrol
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
            this.components = new System.ComponentModel.Container();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.CauntDegerLB = new System.Windows.Forms.Label();
            this.bekletdegerlb = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(212, 12);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(868, 573);
            this.webBrowser1.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 100);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(194, 485);
            this.listBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Başlat";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CauntDegerLB
            // 
            this.CauntDegerLB.AutoSize = true;
            this.CauntDegerLB.Location = new System.Drawing.Point(141, 18);
            this.CauntDegerLB.Name = "CauntDegerLB";
            this.CauntDegerLB.Size = new System.Drawing.Size(35, 13);
            this.CauntDegerLB.TabIndex = 3;
            this.CauntDegerLB.Text = "label1";
            // 
            // bekletdegerlb
            // 
            this.bekletdegerlb.AutoSize = true;
            this.bekletdegerlb.Location = new System.Drawing.Point(40, 74);
            this.bekletdegerlb.Name = "bekletdegerlb";
            this.bekletdegerlb.Size = new System.Drawing.Size(35, 13);
            this.bekletdegerlb.TabIndex = 4;
            this.bekletdegerlb.Text = "label1";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(106, 74);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 5;
            this.textBox4.Text = "200";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 34);
            this.button2.TabIndex = 6;
            this.button2.Text = "Durdur";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Kontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 617);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.bekletdegerlb);
            this.Controls.Add(this.CauntDegerLB);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.webBrowser1);
            this.Name = "Kontrol";
            this.Text = "Kontrol";
            this.Load += new System.EventHandler(this.Kontrol_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label CauntDegerLB;
        private System.Windows.Forms.Label bekletdegerlb;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button2;
    }
}