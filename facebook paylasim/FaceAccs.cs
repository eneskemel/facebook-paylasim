using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace facebook_paylasim
{
    public partial class FaceAccs : Form
    {
        public FaceAccs()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(int hWnd);


        string resimUrl = "";
        string sonindexDegisken = "0";
        public static SqlConnection baglanti = new SqlConnection(("Data Source=BADBOY-PC") + @"\" + ("SqlExpress; Initial Catalog=sst;User ID=sa;Password=1236asd"));
        private int istekbekletdeger = 0;

        void SqlKayitCekIsim()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from ProfilAc WHERE isim is not null", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader["isim"].ToString());
            }
            baglanti.Close();
        }
        void SqlKayitCekSoyisim()
        {

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from ProfilAc WHERE soyisim is not null", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox2.Items.Add(reader["soyisim"].ToString());
            }
            baglanti.Close();
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
            string kayit = "insert into Eposta(id,isim,soyisim,eposta) values (@id,@isim,@soyisim,@eposta)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@id", SonIndexDegerArttir);
            komutpaylasim.Parameters.AddWithValue("@isim", Adtxt.Text);
            komutpaylasim.Parameters.AddWithValue("@soyisim", soyAdTxt.Text);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
        }
        void isimEkleSorusuFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            string kayit = "insert into ProfilAc(isim) values (@isim)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@isim", Adtxt.Text);
            // komutpaylasim.Parameters.AddWithValue("@soyisim", soyAdTxt.Text);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
        }
        void SoyisimEkleSorusuFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            string kayit = "insert into ProfilAc(soyisim) values (@soyisim)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            // komutpaylasim.Parameters.AddWithValue("@isim", Adtxt.Text);
            komutpaylasim.Parameters.AddWithValue("@soyisim", soyAdTxt.Text);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
        }
        void KullanildiIsaretleSorusuFnc()
        {
            string sst = isimTxt.Text + SoyadTxtt.Text;
            string veri = "";
            string[] a = sst.Split(' ');
            for (int i = 0; i < a.Length; i++)
            {
                veri += a[i];
            }
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            //"update Eposta set kullanımDurumu="1",isimSoyisim=@isimSoyisim where eposta=@eposta";
            string kayit = "update Eposta set kullanımDurumu='1',isimSoyisim=@isimSoyisim where eposta=@eposta";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@isimSoyisim", veri);
            komutpaylasim.Parameters.AddWithValue("@eposta", EpostaTxt.Text);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
        }
        void FastKullaniciKullanildiSorgusuFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            string kayit = "update fastKullanici set IslemDurumu='1' where eposta=@eposta";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@eposta", listBox1.Items[0]);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
        }
        void SqlKayitCek()
        {

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Eposta WHERE kullanımDurumu = 0", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox3.Items.Add(reader["eposta"].ToString());
            }
            baglanti.Close();
        }

        void SqlIndexCek()
        {

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from ProfilAc Where isim='gül'", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                textBox2.Text = (reader["ResimIndex"].ToString());
                textBox3.Text = (reader["SayfaIndex"].ToString());
            }
            baglanti.Close();
        }

        int gunlukIslemDegisken;
        private void FaceAccs_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate("https://passport.yandex.com.tr/");
            SqlKayitCekSoyisim();
            SqlKayitCekIsim();
            SqlKayitCekDüzenlePaylasim2();
            SqlIndexCek();
            SqlKayitCek();
            SqlSayfaVeriCekFnc();
            TxtVeriCek();
            if (1 == Convert.ToInt16(listBox9.Items[15]) || 4 == Convert.ToInt16(listBox9.Items[21]) || 1 == Convert.ToInt16(listBox9.Items[27]))
            {
                Deneme.Start();
                //Yandex.Start();
            }
            if (1 == Convert.ToInt16(listBox9.Items[29]))
            {
                listBox1.Items.Clear();
                SqlFastKullaniciKayitCek();
                if (listBox1.Items.Count < 1)
                {
                    MessageBox.Show("islem yapıcak üyelik yok");
                }
                else
                {
                    ProfilDuzenleTmr.Start();
                }
            }
            if (listBox3.Items.Count < 0)
            {
                Deneme.Stop();
            }
        }
        void TxtIcınVeriGunlukIslemDuzenle()
        {
            if (gunlukIslemDegisken > 4)
            {
                gunlukIslemDegisken = 1;
            }
            string[] veriler1 = new string[listBox9.Items.Count];
            for (int i = 0; i < listBox9.Items.Count; i++)
            {
                veriler1[i] = listBox9.Items[i].ToString();
            }
            veriler1[21] = gunlukIslemDegisken.ToString();
            listBox9.Items.Clear();
            for (int i = 0; i < veriler1.Length; i++)
            {
                listBox9.Items.Add(veriler1[i]);
            }
            TxtVeriYaz();
        }
        void TxtVeriYaz()
        {
            string b = "\\";
            string yol = "C:\\Yönet" + b + "Yönet.txt";
            // StreamReader yaz;


            StreamWriter s = new StreamWriter(yol);
            for (int i = 0; i < listBox9.Items.Count; i++)
            {

                s.WriteLine(listBox9.Items[i].ToString());

            }
            s.Close();
        }
        void TxtVeriCek()
        {
            string b = "\\";
            string yol = "C:\\Yönet" + b + "Yönet.txt";
            // Dosyamızı okuyacak.
            StreamReader oku;

            // Belirtmiş olduğum yoldaki dosyayı açacak. 
            /* NOT: @ bu işareti koymamın nedeni \\ 2 defa bundan 
            yapmamak içindir. */
            oku = File.OpenText(yol);

            string yazi;

            // Satır boş olana kadar okumaya devam eder.
            while ((yazi = oku.ReadLine()) != null)
            {
                // Listbox'ı .txt içeriği ile doldur.
                listBox9.Items.Add(yazi.ToString());
            }

            // Okumayı kapat.
            oku.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            isimEkleSorusuFnc();
            listBox1.Items.Add(Adtxt.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SoyisimEkleSorusuFnc();
            listBox2.Items.Add(soyAdTxt.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Yandex.Start();
            SadeProfilTmr.Start();

        }
        private void button5_Click(object sender, EventArgs e)
        {
            YandexgirisYap();
            // webClientt.DownloadFile("https://scontent-fra3-1.xx.fbcdn.net/v/t1.0-0/p206x206/11902297_475129342646966_284908970858447342_n.jpg?oh=cf27b4b431923127672f9858ae69c315&oe=57DD2269", "E:\\");
        }
        string paylasimDegsiken = "";
        void paylasimyap()
        {
            try
            {
                foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("textarea"))
                    if (item.GetAttribute("name") == "xhpc_message")
                        item.Focus();
                webBrowser1.Document.GetElementById("xhpc_message").SetAttribute("value", paylasimDegsiken);
                foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
                {
                    foreach (HtmlElement div in item.GetElementsByTagName("button"))
                    {
                        if (div.InnerText == "Paylaş")
                        {
                            div.InvokeMember("click");
                        }
                    }
                }
            }
            catch//Sayı Girilmemesi durumuda çalışacak kod
            { }
        }
        string SonPaylasimBegeniSayisi = "";
        void SonPaylasilanDurumunBegeniSayisiniCekFnc()
        {
            string ZamanCekGenelDegisken = "";
            string html = "";
            html = webBrowser1.Document.Body.InnerHtml.ToString();
            string start_index_text = "<span class=\"_4arz\">";
            string yeni_html = html.Substring(html.IndexOf(start_index_text) + start_index_text.Length, html.Length - (html.IndexOf(start_index_text) + start_index_text.Length));
            ZamanCekGenelDegisken = yeni_html.Substring(0, yeni_html.IndexOf("</span>"));
            textBox1.Text = ZamanCekGenelDegisken;
            listBox4.Items.Clear();
            string[] parcalar;
            parcalar = textBox1.Text.Split('>');

            foreach (string i in parcalar)
            {
                listBox4.Items.Add(i);
            }
            SonPaylasimBegeniSayisi = listBox4.Items[1].ToString();
        }
        void SayfadakiBegenilerdenArkadasEkleFnc()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("button"))
                if (item.GetAttribute("type") == "button")
                {
                    if (item.InnerText == "Arkadaşı Ekle")
                    {
                        item.InvokeMember("click");
                    }

                }
        }
        void SayfadakiBegenelereTıkla()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == SonPaylasimBegeniSayisi)
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void profildenArkadasEkle()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("button"))
                if (item.GetAttribute("value") == "1")
                {
                    if (item.InnerText == "Arkadaşı Ekle")
                    {
                        item.InvokeMember("click");
                    }

                }
        }
        void FaceAcFnc()
        {
            Random rastgeleisim = new Random();
            int isimRastleindex = rastgeleisim.Next(0, listBox1.Items.Count);
            int SoyisimRastgeleindex = rastgeleisim.Next(0, listBox2.Items.Count);
            isimTxt.Text = listBox1.Items[isimRastleindex].ToString();
            SoyadTxtt.Text = listBox2.Items[SoyisimRastgeleindex].ToString();
            EpostaTxt.Text = listBox3.Items[0].ToString();
            //KullanilanEpostayaIsimSoyIsımYazSorgusuFnc();

            webBrowser1.Document.GetElementById("firstname").InnerText = isimTxt.Text;
            webBrowser1.Document.GetElementById("lastname").InnerText = SoyadTxtt.Text;
            webBrowser1.Document.GetElementById("reg_email__").InnerText = EpostaTxt.Text;
            webBrowser1.Document.GetElementById("reg_email_confirmation__").InnerText = EpostaTxt.Text;
            webBrowser1.Document.GetElementById("reg_passwd__").InnerText = SifreTxt.Text;

            webBrowser1.Document.GetElementById("u_0_e").SetAttribute("checked", "true");
            webBrowser1.Document.GetElementById("u_0_j").InvokeMember("click");
        }
        void DogumTarihiAyarlarFaceFnc()
        {
            Random rastgeleisim = new Random();
            string DogumTarihiGunRastgele = rastgeleisim.Next(1, 29).ToString();
            string DogumTarihiAyRastgele = rastgeleisim.Next(1, 11).ToString();
            string DogumTarihiYılRastgele = rastgeleisim.Next(1994, 1998).ToString();
            webBrowser1.Document.GetElementById("birthday_day").SetAttribute("value", DogumTarihiGunRastgele);
            webBrowser1.Document.GetElementById("birthday_month").SetAttribute("value", DogumTarihiAyRastgele);
            webBrowser1.Document.GetElementById("birthday_year").SetAttribute("value", DogumTarihiYılRastgele);
        }
        void YandexgirisYap()
        {
            //EpostaTxt.Text = listBox3.Items[0].ToString();
            //EpostayıParcala();
            //webBrowser1.Document.GetElementById("login").InnerText = listBox4.Items[0].ToString();
            //System.Threading.Thread.Sleep(1000);
            //webBrowser1.Document.GetElementById("passwd").InnerText = "1236asd";
            //System.Threading.Thread.Sleep(1000);
            //webBrowser1.Document.Forms[0].InvokeMember("submit");
            //System.Threading.Thread.Sleep(1000);



            EpostaTxt.Text = listBox3.Items[0].ToString();
            EpostayıParcala();
            webBrowser1.Document.GetElementById("login").InnerText = listBox4.Items[0].ToString();
            System.Threading.Thread.Sleep(2000);
            SendKeys.Send("{TAB}");
            SendKeys.Send("1236asd");
            System.Threading.Thread.Sleep(2000);
            webBrowser1.Document.Forms[0].InvokeMember("submit");
            System.Threading.Thread.Sleep(3000);


            //EpostaTxt.Text = listBox3.Items[0].ToString();
            //EpostayıParcala();
            //SendKeys.Send(listBox4.Items[0].ToString());
            //SendKeys.Send("{TAB}");
            //SendKeys.Send("1236asd");
            //webBrowser1.Document.Forms[0].InvokeMember("submit");

        }
        void Facecikis()
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
        private void Yandex_Tick(object sender, EventArgs e)
        {
            // webBrowser1.Focus();
            //değer oku ve işlem yap
            istekbekletdeger++;
            YandexLB.Text = istekbekletdeger.ToString();
            if (istekbekletdeger == 5)
            {
                Facecikis();
            }
            if (istekbekletdeger == 12)
            {
                webBrowser1.Navigate("www.facebook.com");
            }
            if (istekbekletdeger == 25)
            {
                DogumTarihiAyarlarFaceFnc();
            }
            if (istekbekletdeger == 35)
            {
                FaceAcFnc();
                KullanildiIsaretleSorusuFnc();
            }
            if (istekbekletdeger == 50)
            {
                Facecikis();
            }
            if (istekbekletdeger == 60)
            {
                webBrowser1.Navigate("https://mail.yandex.com.tr/");
            }
            if (istekbekletdeger == 70)
            {
                cikis();
            }
            if (istekbekletdeger == 80)
            {
                webBrowser1.Navigate("https://mail.yandex.com.tr/");
            }
            if (istekbekletdeger == 95)
            {
                YandexgirisYap();
            }
            if (istekbekletdeger == 110)
            {
                FacebookMailTıklaFnc();
            }
            if (istekbekletdeger == 125)
            {
                FacebookMailOnayTıklaFnc();
            }
            if (istekbekletdeger == 140)
            {
                try
                {
                    //  webBrowser1.Document.GetElementById("email").InnerText = listBox3.Items[0].ToString();
                    webBrowser1.Document.GetElementById("pass").InnerText = "1236asd";
                    webBrowser1.Document.Forms[0].InvokeMember("submit");
                }
                catch (Exception sorun)
                {
                    //  MessageBox.Show(sorun.ToString());
                }
            }
            if (istekbekletdeger == 150)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            //YeniProfilTamamClickFnc();
            if (istekbekletdeger == 160)
            {
                YeniProfilTamamClickFnc();
            }
            if (istekbekletdeger == 170)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            //YeniProfilTamamClickFnc();
            if (istekbekletdeger == 180)
            {
                YeniProfilTamamClickFnc();
            }
            if (istekbekletdeger == 190)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            //YeniProfilTamamClickFnc();
            if (istekbekletdeger == 200)
            {
                YeniProfilTamamClickFnc();
            }
            if (istekbekletdeger == 200)
            {
                listBox5.Items.Clear();
                listBox6.Items.Clear();
                listBox7.Items.Clear();
            }
            if (istekbekletdeger == 205)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (istekbekletdeger == 215)
            {
                FotografYukleFnc1();
                FaceFakeResimRandomFnc();
                SqlPaylasilanResimEkle();
            }
            if (istekbekletdeger == 222)
            {
                FotografYukleFnc2();
            }
            if (istekbekletdeger == 225)
            {
                SendKeys.SendWait(resimUrl.ToLower());
            }
            if (istekbekletdeger == 228)
            {
                SendKeys.SendWait("^{ENTER}");
            }
            if (istekbekletdeger == 240)
            {
                ResimKaydetButonBasFnc();
            }
            if (istekbekletdeger == 247)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
                FaceFakeKapakFotosuUrlFnc();
            }
            if (istekbekletdeger == 255)
            {
                KapakFotosuEkleFnc1();
            }
            if (istekbekletdeger == 260)
            {
                FotoPaylasFnc2();
                FaceFakeKapakFotosuUrlFnc();
            }
            if (istekbekletdeger == 265)
            {
                SendKeys.SendWait(resimUrl.ToLower());
            }
            if (istekbekletdeger == 267)
            {
                SendKeys.Send("^{ENTER}");
            }
            if (istekbekletdeger == 280)
            {
                KapakFotosuKaydetButonFnc();
            }
            if (istekbekletdeger == 290)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (istekbekletdeger == 300)
            {
                ArkadasSayisiGizlemeFnc1();
            }
            if (istekbekletdeger == 305)
            {
                ArkadasSayisiGizlemeFnc2();
            }
            if (istekbekletdeger == 310)
            {
                ArkadasSayisiGizlemeFnc3();
                ArkadasSayisiGizlemeFnc33();
            }
            if (istekbekletdeger == 315)
            {
                ArkadasSayisiGizlemeFnc4();
            }
            if (istekbekletdeger == 320)
            {
                ArkadasSayisiGizlemeFnc5();
            }
            if (istekbekletdeger == 325)
            {
                webBrowser1.Navigate("https://www.facebook.com/settings?tab=timeline");
            }
            if (istekbekletdeger == 335)
            {
                AyarlarDüzenlemeZamanTuneliFnc1();
            }
            if (istekbekletdeger == 340)
            {
                AyarlarDüzenlemeZamanTuneliFnc2();
            }
            if (istekbekletdeger == 345)
            {
                AyarlarDüzenlemeZamanTuneliFnc3();
            }
            if (istekbekletdeger == 350)
            {
                AyarlarDüzenlemeZamanTuneliFnc4();
            }
            if (istekbekletdeger == 355)
            {
                AyarlarDüzenlemeZamanTuneliFnc5();
            }
            if (istekbekletdeger == 360)
            {
                AyarlarDüzenlemeZamanTuneliFnc6();
            }
            if (istekbekletdeger == 365)
            {
                AyarlarDüzenlemeZamanTuneliFnc7();
            }
            if (istekbekletdeger == 370)
            {
                AyarlarDüzenlemeZamanTuneliFnc8();
            }
            if (istekbekletdeger == 375)
            {
                AyarlarDüzenlemeZamanTuneliFnc9();
            }
            if (istekbekletdeger == 380)
            {
                webBrowser1.Navigate("https://www.facebook.com/settings?tab=followers");
            }
            if (istekbekletdeger == 390)
            {
                AyarlarDüzenlemeTakipciFnc1();
            }
            if (istekbekletdeger == 395)
            {
                AyarlarDüzenlemeTakipciFnc2();
            }
            if (istekbekletdeger == 400)
            {
                webBrowser1.Navigate("https://www.facebook.com/settings?tab=videos");
            }
            if (istekbekletdeger == 410)
            {
                AyarlarDüzenlemeVideoFnc1();
            }
            if (istekbekletdeger == 415)
            {
                AyarlarDüzenlemeVideoFnc2();
            }
            if (istekbekletdeger == 420)
            {
                webBrowser1.Navigate("https://www.facebook.com/settings?tab=privacy&section=composer&view");
            }
            //if (istekbekletdeger == 460)
            //{
            //    // AyarlarDüzenlemeGizlilikFnc1();
            //}
            if (istekbekletdeger == 430)
            {
                AyarlarDüzenlemeGizlilikFnc2();
            }
            if (istekbekletdeger == 435)
            {
                AyarlarDüzenlemeGizlilikFnc3();
            }
            if (istekbekletdeger == 440)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (istekbekletdeger == 450)
            {
                paylasimDegsiken = "Bundan Önceki Gönderiler Gizlide ...";
                paylasimyap();
            }
            if (istekbekletdeger == 460)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (istekbekletdeger == 470)
            {
                paylasimDegsiken = "İyilerin genelde dudaklarında tebessüm, gözlerinde hüzün vardır.";
                paylasimyap();
            }
            if (istekbekletdeger == 480)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (istekbekletdeger == 490)
            {
                paylasimDegsiken = "İnsanlar güçsüz oldukları için ağlamazlar, çok uzun zamandır güçlü oldukları için ağlarlar.";
                paylasimyap();
            }
            if (istekbekletdeger == 500)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (istekbekletdeger == 510)
            {
                paylasimDegsiken = "Kaliteli insan, ona gösterilen ilgi ve samimiyetten haddini aşmayandır.";
                paylasimyap();
            }
            if (istekbekletdeger == 520)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (istekbekletdeger == 530)
            {
                paylasimDegsiken = "En büyük zenginlik, evden çıkarken arkandan dua eden bir anne olmasıdır.";
                paylasimyap();
            }
            if (istekbekletdeger == 540)
            {
                //Sayfa Random Ayarlanacak
                Random rastgeleisim = new Random();
                int SayfaRastGeleİndex = rastgeleisim.Next(0, listBox8.Items.Count);
                webBrowser1.Navigate(listBox8.Items[SayfaRastGeleİndex].ToString());
            }
            if (istekbekletdeger == 555)
            {
                SonPaylasilanDurumunBegeniSayisiniCekFnc();
                SayfadakiBegenelereTıkla();
            }
            if (istekbekletdeger == 565)
            {
                SayfadakiBegenilerdenArkadasEkleFnc();
            }
            if (istekbekletdeger == 570)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (istekbekletdeger == 580)
            {
                profildenArkadasEkle();
            }
            if (istekbekletdeger == 590)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (istekbekletdeger == 600)
            {
                profildenArkadasEkle();
            }
            if (istekbekletdeger == 610)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (istekbekletdeger == 620)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
                SqlKayitCekDüzenli();
                listBox11.Items.Add(EpostaTxt.Text);
                listBox12.Items.Add("1");
                IdDuzenle();
                SqlKayitSilDuzenleme();
                SqlKayitEkleDuzenleme();

            }
            //https://www.facebook.com/checkpoint/block/?
            if (istekbekletdeger == 622)
            {
                gunlukIslemDegisken++;
                TxtIcınVeriGunlukIslemDuzenle();
                Facecikis();
                System.Diagnostics.Process.Start(@"C:\facebook paylasim.exe");
            }
            if (istekbekletdeger == 624)
            {
                Environment.Exit(0);
            }
        }

        void SonIndexSayisiSayfaLinkFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            sonindexDegisken = "-1";
            SqlCommand komut = new SqlCommand("Select * from SayfaLink WHERE id is not null", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                sonindexDegisken = (reader["id"].ToString());
            }
            baglanti.Close();
        }
        void ArkadasSayisiGizlemeFnc1()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("a"))
                {
                    if (div.InnerText == "Arkadaşlar")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void ArkadasSayisiGizlemeFnc2()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("button"))
                if (item.GetAttribute("role") == "button")
                {
                    item.InvokeMember("click");
                }
        }
        void ArkadasSayisiGizlemeFnc3()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Gizliliği Düzenle")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void ArkadasSayisiGizlemeFnc33()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("span"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("a"))
                {
                    if (div.InnerText == "Gizliliği Düzenle")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void ArkadasSayisiGizlemeFnc4()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Herkese Açık")
                    {
                        div.InvokeMember("click");
                    }
                }
            }

            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Sadece Ben")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void ArkadasSayisiGizlemeFnc5()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("a"))
                {
                    if (div.InnerText == "Bitti")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }

        void AyarlarDüzenlemeGizlilikFnc1()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("div"))
                {
                    if (div.InnerText == "İleride paylaşacağın gönderileri kimler görebilir?")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeGizlilikFnc2()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Arkadaşlar")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeGizlilikFnc3()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Herkese Açık")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeZamanTuneliFnc1()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("div"))
                {
                    if (div.InnerText == "Zaman tünelinde kimler paylaşımda bulunabilir?")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeZamanTuneliFnc2()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Arkadaşlar")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeZamanTuneliFnc3()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Sadece Ben")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeZamanTuneliFnc4()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("div"))
                {
                    if (div.InnerText == "Arkadaşlarının seni etiketlediği gönderiler zaman tünelinde görünmeden önce onayına sunulsun mu?")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeZamanTuneliFnc5()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Kapalı")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeZamanTuneliFnc6()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Açık")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeZamanTuneliFnc7()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("div"))
                {
                    if (div.InnerText == "İnsanların senin gönderilerine eklediği etiketler Facebook'ta gözükmeden önce onayına sunulsun mu?")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeZamanTuneliFnc8()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Kapalı")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeZamanTuneliFnc9()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Açık")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeTakipciFnc1()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Arkadaşlar")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeTakipciFnc2()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Herkes")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeVideoFnc1()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Varsayılan")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void AyarlarDüzenlemeVideoFnc2()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Kapalı")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }



        void FotografYukleFnc1()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("div"))
                {
                    if (div.InnerText == "Fotoğraf Ekle")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void FotografYukleFnc2()
        {
            int x = 0;
            foreach (HtmlElement el in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in el.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Fotoğraf Yükle")
                    {
                        foreach (HtmlElement a1 in div.Parent.All)
                        {
                            if (x == 3)
                            {
                                break;
                            }
                            x++;
                            foreach (HtmlElement a2 in a1.GetElementsByTagName("a"))
                            {
                                foreach (HtmlElement a3 in a2.GetElementsByTagName("div"))
                                {
                                    foreach (HtmlElement a4 in a3.GetElementsByTagName("input"))
                                    {
                                        a4.InvokeMember("click");
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }
        void KapakFotosuKaydetButonFnc()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("button"))
            {
                if (item.InnerText == "Değişiklikleri Kaydet")
                {
                    item.InvokeMember("click");
                }
            }
        }
        int yorumSayısı = 0;
        void BunuKaldirFnc1()
        {
            HtmlElementCollection classButton = webBrowser1.Document.All;
            foreach (HtmlElement element in classButton)
            {
                if (element.GetAttribute("className") == "UFICommentCloseButton _50zy _50-0 _50z- _5upp _42ft")
                {
                    element.InvokeMember("click");
                    yorumSayısı++;
                }
            }
        }
        void BunuKaldirFnc2()
        {
            for (int i = 0; i < yorumSayısı + 1; i++)
            {
                foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
                {
                    foreach (HtmlElement div in item.GetElementsByTagName("button"))
                    {
                        if (div.InnerText == "Sil")
                        {
                            div.InvokeMember("click");
                        }
                    }
                }
            }

        }
        void RandomArkadasEklemeFnc1()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "1 saat")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void RandomArkadasEklemeFnc2()
        {
            textBox1.Text = webBrowser1.Document.GetElementById("js_2e").ToString();
            //webBrowser1.Document.GetElementById("firstname").InnerText = isimTxt.Text;
            // string sitekodlari = webBrowser1.Document.DocumentNode.SelectNodes("//div[@id='content']/input[@id='SelectedTournamentId']")[0].Attributes["value"].Value;
        }

        //data-action-type
        string resimPath;
        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("C:\\Fake Face Resimler\\Aylin Türkmen\\549365_108242706181344_624392603938307065_n.jpg");
            resimPath = "C:\\Fake Face Resimler\\Aylin Türkmen\\549365_108242706181344_624392603938307065_n.jpg";


            FileStream fs = new FileStream(resimPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] resim = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();

            SqlCommand kmt = new SqlCommand("insert into Resim(image) Values (@image) ", baglanti);
            kmt.Parameters.Add("@image", SqlDbType.Image, resim.Length).Value = resim;

            try

            {

                baglanti.Open();//www.gorselprogramlama.com

                kmt.ExecuteNonQuery();

                MessageBox.Show(" Veritabanına kayıt yapıldı.");

            }

            catch (Exception ex)//www.gorselprogramlama.com

            {

                MessageBox.Show(ex.Message.ToString());

            }

            finally

            {

                baglanti.Close();

            }

        }
        //u_jsonp_3_5
        private void button7_Click(object sender, EventArgs e)
        {
            BunuKaldirFnc1();
        }
        void FacebookMailTıklaFnc()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Facebook")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void FacebookMailOnayTıklaFnc()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Hesabınızı Onaylayın")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
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
        void EpostayıParcala()
        {
            {
                listBox4.Items.Clear();
                string[] parcalar;

                parcalar = EpostaTxt.Text.Split('@');

                foreach (string i in parcalar)
                {
                    listBox4.Items.Add(i);
                }
            }
        }
        int resimUrlDegerTutDegisken = 0;
        Random ResimRdm = new Random();
        int ResimKlasorDeger = 0;
        int PaylasimDeger = 0;

        void FaceFakeResimRandomFnc()
        {
            listBox5.Items.Clear();
            string[] klasorler = Directory.GetDirectories("C:\\Fake Face Resimler");
            for (int j = 0; j < klasorler.Length; j++)
            {
                listBox5.Items.Add(klasorler[j]);
            }
            //Random ResimRdm = new Random();
            //ResimKlasorDeger = ResimRdm.Next(0, listBox5.Items.Count);
            ResimKlasorDeger = Convert.ToInt16(textBox2.Text);
            listBox6.Items.Clear();

            string[] dosyalar = System.IO.Directory.GetFiles(listBox5.Items[ResimKlasorDeger].ToString());
            for (int j = 0; j < dosyalar.Length; j++)
            {
                listBox6.Items.Add(dosyalar[j]);
            }
            resimUrlDegerTutDegisken = ResimRdm.Next(0, listBox6.Items.Count);
            resimUrl = listBox6.Items[resimUrlDegerTutDegisken].ToString();
        }

        void SqlPaylasilanResimEkle()
        {
            SqlPaylasilanResimSonIndex();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            int SonIndexDegerArttir = Convert.ToInt16(sonindexDegisken);
            SonIndexDegerArttir++;
            string kayit = "insert into ResimPaylasim(id,eposta,ResimUrl,ResimYuklenen) values (@id,@eposta,@ResimUrl,@ResimYuklenen)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@id", SonIndexDegerArttir);
            komutpaylasim.Parameters.AddWithValue("@eposta", listBox1.Items[0]);
            komutpaylasim.Parameters.AddWithValue("@ResimUrl", listBox5.Items[ResimKlasorDeger].ToString());
            komutpaylasim.Parameters.AddWithValue("@ResimYuklenen", resimUrl);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
            //ResimUrl
        }
        void SqlPaylasilanResimSonIndex()
        {
            sonindexDegisken = "";
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from ResimPaylasim WHERE id is not null", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                sonindexDegisken = (reader["id"].ToString());
            }
            baglanti.Close();
        }


        string facelink = "";
        void PaylasimKontrolFnc()
        {
            for (int i = 0; i < listBox5.Items.Count; i++)
            {
                PaylasanTxt.Clear();
                PaylasanTxt.Text = listBox10.Items[i].ToString();
                string PaylasmismiKontrolDegisken = PaylasanTxt.Text.IndexOf(EpostaTxt.Text).ToString();
                int PaylasimKontrolDegiskenInt = Convert.ToInt16(PaylasmismiKontrolDegisken);
                // MessageBox.Show(PaylasimKontrolDegiskenInt.ToString());
                if (-1 == PaylasimKontrolDegiskenInt)
                {
                    facelink = listBox5.Items[i].ToString();
                    PaylasanTxt.Text = PaylasanTxt.Text + " " + EpostaTxt.Text;
                    if (baglanti.State == ConnectionState.Closed)
                        baglanti.Open();
                    SqlCommand komut2 = new SqlCommand("UPDATE Paylasim SET paylasilanEposta = @epostalar WHERE id = @id", baglanti);
                    komut2.Parameters.AddWithValue("@id", i);
                    komut2.Parameters.AddWithValue("@epostalar", PaylasanTxt.Text);
                    komut2.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show(facelink);
                    i = listBox5.Items.Count;
                }
            }
        }
        void SqlKayitCekDüzenlePaylasim()
        {
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Paylasim", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox10.Items.Add(reader["paylasimNvarchar"].ToString());
                listBox11.Items.Add(reader["paylasilanEposta"].ToString());
            }
            baglanti.Close();
        }
        void FaceFakeKapakFotosuUrlFnc()
        {
            resimUrlDegerTutDegisken = ResimRdm.Next(0, listBox6.Items.Count);
            resimUrl = listBox6.Items[resimUrlDegerTutDegisken].ToString();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            //FotografYukleFnc1();
            Deneme.Start();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            istekbekletdeger = Convert.ToInt16(DegerAyarlaTxt.Text);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBroserURlTxt.Text = webBrowser1.Url.ToString();
            //GuvenlikKontrolUrlFnc();
        }
        void GuvenlikKontrolUrlFnc()
        {
            string KontrolDegerTutStrng = WebBroserURlTxt.Text.IndexOf("checkpoint").ToString();
            int KontrolDegerTutİnt = Convert.ToInt16(KontrolDegerTutStrng);
            if (KontrolDegerTutİnt > 1)
            {

                SqlKullaniciBlackOlanıTemizleFnc();
                SqlBlackListEkleSorgusuFnc();
                Environment.Exit(0);

            }
        }
        void SqlKullaniciBlackOlanıTemizleFnc()
        {
            SonIndexSayisiBlackListFnc();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            int SonIndexDegerArttir = Convert.ToInt16(sonindexDegisken);
            string kayit = "Delete from Eposta WHERE eposta = @eposta";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@eposta", EpostaTxt.Text);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
        }
        void SqlBlackListEkleSorgusuFnc()
        {
            SonIndexSayisiBlackListFnc();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            int SonIndexDegerArttir = Convert.ToInt16(sonindexDegisken);
            SonIndexDegerArttir++;
            string kayit = "insert into BlackList(id,eposta) values (@id,@eposta)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@id", SonIndexDegerArttir);
            komutpaylasim.Parameters.AddWithValue("@eposta", EpostaTxt.Text);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
        }
        void SqlBlackListTelEkleSorgusuFnc()
        {
            sonindexDegisken = "-1";
            SonIndexSayisiBlackListTelFnc();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            int SonIndexDegerArttir = Convert.ToInt16(sonindexDegisken);
            SonIndexDegerArttir++;
            string kayit = "insert into BlackListTel(id,eposta) values (@id,@eposta)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@id", SonIndexDegerArttir);
            {
                komutpaylasim.Parameters.AddWithValue("@eposta", EpostaTxt.Text);
                komutpaylasim.ExecuteNonQuery();
                baglanti.Close();
            }
        }
        void SonIndexSayisiBlackListFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from BlackList WHERE id is not null", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                sonindexDegisken = (reader["id"].ToString());
            }
            baglanti.Close();
        }
        void SonIndexSayisiBlackListTelFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from BlackListTel WHERE id is not null", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                sonindexDegisken = (reader["id"].ToString());
            }
            baglanti.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            KapakFotosuEkleFnc2();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            KapakFotosuEkleFnc1();
            // FaceFakeResimRandomFnc();
            //SendKeys.SendWait(resimUrl);
            //SendKeys.Send("{enter}");
        }
        void ResimKaydetButonBasFnc()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("button"))
                if (item.GetAttribute("value") == "1")
                {
                    if (item.InnerText == "Kırp ve Kaydet" || item.InnerText == "Kaydet")
                    {
                        item.InvokeMember("click");
                    }

                }
        }


        //yaz metodun ismini
        void KapakFotosuEkleFnc1()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Kapak Fotoğrafı Ekle")
                    {
                        div.InvokeMember("click");

                    }
                }
            }
        }
        void YeniProfilTamamClickFnc()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("a"))
                {
                    if (div.InnerText == "Tamam")
                    {
                        div.InvokeMember("click");

                    }
                }
            }
        }
        void KapakFotosuEkleFnc2()
        {
            int x = 0;
            int y = 0;
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("label"))
                {
                    if (div.InnerText == "Fotoğraf Yükle...")
                    {
                        foreach (HtmlElement div2 in item.GetElementsByTagName("div"))
                        {
                            if (x == 3)
                            {
                                break;
                            }
                            x++;

                            foreach (HtmlElement input in item.GetElementsByTagName("input"))
                            {
                                y++;
                                if (x == 2)
                                {
                                    input.InvokeMember("click");

                                }

                                if (y == 33)
                                {
                                    break;
                                }
                            }
                        }

                    }
                }
            }
        }
        int ModemResetDurumu = 0;
        void ModemResetVtEkleFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE Modem SET ResetDurumu = @ResetDurumu", baglanti);
            komut.Parameters.AddWithValue("@ResetDurumu", ModemResetDurumu);

            komut.ExecuteNonQuery();
            baglanti.Close();
        }
        int deneme = 0;
        private void Deneme_Tick(object sender, EventArgs e)
        {
            deneme++;
            label5.Text = deneme.ToString();
            if (deneme == 2)
            {
                webBrowser1.Navigate("http://192.168.2.1/");


            }
            if (deneme == 10)
            {
                if (listBox3.Items.Count < 0)
                {
                    gunlukIslemDegisken++;
                    TxtIcınVeriGunlukIslemDuzenle();
                    cikis();
                    System.Diagnostics.Process.Start(@"C:\facebook paylasim.exe");
                    Environment.Exit(0);
                }
                //  webBrowser1.Focus();
                SendKeys.Send("admin");

            }
            if (deneme == 11)
            {
                SendKeys.Send("{TAB}");
            }
            if (deneme == 12)
            {
                SendKeys.Send("superonline");

            }
            if (deneme == 15)
            {
                SendKeys.Send("{ENTER}");

            }
            if (deneme == 22)
            {//4
                SendKeys.Send("{TAB 22}");
                SendKeys.Send("{ENTER}");
            }
            if (deneme == 25)
            {
                SendKeys.Send("{TAB 5}");
                SendKeys.Send("{ENTER}");
            }
            if (deneme == 30)
            {
                SendKeys.Send("{ENTER}");
                ModemResetDurumu = 1;
                ModemResetVtEkleFnc();
            }
            if (deneme == 150)
            {
                ModemResetDurumu = 0;
                ModemResetVtEkleFnc();
            }
            if (deneme == 300)
            {
                deneme = 0;
                Deneme.Stop();
                if (1 == Convert.ToInt16(listBox9.Items[27]))
                {
                    SadeProfilTmr.Start();
                }
                else
                {
                    Yandex.Start();
                }
                webBrowser1.Navigate("https://www.facebook.com/");
            }
        }
        string ModemResetDurmu = "";
        void ModemResetDurumuKontrolFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Modem", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                ModemResetDurmu = (reader["ResetDurumu"].ToString());
            }
            baglanti.Close();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            //SqlCommand komut = new SqlCommand("select Resimler from image", baglanti);
            SqlCommand komut = new SqlCommand("select * from Resim where id = @id", baglanti);
            komut.Parameters.AddWithValue("@id", 0);
            Image UyeResim = null;
            SqlDataReader okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())

            {

                byte[] resim = (byte[])okuyucu[0]; //Okuyucu ile üzerine tıkladığımız üyenin resmini byte dizisi tanımlayıp içine atıyoruz.

                MemoryStream ms = new MemoryStream(resim, 0, resim.Length); // System.IO isim uzayı altındaki MemoryStream sınıfıyla oluşturduğumuz byte dizisi için bir akım oluşturuyoruz.

                ms.Write(resim, 0, resim.Length);

                UyeResim = Image.FromStream(ms, true); // Oluşturduğumuz akım üzerinden aldığımızı image imize atıyoruz.

                pictureBox2.Image = UyeResim;

            }
            okuyucu.Close();

            baglanti.Close();

        }
        private void button15_Click(object sender, EventArgs e)
        {
            HtmlElementCollection classButton = webBrowser1.Document.All;
            string abc = classButton.ToString();
            MessageBox.Show(abc);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            AyarlarDüzenlemeZamanTuneliFnc4();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            AyarlarDüzenlemeZamanTuneliFnc5();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            AyarlarDüzenlemeZamanTuneliFnc6();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://192.168.2.1/tools/tools.html");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            KapakFotosuEkleFnc1();
        }

        private void button19_Click(object sender, EventArgs e)
        {

            FotoPaylasFnc2();
        }

        private void SayfaEkleBtn_Click(object sender, EventArgs e)
        {
            SqlSayfaLinkKayitEkleFnc();
            listBox8.Items.Clear();
            SqlSayfaVeriCekFnc();

        }
        void SqlSayfaLinkKayitEkleFnc()
        {
            sonindexDegisken = "-1";
            SonIndexSayisiSayfaLinkFnc();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            int SonIndexDegerArttir = Convert.ToInt16(sonindexDegisken);
            SonIndexDegerArttir++;
            string kayit = "insert into SayfaLink(id,SayfaLink) values (@id,@SayfaLink)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@id", SonIndexDegerArttir);
            komutpaylasim.Parameters.AddWithValue("@SayfaLink", SayfaEkleTxt.Text);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
        }
        void SqlSayfaVeriCekFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from SayfaLink", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox8.Items.Add(reader["SayfaLink"].ToString());
            }
            baglanti.Close();
        }

        void SqlKayitCekDüzenli()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Kullanici", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox10.Items.Add(reader["id"].ToString());
                listBox11.Items.Add(reader["eposta"].ToString());
                listBox12.Items.Add(reader["arkadasSayisi"].ToString());
            }
            baglanti.Close();
        }
        void IdDuzenle()
        {
            listBox10.Items.Clear();
            for (int i = 0; i < listBox11.Items.Count; i++)
            {
                listBox10.Items.Add(i.ToString());
            }
        }
        void SqlKayitSilDuzenleme()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komutSilme = new SqlCommand("Delete from Kullanici", baglanti);
            SqlDataReader readerSilme = komutSilme.ExecuteReader();
            baglanti.Close();
        }
        void SqlKayitEkleDuzenleme()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            for (int i = 0; i < listBox11.Items.Count; i++)
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                string kayit = "insert into Kullanici(id,eposta,pw,arkadasSayisi) values (@id,@eposta,@pw,@arkadasSayisi)";
                SqlCommand komutEkle = new SqlCommand(kayit, baglanti);
                komutEkle.Parameters.AddWithValue("@id", Convert.ToInt16(listBox10.Items[i]));
                komutEkle.Parameters.AddWithValue("@eposta", listBox11.Items[i].ToString());
                komutEkle.Parameters.AddWithValue("@pw", "1236asd");
                komutEkle.Parameters.AddWithValue("@arkadasSayisi", Convert.ToInt16(listBox12.Items[i]));
                komutEkle.ExecuteNonQuery();
                baglanti.Close();
            }
        }
        void SqlKayitFastEkle()
        {
            SonIndexSayisiFastKullaniciFnc();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            string kayit = "insert into fastKullanici(id,eposta,IslemDurumu) values (@id,@eposta,@IslemDurumu)";
            SqlCommand komutEkle = new SqlCommand(kayit, baglanti);
            komutEkle.Parameters.AddWithValue("@id", Convert.ToInt16(sonindexDegisken) + 1);
            komutEkle.Parameters.AddWithValue("@eposta", EpostaTxt.Text);
            komutEkle.Parameters.AddWithValue("@IslemDurumu", 0);
            komutEkle.ExecuteNonQuery();
            baglanti.Close();
        }
        void SonIndexSayisiFastKullaniciFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            sonindexDegisken = "-1";
            SqlCommand komut = new SqlCommand("Select * from fastKullanici WHERE id is not null", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                sonindexDegisken = (reader["id"].ToString());
            }
            baglanti.Close();
        }
        private void button25_Click(object sender, EventArgs e)
        {
            SqlKayitCekDüzenli();
            listBox11.Items.Add(EpostaTxt.Text);
            listBox12.Items.Add("1");
            IdDuzenle();
            SqlKayitSilDuzenleme();
            SqlKayitEkleDuzenleme();
        }
        void SqlKayitCekDüzenleFotoPaylasim()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Eposta", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox10.Items.Add(reader["id"].ToString());
                listBox11.Items.Add(reader["eposta"].ToString());
                listBox12.Items.Add(reader["kullanımDurumu"].ToString());
                listBox13.Items.Add(reader["isimSoyisim"].ToString());
                listBox14.Items.Add(reader["ResimUrl"].ToString());
                listBox15.Items.Add(reader["ResimYuklenen"].ToString());
            }
            baglanti.Close();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            PaylasimKontrolFnc();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            deneme = Convert.ToInt16(DegerAyarlaTxt.Text);
        }

        private void button27_Click(object sender, EventArgs e)
        {

        }
        int xx = 0;
        void FotoPaylasFnc2()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("input"))
                {
                    if (div.GetAttribute("title") == "Yüklenecek dosyayı seç")
                    {
                        xx++;
                        if (xx == 2)
                        {
                            div.InvokeMember("click");
                        }
                    }
                    if (xx == 2)
                    {
                        break;
                    }
                }
                if (xx == 2)
                {
                    break;
                    xx = 0;
                }
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (Yandex.Enabled == true)
            {
                Yandex.Stop();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (Yandex.Enabled == false)
            {
                Yandex.Start();
            }
        }

        private void SadeProfilTmr_Tick(object sender, EventArgs e)
        {
            // webBrowser1.Focus();
            //değer oku ve işlem yap
            istekbekletdeger++;
            YandexLB.Text = istekbekletdeger.ToString();
            if (istekbekletdeger == 5)
            {
                Facecikis();
            }
            if (istekbekletdeger == 12)
            {
                webBrowser1.Navigate("www.facebook.com");
            }
            if (istekbekletdeger == 25)
            {
                DogumTarihiAyarlarFaceFnc();
            }
            if (istekbekletdeger == 35)
            {
                FaceAcFnc();
                KullanildiIsaretleSorusuFnc();
            }
            if (istekbekletdeger == 50)
            {
                Facecikis();
            }
            if (istekbekletdeger == 60)
            {
                webBrowser1.Navigate("https://mail.yandex.com.tr/");
            }
            if (istekbekletdeger == 70)
            {
                cikis();
            }
            if (istekbekletdeger == 80)
            {
                webBrowser1.Navigate("https://passport.yandex.com.tr/passport?mode=auth&retpath=https%3A%2F%2Fmail.yandex.com.tr%2Flite%2Finbox");
            }
            if (istekbekletdeger == 95)
            {
                //YandexgirisYap();
                EpostaTxt.Text = listBox3.Items[0].ToString();
                EpostayıParcala();
                webBrowser1.Document.GetElementById("login").InnerText = listBox4.Items[0].ToString();
            }
            if (istekbekletdeger == 97)
            {
                SendKeys.Send("{TAB}");
                SendKeys.Send("1236asd");
            }
            if (istekbekletdeger == 100)
            {
                webBrowser1.Document.Forms[0].InvokeMember("submit");
            }
            if (istekbekletdeger == 110)
            {
                FacebookMailTıklaFnc();
            }
            if (istekbekletdeger == 125)
            {
                FacebookMailOnayTıklaFnc();
            }
            if (istekbekletdeger == 140)
            {
                try
                {
                    //  webBrowser1.Document.GetElementById("email").InnerText = listBox3.Items[0].ToString();
                    webBrowser1.Document.GetElementById("pass").InnerText = "1236asd";
                    webBrowser1.Document.Forms[0].InvokeMember("submit");
                }
                catch (Exception sorun)
                {
                    //  MessageBox.Show(sorun.ToString());
                }
            }
            if (istekbekletdeger == 150)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
                SqlKayitFastEkle();
            }
            //https://www.facebook.com/checkpoint/block/?
            if (istekbekletdeger == 155)
            {
                gunlukIslemDegisken++;
                Facecikis();
                System.Diagnostics.Process.Start(@"C:\facebook paylasim.exe");
            }
            if (istekbekletdeger == 157)
            {
                Environment.Exit(0);
            }
        }
        string eposta = "";
        void girisyap()
        {
            try
            {
                eposta = listBox1.Items[0].ToString();
                webBrowser1.Document.GetElementById("email").InnerText = eposta;
                webBrowser1.Document.GetElementById("pass").InnerText = "1236asd";
                webBrowser1.Document.Forms[0].InvokeMember("submit");
            }
            catch (Exception)
            {
            }
        }
        private void ProfilDuzenleTmr_Tick(object sender, EventArgs e)
        {
            istekbekletdeger++;
            YandexLB.Text = istekbekletdeger.ToString();
            if (istekbekletdeger == 2)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (istekbekletdeger == 10)
            {
                girisyap();
            }
            if (istekbekletdeger == 20)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            //YeniProfilTamamClickFnc();
            if (istekbekletdeger == 30)
            {
                YeniProfilTamamClickFnc();
            }
            if (istekbekletdeger == 40)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            //YeniProfilTamamClickFnc();
            if (istekbekletdeger == 50)
            {
                YeniProfilTamamClickFnc();
            }
            if (istekbekletdeger == 60)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            //YeniProfilTamamClickFnc();
            if (istekbekletdeger == 70)
            {
                YeniProfilTamamClickFnc();
            }
            if (istekbekletdeger == 80)
            {
                listBox5.Items.Clear();
                listBox6.Items.Clear();
                listBox7.Items.Clear();
            }
            if (istekbekletdeger == 80)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (istekbekletdeger == 90)
            {
                try
                {
                    FotografYukleFnc1();
                    FaceFakeResimRandomFnc();
                    SqlPaylasilanResimEkle();
                }
                catch (Exception)
                {

                }

            }
            if (istekbekletdeger == 100)
            {
                FotografYukleFnc2();
            }
            if (istekbekletdeger == 110)
            {
                SendKeys.SendWait(resimUrl.ToLower());
            }
            if (istekbekletdeger == 112)
            {
                SendKeys.SendWait("^{ENTER}");
            }
            if (istekbekletdeger == 130)
            {
                ResimKaydetButonBasFnc();
            }
            if (istekbekletdeger == 135)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
                FaceFakeKapakFotosuUrlFnc();
            }
            if (istekbekletdeger == 143)
            {
                KapakFotosuEkleFnc1();
            }
            if (istekbekletdeger == 150)
            {
                FotoPaylasFnc2();
                FaceFakeKapakFotosuUrlFnc();
            }
            if (istekbekletdeger == 153)
            {
                SendKeys.SendWait(resimUrl.ToLower());
            }
            if (istekbekletdeger == 155)
            {
                SendKeys.Send("^{ENTER}");
            }
            if (istekbekletdeger == 170)
            {
                KapakFotosuKaydetButonFnc();
            }
            if (istekbekletdeger == 180)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (istekbekletdeger == 185)
            {
                ArkadasSayisiGizlemeFnc1();
            }
            if (istekbekletdeger == 190)
            {
                ArkadasSayisiGizlemeFnc2();
            }
            if (istekbekletdeger == 200)
            {
                ArkadasSayisiGizlemeFnc3();
                ArkadasSayisiGizlemeFnc33();
            }
            if (istekbekletdeger == 205)
            {
                ArkadasSayisiGizlemeFnc4();
            }
            if (istekbekletdeger == 210)
            {
                ArkadasSayisiGizlemeFnc5();
            }
            if (istekbekletdeger == 215)
            {
                webBrowser1.Navigate("https://www.facebook.com/settings?tab=timeline");
            }
            if (istekbekletdeger == 220)
            {
                AyarlarDüzenlemeZamanTuneliFnc1();
            }
            if (istekbekletdeger == 225)
            {
                AyarlarDüzenlemeZamanTuneliFnc2();
            }
            if (istekbekletdeger == 230)
            {
                AyarlarDüzenlemeZamanTuneliFnc3();
            }
            if (istekbekletdeger == 235)
            {
                AyarlarDüzenlemeZamanTuneliFnc4();
            }
            if (istekbekletdeger == 240)
            {
                AyarlarDüzenlemeZamanTuneliFnc5();
            }
            if (istekbekletdeger == 245)
            {
                AyarlarDüzenlemeZamanTuneliFnc6();
            }
            if (istekbekletdeger == 250)
            {
                AyarlarDüzenlemeZamanTuneliFnc7();
            }
            if (istekbekletdeger == 255)
            {
                AyarlarDüzenlemeZamanTuneliFnc8();
            }
            if (istekbekletdeger == 260)
            {
                AyarlarDüzenlemeZamanTuneliFnc9();
            }
            if (istekbekletdeger == 265)
            {
                webBrowser1.Navigate("https://www.facebook.com/settings?tab=followers");
            }
            if (istekbekletdeger == 275)
            {
                AyarlarDüzenlemeTakipciFnc1();
            }
            if (istekbekletdeger == 280)
            {
                AyarlarDüzenlemeTakipciFnc2();
            }
            if (istekbekletdeger == 285)
            {
                webBrowser1.Navigate("https://www.facebook.com/settings?tab=videos");
            }
            if (istekbekletdeger == 295)
            {
                AyarlarDüzenlemeVideoFnc1();
            }
            if (istekbekletdeger == 300)
            {
                AyarlarDüzenlemeVideoFnc2();
            }
            if (istekbekletdeger == 305)
            {
                webBrowser1.Navigate("https://www.facebook.com/settings?tab=privacy&section=composer&view");
            }
            if (istekbekletdeger == 315)
            {
                AyarlarDüzenlemeGizlilikFnc2();
            }
            if (istekbekletdeger == 320)
            {
                AyarlarDüzenlemeGizlilikFnc3();
            }
            if (istekbekletdeger == 325)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (istekbekletdeger == 335)
            {
                Random PaylasimDegerRdm = new Random();
                PaylasimDeger = PaylasimDegerRdm.Next(0, listBox10.Items.Count);
                paylasimDegsiken = listBox10.Items[PaylasimDeger].ToString();
                paylasimyap();
            }
            if (istekbekletdeger == 345)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (istekbekletdeger == 355)
            {
                Random PaylasimDegerRdm = new Random();
                PaylasimDeger = PaylasimDegerRdm.Next(0, listBox10.Items.Count);
                paylasimDegsiken = listBox10.Items[PaylasimDeger].ToString();
                paylasimyap();
            }
            if (istekbekletdeger == 365)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (istekbekletdeger == 375)
            {
                Random PaylasimDegerRdm = new Random();
                PaylasimDeger = PaylasimDegerRdm.Next(0, listBox10.Items.Count);
                paylasimDegsiken = listBox10.Items[PaylasimDeger].ToString();
                paylasimyap();
            }
            if (istekbekletdeger == 385)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (istekbekletdeger == 395)
            {
                Random PaylasimDegerRdm = new Random();
                PaylasimDeger = PaylasimDegerRdm.Next(0, listBox10.Items.Count);
                paylasimDegsiken = listBox10.Items[PaylasimDeger].ToString();
                paylasimyap();
            }
            if (istekbekletdeger == 405)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (istekbekletdeger == 415)
            {
                Random PaylasimDegerRdm = new Random();
                PaylasimDeger = PaylasimDegerRdm.Next(0, listBox10.Items.Count);
                paylasimDegsiken = listBox10.Items[PaylasimDeger].ToString();
                paylasimyap();
            }
            if (istekbekletdeger == 425)
            {
                //Sayfa Random Ayarlanacak
                Random rastgeleisim = new Random();
                int SayfaRastGeleİndex = Convert.ToInt16(textBox3.Text);
                webBrowser1.Navigate(listBox8.Items[SayfaRastGeleİndex].ToString());
            }
            if (istekbekletdeger == 450)
            {
                SonPaylasilanDurumunBegeniSayisiniCekFnc();
                SayfadakiBegenelereTıkla();
            }
            if (istekbekletdeger == 460)
            {
                SayfadakiBegenilerdenArkadasEkleFnc();
            }
            if (istekbekletdeger == 470)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (istekbekletdeger == 480)
            {
                profildenArkadasEkle();
            }
            if (istekbekletdeger == 490)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (istekbekletdeger == 520)
            {
                profildenArkadasEkle();
            }
            if (istekbekletdeger == 530)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (istekbekletdeger == 560)
            {
                profildenArkadasEkle();
            }
            if (istekbekletdeger == 570)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (istekbekletdeger == 600)
            {
                profildenArkadasEkle();
            }
            if (istekbekletdeger == 610)
            {
                SqlIndexSayisiEkle();
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
                FastKullaniciKullanildiSorgusuFnc();
                SqlKayitCekDüzenli();
                listBox11.Items.Add(listBox1.Items[0]);
                listBox12.Items.Add("1");
                IdDuzenle();
                SqlKayitSilDuzenleme();
                SqlKayitEkleDuzenleme();
                Facecikis();

            }
            //https://www.facebook.com/checkpoint/block/?
            if (istekbekletdeger == 612)
            {
                System.Diagnostics.Process.Start(@"C:\facebook paylasim.exe");
            }
            if (istekbekletdeger == 614)
            {
                Environment.Exit(0);
            }
        }
        void SqlFastKullaniciKayitCek()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from fastKullanici Where IslemDurumu = 0", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader["Eposta"].ToString());
            }
            baglanti.Close();
        }
        void SqlIndexSayisiEkle()
        {
            int Index1 = 0;
            int Index2 = 0;
            if (Convert.ToInt16(textBox2.Text) < listBox5.Items.Count)
            {
                Index1 = Convert.ToInt16(textBox2.Text) + 1;
            }
            else
            {
                Index1 = 0;
            }
            if (Convert.ToInt16(textBox3.Text) < listBox8.Items.Count)
            {
                Index2 = Convert.ToInt16(textBox3.Text) + 1;
            }
            else
            {
                Index2 = 0;
            }
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE ProfilAc SET ResimIndex = @ResimIndex, SayfaIndex = @SayfaIndex  WHERE isim ='gül'", baglanti);
            komut.Parameters.AddWithValue("@ResimIndex", Index1);
            komut.Parameters.AddWithValue("@SayfaIndex", Index2);
            komut.ExecuteNonQuery();
            baglanti.Close();
        }
        void SqlKayitCekDüzenlePaylasim2()
        {
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Paylasim", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox10.Items.Add(reader["paylasimNvarchar"].ToString());
            }
            baglanti.Close();
        }
    }
}

