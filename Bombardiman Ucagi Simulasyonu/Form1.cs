using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombardimanUcagiSimulasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int ucakHizi, atisHizi, yercekimi, bombaBoyutu, hedefHizi;
        int ucakKonum, hedefkonum;
        Point bombaKonum;
        double bombaHızıY, bombaHızıX;

        private void timer2_Tick(object sender, EventArgs e)
        {
            BombaHareket();
            label9.Text = bombaHızıY.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                timer3.Start();
            }
            else
            {
                timer3.Stop();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            bombaKonum.X = pictureBox1.Location.X;
            bombaKonum.Y = pictureBox1.Location.Y;
            bombaHızıY = atisHizi;
            if (checkBox1.Checked == true)
            {
                bombaHızıX = ucakHizi;
            }
            else
            {
                bombaHızıX = 0;
            }
            label11.Text = "";
            timer2.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox2.Location = new Point(hedefkonum, pictureBox2.Location.Y);
            if (pictureBox2.Location.X > tabControl1.Width - pictureBox2.Width)
            {
                hedefHizi *= -1;
            }
            else if (pictureBox2.Location.X < 0)
            {
                hedefHizi *= -1;
            }
            hedefkonum += hedefHizi;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(ucakKonum, pictureBox1.Location.Y);
            if (pictureBox1.Location.X > tabControl1.Width - pictureBox1.Width)
            {
                ucakHizi *= -1;
            }
            else if (pictureBox1.Location.X < 0)
            {
                ucakHizi *= -1;
            }
            ucakKonum += ucakHizi;

            label7.Text = Math.Abs(ucakHizi).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Eksik bilgi Girdiniz", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                ucakHizi = int.Parse(textBox1.Text);
                yercekimi = int.Parse(textBox2.Text);
                atisHizi = int.Parse(textBox3.Text);
                bombaBoyutu = int.Parse(textBox4.Text);
                hedefHizi = int.Parse(textBox5.Text);
                ucakKonum = pictureBox1.Location.X;
                hedefkonum = pictureBox2.Location.X;
                label9.Text = atisHizi.ToString();
                timer1.Start();
            }

        }

        void BombaCiz()
        {
            Brush b = new SolidBrush(Color.Red);          
            Graphics g = tabPage2.CreateGraphics();          
            g.Clear(Color.White);
            g.FillEllipse(b, bombaKonum.X, bombaKonum.Y, bombaBoyutu, bombaBoyutu);         
            CarpısmaHesapla();

            if (bombaKonum.Y > tabControl1.Height)
            {
                //blokta bomba boşluğa giderse çalışır.
                timer2.Stop();
                bombaKonum.X = 0;
                bombaKonum.Y = 0;
            }
        }

        void BombaHareket()
        {
            bombaKonum.X += (int)bombaHızıX;
            double hiz = Math.Sqrt(2 * yercekimi * tabControl1.Height); //v^2=2*g*h formulu

            bombaHızıY += hiz;

            bombaHızıY /= 8;

            bombaKonum.Y += (int)bombaHızıY;
            BombaCiz();
        }
        void CarpısmaHesapla()
        {
            //burada bombanın hedefe çarpma olayı kontrol edilmiştir.fark değişkeni ile ne kadar uzağa düştüğü hesaplanmıştır.
            //fark sonucunda belirlenen aralıklar için kullanıcı bilgilendirilmiştir.
            int farkVurulmadı = -1, farkVuruldu = -1;
            if (bombaKonum.X+bombaBoyutu/2 < pictureBox2.Location.X && bombaKonum.Y >= pictureBox2.Location.Y &&bombaKonum.Y>=pictureBox2.Location.Y+pictureBox2.Height)
            {
                farkVurulmadı = pictureBox2.Location.X - (bombaKonum.X + bombaBoyutu);
                if (bombaKonum.X + bombaBoyutu >= pictureBox2.Location.X)
                {
                    farkVuruldu = (bombaKonum.X + bombaBoyutu) - pictureBox2.Location.X;
                    farkVurulmadı = -1;

                }
            }
            if ((bombaKonum.X+bombaBoyutu/2 > pictureBox2.Location.X + pictureBox2.Width) && bombaKonum.Y >= pictureBox2.Location.Y && bombaKonum.Y >= pictureBox2.Location.Y + pictureBox2.Height)
            {
                farkVurulmadı = (bombaKonum.X) - (pictureBox2.Location.X + pictureBox2.Width);
                if ((bombaKonum.X) < (pictureBox2.Location.X + pictureBox2.Width))
                {
                    farkVuruldu = (pictureBox2.Location.X + pictureBox2.Width) - (bombaKonum.X );
                    farkVurulmadı = -1;

                }
            }
            if (bombaKonum.X+bombaBoyutu/2 <= (pictureBox2.Location.X + pictureBox2.Width) && bombaKonum.X+bombaBoyutu/2 >= pictureBox2.Location.X && bombaKonum.Y >= pictureBox2.Location.Y&&bombaKonum.Y>=pictureBox2.Location.Y+pictureBox2.Height)
            {
                label11.Text = "Başarıyla vuruldu";
                farkVuruldu = -1;
            }
            if (farkVurulmadı > 0 && farkVurulmadı < 50)
            {
                label11.Text = "Şanslısın";
            }
            if (farkVurulmadı >= 50 && farkVurulmadı <= 100)
            {
                label11.Text = "Yaklastın";
            }
            if (farkVuruldu >= pictureBox2.Width)
            {
                label11.Text = "Başarıyla vuruldu";
            }
            if (farkVuruldu < pictureBox2.Width && farkVuruldu > 0)
            {
                label11.Text = farkVuruldu.ToString() + " " + "oranıyla vuruldu";
            }
        }
        }
}
