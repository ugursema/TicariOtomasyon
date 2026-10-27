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
    public partial class FrmMusteriler : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            Listele();
            SehirList();
            gridView1.OptionsBehavior.Editable = false;//grid edit özelliği kapatma

        }
        SqlBaglantisi connect = new SqlBaglantisi();

        void Listele()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Musteriler", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl1.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }
      
        
        void SehirList()
        {
            SqlCommand il = new SqlCommand("select SehirAd from tbl_Iller", connect.baglanti());
            SqlDataReader dr = il.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);
            }
            connect.baglanti().Close();
        }
        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();
            SqlCommand ilce = new SqlCommand("select IlceAd from tbl_Ilceler where SehirAd=@p1", connect.baglanti());
            ilce.Parameters.AddWithValue("@p1", cmbil.SelectedIndex + 1);
            SqlDataReader dr = ilce.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            connect.baglanti().Close();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int secilen = e.RowHandle;
            //MessageBox.Show("Çalıştı"); // calışmayı test etmek icin


            txtId.Text = gridView1.GetRowCellValue(secilen, "MusteriId").ToString();
            txtAd.Text = gridView1.GetRowCellValue(secilen, "MusteriAd").ToString();
            txtSoyad.Text = gridView1.GetRowCellValue(secilen, "MusteriSoyad").ToString(); 
            msktxtTel1.Text = gridView1.GetRowCellValue(secilen, "MusteriTelefon").ToString();
            msktxtTel2.Text = gridView1.GetRowCellValue(secilen, "MusteriTelefon2").ToString();
            txtMail.Text = gridView1.GetRowCellValue(secilen, "MusteriMail").ToString();
            cmbil.Text = gridView1.GetRowCellValue(secilen, "MusteriIl").ToString();
            cmbilce.Text = gridView1.GetRowCellValue(secilen, "MusteriIlce").ToString();
            rchtxtAdres.Text = gridView1.GetRowCellValue(secilen, "MusteriAdres").ToString();
            txtvergi.Text = gridView1.GetRowCellValue(secilen, "MusteriVergiDairesi").ToString(); 
        }

        //cift tıklayyıbca ekrana gelmesi 2.yöntem
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (view == null) return;

            //int secilen = view.FocusedRowHandle;
            //if (secilen < 0) return;

            //txtId.Text = view.GetRowCellValue(secilen, "MusteriId")?.ToString();
            //txtAd.Text = view.GetRowCellValue(secilen, "MusteriAd")?.ToString();
            //txtSoyad.Text = view.GetRowCellValue(secilen, "MusteriSoyad")?.ToString();
            //msktxtTel1.Text = view.GetRowCellValue(secilen, "MusteriTelefon")?.ToString();
            //msktxtTel2.Text = view.GetRowCellValue(secilen, "MusteriTelefon2")?.ToString();
            //txtMail.Text = view.GetRowCellValue(secilen, "MusteriMail")?.ToString();
            //cmbil.Text = view.GetRowCellValue(secilen, "MusteriIl")?.ToString();
            //cmbilce.Text = view.GetRowCellValue(secilen, "MusteriIlce")?.ToString();
            //rchtxtAdres.Text = view.GetRowCellValue(secilen, "MusteriAdres")?.ToString();
            //txtvergi.Text = view.GetRowCellValue(secilen, "MusteriVergiDairesi")?.ToString();
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

            string add = "INSERT INTO tbl_Musteriler (MusteriAd,MusteriSoyad,MusteriTelefon,MusteriTelefon2,MusteriTC,MusteriMail,MusteriIl,MusteriIlce,MusteriAdres,MusteriVergiDairesi) VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)";

            SqlCommand komutekle = new SqlCommand(add, connect.baglanti());
            komutekle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutekle.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komutekle.Parameters.AddWithValue("@p3", msktxtTel1.Text);
            komutekle.Parameters.AddWithValue("@p4", msktxtTel2.Text);
            komutekle.Parameters.AddWithValue("@p5", mksTC.Text);
            komutekle.Parameters.AddWithValue("@p6", txtMail.Text);
            komutekle.Parameters.AddWithValue("@p7", cmbil.Text);
            komutekle.Parameters.AddWithValue("@p8", cmbilce.Text); 
            komutekle.Parameters.AddWithValue("@p9", rchtxtAdres.Text);
            komutekle.Parameters.AddWithValue("@p10", txtvergi.Text);
            komutekle.ExecuteNonQuery();
            connect.baglanti().Close();
            MessageBox.Show("Kayıt Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            temizle();


        }
        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            msktxtTel1.Text = "";
            msktxtTel2.Text = "";
            mksTC.Text = "";
            txtMail.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            rchtxtAdres.Text = "";
            txtvergi.Text = "";
            txtId.Focus();

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

                string update = "UPDATE tbl_Musteriler SET " +
                                "MusteriAd = @p1, " +
                                "MusteriSoyad = @p2, " +
                                "MusteriTelefon = @p3, " +
                                "MusteriTelefon2 = @p4, " +
                                "MusteriTC = @p5, " +
                                "MusteriMail = @p6, " +
                                "MusteriIl = @p7, " +
                                "MusteriIlce = @p8, " +
                                "MusteriAdres = @p9, " +
                                "MusteriVergiDairesi = @p10 " +
                                "WHERE MusteriId = @p11";

                SqlCommand komut = new SqlCommand(update, connect.baglanti());

                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", msktxtTel1.Text);
                komut.Parameters.AddWithValue("@p4", msktxtTel2.Text);
                komut.Parameters.AddWithValue("@p5", mksTC.Text);
                komut.Parameters.AddWithValue("@p6", txtMail.Text);
                komut.Parameters.AddWithValue("@p7", cmbil.Text);
                komut.Parameters.AddWithValue("@p8", cmbilce.Text);
                komut.Parameters.AddWithValue("@p9", rchtxtAdres.Text);
                komut.Parameters.AddWithValue("@p10", txtvergi.Text);
                komut.Parameters.AddWithValue("@p11", txtId.Text);

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
                string delete = "DELETE FROM tbl_Musteriler WHERE MusteriId = @p1";
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        //pdf
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

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
        //excel
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
        public int kullaniciId;
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form1 anasayfa = new Form1(kullaniciId);
            anasayfa.Show();
            this.Close();
        }

        private void ribbonStatusBar1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainerControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtvergi_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl11_Click(object sender, EventArgs e)
        {

        }

        private void cmbilce_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelControl10_Click(object sender, EventArgs e)
        {

        }

        private void rchtxtAdres_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        private void txtMail_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }

        private void msktxtTel1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mksTC_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void msktxtTel2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void labelControl6_Click(object sender, EventArgs e)
        {

        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void txtSoyad_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void txtAd_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void txtId_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void xtraSaveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
