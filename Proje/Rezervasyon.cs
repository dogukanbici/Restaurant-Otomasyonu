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
    public partial class Rezervasyon : Form
    {
        public Rezervasyon()
        {
            InitializeComponent();
        }
        //masa id çek 
        string masaid;
        public Rezervasyon(string masaid)
        {
            this.masaid = masaid;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
        public static int durum = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            //masa rezerve durumu güncelle 
            Hesap.rezerve(masaid, true);
            this.Close();
          
        }

        private void Rezervasyon_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hesap.rezerveiptal(masaid);
            this.Close();
        }
    }
}
