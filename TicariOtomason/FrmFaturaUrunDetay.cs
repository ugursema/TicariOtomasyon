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
    public partial class FrmFaturaUrunDetay : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public string id;
        SqlBaglantisi connect = new SqlBaglantisi();
        public FrmFaturaUrunDetay()
        {
            InitializeComponent();
        }

        private void FrmFaturaUrunDetay_Load(object sender, EventArgs e)
        {
            gridView1.OptionsBehavior.Editable = false;//grid edit özelliği kapatma

           // labelControl1.Text = id;
            Listele();
        }

        void Listele()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_FaturaDetay where FaturaId='"+ id + "'" , connect.baglanti()); 
            da.Fill(dt); 
            gridControl1.DataSource = dt; 
        }

        void temizle()
        {
            //txtId.Text = "";
            //txtSeri.Text = "";
            //txtSiraNo.Text = "";
            //mskTarih.Text = "";
            //mskSaat.Text = "";
            //txtVergi.Text = "";
            //txtTeslimEden.Text = "";
            //txtTeslimAlan.Text = "";
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDuzenleme faturaUrunDuzenleme = new FrmFaturaUrunDuzenleme();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr !=null)
            {
                faturaUrunDuzenleme.urunId = dr["FaturaUrunId"].ToString();
            }
            faturaUrunDuzenleme.Show();
            //this.Hide();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            gridControl1.ShowRibbonPrintPreview();

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            xtraSaveFileDialog1.AddExtension = true;
            xtraSaveFileDialog1.Filter = "Pdf (*.pdf)|*.pdf";
            xtraSaveFileDialog1.FileName = "FaturaDetay_Liste";
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
            xtraSaveFileDialog1.FileName = "FaturaDetay_Liste";
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
