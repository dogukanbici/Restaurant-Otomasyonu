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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Giris grs = new Giris();
            grs.ShowDialog();
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Masalar frm = new Masalar();
            frm.ShowDialog();
           
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label1.Text = Kontrol.kisi;
        }

        private void Menu_Resize(object sender, EventArgs e)
        {
            int a = this.Width;
            int b = this.Height;
            panel1.Location = new Point((a - panel1.Width) / 2, (b - panel1.Height) / 2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Mutfak frm = new Mutfak();
            frm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongDateString() + "\n " + DateTime.Now.ToLongTimeString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Rapor frm = new Rapor();
            frm.ShowDialog();
            //this.Hide();
            //this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Ayarlar frm = new Ayarlar();
            frm.ShowDialog();
            //this.Hide();
            //this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Personel frm = new Personel();
            frm.ShowDialog();
            //this.Hide();
            //this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Masalar form2 = new Masalar();
            form2.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Kasalar form2 = new Kasalar();
            form2.ShowDialog();
            this.Hide();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

