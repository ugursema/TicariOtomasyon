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

namespace TicariOtomason
{
    public partial class FrmFaturaUrunDuzenleme : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        public string urunId;
        SqlBaglantisi connect = new SqlBaglantisi();
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }

        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            txtUrunId.Text = urunId;
            SqlCommand komut = new SqlCommand("select * from tbl_FaturaDetay where FaturaUrunId=@p1",connect.baglanti());
            komut.Parameters.AddWithValue("@p1", urunId);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtFiyat.Text = dr[3].ToString();
                txtMiktar.Text = dr[2].ToString();
                TxtTutar.Text = dr[4].ToString();
                txtUrunAd.Text = dr[1].ToString();

                connect.baglanti().Close();
             }
            
        }

        private void btnGuncelleUrun_Click(object sender, EventArgs e)
        {
            string update = "UPDATE tbl_FaturaDetay set UrunAd=@p1,UrunMiktar=@p2,UrunFiyat=@p3,Tutar=@p4 where FaturaUrunId=@p5";
            SqlCommand komut1 = new SqlCommand(update, connect.baglanti());

            komut1.Parameters.AddWithValue("@p1", txtUrunAd.Text);
            komut1.Parameters.AddWithValue("@p2", txtMiktar.Text);

            komut1.Parameters.AddWithValue("@p3",decimal.Parse( txtFiyat.Text));

            komut1.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));

            komut1.Parameters.AddWithValue("@p5", txtUrunId.Text);
            komut1.ExecuteNonQuery();
            connect.baglanti().Close();

            MessageBox.Show("Değişiklikler Kaydedildi","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Banka kaydı silinsin mi?",
                                              "Uyarı",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Warning);
            if ( cevap==DialogResult.Yes)
            {
                SqlCommand komut2 = new SqlCommand("Delete from tbl_FaturaDetay where FaturaUrunId=@p1", connect.baglanti());
                komut2.Parameters.AddWithValue("@p1", txtUrunId.Text);
                komut2.ExecuteNonQuery();
                connect.baglanti().Close();
                MessageBox.Show("Değişiklikler Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("İşlem iptal edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public int kullaniciId;
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form1 anasayfa = new Form1(kullaniciId);
            anasayfa.Show();
            this.Close();
        }
    }
}
