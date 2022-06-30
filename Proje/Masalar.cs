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

    public partial class Masalar : Form
    {
        public Masalar()
        {
            InitializeComponent();
        }
        public static int MasaNumarasi;
        public static int masaSayisi;
        private void button1_Click(object sender, EventArgs e)
        {
            Menu mn = new Menu();
            mn.ShowDialog();
            this.Close();
        }
        void masacek()
        {
            DataTable dt = VeritabaniBaglanti.VeriGetir("SELECT MasaSayisi FROM MasaAyar ");

            int satirsayi = Convert.ToInt32(dt.Rows[0][0].ToString()) / 5;

            int sutun = 5;

            for (int i = 0; i <= satirsayi; i++)
            {
                if (i == satirsayi)
                {
                    sutun = Convert.ToInt32(dt.Rows[0][0].ToString()) % 5;
                }

                for (int j = 0; j < sutun; j++)
                {

                    Button dugme = new Button();
                    dugme.Top = 180 * i;
                    dugme.Left = 150 * j;
                    dugme.Width = 150;
                    dugme.Height = 150;
                    dugme.Font = new Font(dugme.Font.Name, 20, FontStyle.Bold);
                    dugme.ForeColor = Color.White;
                    dugme.Text = ((i * 5 + j)+1).ToString();
                    dugme.BackgroundImageLayout = ImageLayout.Stretch;
                    if (Hesap.masadrmGetir(((i * 5 + j) + 1).ToString()) && Hesap.rezervemi(((i * 5 + j) + 1).ToString()))
                    {
                        dugme.BackgroundImage = Image.FromFile(@"D:\masaüstü\projeson\Proje\Resources\Rezerve.jpeg");
                    }
                    else if (Hesap.masadrmGetir(((i * 5 + j) + 1).ToString()))
                    {
                        dugme.BackgroundImage = Image.FromFile(@"D:\masaüstü\projeson\Proje\Resources\dolu.jpeg");

                    }
                    else if (Rezervasyon.durum == 0)
                    {
                        dugme.BackgroundImage = Image.FromFile(@"D:\masaüstü\projeson\Proje\Resources\Bos.jpeg");

                    }

                    dugme.Click += Dugme_Click;
                    panel1.Controls.Add(dugme);
                }

            }
        }
        private void Masalar_Load(object sender, EventArgs e)
        {
            masacek();  
        }
        private static int masano;

        public int Masano
        {
            get
            {
                return masano;
            }

            set
            {
                masano = value;
            }
        }

        private void Dugme_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
           // MessageBox.Show(btn.Text.ToString());
            Masano = Convert.ToInt32(btn.Text.ToString());
            btn.BackColor = Color.Red;
            Siparis frmSiparis = new Siparis();
            frmSiparis.ShowDialog();
            this.Close();
        
        }

        private void Masalar_Resize(object sender, EventArgs e)
        {
            int a = this.Width;
            int b = this.Height;
            panel1.Location = new Point((a - panel1.Width) / 2, (b - panel1.Height) / 2);
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
          
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
