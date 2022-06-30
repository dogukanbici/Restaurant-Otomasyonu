using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proje
{
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }
        object comboValue;
        
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
            {

                if (textBox1.Text == "")
                {
                    MessageBox.Show("Lütfen Ürün Adı Girin!");
                }
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Lütfen Ürün Fiyatı Girin!");
                }
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Lütfen Grup Seçiniz!");
                }
            }
            else
            {
                if (Hesap.veriVarmi("Select Adi from Urun where Adi='" + textBox1.Text + "'"))
                {
                    label7.Text = "Böyle bir ürün vardır.";
                }

                if (Urun.urunEkle(textBox1.Text, textBox2.Text, comboValue))
                    {
                    comboBox1_SelectedIndexChanged(comboBox1, e);
                        textBox1.Clear();
                        textBox2.Clear();                                                
                        label7.ForeColor = Color.Green;
                        label7.Text = "Ürün Başarıyla Eklenmiştir.";
                    }
                    else
                    {
                   
                        textBox1.Clear();
                        textBox2.Clear();
                        label7.ForeColor = Color.Red;
                        label7.Text = "Ürün Eklenirken Hata!";
                    }
                }
            }
        private void comboDoldur()
        {
            DataTable dt = VeritabaniBaglanti.VeriGetir("select * from UrunGrup");
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Adi";
            comboBox1.ValueMember = "UrunGrupNo";
            comboValue = comboBox1.SelectedValue;
        }
        private void comboDoldur2()
        {
            DataTable dt = VeritabaniBaglanti.VeriGetir("select * from UrunGrup");
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "Adi";
            comboBox2.ValueMember = "UrunGrupNo";
            comboValue = comboBox2.SelectedValue;
        }


        private void Mutfak_Load(object sender, EventArgs e)
        {
            comboDoldur();
            comboDoldur2();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboValue = comboBox1.SelectedValue;
            DataTable dt = VeritabaniBaglanti.VeriGetir("SELECT Adi,Fiyat FROM Urun where UrunGrupNo='" + (comboBox1.SelectedIndex + 1).ToString() + "'");
            listBox2.Items.Clear();
            string items = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                items = dt.Rows[i][0].ToString() + "-" + dt.Rows[i][1].ToString();
                listBox2.Items.Add(items);
            }
        }

        private void comboBox1_Click_1(object sender, EventArgs e)
        {
            DataTable dt = VeritabaniBaglanti.VeriGetir("select * from UrunGrup");
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Adi";
            comboBox1.ValueMember = "UrunGrupNo";
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
            this.Close();
        }
        private void temizle()
        {
            textBox3.Clear();
            textBox4.Clear();
            
        }
        int UrunNo = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "" || comboBox2.Text == "")
            {
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Lütfen Ürün Adı Girin!");
                }
                if (textBox4.Text == "")
                {
                    MessageBox.Show("Lütfen Ürün Fiyatı Girin!");
                }
                if (comboBox2.Text == "")
                {
                    MessageBox.Show("Lütfen Grup Seçiniz!");
                }

            }
            else
            {

                if (Urun.urunDuzenle(UrunNo, textBox4.Text, comboValue, textBox3.Text))
                {
                    temizle();
                    label8.ForeColor = Color.Green;
                    label8.Text = "Ürün Başarıyla Düzenlendi.";
                    comboBox2_SelectedIndexChanged(comboBox2, e);
                }
                else
                {

                    temizle();
                    label8.ForeColor = Color.Red;
                    label8.Text = "Ürün Düzenlenirken Hata!";
                }




            }
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboValue = comboBox2.SelectedValue;
            DataTable dt = VeritabaniBaglanti.VeriGetir("SELECT Adi,Fiyat FROM Urun where UrunGrupNo='" + (comboBox2.SelectedIndex+1).ToString()+ "'");
             listBox1.Items.Clear();
            string items = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
              

                items = dt.Rows[i][0].ToString() + "-" + dt.Rows[i][1].ToString();
                listBox1.Items.Add(items);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] dizi = listBox1.GetItemText(listBox1.SelectedItem).ToString().Split('-');
                textBox3.Text = dizi[0];
                textBox4.Text = dizi[1];
                UrunNo = Hesap.urunNoGetir(dizi[0]);
            }
            catch (Exception)
            {

            }
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "" || comboBox2.Text == "")
            {
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Lütfen Ürün Adı Girin!");
                }
                if (textBox4.Text == "")
                {
                    MessageBox.Show("Lütfen Ürün Fiyatı Girin!");
                }
                if (comboBox2.Text == "")
                {
                    MessageBox.Show("Lütfen Grup Seçiniz!");
                }
                else
                {
                    if (Urun.urunSil(UrunNo))
                    {
                        temizle();
                        label8.ForeColor = Color.Green;
                        label8.Text = "Ürün Başarıyla Silindi.";
                        comboBox2_SelectedIndexChanged(comboBox2, e);
                    }
                    else
                    {

                        temizle();
                        label8.ForeColor = Color.Red;
                        label8.Text = "Ürün Silinirken Hata!";
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
           // System.Diagnostics.Process.Start("osk.exe");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            int a = this.Width;
            int b = this.Height;
            panel1.Location = new Point((a - panel1.Width) / 2, (b - panel1.Height) / 2);
        }
    }
}

