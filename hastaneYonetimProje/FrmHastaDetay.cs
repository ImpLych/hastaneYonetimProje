using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace hastaneYonetimProje
{
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc,adSoyad;



        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            //Ad Soyad ve TC No. Çekme İşlemleri
            sqlbaglantisi bgl =new sqlbaglantisi();
            SqlCommand komut= new SqlCommand("select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] +" "+ dr[1];
            }
            bgl.baglanti().Close();



            //Randevu Geçmişi Çekme İşlemleri
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevular where hastaTC=" +tc, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            //Branş Çekme İşlemleri
            SqlCommand komut2 = new SqlCommand("select BransAd from tbl_branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

        }

            //bilgi Düzenleme işlemleri
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCno=LblTC.Text;
            fr.Show();

            
        }
       

            //Branş seçimi sonrası doktor bilgilerinin çekilmesi
        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlbaglantisi bgl = new sqlbaglantisi();
            SqlCommand komut3 = new SqlCommand("select doktorad,doktorsoyad from tbl_doktorlar where Doktorbrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.SelectedItem.ToString());
           
            SqlDataReader dr3 = komut3.ExecuteReader();
            CmbDoktor.Items.Clear();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0]+" " + dr3[1]);
                
            }
            bgl.baglanti().Close();

        }

            private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlbaglantisi bgl = new sqlbaglantisi();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevular where RandevuBrans='" + CmbBrans.Text +"'"+"and randevuDoktor='"+CmbDoktor.Text +"'and randevuDurum=0", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();


        }

        private void BtnGeri_Click(object sender, EventArgs e)
        {

            FrmHastaGiris fr = new FrmHastaGiris();
            fr.Show();
            this.Hide();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            sqlbaglantisi bgl = new sqlbaglantisi();
            SqlCommand komut = new SqlCommand("update tbl_randevular set randevudurum=1,HastaTc=@p1,hastasikayet=@p2 where randevuid=@p3",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",LblTC.Text);
            komut.Parameters.AddWithValue("@p2",RchSikayet.Text);
            komut.Parameters.AddWithValue("@p3",Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);

        }
    }
}
