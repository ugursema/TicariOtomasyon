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
using DevExpress.Charts;
using System.IO;

namespace TicariOtomason
{
    public partial class FrmKasa : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmKasa()
        {
            InitializeComponent();
        }
   

        // aktif  kullanıcı 
        public string ad;
        public int kullaniciId;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            lblAktifKullanici.Text = ad;
            MusteriHareket();
            FirmaHareket();
            tutar();
        }
        SqlBaglantisi connect = new SqlBaglantisi();
        void MusteriHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketler", connect.baglanti());

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl2.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }
        void FirmaHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute FirmaHareketler", connect.baglanti());

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl1.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }

        //Toplam tutar hesaplama
        void tutar()
        {
            SqlCommand komut1 = new SqlCommand("Select Sum(Tutar) From tbl_FaturaDetay", connect.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblToplamTutar.Text = dr1[0].ToString() + " ₺";
            }
            connect.baglanti().Close();

            //gider hesaplama
            SqlCommand komut2 = new SqlCommand("Select (GiderElektrik+GiderSu+GiderDogalgaz+GiderInternet+GiderEkstra) From tbl_Giderler order by GiderId asc", connect.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblOdemeler.Text = dr2[0].ToString() + " ₺";
            }
            connect.baglanti().Close();

            //maas toplamları
            SqlCommand komut3 = new SqlCommand("Select SUM(GiderMaaslar) From tbl_Giderler ", connect.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblPersonelMaas.Text = dr3[0].ToString() + " ₺";
            }
            connect.baglanti().Close();

            //müsteri sayısı toplam
            SqlCommand komut4 = new SqlCommand("SELECT COUNT(*) from tbl_Firmalar ", connect.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblMusteriSayisi.Text = dr4[0].ToString();
            }
            connect.baglanti().Close();


            //firma sayısı toplam
            SqlCommand komut5 = new SqlCommand("SELECT COUNT(*) from tbl_Firmalar", connect.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblFirmaSayisi.Text = dr5[0].ToString();
            }
            connect.baglanti().Close();

            //STOK sayısı toplam
            SqlCommand komut6 = new SqlCommand("SELECT SUM(UrunAdet) FROM tbl_Urunler", connect.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblStok.Text = dr6[0].ToString();
            }
            connect.baglanti().Close();

     


        }
        int sayac = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
          
            //Elektrik
                if (sayac > 0 && sayac <= 5)
                {
                    groupControl10.Text = "Elektrik";
                    chartControl1.Series["Aylar"].Points.Clear();
                    SqlCommand komut10 = new SqlCommand("Select top 4 GiderAy,GiderElektrik from tbl_Giderler order by GiderId Desc", connect.baglanti());
                    SqlDataReader dr10 = komut10.ExecuteReader();
                    while (dr10.Read())
                    {
                        chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                    }
                    connect.baglanti().Close();
                }

                //Su
                if (sayac > 5 && sayac <= 10)
                {
                    groupControl10.Text = "Su";
                    chartControl1.Series["Aylar"].Points.Clear();
                    SqlCommand komut11 = new SqlCommand("Select Top 4 GiderAy,GiderSu from tbl_Giderler order by GiderId Desc", connect.baglanti());
                    SqlDataReader dr11 = komut11.ExecuteReader();
                    while (dr11.Read())
                    {
                        chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                    }
                    connect.baglanti().Close();
                }

                //Doğalgaz
                if (sayac > 10 && sayac <= 15)
                {
                    groupControl10.Text = "Doğalgaz";
                    chartControl1.Series["Aylar"].Points.Clear();
                    //Chart Controle Su Faturası Son 4 GiderAy Listeleme
                    SqlCommand komut11 = new SqlCommand("Select Top 4 GiderAy,GiderDogalgaz from tbl_Giderler order by GiderId Desc", connect.baglanti());
                    SqlDataReader dr11 = komut11.ExecuteReader();
                    while (dr11.Read())
                    {
                        chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                    }
                    connect.baglanti().Close();
                }

                //İnternet
                if (sayac > 15 && sayac <= 20)
                {
                    groupControl10.Text = "İnternet";
                    chartControl1.Series["Aylar"].Points.Clear();
                    //Chart Controle Su Faturası Son 4 GiderAy Listeleme
                    SqlCommand komut11 = new SqlCommand("Select Top 4 GiderAy,GiderInternet from tbl_Giderler order by GiderId Desc", connect.baglanti());
                    SqlDataReader dr11 = komut11.ExecuteReader();
                    while (dr11.Read())
                    {
                        chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                    }
                    connect.baglanti().Close();
                }

                //Ekstra
                if (sayac > 20 && sayac <= 25)
                {
                    groupControl10.Text = "Ekstra";
                    chartControl1.Series["Aylar"].Points.Clear();
                    //Chart Controle Su Faturası Son 4 GiderAy Listeleme
                    SqlCommand komut11 = new SqlCommand("Select Top 4 GiderAy,GiderEkstra from tbl_Giderler order by GiderId Desc", connect.baglanti());
                    SqlDataReader dr11 = komut11.ExecuteReader();
                    while (dr11.Read())
                    {
                        chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                    }
                    connect.baglanti().Close();
                }
                if (sayac == 26)
                {
                    sayac = 0;
                }
            }
            int sayac2;

        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;

            //Elektrik
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl11.Text = "Elektrik";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 GiderAy,GiderElektrik from tbl_Giderler order by GiderId Desc", connect.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                connect.baglanti().Close();
            }

            //Su
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl11.Text = "Su";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select Top 4 GiderAy,GiderSu from tbl_Giderler order by GiderId Desc", connect.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                connect.baglanti().Close();
            }

            //Doğalgaz
            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl11.Text = "Doğalgaz";
                chartControl2.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 GiderAy Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 GiderAy,GiderDogalgaz from tbl_Giderler order by GiderId Desc", connect.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                connect.baglanti().Close();
            }

            //İnternet
            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl11.Text = "İnternet";
                chartControl2.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 GiderAy Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 GiderAy,GiderInternet from tbl_Giderler order by GiderId Desc", connect.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                connect.baglanti().Close();
            }

            //Ekstra
            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl11.Text = "Ekstra";
                chartControl2.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 GiderAy Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 GiderAy,GiderEkstra from tbl_Giderler order by GiderId Desc", connect.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                connect.baglanti().Close();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
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
            xtraSaveFileDialog1.FileName = "Kasa_Liste";
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
            xtraSaveFileDialog1.FileName = "Kasa_Liste";
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

