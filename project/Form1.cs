using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageProcess2;
using System.Collections;

namespace Coin_Detections
{
    public partial class Form1 : Form
    {
        Bitmap pirst, sekond;
        ArrayList dblob;
        float value;
        int cent5 = 0;
        int cent10 = 0;
        int cent25 = 0;
        int peso = 0;
        int peso5 = 0;
        public Form1()
        {
            InitializeComponent();
            dblob = new ArrayList();
            value = 0;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //pirst = new Bitmap("D:\\Documents\\Visual Studio 2015\\Projects\\Coin_Detections\\Coin_Detections\\coins1");
            //pictureBox1.Image = pirst;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //openFileDialog1.ShowDialog();
            pirst = new Bitmap(@"D:\\Documents\\Visual Studio 2015\\Projects\\Coin_Detections\\Coin_Detections\\coins1.jpg");
            pictureBox1.Image = pirst;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BitmapFilter.Contrast(pirst, 100);
            progressBar1.Value += 2;
            BitmapFilter.GaussianBlur(pirst, 10);
            progressBar1.Value += 3;
            BitmapFilter.GrayScale(pirst);
            progressBar1.Value += 2;
            BitmapFilter.Threshold(ref pirst, ref sekond, 235);
            progressBar1.Value += 15;
            read();
            label1.Text = "Done";
            pictureBox2.Image = sekond;
            show();
        }
        private void read()
        {
            for(int i = 0; i < sekond.Width; i++)
            {
                for(int j = 0; j < sekond.Height; j++)
                {
                    if (sekond.GetPixel(i, j).R == 0)
                    {
                        bool found = false;
                        foreach(GroupBlobs b in dblob)
                        {
                           Blob groupTemp = b.getZero();
                           if (groupTemp.isNear(i, j))
                           {
                              b.groupAdd(new Blob(i,j));
                              found = true;
                              break;
                           }
                        }
                        if (!found)
                        {
                            Blob b = new Blob(i, j);
                            GroupBlobs a = new GroupBlobs();
                            a.groupAdd(b);
                            dblob.Add(a);
                        }
                    }
                }
                progressBar1.Value += 1;
            }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            label1.Visible = true;
            label1.Text = "Processing...";
            progressBar1.Visible = true;
        }

        public void show()
        {
            float val;
            //Console.WriteLine("#ofGroupBlobs: " + dblob.Count);
            foreach(GroupBlobs b in dblob)
            {
                //Console.WriteLine(b.getCount());
                val = b.getCount();
                if (val <= 7499 && val >= 5000)
                    cent5++;
                else if (val <= 9299 && val >= 7500)
                    cent10++;
                else if (val <= 14599 && val >= 9300)
                    cent25++;
                else if (val <= 19500 && val >= 14600)
                    peso++;
                else if (val <= 25000 && val >= 19000)
                    peso5++;
            }
            value = cent5 * .05F + cent10 * .10F + cent25 * .25F + peso + peso5 * 5F;
            string s= "Total value is: "+value + " with " + cent5 + ", five centavo coin; " + cent10 + ", ten centavo coin; " + cent25 + ", twenty-five centavo coin; " + peso + ",  one peso coin; " + peso5+ ", five peso coin.";
            MessageBox.Show(s, "Success!");
        }
    }
}
