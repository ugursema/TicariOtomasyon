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
    public partial class FrmFaturalar : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SqlBaglantisi connect = new SqlBaglantisi();
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            Listele();
            gridView1.OptionsBehavior.Editable = false;//grid edit özelliği kapatma
            temizle();
            FaturaListele();
          //  MessageBox.Show("ID = " + txtId.Text);

        }
        void Listele()
        {

            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_FaturaBilgi", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl1.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }
        void temizle()
        {
            txtId.Text = "";
            txtSeri.Text = "";
            txtSiraNo.Text = "";
            mskTarih.Text = "";
            mskSaat.Text = "";
            txtVergi.Text = "";
            txtTeslimEden.Text = "";
            txtTeslimAlan.Text = ""; 
        }


        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if(txtId.Text =="")
            {
                SqlCommand komut = new SqlCommand("INSERT INTO tbl_FaturaBilgi (FaturaSeri, FaturaSıraNo, FaturaTarihi, FaturaSaat,FaturaVergiDaire, FaturaAlıcı, FaturaTeslimEden, FaturaTeslimAlan) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)", connect.baglanti());
                komut.Parameters.AddWithValue("@p1", txtSeri.Text);
                komut.Parameters.AddWithValue("@p2", txtSiraNo.Text); 
                //komut.Parameters.AddWithValue("@p3", mskTarih.Text);
                //komut.Parameters.AddWithValue("@p4", mskSaat.Text);
                // saat ve tarih otomatik alınsın
                komut.Parameters.AddWithValue("@p3", DateTime.Now.ToString("dd.MM.yyyy")); // Güncel Tarih
                komut.Parameters.AddWithValue("@p4", DateTime.Now.ToString("HH:mm"));       // Güncel Saat
                komut.Parameters.AddWithValue("@p5", txtVergi.Text);
                komut.Parameters.AddWithValue("@p6", txtAlici.Text);
                komut.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
                komut.Parameters.AddWithValue("@p8", txtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                connect.baglanti().Close();
                MessageBox.Show("Fatura eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
            }
            else if(txtId.Text !="")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(txtFiyat.Text);
                miktar = Convert.ToDouble(txtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();
                SqlCommand komut2 = new SqlCommand("INSERT INTO tbl_FaturaDetay (UrunAd, UrunMiktar, UrunFiyat, Tutar, FaturaId) VALUES(@p1, @p2, @p3, @p4, @p5)", connect.baglanti());
                komut2.Parameters.AddWithValue("@p1", txtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", txtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", txtFiyat.Text);
                komut2.Parameters.AddWithValue("@p4", TxtTutar.Text);
                komut2.Parameters.AddWithValue("@p5", cmbId.SelectedValue);
                komut2.ExecuteNonQuery();
                connect.baglanti().Close();
                MessageBox.Show("Urun Fatura eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
            }
        }

        void FaturaListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select FaturaBilgiId, FaturaBilgiId From tbl_FaturaBilgi", connect.baglanti());
            da.Fill(dt);

            // 1. Veri kaynağını belirle
            cmbId.DataSource = dt;

            // 2. Kullanıcının göreceği sütun (İsim)
            //cmbId.DisplayMember = "AD";
             cmbId.DisplayMember = "FaturaBilgiId";
            cmbId.ValueMember = "FaturaBilgiId";

            // 3. Arka planda tutulacak değer (ID)
            //  cmbId.ValueMember = "ID";
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            string guncelle = @"UPDATE tbl_FaturaBilgi SET  
    FaturaSeri=@p1, 
    FaturaSıraNo=@p2, 
    FaturaTarihi=@p3, 
    FaturaSaat=@p4, 
    FaturaVergiDaire=@p5, 
    FaturaAlıcı=@p6,
    FaturaTeslimEden=@p7, 
    FaturaTeslimAlan=@p8  
    WHERE FaturaBilgiId=@p9";

            SqlCommand komut = new SqlCommand(guncelle, connect.baglanti());

            komut.Parameters.AddWithValue("@p1", txtSeri.Text);
            komut.Parameters.AddWithValue("@p2", txtSiraNo.Text);
            komut.Parameters.AddWithValue("@p3", mskTarih.Text); 
            komut.Parameters.AddWithValue("@p4", mskSaat.Text);
            komut.Parameters.AddWithValue("@p5", txtVergi.Text);
            komut.Parameters.AddWithValue("@p6", txtAlici.Text);
            komut.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
            komut.Parameters.AddWithValue("@p8", txtTeslimAlan.Text);
            komut.Parameters.AddWithValue("@p9", txtId.Text);  

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
                SqlCommand komut1 = new SqlCommand("Delete From tbl_FaturaBilgi where FaturaBilgiId=@p1", connect.baglanti());
                komut1.Parameters.AddWithValue("@p1", txtId.Text);
                komut1.ExecuteNonQuery();
                connect.baglanti().Close();

                MessageBox.Show("Fatura silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(e.FocusedRowHandle);
            if (dr != null)
            {
                Doldur(dr);
            }
        }
        private void Doldur(DataRow dr)
        {  
            txtId.Text = dr["FaturaBilgiId"].ToString();
            txtSeri.Text = dr["FaturaSeri"].ToString();
            txtSiraNo.Text = dr["FaturaSıraNo"].ToString();
            mskTarih.Text = dr["FaturaTarihi"].ToString();
            mskSaat.Text = dr["FaturaSaat"].ToString();
            txtVergi.Text = dr["FaturaVergiDaire"].ToString();
            txtAlici.Text = dr["FaturaAlıcı"].ToString();
            txtTeslimEden.Text = dr["FaturaTeslimEden"].ToString();
            txtTeslimAlan.Text = dr["FaturaTeslimAlan"].ToString();  
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay frmFaturaUrun = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            
            if(dr != null)
            {
                frmFaturaUrun.id = dr["FaturaBilgiId"].ToString();
                frmFaturaUrun.Show();
            }
        }

        private void ribbonStatusBar1_Click(object sender, EventArgs e)
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
            xtraSaveFileDialog1.FileName = "Fatura_Liste";
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
            xtraSaveFileDialog1.FileName = "Fatura_Liste";
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
