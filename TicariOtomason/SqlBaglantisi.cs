using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace TicariOtomason
{
    //    class SqlBaglantisi
    //    {
    //        public SqlConnection baglanti()
    //        {
    //            SqlConnection baglan = new SqlConnection(@"Data Source=localhost;Initial Catalog=TicariOtomasyon;Integrated Security=True");
    //            baglan.Open();
    //            return baglan;
    //        }
    //    }
    //}
    //class SqlBaglantisi
    //{
    //    public SqlConnection baglanti()
    //    {
    //        SqlConnection baglan = new SqlConnection(
    //            @"Server=tcp:ticariotomasyon2026.database.windows.net,1433;
    //          Initial Catalog=TicariOtomasyonPC2;
    //          User ID=TicariOtomasyonAdmin;
    //          Password=;
    //          Encrypt=True;
    //          TrustServerCertificate=False;
    //          Connection Timeout=30;"
    //        );

    //        baglan.Open();
    //        return baglan;
    //    }
    //}
  

    class SqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            string connectionString =
                ConfigurationManager
                .ConnectionStrings["AzureTicariOtomasyon"]
                .ConnectionString;

            SqlConnection baglan = new SqlConnection(connectionString);
            baglan.Open();

            return baglan;
        }
    }
}
