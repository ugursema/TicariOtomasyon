using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TicariOtomason
{
    public partial class FrmUrunler : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            Listele();
            gridView1.OptionsBehavior.Editable = false;//grid edit özelliği kapatma


        }

        SqlBaglantisi connect = new SqlBaglantisi();

        void Listele()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Urunler", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur
             
            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl1.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }
        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtMarka.Text = "";
            txtModel.Text = "";
            mkstxtYil.Text = "";
            numDownAdet.Text = "";
            txtAlis.Text = "";
            txtSatis.Text = "";
            rchtxtDetay.Text = "";

            txtAd.Focus();

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
            xtraSaveFileDialog1.FileName = "Urunler_Liste";
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
            xtraSaveFileDialog1.FileName = "Urunler_Liste";
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = xtraSaveFileDialog1.FileName;
                string path = Path.GetFullPath(fileName);
                gridControl1.ExportToXlsx(path);
            }
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

            string add = "INSERT INTO tbl_Urunler (UrunAd,UrunMarka,UrunModel,UrunYil,UrunAdet,UrunAlisFiyat,UrunSatisFiyat,UrunDetay) VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)";

            SqlCommand komutekle = new SqlCommand(add, connect.baglanti());
            komutekle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutekle.Parameters.AddWithValue("@p2", txtMarka.Text);
            komutekle.Parameters.AddWithValue("@p3", txtModel.Text);
            komutekle.Parameters.AddWithValue("@p4", mkstxtYil.Text);
            komutekle.Parameters.AddWithValue("@p5", numDownAdet.Value);
            komutekle.Parameters.AddWithValue("@p6", txtAlis.Text);
            komutekle.Parameters.AddWithValue("@p7", txtSatis.Text);
            komutekle.Parameters.AddWithValue("@p8", rchtxtDetay.Text); 
            komutekle.ExecuteNonQuery();
            komutekle.Connection.Close();
            MessageBox.Show("Kayıt Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            temizle();


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string update = "UPDATE tbl_Urunler SET " +
                            "UrunAd = @p1, " +
                            "UrunMarka = @p2, " +
                            "UrunModel = @p3, " +
                            "UrunYil = @p4, " +
                            "UrunAdet = @p5, " +
                            "UrunAlisFiyat = @p6, " +
                            "UrunSatisFiyat = @p7, " +
                            "UrunDetay = @p8 " +
                            "WHERE UrunId = @p9";

            SqlConnection conn = connect.baglanti();
            SqlCommand komut = new SqlCommand(update, conn);

            // STRING alanlar
            komut.Parameters.Add("@p1", SqlDbType.NVarChar).Value = txtAd.Text;
            komut.Parameters.Add("@p2", SqlDbType.NVarChar).Value = txtMarka.Text;
            komut.Parameters.Add("@p3", SqlDbType.NVarChar).Value = txtModel.Text;
            komut.Parameters.Add("@p4", SqlDbType.NVarChar).Value = mkstxtYil.Text;
            komut.Parameters.Add("@p8", SqlDbType.NVarChar).Value = rchtxtDetay.Text;

            // NUMERIC alanlar
            komut.Parameters.Add("@p5", SqlDbType.Int).Value = Convert.ToInt32(numDownAdet.Value);
            komut.Parameters.Add("@p6", SqlDbType.Decimal).Value = Convert.ToDecimal(txtAlis.Text);
            komut.Parameters.Add("@p7", SqlDbType.Decimal).Value = Convert.ToDecimal(txtSatis.Text);
            komut.Parameters.Add("@p9", SqlDbType.Int).Value = Convert.ToInt32(txtId.Text);

            komut.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Ürün güncellendi", "Bilgi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            Listele();
            temizle();
        }


        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int secilen = e.RowHandle;

            txtId.Text = gridView1.GetRowCellValue(secilen, "UrunId").ToString();
            txtAd.Text = gridView1.GetRowCellValue(secilen, "UrunAd").ToString();
            txtMarka.Text = gridView1.GetRowCellValue(secilen, "UrunMarka").ToString();
            txtModel.Text = gridView1.GetRowCellValue(secilen, "UrunModel").ToString();
            mkstxtYil.Text = gridView1.GetRowCellValue(secilen, "UrunYil").ToString();
            numDownAdet.Value = Convert.ToDecimal(gridView1.GetRowCellValue(secilen, "UrunAdet"));
            txtAlis.Text = gridView1.GetRowCellValue(secilen, "UrunAlisFiyat").ToString();
            txtSatis.Text = gridView1.GetRowCellValue(secilen, "UrunSatisFiyat").ToString();
            rchtxtDetay.Text = gridView1.GetRowCellValue(secilen, "UrunDetay").ToString();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Kişi silinsin mi?",
                                                 "Uyarı",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

            if (cevap == DialogResult.Yes)
            {
                string delete = "DELETE FROM tbl_Urunler WHERE UrunId = @p1";
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
    }
}
