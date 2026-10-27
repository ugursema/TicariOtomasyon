using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomason
{
    public partial class FrmStoklar : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }
        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            Listele();
        }

        SqlBaglantisi connect = new SqlBaglantisi();
       
        void Listele()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("Select u.UrunAd,SUM(UrunAdet) as Adet from tbl_Urunler as u group by UrunAd", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl1.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir

            //charta stok miktarı listeleme**
            SqlCommand komut = new SqlCommand("Select u.UrunAd,SUM(UrunAdet) as Adet from tbl_Urunler as u group by UrunAd", connect.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
               
                chartControl1.Series["Series 1"].Points.AddPoint(dr[0].ToString(), Convert.ToDouble(dr[1]));
                chartControl2.Series["Series 1"].Points.AddPoint(dr[0].ToString(), Convert.ToDouble(dr[1]));
            }
        }

       

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

      
    }
}
