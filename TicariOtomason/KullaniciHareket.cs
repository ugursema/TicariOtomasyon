using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomason
{
    class KullaniciHareket
    {
        SqlBaglantisi connect = new SqlBaglantisi();

        public void LogKaydet(int kullaniciId, Form form, string islem, string aciklama)
        {
            SqlConnection baglan = connect.baglanti();

            try
            {
                string sorgu = @"INSERT INTO tbl_KullaniciHareketleri
                                (KullaniciId, FormAdi, Islem, Tarih, Aciklama)
                                VALUES
                                (@KullaniciId, @FormAdi, @Islem, GETDATE(), @Aciklama)";

                SqlCommand komut = new SqlCommand(sorgu, baglan);

                komut.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                komut.Parameters.AddWithValue("@FormAdi", form.Name);
                komut.Parameters.AddWithValue("@Islem", islem);
                komut.Parameters.AddWithValue("@Aciklama", aciklama);

                komut.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // Loglama sırasında oluşan hata ana uygulamayı durdurmasın.
            }
            finally
            {
                baglan.Close();
            }
        }
    }
}
 