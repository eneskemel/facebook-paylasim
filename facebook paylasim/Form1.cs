using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using YoutubeExtractor;
using System.ComponentModel;
using System.Collections;

namespace facebook_paylasim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //  connetionString =                          "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
        public static SqlConnection baglanti = new SqlConnection(("Data Source=BADBOY-PC") + @"\" + ("SqlExpress; Initial Catalog=sst;User ID=sa;Password=1236asd"));
        #region Değişkenler
        int fastArakdasKabulDegerDegisken = 0;
        int ReturnDegisken = 0;
        int ReturnDegisken2 = 0;
        public static string baglantidegisken;
        string html;
        string eposta;
        string pw = "";
        int caunt = 0;
        int caunt2 = 0;
        int kontrol = 0;
        string url = "";
        string acıkurl;
        string ArkadaslikGenelDegisken = "0";
        int ArkadaslikBireyselDegisken = 0;
        int ArkadasSayisiTut = 0;
        int ArkadasSayisiVeritabanindanGelen = 0;
        int dondur1 = 0;
        int dondur2 = 0;
        string SonIndexTxt = "";
        #endregion
        #region Sayfa Davet ScrollBar Kaydir Fnc
        void SayfaDavetScrollBarKaydirFnc()
        {
            webBrowser1.Navigate("javascript:var s = function() { var x = document.getElementsByClassName(\"scrollable\"); for(var i = 0; i < x.length; i++) { x[i].scrollTop += 200; } setTimeout(s, 1); }; s();");
        }
        #endregion
        #region Webbrowser kontrol
        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            //if ((int)e.CurrentProgress > 0)
            //{
            //    progressBar1.Maximum = (int)e.MaximumProgress;
            //    if (progressBar1.Maximum == (int)e.MaximumProgress)
            //        progressBar1.Value = 0;
            //    // progressBar1.Value = (int)e.CurrentProgress;
            //}
            //// int kbdeger = Convert.ToInt64(e.MaximumProgress);
            //kbdegerlb.Text = e.MaximumProgress.ToString();
        }
        #endregion
        #region girisyapfonction
        void girisyap()
        {
            try
            {
                eposta = listBox1.Items[caunt].ToString();


                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                SqlCommand komut = new SqlCommand("Select * from Kullanici Where @eposta=eposta", baglanti);
                komut.Parameters.AddWithValue("@eposta", listBox1.Items[caunt].ToString());
                SqlDataReader reader = komut.ExecuteReader();
                while (reader.Read())
                {
                    pw = (reader["pw"].ToString());
                }
                baglanti.Close();



                webBrowser1.Document.GetElementById("email").InnerText = eposta;
                webBrowser1.Document.GetElementById("pass").InnerText = pw;
                webBrowser1.Document.Forms[0].InvokeMember("submit");
            }
            catch (Exception)
            {
            }
        }
        void FastGirisyap()
        {
            try
            {
                eposta = listBox4.Items[caunt].ToString();
                webBrowser1.Document.GetElementById("email").InnerText = eposta;
                webBrowser1.Document.GetElementById("pass").InnerText = "1236asd";
                webBrowser1.Document.Forms[0].InvokeMember("submit");
                DonenPingDegeri = Convert.ToInt16(gecikmeOlc(textBox1.Text).ToString());
                PingLB.Text = "Son Gelen Ping = " + DonenPingDegeri.ToString();
                if (DonenPingDegeri > 500)
                {
                    facepaylasim.Stop();
                    ArkKabul.Stop();
                    İstekTMR.Stop();
                    ArkadasSayisiTmr.Stop();
                    TxtPaylasim.Stop();
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion
        #region Cikis fonksiyon
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
        #endregion
        #region Face Paylasim Yap Fonction
        void paylasimyap()
        {

            try
            {
                foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("textarea"))
                    if (item.GetAttribute("name") == "xhpc_message")
                        item.Focus();
                webBrowser1.Document.GetElementById("xhpc_message").SetAttribute("value", facelink.Text);

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
            {
            }

        }
        void paylasimYap2()
        {

        }
        #endregion
        #region Face Paylasim Timer
        int paylasimbeklet = 0;
        private void face_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            CauntDegerLB.Text = caunt.ToString();
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
                facepaylasim.Stop();
                bekletdegerlb.Text = paylasimbeklet.ToString();
                caunt = 0;
                listBox1.SelectedIndex = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
                if (1 == Convert.ToInt16(listBox3.Items[19]))
                {
                    TxtIcınVeriGunlukIslemDuzenle();
                    ReturnDegisken = ReturnDegisken2 - 10;
                }
                Environment.Exit(0);
            }


            //değer oku ve işlem yap

            bekletdegerlb.Text = paylasimbeklet.ToString();
            if (paylasimbeklet == 5)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (paylasimbeklet == 12)
            {
                girisyap();
                SqlKayitCekDüzenlePaylasim();
            }
            if (paylasimbeklet == 20)
            {
                // webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
                PaylasimKontrolFnc();
            }
            if (paylasimbeklet == 25)
            {
                paylasimyap();

            }
            if (paylasimbeklet == 33)
            {
                cikis();
            }
            if (paylasimbeklet == 35)
            {
                webBrowser1.Navigate("");
                paylasimbeklet = 0;
                caunt++;
                //TxtIcınVeriGunlukIslemDuzenle();
            }

        }
        #endregion
        #region Arkadaş Kabul Et Fonction
        int ArkadasKabulEtDegisken = 0;
        void ArkadasKabulET()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("button"))
                if (item.GetAttribute("value") == "1")
                {
                    if (item.InnerText == "Onayla")
                    {
                        ArkadasKabulEtDegisken++;
                        item.InvokeMember("click");
                    }

                }
        }
        #endregion
        #region Arkadaş İstek Yolla Fnc
        void ArkadasIstekYollaFnc()
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
        #endregion
        #region paylasim beklet timer
        int bekletdeger = 0;
        private void PaylasimBek_Tick(object sender, EventArgs e)
        {
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
                facepaylasim.Stop();
                bekletdegerlb.Text = paylasimbeklet.ToString();
                caunt = 0;
            }

        }
        #endregion
        #region arkadaş kabul timer
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
        private void ArkKabul_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                ModemResetDurumuKontrolFnc();
                //kaçıncıda oldugunu yaz
                CauntDegerLB.Text = caunt.ToString();
                textBox4.Text = "86";
                //değer saydir
                if (listBox1.Items.Count > caunt)
                {
                    if (arkadaskabuldeger < Convert.ToInt32(textBox4.Text))
                    {
                        arkadaskabuldeger++;
                        ArkBeklet.Text = arkadaskabuldeger.ToString();
                    }
                    else
                    {
                        arkadaskabuldeger = 1;
                        ArkBeklet.Text = arkadaskabuldeger.ToString();
                    }
                }
                else
                {
                    arkadaskabuldeger = 0;
                    // ArkKabul.Stop();
                    ArkBeklet.Text = arkadaskabuldeger.ToString();
                    caunt = 0;
                    dondur2++;
                    TxtIcınVeriDuzenle();
                }


                //değer oku ve işlem yap
                ArkBeklet.Text = arkadaskabuldeger.ToString();
                if (arkadaskabuldeger == 2)
                {
                    //ArkadasSayisiCekmeBireyselFnc();
                    //if (Convert.ToInt16(ArkadasSayisiBireyselTxt.Text) > 4900)
                    //{
                    //    caunt++;
                    //    arkadaskabuldeger = 1;
                    //}
                }
                if (arkadaskabuldeger == 5)
                {
                    webBrowser1.Navigate("http://facebook.com/");
                }
                if (arkadaskabuldeger == 10)
                {
                    girisyap();
                }
                if (arkadaskabuldeger == 20)
                {
                    webBrowser1.Navigate("http://facebook.com/profile.php?=");
                }
                if (arkadaskabuldeger == 35)
                {
                    html = webBrowser1.Document.Body.InnerHtml.ToString();
                    WebUrlTXT.Text = webBrowser1.Url.ToString();
                    ArkadasSayisiCekmeFnc();

                }
                if (arkadaskabuldeger == 35)
                {
                    if (ArkadaslikGenelDegisken.Length > 3)
                    {
                        //ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToUInt16(ArkadaslikGenelDegisken.Remove(1, 1));
                        ArkadaslikBireyselDegisken = Convert.ToUInt16(ArkadaslikGenelDegisken.Remove(1, 1));
                    }
                    else
                    {
                        // ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToUInt16(ArkadaslikGenelDegisken);
                        ArkadaslikBireyselDegisken = Convert.ToUInt16(ArkadaslikGenelDegisken);
                    }

                    if (ArkadaslikBireyselDegisken > 4960)
                    {
                        arkadaskabuldeger = 1;
                        ArkadasSaayisiEklemeBireysel();
                        cikis();
                        caunt++;
                    }
                    else
                    {
                        ArkadasSaayisiEklemeBireysel();
                        webBrowser1.Navigate("https://www.facebook.com/friends/requests/?fcref=jwl");
                    }
                }
                if (arkadaskabuldeger == 40)
                {
                    ArkadasKabulET();
                }
                if (arkadaskabuldeger == 44)
                {
                    if (ArkadasKabulEtDegisken < 2)
                    {
                        arkadaskabuldeger = 85;
                        ArkadasKabulEtDegisken = 0;
                        cikis();
                    }
                }
                if (arkadaskabuldeger == 80)
                {
                    cikis();
                }
                if (arkadaskabuldeger == 86)
                {
                    webBrowser1.Navigate("");
                    arkadaskabuldeger = 0;
                    caunt++;
                }
            }
            if (checkBox1.Checked == true)
            {
                ModemResetDurumuKontrolFnc();
                //kaçıncıda oldugunu yaz
                CauntDegerLB.Text = caunt.ToString();
                textBox4.Text = "86";
                //değer saydir
                if (listBox1.Items.Count > caunt)
                {
                    if (arkadaskabuldeger < Convert.ToInt32(textBox4.Text))
                    {
                        arkadaskabuldeger++;
                        ArkBeklet.Text = arkadaskabuldeger.ToString();
                    }
                    else
                    {
                        arkadaskabuldeger = 1;
                        ArkBeklet.Text = arkadaskabuldeger.ToString();
                    }
                }
                else
                {
                    arkadaskabuldeger = 0;
                    // ArkKabul.Stop();
                    ArkBeklet.Text = arkadaskabuldeger.ToString();
                    caunt = 0;
                    dondur2++;
                    TxtIcınVeriDuzenle();
                }


                //değer oku ve işlem yap
                ArkBeklet.Text = arkadaskabuldeger.ToString();
                if (arkadaskabuldeger == 2)
                {
                    ArkadasSayisiCekmeBireyselFnc();
                    if (Convert.ToInt16(ArkadasSayisiBireyselTxt.Text) > 4960)
                    {
                        caunt++;
                        arkadaskabuldeger = 1;
                    }
                }
                if (arkadaskabuldeger == 5)
                {
                    webBrowser1.Navigate("http://facebook.com/");
                }
                if (arkadaskabuldeger == 10)
                {
                    girisyap();
                }
                if (arkadaskabuldeger == 15)
                {
                    webBrowser1.Navigate("https://www.facebook.com/friends/requests/?fcref=jwl");
                }
                if (arkadaskabuldeger == 22)
                {
                    ArkadasKabulET();
                }
                if (arkadaskabuldeger == 23)
                {
                    if (ArkadasKabulEtDegisken < 2)
                    {
                        arkadaskabuldeger = 75;
                        ArkadasKabulEtDegisken = 0;
                        cikis();
                    }
                }
                if (arkadaskabuldeger == 60)
                {
                    webBrowser1.Navigate("http://facebook.com/profile.php?=");
                }
                if (arkadaskabuldeger == 70)
                {
                    html = webBrowser1.Document.Body.InnerHtml.ToString();
                    WebUrlTXT.Text = webBrowser1.Url.ToString();
                    ArkadasSayisiCekmeFnc();

                }
                if (arkadaskabuldeger == 72)
                {
                    if (ArkadaslikGenelDegisken.Length > 3)
                    {
                        //ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToUInt16(ArkadaslikGenelDegisken.Remove(1, 1));
                        ArkadaslikBireyselDegisken = Convert.ToUInt16(ArkadaslikGenelDegisken.Remove(1, 1));
                    }
                    else
                    {
                        // ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToUInt16(ArkadaslikGenelDegisken);
                        ArkadaslikBireyselDegisken = Convert.ToUInt16(ArkadaslikGenelDegisken);
                    }

                    if (ArkadaslikBireyselDegisken > 4960)
                    {
                        arkadaskabuldeger = 1;
                        ArkadasSaayisiEklemeBireysel();
                        cikis();
                        caunt++;
                    }
                    else
                    {
                        ArkadasSaayisiEklemeBireysel();
                        //  webBrowser1.Navigate("https://www.facebook.com/friends/requests/?fcref=jwl");
                    }
                }
                if (arkadaskabuldeger == 75)
                {
                    cikis();
                }
                if (arkadaskabuldeger == 77)
                {
                    webBrowser1.Navigate("");
                    arkadaskabuldeger = 0;
                    caunt++;
                }
            }
        }

        #endregion
        #region arkadaş kabul beklet timer
        int arkadaskabuldeger = 0;
        private void arkadaskabul_Tick(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > caunt)
            {
                if (arkadaskabuldeger < Convert.ToInt32(textBox4.Text))
                {
                    arkadaskabuldeger++;
                    ArkBeklet.Text = arkadaskabuldeger.ToString();
                }
                else
                {
                    arkadaskabuldeger = 1;
                    ArkBeklet.Text = arkadaskabuldeger.ToString();
                }
            }
            else
            {
                arkadaskabuldeger = 0;
                ArkKabul.Stop();
                ArkBeklet.Text = arkadaskabuldeger.ToString();
                caunt = 0;
            }

        }
        #endregion
        #region Arkadaş isteği yolla beklet tmr
        int istekbekletdeger = 0;
        private void İstekBekletTMR_Tick(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > caunt)
            {
                if (istekbekletdeger < Convert.ToInt32(textBox4.Text))
                {
                    istekbekletdeger++;
                    arkisteklb.Text = istekbekletdeger.ToString();
                }
                else
                {
                    istekbekletdeger = 1;
                    arkisteklb.Text = istekbekletdeger.ToString();
                }
            }
            else
            {
                İstekTMR.Stop();
                istekbekletdeger = 0;
                caunt = 0;
            }

        }
        #endregion
        #region İnterval Kontrol
        void IntervalKontrol()
        {
            //hızını ayarla
            if (comboBox1.SelectedIndex == 0)
            {
                facepaylasim.Interval = 1000;
                ArkKabul.Interval = 1000;
                İstekTMR.Interval = 1000;
                ArkadasSayisiTmr.Interval = 1000;
                TxtPaylasim.Interval = 1000;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                facepaylasim.Interval = 800;
                ArkKabul.Interval = 800;
                İstekTMR.Interval = 800;
                ArkadasSayisiTmr.Interval = 800;
                TxtPaylasim.Interval = 800;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                facepaylasim.Interval = 500;
                ArkKabul.Interval = 500;
                İstekTMR.Interval = 500;
                ArkadasSayisiTmr.Interval = 500;
                TxtPaylasim.Interval = 500;
            }
        }
        #endregion
        #region ArkadasKabulEt TMR
        private void İstekTMR_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                ModemResetDurumuKontrolFnc();
                //kaçıncıda oldugunu yaz
                CauntDegerLB.Text = caunt.ToString();

                //değer saydir
                if (listBox1.Items.Count > caunt)
                {
                    if (istekbekletdeger < Convert.ToInt32(textBox4.Text))
                    {
                        istekbekletdeger++;
                        arkisteklb.Text = istekbekletdeger.ToString();
                    }
                    else
                    {
                        istekbekletdeger = 1;
                        arkisteklb.Text = istekbekletdeger.ToString();
                    }
                }
                else
                {
                    İstekTMR.Stop();
                    istekbekletdeger = 0;
                    caunt = 0;
                    dondur2++;
                    TxtIcınVeriDuzenle();
                    if (1 == Convert.ToInt16(listBox3.Items[19]))
                    {
                        TxtIcınVeriGunlukIslemDuzenle();
                        ReturnDegisken = ReturnDegisken2 - 10;
                    }
                }

                //değer oku ve işlem yap
                arkisteklb.Text = istekbekletdeger.ToString();
                if (istekbekletdeger == 3)
                {
                    webBrowser1.Navigate("http://facebook.com/");
                    textBox4.Text = "55";
                }
                if (istekbekletdeger == 12)
                {
                    girisyap();
                }
                if (istekbekletdeger == 20)
                {
                    webBrowser1.Navigate("http://facebook.com/profile.php?=");
                }
                if (istekbekletdeger == 30)
                {
                    html = webBrowser1.Document.Body.InnerHtml.ToString();
                    WebUrlTXT.Text = webBrowser1.Url.ToString();
                    ArkadasSayisiCekmeFnc();

                }
                if (istekbekletdeger == 35)
                {
                    if (ArkadaslikGenelDegisken.Length > 3)
                    {
                        //ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToUInt16(ArkadaslikGenelDegisken.Remove(1, 1));
                        ArkadaslikBireyselDegisken = Convert.ToUInt16(ArkadaslikGenelDegisken.Remove(1, 1));
                    }
                    else
                    {
                        // ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToUInt16(ArkadaslikGenelDegisken);
                        ArkadaslikBireyselDegisken = Convert.ToUInt16(ArkadaslikGenelDegisken);
                    }

                    if (ArkadaslikBireyselDegisken > 4960)
                    {
                        istekbekletdeger = 1;
                        //  ArkadasSaayisiEklemeBireysel();
                        cikis();
                        caunt++;
                    }
                    else
                    {
                        webBrowser1.Navigate("https://www.facebook.com/");
                    }
                }

                if (istekbekletdeger == 45)
                {
                    ArkadasIstekYollaFnc();
                }
                if (istekbekletdeger == 50)
                {
                    cikis();
                    caunt++;
                }
                if (istekbekletdeger == 55)
                {

                    webBrowser1.Navigate("www.facebook.com.tr");
                    istekbekletdeger = 0;
                }
            }
            if (checkBox1.Checked == true)
            {
                ModemResetDurumuKontrolFnc();
                //kaçıncıda oldugunu yaz
                CauntDegerLB.Text = caunt.ToString();

                //değer saydir
                if (listBox1.Items.Count > caunt)
                {
                    if (istekbekletdeger < Convert.ToInt32(textBox4.Text))
                    {
                        istekbekletdeger++;
                        arkisteklb.Text = istekbekletdeger.ToString();
                    }
                    else
                    {
                        istekbekletdeger = 1;
                        arkisteklb.Text = istekbekletdeger.ToString();
                    }
                }
                else
                {
                    İstekTMR.Stop();
                    istekbekletdeger = 0;
                    caunt = 0;
                    dondur2++;
                    TxtIcınVeriDuzenle();
                    if (1 == Convert.ToInt16(listBox3.Items[19]))
                    {
                        TxtIcınVeriGunlukIslemDuzenle();
                        ReturnDegisken = ReturnDegisken2 - 10;
                    }
                }

                //değer oku ve işlem yap
                arkisteklb.Text = istekbekletdeger.ToString();
                if (istekbekletdeger == 2)
                {
                    ArkadasSayisiCekmeBireyselFnc();
                    if (Convert.ToInt16(ArkadasSayisiBireyselTxt.Text) > 4900)
                    {
                        caunt++;
                        istekbekletdeger = 1;
                    }
                }
                if (istekbekletdeger == 3)
                {
                    webBrowser1.Navigate("http://facebook.com/");
                }
                if (istekbekletdeger == 12)
                {
                    girisyap();
                }
                if (istekbekletdeger == 20)
                {
                    ArkadasIstekYollaFnc();
                }
                if (istekbekletdeger == 25)
                {
                    webBrowser1.Navigate("http://facebook.com/profile.php?=");
                }
                if (istekbekletdeger == 35)
                {
                    html = webBrowser1.Document.Body.InnerHtml.ToString();
                    WebUrlTXT.Text = webBrowser1.Url.ToString();
                    ArkadasSayisiCekmeFnc();

                }
                if (istekbekletdeger == 37)
                {
                    if (ArkadaslikGenelDegisken.Length > 3)
                    {
                        //ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToUInt16(ArkadaslikGenelDegisken.Remove(1, 1));
                        ArkadaslikBireyselDegisken = Convert.ToUInt16(ArkadaslikGenelDegisken.Remove(1, 1));
                    }
                    else
                    {
                        // ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToUInt16(ArkadaslikGenelDegisken);
                        ArkadaslikBireyselDegisken = Convert.ToUInt16(ArkadaslikGenelDegisken);
                    }

                    if (ArkadaslikBireyselDegisken > 4960)
                    {
                        istekbekletdeger = 1;
                        //  ArkadasSaayisiEklemeBireysel();
                        cikis();
                        caunt++;
                    }
                    else
                    {
                        // webBrowser1.Navigate("https://www.facebook.com/");
                    }
                }
                if (istekbekletdeger == 40)
                {
                    cikis();
                    caunt++;
                }
                if (istekbekletdeger == 43)
                {
                    webBrowser1.Navigate("www.facebook.com.tr");
                    istekbekletdeger = 0;
                }
            }

        }
        #endregion
        #region Sql Kayit Ekleme Fonksiyon
        void SqlKayitEkle()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                string kayit = "insert into Kullanici(id,eposta,pw,arkadasSayisi) values (@id,@eposta,@pw,@arkadasSayisi)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                komut.Parameters.AddWithValue("@id", i);
                komut.Parameters.AddWithValue("@eposta", listBox1.Items[i].ToString());
                komut.Parameters.AddWithValue("@pw", "1236asd");
                komut.Parameters.AddWithValue("@arkadasSayisi", 1);
                komut.ExecuteNonQuery();
                baglanti.Close();
            }

        }
        #endregion
        #region Sql Kayit güncelleme
        void sqlKayitGüncelleme()

        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            string silme = "Delete From Kullanici";
            SqlCommand silmekomut = new SqlCommand(silme, baglanti);
            silmekomut.ExecuteNonQuery();
            SqlKayitEkle();
            baglanti.Close();
        }
        #endregion
        #region Sql Veri Çekme
        void SqlKayitCek()
        {

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Kullanici", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader["eposta"].ToString());
            }
            baglanti.Close();
        }
        string ArkadasListesiTut = "";
        void SqlArkadasListCek()
        {
            //if (baglanti.State == ConnectionState.Closed)
            //    baglanti.Open();

            //SqlCommand komut = new SqlCommand("Select * from Kullanici where eposta=@eposta", baglanti);
            //komut.Parameters.AddWithValue("@eposta", listBox1.Items[caunt].ToString());
            //SqlDataReader reader = komut.ExecuteReader();
            //while (reader.Read())
            //{
            //    ArkadasListesiTut = (reader["ArkadasListesi"].ToString());
            //}
            //baglanti.Close();
            arr6.Clear();
            string b = "\\";
            string yol = "C:\\Yönet" + b + "linkler.txt";
            StreamReader oku;
            oku = File.OpenText(yol);
            string yazi;
            while ((yazi = oku.ReadLine()) != null)
            {
                arr6.Add(yazi.ToString());
               // listBox5.Items.Add(yazi.ToString());
            }
            oku.Close();
            string[] parcalar;
            for (int i = 0; i < arr6.Count; i++)
            {
                ArkadasListesiTut = arr6[i].ToString();
                parcalar = ArkadasListesiTut.Split('-');
                foreach (string i2 in parcalar)
                {
                   // arr6.Add(i2); // veri tabanından gelen ham veri
                    arr7.Add(i2);
                    listBox5.Items.Add(i2);
                }
            }

            //SqlArkadasListesiCek2();
        }
        void SqlIslemKayitCek()
        {

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from GunlukList", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox2.Items.Add(reader["isList"].ToString());
            }
            baglanti.Close();
        }
        void SqlFastKullaniciKayitCek()
        {

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from ResimPaylasim", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox4.Items.Add(reader["Eposta"].ToString());
            }
            baglanti.Close();
        }

        #endregion
        #region Butonlar ve tmrlar
        private void ArkadasSayisiTmr_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            textBox4.Text = "40";
            //kaçıncıda oldugunu yaz
            CauntDegerLB.Text = caunt.ToString();

            //Değer Saydir 
            arkadassayisilb.Text = arkadassayisideger.ToString();
            if (listBox1.Items.Count > caunt)
            {
                if (arkadassayisideger < Convert.ToInt32(textBox4.Text))
                {
                    arkadassayisideger++;
                    arkadassayisilb.Text = arkadassayisideger.ToString();
                }
                else
                {
                    arkadassayisideger = 1;
                    arkadassayisilb.Text = arkadassayisideger.ToString();
                }
            }
            else
            {
                arkadassayisilb.Text = arkadassayisideger.ToString();
                ArkadasSayisiTmr.Stop();
                arkadassayisideger = 0;
                caunt = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
                if (Convert.ToInt32(MevcutArkadasSayisiTxt.Text) < Convert.ToInt32(ArkadasSayisiTxt.Text))
                {
                    SqlArkadasSayisiEkleme();
                    ArkadaslikGenelDegisken = "0";
                }
                ArkadasSayisiTxt.Text = "";

            }


            //değer oku ve işlem yap
            arkadassayisilb.Text = arkadassayisideger.ToString();
            if (arkadassayisideger == 5)
            {
                webBrowser1.Navigate("http://facebook.com/");
                ArkadasSayisiCekmeSorgusuFnc();
            }
            if (arkadassayisideger == 15)
            {
                girisyap();
            }
            if (arkadassayisideger == 25)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=");
            }
            if (arkadassayisideger == 35)
            {
                html = webBrowser1.Document.Body.InnerHtml.ToString();
                WebUrlTXT.Text = webBrowser1.Url.ToString();
                ArkadasSayisiCekmeFnc();
                ArkadasSaayisiEklemeBireysel();
                cikis();
            }
            //if (arkadassayisideger == 45)
            //{

            //}
            if (arkadassayisideger == 40)
            {
                arkadaskabuldeger = 0;
                try
                {
                    CauntDegerLB.Text = caunt.ToString();
                    arkadaskabuldeger = 0;
                    if (ArkadaslikGenelDegisken.Length > 3)
                    {
                        ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToUInt16(ArkadaslikGenelDegisken.Remove(1, 1));
                        ArkadaslikBireyselDegisken = Convert.ToUInt16(ArkadaslikGenelDegisken.Remove(1, 1));
                    }
                    else
                    {
                        ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToUInt16(ArkadaslikGenelDegisken);
                        ArkadaslikBireyselDegisken = Convert.ToUInt16(ArkadaslikGenelDegisken);
                    }

                    ArkadasSayisiTxt.Text = ArkadasSayisiTut.ToString();
                    ArkadasSaayisiEklemeBireysel();
                }
                catch (Exception alınanhata)
                {
                    arkadassayisideger = 0;
                    caunt = 0;
                    ArkadasSayisiTut = 0;
                    ArkadaslikGenelDegisken = "0";
                    hataLB.Text = caunt + alınanhata.ToString();
                }
                caunt++;
                webBrowser1.Navigate("");
                arkadaskabuldeger = 0;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            caunt++;
            CauntDegerLB.Text = caunt.ToString();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            sqlKayitGüncelleme();
        }
        private void button7_Click_1(object sender, EventArgs e)
        {
            cikis();
            textBox4.Text = "55";
            ArkadaslikGenelDegisken = "0";
            ArkadasSayisiTut = 0;
            ArkadasSayisiTxt.Text = "0";
            IntervalKontrol();
            ArkadasSayisiCekmeSorgusuFnc();
            ArkadasSayisiTmr.Start();
            //ArkSayisiTMR.Start();
        }


        private Pylsm paylasimgetir(int paylasim_id)
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT * FROM Paylasim WHERE id = @id", baglanti);
            komut2.Parameters.AddWithValue("@id", paylasim_id);
            SqlDataReader dr = komut2.ExecuteReader();
            if (dr.Read())
            {
                Pylsm paylasim = new Pylsm { Id = paylasim_id, Paylasim = dr["paylasimNvarchar"].ToString(), Paylasanlar = dr["paylasilanEposta"].ToString() };
                dr.Close();
                baglanti.Close();
                return paylasim;
            }
            else
            {
                dr.Close();
                baglanti.Close();
                return null;
            }

        }

        private int paylastiOlarakIsaretle(int paylasim_id, String onceden_paylasmis_epostalar, String paylasan_eposta)
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komut2 = new SqlCommand("UPDATE Paylasim SET paylasilanEposta = @epostalar WHERE id = @id", baglanti);
            komut2.Parameters.AddWithValue("@id", paylasim_id);
            komut2.Parameters.AddWithValue("@epostalar", onceden_paylasmis_epostalar + paylasan_eposta);
            int sonuc = komut2.ExecuteNonQuery();

            baglanti.Close();
            return sonuc;
        }
        string a;
        Ping p = new Ping();
        PingReply pr;
        int DonenPingDegeri = 0;
        private void button3_Click_1(object sender, EventArgs e)
        {
            cikis();

        }
        public bool InternetKontrol()
        {
            try
            {
                System.Net.Sockets.TcpClient kontrol_client = new System.Net.Sockets.TcpClient("www.google.com.tr", 80);
                kontrol_client.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Text = "200";
            cikis();
            IntervalKontrol();
            ArkKabul.Start();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            textBox4.Text = "45";
            cikis();
            IntervalKontrol();
            İstekTMR.Start();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = "50";
            cikis();
            IntervalKontrol();
            facepaylasim.Start();
        }
        int gunlukIslemDegisken;
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

            int modemResetDegerTutDegisken = Convert.ToUInt16(ModemResetDurmu);
            if (modemResetDegerTutDegisken == 1)
            {
                Environment.Exit(0);
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate("https://www.facebook.com/");
            comboBox1.SelectedIndex = 0;
            //baglanti.Open();
            SqlKayitCek();
            //SqlIslemKayitCek();
            SqlFastKullaniciKayitCek();
            TxtVeriCek();
            listBox1.SelectedIndex = 0;
            PotansiyelLb.Text = "Potansiyel = " + (listBox1.Items.Count * 5000) + "--" + listBox1.Items.Count.ToString();
            ReturnDegisken2 = Convert.ToInt16(listBox3.Items[3]);
            caunt = Convert.ToInt16(listBox3.Items[1]);
            if (1 == Convert.ToInt16(listBox3.Items[5]))
            {
                ArkKabul.Start();
            }
            if (1 == Convert.ToInt16(listBox3.Items[7]))
            {
                SayfaDavetTmr.Start();
            }
            if (1 == Convert.ToInt16(listBox3.Items[9]))
            {
                ArkadasSayisiTmr.Start();
            }
            if (1 == Convert.ToInt16(listBox3.Items[11]))
            {
                İstekTMR.Start();
            }
            if (1 == Convert.ToInt16(listBox3.Items[13]))
            {
                fastArkadasKabul.Start();
            }
            if (1 == Convert.ToInt16(listBox3.Items[15]) || 1 == Convert.ToInt16(listBox3.Items[27]) || 1 == Convert.ToInt16(listBox3.Items[29]))
            {
                FaceAccs faceAc = new FaceAccs();
                faceAc.ShowDialog();
                ReturnTmr.Stop();
            }
            if (1 == Convert.ToInt16(listBox3.Items[17]))
            {
                YorumSilTmr.Start();
            }
            if (1 == Convert.ToInt16(listBox3.Items[19]))
            {
                gunlukIslemDegisken = Convert.ToInt16(listBox3.Items[21]);
                if (1 == Convert.ToInt16(listBox3.Items[21]))
                {
                    facepaylasim.Start();
                    gunlukIslemDegisken++;
                }
                if (2 == Convert.ToInt16(listBox3.Items[21]))
                {
                    // yorum sili döngüye entegre etmek için txt veri düzenle me return degisken kismini pasif yapıp yorumsiltmr i aktifleştirmek yeterli.
                    //YorumSilTmr.Start();  
                    gunlukIslemDegisken++;
                    TxtIcınVeriGunlukIslemDuzenle();
                    ReturnDegisken = ReturnDegisken2 - 10;
                }
                if (3 == Convert.ToInt16(listBox3.Items[21]))
                {
                    İstekTMR.Start();
                    gunlukIslemDegisken++;
                }
                if (4 == Convert.ToInt16(listBox3.Items[21]))
                {
                    //FaceAccs faceAc = new FaceAccs();
                    //faceAc.ShowDialog();
                    //ReturnTmr.Stop();
                }
            }
            if (0 == Convert.ToInt16(listBox3.Items[23]))
            {
                checkBox1.Checked = false;
            }
            if (1 == Convert.ToInt16(listBox3.Items[25]))
            {
                ReturnTmr.Stop();
                SayfaFotoPaylasTmr.Start();
            }
            if (1 == Convert.ToInt16(listBox3.Items[31]))
            {
                BegenTmr.Start();
            }
            if (1 == Convert.ToInt16(listBox3.Items[33]))
            {
                FotoPaylasTmr.Start();
            }
            if (1 == Convert.ToInt16(listBox3.Items[35]))
            {
                facepaylasim.Start();
            }
            if (1 == Convert.ToInt16(listBox3.Items[37]))
            {
                SayfadakiPaylasimTmr.Start();
            }
            if (1 == Convert.ToInt16(listBox3.Items[39]))
            {
                SonPaylasimSilTmr.Start();
            }
            if (1 == Convert.ToInt16(listBox3.Items[45]))
            {
                FotoCek.Start();
                ReturnTmr.Stop();
                listBox1.Items.Clear();
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                SqlCommand komut = new SqlCommand("Select * from Kullanici where FotoDurum = 0", baglanti);
                SqlDataReader reader = komut.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader["eposta"].ToString());
                }
                baglanti.Close();
            }
            SayfaDavetTxt.Text = listBox3.Items[47].ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }
        private void Beklet_Tick(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            SqlKayitEkle();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            SqlKayitCek();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
        }
        string Degerr = "";
        private void button6_Click(object sender, EventArgs e)
        {
            PaylasanTxt.Text = webBrowser1.Document.Body.InnerHtml.ToString();
            listBox6.Items.Clear();
            string[] parcalar;
            parcalar = PaylasanTxt.Text.Split('<');

            foreach (string i in parcalar)
            {
                listBox6.Items.Add(i);
            }
        }
        // 

        #endregion
        #region WEB browser işlemleri
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                DonenPingDegeri = Convert.ToInt16(gecikmeOlc(textBox1.Text).ToString());
                PingLB.Text = "Son Gelen Ping = " + DonenPingDegeri.ToString();
                if (DonenPingDegeri > 500)
                {
                    SayfaDavetTmr.Stop();
                    facepaylasim.Stop();
                    ArkKabul.Stop();
                    İstekTMR.Stop();
                    ArkadasSayisiTmr.Stop();
                    TxtPaylasim.Stop();
                    fastArkadasKabul.Stop();
                    YorumSilTmr.Stop();
                    FotoPaylasTmr.Stop();
                    ReturnDegisken = ReturnDegisken2 - 100;
                }
                //if (a != "Success")
                //{
                //    facepaylasim.Stop();
                //    ArkKabul.Stop();
                //    İstekTMR.Stop();
                //    ArkadasSayisiTmr.Stop();
                //    TxtPaylasim.Stop();
                //    fastArkadasKabul.Stop();
                //    YorumSilTmr.Stop();
                //}
                html = webBrowser1.Document.Body.InnerHtml.ToString();
                WebUrlTXT.Text = webBrowser1.Url.ToString();
                GuvenlikKontrolUrlFnc();
            }
            catch (Exception)
            {
            }

        }
        void GuvenlikKontrolUrlFnc()
        {
            string KontrolDegerTutStrng = WebUrlTXT.Text.IndexOf("checkpoint").ToString();
            int KontrolDegerTutİnt = Convert.ToInt16(KontrolDegerTutStrng);
            if (KontrolDegerTutİnt > 1)
            {
                if (1 == Convert.ToInt16(listBox3.Items[13]))
                {
                    listBox5.Items.Clear();
                    listBox6.Items.Clear();
                    listBox7.Items.Clear();
                    hataLB.Text = "yeni hesaplardan" + caunt.ToString() + "black liste eklenmeli";
                    SqlKayitCekDüzenliBlackList();
                    listBox6.Items.Add(listBox4.Items[caunt].ToString());
                    IdDuzenle();
                    SqlKayitSilDuzenlemeBlackList();
                    SqlKayitEkleDuzenlemeBlackList();
                }
                else
                {
                    listBox5.Items.Clear();
                    listBox6.Items.Clear();
                    listBox7.Items.Clear();
                    //  ArkKabul.Stop();
                    SqlKayitCekDüzenliBlackList();
                    listBox6.Items.Add(listBox1.Items[caunt].ToString());
                    IdDuzenle();
                    SqlKayitSilDuzenlemeBlackList();
                    SqlKayitEkleDuzenlemeBlackList();
                    SqlKullaniciBlackOlanıTemizleFnc();
                    listBox1.Items.Clear();
                    SqlKayitCek();
                    cikis();
                    arkadaskabuldeger = 0;
                    paylasimbeklet = 0;
                    istekbekletdeger = 0;
                    arkadassayisideger = 0;
                    sayfaDavetDeger = 0;
                    fastArakdasKabulDegerDegisken = 0;
                    ModemResetDurumu = 1;
                    ModemResetVtEkleFnc();
                }

            }


        }
        void SqlKullaniciBlackOlanıTemizleFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            int SonIndexDegerArttir = Convert.ToInt16(sonindexDegisken);
            string kayit = "Delete from Kullanici WHERE eposta = @eposta";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@eposta", listBox1.Items[caunt].ToString());
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();

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
        #endregion  
        #region Paylasilcak Metin Ekle Fnc
        void SqlBlackListEkleSorgusuFnc()
        {

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            string kayit = "insert into BlackList(id,eposta) values (@id,@eposta)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@id", 1);
            if (fastArkadasKabul.Enabled == true)
            {
                komutpaylasim.Parameters.AddWithValue("@eposta", listBox4.Items[caunt].ToString());
                komutpaylasim.ExecuteNonQuery();
                baglanti.Close();
            }
            else
            {
                komutpaylasim.Parameters.AddWithValue("@eposta", listBox1.Items[caunt].ToString());
                komutpaylasim.ExecuteNonQuery();
                baglanti.Close();
            }


        }


        #endregion
        #region Arkadas Sayısı Cekme tüm Fonksiyonlar
        void ArkadasSayisiCekmeBireyselFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Kullanici Where eposta=@eposta", baglanti);
            komut.Parameters.AddWithValue("@eposta", listBox1.Items[caunt]);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                ArkadasSayisiBireyselTxt.Text = (reader["arkadasSayisi"].ToString());
            }
            baglanti.Close();
        }

        void ArkadasSaayisiEklemeBireysel()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE Kullanici SET arkadasSayisi = @arkadasSayisi WHERE eposta = @eposta", baglanti);
            komut.Parameters.AddWithValue("@eposta", listBox1.Items[caunt].ToString());
            komut.Parameters.AddWithValue("@arkadasSayisi", Convert.ToInt32(ArkadaslikBireyselDegisken));
            komut.ExecuteNonQuery();
            baglanti.Close();
        }
        void fastArkadasSayisiEklemeBirekselFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE fastKullanici SET arkadasSayisi = @arkadasSayisi WHERE id = @id", baglanti);
            komut.Parameters.AddWithValue("@id", caunt);
            komut.Parameters.AddWithValue("@arkadasSayisi", Convert.ToInt32(ArkadaslikBireyselDegisken));
            komut.ExecuteNonQuery();
            baglanti.Close();
        }
        void SqlArkadasSayisiEkleme()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            //Select * from tblstsabit where stok_adi is null
            string silme = "Delete From Arkadaslar WHERE ToplamArkadasSayisi is not null";
            SqlCommand silmekomut = new SqlCommand(silme, baglanti);
            silmekomut.ExecuteNonQuery();

            string kayit2 = "insert into Arkadaslar(ToplamArkadasSayisi) values (@ToplamArkadasSayisi)";
            SqlCommand komut2 = new SqlCommand(kayit2, baglanti);
            komut2.Parameters.AddWithValue("@ToplamArkadasSayisi", Convert.ToInt32(ArkadasSayisiTxt.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
        }
        void ArkadasSayisiCekmeSorgusuFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Arkadaslar", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                MevcutArkadasSayisiTxt.Text = (reader["ToplamArkadasSayisi"].ToString());
            }
            baglanti.Close();
        }


        void ArkadasSayisiCek()
        {
            string x = "<a class=\"_51sx\" href=\"";
            string y = html.Substring(html.IndexOf(x) + x.Length, html.Length - (html.IndexOf(x) + x.Length));
            string z = "\" > Arkadaşlar </ a > ";
            url = y.Substring(0, y.IndexOf("\""));
            if (url != "")
            {
                webBrowser1.Navigate(url);
            }
        }
        int arkadassayisideger = 0;
        private void ArkSayisiTMR_Tick(object sender, EventArgs e)
        {
            arkadassayisilb.Text = arkadassayisideger.ToString();
            if (listBox1.Items.Count > caunt)
            {
                if (arkadassayisideger < Convert.ToInt32(textBox4.Text))
                {
                    arkadassayisideger++;
                    arkadassayisilb.Text = arkadassayisideger.ToString();
                }
                else
                {
                    arkadassayisideger = 1;
                    arkadassayisilb.Text = arkadassayisideger.ToString();
                }
            }
            else
            {
                arkadassayisilb.Text = arkadassayisideger.ToString();
                ArkadasSayisiTmr.Stop();
                arkadassayisideger = 0;
                caunt = 0;
                if (Convert.ToInt32(MevcutArkadasSayisiTxt.Text) < Convert.ToInt32(ArkadasSayisiTxt.Text))
                {
                    SqlArkadasSayisiEkleme();
                    ArkadaslikGenelDegisken = "0";
                }

            }
        }
        void KullaniciAdiCekmeVeArkadasSayfasınaGitmeFnc()
        {
            try
            {
                acıkurl = webBrowser1.Url.ToString();
                acıkurl = acıkurl.Remove(0, 25);
                listBox2.Items.Add(acıkurl);
            }
            catch (Exception)
            {

                throw;
            }

        }
        void ArkadasSayisiCekmeFnc()
        {
            try
            {
                string start_index_text = "<span class=\"_gs6\">";
                string yeni_html = html.Substring(html.IndexOf(start_index_text) + start_index_text.Length, html.Length - (html.IndexOf(start_index_text) + start_index_text.Length));
                ArkadaslikGenelDegisken = yeni_html.Substring(0, yeni_html.IndexOf("</span>"));
                textBox2.Text = ArkadaslikGenelDegisken;
            }
            catch (Exception)
            {
            }

        }
        #endregion
        #region Sayfa Davet TMR
        int sayfaDavetDeger = 0;
        private void SayfaDavetTmr_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            //kaçıncıda oldugunu yaz
            CauntDegerLB.Text = caunt.ToString();
            textBox4.Text = "250";

            //Değer Saydir 
            SayfaDavetLb.Text = sayfaDavetDeger.ToString();
            if (listBox1.Items.Count > caunt)
            {
                if (sayfaDavetDeger < Convert.ToInt32(textBox4.Text))
                {
                    sayfaDavetDeger++;
                    SayfaDavetLb.Text = sayfaDavetDeger.ToString();
                }
                else
                {
                    sayfaDavetDeger = 1;
                    SayfaDavetLb.Text = sayfaDavetDeger.ToString();
                }
            }
            else
            {
                SayfaDavetLb.Text = sayfaDavetDeger.ToString();
                sayfaDavetDeger = 0;
                caunt = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
            }


            //değer oku ve işlem yap
            arkadassayisilb.Text = arkadassayisideger.ToString();
            if (sayfaDavetDeger == 8)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (sayfaDavetDeger == 15)
            {
                girisyap();
            }
            if (sayfaDavetDeger == 25)
            {
                webBrowser1.Navigate(SayfaDavetTxt.Text);
            }
            if (sayfaDavetDeger == 45)
            {
                SayfaDavetTıklaFnc();
            }
            if (sayfaDavetDeger == 55)
            {
                SayfaDavetScrollBarKaydirFnc();
            }
            if (sayfaDavetDeger == 175)
            {
                SayfaDavetYolla();
                if (SayfaDavetEdilecekKisiSayisiDegisken < 3)
                {
                    sayfaDavetDeger = 248;
                    SayfaDavetEdilecekKisiSayisiDegisken = 0;
                }
                SayfaDavetEdilecekKisiSayisiDegisken = 0;
            }
            if (sayfaDavetDeger == 235)
            {
                cikis();
            }
            if (sayfaDavetDeger == 250)
            {
                webBrowser1.Navigate("");
                caunt++;
                sayfaDavetDeger = 0;
                cikis();
            }


        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox4.Text = "250";
            cikis();
            IntervalKontrol();
            SayfaDavetTmr.Start();
        }
        #endregion
        #region Sayfa Davet Buton Tıkla Fnc
        void SayfaDavetTıklaFnc()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("div"))
                {
                    if (div.InnerText == "Arkadaşlarını bu Sayfayı beğenmeye davet et")
                    {
                        item.InvokeMember("click");
                    }
                }
            }
        }
        #endregion
        #region Sayfa Davet Yolla Fnc
        int SayfaDavetEdilecekKisiSayisiDegisken = 0;
        void SayfaDavetYolla()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Davet Et")
                    {
                        SayfaDavetEdilecekKisiSayisiDegisken++;
                        if (SayfaDavetEdilecekKisiSayisiDegisken < 100)
                        {
                            item.InvokeMember("click");
                        }
                    }
                }
            }
        }
        #endregion
        #region Programlama Timer
        private void Programla_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            if (ArkKabul.Enabled == true && SayfaDavetTmr.Enabled == true)
            {
                dondur1 = Convert.ToInt16(dondurTxt.Text);
                if (dondur1 < dondur2)
                {
                    ArkKabul.Stop();
                    SayfaDavetTmr.Stop();
                    fastArkadasKabul.Stop();
                    dondur2 = 0;
                    Environment.Exit(0);
                    System.Diagnostics.Process.Start(@"C:\facebook paylasim.exe");

                }
            }
        }
        #endregion
        private void button11_Click(object sender, EventArgs e)
        {
            IslemSaniyesi frm2 = new IslemSaniyesi();
            frm2.Show();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Paylasim frm3 = new Paylasim();
            frm3.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                indirilecek = listBox2.Items[i].ToString();
                //dosyaAdi2 = listBox10.Items[indirmeindexName].ToString();
                dosyaAdi = listBox10.Items[indirmeindexName].ToString() + i.ToString();
                indir();
            }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            PaylasmismiKontrolET();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = listBox2.SelectedItem.ToString();
        }
        #region paylasimDeğerArttirarak
        void PaylasildiOlarakİsaretle()
        {

            listBox1.Items.IndexOf(textBox1.Text).ToString();
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen hesap seçin!");
                return;
            }
            if (listBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen paylaşım seçin!");
                return;
            }

            Pylsm paylasim = paylasimgetir(Convert.ToInt32(listBox2.SelectedValue));
            if (paylasim != null)
            {
                if (paylasim.Paylasanlar.IndexOf(listBox1.SelectedItem.ToString()) >= 0)
                {
                    MessageBox.Show("Bu hesap zaten paylaşmış!");
                }
                else
                {
                    int id = paylasim.Id;
                    String paylasanlar = paylasim.Paylasanlar;
                    String paylasan = listBox1.SelectedItem.ToString();
                    if (paylastiOlarakIsaretle(id, paylasanlar, paylasan) > 0)
                    {
                        //   MessageBox.Show("Paylaştı olarak işaretlendi!");
                    }
                    else
                    {
                        MessageBox.Show("Hata");
                    }
                }
            }
            else
            {
                MessageBox.Show("Paylaşım bulunamadı!");
            }
        }

        void PaylasmismiKontrolET()
        {

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                if (listBox1.SelectedIndex < 0)
                {
                    // MessageBox.Show("Lütfen hesap seçin!");
                    return;
                }
                if (listBox2.SelectedIndex < 0)
                {
                    //MessageBox.Show("Lütfen paylaşım seçin!");
                    return;
                }

                Pylsm paylasim = paylasimgetir(Convert.ToInt32(listBox2.SelectedValue));
                if (paylasim != null)
                {
                    if (paylasim.Paylasanlar.IndexOf(listBox1.SelectedItem.ToString()) >= 0)
                    {
                        //  MessageBox.Show("Bu hesap zaten paylaşmış!");
                        if (listBox2.SelectedIndex < listBox2.Items.Count)
                        {
                            listBox2.SelectedIndex++;
                            PaylasmismiKontrolET();
                        }
                        else
                        {
                            //   listBox1.SelectedIndex++;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Bu hesap paylaşmamış!");
                        facelink.Text = listBox2.Text;
                    }
                }
                else
                {
                    // MessageBox.Show("Paylaşım bulunamadı!");
                }

            }
            catch (Exception sorunnn)
            {
                MessageBox.Show(sorunnn.ToString());
            }

        }
        void PaylasimlariCekFnc()
        {

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from Paylasim", baglanti);
            komut.Parameters.AddWithValue("@id", caunt2);
            SqlDataReader reader = komut.ExecuteReader();

            List<Pylsm> paylasimlar = new List<Pylsm>();

            while (reader.Read())
            {
                paylasimlar.Add(new Pylsm { Id = Convert.ToInt32(reader["id"]), Paylasim = reader["paylasimNvarchar"].ToString() });

            }
            reader.Close();
            baglanti.Close();

            listBox2.DataSource = paylasimlar;
            listBox2.DisplayMember = "Paylasim";
            listBox2.ValueMember = "Id";

            // facelink.Text = listBox2.Text;
        }
        #endregion
        private void button18_Click(object sender, EventArgs e)
        {
            textBox4.Text = "40";
            cikis();
            IntervalKontrol();
            TxtPaylasim.Start();
        }

        private void TxtPaylasim_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            //kaçıncıda oldugunu yaz
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
                TxtPaylasim.Stop();
                bekletdegerlb.Text = paylasimbeklet.ToString();
                caunt = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
            }


            //değer oku ve işlem yap

            bekletdegerlb.Text = paylasimbeklet.ToString();
            if (paylasimbeklet == 5)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            else if (paylasimbeklet == 12)
            {
                girisyap();
            }
            if (paylasimbeklet == 22)
            {
                paylasimyap();
            }
            else if (paylasimbeklet == 28)
            {
                cikis();
            }
            else if (paylasimbeklet == 30)
            {
                webBrowser1.Navigate("www.facebook.com.tr");
                paylasimbeklet = 0;
                caunt++;
            }
        }

        private void ReturnTmr_Tick(object sender, EventArgs e)
        {


            //bool kontrol = InternetKontrol(); // Kontrol fonksiyonumuzu çağırdık
            //                                  // Eğer internet varsa true yoksa false değeri gelecek. Bunu if ile kontrol edelim
            //if (kontrol == true)
            //{

            //}
            //else
            //{
            //    facepaylasim.Stop();
            //    ArkKabul.Stop();
            //    İstekTMR.Stop();
            //    ArkadasSayisiTmr.Stop();
            //    SayfaDavetTmr.Stop();
            //    TxtPaylasim.Stop();
            //    fastArkadasKabul.Stop();
            //    YorumSilTmr.Stop();
            //}

            ReturnDegisken++;
            ReTurnLbl.Text = ReturnDegisken.ToString();
            if (ReturnDegisken == ReturnDegisken2 - 2)
            {
                TxtIcınVeriDuzenle();
                cikis();
                System.Diagnostics.Process.Start(@"C:\facebook paylasim.exe");

            }
            if (ReturnDegisken == ReturnDegisken2)
            {
                Environment.Exit(0);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            SqlKayitCekDüzenli();
            listBox6.Items.Add(textBox1.Text);
            listBox7.Items.Add("1");
            IdDuzenle();
            SqlKayitSilDuzenleme();
            SqlKayitEkleDuzenleme();
            //sqlKayitGüncelleme();
            //listBox1.Items.Clear();
            //SqlKayitCek();
        }
        string sonindexDegisken = "-1";
        void FastArkKabulVtEkleFnc()
        {
            SonIndexSayisiFastKullaniciFnc();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komutsilme = new SqlCommand("Delete From fastKullanici", baglanti);
            komutsilme.ExecuteReader();
            baglanti.Close();
            for (int i = 0; i < listBox4.Items.Count; i++)
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                int SonIndexDegerArttir = Convert.ToInt16(sonindexDegisken);
                SonIndexDegerArttir++;
                string kayit = "insert into fastKullanici(id,eposta,arkadasSayisi) values (@id,@eposta,@arkadasSayisi)";
                SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
                komutpaylasim.Parameters.AddWithValue("@id", i);
                komutpaylasim.Parameters.AddWithValue("@eposta", listBox4.Items[i].ToString());
                komutpaylasim.Parameters.AddWithValue("@arkadasSayisi", 0);
                komutpaylasim.ExecuteNonQuery();
                baglanti.Close();
            }
        }


        #region txt işlemleri
        void TxtVeriYaz()
        {
            string b = "\\";
            string yol = "C:\\Yönet" + b + "Yönet.txt";
            // StreamReader yaz;


            StreamWriter s = new StreamWriter(yol);
            for (int i = 0; i < listBox3.Items.Count; i++)
            {

                s.WriteLine(listBox3.Items[i].ToString());

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
                listBox3.Items.Add(yazi.ToString());
            }

            // Okumayı kapat.
            oku.Close();
        }

        void TxtIcınVeriDuzenle()
        {
            string[] veriler1 = new string[listBox3.Items.Count];
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                veriler1[i] = listBox3.Items[i].ToString();
            }
            veriler1[1] = caunt.ToString();
            listBox3.Items.Clear();
            for (int i = 0; i < veriler1.Length; i++)
            {
                listBox3.Items.Add(veriler1[i]);
            }
            TxtVeriYaz();
        }
        void TxtIcınVeriGunlukIslemDuzenle()
        {
            if (gunlukIslemDegisken > 4)
            {
                gunlukIslemDegisken = 1;
            }
            string[] veriler1 = new string[listBox3.Items.Count];
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                veriler1[i] = listBox3.Items[i].ToString();
            }
            veriler1[21] = gunlukIslemDegisken.ToString();
            listBox3.Items.Clear();
            for (int i = 0; i < veriler1.Length; i++)
            {
                listBox3.Items.Add(veriler1[i]);
            }
            TxtVeriYaz();
        }
        #endregion
        #region Son index Sayisi Çek
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

        void SonIndexSayisiFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            sonindexDegisken = "-1";
            SqlCommand komut = new SqlCommand("Select * from Kullanici WHERE id is not null", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                sonindexDegisken = (reader["id"].ToString());
            }
            baglanti.Close();
        }
        #endregion  
        #region Paylasilcak Metin Ekle Fnc
        void paylasilacakMetinEkleFnc()
        {
            SonIndexSayisiFnc();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            int SonIndexDegerArttir = Convert.ToInt16(sonindexDegisken);
            SonIndexDegerArttir++;
            string kayit = "insert into Kullanici(id,eposta,pw,arkadasSayisi) values (@id,@eposta,@pw,@arkadasSayisi)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@id", SonIndexDegerArttir);
            komutpaylasim.Parameters.AddWithValue("@eposta", textBox1.Text);
            komutpaylasim.Parameters.AddWithValue("@pw", pw);
            komutpaylasim.Parameters.AddWithValue("@arkadasSayisi", 0);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
        }
        #endregion
        #region Ping yolla
        int gecikmeOlc(string IpOrLink)
        {
            try
            {
                pr = p.Send("www.google.com");//adında ping ile ilgili sonuçarı tutacak bir değişken tanımladık.Ping yollayacağı adresi "p.send(textbox1.text)" ile belirtmiştik yani textbox1 de yazacağınız adrese ping atacak.
                a = pr.Status.ToString();//Bu değişken yollanan ping yerine ulaşıp ulaşmadığı bilgisini verir
                return Convert.ToInt32(pr.RoundtripTime);
            }
            catch
            {

                return 0;
            }
        }
        #endregion



        void FastSqlKayitEklemeTabloDegistir()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SonIndexSayisiFnc();
            int SonIndexDegerArttir = Convert.ToInt16(sonindexDegisken);
            SonIndexDegerArttir++;
            string kayit = "insert into Kullanici(id,eposta,pw,arkadasSayisi) values (@id,@eposta,@pw,@arkadasSayisi)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@id", SonIndexDegerArttir);
            komutpaylasim.Parameters.AddWithValue("@eposta", listBox4.Items[caunt].ToString());
            komutpaylasim.Parameters.AddWithValue("@pw", pw);
            komutpaylasim.Parameters.AddWithValue("@arkadasSayisi", ArkadaslikBireyselDegisken);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
            SonIndexSayisiFnc();
        }

        void FastSqlKayitSilme()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            string silme = "Delete From fastKullanici Where eposta = @eposta";
            SqlCommand silmekomut = new SqlCommand(silme, baglanti);
            silmekomut.Parameters.AddWithValue("@eposta", listBox4.Items[caunt].ToString());
            silmekomut.ExecuteNonQuery();
            // SqlKayitEkle();
            baglanti.Close();
        }
        private void fastArkadasKabul_Tick(object sender, EventArgs e)
        {

            ModemResetDurumuKontrolFnc();//kaçıncıda oldugunu yaz
            CauntDegerLB.Text = caunt.ToString();
            textBox4.Text = "95";
            //değer saydir
            if (listBox4.Items.Count == 0)
            {
                fastArkadasKabul.Stop();
                MessageBox.Show("Fast arkadas kabul için e posta yok");
            }
            if (listBox4.Items.Count > caunt)
            {
                if (fastArakdasKabulDegerDegisken < Convert.ToInt32(textBox4.Text))
                {
                    fastArakdasKabulDegerDegisken++;
                    fastArkadasKabulLB.Text = fastArakdasKabulDegerDegisken.ToString();
                }
                else
                {
                    fastArakdasKabulDegerDegisken = 1;
                    fastArkadasKabulLB.Text = fastArakdasKabulDegerDegisken.ToString();
                }
            }
            else
            {
                fastArakdasKabulDegerDegisken = 0;
                // ArkKabul.Stop();
                fastArkadasKabulLB.Text = fastArakdasKabulDegerDegisken.ToString();
                caunt = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
            }


            //değer oku ve işlem yap
            fastArkadasKabulLB.Text = fastArakdasKabulDegerDegisken.ToString();
            if (fastArakdasKabulDegerDegisken == 5)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (fastArakdasKabulDegerDegisken == 12)
            {
                FastGirisyap();
            }
            if (fastArakdasKabulDegerDegisken == 17)
            {
                webBrowser1.Navigate("https://www.facebook.com/friends/requests/?fcref=jwl");
            }
            if (fastArakdasKabulDegerDegisken == 25)
            {
                ArkadasKabulET();
            }
            //if (fastArakdasKabulDegerDegisken == 60)
            //{
            //    webBrowser1.Navigate("https://www.facebook.com/friends/requests/?fcref=jwl");
            //}
            //if (fastArakdasKabulDegerDegisken == 70)
            //{
            //    ArkadasKabulET();
            //}
            if (fastArakdasKabulDegerDegisken == 50)
            {
                webBrowser1.Navigate("https://www.facebook.com/friends/requests/?fcref=jwl");
            }
            if (fastArakdasKabulDegerDegisken == 55)
            {
                ArkadasKabulET();
            }
            if (fastArakdasKabulDegerDegisken == 77)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=");
            }
            if (fastArakdasKabulDegerDegisken == 85)
            {
                html = webBrowser1.Document.Body.InnerHtml.ToString();
                WebUrlTXT.Text = webBrowser1.Url.ToString();
                ArkadasSayisiCekmeFnc();
            }
            if (fastArakdasKabulDegerDegisken == 90)
            {
                cikis();
            }
            if (fastArakdasKabulDegerDegisken == 95)
            {
                {
                    fastArakdasKabulDegerDegisken = 0;
                    try
                    {
                        CauntDegerLB.Text = caunt.ToString();
                        fastArakdasKabulDegerDegisken = 0;
                        if (ArkadaslikGenelDegisken.Length > 3)
                        {
                            ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToUInt16(ArkadaslikGenelDegisken.Remove(1, 1));
                            ArkadaslikBireyselDegisken = Convert.ToUInt16(ArkadaslikGenelDegisken.Remove(1, 1));
                            textBox2.Text = ArkadaslikBireyselDegisken.ToString();
                        }
                        else
                        {
                            ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToUInt16(ArkadaslikGenelDegisken);
                            ArkadaslikBireyselDegisken = Convert.ToUInt16(ArkadaslikGenelDegisken);
                            textBox2.Text = ArkadaslikBireyselDegisken.ToString();
                        }

                        ArkadasSayisiTxt.Text = ArkadasSayisiTut.ToString();
                        //ArkadasSaayisiEklemeBireysel();
                        fastArkadasSayisiEklemeBirekselFnc();
                    }
                    catch (Exception alınanhata)
                    {
                        arkadassayisideger = 0;
                        caunt = 0;
                        ArkadasSayisiTut = 0;
                        ArkadaslikGenelDegisken = "0";
                        hataLB.Text = caunt + alınanhata.ToString();
                    }

                    webBrowser1.Navigate("");
                    fastArakdasKabulDegerDegisken = 0;
                    int arkkkasssass = Convert.ToUInt16(textBox2.Text);
                    if (arkkkasssass > 300)
                    {
                        SqlKayitCekDüzenli();
                        listBox6.Items.Add(listBox4.Items[caunt].ToString());
                        listBox7.Items.Add("1");
                        IdDuzenle();
                        SqlKayitSilDuzenleme();
                        SqlKayitEkleDuzenleme();
                        FastSqlKayitSilme();
                        listBox4.Items.Clear();
                        SqlFastKullaniciKayitCek();
                    }
                    caunt++;
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBox4.Text = "160";
            cikis();
            fastArkadasKabul.Start();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            listBox4.Items.Add(FastArkadasEkleTxt.Text);
            FastArkKabulVtEkleFnc();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Eposta epost = new Eposta();
            epost.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            FaceAccs faceAc = new FaceAccs();
            faceAc.Show();
        }
        private void button24_Click(object sender, EventArgs e)
        {
            ArkadasSayisiTut = 0;
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Kullanici", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                string arkadassayisieziqdegisken = (reader["arkadasSayisi"].ToString());
                ArkadasSayisiTut = ArkadasSayisiTut + Convert.ToInt16(arkadassayisieziqdegisken);
                //MessageBox.Show(ArkadasSayisiTut.ToString());
                MevcutArkadasSayisiTxt.Text = ArkadasSayisiTut.ToString();
            }
            baglanti.Close();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            SqlKayitCekDüzenleFotoPaylasim();
        }
        string DosyaYoluVtGelen = "";
        void SqlKayitCekDüzenleFotoPaylasim()
        {
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            string[] klasorler = Directory.GetDirectories("C:\\Fake Face Resimler");
            for (int j = 0; j < klasorler.Length; j++)
            {
                listBox5.Items.Add(klasorler[j]);
            }

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from ResimPaylasim Where eposta = @eposta", baglanti);
            komut.Parameters.AddWithValue("@eposta", listBox1.Items[caunt].ToString());
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                DosyaYoluVtGelen = (reader["ResimUrl"].ToString().TrimEnd());
                PaylasanTxt.Text = (reader["ResimYuklenen"].ToString());
            }
            //  VtdengelenLink = listBox5.Items[0].ToString();
            baglanti.Close();
            for (int i = 0; i < listBox5.Items.Count; i++)
            {
                if (DosyaYoluVtGelen == listBox5.Items[i].ToString())
                {
                    iiii = i;
                    //   MessageBox.Show(listBox5.Items[i].ToString());
                    listBox6.Items.Clear();

                    string[] dosyalar = System.IO.Directory.GetFiles(listBox5.Items[iiii].ToString());
                    for (int j = 0; j < dosyalar.Length; j++)
                    {
                        listBox6.Items.Add(dosyalar[j]);
                    }
                }
            }
            for (int i = 0; i < listBox6.Items.Count; i++)
            {
                string PaylasmismiKontrolDegisken = PaylasanTxt.Text.IndexOf(listBox6.Items[i].ToString()).ToString();
                int PaylasimKontrolDegiskenInt = Convert.ToInt16(PaylasmismiKontrolDegisken);
                if (-1 == PaylasimKontrolDegiskenInt)
                {
                    resimUrl = (listBox6.Items[i].ToString());
                    i = listBox6.Items.Count;
                }
            }
            if (resimUrl == "")
            {
                paylasimbeklet = 78;
            }
        }
        void SayfaFotoPaylasimGenelFnc()
        {
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            string[] klasorler = Directory.GetDirectories("C:\\Sayfa");
            for (int j = 0; j < klasorler.Length; j++)
            {
                listBox5.Items.Add(klasorler[j]);
            }

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from SayfaFotoPaylasim Where eposta = @eposta", baglanti);
            komut.Parameters.AddWithValue("@eposta", "badboy0625");
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                DosyaYoluVtGelen = (reader["ResimUrl"].ToString().TrimEnd());
                PaylasanTxt.Text = (reader["ResimYuklenen"].ToString());
            }
            //  VtdengelenLink = listBox5.Items[0].ToString();
            baglanti.Close();
            for (int i = 0; i < listBox5.Items.Count; i++)
            {
                if (DosyaYoluVtGelen == listBox5.Items[i].ToString())
                {
                    iiii = i;
                    //   MessageBox.Show(listBox5.Items[i].ToString());
                    listBox6.Items.Clear();

                    string[] dosyalar = System.IO.Directory.GetFiles(listBox5.Items[iiii].ToString());
                    for (int j = 0; j < dosyalar.Length; j++)
                    {
                        listBox6.Items.Add(dosyalar[j]);
                    }
                }
            }
            for (int i = 0; i < listBox6.Items.Count; i++)
            {
                string PaylasmismiKontrolDegisken = PaylasanTxt.Text.IndexOf(listBox6.Items[i].ToString()).ToString();
                int PaylasimKontrolDegiskenInt = Convert.ToInt16(PaylasmismiKontrolDegisken);
                if (-1 == PaylasimKontrolDegiskenInt)
                {
                    resimUrl = (listBox6.Items[i].ToString());
                    i = listBox6.Items.Count;
                }
            }


        }
        void SqlKayitCekDüzenliBlackList()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from BlackList", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox5.Items.Add(reader["id"].ToString());
                listBox6.Items.Add(reader["eposta"].ToString());
            }
            baglanti.Close();
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
                listBox5.Items.Add(reader["paylasimNvarchar"].ToString());
                listBox6.Items.Add(reader["paylasilanEposta"].ToString());
            }
            baglanti.Close();
        }
        void SqlKayitCekDüzenli()
        {
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            listBox7.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Kullanici", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox5.Items.Add(reader["id"].ToString());
                listBox6.Items.Add(reader["eposta"].ToString());
                listBox7.Items.Add(reader["arkadasSayisi"].ToString());
            }
            baglanti.Close();
        }
        void IdDuzenle()
        {
            listBox5.Items.Clear();
            for (int i = 0; i < listBox6.Items.Count; i++)
            {
                listBox5.Items.Add(i.ToString());
            }
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
        }
        void SqlKayitSilDuzenlemeBlackList()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komutSilme = new SqlCommand("Delete from BlackList", baglanti);
            SqlDataReader readerSilme = komutSilme.ExecuteReader();
            baglanti.Close();
        }
        void SqlKayitSilDuzenleme()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komutSilme = new SqlCommand("Delete from Kullanici", baglanti);
            SqlDataReader readerSilme = komutSilme.ExecuteReader();
            baglanti.Close();
        }
        void SqlKayitEkleDuzenlemeBlackList()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            for (int i = 0; i < listBox6.Items.Count; i++)
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                string kayit = "insert into BlackList(id,eposta) values (@id,@eposta)";
                SqlCommand komutEkle = new SqlCommand(kayit, baglanti);
                komutEkle.Parameters.AddWithValue("@id", Convert.ToInt16(listBox5.Items[i]));
                komutEkle.Parameters.AddWithValue("@eposta", listBox6.Items[i].ToString());
                komutEkle.ExecuteNonQuery();
                baglanti.Close();
            }
        }
        void SqlKayitEkleDuzenleme()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            for (int i = 0; i < listBox6.Items.Count; i++)
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                string kayit = "insert into Kullanici(id,eposta,pw,arkadasSayisi) values (@id,@eposta,@pw,@arkadasSayisi)";
                SqlCommand komutEkle = new SqlCommand(kayit, baglanti);
                komutEkle.Parameters.AddWithValue("@id", Convert.ToInt16(listBox5.Items[i]));
                komutEkle.Parameters.AddWithValue("@eposta", listBox6.Items[i].ToString());
                komutEkle.Parameters.AddWithValue("@pw", "1236asd");
                komutEkle.Parameters.AddWithValue("@arkadasSayisi", Convert.ToInt16(listBox7.Items[i]));
                komutEkle.ExecuteNonQuery();
                baglanti.Close();
            }
        }
        int ResimKlasorDeger = 0;
        int resimUrlDegerTutDegisken = 0;
        string resimUrl = "";
        string VtdengelenLink = "";
        private void button26_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox6.Items.Count; i++)
            {
                string PaylasmismiKontrolDegisken = PaylasanTxt.Text.IndexOf(listBox6.Items[i].ToString()).ToString();
                int PaylasimKontrolDegiskenInt = Convert.ToInt16(PaylasmismiKontrolDegisken);
                if (-1 == PaylasimKontrolDegiskenInt)
                {
                    resimUrl = (listBox6.Items[i].ToString());
                    i = listBox6.Items.Count;

                }
            }
        }
        void PaylasimKontrolFnc()
        {
            for (int i = 0; i < listBox5.Items.Count; i++)
            {
                PaylasanTxt.Clear();
                PaylasanTxt.Text = listBox6.Items[i].ToString();
                string PaylasmismiKontrolDegisken = PaylasanTxt.Text.IndexOf(listBox1.Items[caunt].ToString()).ToString();
                int PaylasimKontrolDegiskenInt = Convert.ToInt16(PaylasmismiKontrolDegisken);
                // MessageBox.Show(PaylasimKontrolDegiskenInt.ToString());
                if (-1 == PaylasimKontrolDegiskenInt)
                {
                    facelink.Text = listBox5.Items[i].ToString();
                    PaylasanTxt.Text = PaylasanTxt.Text + " " + listBox1.Items[caunt].ToString();
                    if (baglanti.State == ConnectionState.Closed)
                        baglanti.Open();
                    SqlCommand komut2 = new SqlCommand("UPDATE Paylasim SET paylasilanEposta = @epostalar WHERE id = @id", baglanti);
                    komut2.Parameters.AddWithValue("@id", i);
                    komut2.Parameters.AddWithValue("@epostalar", PaylasanTxt.Text);
                    komut2.ExecuteNonQuery();
                    baglanti.Close();
                    i = listBox5.Items.Count;
                }
            }
        }
        int iiii = 0;
        private void button27_Click(object sender, EventArgs e)
        {


        }

        private void button28_Click(object sender, EventArgs e)
        {

        }

        private void YorumSilBtn_Click(object sender, EventArgs e)
        {
            textBox4.Text = "300";
            cikis();
            YorumSilTmr.Start();
        }

        private void YorumSilTmr_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            //kaçıncıda oldugunu yaz
            CauntDegerLB.Text = caunt.ToString();

            //değer saydir
            if (listBox1.Items.Count > caunt)
            {
                if (paylasimbeklet < Convert.ToInt32(textBox4.Text))
                {
                    paylasimbeklet++;
                    YorumSilLb.Text = paylasimbeklet.ToString();
                }
                else
                {
                    paylasimbeklet = 1;
                    YorumSilLb.Text = paylasimbeklet.ToString();
                }
            }
            else
            {
                YorumSilTmr.Stop();
                YorumSilLb.Text = paylasimbeklet.ToString();
                caunt = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
                if (1 == Convert.ToInt16(listBox3.Items[19]))
                {
                    TxtIcınVeriGunlukIslemDuzenle();
                    ReturnDegisken = ReturnDegisken2 - 10;
                }
            }
            //değer oku ve işlem yap

            YorumSilLb.Text = paylasimbeklet.ToString();
            if (paylasimbeklet == 5)
            {
                textBox4.Text = "180";
                webBrowser1.Navigate("http://facebook.com/");
            }
            else if (paylasimbeklet == 15)
            {
                girisyap();
            }
            else if (paylasimbeklet == 25)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (paylasimbeklet == 40)
            {
                webBrowser1.Navigate("javascript:var s = function() { window.scrollBy(0,500); setTimeout(s, 100); }; s();");
            }
            if (paylasimbeklet == 80)
            {
                BunuKaldirFnc1();
            }
            if (paylasimbeklet == 85)
            {
                if (yorumSayısı < 2)
                {
                    paylasimbeklet = 174;
                    yorumSayısı = 0;
                }
            }
            if (paylasimbeklet == 100)
            {
                BunuKaldirFnc2();
                yorumSayısı = 0;
            }
            if (paylasimbeklet == 110)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (paylasimbeklet == 120)
            {
                webBrowser1.Navigate("javascript:var s = function() { window.scrollBy(0,500); setTimeout(s, 100); }; s();");
            }
            if (paylasimbeklet == 150)
            {
                BunuKaldirFnc1();
            }
            if (paylasimbeklet == 155)
            {
                if (yorumSayısı < 2)
                {
                    paylasimbeklet = 174;
                    yorumSayısı = 0;
                }
            }
            if (paylasimbeklet == 160)
            {
                BunuKaldirFnc2();
            }
            if (paylasimbeklet == 170)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
                yorumSayısı = 0;
            }
            if (paylasimbeklet == 175)
            {
                cikis();
            }
            if (paylasimbeklet == 180)
            {
                webBrowser1.Navigate("www.facebook.com.tr");
                paylasimbeklet = 0;
                caunt++;
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
            yorumSayısı = 0;

        }
        private void button29_Click(object sender, EventArgs e)
        {
            textBox4.Text = "300";
            cikis();
            FotoPaylasTmr.Start();
        }
        void FotografYukleFnc1()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Fotoğraf / Video")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        private void FotoPaylasTmr_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            //kaçıncıda oldugunu yaz
            CauntDegerLB.Text = caunt.ToString();
            //değer saydir
            if (listBox1.Items.Count > caunt)
            {
                if (paylasimbeklet < Convert.ToInt32(textBox4.Text))
                {
                    paylasimbeklet++;
                    FotoPaylasDegerLb.Text = paylasimbeklet.ToString();
                }
                else
                {
                    paylasimbeklet = 1;
                    FotoPaylasDegerLb.Text = paylasimbeklet.ToString();
                }
            }
            else
            {
                FotoPaylasTmr.Stop();
                FotoPaylasDegerLb.Text = paylasimbeklet.ToString();
                caunt = 0;
                listBox1.SelectedIndex = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
                //if (1 == Convert.ToInt16(listBox3.Items[19]))
                //{
                //    TxtIcınVeriGunlukIslemDuzenle();
                //    ReturnDegisken = ReturnDegisken2 - 10;
                //}
                Environment.Exit(0);
            }

            //değer oku ve işlem yap

            FotoPaylasDegerLb.Text = paylasimbeklet.ToString();
            if (paylasimbeklet == 3)
            {
                for (int i = 0; i < 999999; i++)
                {

                    if (i == listBox4.Items.Count)
                    {
                        i = 0;
                        caunt++;
                    }
                    if (listBox1.Items[caunt].ToString() == listBox4.Items[i].ToString())
                    {
                        i = 999999;
                        //  MessageBox.Show(caunt.ToString());
                    }
                }
            }
            if (paylasimbeklet == 5)
            {
                resimUrl = "";
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (paylasimbeklet == 15)
            {
                girisyap();
                SqlKayitCekDüzenleFotoPaylasim();
            }
            if (paylasimbeklet == 30)
            {
                PaylasanTxt.Text = PaylasanTxt.Text + " " + resimUrl;
                FotoPaylasFnc2();
            }
            if (paylasimbeklet == 35)
            {
                SendKeys.Send(resimUrl);
                SendKeys.SendWait("^{ENTER}");
            }
            if (paylasimbeklet == 70)
            {
                xx = 0;
                foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("button"))
                    if (item.GetAttribute("aria-haspopup") == "true")
                    {
                        if (item.InnerText == "Paylaş")
                        {
                            item.InvokeMember("click");
                        }

                    }
            }
            if (paylasimbeklet == 80)
            {
                SendKeys.SendWait("^{ESC}");
                SendKeys.SendWait("^{ESC}");
                SendKeys.SendWait("^{ESC}");
                SendKeys.SendWait("^{ESC}");
                cikis();
            }
            if (paylasimbeklet == 85)
            {
                FotoPaylasildiOlarakBelirileFnc();
                webBrowser1.Navigate("");
                paylasimbeklet = 0;
                listBox1.SelectedIndex++;
                caunt++;
                //TxtIcınVeriGunlukIslemDuzenle();
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            FotoAlFnc2();


        }
        string AnaMetin = "https://";
        int fotopylasKontrolDeger = 0;
        void FotoPaylasFnc1()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("a"))
                {
                    if (div.InnerText == "Fotoğraflar")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
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
                        div.InvokeMember("click");
                        xx++;
                    }
                    if (xx == 1)
                    {
                        break;
                    }
                }
                if (xx == 1)
                {
                    break;
                    xx = 0;
                }
            }
        }
        void FotoPaylasildiOlarakBelirileFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komut2 = new SqlCommand("UPDATE ResimPaylasim SET ResimYuklenen = @ResimYuklenen WHERE Eposta = @Eposta", baglanti);
            komut2.Parameters.AddWithValue("@Eposta", listBox1.Items[caunt].ToString());
            komut2.Parameters.AddWithValue("@ResimYuklenen", PaylasanTxt.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
        }
        void SayfaFotoPaylasildiOlarakBelirileFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komut2 = new SqlCommand("UPDATE SayfaFotoPaylasim SET ResimYuklenen = @ResimYuklenen WHERE Eposta = @Eposta", baglanti);
            komut2.Parameters.AddWithValue("@Eposta", "badboy0625");
            komut2.Parameters.AddWithValue("@ResimYuklenen", PaylasanTxt.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
        }
        int x = 0;
        int xs = 0;
        private void button31_Click(object sender, EventArgs e)
        {
            DosyaAdıTut = "sst";
            try
            {
                if (Directory.Exists(@"E:\" + DosyaAdıTut))
                {//dosya adı varsa
                    dosyaAdeti++;
                    DosyaAdıTut = DosyaAdıTut + " " + dosyaAdeti.ToString();
                    paylasimbeklet = 63;
                }
                else
                { // Dosya Adı Yoksa
                    Directory.CreateDirectory(@"E:\" + DosyaAdıTut);
                }
            }
            catch (Exception)
            {
            }
        }
        private void SayfaFotoPaylasTmr_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            //kaçıncıda oldugunu yaz
            CauntDegerLB.Text = caunt.ToString();
            //değer saydir
            if (listBox1.Items.Count > caunt)
            {
                if (paylasimbeklet < Convert.ToInt32(textBox4.Text))
                {
                    paylasimbeklet++;
                    SayfaFotoPaylasLB.Text = paylasimbeklet.ToString();
                }
                else
                {
                    paylasimbeklet = 1;
                    SayfaFotoPaylasLB.Text = paylasimbeklet.ToString();
                }
            }
            else
            {
                FotoPaylasTmr.Stop();
                SayfaFotoPaylasLB.Text = paylasimbeklet.ToString();
                caunt = 0;
                listBox1.SelectedIndex = 0;
                dondur2++;
            }

            SayfaFotoPaylasLB.Text = paylasimbeklet.ToString();
            if (paylasimbeklet == 3)
            {
                textBox4.Text = "3600";
            }
            if (paylasimbeklet == 5)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (paylasimbeklet == 15)
            {
                try
                {
                    webBrowser1.Document.GetElementById("email").InnerText = "badboy0625";
                    webBrowser1.Document.GetElementById("pass").InnerText = "13864asd";
                    webBrowser1.Document.Forms[0].InvokeMember("submit");
                }
                catch (Exception)
                {
                }
                SayfaFotoPaylasimGenelFnc();
            }
            if (paylasimbeklet == 25)
            {
                webBrowser1.Navigate(SayfaDavetTxt.Text);
            }
            if (paylasimbeklet == 40)
            {
                PaylasanTxt.Text = PaylasanTxt.Text + " " + resimUrl;
                SayfadaFotoPaylasFnc1();
            }
            if (paylasimbeklet == 50)
            {
                SayfadaFotoPaylasFnc2();
            }
            if (paylasimbeklet == 60)
            {
                SayfadaFotoPaylasFnc3();
            }
            if (paylasimbeklet == 65)
            {
                SendKeys.Send(resimUrl.ToLower().TrimEnd());
                SendKeys.SendWait("^{ENTER}");
            }
            if (paylasimbeklet == 100)
            {
                xx = 0;
                foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("button"))
                    if (item.GetAttribute("aria-haspopup") == "true")
                    {
                        if (item.InnerText == "Paylaş")
                        {
                            item.InvokeMember("click");
                        }

                    }
            }
            if (paylasimbeklet == 110)
            {
                SayfaFotoPaylasildiOlarakBelirileFnc();
            }
            if (paylasimbeklet == 3560)
            {
                cikis();
            }
            if (paylasimbeklet == 3595)
            {
                System.Diagnostics.Process.Start(@"C:\facebook paylasim.exe");
                Environment.Exit(0);
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            ProfilResimTikla3();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            FotoCekfnc2();
        }
        private void button35_Click(object sender, EventArgs e)
        {

        }
        private void button36_Click(object sender, EventArgs e)
        {

        }

        void SayfadaFotoPaylasFnc1()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("span"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Fotoğraf / Video")
                    {
                        item.InvokeMember("click");
                    }
                }
            }
        }
        void SayfadaFotoPaylasFnc2()
        {
            int x = 0;
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("input"))
                {
                    if (div.GetAttribute("type") == "file")
                    {
                        x++;
                        if (x == 1)
                        {
                            div.InvokeMember("click");
                        }

                    }
                }
            }
        }

        void BegenButonunaBas()
        {
            int begeniSayisi = 0;
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("a"))
                {
                    if (div.GetAttribute("data-testid") == "fb-ufi-likelink" && div.GetAttribute("role") == "button")
                    {
                        begeniSayisi++;
                        if (begeniSayisi < 8)
                        {
                            div.InvokeMember("click");
                        }
                    }
                }
            }
        }
        void SayfadaFotoPaylasFnc3()
        {
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

        private void button33_Click(object sender, EventArgs e)
        {
            SayfaFotoPaylasTmr.Start();
        }

        private void BegenTmr_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            CauntDegerLB.Text = caunt.ToString();
            if (listBox1.Items.Count > caunt)
            {
                if (paylasimbeklet < Convert.ToInt32(textBox4.Text))
                {
                    paylasimbeklet++;
                    BegeniLB.Text = paylasimbeklet.ToString();
                }
                else
                {
                    paylasimbeklet = 1;
                    BegeniLB.Text = paylasimbeklet.ToString();
                }
            }
            else
            {
                BegenTmr.Stop();
                BegeniLB.Text = paylasimbeklet.ToString();
                caunt = 0;
                listBox1.SelectedIndex = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
                if (1 == Convert.ToInt16(listBox3.Items[19]))
                {
                    TxtIcınVeriGunlukIslemDuzenle();
                    ReturnDegisken = ReturnDegisken2 - 10;
                }
                Environment.Exit(0);
            }
            BegeniLB.Text = paylasimbeklet.ToString();
            if (paylasimbeklet == 5)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (paylasimbeklet == 12)
            {
                girisyap();
            }
            if (paylasimbeklet == 20)
            {
                webBrowser1.Navigate("javascript:var s = function() { window.scrollBy(0,100); setTimeout(s, 2000); }; s();");
            }
            if (paylasimbeklet == 27)
            {
                BegenButonunaBas();
            }
            if (paylasimbeklet == 55)
            {
                cikis();
            }
            if (paylasimbeklet == 60)
            {
                webBrowser1.Navigate("");
                paylasimbeklet = 0;
                listBox1.SelectedIndex++;
                caunt++;
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            BegenTmr.Start();
        }

        private void SonPaylasimSilTmr_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            CauntDegerLB.Text = caunt.ToString();
            if (listBox1.Items.Count > caunt)
            {
                if (paylasimbeklet < Convert.ToInt32(textBox4.Text))
                {
                    paylasimbeklet++;
                    SonPaylasimSilLB.Text = paylasimbeklet.ToString();
                }
                else
                {
                    paylasimbeklet = 1;
                    SonPaylasimSilLB.Text = paylasimbeklet.ToString();
                }
            }
            else
            {
                SonPaylasimSilTmr.Stop();
                SonPaylasimSilLB.Text = paylasimbeklet.ToString();
                caunt = 0;
                listBox1.SelectedIndex = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
                if (1 == Convert.ToInt16(listBox3.Items[19]))
                {
                    TxtIcınVeriGunlukIslemDuzenle();
                    ReturnDegisken = ReturnDegisken2 - 10;
                }
                Environment.Exit(0);
            }
            SonPaylasimSilLB.Text = paylasimbeklet.ToString();
            if (paylasimbeklet == 5)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (paylasimbeklet == 12)
            {
                girisyap();
            }
            if (paylasimbeklet == 20)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (paylasimbeklet == 30)
            {
                ProfilSonPaylasimClickFnc1();
            }
            if (paylasimbeklet == 35)
            {
                ProfilSonPaylasimClickFnc2();
            }
            if (paylasimbeklet == 45)
            {
                ProfilSonPaylasimClickFnc3();
            }
            if (paylasimbeklet == 55)
            {
                ProfilSonPaylasimClickFnc4();
            }
            if (paylasimbeklet == 65)
            {
                cikis();
            }
            if (paylasimbeklet == 70)
            {
                webBrowser1.Navigate("");
                paylasimbeklet = 0;
                listBox1.SelectedIndex++;
                caunt++;
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            SonPaylasimSilTmr.Start();
        }
        string SonPaylasimBegeniSayisi = "";
        void ProfilSonPaylasimClickFnc1()
        {
            string ZamanCekGenelDegisken = "";
            string html = "";
            html = webBrowser1.Document.Body.InnerHtml.ToString();
            string start_index_text = "<span class=\"fsm fwn fcg\">";
            string yeni_html = html.Substring(html.IndexOf(start_index_text) + start_index_text.Length, html.Length - (html.IndexOf(start_index_text) + start_index_text.Length));
            ZamanCekGenelDegisken = yeni_html.Substring(0, yeni_html.IndexOf("</span>"));
            textBox1.Text = ZamanCekGenelDegisken;
            listBox5.Items.Clear();
            string[] parcalar;
            parcalar = textBox1.Text.Split('>');
            foreach (string i in parcalar)
            {
                listBox5.Items.Add(i);
            }
            SonPaylasimBegeniSayisi = listBox5.Items[3].ToString();
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
        void ProfilSonPaylasimClickFnc2()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("a"))
                {
                    if (div.GetAttribute("rel") == "toggle" && div.GetAttribute("role") == "button" && div.GetAttribute("aria-expanded") == "false" && div.GetAttribute("aria-haspopup") == "true")
                    {
                        x++;
                        if (x == 3)
                        {
                            div.InvokeMember("click");
                        }
                    }
                    if (x == 3)
                    {
                        break;
                    }
                }
                if (x == 3)
                {
                    break;
                }
            }
            x = 0;
        }
        void ProfilSonPaylasimClickFnc3()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("span"))
                {
                    if (div.InnerText == "Sil")
                    {
                        div.InvokeMember("click");
                    }
                }
            }
        }
        void ProfilSonPaylasimClickFnc4()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("button"))
            {
                if (item.InnerText == "Gönderiyi Sil")
                {
                    item.InvokeMember("click");
                }
            }
        }

        private void SayfadakiPaylasimTmr_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            CauntDegerLB.Text = caunt.ToString();
            if (listBox1.Items.Count > caunt)
            {
                if (paylasimbeklet < Convert.ToInt32(textBox4.Text))
                {
                    paylasimbeklet++;
                    SayfaPaylasimLB.Text = paylasimbeklet.ToString();
                }
                else
                {
                    paylasimbeklet = 1;
                    SayfaPaylasimLB.Text = paylasimbeklet.ToString();
                }
            }
            else
            {
                SayfadakiPaylasimTmr.Stop();
                SayfaPaylasimLB.Text = paylasimbeklet.ToString();
                caunt = 0;
                listBox1.SelectedIndex = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
                if (1 == Convert.ToInt16(listBox3.Items[19]))
                {
                    TxtIcınVeriGunlukIslemDuzenle();
                    ReturnDegisken = ReturnDegisken2 - 10;
                }
                Environment.Exit(0);
            }
            SayfaPaylasimLB.Text = paylasimbeklet.ToString();
            if (paylasimbeklet == 5)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (paylasimbeklet == 12)
            {
                girisyap();
            }
            if (paylasimbeklet == 20)
            {
                webBrowser1.Navigate(SayfaDavetTxt.Text);
            }
            if (paylasimbeklet == 35)
            {
                SayfadakiSonGonderiyiPaylasFnc1();
            }
            if (paylasimbeklet == 45)
            {
                SayfadakiSonGonderiyiPaylasFnc2();
            }
            if (paylasimbeklet == 55)
            {
                cikis();
            }
            if (paylasimbeklet == 60)
            {
                webBrowser1.Navigate("");
                paylasimbeklet = 0;
                listBox1.SelectedIndex++;
                caunt++;
            }
        }

        void SayfadakiSonGonderiyiPaylasFnc1()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
            {
                foreach (HtmlElement div in item.GetElementsByTagName("a"))
                {
                    if (div.InnerText == "Paylaş")
                    {
                        x++;
                        if (x == 1)
                        {
                            div.InvokeMember("click");
                        }
                    }
                }
            }
            x = 0;
        }
        int sayfaPaylasimKotnrol = 0;
        void SayfadakiSonGonderiyiPaylasFnc2()
        {
            int sayfaPaylasimKotnrol = 0;
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                if (item.GetAttribute("title") == "Gönderiyi Hemen Paylaş (Herkese Açık)")
                {
                    sayfaPaylasimKotnrol++;
                    item.InvokeMember("click");
                }
            }
            if (sayfaPaylasimKotnrol == 0)
            {
                foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("span"))
                {
                    if (item.InnerText == "Hemen Paylaş (Herkese Açık)")
                    {
                        item.InvokeMember("click");
                    }
                }
            }
        }


        private void button39_Click(object sender, EventArgs e)
        {
            SayfadakiPaylasimTmr.Start();
        }

        private void button40_Click(object sender, EventArgs e)
        {

        }

        private void button41_Click(object sender, EventArgs e)
        {

        }

        private void button42_Click(object sender, EventArgs e)
        {

        }

        void ProfilSikayetFnc1()
        {
            try
            {
                int x = 0;
                foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("div"))
                {
                    foreach (HtmlElement div in item.GetElementsByTagName("button"))
                    {
                        if (div.GetAttribute("id") == "u_0_11" && div.GetAttribute("id") == "u_0_z")
                        {
                            x++;
                            if (x == 1)
                            {
                                div.InvokeMember("click");
                            }
                        }
                    }
                }
                //   webBrowser1.Document.GetElementById("u_0_z").InvokeMember("click");
                webBrowser1.Document.GetElementById("u_0_12").InvokeMember("click");
            }
            catch (Exception)
            {


            }

        }
        void ProfilSikayetFnc2()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("span"))
            {
                if (item.InnerText == "Şikayet Et")
                {
                    item.InvokeMember("click");
                }
            }
        }
        void ProfilSikayetFnc3()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("label"))
            {
                if (item.InnerText == "Bu profili şikayet et")
                {
                    item.InvokeMember("click");
                }
            }
        }
        void ProfilSikayetFnc4()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("button"))
            {
                if (item.InnerText == "Devam")
                {
                    item.InvokeMember("click");
                }
            }
        }
        void ProfilSikayetFnc5()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("label"))
            {
                if (item.InnerText == "Bu sahte bir hesap")
                {
                    item.InvokeMember("click");
                }
            }
        }
        void ProfilSikayetFnc6()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("label"))
            {
                if (item.InnerText == "Diğer...")
                {
                    item.InvokeMember("click");
                }
            }
        }
        void ProfilSikayetFnc7()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                if (item.InnerText == "İncelenmesi için Facebook'a gönder")
                {
                    item.InvokeMember("click");
                }
            }
        }

        private void SikayetTmr_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            //kaçıncıda oldugunu yaz
            CauntDegerLB.Text = caunt.ToString();
            textBox4.Text = "100";
            //değer saydir
            if (listBox1.Items.Count > caunt)
            {
                if (arkadaskabuldeger < Convert.ToInt32(textBox4.Text))
                {
                    arkadaskabuldeger++;
                    SikayetLB.Text = arkadaskabuldeger.ToString();
                }
                else
                {
                    arkadaskabuldeger = 1;
                    SikayetLB.Text = arkadaskabuldeger.ToString();
                }
            }
            else
            {
                arkadaskabuldeger = 0;
                SikayetTmr.Stop();
                SikayetLB.Text = arkadaskabuldeger.ToString();
                caunt = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
            }


            //değer oku ve işlem yap
            SikayetLB.Text = arkadaskabuldeger.ToString();
            if (arkadaskabuldeger == 5)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (arkadaskabuldeger == 12)
            {
                girisyap();
            }
            if (arkadaskabuldeger == 15)
            {
                webBrowser1.Navigate(SikayetTxt.Text);
            }
            if (arkadaskabuldeger == 25)
            {
                ProfilSikayetFnc1();
            }
            if (arkadaskabuldeger == 30)
            {
                ProfilSikayetFnc2();
            }
            if (arkadaskabuldeger == 35)
            {
                ProfilSikayetFnc3();
            }
            if (arkadaskabuldeger == 40)
            {
                ProfilSikayetFnc4();
            }
            if (arkadaskabuldeger == 45)
            {
                ProfilSikayetFnc5();
            }
            if (arkadaskabuldeger == 50)
            {
                ProfilSikayetFnc4();
            }
            if (arkadaskabuldeger == 55)
            {
                ProfilSikayetFnc6();
            }
            if (arkadaskabuldeger == 60)
            {
                ProfilSikayetFnc4();
            }
            if (arkadaskabuldeger == 65)
            {
                ProfilSikayetFnc7();
            }
            if (arkadaskabuldeger == 70)
            {
                cikis();
            }
            if (arkadaskabuldeger == 75)
            {
                webBrowser1.Navigate("");
                arkadaskabuldeger = 0;
                caunt++;
            }
        }
        private void button43_Click(object sender, EventArgs e)
        {
            SikayetTmr.Start();
        }

        private void FotoCek_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            CauntDegerLB.Text = caunt.ToString();
            if (listBox1.Items.Count > caunt)
            {
                if (paylasimbeklet < Convert.ToInt32(textBox4.Text))
                {
                    paylasimbeklet++;
                    FotoCekLb.Text = paylasimbeklet.ToString();
                }
                else
                {
                    paylasimbeklet = 1;
                    FotoCekLb.Text = paylasimbeklet.ToString();
                }
            }
            else
            {
                FotoCek.Stop();
                FotoCekLb.Text = paylasimbeklet.ToString();
                caunt = 0;
                listBox1.SelectedIndex = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
                if (1 == Convert.ToInt16(listBox3.Items[19]))
                {
                    TxtIcınVeriGunlukIslemDuzenle();
                    ReturnDegisken = ReturnDegisken2 - 10;
                }
                Environment.Exit(0);
            }


            //değer oku ve işlem yap

            FotoCekLb.Text = paylasimbeklet.ToString();
            if (paylasimbeklet == 5)
            {
                webBrowser1.Navigate("http://facebook.com/");
            }
            if (paylasimbeklet == 12)
            {
               // girisyap();
            }
            if (paylasimbeklet == 20)
            {
                webBrowser1.Navigate("http://facebook.com/profile.php?=73322363");
            }
            if (paylasimbeklet == 25)
            {
                ArkadasListesiTıkla();
            }
            if (paylasimbeklet == 33)
            {
                webBrowser1.Navigate("javascript:var s = function() { window.scrollBy(0,500); setTimeout(s, 100); }; s();");
                SqlArkadasListCek();
            }
            if (paylasimbeklet == 75)
            {
                FotoCekfnc1();
            }
            if (paylasimbeklet == 77)
            {
                FotoCekfnc2();
            }
            if (paylasimbeklet == 80)
            {
                ArkadasLinkYaz();
               FotoCekProfil.Start();
                FotoCek.Stop();
                paylasimbeklet = 0;
                string b = "\\";
                string yol = "C:\\Yönet" + b + "Linkler.txt";
                // StreamReader yaz;
                StreamWriter s = new StreamWriter(yol);
                for (int i = 0; i < listBox10.Items.Count; i++)
                {

                    s.WriteLine(listBox10.Items[i].ToString());

                }
                s.Close();
            }

        }
        System.Collections.ArrayList arr1 = new System.Collections.ArrayList(); // listbox1
        System.Collections.ArrayList arr2 = new System.Collections.ArrayList(); // listbox2
        System.Collections.ArrayList arr3 = new System.Collections.ArrayList(); // listbox3
        System.Collections.ArrayList arr4 = new System.Collections.ArrayList(); // listbox4
        System.Collections.ArrayList arr5 = new System.Collections.ArrayList(); // listbox5
        System.Collections.ArrayList arr6 = new System.Collections.ArrayList(); // Vt den gelen profil linkleri ham
        System.Collections.ArrayList arr7 = new System.Collections.ArrayList(); // Vt gönderilcek Verileri tutuyor
        System.Collections.ArrayList arr8 = new System.Collections.ArrayList(); // vtden gelen ve çekilenler arasındaki kontrolu tutan arry

        void FotoCekfnc1()
        {
            string html = webBrowser1.Document.Body.InnerHtml.ToString();
            string[] parcalar;
            parcalar = html.Split('<');
            arr1.Clear();
            foreach (string i in parcalar)
            {
                arr1.Add(i);
                listBox8.Items.Add(i);
            }

            arr2.Clear();
            for (int i = 0; i < arr1.Count; i++)
            {
                string Metin = arr1[i].ToString();
                int sonuc = Metin.IndexOf("fsl fwb fcb");
                int sonu2 = Metin.IndexOf("title");
                if (sonuc > 0)
                {
                    if (sonu2 < 0)
                    {
                        string Metin2 = arr1[i + 1].ToString();
                        if (Metin2.Length > 150)
                        {
                            arr2.Add(Metin2);
                            listBox9.Items.Add(Metin2);
                        }
                    }
                }
            }
        }
        ArrayList arkadasListeFace = new ArrayList();
        string ProfilLinkGenelDegisken = "";
        void FotoCekfnc2()
        {
            for (int i = 0; i < arr2.Count; i++)
            {
                string[] parcalar;
                string metin = arr2[i].ToString().Trim();
                parcalar = metin.Split('"');
                arr3.Clear();
                foreach (string i2 in parcalar)
                {
                    arr3.Add(i2);
                    listBox11.Items.Add(i2);
                }
                arr4.Add(arr3[1].ToString());
                string Metin2 = arr3[arr3.Count - 1].ToString();
                arr5.Add(Metin2.Remove(0, 1));
                ProfilLinkGenelDegisken = ProfilLinkGenelDegisken + arr4[i].ToString() + "+" + arr5[i].ToString() + "-".Trim();
            }
            string[] parcalar2;
            parcalar2 = ProfilLinkGenelDegisken.Split('-');
            foreach (string i2 in parcalar2)
            {
                arkadasListeFace.Add(i2.Trim()); // 
            }


            for (int i4 = 0; i4 <= arkadasListeFace.Count - 1; i4++) // 2 - arkadaslsite 1 - 7
            {
                if (arr7.IndexOf(arkadasListeFace[i4]) == -1)
                {
                    arr8.Add(arkadasListeFace[i4]);
                    arr7.Add(arkadasListeFace[i4]);
                }
            }
            string[] parcalar3;
            string ProfilLinkGenelDegisken2 = "";
            for (int i = 0; i < arr8.Count; i++)
            {
                ProfilLinkGenelDegisken2 = arr8[i].ToString();
                parcalar3 = ProfilLinkGenelDegisken2.Split('+');
                foreach (string i3 in parcalar3)
                {
                    listBox10.Items.Add(i3.Trim()); // 
                }
            }
           




            //string[] parcalar2;
            //parcalar2 = ProfilLinkGenelDegisken.Split('-');
            //foreach (string i2 in parcalar2)
            //{
            //    arkadasListeFace.Add(i2.Trim()); // 
            //}
            //string[] arkadas;
            //for (int i = 0; i < arkadasListeFace.Count; i++)
            //{
            //    if (arr6.IndexOf(arkadasListeFace[i]) > -1)
            //    {
            //        ArkadasListesiTut += arkadasListeFace[i];
            //        arkadas = arkadasListeFace[i].ToString().Split('+');
            //        try
            //        {
            //            listBox10.Items.Add(arkadas[0]);
            //            listBox10.Items.Add(arkadas[1]);
            //        }
            //        catch (IndexOutOfRangeException)
            //        {

            //        }

            //    }
            //}

        }
        void ArkadasListesiTıkla()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                if (item.InnerText == "Arkadaşlar")
                {
                    item.InvokeMember("click");
                }
            }
        }

        void ArkadasLinkYaz()
        {
            //if (baglanti.State == ConnectionState.Closed)
            //    baglanti.Open();
            //SqlCommand komut = new SqlCommand("UPDATE Kullanici SET ArkadasListesi = @ArkadasListesi where eposta=@eposta", baglanti);
            //komut.Parameters.AddWithValue("@eposta", listBox1.Items[caunt].ToString());
            //komut.Parameters.AddWithValue("@ArkadasListesi", ArkadasListesiTut);
            //komut.ExecuteNonQuery();
            //baglanti.Close();

            string b = "\\";
            string yol = "C:\\Yönet" + b + "linkler.txt";
            // StreamReader yaz;


            StreamWriter s = new StreamWriter(yol);
            for (int i = 0; i < arr7.Count; i++)
            {
                s.Write(arr7[i].ToString() + "-");
            }
            s.Close();


        }
        private void button44_Click(object sender, EventArgs e)
        {
            FotoCek.Start();
            ReturnTmr.Stop();
            //listBox1.Items.Clear();
            //if (baglanti.State == ConnectionState.Closed)
            //    baglanti.Open();

            //SqlCommand komut = new SqlCommand("Select * from Kullanici where FotoDurum = 0", baglanti);
            //SqlDataReader reader = komut.ExecuteReader();
            //while (reader.Read())
            //{
            //    listBox1.Items.Add(reader["eposta"].ToString());
            //}
            //baglanti.Close();
        }
        int dosyaAdeti = 0;
        int CauntProfil = 0;
        int indirmeindexName = 1;
        string DosyaAdıTut = "";
        int tur = 0;
        private void FotoCekProfil_Tick(object sender, EventArgs e)
        {
            ModemResetDurumuKontrolFnc();
            CountProfilLB.Text = (CauntProfil/2).ToString();
            if (listBox10.Items.Count > CauntProfil)
            {
                if (paylasimbeklet < Convert.ToInt32(textBox4.Text))
                {
                    paylasimbeklet++;
                    FotoProfil.Text = paylasimbeklet.ToString();
                }
                else
                {
                    paylasimbeklet = 1;
                    FotoProfil.Text = paylasimbeklet.ToString();
                }
            }
            else
            {
                FotoCekProfil.Stop();
                FotoProfil.Text = paylasimbeklet.ToString();
                CauntProfil = 0;
                listBox1.SelectedIndex = 0;
                dondur2++;
                TxtIcınVeriDuzenle();
                if (1 == Convert.ToInt16(listBox3.Items[19]))
                {
                    TxtIcınVeriGunlukIslemDuzenle();
                    ReturnDegisken = ReturnDegisken2 - 10;
                }
                Environment.Exit(0);
            }
            //değer oku ve işlem yap

            FotoProfil.Text = paylasimbeklet.ToString();
            if (paylasimbeklet == 5)
            {
                webBrowser1.Navigate(listBox10.Items[CauntProfil].ToString());
            }
            if (paylasimbeklet == 12)
            {
                ProfilResimTikla();
            }
            if (paylasimbeklet == 18)
            {
                ProfilResimTikla2();
            }
            if (paylasimbeklet == 22)
            {
                ProfilResimTikla3();
            }
            if (paylasimbeklet == 26)
            {
                webBrowser1.Navigate("javascript:var s = function() { window.scrollBy(0,500); setTimeout(s, 100); }; s();");
            }
            if (paylasimbeklet == 55)
            {
                FotoAlFnc1();
            }
            if (paylasimbeklet == 60)
            {
                FotoAlFnc2();
                DosyaAdıTut = listBox10.Items[indirmeindexName].ToString();
            }
            if (paylasimbeklet == 65)
            {
                try
                {
                    if (Directory.Exists(@"E:\" + DosyaAdıTut))
                    {//dosya adı varsa
                        dosyaAdeti++;
                        DosyaAdıTut = DosyaAdıTut + " " + dosyaAdeti.ToString();
                        paylasimbeklet = 63;
                    }
                    else
                    { // Dosya Adı Yoksa
                        Directory.CreateDirectory(@"E:\" + DosyaAdıTut);
                    }
                }
                catch (Exception)
                {
                }

            }
            if (paylasimbeklet == 70)
            {
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    indirilecek = listBox2.Items[i].ToString();
                    //dosyaAdi2 = listBox10.Items[indirmeindexName].ToString();
                    dosyaAdi = listBox10.Items[indirmeindexName].ToString() + i.ToString();
                    indir();
                }
            }
            if (paylasimbeklet == 75)
            {
                CauntProfil = CauntProfil + 2;
                indirmeindexName = indirmeindexName + 2;
                if (CauntProfil > listBox10.Items.Count / 2 && indirmeindexName > listBox10.Items.Count / 2)
                {
                    FotoCekProfil.Stop();
                }
                paylasimbeklet = 1;
                tur++;
                if (tur == 10)
                {
                    Environment.Exit(0);
                }
            }
        }


        public string uzanti = ".jpg";
        public string indirilecek = "";
        public string klasor = "E:\\";
        public string dosyaAdi = "";
        public string dosyaAdi2 = "";
        void indir()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileAsync(new Uri(indirilecek), klasor + DosyaAdıTut + "\\" + dosyaAdi + uzanti);
            }
            catch (Exception)
            {

          
            }

        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //label1.Text = ("Dosya indiriliyor:" + e.ProgressPercentage.ToString());
        }
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            // label2.Text = ("Dosya indirme tamamlandı!");
        }

        //if (Directory.Exists(@"E:\" + DosyaAdıTut))
        //{//dosya adı varsa
        //    dosyaAdeti++;
        //    DosyaAdıTut = DosyaAdıTut + dosyaAdeti.ToString();
        //    paylasimbeklet = 64;
        //}
        //else
        //{ // Dosya Adı Yoksa
        //    Directory.CreateDirectory(@"E:\" + DosyaAdıTut);
        //    //Directory.CreateDirectory("C:" + Klasor);
        //}




        string ResimUrlTut = "";
        void FotoAlFnc1()
        {
            ResimUrlTut = webBrowser1.Document.Body.InnerHtml.ToString();
            listBox6.Items.Clear();
            string[] parcalar;
            parcalar = ResimUrlTut.Split('<');

            foreach (string i in parcalar)
            {
                listBox6.Items.Add(i);
            }
        }
        void FotoAlFnc2()
        {
            listBox5.Items.Clear();
            listBox7.Items.Clear();
            listBox8.Items.Clear();
            listBox9.Items.Clear();
            listBox2.Items.Clear();
            for (int i = 0; i < listBox6.Items.Count; i++)
            {
                string Metin = listBox6.Items[i].ToString();
                int sonuc = Metin.IndexOf("data-starred-src");
                if (sonuc > 0)
                {
                    listBox5.Items.Add(Metin.Remove(0, 450));
                }
            }
            for (int i = 0; i < listBox5.Items.Count; i++)
            {
                listBox7.Items.Clear();
                string[] parcalar;
                string metin2 = listBox5.Items[i].ToString();
                parcalar = metin2.Split('/');

                foreach (string a in parcalar)
                {
                    listBox7.Items.Add(a);
                }
                for (int i3 = 2; i3 < listBox7.Items.Count; i3++)
                {
                    AnaMetin = AnaMetin + listBox7.Items[i3].ToString() + "/";
                }
                listBox8.Items.Add(AnaMetin);
                AnaMetin = "https://";
            }
            for (int i = 0; i < listBox8.Items.Count; i++)
            {
                string x1 = listBox8.Items[i].ToString();
                if (x1.Length > 120)
                {
                    listBox9.Items.Clear();
                    x1 = x1.Substring(0, x1.Length - 3);
                    string[] parcalar;
                    parcalar = x1.Split(';');
                    foreach (string a in parcalar)
                    {
                        listBox9.Items.Add(a);
                    }
                    string x2 = listBox9.Items[0].ToString();
                    x2 = x2.Substring(0, x2.Length - 3);
                    listBox2.Items.Add(x2 + listBox9.Items[1].ToString());

                }
                else
                {
                    listBox2.Items.Add(x1.Substring(0, x1.Length - 3));
                }
            }
        }
        void ProfilResimTikla()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                if (item.InnerText == "Fotoğraflar")
                {
                    item.InvokeMember("click");
                }
            }
        }
        void ProfilResimTikla2()
        {
            listBox5.Items.Clear();
            listBox7.Items.Clear();
            listBox8.Items.Clear();
            listBox9.Items.Clear();
            listBox2.Items.Clear();
            ResimUrlTut = webBrowser1.Document.Body.InnerHtml.ToString();
            listBox6.Items.Clear();
            string[] parcalar;
            parcalar = ResimUrlTut.Split('<');

            foreach (string i in parcalar)
            {
                listBox6.Items.Add(i);
            }
            for (int i = 0; i < listBox6.Items.Count; i++)
            {
                string Metin = listBox6.Items[i].ToString();
                int sonuc = Metin.IndexOf("_3sz");
                if (sonuc > 0)
                {
                    listBox5.Items.Add(Metin.Remove(0, 18));
                }
            }
            for (int i = 0; i < listBox5.Items.Count; i++)
            {
                string[] parcalar2;
                parcalar2 = ResimUrlTut.Split('<');
                foreach (string i2 in parcalar2)
                {
                    listBox6.Items.Add(i2);
                }
            }
        }
        void ProfilResimTikla3()
        {
            string Kotnrolet = listBox5.Items[1].ToString();
            if (Kotnrolet.Length > 9)
            {
                foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
                {
                    if (item.InnerText == listBox5.Items[1].ToString())
                    {
                        item.InvokeMember("click");
                    }
                }
            }
        }
    }
}

