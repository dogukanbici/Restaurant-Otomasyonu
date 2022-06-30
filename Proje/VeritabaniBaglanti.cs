using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Proje
{
    class VeritabaniBaglanti
    {
        public static SqlConnection con = new SqlConnection(@"Data Source=DOĞUKANBICI\SQLEXPRESS;Initial Catalog=RestaurantProje;Integrated Security=True");
        public static void baglantiKontrol()
        {
          
            if (con.State == System.Data.ConnectionState.Closed)
            {
                try { con.Open(); }
                catch
                {
                    
                    System.Windows.Forms.MessageBox.Show("Veri Tabanı Baglantısı yapılamadı.");
                    con.Close();
                }
                
            }
          
        }
        public static DataTable VeriGetir(string sql)
        {
            baglantiKontrol();     
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            return dt;
            

        }

    }
}
