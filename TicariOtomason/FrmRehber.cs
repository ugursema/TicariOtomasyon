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

namespace TicariOtomason
{
    public partial class FrmRehber : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SqlBaglantisi connect = new SqlBaglantisi();

        public FrmRehber()
        {
            InitializeComponent();
        }

        private void FrmRehber_Load(object sender, EventArgs e)
        {
            gridView1.OptionsBehavior.Editable = false;//grid edit özelliği kapatma
            gridView2.OptionsBehavior.Editable = false;//grid edit özelliği kapatma

            verilerMusteri();
            verilerFirmalar();
        }


        void verilerMusteri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT MusteriTC,MusteriAd, MusteriSoyad, MusteriTelefon,MusteriTelefon2,MusteriMail FROM Tbl_Musteriler", connect.baglanti());

            da.Fill(dt);
            gridControl1.DataSource = dt;
            
        }



        void verilerFirmalar()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT  FirmaAd, FirmaYetkiliStatu, FirmaMail, FirmaYetkiliAdSoyad, YetkiliTC, FirmaTelefon1,FirmaTelefon2,FirmaTelefon3 FROM tbl_Firmalar", connect.baglanti());
            da1.Fill(dt1);
            gridControl2.DataSource = dt1;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                frm.mail = dr["MusteriMail"].ToString();
            }
            frm.Show();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                frm.mail = dr["FirmaMail"].ToString();
            }
            frm.Show();
        }
    }
}
