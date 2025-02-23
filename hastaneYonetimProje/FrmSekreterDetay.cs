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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string TCnumara;
    
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            //TC No.
            LblTC.Text = TCnumara;

            //Ad Soyad 
            SqlCommand komut1 = new SqlCommand("select SekreterAdSoyad from tbl_sekreter where sekreterTC=@p1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",LblTC.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();


            //Branşların DataGrid'e taşınması
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select bransAd from tbl_Branslar",bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Doktorların DataGrid'e taşınması
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select(Doktorad +' '+ Doktorsoyad)as 'Doktorlar',doktorbrans from tbl_doktorlar",bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;


            //Branşların ComboBoxta Listelenmesi
            SqlCommand komut2 = new SqlCommand("select bransAd from tbl_branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

            

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into  tbl_randevular(RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)",bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1",MskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2",MskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3",CmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4",CmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Başarıyla Oluşturuldu");
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("select doktorad,doktorsoyad from tbl_doktorlar where doktorbrans=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",CmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0]+" " + dr[1]);
            }
            bgl.baglanti().Close();

        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_duyurular (duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1",RchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu.");
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans frb = new FrmBrans();
            frb.Show();

        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frl = new FrmRandevuListesi();
            frl.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();

        }

        private void BtnGeri_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris fr = new FrmSekreterGiris();
            fr.Show();
            this.Hide();

        }
    }
}
