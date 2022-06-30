using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Proje
{
    class Kontrol
    {
        public static string kisi;
        public static bool kullaniciKontrol(string sifre)
        {
            VeritabaniBaglanti.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("SELECT kullaniciNo,Adi,Soyadi,Ünvan FROM KullaniciBilgi Where Sifre=@sifre", VeritabaniBaglanti.con);
            //cmd.Parameters.AddWithValue("@kuladi", kulAdi);
            cmd.Parameters.AddWithValue("@sifre", sifre);
            SqlDataReader dr = cmd.ExecuteReader();
           
            if (dr.Read())
            {
                kisi = dr[1].ToString()+"  "+dr[2].ToString()+"  "+dr[3].ToString();
                dr.Close();
                cmd.Connection.Close();          
                return true;
                
            }
            else {
                return false;
            }
        }
        public static bool KullaniciEkle(object KullaniciAdi, object Sifre, object Adi, object Soyadi , object Unvan)//ilk kısımda kullaniciekle için kullandık.
        {
            VeritabaniBaglanti.baglantiKontrol();
           
            {
                SqlCommand cmd = new SqlCommand("insert into KullaniciBilgi (KullaniciAdi,Sifre,Adi,Soyadi,Ünvan) values (@KullaniciAdi,@Sifre,@Adi,@Soyadi,@unvan)", VeritabaniBaglanti.con);
                cmd.Parameters.AddWithValue("@KullaniciAdi", KullaniciAdi);
                cmd.Parameters.AddWithValue("@Sifre", Sifre);
                cmd.Parameters.AddWithValue("@Adi", Adi);
                cmd.Parameters.AddWithValue("@Soyadi", Soyadi);
                cmd.Parameters.AddWithValue("@unvan", Unvan);
                cmd.ExecuteNonQuery();
                return true;
            }
            
        }
        public static bool KasaEkle(object Adi, object KasaAdi)
        {
            VeritabaniBaglanti.baglantiKontrol();

            {
                SqlCommand cmd = new SqlCommand("insert into OdemeTuru (Adi,KasaAdi) values (@Adi,@KasaAdi)", VeritabaniBaglanti.con);
                cmd.Parameters.AddWithValue("@Adi", Adi);
                cmd.Parameters.AddWithValue("@KasaAdi", KasaAdi);
                cmd.ExecuteNonQuery();
                return true;
            }

        }
    }
}
