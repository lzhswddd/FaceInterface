using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace OperateCamera
{
    public partial class ScreenForm : Form
    {
        private Bitmap screen = null;
        public Rectangle rectangle = new Rectangle(-1, -1, -1, -1);
        public ScreenForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void screenbutton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                Hide();
            }
            FaceApi.ImageDllcallBack d = new FaceApi.ImageDllcallBack(GetScreen);
            FaceApi.CatchFrame(d, false);
        }

        private void GetScreen(IntPtr scan0, int width, int height, int step)
        {
            using (Bitmap bitmap = new Bitmap(width, height, step, PixelFormat.Format24bppRgb, scan0))
            {
                if (pictureBox1.BackgroundImage != null)
                {
                    pictureBox1.BackgroundImage.Dispose();
                    pictureBox1.BackgroundImage = null;
                }
                if (screen != null)
                {
                    screen.Dispose();
                    screen = null;
                }
                if (bitmap != null)
                {
                    if (comboBox2.SelectedIndex == 1)
                    {
                        BoardForm boardForm = new BoardForm(this, new Bitmap(bitmap));
                        boardForm.ShowDialog();
                    }
                    if (rectangle.X == rectangle.Width && rectangle.Y == rectangle.Height)
                        screen = new Bitmap(bitmap);
                    else
                        screen = bitmap.Clone(rectangle, bitmap.PixelFormat);
                    pictureBox1.BackgroundImage = new Bitmap(screen);
                }
                else
                {
                    MessageBox.Show("截屏失败!");
                }
            }
            if (comboBox1.SelectedIndex == 0)
            {
                Show();
            }
        }

        private void facebutton_Click(object sender, EventArgs e)
        {
            if (screen != null)
            {
                Bitmap bitmap = new Bitmap(screen);
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            BitmapHelper.SaveBitmap(screen);
        }

        private void reset_Click(object sender, EventArgs e)
        {
            if (pictureBox1.BackgroundImage != null)
            {
                pictureBox1.BackgroundImage.Dispose();
                pictureBox1.BackgroundImage = null;
            }
        }
    }
}
