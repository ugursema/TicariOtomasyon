using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomason
{
    public partial class FrmFirmalar : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmFirmalar()
        {
            InitializeComponent();
           
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            Listele();
            SehirList();
            cariKodAciklamalar();
        }

        SqlBaglantisi connect = new SqlBaglantisi();

        void Listele()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Firmalar", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl1.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }

        void temizle()
        {
            txtfId.Text = "";
            txtfAd.Text = "";
            cmbyil.Text = "";
            cmbyilce.Text = "";
            mskyTC.Text = "";
            txtfSektör.Text = "";
            txtfYetkili.Text = "";
            txtfYGörev.Text = "";
            mskyTC.Text = "";
            mskytel1.Text = "";
            mskytel2.Text = "";
            mskytel3.Text = "";
            mskyfax.Text = "";
            txtmail.Text = "";
            rchyadres.Text = "";
            txtkod1.Text = "";
            txtkod2.Text = "";
            txtkod3.Text = "";
            txtmail.Text = "";

            txtfAd.Focus();
        }
        void cariKodAciklamalar()
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM tbl_kodlar", connect.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                rch1.Text = dr[0].ToString();
                rch2.Text = dr[1].ToString();
                rhc3.Text = dr[2].ToString();
            }
            connect.baglanti().Close();
        }
        private void btnyKaydet_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtfId.Text))
            {
                MessageBox.Show("Bu kayıt zaten mevcut. Güncelle butonunu kullanın.",
                                "Uyarı",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            string add = @"INSERT INTO tbl_Firmalar
    (FirmaAd, FirmaYetkiliStatu, FirmaYetkiliAdSoyad, YetkiliTC, Sektör,
     FirmaMail, FirmaTelefon1, FirmaTelefon2, FirmaTelefon3, FirmaFax,
     FirmaIl, FirmaIlce, FirmaVergiDairesi, FirmaAdres,
     OzelKod1, OzelKod2, OzelKod3)
     VALUES
    (@p1, @p2, @p3, @p4, @p5,
     @p6, @p7, @p8, @p9, @p10,
     @p11, @p12, @p13, @p14,
     @p15, @p16, @p17)";

            SqlCommand komut = new SqlCommand(add, connect.baglanti());

            komut.Parameters.AddWithValue("@p1", txtfAd.Text);
            komut.Parameters.AddWithValue("@p2", txtfYGörev.Text);
            komut.Parameters.AddWithValue("@p3", txtfYetkili.Text);
            komut.Parameters.AddWithValue("@p4", mskyTC.Text);
            komut.Parameters.AddWithValue("@p5", txtfSektör.Text);
            
            komut.Parameters.AddWithValue("@p6", txtmail.Text); //  

            komut.Parameters.AddWithValue("@p7", mskytel1.Text);
            komut.Parameters.AddWithValue("@p8", mskytel2.Text);
            komut.Parameters.AddWithValue("@p9", mskytel3.Text);
            komut.Parameters.AddWithValue("@p10", mskyfax.Text);

            komut.Parameters.AddWithValue("@p11", cmbyil.Text);
            komut.Parameters.AddWithValue("@p12", cmbyilce.Text);
            komut.Parameters.AddWithValue("@p13", txtyvergi.Text);
            komut.Parameters.AddWithValue("@p14", rchyadres.Text);

            komut.Parameters.AddWithValue("@p15", txtkod1.Text);
            komut.Parameters.AddWithValue("@p16", txtkod2.Text);
            komut.Parameters.AddWithValue("@p17", txtkod3.Text);

            komut.ExecuteNonQuery();
            komut.Connection.Close();

            MessageBox.Show("Kayıt Eklendi", "Bilgi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            Listele();
            temizle();
        }


        private void btnyGuncelle_Click(object sender, EventArgs e)
        {
            string update = @"UPDATE tbl_Firmalar SET
                    FirmaAd = @p1,
                    FirmaYetkiliStatu = @p2,
                    FirmaYetkiliAdSoyad = @p3,
                    YetkiliTC = @p4,
                    Sektör = @p5,
                    FirmaMail = @p6,
                    FirmaTelefon1 = @p7,
                    FirmaTelefon2 = @p8,
                    FirmaTelefon3 = @p9,
                    FirmaFax = @p10,
                    FirmaIl = @p11,
                    FirmaIlce = @p12,
                    FirmaVergiDairesi = @p13,
                    FirmaAdres = @p14,
                    OzelKod1 = @p15,
                    OzelKod2 = @p16,
                    OzelKod3 = @p17
                    WHERE FirmaId = @p18";

            SqlCommand komut = new SqlCommand(update, connect.baglanti());

            komut.Parameters.AddWithValue("@p1", txtfAd.Text);
            komut.Parameters.AddWithValue("@p2", txtfYGörev.Text);
            komut.Parameters.AddWithValue("@p3", txtfYetkili.Text);
            komut.Parameters.AddWithValue("@p4", mskyTC.Text);
            komut.Parameters.AddWithValue("@p5", txtfSektör.Text);
            komut.Parameters.AddWithValue("@p6", txtmail.Text);   //  

            komut.Parameters.AddWithValue("@p7", mskytel1.Text);
            komut.Parameters.AddWithValue("@p8", mskytel2.Text);
            komut.Parameters.AddWithValue("@p9", mskytel3.Text);
            komut.Parameters.AddWithValue("@p10", mskyfax.Text);

            komut.Parameters.AddWithValue("@p11", cmbyil.Text);
            komut.Parameters.AddWithValue("@p12", cmbyilce.Text);
            komut.Parameters.AddWithValue("@p13", txtyvergi.Text);
            komut.Parameters.AddWithValue("@p14", rchyadres.Text);

            komut.Parameters.AddWithValue("@p15", txtkod1.Text);
            komut.Parameters.AddWithValue("@p16", txtkod2.Text);
            komut.Parameters.AddWithValue("@p17", txtkod3.Text);

            komut.Parameters.AddWithValue("@p18", Convert.ToInt32(txtfId.Text));


            komut.ExecuteNonQuery();
            komut.Connection.Close();
            MessageBox.Show("Kayıt Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            temizle();

        } 
         
    private void btnySil_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Kişi Silinsin mi","Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(cevap == DialogResult.Yes)
            {
                string delete = "DELETE FROM tbl_Firmalar where FirmaId = @p1";
                SqlCommand komutsil = new SqlCommand(delete, connect.baglanti());
                komutsil.Parameters.AddWithValue("@p1", txtfId.Text);
                komutsil.ExecuteNonQuery();
                komutsil.Connection.Close();
                MessageBox.Show("Kayıt Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                temizle();
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                txtfId.Text = dr["FirmaId"].ToString();
                txtfAd.Text = dr["FirmaAd"].ToString();
                txtfYGörev.Text = dr["FirmaYetkiliStatu"].ToString();
                txtfYetkili.Text = dr["FirmaYetkiliAdSoyad"].ToString();
                mskyTC.Text = dr["YetkiliTC"].ToString();
                txtfSektör.Text = dr["Sektör"].ToString(); 
                txtfId.Text = dr["FirmaId"].ToString();
                mskytel1.Text = dr["FirmaTelefon1"].ToString();
                mskytel2.Text = dr["FirmaTelefon2"].ToString();
                mskytel3.Text = dr["FirmaTelefon3"].ToString();
                mskyfax.Text = dr["FirmaFax"].ToString();
                cmbyil.Text = dr["FirmaIl"].ToString();
                cmbyilce.Text = dr["FirmaIlce"].ToString();
                txtyvergi.Text = dr["FirmaVergiDairesi"].ToString();
                rchyadres.Text = dr["FirmaAdres"].ToString();
                txtkod1.Text = dr["OzelKod1"].ToString();
                txtkod2.Text = dr["OzelKod2"].ToString();
                txtkod3.Text = dr["OzelKod3"].ToString();
                txtmail.Text = dr["FirmaMail"].ToString();

            }
        }
        void SehirList()
        {
            SqlCommand sehir = new SqlCommand("select SehirAd from tbl_Iller", connect.baglanti());
            SqlDataReader dr = sehir.ExecuteReader();
            while(dr.Read())
            {
                cmbyil.Properties.Items.Add(dr[0]);
            }
            connect.baglanti().Close();
        }

        private void cmbyil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbyilce.Properties.Items.Clear();
            SqlCommand ilce = new SqlCommand("select IlceAd from tbl_Ilceler where SehirAd=@p1", connect.baglanti());
            ilce.Parameters.AddWithValue("@p1", cmbyil.SelectedIndex + 1);
            SqlDataReader dr = ilce.ExecuteReader();
            while (dr.Read())
            {
                cmbyilce.Properties.Items.Add(dr[0]);
            }
            connect.baglanti().Close();
            
        }

        private void btnyTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
            gridControl1.ShowRibbonPrintPreview();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            xtraSaveFileDialog1.AddExtension = true;
            xtraSaveFileDialog1.Filter = "Pdf (*.pdf)|*.pdf";
            xtraSaveFileDialog1.FileName = "Firma_Liste";
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = xtraSaveFileDialog1.FileName;
                string path = Path.GetFullPath(fileName);
                gridControl1.ExportToPdf(path);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.AddExtension = true;
            xtraSaveFileDialog1.Filter = "Excel (*.xlsx)|*.xlsx";
            xtraSaveFileDialog1.FileName = "Firma_Liste";
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = xtraSaveFileDialog1.FileName;
                string path = Path.GetFullPath(fileName);
                gridControl1.ExportToXlsx(path);
            }
        }
        public int kullaniciId;
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form1 anasayfa = new Form1(kullaniciId);
            anasayfa.Show();
            this.Close();
        }
    }
}
