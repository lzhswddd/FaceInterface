using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperateCamera
{
    public partial class DistFrom : Form
    {       
        public DistFrom()
        {
            InitializeComponent();
        }

        private void TestFrom_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = FileTools.GetImagePath();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = FileTools.GetImagePath();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                label3.Text = FaceApi.FaceRecognition(label1.Text, label2.Text) + "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
