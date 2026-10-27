using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Globalization;
using System.Net;
using System.Data.SqlClient;

namespace TicariOtomason
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1(int kullaniciId)
        {
            InitializeComponent();
            this.kullaniciId = kullaniciId;
        }

        public string kullanici;
        public int kullaniciId;
        private void Form1_Load(object sender, EventArgs e)
        {
            KullaniciHareket log = new KullaniciHareket();

            log.LogKaydet(
                kullaniciId,
                this,
                "Giriş",
                "Ana form açıldı"
            );

            DovizleriGetir();
            stoklar();
            ajanda();
            Hareket();
            Firmaİletisim();

            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");

        }

        SqlBaglantisi connect = new SqlBaglantisi();
        void stoklar()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("Select u.UrunAd,SUM(UrunAdet) as Adet from tbl_Urunler as u group by UrunAd Having	Sum(u.UrunAdet) <=50 order by SUM(u.UrunAdet)", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl1.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }
        void ajanda()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("select NotTarih,NotSaat,NotOlusturan,NotBaslik, NotDetay, NotHitap from tbl_Notlar", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl2.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }

        void Hareket()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("select top 10 Tarih, Notlar,Fiyat,Toplam from Tbl_FirmaHareketler", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl3.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }
        void Firmaİletisim()
        {
            DataTable dt = new DataTable();
            // Veritabanından gelen verileri geçici olarak tutacağımız tablo

            SqlDataAdapter da = new SqlDataAdapter("select FirmaAd, FirmaYetkiliAdSoyad, FirmaTelefon1 from tbl_Firmalar", connect.baglanti());
            // tbl_Urunler tablosundaki tüm verileri çekmek için DataAdapter oluşturulur

            da.Fill(dt);
            // Çekilen veriler DataTable içine doldurulur

            gridControl4.DataSource = dt;
            // DataTable gridControl’e bağlanarak veriler ekranda gösterilir
        }




        //FrmUrunler fr; // sınıf düzeyinde 
        private void barBtnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if(fr==null){
            //fr = new FrmUrunler();
            //fr.MdiParent=this; // bu kısım formun içinde açıyor forma xtabbedmianager eklendi
            //fr.Show();
          //}
           FrmUrunler fr = new FrmUrunler();
            fr.Show();
        }

        private void barBtnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmMusteriler musteriler = new FrmMusteriler();
            musteriler.Show();
        }

        private void barBtnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFirmalar firmalar = new FrmFirmalar();
            firmalar.Show();
        }

        private void barBtnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPersoneller personeller = new FrmPersoneller();
            personeller.Show();
        }

        private void barBtnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmRehber rehber = new FrmRehber();
            rehber.Show();
        }

        private void barBtnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmGiderler giderler = new FrmGiderler();
            giderler.Show();
        }

        private void barBtnBanka_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmBankalar bankalar = new FrmBankalar();
            bankalar.Show();
        }

        private void barBtnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFaturalar faturalar = new FrmFaturalar();
            faturalar.Show();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmNotlar Notlar = new FrmNotlar();
            Notlar.Show();
        }

        //HAREKETLER
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmHareketler hareketler = new FrmHareketler();
            hareketler.Show();

        }

        //RAPORLAR
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmRaporlar raporlar = new FrmRaporlar();
            raporlar.Show();

        }

        private void barBtnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmStoklar stoklar = new FrmStoklar();
            stoklar.Show();
        }

        private void barBtnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAyarlar ayarlar = new FrmAyarlar();
            ayarlar.Show();
        }

        private void barBtnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmKasa kasa = new FrmKasa();
            kasa.ad = kullanici;
            kasa.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string kaynakDoviz = CmbDoviz1.Text;
                string hedefDoviz = CmbDoviz2.Text;

                decimal miktar = Convert.ToDecimal(
                    txtMiktar.Text,
                    CultureInfo.CurrentCulture);

                if (miktar <= 0)
                {
                    MessageBox.Show("Lütfen geçerli bir miktar giriniz.");
                    return;
                }

                decimal kaynakKur = KurGetir(kaynakDoviz);
                decimal hedefKur = KurGetir(hedefDoviz);

                // Önce TL'ye çeviriyoruz
                decimal tlKarsiligi = miktar * kaynakKur;

                // TL'den hedef dövize çeviriyoruz
                decimal sonuc = tlKarsiligi / hedefKur;

                lblSonuc.Text = sonuc.ToString("N2") + " " + hedefDoviz;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Hesaplama sırasında hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
        private void DovizleriGetir()
        {
            try
            {
                string adres = "https://www.tcmb.gov.tr/kurlar/today.xml";

                using (WebClient client = new WebClient())
                {
                    string xmlData = client.DownloadString(adres);

                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(xmlData);

                    CmbDoviz1.Properties.Items.Clear();
                    CmbDoviz2.Properties.Items.Clear();

                    // TL
                    CmbDoviz1.Properties.Items.Add("TRY");
                    CmbDoviz2.Properties.Items.Add("TRY");

                    XmlNodeList kurlar = xml.SelectNodes("/Tarih_Date/Currency");

                    foreach (XmlNode kur in kurlar)
                    {
                        string kod = kur.Attributes["CurrencyCode"].Value;

                        CmbDoviz1.Properties.Items.Add(kod);
                        CmbDoviz2.Properties.Items.Add(kod);
                    }

                    CmbDoviz1.SelectedIndex = 0;
                    CmbDoviz2.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Döviz kurları alınamadı.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private decimal KurGetir(string doviz)
        {
            if (doviz == "TRY")
                return 1;

            string adres = "https://www.tcmb.gov.tr/kurlar/today.xml";

            using (WebClient client = new WebClient())
            {
                string xmlData = client.DownloadString(adres);

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlData);

                XmlNode node = xml.SelectSingleNode(
                    "/Tarih_Date/Currency[@CurrencyCode='" + doviz + "']/ForexSelling");

                if (node == null)
                {
                    throw new Exception(doviz + " kuru bulunamadı.");
                }

                return Convert.ToDecimal(
                    node.InnerText.Replace(".", ","),
                    CultureInfo.GetCultureInfo("tr-TR"));
            }
        }
    }
}
