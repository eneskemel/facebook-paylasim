using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace facebook_paylasim
{
    public partial class Arakla : Form
    {
        public Arakla()
        {
            InitializeComponent();
        }

        int caunt = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            string html = webBrowser1.Document.Body.InnerHtml.ToString();
            string[] parcalar;
            parcalar = html.Split('<');

            foreach (string i in parcalar)
            {
                listBox4.Items.Add(i);
            }

            for (int i = 0; i < listBox4.Items.Count; i++)
            {
                string Metin = listBox4.Items[i].ToString();
                int sonuc = Metin.IndexOf("_4arz");
                if (sonuc > 0)
                {
                    string Metin2 = listBox4.Items[i + 1].ToString();
                    int sonuc2 = Metin2.IndexOf(">");
                    arr2.Add(Metin2.Remove(0, sonuc2 + 1));
                }
                for (int i2 = 0; i2 <= arr2.Count - 1; i2++)
                {
                    if (arr1.IndexOf(arr2[i2]) == -1)
                    {
                        listBox5.Items.Add(arr2[i2]);
                        arr1.Add(arr2[i2]);
                    }
                }
            }

            arr1.Clear();
            arr2.Clear();

        }

        private void Arakla_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = false;
            webBrowser1.Navigate("https://www.facebook.com/badboy0625/friends?source_ref=pb_friends_tl");
            listBox3.Items.Clear();
        }

        System.Collections.ArrayList arr1 = new System.Collections.ArrayList();
        System.Collections.ArrayList arr2 = new System.Collections.ArrayList();

        private void button2_Click(object sender, EventArgs e)
        {
            string html = webBrowser1.Document.Body.InnerHtml.ToString();
            string[] parcalar;
            parcalar = html.Split('<');
            listBox1.Items.Clear();
            foreach (string i in parcalar)
            {
                listBox1.Items.Add(i);
            }

            listBox2.Items.Clear();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string Metin = listBox1.Items[i].ToString();
                int sonuc = Metin.IndexOf("_5i_s _8o _8r lfloat _ohe");
                if (sonuc > 0)
                {
                    listBox2.Items.Add(Metin.Remove(0, 75));
                }
            }
            // listBox3.Items.Clear();
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                string Metin2 = listBox2.Items[i].ToString();
                int sonuc2 = Metin2.IndexOf("fref");
                int sonIndex = Metin2.Length - sonuc2;
                string SonMetin = Metin2.Remove(sonuc2, sonIndex).ToString();
                arr2.Add(SonMetin);
            }

            for (int i = 0; i <= arr2.Count - 1; i++)
            {
                if (arr1.IndexOf(arr2[i]) == -1)
                {
                    listBox3.Items.Add(arr2[i]);
                    arr1.Add(arr2[i]);
                }
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            string html = webBrowser1.Document.Body.InnerHtml.ToString();
            string[] parcalar;
            parcalar = html.Split('<');
            listBox1.Items.Clear();
            foreach (string i in parcalar)
            {
                listBox1.Items.Add(i);
            }

            listBox2.Items.Clear();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string Metin = listBox1.Items[i].ToString();
                int sonuc = Metin.IndexOf("_5q6s _8o _8t lfloat _ohe");
                if (sonuc > 0)
                {
                    listBox2.Items.Add(Metin.Remove(0, 75));
                }
            }

            // listBox3.Items.Clear();
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                string Metin2 = listBox2.Items[i].ToString();
                int sonuc2 = Metin2.IndexOf("fref");
                int sonIndex = Metin2.Length - sonuc2;
                //string SonMetin = Metin2.Remove(sonuc2, sonIndex).ToString();
                listBox3.Items.Add(Metin2.Remove(sonuc2, sonIndex).ToString());
            }

            //for (int i = 0; i <= arr2.Count - 1; i++)
            //{
            //    if (arr1.IndexOf(arr2[i]) == -1)
            //    {
            //        listBox3.Items.Add(arr2[i]);
            //        arr1.Add(arr2[i]);
            //    }
            //}


        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = listBox2.SelectedItem.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {



        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        int deger = 0;
        string araDegisken = "";
        int clickDegisken = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = caunt.ToString();
            label2.Text = listBox3.Items.Count.ToString();

            if (listBox5.Items.Count > caunt)
            {
                if (deger < Convert.ToInt32(textBox4.Text))
                {
                    deger++;
                    label1.Text = deger.ToString();
                }
                else
                {
                    deger = 1;
                    label1.Text = deger.ToString();
                }
            }
            else
            {
                deger = 0;
                timer1.Stop();
                label1.Text = deger.ToString();
                caunt = 0;
                TxtVeriYaz();
            }

            if (deger == 5)
            {
                if (listBox5.Items[caunt].ToString() == araDegisken)
                {
                    clickDegisken++;
                }
                if(listBox5.Items[caunt].ToString() != araDegisken)
                {
                    clickDegisken = 1;
                    araDegisken = "";
                }
                int x = 0;
                foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
                {
                    foreach (HtmlElement div in item.GetElementsByTagName("span"))
                    {
                        if (div.InnerText == listBox5.Items[caunt].ToString())
                        {
                            araDegisken = listBox5.Items[caunt].ToString();
                            x++;
                            if (clickDegisken == x)
                            {
                                div.InvokeMember("click");
                            }
                        }
                    }
                }
            }
            if (deger == 10)
            {
                LinkAl();
            }
            if (deger == 15)
            {
                Kapat();
            }
            if (deger == 20)
            {
                deger = 0;
                caunt++;
            }


        }
        void TxtVeriYaz()
        {
            string b = "\\";
            string yol = "C:\\Yönet" + b + "Linkler.txt";
            // StreamReader yaz;


            StreamWriter s = new StreamWriter(yol);
            for (int i = 0; i < listBox3.Items.Count; i++)
            {

                s.WriteLine(listBox3.Items[i].ToString());

            }
            s.Close();
        }
        int linkSayisi = 0;

        void LinkAl()
        {
            string html = webBrowser1.Document.Body.InnerHtml.ToString();
            string[] parcalar;
            parcalar = html.Split('<');
            listBox1.Items.Clear();
            foreach (string i in parcalar)
            {
                listBox1.Items.Add(i);
            }

            listBox2.Items.Clear();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string Metin = listBox1.Items[i].ToString();
                int sonuc = Metin.IndexOf("_5i_s _8o _8r lfloat _ohe");
                if (sonuc > 0)
                {
                    listBox2.Items.Add(Metin.Remove(0, 75));
                }
            }

            // listBox3.Items.Clear();
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                string Metin2 = listBox2.Items[i].ToString();
                int sonuc2 = Metin2.IndexOf("fref");
                int sonIndex = Metin2.Length - sonuc2;
                string SonMetin = Metin2.Remove(sonuc2, sonIndex).ToString();
                arr2.Add(SonMetin);
            }

            for (int i = 0; i <= arr2.Count - 1; i++)
            {
                if (arr1.IndexOf(arr2[i]) == -1)
                {
                    listBox3.Items.Add(arr2[i]);
                    arr1.Add(arr2[i]);
                }
            }
        }
        void Kapat()
        {
            foreach (HtmlElement item in webBrowser1.Document.GetElementsByTagName("a"))
            {
                if (item.GetAttribute("title") == "Kapat")
                {
                    item.InvokeMember("click");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox5.SelectedIndex = 0;
            timer1.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            string html = webBrowser1.Document.Body.InnerHtml.ToString();
            string[] parcalar;
            parcalar = html.Split('<');
            listBox1.Items.Clear();
            foreach (string i in parcalar)
            {
                listBox1.Items.Add(i);
            }

            listBox2.Items.Clear();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string Metin = listBox1.Items[i].ToString();
                int sonuc = Metin.IndexOf("fsl fwb fcb");
                if (sonuc > 0)
                {
                    string Metin2 = listBox1.Items[i + 1].ToString();
                    if (Metin2.Length > 150)
                    {
                        listBox2.Items.Add(Metin2);
                    }
                }
            }
        }

        void ArkadasLinkYaz()
        {
            string b = "\\";
            string yol = "C:\\Link.txt";
            // StreamReader yaz;


            StreamWriter s = new StreamWriter(yol);
            for (int i = 0; i < listBox4.Items.Count; i++)
            {

                s.WriteLine(listBox4.Items[i].ToString() + " " + listBox5.Items[i].ToString());

            }
            s.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                string[] parcalar;
                string metin = listBox2.Items[i].ToString();
                parcalar = metin.Split('"');
                listBox3.Items.Clear();
                foreach (string i2 in parcalar)
                {
                    listBox3.Items.Add(i2);
                }
                listBox4.Items.Add(listBox3.Items[1].ToString());
                string Metin2 = listBox3.Items[listBox3.Items.Count - 1].ToString();
                listBox5.Items.Add(Metin2.Remove(0,1));
                ArkadasLinkYaz();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            webBrowser1.Navigate(listBox2.SelectedItem.ToString());
        }
    }
}
