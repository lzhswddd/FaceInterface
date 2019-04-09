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
    public partial class BoardForm : Form
    {
        private Image image = null;
        private Form superForm;
        public BoardForm(Form form, Image image)
        {
            InitializeComponent();
            this.image = image;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            superForm = form;
        }

        private void BoardForm_Shown(object sender, EventArgs e)
        {
            BackgroundImage = image;
            TransparentForm transparentForm = new TransparentForm(superForm);
            transparentForm.Dock = DockStyle.Fill;
            transparentForm.ShowDialog();
            Close();
        }
    }
}
