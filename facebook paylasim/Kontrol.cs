using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace facebook_paylasim
{
    public partial class Kontrol : Form
    {
        public Kontrol()
        {
            InitializeComponent();
        }
        public static SqlConnection baglanti = new SqlConnection(("Data Source=BADBOY-PC") + @"\" + ("SqlExpress; Initial Catalog=sst;User ID=sa;Password=1236asd"));
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        int paylasimbeklet = 0;
        int caunt = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            CauntDegerLB.Text = caunt.ToString();

            //değer saydir
            if (listBox1.Items.Count > caunt)
            {
                if (paylasimbeklet < Convert.ToInt32(textBox4.Text))
                {
                    paylasimbeklet++;
                    bekletdegerlb.Text = paylasimbeklet.ToString();
                }
                else
                {
                    paylasimbeklet = 1;
                    bekletdegerlb.Text = paylasimbeklet.ToString();
                }
            }
            else
            {
                timer1.Stop();
                bekletdegerlb.Text = paylasimbeklet.ToString();
                caunt = 0;
                listBox1.SelectedIndex = 0;
            }


            //değer oku ve işlem yap

            bekletdegerlb.Text = paylasimbeklet.ToString();
            if (paylasimbeklet == 2)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (paylasimbeklet == 6)
            {
                girisyap();
            }
            if (paylasimbeklet == 12)
            {
                cikis();
            }
            if (paylasimbeklet == 14)
            {
                webBrowser1.Navigate("");
                paylasimbeklet = 0;
                caunt++;
            }

        }
        string eposta = "";
        void girisyap()
        {
            try
            {
                eposta = listBox1.Items[caunt].ToString();
                webBrowser1.Document.GetElementById("email").InnerText = eposta;
                webBrowser1.Document.GetElementById("pass").InnerText = "1236asd";
                webBrowser1.Document.Forms[0].InvokeMember("submit");
            }
            catch (Exception)
            {
            }
        }
        void cikis()
        {
            try
            {
                webBrowser1.Navigate("javascript:void((function(){var a,b,c,e,f;f=0;a=document.cookie.split('; ');for(e=0;e<a.length&&a[e];e++){f++;for(b='.'+location.host;b;b=b.replace(/^(?:%5C.|[^%5C.]+)/,'')){for(c=location.pathname;c;c=c.replace(/.$/,'')){document.cookie=(a[e]+'; domain='+b+'; path='+c+'; expires='+new Date((new Date()).getTime()-1e11).toGMTString());}}}})())");
            }
            catch (Exception hataaa)
            {
                //hataLB.Text = "çıkış işleminde hata" + hataaa.ToString();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void Kontrol_Load(object sender, EventArgs e)
        {
            SqlKayitCek();
            webBrowser1.ScriptErrorsSuppressed = true;
        }
        void SqlKayitCek()
        {

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from BlackList", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader["eposta"].ToString());
            }
            baglanti.Close();
        }
    }
}
