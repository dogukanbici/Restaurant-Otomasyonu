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
    public partial class pinpad : Form
    {
        public pinpad()
        {
            InitializeComponent();
        }
        public string deger = "";
        private void Dugme_Click(object sender, EventArgs e)
        {
           
            Button btn = (sender as Button);        
            deger += btn.Text;
            label1.Text = deger;   
        }
        private void pinpad_Load(object sender, EventArgs e)
        {
          
        }

        private void button12_Click(object sender, EventArgs e)
        {        
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string temp = label1.Text;
            deger = "";
            for (int i = 0; i < temp.Length - 1; i++)
            {
                deger += temp[i];
            }
            label1.Text = deger;
        }
    }
}
