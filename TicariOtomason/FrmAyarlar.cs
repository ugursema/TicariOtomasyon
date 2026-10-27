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
    public partial class FrmAyarlar : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }
        public int kullaniciId;
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            Listele();
        }
        SqlBaglantisi connect = new SqlBaglantisi();
        void Listele()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("Select * from tbl_Admin ", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl1.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Eğer Id doluysa bu kayıt zaten vardır → INSERT yapma hata mesajı ver
            if (!string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Bu kayıt zaten mevcut. Güncelle butonunu kullanın.",
                                "Uyarı",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return; // işlemi durdur
            }

            string add = "INSERT INTO tbl_Admin (KullaniciAd,KullaniciSifre) VALUES(@p1,@p2)";

            SqlCommand komutekle = new SqlCommand(add, connect.baglanti());
            komutekle.Parameters.AddWithValue("@p1", txtKullaniciAd.Text);
            komutekle.Parameters.AddWithValue("@p2", txtSifre.Text); 
            komutekle.ExecuteNonQuery();
            connect.baglanti().Close();
            MessageBox.Show("Kayıt Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            temizle();
        }
        void temizle()
        {
             
            txtKullaniciAd.Text = "";
            txtSifre.Text = "";
            txtKullaniciAd.Focus();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Güncellenecek kayıt seçiniz.",
                                "Uyarı",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            string update = "UPDATE tbl_Admin SET " +
                            "KullaniciAd = @p1, " +
                            "KullaniciSifre = @p2 " +
                            "WHERE KullaniciId = @p3";

            SqlCommand komut = new SqlCommand(update, connect.baglanti());

            komut.Parameters.AddWithValue("@p1", txtKullaniciAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text); 
            komut.Parameters.AddWithValue("@p3", txtId.Text);

            komut.ExecuteNonQuery();
            komut.Connection.Close();

            MessageBox.Show("Kayıt güncellendi",
                            "Bilgi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            Listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            DialogResult cevap = MessageBox.Show("Kişi silinsin mi?",
                                                 "Uyarı",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

            if (cevap == DialogResult.Yes)
            {
                string delete = "DELETE FROM tbl_Admin WHERE KullaniciId = @p1";
                SqlCommand komutSil = new SqlCommand(delete, connect.baglanti());
                komutSil.Parameters.AddWithValue("@p1", txtId.Text);

                komutSil.ExecuteNonQuery();
                komutSil.Connection.Close();

                MessageBox.Show("Kayıt silindi");

                Listele();
                temizle();
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi");
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form1 anasayfa = new Form1(kullaniciId);
            anasayfa.Show();
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            gridControl1.ShowRibbonPrintPreview();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { //pdf

            xtraSaveFileDialog1.AddExtension = true;
            xtraSaveFileDialog1.Filter = "Pdf (*.pdf)|*.pdf";
            xtraSaveFileDialog1.FileName = "Musteri_Liste";
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = xtraSaveFileDialog1.FileName;
                string path = Path.GetFullPath(fileName);
                gridControl1.ExportToPdf(path);
            }

        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //excel
            xtraSaveFileDialog1.AddExtension = true;
            xtraSaveFileDialog1.Filter = "Excel (*.xlsx)|*.xlsx";
            xtraSaveFileDialog1.FileName = "Musteri_Liste";
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = xtraSaveFileDialog1.FileName;
                string path = Path.GetFullPath(fileName);
                gridControl1.ExportToXlsx(path);
            }
        }
       

    }
}
