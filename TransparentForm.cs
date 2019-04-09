using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperateCamera
{
    public partial class TransparentForm : Form
    {
        private bool mouseStatus = false;//鼠标状态，false为松开
        private Point startPoint;//鼠标按下的点
        private Point endPoint;//
        private Rectangle currRect;//当前正在绘制的举行
        private int minStartX, minStartY, maxEndX, maxEndY;//最大重绘矩形的上下左右的坐标，这样重绘的效率更高。
        private Form superForm = null;
        public TransparentForm(Form form)
        {
            InitializeComponent();
            BackColor = Color.Black;
            Determine.Text = "";
            Cencel.Text = "";
            Determine.BackColor = Color.Aqua;
            Cencel.BackColor = Color.Aqua;
            TransparencyKey = Color.Aqua;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            superForm = form;
        }
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern IntPtr GetActiveWindow();//获取当前窗体的活动状态
        // 判断当前窗口是否处于活动状态的方法
        private bool ThisIsActive() { return (GetActiveWindow() == this.Handle); }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!ThisIsActive())
            {
                this.Activate();
            }
            this.WindowState = FormWindowState.Normal;
        }
        private void TransparentForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseStatus = true;
            startPoint.X = e.X;
            startPoint.Y = e.Y;
            //重新一个矩形，重置最大重绘矩形的上下左右的坐标
            minStartX = e.X;
            minStartY = e.Y;
            maxEndX = e.X;
            maxEndY = e.Y;
            Determine.BackColor = Color.Aqua;
            Cencel.BackColor = Color.Aqua;
            Determine.Text = "";
            Cencel.Text = "";
            Invalidate(new Rectangle(0, 0, Width, Height));//失效一个区域，并使其重绘。
        }

        private void button1_Click()
        {
            Close();
        }

        private void Determine_Click(object sender, EventArgs e)
        {
            (superForm as ScreenForm).rectangle = currRect;
            button1_Click();
        }

        private void Cencel_Click(object sender, EventArgs e)
        {
            button1_Click();
        }

        private void TransparentForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseStatus)
            {
                endPoint.X = e.X; endPoint.Y = e.Y;
                //这一段是获取要绘制矩形的上下左右的坐标，如果不这样处理的话，只有从左上开始往右下角才能画出矩形。
                //这样处理的话，可以任意方向，当然中途可以更换方向。
                int realStartX = Math.Min(startPoint.X, endPoint.X);
                int realStartY = Math.Min(startPoint.Y, endPoint.Y);
                int realEndX = Math.Max(startPoint.X, endPoint.X);
                int realEndY = Math.Max(startPoint.Y, endPoint.Y);
                minStartX = Math.Min(minStartX, realStartX);
                minStartY = Math.Min(minStartY, realStartY);
                maxEndX = Math.Max(maxEndX, realEndX);
                maxEndY = Math.Max(maxEndY, realEndY);
                currRect = new Rectangle(realStartX, realStartY, realEndX - realStartX, realEndY - realStartY);
                //一下是为了获取最大重绘矩形。
                Rectangle refeshRect = new Rectangle(minStartX, minStartY, maxEndX - minStartX, maxEndY - minStartY);
                refeshRect.Inflate(1, 1);//重绘矩形的大小扩展1个单位
                Invalidate(refeshRect);//失效一个区域，并使其重绘。
            }
        }
        private void TransparentForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseStatus = false;
            endPoint.X = e.X; endPoint.Y = e.Y;
            int realStartX = Math.Min(startPoint.X, endPoint.X);
            int realStartY = Math.Min(startPoint.Y, endPoint.Y);
            int realEndX = Math.Max(startPoint.X, endPoint.X);
            int realEndY = Math.Max(startPoint.Y, endPoint.Y);
            currRect = new Rectangle(realStartX, realStartY, realEndX - realStartX, realEndY - realStartY);
            //rect = currRect;//当前矩形算是被认可了，所以存起来
            this.Invalidate();//重绘整个界面
            Determine.Text = "确认";
            Cencel.Text = "取消";
            Determine.Location = new Point(realEndX - Cencel.Width - Determine.Width, realEndY + Determine.Height / 2);
            Cencel.Location = new Point(realEndX - Cencel.Width,  realEndY + Determine.Height / 2);
            Determine.BackColor = Color.White;
            Cencel.BackColor = Color.White;
        }
        private void TransparentForm_Paint(object sender, PaintEventArgs e)//处理重绘情况
        {
            Graphics g = e.Graphics;
            g.Clear(BackColor);
            g.FillRectangle(new SolidBrush(Color.Aqua), currRect);
            //g.DrawRectangle(new Pen(Color.Red, 1), rect);
        }
    }
}
