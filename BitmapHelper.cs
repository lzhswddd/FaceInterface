using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperateCamera
{
    public static class BitmapHelper
    {
        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="bitmap">原图片</param>
        /// <param name="width">新图片宽度</param>
        /// <param name="height">新图片高度</param>
        /// <returns>新图片</returns>
        public static Bitmap ScaleToSize(this Bitmap bitmap, int width, int height)
        {
            if (bitmap.Width == width && bitmap.Height == height)
            {
                return bitmap;
            }

            var scaledBitmap = new Bitmap(width, height);
            using (var g = Graphics.FromImage(scaledBitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bitmap, 0, 0, width, height);
            }

            return scaledBitmap;
        }
        public static Bitmap ReadImageFile(string path)
        {
            Bitmap bitmap = null;
            try
            {
                using (FileStream fs = File.OpenRead(path))
                { //OpenRead
                    int filelength = 0;
                    filelength = (int)fs.Length; //获得文件长度 
                    Byte[] image1 = new Byte[filelength]; //建立一个字节数组 
                    fs.Read(image1, 0, filelength); //按字节流读取
                    bitmap = (Bitmap)Image.FromStream(fs);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            return bitmap;
        }
        public static Bitmap DrawRectangleInPicture(Bitmap bmp, System.Drawing.Point p0, System.Drawing.Point p1, Color RectColor, int LineWidth, DashStyle ds)
        {
            if (bmp == null) return null;
            try
            {
                Graphics g = Graphics.FromImage(bmp);
                Brush brush = new SolidBrush(RectColor);
                Pen pen = new Pen(brush, LineWidth);
                pen.DashStyle = ds;
                g.DrawRectangle(pen, new Rectangle(p0.X, p0.Y, Math.Abs(p0.X - p1.X), Math.Abs(p0.Y - p1.Y)));
                g.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bmp;
        }

        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="bitmap">原图片</param>
        /// <param name="size">新图片大小</param>
        /// <returns>新图片</returns>
        public static Bitmap ScaleToSize(this Bitmap bitmap, Size size)
        {
            return bitmap.ScaleToSize(size.Width, size.Height);
        }

        /// <summary>
        /// 按比例来缩放
        /// </summary>
        /// <param name="bitmap">原图</param>
        /// <param name="ratio">ratio大于1,则放大;否则缩小</param>
        /// <returns>新图片</returns>
        public static Bitmap ScaleToSize(this Bitmap bitmap, float ratio)
        {
            return bitmap.ScaleToSize((int)(bitmap.Width * ratio), (int)(bitmap.Height * ratio));
        }

        /// <summary>
        /// 按给定长度/宽度等比例缩放
        /// </summary>
        /// <param name="bitmap">原图</param>
        /// <param name="width">新图片宽度</param>
        /// <param name="height">新图片高度</param>
        /// <returns>新图片</returns>
        public static Bitmap ScaleProportional(this Bitmap bitmap, int width, int height)
        {
            float proportionalWidth, proportionalHeight;

            if (width.Equals(0))
            {
                proportionalWidth = ((float)height) / bitmap.Size.Height * bitmap.Width;
                proportionalHeight = height;
            }
            else if (height.Equals(0))
            {
                proportionalWidth = width;
                proportionalHeight = ((float)width) / bitmap.Size.Width * bitmap.Height;
            }
            else if (((float)width) / bitmap.Size.Width * bitmap.Size.Height <= height)
            {
                proportionalWidth = width;
                proportionalHeight = ((float)width) / bitmap.Size.Width * bitmap.Height;
            }
            else
            {
                proportionalWidth = ((float)height) / bitmap.Size.Height * bitmap.Width;
                proportionalHeight = height;
            }

            return bitmap.ScaleToSize((int)proportionalWidth, (int)proportionalHeight);
        }

        /// <summary>
        /// 按给定长度/宽度缩放,同时可以设置背景色
        /// </summary>
        /// <param name="bitmap">原图</param>
        /// <param name="backgroundColor">背景色</param>
        /// <param name="width">新图片宽度</param>
        /// <param name="height">新图片高度</param>
        /// <returns></returns>
        public static Bitmap ScaleToSize(this Bitmap bitmap, Color backgroundColor, int width, int height)
        {
            var scaledBitmap = new Bitmap(width, height);
            using (var g = Graphics.FromImage(scaledBitmap))
            {
                g.Clear(backgroundColor);

                var proportionalBitmap = bitmap.ScaleProportional(width, height);

                var imagePosition = new Point((int)((width - proportionalBitmap.Width) / 2m), (int)((height - proportionalBitmap.Height) / 2m));
                g.DrawImage(proportionalBitmap, imagePosition);
            }

            return scaledBitmap;
        }
        public static void SaveBitmap(Image image)
        {
            if (image != null)
            {
                string savepath = FileTools.ShowSaveImageDialog();
                if (savepath != null)
                {
                    try
                    {
                        using (Bitmap bitmap = new Bitmap(image))
                        {
                            bitmap.Save(savepath);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("保存失败: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("保存失败: 请输入正确的路径");
                }
            }
            else
            {
                MessageBox.Show("保存失败: 没有可保存的图像");
            }
        }
    }
}
