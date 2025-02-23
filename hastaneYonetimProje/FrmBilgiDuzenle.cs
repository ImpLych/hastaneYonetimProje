﻿using System;
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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string TCno,ad,soyad,telNo;

        private void BtnGeri_Click(object sender, EventArgs e)
        {

        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update tbl_hastalar set hastaAd=@p1,hastaSoyad=@p2,hastaTelefon=@p3,hastaSifre=@p4,hastaCinsiyet=@p5 where hastatc=@p6",bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1",TxtAd.Text);
            komut2.Parameters.AddWithValue("@p2",TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3",MskTelefon.Text);
            komut2.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut2.Parameters.AddWithValue("@p5",CmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6",MskTC.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCno;
            SqlCommand komut = new SqlCommand("select * from Tbl_Hastalar where hastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTC.Text);
            SqlDataReader dr= komut.ExecuteReader();
            while (dr.Read()) 
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                MskTelefon.Text = dr[4].ToString();
                TxtSifre.Text = dr[5].ToString();
                CmbCinsiyet.Text = dr[6].ToString();
                bgl.baglanti().Close();

            }
        }
    }
}
