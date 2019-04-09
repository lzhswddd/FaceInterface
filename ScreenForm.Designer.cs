namespace OperateCamera
{
    partial class ScreenForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.screenbutton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.facebutton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.reset = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(490, 378);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // screenbutton
            // 
            this.screenbutton.Location = new System.Drawing.Point(184, 12);
            this.screenbutton.Name = "screenbutton";
            this.screenbutton.Size = new System.Drawing.Size(75, 25);
            this.screenbutton.TabIndex = 1;
            this.screenbutton.Text = "截图";
            this.screenbutton.UseVisualStyleBackColor = true;
            this.screenbutton.Click += new System.EventHandler(this.screenbutton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "隐藏窗口",
            "不隐藏窗口"});
            this.comboBox1.Location = new System.Drawing.Point(13, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(78, 20);
            this.comboBox1.TabIndex = 2;
            // 
            // facebutton
            // 
            this.facebutton.Location = new System.Drawing.Point(427, 12);
            this.facebutton.Name = "facebutton";
            this.facebutton.Size = new System.Drawing.Size(75, 25);
            this.facebutton.TabIndex = 3;
            this.facebutton.Text = "识别人脸";
            this.facebutton.UseVisualStyleBackColor = true;
            this.facebutton.Click += new System.EventHandler(this.facebutton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(346, 12);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 25);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "保存";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(265, 12);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(75, 25);
            this.reset.TabIndex = 5;
            this.reset.Text = "重置";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "全屏",
            "手动框选"});
            this.comboBox2.Location = new System.Drawing.Point(97, 15);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(78, 20);
            this.comboBox2.TabIndex = 6;
            // 
            // ScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 431);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.facebutton);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.screenbutton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ScreenForm";
            this.Text = "SceenForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button screenbutton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button facebutton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}