using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperateCamera
{
    public partial class DrawFaceForm : Form
    {       
        Bitmap image = null;
        public DrawFaceForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string imagePath = FileTools.GetImagePath();
            try
            {
                if (imagePath != null)
                {
                    image = new Bitmap(BitmapHelper.ReadImageFile(imagePath));
                    pictureBox1.BackgroundImage = image.Clone(new Rectangle(0, 0, image.Width, image.Height), PixelFormat.Format24bppRgb);
                }
                else
                {
                    pictureBox1.BackgroundImage = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(pictureBox1.BackgroundImage != null)
            {
                Bitmap bitmap = new Bitmap(image);
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int success = FaceApi.DrawFaces(bitmapData.Scan0, bitmapData.Width, bitmapData.Height, bitmapData.Stride, 3);
                bitmap.UnlockBits(bitmapData);
                if (success == 0)
                {
                    pictureBox1.BackgroundImage.Dispose();
                    pictureBox1.BackgroundImage = bitmap;
                }
                else
                {
                    bitmap.Dispose();
                    MessageBox.Show("绘制人脸失败!");
                }
            }
            else
            {
                MessageBox.Show("请添加图片!");
            }
        }
    }
}
