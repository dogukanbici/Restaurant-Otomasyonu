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
    public partial class Mutfak : Form
    {
        public Mutfak()
        {
            InitializeComponent();
        }
        List<string> CbBlist = new List<string>();
        private void Mutfak_Load(object sender, EventArgs e)
        {
            CbBlist=Hesap.doluMasa();

            for (int i = 0; i < CbBlist.Count; i++)
            {
                comboBox1.Items.Add(CbBlist[i]);
            }
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        List<string> hazirlanacak = new List<string>();
          
            hazirlanacak = Hesap.hazirlanacakliste(comboBox1.SelectedItem.ToString());
            listBox1.Items.Clear();
            for (int i = 0; i < hazirlanacak.Count; i++)
            {
                listBox1.Items.Add(hazirlanacak[i]);
            }

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            
            this.Close();
            
            
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            int a = this.Width;
            int b = this.Height;
            panel1.Location = new Point((a - panel1.Width) / 2, (b - panel1.Height) / 2);
        }
        int masa = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            Hesap.hazir(urunadi, masano);

            List<string> hazirlanacak = new List<string>();

            hazirlanacak = Hesap.hazirlanacakliste(comboBox1.SelectedItem.ToString());
            listBox1.Items.Clear();
            for (int i = 0; i < hazirlanacak.Count; i++)
            {
                listBox1.Items.Add(hazirlanacak[i]);
            }


        }
        private string masano, urunadi;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                urunadi = listBox1.SelectedItem.ToString();
                urunadi = urunadi.Split('-')[0];
                masano = listBox1.SelectedItem.ToString();
                masano = masano.Split('-')[2];
            }
            catch 
            {
                MessageBox.Show("Lütfen hazır olan ürünü seçiniz!");
            }
            


        }
    }
}
