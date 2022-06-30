using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Proje
{
    public partial class Personel : Form
    {
        public Personel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("osk.exe");
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız!");
            }
            else
            {
                if (Hesap.veriVarmi("select KullaniciAdi from KullaniciBilgi where KullaniciAdi='" + textBox1.Text + "'"))
                {
                    label6.ForeColor = Color.Red;
                    label6.Text = "Böyle bir kullanıcı var!.";
                }
                else
                {
                    if (Kontrol.KullaniciEkle(textBox1.Text,textBox2.Text,textBox3.Text,textBox4.Text,comboBox1.Text))
                    {
                        label6.ForeColor = Color.Green;
                        label6.Text = "Başarıyla kullanıcı eklenmiştir.";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                    }
                    else
                    {
                        label6.ForeColor = Color.Red;
                        label6.Text = "Kullanıcı eklenemedi!.";
                    }
                }
                
            }
            
        }

        private void Personel_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Process.Start("osk.exe");
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("osk.exe");
        }
    }
}
