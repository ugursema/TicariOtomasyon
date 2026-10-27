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
using System.IO;

namespace TicariOtomason
{
    public partial class FrmGiderler : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SqlBaglantisi connect = new SqlBaglantisi();
        public FrmGiderler()
        {
            InitializeComponent();
            YilListele();
            Listele();
            temizle();
        }

        void YilListele()
        {
            cmbYil.Properties.Items.Clear();

            for (int i = 2000; i <= 2030; i++)
            {
                cmbYil.Properties.Items.Add(i);
            }
        }


        void Listele()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Giderler", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl1.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }
        void temizle()
        {
            txtId.Text = "";
            cbmAy.Text = "";
            cmbYil.Text = "";
            txtElktrik.Text = "";
            txtSu.Text = "";
            txtGaz.Text = "";
            txtInternet.Text = "";
            txtMaas.Text = "";
            txtEkstra.Text = "";
            rchtxtNotlar.Text = "";

            cbmAy.Focus();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string ekle = @"INSERT INTO tbl_Giderler
    (GiderAy,GiderYil,GiderElektrik,GiderSu,GiderDogalgaz,
     GiderInternet,GiderMaaslar,GiderEkstra,GiderNotlar)
    VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)";

            SqlCommand komutadd = new SqlCommand(ekle, connect.baglanti());

            // YIL
            int yil = int.Parse(cmbYil.Text);

            // DECIMAL (boşsa 0)
            decimal elektrik = string.IsNullOrWhiteSpace(txtElktrik.Text)
                ? 0
                : decimal.Parse(txtElktrik.Text);

            decimal su = string.IsNullOrWhiteSpace(txtSu.Text)
                ? 0
                : decimal.Parse(txtSu.Text);

            decimal gaz = string.IsNullOrWhiteSpace(txtGaz.Text)
                ? 0
                : decimal.Parse(txtGaz.Text);

            decimal internet = string.IsNullOrWhiteSpace(txtInternet.Text)
                ? 0
                : decimal.Parse(txtInternet.Text);

            decimal maas = string.IsNullOrWhiteSpace(txtMaas.Text)
                ? 0
                : decimal.Parse(txtMaas.Text);

            decimal ekstra = string.IsNullOrWhiteSpace(txtEkstra.Text)
                ? 0
                : decimal.Parse(txtEkstra.Text);

            // PARAMETRELER
            komutadd.Parameters.Add("@p1", SqlDbType.NVarChar).Value = cbmAy.Text;
            komutadd.Parameters.Add("@p2", SqlDbType.Int).Value = yil;

            komutadd.Parameters.Add("@p3", SqlDbType.Decimal).Value = elektrik;
            komutadd.Parameters["@p3"].Precision = 10;
            komutadd.Parameters["@p3"].Scale = 2;

            komutadd.Parameters.Add("@p4", SqlDbType.Decimal).Value = su;
            komutadd.Parameters["@p4"].Precision = 10;
            komutadd.Parameters["@p4"].Scale = 2;

            komutadd.Parameters.Add("@p5", SqlDbType.Decimal).Value = gaz;
            komutadd.Parameters["@p5"].Precision = 10;
            komutadd.Parameters["@p5"].Scale = 2;

            komutadd.Parameters.Add("@p6", SqlDbType.Decimal).Value = internet;
            komutadd.Parameters["@p6"].Precision = 10;
            komutadd.Parameters["@p6"].Scale = 2;

            komutadd.Parameters.Add("@p7", SqlDbType.Decimal).Value = maas;
            komutadd.Parameters["@p7"].Precision = 10;
            komutadd.Parameters["@p7"].Scale = 2;

            komutadd.Parameters.Add("@p8", SqlDbType.Decimal).Value = ekstra;
            komutadd.Parameters["@p8"].Precision = 10;
            komutadd.Parameters["@p8"].Scale = 2;

            komutadd.Parameters.Add("@p9", SqlDbType.NVarChar).Value = rchtxtNotlar.Text;

            komutadd.ExecuteNonQuery();
            komutadd.Connection.Close();

            MessageBox.Show("Gider eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string guncelle = @"UPDATE tbl_Giderler SET GiderAy = @p1,GiderYil=@p2,GiderElektrik=@p3,GiderSu=@p4,GiderDogalgaz=@p5,GiderInternet=@p6,GiderMaaslar=@p7,GiderEkstra=@p8,GiderNotlar=@p9 WHERE GiderId=@p10";
            SqlCommand komutUpdate = new SqlCommand(guncelle, connect.baglanti());
            // ID
            int id = int.Parse(txtId.Text);

            // YIL
            int yil = int.Parse(cmbYil.Text);

            // DECIMAL (boşsa 0)
            decimal elektrik = string.IsNullOrWhiteSpace(txtElktrik.Text)
                ? 0
                : decimal.Parse(txtElktrik.Text);

            decimal su = string.IsNullOrWhiteSpace(txtSu.Text)
                ? 0
                : decimal.Parse(txtSu.Text);

            decimal gaz = string.IsNullOrWhiteSpace(txtGaz.Text)
                ? 0
                : decimal.Parse(txtGaz.Text);

            decimal internet = string.IsNullOrWhiteSpace(txtInternet.Text)
                ? 0
                : decimal.Parse(txtInternet.Text);

            decimal maas = string.IsNullOrWhiteSpace(txtMaas.Text)
                ? 0
                : decimal.Parse(txtMaas.Text);

            decimal ekstra = string.IsNullOrWhiteSpace(txtEkstra.Text)
                ? 0
                : decimal.Parse(txtEkstra.Text);

            // PARAMETRELER
            komutUpdate.Parameters.Add("@p1", SqlDbType.NVarChar).Value = cbmAy.Text;
            komutUpdate.Parameters.Add("@p2", SqlDbType.Int).Value = yil;

            komutUpdate.Parameters.Add("@p3", SqlDbType.Decimal).Value = elektrik;
            komutUpdate.Parameters["@p3"].Precision = 10;
            komutUpdate.Parameters["@p3"].Scale = 2;

            komutUpdate.Parameters.Add("@p4", SqlDbType.Decimal).Value = su;
            komutUpdate.Parameters["@p4"].Precision = 10;
            komutUpdate.Parameters["@p4"].Scale = 2;

            komutUpdate.Parameters.Add("@p5", SqlDbType.Decimal).Value = gaz;
            komutUpdate.Parameters["@p5"].Precision = 10;
            komutUpdate.Parameters["@p5"].Scale = 2;

            komutUpdate.Parameters.Add("@p6", SqlDbType.Decimal).Value = internet;
            komutUpdate.Parameters["@p6"].Precision = 10;
            komutUpdate.Parameters["@p6"].Scale = 2;

            komutUpdate.Parameters.Add("@p7", SqlDbType.Decimal).Value = maas;
            komutUpdate.Parameters["@p7"].Precision = 10;
            komutUpdate.Parameters["@p7"].Scale = 2;

            komutUpdate.Parameters.Add("@p8", SqlDbType.Decimal).Value = ekstra;
            komutUpdate.Parameters["@p8"].Precision = 10;
            komutUpdate.Parameters["@p8"].Scale = 2;

            komutUpdate.Parameters.Add("@p9", SqlDbType.NVarChar).Value = rchtxtNotlar.Text;
            komutUpdate.Parameters.Add("@p10", SqlDbType.Int).Value = id;

             komutUpdate.ExecuteNonQuery();
            connect.baglanti().Close();
            MessageBox.Show("Gider güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string sil = @"DELETE FROM tbl_Giderler where GiderId=@p1";
                SqlCommand komut = new SqlCommand(sil, connect.baglanti());
                komut.Parameters.AddWithValue("@p1", txtId.Text); 
                komut.ExecuteNonQuery();
                connect.baglanti().Close();

                MessageBox.Show("Personel kaydı silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tabloyu ve kutuları temizlemek için eklemelisin:
                Listele();
                temizle();
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
             
        }
        private void Doldur(DataRow dr)
        {
            if (dr == null) return;

            txtId.Text = dr["GiderId"].ToString();
            cbmAy.Text = dr["GiderAy"].ToString();
            cmbYil.Text = dr["GiderYil"].ToString();

            txtElktrik.Text = dr["GiderElektrik"].ToString();
            txtSu.Text = dr["GiderSu"].ToString();
            txtGaz.Text = dr["GiderDogalgaz"].ToString();
            txtInternet.Text = dr["GiderInternet"].ToString();
            txtMaas.Text = dr["GiderMaaslar"].ToString();
            txtEkstra.Text = dr["GiderEkstra"].ToString();

            rchtxtNotlar.Text = dr["GiderNotlar"].ToString();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(e.FocusedRowHandle);
            if (dr != null)
            {
                Doldur(dr);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.AddExtension = true;
            xtraSaveFileDialog1.Filter = "Pdf (*.pdf)|*.pdf";
            xtraSaveFileDialog1.FileName = "Gider_Liste";
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
            xtraSaveFileDialog1.FileName = "Gider_Liste";
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
