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
    public partial class Kasalar : Form
    {
        public Kasalar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Kontrol.KasaEkle(textBox1.Text, comboBox1.Text);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }
    }
}
