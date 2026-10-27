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
    public partial class FrmAnaSayfa : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        SqlBaglantisi connect = new SqlBaglantisi();
       
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();

        }

        
        void stoklar()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("Select u.UrunAd,SUM(UrunAdet) as Adet from tbl_Urunler as u group by UrunAd Having	Sum(u.UrunAdet) <=20 order by SUM(u.UrunAdet)", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl1.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
