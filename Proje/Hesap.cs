using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Proje
{
    class Hesap
    {
        public static bool hesapOde(object siparisNo, object odemeTuru, object tutar)
        {
            VeritabaniBaglanti.baglantiKontrol();
            
                SqlCommand Hesap = new SqlCommand("insert into HesapBilgi (SiparisNo,OdemeTuruNo,Tutar,Tarih) values (@siparisNo,@odemeTuruNo,@tutar,@tarih)", VeritabaniBaglanti.con);
                Hesap.Parameters.AddWithValue("@siparisNo", siparisNo);
                Hesap.Parameters.AddWithValue("@odemeTuruNo", odemeTuru);
                Hesap.Parameters.AddWithValue("@tutar", tutar);
                Hesap.Parameters.AddWithValue("@tarih", System.DateTime.Now);
                Hesap.ExecuteNonQuery();
                return true;

        }
               public static List<string> doluMasa()
        {
            List<string> list = new List<string>();
            DataTable dt = VeritabaniBaglanti.VeriGetir("select MasaNo from SiparisDetay  where Hesap= 'false'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add("Masa " +dt.Rows[i][0]);
            }
            list.Add("Tüm Masalar");
            return list;
        }
        public static Boolean  hazir(object urunadi,object masaNo)
        {


            VeritabaniBaglanti.baglantiKontrol();
            
            SqlCommand cmd = new SqlCommand("update SiparisUrun set UrunHazirmi = 'True' where SiparisUrunNo = (select ur.SiparisUrunNo  from SiparisDetay d, SiparisUrun ur where d.MasaNo = @masano and d.Hesap = 'false' and ur.SiparisNo = d.SiparisNo and ur.UrunNo = (select UrunNo from Urun where Adi = @urunadi) and ur.UrunHazirmi = 'false')");
            cmd.Connection =VeritabaniBaglanti.con;
            cmd.Parameters.AddWithValue("@urunadi", urunadi);
            cmd.Parameters.AddWithValue("@masano", masaNo);         
            //TODO: sql command ..
            cmd.ExecuteNonQuery();
            

            return true;
        }
        public static List<string> hazirlanacakliste(string masaNo)
        {
            List<string> list = new List<string>();

            DataTable dt;
            if (masaNo.Split(' ')[1]== "Masalar")
            {
                 dt = VeritabaniBaglanti.VeriGetir(" select u.Adi, d.MasaNo from SiparisDetay d, SiparisUrun ur, Urun u  where d.Hesap= 'false' and ur.SiparisNo= d.SiparisNo and u.UrunNo= ur.UrunNo and ur.UrunHazirmi= 'false'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i][0].ToString()+" -MasaNo- "+ dt.Rows[i][1].ToString()+"");
                }
            }
            else
            {
                dt = VeritabaniBaglanti.VeriGetir(" select u.Adi  from SiparisDetay d, SiparisUrun ur, Urun u  where d.MasaNo=" + masaNo.Split(' ')[1] + " and d.Hesap= 'false' and ur.SiparisNo= d.SiparisNo and u.UrunNo= ur.UrunNo and ur.UrunHazirmi= 'false'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i][0].ToString());
                }
            }
      
            return list;
        }
        public static bool rezervemi(object masano)
        {
            DataTable dt = VeritabaniBaglanti.VeriGetir("select SiparisNo from SiparisDetay where MasaNo=" + masano + "And Rezervasyon=" + 1);
            if (dt.Rows.Count > 0)
            {
                //  int urunNo = Convert.ToInt32(dt.Rows[0][0].ToString());//yeni açılan masanın urun numarasını aldık
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool masarezdrmGetir(object masano)
        {
            DataTable dt = VeritabaniBaglanti.VeriGetir("select SiparisNo from SiparisDetay where MasaNo=" + masano + "And Rezervasyon=" + 1);
            if (dt.Rows.Count > 0)
            {
                //  int urunNo = Convert.ToInt32(dt.Rows[0][0].ToString());//yeni açılan masanın urun numarasını aldık
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool masadrmGetir(object masano)
        {
            DataTable dt = VeritabaniBaglanti.VeriGetir("select SiparisNo from SiparisDetay where MasaNo=" + masano + "And Hesap=" + 0);
            if (dt.Rows.Count > 0)
            {
              //  int urunNo = Convert.ToInt32(dt.Rows[0][0].ToString());//yeni açılan masanın urun numarasını aldık
                return true;
            }
            else
            {
                return false;
            }
        }
        public static ArrayList urunAdiGetir(object siparisno)
        {
            ArrayList  list=new ArrayList();
            DataTable dt = VeritabaniBaglanti.VeriGetir("select UrunNo from SiparisUrun   Where SiparisNo='" + siparisno + "'");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dt1 = VeritabaniBaglanti.VeriGetir("select Adi,Fiyat from Urun   Where UrunNo='" + dt.Rows[i][0].ToString() + "'");

                    if (dt1.Rows.Count > 0)
                    {
                        list.Add(dt1.Rows[0][0].ToString()+"-"+ dt1.Rows[0][1].ToString());
                    }
                   
                }
                return list;
            }
            else
            {
                return list;
            }
        }
        public static int urunNoGetir(object urunAdi)
        {
            DataTable dt = VeritabaniBaglanti.VeriGetir("select UrunNo from Urun  Where Adi='" + urunAdi +"'");
            if (dt.Rows.Count > 0)
            {
                int urunNo = Convert.ToInt32(dt.Rows[0][0].ToString());//yeni açılan masanın urun numarasını aldık
                return urunNo;
            }
            else
            {
                return -1;
            }
        }
        public static int siparisNoGetir(object masaNo,object hesap)
        {
            DataTable dt = VeritabaniBaglanti.VeriGetir("select SiparisNo from SiparisDetay where MasaNo=" + masaNo +"And Hesap="+ 0);
            if (dt.Rows.Count > 0)
            {
                int siparisNo = Convert.ToInt32(dt.Rows[0][0].ToString());//yeni açılan masanın siparis numarasını aldık
                return siparisNo;
            }
            else
            {
                return -1;
            }
        }
        public static bool rezerveiptal(object masaNo )
        {
            VeritabaniBaglanti.baglantiKontrol();

            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from SiparisDetay where MasaNo=@masano and Hesap='false';", VeritabaniBaglanti.con);
                cmd.Parameters.AddWithValue("@masano", masaNo);
               



                //cmd.Parameters.AddWithValue("@hesap", 0);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }


        }

        public static bool rezerve(object masaNo, object durum)
        {
            VeritabaniBaglanti.baglantiKontrol();

            try
            {
                SqlCommand cmd;
                    cmd = new SqlCommand("insert into SiparisDetay (MasaNo,Hesap,Rezervasyon) values (@masano,@hesap,@rezerve)", VeritabaniBaglanti.con);
                    cmd.Parameters.AddWithValue("@masano", masaNo);
                    cmd.Parameters.AddWithValue("@hesap", false);
                    cmd.Parameters.AddWithValue("@rezerve", durum);



                //cmd.Parameters.AddWithValue("@hesap", 0);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }


        }
        public static bool siparisEkle(object masaNo,object hesap,object siparisno)//Siparis tablosuna siparisleri ekliyor
        {
           VeritabaniBaglanti.baglantiKontrol();

           try
            { SqlCommand cmd;
                if ((bool)hesap)
                {
                    cmd = new SqlCommand("update SiparisDetay set Hesap=@hesap where SiparisNo='"+ siparisno+"'", VeritabaniBaglanti.con);
                    cmd.Parameters.AddWithValue("@hesap", hesap);
                }
                else
                {
                    cmd = new SqlCommand("insert into SiparisDetay (MasaNo,Hesap) values (@masano,@hesap)", VeritabaniBaglanti.con);
                    cmd.Parameters.AddWithValue("@masano", masaNo);
                    cmd.Parameters.AddWithValue("@hesap", hesap);
  
                }          

                //cmd.Parameters.AddWithValue("@hesap", 0);
                cmd.ExecuteNonQuery();
              
                return true;
            }
            catch
            {
                return false;
            }


        }
        public static bool siparisUrunEkle( object siparisNo ,object urunNo, object urunAdet)
        {
            VeritabaniBaglanti.baglantiKontrol();
            try
            {
                SqlCommand cmd = new SqlCommand("insert into SiparisUrun (SiparisNo,UrunNo,UrunAdet,UrunHazirmi) values (@siparisno,@urunNo,@urunAdet,@urunhazirmi)", VeritabaniBaglanti.con);
                cmd.Parameters.AddWithValue("@siparisno", siparisNo);
                cmd.Parameters.AddWithValue("@urunNo",urunNo);
                cmd.Parameters.AddWithValue("@urunAdet",urunAdet);
                cmd.Parameters.AddWithValue("@urunhazirmi", false);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }//SiparisUrun tablosuna ekleme yapar

        public static bool siparisUrunTemizle(object siparisNo)
        {
            VeritabaniBaglanti.baglantiKontrol();
            try
            {

                SqlCommand sil = new SqlCommand("delete from SiparisUrun where SiparisNo=@siparisNo", VeritabaniBaglanti.con);
                sil.Parameters.AddWithValue("@siparisNo", siparisNo);
                sil.ExecuteNonQuery();
                return true;

            }
            catch
            {
                return false;
            }
        }//SiparisUrun tablosunda siparisNo ile silme işlemi yapar


        public static bool siparisTemizle(object siparisNo)
        {
            VeritabaniBaglanti.baglantiKontrol();
            try
            {

                SqlCommand sil = new SqlCommand("delete from SiparisDetay where SiparisNo=@siparisNo", VeritabaniBaglanti.con);
                sil.Parameters.AddWithValue("@siparisNo", siparisNo);
                sil.ExecuteNonQuery();
                return true;

            }
            catch
            {
                return false;
            }
        }


        public static bool veriVarmi(string sql)
        {
            SqlDataReader dr;    
            VeritabaniBaglanti.baglantiKontrol();
            SqlCommand cmd = new SqlCommand(sql, VeritabaniBaglanti.con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                return true;
                

            }
            else
            {
                dr.Close();
                return false;
             
            }
        }
        public static string toplamlari(string baslangictrh,string bitistrh)
        {
            SqlCommand cmd = new SqlCommand("select  sum(h.Tutar) toplamtutar  from SiparisDetay s, HesapBilgi h where s.Hesap='true' and s.SiparisNo=h.SiparisNo and h.Tarih between @baslangictarihi and @bitistarihi ", VeritabaniBaglanti.con);
            cmd.Parameters.AddWithValue("@baslangictarihi", baslangictrh);
            cmd.Parameters.AddWithValue("@bitistarihi", bitistrh);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            return dr[0].ToString();
            //dr.Close();

        }

        public static DataTable raporurungetir(string BaslangicTrh, string MasaNo)
        {
            VeritabaniBaglanti.baglantiKontrol();
            DataTable dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand("select u.Adi,su.UrunAdet From SiparisDetay s, SiparisUrun su, Urun u, HesapBilgi h where s.MasaNo =@masano and s.SiparisNo = su.SiparisNo and su.UrunNo = u.UrunNo and s.Hesap = 'true' and h.Tarih =@tarih  and h.SiparisNo = s.SiparisNo", VeritabaniBaglanti.con);

                string[] dtime = BaslangicTrh.Split('.');
                cmd.Parameters.AddWithValue("@masano", MasaNo);
                cmd.Parameters.AddWithValue("@tarih",dtime[1]+"."+dtime[0]+"."+dtime[2]); 
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                return dt;
        }
            catch
            {
                return dt;
            }
}
        public static DataTable rapor(string BaslangicTrh, string BitisTrh, string MasaNo)
        {
            VeritabaniBaglanti.baglantiKontrol();
            DataTable dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand("select s.MasaNo, h.Tarih, h.Tutar   from SiparisDetay s, HesapBilgi h where s.Hesap='true' and s.SiparisNo=h.SiparisNo and h.Tarih between @baslangic and @bitis ",VeritabaniBaglanti.con);
              
                cmd.Parameters.AddWithValue("@baslangic",BaslangicTrh);
                cmd.Parameters.AddWithValue("@bitis", BitisTrh);
                if (MasaNo!="")
                {
                    cmd.CommandText += " and s.MasaNo = @masano";
                    cmd.Parameters.AddWithValue("@masano", MasaNo);
                }

                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                return dt;
        }
            catch
            {
                return dt;
            }
         }//SiparisUrun tablosuna ekleme yapar

    }
}
