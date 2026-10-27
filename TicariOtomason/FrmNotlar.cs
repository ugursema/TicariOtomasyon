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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }
        public int kullaniciId;
        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            gridView1.OptionsBehavior.Editable = false; // grid yazı yazılmasın
            msktxtTarih.ReadOnly = true;
            msktxtSaat.ReadOnly = true;

            DateTime simdi = DateTime.Now;

            msktxtTarih.Text = simdi.ToString("dd.MM.yyyy");
            msktxtSaat.Text = simdi.ToString("HH:mm");
            Listele();
        }

        SqlBaglantisi connect = new SqlBaglantisi();
        void Listele()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Notlar", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl1.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }

        void temizle()
        {
            txtId.Text = "";
            msktxtTarih.Text = "";
            msktxtSaat.Text = "";
            txtBaslik.Text = "";
            txtOlusturan.Text = "";
            txtHitap.Text = "";
            rchtxtDetay.Text = "";
            txtId.Focus();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Eğer Id doluysa bu kayıt zaten vardır
            if (!string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Bu kayıt zaten mevcut. Güncelle butonunu kullanın.",
                                "Uyarı",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            string add = @"INSERT INTO tbl_Notlar 
                   (NotTarih, NotSaat, NotBaslik, NotDetay, NotOlusturan, NotHitap) 
                   VALUES (@p1, @p2, @p3, @p4, @p5, @p6)";

            SqlCommand komutekle = new SqlCommand(add, connect.baglanti());

            DateTime simdi = DateTime.Now;

            komutekle.Parameters.AddWithValue("@p1", simdi.ToString("dd.MM.yyyy"));
            komutekle.Parameters.AddWithValue("@p2", simdi.ToString("HH:mm"));
            komutekle.Parameters.AddWithValue("@p3", txtBaslik.Text);
            komutekle.Parameters.AddWithValue("@p4", rchtxtDetay.Text);
            komutekle.Parameters.AddWithValue("@p5", txtOlusturan.Text);
            komutekle.Parameters.AddWithValue("@p6", txtHitap.Text);

            komutekle.ExecuteNonQuery();

            connect.baglanti().Close();

            MessageBox.Show("Kayıt Eklendi",
                            "Bilgi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            Listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                txtId.Text = dr["NotId"].ToString();
                msktxtTarih.Text = dr["NotTarih"].ToString();
                msktxtSaat.Text = dr["NotSaat"].ToString();
                txtBaslik.Text = dr["NotBaslik"].ToString();
                rchtxtDetay.Text = dr["NotDetay"].ToString();
                txtOlusturan.Text = dr["NotOlusturan"].ToString();
                txtHitap.Text = dr["NotHitap"].ToString();
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

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

            string update = "UPDATE tbl_Notlar SET " +
                            "NotBaslik = @p1, " +
                            "NotDetay = @p2, " +
                            "NotOlusturan = @p3, " +
                            "NotHitap = @p4 " +
                            "WHERE NotId = @p5";

            SqlCommand komut = new SqlCommand(update, connect.baglanti());

            komut.Parameters.AddWithValue("@p1", txtBaslik.Text);
            komut.Parameters.AddWithValue("@p2", rchtxtDetay.Text);
            komut.Parameters.AddWithValue("@p3", txtOlusturan.Text);
            komut.Parameters.AddWithValue("@p4", txtHitap.Text);
            komut.Parameters.AddWithValue("@p5", txtId.Text);

            komut.ExecuteNonQuery();
            komut.Connection.Close();

            MessageBox.Show("Kayıt güncellendi",
                            "Bilgi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            Listele();
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Silinecek kayıt seçiniz.",
                                "Uyarı",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            DialogResult cevap = MessageBox.Show("Not silinsin mi?",
                                                 "Uyarı",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

            if (cevap == DialogResult.Yes)
            {
                string delete = "DELETE FROM tbl_Notlar WHERE NotId = @p1";

                SqlCommand komutSil = new SqlCommand(delete, connect.baglanti());

                komutSil.Parameters.AddWithValue("@p1", txtId.Text);

                komutSil.ExecuteNonQuery();
                komutSil.Connection.Close();

                MessageBox.Show("Kayıt silindi",
                                "Bilgi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                Listele();
                temizle();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.AddExtension = true;
            xtraSaveFileDialog1.Filter = "Pdf (*.pdf)|*.pdf";
            xtraSaveFileDialog1.FileName = "Notlar_Liste";
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
            xtraSaveFileDialog1.FileName = "Notlar_Liste";
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = xtraSaveFileDialog1.FileName;
                string path = Path.GetFullPath(fileName);
                gridControl1.ExportToXlsx(path);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form1 anasayfa = new Form1(kullaniciId);
            anasayfa.Show();
            this.Close();
        }
    }
}
