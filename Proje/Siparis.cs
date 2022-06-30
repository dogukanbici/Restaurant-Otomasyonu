using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    public partial class Siparis : Form
    {
        public Siparis()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Button btn = (sender as Button);
            string items = "";

            DataTable dt = VeritabaniBaglanti.VeriGetir("SELECT Adi,Fiyat FROM Urun where UrunGrupNo='" + btn.Tag.ToString() + "'");
            listBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                items = dt.Rows[i][0].ToString() + "-" + dt.Rows[i][1].ToString();
                listBox1.Items.Add(items);

            }
        }
        float fiyat = 0;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            string[] dizi = listBox1.GetItemText(listBox1.SelectedItem).ToString().Split('-');
            fiyat += Convert.ToSingle(dizi[1]);

            listBox2.Items.Add(listBox1.SelectedItem);
            textBox1.Text = fiyat.ToString();
            }
            catch (Exception)
            {

             
            }
          

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private static int listcount = 0;
        object comboValue;
        private void comboDoldur1()
        {
            DataTable dt = VeritabaniBaglanti.VeriGetir("select * from OdemeTuru");
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Adi";
            comboBox1.ValueMember = "OdemeTuruNo";
            comboValue = comboBox1.SelectedValue;
        }
        private void Siparis_Load(object sender, EventArgs e)//DOLU MASADAKİ SİPARİŞLERİ GÖSTERİYOR
        {
            
            Masalar m = new Masalar(); 
            Hesap.siparisNoGetir(m.Masano.ToString(), false).ToString();

            int siparisno = Hesap.siparisNoGetir(m.Masano.ToString(), false);

            ArrayList al = Hesap.urunAdiGetir(siparisno);

            for (int i = 0; i < al.Count; i++)
            {
                listBox2.Items.Add(al[i]);
                fiyat += Convert.ToSingle(al[i].ToString().Split('-')[1]);
            }

            textBox1.Text = fiyat.ToString();
            listcount = listBox2.Items.Count;
            comboDoldur1();

            
        }

        private void button11_Click(object sender, EventArgs e)//ÇIKIŞ DA KAYIT YAPIYOR
        {
            
            if (listBox2.Items.Count != listcount)
            {
                siparisleriKaydet(false);
            }
            this.Close();
            Masalar frm = new Masalar();
            frm.ShowDialog();
        }

        private void siparisleriKaydet(bool hesap_durum)
        {
            int siparisno;
            Masalar m = new Masalar();
            siparisno = Hesap.siparisNoGetir(m.Masano.ToString(), false);
            if (hesap_durum)
            {
                Hesap.hesapOde(siparisno, comboBox1.Text, textBox1.Text);
                Hesap.siparisEkle(m.Masano.ToString(), hesap_durum, siparisno);
                siparisno = Hesap.siparisNoGetir(m.Masano.ToString(), false);
                Hesap.siparisTemizle(siparisno);

            }
            else
            {
                Hesap.siparisEkle(m.Masano.ToString(), hesap_durum, siparisno);
                siparisno = Hesap.siparisNoGetir(m.Masano.ToString(), false);
                for (int i = listcount; i < listBox2.Items.Count; i++)
                {
                    Hesap.siparisUrunEkle(siparisno, Hesap.urunNoGetir(listBox2.Items[i].ToString().Split('-')[0]).ToString(), 1);
                }
            }
      
            m.ShowDialog();
            this.Close();









            #region Onceki


            /*
            if (listBox2.Items.Count > 0)
            {
                if (Hesap.veriVarmi("select * from SiparisDetay where MasaNo=" + masaNo ))
                {
                    int siparisNo = Hesap.siparisNoGetir(masaNo);
                    if (Hesap.siparisUrunTemizle(siparisNo))
                    {
                        foreach (string item in listBox2.Items)
                        {
                            for (int g = 0; g < listBox2.Items.Count; g++)
                            {
                                if (item == listBox2.Items[g].ToString())
                                {
                                    MessageBox.Show(item);





                                    string[] deneme = item.Split('-');
                                    MessageBox.Show(deneme[0].ToString());
                                    if (!Hesap.veriVarmi("select * from Urun u ,SiparisUrun s Where u.Adi="+deneme[0]+" AND s.SiparisNo="+ siparisNo + ""))
                                    //Ürün bir defa eklendiyse bir daha eklemiyor bunun kontrolü
                                    {

                                        int urunAdeti = 0;
                                        for (int i = 0; i < listBox2.Items.Count; i++)
                                        {
                                            if (item == listBox2.Items[i].ToString())
                                            {
                                                urunAdeti++;
                                                //eklenen ürün birden fazla ise sayısını buluyoruz
                                            }
                                        }
                                        if (Hesap.siparisUrunEkle(siparisNo, 9, urunAdeti))
                                        {
                                            //Ürünü ekliyor
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ürünler eklenirken hata!");
                                        }
                                    }

                                }
                                else
                            {

                            }
                            }
                            
                          
                         

                        }


                    }
                }
                else
                {
                    if (Hesap.siparisEkle(masaNo))
                    {
                        int siparisNo = Hesap.siparisNoGetir(masaNo);

                        foreach (string item in listBox2.Items)
                        {

                            for (int g = 0; g < listBox2.Items.Count; g++)
                            {
                                if ( item == listBox2.Items[g].ToString())
                                {
                                    MessageBox.Show(item);

                                    MessageBox.Show("siparis no: "+siparisNo.ToString());

                                    if (!Hesap.veriVarmi("select * from  SiparisUrun s, Urun u where u.Adi=" + item + " AND s.SiparisNo="+ siparisNo))
                                    //Ürün bir defa eklendiyse bir daha eklemiyor bunun kontrolü
                                    {

                                        int urunAdeti = 0;
                                        for (int i = 0; i < listBox2.Items.Count; i++)
                                        {
                                            if (item == listBox2.Items[i].ToString())
                                            {
                                                urunAdeti++;
                                                //eklenen ürün birden fazla ise sayısını buluyoruz
                                            }
                                        }
                                        if (Hesap.siparisUrunEkle(siparisNo, 8, urunAdeti))
                                        {
                                            //Ürünü ekliyor
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ürünler eklenirken hata!");
                                        }
                                    }

                                }
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Siparisler eklenirken hata meydana geldi.");
                    }

                }
            }*/
            #endregion
        }
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.Items.Count > 0 && Convert.ToSingle(textBox3.Text) >= 0 && textBox2.Text.Length >= 0)
                {
                    if (MessageBox.Show("Hesabı Ödemek İstiyor Musunuz?", "Hesap Öde", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {

                        siparisleriKaydet(true);
                        listcount = 0;


                        listBox2.Items.Clear();

                    }
                }
                else
                {
                    MessageBox.Show("Hatalı İşlem Yaptınız");
                }
            }
            catch 
            {

                MessageBox.Show("Lütfen Önce Tutarı Belirtiniz!");
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox3.Text = (Convert.ToSingle(textBox2.Text) - Convert.ToSingle(textBox1.Text)).ToString();

            }
            catch (Exception)
            {
            }
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            pinpad pinpad = new pinpad();
            pinpad.ShowDialog();
            textBox2.Text = pinpad.deger;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
  string[] dizi = listBox2.GetItemText(listBox2.SelectedItem).ToString().Split('-');
         //   MessageBox.Show(dizi[1]);
            fiyat = fiyat-Convert.ToSingle(dizi[1]);

            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            textBox1.Text = fiyat.ToString();
            }
            catch (Exception)
            {

          
            }
          
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int a = this.Width;
            int b = this.Height;
            panel1.Location = new Point((a - panel1.Width) / 2, (b - panel1.Height) / 2);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Masalar m = new Masalar();
           
            Rezervasyon frm = new Rezervasyon(m.Masano.ToString());
            frm.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog onizleme = new PrintPreviewDialog();
            onizleme.Document = printDocument1;
            onizleme.ShowDialog();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Tutar Giriniz.");
            }
            else
            {
                Font fn = new Font("Arial", 12, FontStyle.Regular );
                int fnh = fn.Height;
                int satir = 20;
                int sutun = 200;

                //e.Graphics.DrawString("Müşteri Adı:" + ma + " " + TextBox2.Text, fn, Brushes.Black, 10, satir);
                satir = 2 * fnh + satir;
                e.Graphics.DrawString("-------Örnek Cafe Restaurant-------", fn, Brushes.Black, 10 + 1 * sutun, satir);
                satir = satir + fnh;
                e.Graphics.DrawString("----------------------------------------------", fn, Brushes.Black, 10 + 1 * sutun, satir);
                satir = satir + fnh;
                e.Graphics.DrawString("Kasa Türü: " + comboBox1.Text, fn, Brushes.Black, 10 + 1 * sutun, satir);
                satir = satir + fnh;
                e.Graphics.DrawString("Tarih-Saat: " + DateTime.Now.ToShortDateString(), fn, Brushes.Black, 10 + 1 * sutun, satir);
                e.Graphics.DrawString(" " + DateTime.Now.ToLongTimeString(), fn, Brushes.Black, 10 + 2 * sutun, satir);
                satir = satir + fnh;
                e.Graphics.DrawString("----------------------------------------------", fn, Brushes.Black, 10 + 1 * sutun, satir);
                satir = satir + fnh;
                e.Graphics.DrawString("Urun Adı", fn, Brushes.Black, 10 + 1 * sutun, satir);
                e.Graphics.DrawString("Fiyatı", fn, Brushes.Black, 10 + 2 * sutun, satir);
                satir = satir + fnh;
                e.Graphics.DrawString("----------------------------------------------", fn, Brushes.Black, 10 + 1 * sutun, satir);
                satir = fnh + satir;
                for (int i = 0; i <= listBox2.Items.Count - 1; i++)
                {
                    e.Graphics.DrawString(listBox2.Items[i].ToString().Split('-')[0], fn, Brushes.Black, 10 + 1 * sutun, satir);
                    e.Graphics.DrawString(listBox2.Items[i].ToString().Split('-')[1], fn, Brushes.Black, 10 + 2 * sutun, satir);
                    satir = satir + fnh;
                }
                e.Graphics.DrawString("", fn, Brushes.Black, 10 + 1 * sutun, satir);
                e.Graphics.DrawString("----------------------------------------------", fn, Brushes.Black, 10 + 1 * sutun, satir);
                satir = satir + fnh;
                e.Graphics.DrawString("Toplam Tutar=   ", fn, Brushes.Black, 10 + 1 * sutun, satir);
                e.Graphics.DrawString(textBox1.Text, fn, Brushes.Black, 10 + 2 * sutun, satir);
                satir = satir + fnh;
                e.Graphics.DrawString("Verilen Tutar=   ", fn, Brushes.Black, 10 + 1 * sutun, satir);
                e.Graphics.DrawString(textBox2.Text, fn, Brushes.Black, 10 + 2 * sutun, satir);
                satir = satir + fnh;
                e.Graphics.DrawString("Para Üstü=   ", fn, Brushes.Black, 10 + 1 * sutun, satir);
                e.Graphics.DrawString(textBox3.Text, fn, Brushes.Black, 10 + 2 * sutun, satir);
                satir = satir + fnh;
                
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox3.Text)<0)
            {
                textBox3.BackColor = Color.Red;
            }
            else
            {
                textBox3.BackColor = Color.Green;
                textBox3.ForeColor = Color.Black;
            }
        }
    }
}
        
    


