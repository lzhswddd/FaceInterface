namespace OperateCamera
{
    partial class TransparentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Cencel = new System.Windows.Forms.Label();
            this.Determine = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Cencel
            // 
            this.Cencel.AutoSize = true;
            this.Cencel.BackColor = System.Drawing.Color.White;
            this.Cencel.Font = new System.Drawing.Font("宋体", 10F);
            this.Cencel.Location = new System.Drawing.Point(715, 169);
            this.Cencel.Name = "Cencel";
            this.Cencel.Size = new System.Drawing.Size(35, 14);
            this.Cencel.TabIndex = 0;
            this.Cencel.Text = "取消";
            this.Cencel.Click += new System.EventHandler(this.Cencel_Click);
            // 
            // Determine
            // 
            this.Determine.AutoSize = true;
            this.Determine.BackColor = System.Drawing.Color.White;
            this.Determine.Font = new System.Drawing.Font("宋体", 10F);
            this.Determine.Location = new System.Drawing.Point(674, 169);
            this.Determine.Name = "Determine";
            this.Determine.Size = new System.Drawing.Size(35, 14);
            this.Determine.TabIndex = 1;
            this.Determine.Text = "确定";
            this.Determine.Click += new System.EventHandler(this.Determine_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TransparentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Determine);
            this.Controls.Add(this.Cencel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TransparentForm";
            this.Opacity = 0.7D;
            this.Text = "TransparentForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TransparentForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TransparentForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TransparentForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TransparentForm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Cencel;
        private System.Windows.Forms.Label Determine;
        private System.Windows.Forms.Timer timer1;
    }
}