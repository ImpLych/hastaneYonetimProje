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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            //datagrid'e SQLde bulunan doktorlar tablosunun gösterimi
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select * from tbl_doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;


            //Branşların Combobox'a aktarılması
            SqlCommand komut2 = new SqlCommand("select bransad from tbl_branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);

            }
            bgl.baglanti().Close();
        }


        //Yeni doktor ekleme işlemleri 
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_doktorlar(doktorad,doktorsoyad,doktorbrans,doktorTC,doktorsifre) values (@d1,@d2,@d3,@d4,@d5)",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1",TxtAd.Text);
            komut.Parameters.AddWithValue("@d2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@d3",CmbBrans.Text);
            komut.Parameters.AddWithValue("@d4",MskTC.Text);
            komut.Parameters.AddWithValue("@d5",TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }


        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }


        //Doktor Silme işlemleri
        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_Doktorlar where DoktorTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Silindi","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }


        //Doktor bilgilerinin Güncellenmesi
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update tbl_doktorlar set doktorad=@d1,doktorsoyad=@d2,doktorbrans=@d3,doktorsifre=@d5 where doktorTC=@d4",bgl.baglanti());
            komut2.Parameters.AddWithValue("@d1", TxtAd.Text);
            komut2.Parameters.AddWithValue("@d2", txtSoyad.Text);
            komut2.Parameters.AddWithValue("@d3", CmbBrans.Text);
            komut2.Parameters.AddWithValue("@d4", MskTC.Text);
            komut2.Parameters.AddWithValue("@d5", TxtSifre.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }




    }
}
