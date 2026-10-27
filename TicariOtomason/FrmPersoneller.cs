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
    public partial class FrmPersoneller : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmPersoneller()
        {
            InitializeComponent();
        }

        SqlBaglantisi connect = new SqlBaglantisi();

        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
            SehirList();
            Listele();
            temizle();
            gridView1.OptionsBehavior.Editable = false;//grid edit özelliği kapatma
                                                       // Grid boş değilse ilk satırı manuel olarak tetikle veya doldur
            if (gridView1.RowCount > 0)
            {
                // FocusedRowChanged bazen Load anından sonra devreye girebilir
                // Garantiye almak için doldurma işlemini bir metoda çıkarıp buradan çağırabilirsin.
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr != null)
                {
                    Doldur(dr); 
                }
            }
        }

        void Listele()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Personeller", connect.baglanti());
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
            txtSoyad.Text = "";
            msktxtTel1.Text = "";
            mksTC.Text = "";
            txtMail.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            rchtxtAdres.Text = "";
            txtGorev.Text = "";
            txtAd.Focus();

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

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string ekle = @"INSERT INTO tbl_Personeller ( PersonelAd, PersonelSoyad, PersonelTelefon,PersonelTC, PersonelMail, PersonelIl, PersonelIlce, PersonelAdres,PersonelGörev) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)";
            SqlCommand komutadd = new SqlCommand(ekle, connect.baglanti());
            komutadd.Parameters.AddWithValue("@p1", txtAd.Text);
            komutadd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komutadd.Parameters.AddWithValue("@p3", msktxtTel1.Text);
            komutadd.Parameters.AddWithValue("@p4", mksTC.Text);
            komutadd.Parameters.AddWithValue("@p5", txtMail.Text);
            komutadd.Parameters.AddWithValue("@p6", cmbil.Text);
            komutadd.Parameters.AddWithValue("@p7", cmbilce.Text);
            komutadd.Parameters.AddWithValue("@p8", rchtxtAdres.Text);
            komutadd.Parameters.AddWithValue("@p9", txtGorev.Text);
            komutadd.ExecuteNonQuery();
            connect.baglanti().Close();
            MessageBox.Show("Personel eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            temizle();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string guncelle = @"UPDATE tbl_Personeller 
                    SET PersonelAd=@p1, 
                        PersonelSoyad=@p2, 
                        PersonelTelefon=@p3, 
                        PersonelTC=@p4, 
                        PersonelMail=@p5, 
                        PersonelIl=@p6, 
                        PersonelIlce=@p7, 
                        PersonelAdres=@p8, 
                        PersonelGörev=@p9 
                    WHERE PersonelId=@p10"; SqlCommand komutUpdate = new SqlCommand(guncelle, connect.baglanti());
            komutUpdate.Parameters.AddWithValue("@p1", txtAd.Text);
            komutUpdate.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komutUpdate.Parameters.AddWithValue("@p3", msktxtTel1.Text);
            komutUpdate.Parameters.AddWithValue("@p4", mksTC.Text);
            komutUpdate.Parameters.AddWithValue("@p5", txtMail.Text);
            komutUpdate.Parameters.AddWithValue("@p6", cmbil.Text);
            komutUpdate.Parameters.AddWithValue("@p7", cmbilce.Text);
            komutUpdate.Parameters.AddWithValue("@p8", rchtxtAdres.Text);
            komutUpdate.Parameters.AddWithValue("@p9", txtGorev.Text);
            komutUpdate.Parameters.AddWithValue("@p10", txtId.Text);
            komutUpdate.ExecuteNonQuery();
            connect.baglanti().Close();
            MessageBox.Show("Personel güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                 string sil = @"DELETE FROM tbl_Personeller WHERE PersonelId = @p1";

                SqlCommand komut = new SqlCommand(sil, connect.baglanti());
                komut.Parameters.AddWithValue("@p1", txtId.Text);
                komut.ExecuteNonQuery();
                connect.baglanti().Close();

                MessageBox.Show("Personel kaydı silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                 Listele();
                temizle();
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Verileri kontrollere basan ortak metot
        private void Doldur(DataRow dr)
        {
            txtId.Text = dr["PersonelId"].ToString();
            txtAd.Text = dr["PersonelAd"].ToString();
            txtSoyad.Text = dr["PersonelSoyad"].ToString();
            msktxtTel1.Text = dr["PersonelTelefon"].ToString();
            mksTC.Text = dr["PersonelTC"].ToString();
            txtMail.Text = dr["PersonelMail"].ToString();
            cmbil.Text = dr["PersonelIl"].ToString();
            cmbilce.Text = dr["PersonelIlce"].ToString();
            rchtxtAdres.Text = dr["PersonelAdres"].ToString();
            txtGorev.Text = dr["PersonelGörev"].ToString(); //  
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

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.AddExtension = true;
            xtraSaveFileDialog1.Filter = "Pdf (*.pdf)|*.pdf";
            xtraSaveFileDialog1.FileName = "Personel_Liste";
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
            xtraSaveFileDialog1.FileName = "Personel_Liste";
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
        //private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    //DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
        //    //if (dr != null)
        //    //{
        //    //    txtId.Text = dr["PersonelId"].ToString();
        //    //    txtAd.Text = dr["PersonelAd"].ToString();
        //    //    txtSoyad.Text = dr["PersonelSoyad"].ToString();
        //    //    msktxtTel1.Text = dr["PersonelTelefon"].ToString();
        //    //    mksTC.Text = dr["PersonelTC"].ToString();
        //    //    txtMail.Text = dr["PersonelMail"].ToString();
        //    //    cmbil.Text = dr["PersonelIl"].ToString();
        //    //    cmbilce.Text = dr["PersonelIlce"].ToString();
        //    //    rchtxtAdres.Text = dr["PersonelAdres"].ToString();
        //    //    txtGorev.Text = dr["PersonelGörev"].ToString(); 

        //    //}
        //}
    }
}
