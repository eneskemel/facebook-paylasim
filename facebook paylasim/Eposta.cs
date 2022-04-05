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
    public partial class Eposta : Form
    {
        public Eposta()
        {
            InitializeComponent();
        }
        string sonindexDegisken = "0";
        public static SqlConnection baglanti = new SqlConnection(("Data Source=BADBOY-PC") + @"\" + ("SqlExpress; Initial Catalog=sst;User ID=sa;Password=1236asd"));
        private void Eposta_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://passport.yandex.com.tr/registration/mail?from=mail&require_hint=1&origin=hostroot_com_tr_l_mobile_left&retpath=https%3A%2F%2Fpassport.yandex.com.tr%2Fpassport%3Fmode%3Dsubscribe%26from%3Dmail%26retpath%3Dhttps%253A%252F%252Fmail.yandex.com.tr");
        }
        void SonIndexSayisiFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Eposta WHERE id is not null", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                sonindexDegisken = (reader["id"].ToString());
            }
            baglanti.Close();
        }
        void EpostaEkleFnc()
        {
            sonindexDegisken = "-1";
            SonIndexSayisiFnc();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            int SonIndexDegerArttir = Convert.ToInt16(sonindexDegisken);
            SonIndexDegerArttir++;
            string kayit = "insert into Eposta(id,eposta,kullanımDurumu) values (@id,@eposta,@kullanımDurumu)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@id", SonIndexDegerArttir);
            komutpaylasim.Parameters.AddWithValue("@eposta", KullanıcıAdıTxt.Text + "@yandex.com");
            komutpaylasim.Parameters.AddWithValue("@kullanımDurumu", 0);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
        }
        void EpostaEkleManuelFnc()
        {
            sonindexDegisken = "-1";
            SonIndexSayisiFnc();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            int SonIndexDegerArttir = Convert.ToInt16(sonindexDegisken);
            SonIndexDegerArttir++;
            string kayit = "insert into Eposta(id,eposta,kullanımDurumu) values (@id,@eposta,@kullanımDurumu)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@id", SonIndexDegerArttir);
            komutpaylasim.Parameters.AddWithValue("@eposta", ManuelEpostaTxt.Text + "@yandex.com");
            komutpaylasim.Parameters.AddWithValue("@kullanımDurumu", 0);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
        }
        void TelefonumYokTıkla()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("label"))
                {
                    if (div.InnerText == "Telefonum yok")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void GuvenlikSorusuSec()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("select"))
            {
                item.SetAttribute("value", "12");
            }
        }
        public static string RasgeleString(int length)
        {

            const string karakterler = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            String rasgeleString = "";
            Random rasgele = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < length; i++)
            {
                rasgeleString += karakterler[rasgele.Next(karakterler.Length)];
            }

            return rasgeleString;
        }
        private void MailAcBtn_Click(object sender, EventArgs e)
        {
            webBrowser1.Focus();
            KullanıcıAdıTxt.Text = "sst" + RasgeleString(10);
            TelefonumYokTıkla();
            GuvenlikSorusuSec();
            webBrowser1.Document.GetElementById("firstname").InnerText = AdTxt.Text;
            webBrowser1.Document.GetElementById("lastname").InnerText = SoyadTxt.Text;
            webBrowser1.Document.GetElementById("login").InnerText = KullanıcıAdıTxt.Text;
            webBrowser1.Document.GetElementById("password").InnerText = SifreTxt.Text;
            webBrowser1.Document.GetElementById("password_confirm").InnerText = SifreTxt.Text;
            webBrowser1.Document.GetElementById("hint_answer").InnerText = GüvenlikSorusuTxt.Text;
            webBrowser1.Document.GetElementById("answer").InnerText = GüvenlikKoduTxt.Text;
            webBrowser1.Document.Forms[0].InvokeMember("submit");
            EpostaEkleFnc();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TelefonumYokTıkla();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            TelefonumYokTıkla();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cikis();
            webBrowser1.Navigate("https://passport.yandex.com.tr/registration/mail?from=mail&require_hint=1&origin=hostroot_com_tr_l_mobile_left&retpath=https%3A%2F%2Fpassport.yandex.com.tr%2Fpassport%3Fmode%3Dsubscribe%26from%3Dmail%26retpath%3Dhttps%253A%252F%252Fmail.yandex.com.tr");
        }
        void cikis()
        {
            try
            {
                foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
                {
                    foreach (HtmlElement div in item.GetElementsByTagName("a"))
                    {
                        if (div.InnerText == "Çıkış")
                        {
                            div.InvokeMember("click");
                        }
                    }
                }
            }
            catch (Exception hataaa)
            {
                //hataLB.Text = "çıkış işleminde hata" + hataaa.ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            EpostaEkleManuelFnc();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ManuelEpostaTxt.Text = "sst" + RasgeleString(10);
        }
    }
}
