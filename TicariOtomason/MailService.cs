using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
 


namespace TicariOtomason
{
    class MailService
    {
        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string SmtpUser = "sema1907.su@gmail.com";
        private const string SmtpPass = "xbtl xwlg omve sdns";

        public void SendMail(MailType type, string toEmail, int? belgeId = null)
        {
            string subject = "";
            string body = "";
            switch (type)
            {
                case MailType.Fatura:
                    subject = "Fatura Bilgilendirmesi";
                    body = GenerateFaturaBody(0, belgeId ?? 0);
                    break;

                case MailType.Ekstre:
                    subject = "Cari Hesap Ekstresi";
                    body = GenerateEkstreBody(0);
                    break;

                case MailType.TerfiKutlama:   //  
                    subject = "Terfin Kutlu Olsun ❤️";
                    body = GenerateKutlamaBody("Aşkım");
                    break;

                default:
                    subject = "Bilgilendirme";
                    body = "<p>Test mail içeriği</p>";
                    break;
            }


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(SmtpUser, "Cari Otomasyon");
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(SmtpServer, SmtpPort);
            smtp.Credentials = new NetworkCredential(SmtpUser, SmtpPass);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

        private string GenerateFaturaBody(int cariId, int belgeId)
        {
            return $"<h2>Fatura Maili</h2><p>CariId: {cariId} - BelgeId: {belgeId}</p>";
        }

        private string GenerateEkstreBody(int cariId)
        {
            return $"<h2>Ekstre Maili</h2><p>CariId: {cariId}</p>";
        }

        //private string GetCariEmail(int cariId)
        //{
        //    // Şimdilik test için sabit mail döndürüyoruz
        //    return "gonderilecekmail@gmail.com";

        //    // İleride:
        //    // DB’den cariId’ye göre mail çekilecek
        //}
        private string GenerateKutlamaBody(string isim)
        {
            return @"
    <html>
    <body style='margin:0; padding:0; background-color:#f6f8fb; font-family:Arial, Helvetica, sans-serif;'>

        <table width='100%' cellpadding='0' cellspacing='0' style='padding:30px 0;'>
            <tr>
                <td align='center'>

                    <table width='600' cellpadding='0' cellspacing='0' style='background-color:#ffffff; border-radius:10px; overflow:hidden;'>

                        <!-- HEADER -->
                        <tr>
                            <td style='background:linear-gradient(90deg,#7c3aed,#ec4899); padding:30px; text-align:center;'>
                                <h1 style='color:white; margin:0; font-size:26px;'>🎉 Tebrikler!</h1>
                            </td>
                        </tr>

                        <!-- BODY -->
                        <tr>
                            <td style='padding:40px;'>

                                <h2 style='color:#1f2937; margin-top:0;'>
                                    Sevgili " + isim + @",
                                </h2>

                                <p style='font-size:16px; color:#374151; line-height:1.6;'>
                                    Başarınla gurur duyuyorum. Bu terfi, emeğinin, disiplininin 
                                    ve azminin bir sonucu.
                                </p>

                                <p style='font-size:16px; color:#374151; line-height:1.6;'>
                                    Her zaman daha iyisini hak ediyorsun ve bugün bunun bir kanıtı.
                                </p>

                                <div style='margin:30px 0; padding:20px; background-color:#f3f4f6; border-radius:8px; text-align:center;'>
                                    <strong style='font-size:18px; color:#7c3aed;'>
                                        Yeni pozisyonun sana başarı ve mutluluk getirsin ❤️
                                    </strong>
                                </div>

                                <p style='font-size:15px; color:#6b7280;'>
                                    Seninle gurur duyuyorum ve her zaman yanındayım.
                                </p>

                                <br>

                                <p style='font-size:14px; color:#9ca3af;'>
                                    Bu mail sana kalpten gönderildi 💌
                                </p>

                            </td>
                        </tr>

                        <!-- FOOTER -->
                        <tr>
                            <td style='background-color:#f3f4f6; padding:20px; text-align:center; font-size:12px; color:#9ca3af;'>
                                Özel bir kutlama mesajıdır.
                            </td>
                        </tr>

                    </table>

                </td>
            </tr>
        </table>

    </body>
    </html>";
        }

    }
}