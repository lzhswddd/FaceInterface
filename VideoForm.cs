using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace OperateCamera
{
    public partial class VideoForm : Form
    {
        private Thread quitthread = null;
        private Thread thread = null;
        delegate void SetBitmapCallback(Bitmap bitmap);
        delegate void CloseCallback();
        public bool close = false;
        public bool stop = false;
        public bool process = false;
        public VideoForm()
        {
            InitializeComponent();
        }

        public void CsharpCall(IntPtr scan0, int width, int height, int step)
        {
            if (scan0 == null || width == 0 || height == 0 || step == 0)
            {
                CreateBitmap(null);
            }
            else
            {
                if (stop)
                    CreateBitmap(null);
                else
                    CreateBitmap(new Bitmap(width, height, step, System.Drawing.Imaging.PixelFormat.Format24bppRgb, scan0));
            }
        }
        public void CreateBitmap(Image img)
        {
            if (pictureBox1.InvokeRequired)//调用和创建该控件的线程是不同的线程，必须调用Invoke方法
            {
                if (close) return;
                //创建该方法的委托实例
                SetBitmapCallback d = new SetBitmapCallback(CreateBitmap);
                //调用该委托实例，并传递参数，参数为object类型，使用this调用Invoke（this为当前窗体，是创建该窗体控件的线程）
                Invoke(d, new object[] { img });//this指定创建该控件的线程来Invoke（调用）
            }
            else//调用与创建该控件的线程是同一个线程
            {
                if (img != null)
                {
                    Bitmap bitmap = new Bitmap(img);
                    if (process)
                    {
                        BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                        int success = FaceApi.DrawFaces(bitmapData.Scan0, bitmapData.Width, bitmapData.Height, bitmapData.Stride, 3);
                        bitmap.UnlockBits(bitmapData);
                        if (pictureBox1.BackgroundImage != null)
                        {
                            pictureBox1.BackgroundImage.Dispose();
                            pictureBox1.BackgroundImage = null;
                        }
                        pictureBox1.BackgroundImage = bitmap;
                    }
                    else
                    {
                        if (pictureBox1.BackgroundImage != null)
                        {
                            pictureBox1.BackgroundImage.Dispose();
                            pictureBox1.BackgroundImage = null;
                        }
                        pictureBox1.BackgroundImage = bitmap;
                    }
                }
                else
                {
                    if (pictureBox1.BackgroundImage != null)
                    {
                        pictureBox1.BackgroundImage.Dispose();
                        pictureBox1.BackgroundImage = null;
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            process = !process;
        }
        public void videoProcess(object obj)
        {
            string vedioFile = (string)obj;
            if (FaceApi.OpenVideo(vedioFile))
            {
                FaceApi.ImageDllcallBack dllcall = new FaceApi.ImageDllcallBack(CsharpCall);
                if (textBox1.Text == "" || textBox2.Text == "")
                    while (!stop && FaceApi.GetVideoFrame(dllcall, false)) ;
                else
                    while (!stop && FaceApi.GetVideoFrame(dllcall, true, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text))) ;
                CreateBitmap(null);
                FaceApi.CloseVideo();
            }
            else
            {
                MessageBox.Show("视频打开失败!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stop = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string vedioFile = FileTools.GetVideoPath();
            try
            {
                if (vedioFile != null)
                {
                    if (thread == null || thread.ThreadState != ThreadState.Running)
                    {
                        thread = new Thread(new ParameterizedThreadStart(videoProcess));
                        thread.Start(vedioFile);
                    }
                }
                else
                {
                    if (pictureBox1.BackgroundImage != null)
                    {
                        pictureBox1.BackgroundImage.Dispose();
                        pictureBox1.BackgroundImage = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= 48 && e.KeyChar <= 57))
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= 48 && e.KeyChar <= 57))
                e.Handled = true;
        }

        private void VideoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!close)
            {
                button4_Click(null, null);
                e.Cancel = true;
            }
        }
        private void CloseForm()
        {
            if (pictureBox1.InvokeRequired)//调用和创建该控件的线程是不同的线程，必须调用Invoke方法
            {
                //创建该方法的委托实例
                CloseCallback d = new CloseCallback(CloseForm);
                //调用该委托实例，并传递参数，参数为object类型，使用this调用Invoke（this为当前窗体，是创建该窗体控件的线程）
                Invoke(d, new object[] { });//this指定创建该控件的线程来Invoke（调用）
            }
            else//调用与创建该控件的线程是同一个线程
            {
                if (pictureBox1.BackgroundImage != null)
                {
                    pictureBox1.BackgroundImage.Dispose();
                    pictureBox1.BackgroundImage = null;
                }
                Close();
            }
        }       

        private void button4_Click(object sender, EventArgs e)
        {
            if (quitthread == null || quitthread.ThreadState != ThreadState.Running)
            {
                quitthread = new Thread(new ThreadStart(QuitEvent));
                quitthread.Start();
            }
        }

        private void QuitEvent()
        {
            stop = true;
            close = true;
            while (thread != null && thread.ThreadState != ThreadState.Stopped) ;
            CloseForm();
        }
    }
}
