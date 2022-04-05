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
    public partial class IslemSaniyesi : Form
    {
        public IslemSaniyesi()
        {
            InitializeComponent();
        }
        public static SqlConnection baglanti = new SqlConnection("Data Source=BADBOY-PC; Initial Catalog=sst;User ID=sa;Password=1236asd");
        #region Sql Verileri Çekme Fnc
        void SqlVeriCekFnc()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * from Islem", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader["Paylasim"].ToString());
            }
            baglanti.Close();


        }

        #endregion
        #region Paylasim Kayit güncelle
        void PaylasimKayitGuncelle()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            string silme = "Delete From Islem Where Paylasim is not null";
            SqlCommand silmekomut = new SqlCommand(silme, baglanti);
            silmekomut.ExecuteNonQuery();
            SqlPaylasimKayitEkle();
            baglanti.Close();
        }
        void SqlPaylasimKayitEkle()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                string kayit = "insert into Islem(Paylasim) values (@Paylasim)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                komut.Parameters.AddWithValue("@Paylasim", listBox1.Items[i].ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
        }
        #endregion









        private void button1_Click(object sender, EventArgs e)
        {
            PaylasimKayitGuncelle();
        }

        private void IslemSaniyesi_Load(object sender, EventArgs e)
        {
            SqlVeriCekFnc();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(PaylasimTxt1.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlVeriCekFnc();
        }
    }
}
