namespace facebook_paylasim
{
    partial class Paylasim
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
            this.SonIndexTxt = new System.Windows.Forms.TextBox();
            this.SonIndexLB = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.paylasimNvarchar = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // SonIndexTxt
            // 
            this.SonIndexTxt.Location = new System.Drawing.Point(12, 12);
            this.SonIndexTxt.Name = "SonIndexTxt";
            this.SonIndexTxt.Size = new System.Drawing.Size(100, 20);
            this.SonIndexTxt.TabIndex = 0;
            // 
            // SonIndexLB
            // 
            this.SonIndexLB.AutoSize = true;
            this.SonIndexLB.Location = new System.Drawing.Point(118, 12);
            this.SonIndexLB.Name = "SonIndexLB";
            this.SonIndexLB.Size = new System.Drawing.Size(55, 13);
            this.SonIndexLB.TabIndex = 1;
            this.SonIndexLB.Text = "Son İndex";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(156, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 55);
            this.button1.TabIndex = 2;
            this.button1.Text = "Paylasilacak Metin Ekle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // paylasimNvarchar
            // 
            this.paylasimNvarchar.Location = new System.Drawing.Point(12, 184);
            this.paylasimNvarchar.Multiline = true;
            this.paylasimNvarchar.Name = "paylasimNvarchar";
            this.paylasimNvarchar.Size = new System.Drawing.Size(138, 55);
            this.paylasimNvarchar.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 28);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(597, 508);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(136, 40);
            this.button3.TabIndex = 7;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(378, 28);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(242, 381);
            this.listBox1.TabIndex = 8;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(639, 28);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(350, 381);
            this.listBox2.TabIndex = 9;
            // 
            // Paylasim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 602);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.paylasimNvarchar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SonIndexLB);
            this.Controls.Add(this.SonIndexTxt);
            this.Name = "Paylasim";
            this.Text = "Paylasim";
            this.Load += new System.EventHandler(this.Paylasim_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SonIndexTxt;
        private System.Windows.Forms.Label SonIndexLB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox paylasimNvarchar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
    }
}