using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace convertrgb2hsv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        public Bitmap bmp;

        string img;

        public static void RGB2HSV(Color color, out double h, out double s, out double v)
        {
            int min_rgb = Math.Max(color.R, Math.Max(color.G, color.B));
            int max_rgb = Math.Min(color.R, Math.Min(color.G, color.B));

            h = color.GetHue();
            s = (max_rgb == 0) ? 0 : 1.0 - (1.0 * min_rgb / max_rgb);
            v = max_rgb / 255.0;
        }

        public static Color HSV2RGB(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (v > 255)
            {
                v = 255;
            }
            if (v < 0)
            {
                v = 0;
            }
            if (p > 255)
            {
                p = 255;
            }
            if (p < 0)
            {
                p = 0;
            }
            if (q > 255)
            {
                q = 255;
            }
            if (q < 0)
            {
                q = 0;
            }
            if (t > 255)
            {
                t = 255;
            }
            if (t < 0)
            {
                t = 0;
            }
            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public void ConvertHSV()
        {

            Bitmap conv_img = (Bitmap)this.bmp.Clone();

            int x, y;
            double hue, satur, val;
            double tr_h = trackBar1.Value;
            double tr_s = (double)trackBar2.Value / 100.0;
            double tr_v = (double)trackBar3.Value / 100.0;

            for (x = 0; x < conv_img.Width; x++)
            {
                for (y = 0; y < conv_img.Height; y++)
                {
                    Color old_color = conv_img.GetPixel(x, y);
                    RGB2HSV(old_color, out hue, out satur, out val);
                    Color new_color = HSV2RGB(hue + tr_h, satur + tr_s, val + tr_v);
                    conv_img.SetPixel(x, y, new_color);

                }
            }
            pictureBox1.Image = conv_img;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.bmp != null)
                ConvertHSV();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //this.img = "D:\\example.jpg";
            //this.bmp = new Bitmap(this.img);

            //pictureBox1.Image = this.bmp;
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpeg;*.jpg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.img = ofd.FileName;
                    this.bmp = new Bitmap(this.img);
                }
                pictureBox1.Image = this.bmp;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Bitmap p = new Bitmap(pictureBox1.Image);
            p.Save(this.img.Substring(0, this.img.Length - 4) + "(converted).jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
