using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace facebook_paylasim
{
    public partial class Paylasim : Form
    {
        public Paylasim()
        {
            InitializeComponent();
        }
        public static SqlConnection baglanti = new SqlConnection(("Data Source=BADBOY-PC") + @"\" + ("SqlExpress; Initial Catalog=sst;User ID=sa;Password=1236asd"));


        private void Paylasim_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SonIndexSayisiFnc();
        }

        #region Son index Sayisi Çek
        void SonIndexSayisiFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Paylasim WHERE id is not null", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                SonIndexTxt.Text = (reader["id"].ToString());
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
            int SonIndexDegerArttir = Convert.ToInt16(SonIndexTxt.Text);
            SonIndexDegerArttir++;
            string kayit = "insert into Paylasim(id,paylasimNvarchar) values (@id,@paylasimNvarchar)";
            SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
            komutpaylasim.Parameters.AddWithValue("@id", SonIndexDegerArttir);
            komutpaylasim.Parameters.AddWithValue("@paylasimNvarchar", paylasimNvarchar.Text);
            komutpaylasim.ExecuteNonQuery();
            baglanti.Close();
            SonIndexSayisiFnc();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            paylasilacakMetinEkleFnc();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Paylasim", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader["paylasimNvarchar"].ToString());
                listBox2.Items.Add(reader["paylasilanEposta"].ToString());
            }
            baglanti.Close();


            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                string kayit = "insert into Paylasim2(id,paylasimNvarchar) values (@id,@paylasimNvarchar)";
                SqlCommand komutEkle = new SqlCommand(kayit, baglanti);
                komutEkle.Parameters.AddWithValue("@id", i);
                komutEkle.Parameters.AddWithValue("@paylasimNvarchar", listBox1.Items[i].ToString());
                komutEkle.ExecuteNonQuery();
                baglanti.Close();
            }



        }
    }
}
