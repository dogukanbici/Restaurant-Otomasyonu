using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Proje
{
    class Urun
    {
        public static bool urunEkle(object adi, object fiyati, object grupNo)
        {
            VeritabaniBaglanti.baglantiKontrol();

            //try
            {
                SqlCommand cmd = new SqlCommand("insert into Urun (Adi,Fiyat,UrunGrupNo) values (@adi,@fiyati,@urungrupno)", VeritabaniBaglanti.con);
                cmd.Parameters.AddWithValue("@adi", adi);
                cmd.Parameters.AddWithValue("@fiyati", fiyati);
                cmd.Parameters.AddWithValue("@urungrupno", grupNo);
                cmd.ExecuteNonQuery();
                return true;
            }
            //catch
            //{
            //    return false;
            //}

        }
        public static bool hazirmi(object urunhazirmi)
        {
            VeritabaniBaglanti.baglantiKontrol();

            try
            {
                SqlCommand cmd = new SqlCommand("update SiparisUrun set UrunHazirmi=@hazir  where UrunNo=@urunno", VeritabaniBaglanti.con);
                cmd.Parameters.AddWithValue("@hazir", urunhazirmi);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static bool urunDuzenle(object urunNo, object fiyat, object grupNo, object urunAdi)
        {
            VeritabaniBaglanti.baglantiKontrol();

            try
            {
                SqlCommand cmd = new SqlCommand("update Urun set Fiyat=@fiyati, UrunGrupNo=@urungrupno, Adi=@adi  where UrunNo=@urunno", VeritabaniBaglanti.con);
                cmd.Parameters.AddWithValue("@fiyati", fiyat);
                cmd.Parameters.AddWithValue("@urungrupno", grupNo);
                cmd.Parameters.AddWithValue("@adi", urunAdi);
                cmd.Parameters.AddWithValue("@urunno", urunNo);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static bool urunSil(object urunNo)
        {
            VeritabaniBaglanti.baglantiKontrol();

            try
            {
                SqlCommand cmd = new SqlCommand("delete Urun  where UrunNo=@urunno", VeritabaniBaglanti.con);
                cmd.Parameters.AddWithValue("@urunno", urunNo);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
