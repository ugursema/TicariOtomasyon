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
    public partial class FrmBankalar : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SqlBaglantisi connect = new SqlBaglantisi();


        public FrmBankalar()
        {
            InitializeComponent();
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            Listele();
            SehirList();
            FirmaList();
        }
        void Listele()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("execute BankBilgileri", connect.baglanti());
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

        void FirmaList()
        {
            //SqlCommand firma = new SqlCommand("select FirmaAd from tbl_Firmalar", connect.baglanti());
            //SqlDataReader dr = firma.ExecuteReader();
            //while (dr.Read())
            //{
            //    cmbFirma.Properties.Items.Add(dr[0]);
            //}
            //connect.baglanti().Close();

            SqlDataAdapter da = new SqlDataAdapter("SELECT FirmaId, FirmaAd FROM tbl_Firmalar", connect.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);

            lookUpEditFirma.Properties.DataSource = dt;
            lookUpEditFirma.Properties.DisplayMember = "FirmaAd";
            lookUpEditFirma.Properties.ValueMember = "FirmaId";

            if (dt.Rows.Count > 0)
                lookUpEditFirma.EditValue = dt.Rows[0]["FirmaId"];  // ilk değer seçili gelsin

            connect.baglanti().Close();


        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string ekle = @"INSERT INTO tbl_Bankalar 
    (BankaAd, BankaSube, BankaIl, BankaIlce, Iban, HesapNo, 
     BankaYetkili, BankaTelefon, Tarih, HesapTürü, Firma_id) 
    VALUES 
    (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)";

            SqlCommand komutadd = new SqlCommand(ekle, connect.baglanti());

            komutadd.Parameters.AddWithValue("@p1", txtAd.Text);
            komutadd.Parameters.AddWithValue("@p2", txtSube.Text);
            komutadd.Parameters.AddWithValue("@p3", cmbil.Text);
            komutadd.Parameters.AddWithValue("@p4", cmbilce.Text);
            komutadd.Parameters.AddWithValue("@p5", txtIBAN.Text);
            komutadd.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            komutadd.Parameters.AddWithValue("@p7", txtYetkili.Text);
            komutadd.Parameters.AddWithValue("@p8", mskTel.Text);
            komutadd.Parameters.AddWithValue("@p9", mskTarih.Text);
            komutadd.Parameters.AddWithValue("@p10", txtHesapTürü.Text);

            komutadd.Parameters.AddWithValue("@p11", lookUpEditFirma.EditValue);
            komutadd.ExecuteNonQuery();
            connect.baglanti().Close();

            MessageBox.Show("Banka eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
                string guncelle = @"UPDATE tbl_Bankalar SET  
    BankaAd=@p1, 
    BankaSube=@p2, 
    BankaIl=@p3, 
    BankaIlce=@p4, 
    Iban=@p5, 
    HesapNo=@p6,
    BankaYetkili=@p7, 
    BankaTelefon=@p8, 
    Tarih=@p9,
    HesapTürü=@p10,
    Firma_id=@p11 
    WHERE BankaId=@p12";

                SqlCommand komut = new SqlCommand(guncelle, connect.baglanti());

                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSube.Text);
                komut.Parameters.AddWithValue("@p3", cmbil.Text);
                komut.Parameters.AddWithValue("@p4", cmbilce.Text);
                komut.Parameters.AddWithValue("@p5", txtIBAN.Text);
                komut.Parameters.AddWithValue("@p6", txtHesapNo.Text);
                komut.Parameters.AddWithValue("@p7", txtYetkili.Text);
                komut.Parameters.AddWithValue("@p8", mskTel.Text);
                komut.Parameters.AddWithValue("@p9", mskTarih.Text);
                komut.Parameters.AddWithValue("@p10", txtHesapTürü.Text);

                // Firma null kontrolü
                if (lookUpEditFirma.EditValue == null)
                    komut.Parameters.AddWithValue("@p11", DBNull.Value);
                else
                    komut.Parameters.AddWithValue("@p11", lookUpEditFirma.EditValue);

                 komut.Parameters.AddWithValue("@p12", txtId.Text);

                komut.ExecuteNonQuery();
                connect.baglanti().Close();

                MessageBox.Show("Banka başarıyla güncellendi.",
                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Listele();
            

        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Banka kaydı silinsin mi?",
                                                "Uyarı",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning);
            if (cevap == DialogResult.Yes)
            {
                string sil = @"DELETE FROM tbl_Bankalar WHERE BankaId = @p1";

                SqlCommand komut = new SqlCommand(sil, connect.baglanti());
                komut.Parameters.AddWithValue("@p1", txtId.Text);
                komut.ExecuteNonQuery();
                connect.baglanti().Close();

                MessageBox.Show("Banka kaydı silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Listele();
               
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {

        }

      

        // Verileri kontrollere basan ortak metot (Banka için)
        private void Doldur(DataRow dr)
        {
            txtId.Text = dr["BankaId"].ToString();
            txtAd.Text = dr["BankaAd"].ToString();
            txtSube.Text = dr["BankaSube"].ToString();
            cmbil.Text = dr["BankaIl"].ToString();
            cmbilce.Text = dr["BankaIlce"].ToString();
            txtIBAN.Text = dr["Iban"].ToString();
            txtHesapNo.Text = dr["HesapNo"].ToString();
            txtYetkili.Text = dr["BankaYetkili"].ToString();
            mskTel.Text = dr["BankaTelefon"].ToString();
            mskTarih.Text = dr["Tarih"].ToString();
            txtHesapTürü.Text = dr["HesapTürü"].ToString();

            // Firma (LookUpEdit kullanıyorsan)
            if (dr["Firma_id"] != DBNull.Value)
                lookUpEditFirma.EditValue = dr["Firma_id"];
            else
                lookUpEditFirma.EditValue = null;
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(e.FocusedRowHandle);
            if (dr != null)
            {
                Doldur(dr);
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
            xtraSaveFileDialog1.FileName = "Banka_Liste";
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
            xtraSaveFileDialog1.FileName = "Banka_Liste";
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
