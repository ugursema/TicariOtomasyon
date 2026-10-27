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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        SqlBaglantisi connect = new SqlBaglantisi();

        public string kullanici;


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand ("Select* from tbl_Admin where KullaniciAd=@p1 and KullaniciSifre=@p2", connect.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            //if (dr.Read())
            //{
            //    Form1 form1 = new Form1();
            //    form1.kullanici = txtAd.Text;
            //    form1.Show();
            //    this.Hide();

            //}
            if (dr.Read())
            {
                int kullaniciId = Convert.ToInt32(dr["KullaniciId"]);

                Form1 form1 = new Form1(kullaniciId);
                form1.kullanici = txtAd.Text;
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı ya da Sifre", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            connect.baglanti().Close();
        }

        //mouse buton üzerine gelince oluşan renk
        private void simpleButton1_MouseHover(object sender, EventArgs e)
        {
            BtnGirisYap.Appearance.BackColor = Color.Yellow;
        }

        //mouse buton üzerinden ayrılınca oluşan renk
        private void simpleButton1_MouseLeave(object sender, EventArgs e)
        {
            BtnGirisYap.Appearance.BackColor = Color.White;
        }
    }
}
