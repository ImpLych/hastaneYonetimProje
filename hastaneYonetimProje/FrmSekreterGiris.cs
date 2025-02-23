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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void BtnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from tbl_sekreter where sekreterTC=@p1 and sekreterSifre=@p2 ",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1" , MskTC.Text);
            komut.Parameters.AddWithValue("@p2" , TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmSekreterDetay frs = new FrmSekreterDetay();
                frs.TCnumara = MskTC.Text;
                frs.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Hatalı TC ya da Şifre");
            }
                bgl.baglanti().Close();
            

        }



        private void BtnGeri_Click(object sender, EventArgs e)
        {

            FrmGirisler fr = new FrmGirisler();
            fr.Show();
            this.Hide();

        }
    }
}
