using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomason
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }

        public string mail;
        private void FrmMail_Load(object sender, EventArgs e)
        {
            label1.Text = mail;
        }

        private void btnMailTest_Click(object sender, EventArgs e)
        {
            MailService mailService = new MailService();

            try
            {
                mailService.SendMail(MailType.TerfiKutlama, label1.Text);

                //mailService.SendMail(MailType.Ekstre, label1.Text);
                MessageBox.Show("Mail gönderildi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

    }
}
