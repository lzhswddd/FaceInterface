namespace OperateCamera
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Photograph = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tscbxCameras = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.searchFace = new System.Windows.Forms.PictureBox();
            this.faceBox = new System.Windows.Forms.PictureBox();
            this.idText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.resolutionBox = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.insertbutton = new System.Windows.Forms.Button();
            this.userBox = new System.Windows.Forms.ComboBox();
            this.feaBox = new System.Windows.Forms.PictureBox();
            this.addItemsbutton = new System.Windows.Forms.Button();
            this.stopAddsbutton = new System.Windows.Forms.Button();
            this.matchResult = new System.Windows.Forms.Label();
            this.selectBox = new System.Windows.Forms.ComboBox();
            this.labelmethod = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.searchFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.feaBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Photograph
            // 
            this.Photograph.Location = new System.Drawing.Point(398, 12);
            this.Photograph.Name = "Photograph";
            this.Photograph.Size = new System.Drawing.Size(75, 52);
            this.Photograph.TabIndex = 3;
            this.Photograph.Text = "开始";
            this.Photograph.UseVisualStyleBackColor = true;
            this.Photograph.Click += new System.EventHandler(this.Photograph_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(317, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 53);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭相机";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tscbxCameras
            // 
            this.tscbxCameras.FormattingEnabled = true;
            this.tscbxCameras.ItemHeight = 12;
            this.tscbxCameras.Location = new System.Drawing.Point(109, 13);
            this.tscbxCameras.Name = "tscbxCameras";
            this.tscbxCameras.Size = new System.Drawing.Size(121, 20);
            this.tscbxCameras.TabIndex = 7;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(236, 11);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 53);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "连接相机";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "视频输入设备：";
            // 
            // videoSourcePlayer
            // 
            this.videoSourcePlayer.Location = new System.Drawing.Point(12, 70);
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.Size = new System.Drawing.Size(640, 360);
            this.videoSourcePlayer.TabIndex = 10;
            this.videoSourcePlayer.Text = "10";
            this.videoSourcePlayer.VideoSource = null;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(479, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 52);
            this.button1.TabIndex = 4;
            this.button1.Text = "停止";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(631, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 14);
            this.label2.TabIndex = 13;
            this.label2.Text = "等待";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(560, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "程序输出：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(560, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "程序状态：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("宋体", 9F);
            this.label5.Location = new System.Drawing.Point(631, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 14);
            this.label5.TabIndex = 15;
            this.label5.Text = "未进行人脸检测：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(970, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 22);
            this.button2.TabIndex = 5;
            this.button2.Text = "加入人脸数据";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.AddItem_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(797, 109);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(269, 317);
            this.treeView1.TabIndex = 9;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
            // 
            // searchFace
            // 
            this.searchFace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.searchFace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchFace.Location = new System.Drawing.Point(560, 72);
            this.searchFace.Name = "searchFace";
            this.searchFace.Size = new System.Drawing.Size(90, 114);
            this.searchFace.TabIndex = 19;
            this.searchFace.TabStop = false;
            // 
            // faceBox
            // 
            this.faceBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.faceBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.faceBox.Location = new System.Drawing.Point(658, 87);
            this.faceBox.Name = "faceBox";
            this.faceBox.Size = new System.Drawing.Size(132, 170);
            this.faceBox.TabIndex = 20;
            this.faceBox.TabStop = false;
            // 
            // idText
            // 
            this.idText.Location = new System.Drawing.Point(797, 12);
            this.idText.Name = "idText";
            this.idText.Size = new System.Drawing.Size(110, 21);
            this.idText.TabIndex = 21;
            this.idText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.idText_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(777, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "ID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "视频分辨率：";
            // 
            // resolutionBox
            // 
            this.resolutionBox.FormattingEnabled = true;
            this.resolutionBox.ItemHeight = 12;
            this.resolutionBox.Location = new System.Drawing.Point(109, 43);
            this.resolutionBox.Name = "resolutionBox";
            this.resolutionBox.Size = new System.Drawing.Size(121, 20);
            this.resolutionBox.TabIndex = 23;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(970, 46);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 22);
            this.button3.TabIndex = 6;
            this.button3.Text = "删除人脸数据";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.RemoveItem_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(796, 46);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(71, 22);
            this.button4.TabIndex = 8;
            this.button4.Text = "重置";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.ReSet_Click);
            // 
            // insertbutton
            // 
            this.insertbutton.Location = new System.Drawing.Point(873, 46);
            this.insertbutton.Name = "insertbutton";
            this.insertbutton.Size = new System.Drawing.Size(91, 22);
            this.insertbutton.TabIndex = 7;
            this.insertbutton.Text = "插入人脸数据";
            this.insertbutton.UseVisualStyleBackColor = true;
            this.insertbutton.Click += new System.EventHandler(this.InsertItem_Click);
            // 
            // userBox
            // 
            this.userBox.FormattingEnabled = true;
            this.userBox.Items.AddRange(new object[] {
            "相机",
            "本地"});
            this.userBox.Location = new System.Drawing.Point(913, 13);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(54, 20);
            this.userBox.TabIndex = 28;
            // 
            // feaBox
            // 
            this.feaBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.feaBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.feaBox.Location = new System.Drawing.Point(658, 263);
            this.feaBox.Name = "feaBox";
            this.feaBox.Size = new System.Drawing.Size(132, 163);
            this.feaBox.TabIndex = 29;
            this.feaBox.TabStop = false;
            // 
            // addItemsbutton
            // 
            this.addItemsbutton.Location = new System.Drawing.Point(797, 74);
            this.addItemsbutton.Name = "addItemsbutton";
            this.addItemsbutton.Size = new System.Drawing.Size(110, 29);
            this.addItemsbutton.TabIndex = 30;
            this.addItemsbutton.Text = "批量导入人脸数据";
            this.addItemsbutton.UseVisualStyleBackColor = true;
            this.addItemsbutton.Click += new System.EventHandler(this.addItemsbutton_Click);
            // 
            // stopAddsbutton
            // 
            this.stopAddsbutton.Location = new System.Drawing.Point(912, 74);
            this.stopAddsbutton.Name = "stopAddsbutton";
            this.stopAddsbutton.Size = new System.Drawing.Size(52, 29);
            this.stopAddsbutton.TabIndex = 31;
            this.stopAddsbutton.Text = "中止";
            this.stopAddsbutton.UseVisualStyleBackColor = true;
            this.stopAddsbutton.Click += new System.EventHandler(this.stopAddsbutton_Click);
            // 
            // matchResult
            // 
            this.matchResult.AutoSize = true;
            this.matchResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.matchResult.Location = new System.Drawing.Point(658, 70);
            this.matchResult.Name = "matchResult";
            this.matchResult.Size = new System.Drawing.Size(0, 14);
            this.matchResult.TabIndex = 32;
            // 
            // selectBox
            // 
            this.selectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectBox.FormattingEnabled = true;
            this.selectBox.Items.AddRange(new object[] {
            "视频",
            "图片",
            "截图",
            "相似度"});
            this.selectBox.Location = new System.Drawing.Point(1020, 79);
            this.selectBox.Name = "selectBox";
            this.selectBox.Size = new System.Drawing.Size(46, 20);
            this.selectBox.TabIndex = 33;
            this.selectBox.SelectedIndexChanged += new System.EventHandler(this.selectBox_SelectedIndexChanged);
            // 
            // labelmethod
            // 
            this.labelmethod.AutoSize = true;
            this.labelmethod.BackColor = System.Drawing.SystemColors.Menu;
            this.labelmethod.Location = new System.Drawing.Point(968, 82);
            this.labelmethod.Name = "labelmethod";
            this.labelmethod.Size = new System.Drawing.Size(53, 12);
            this.labelmethod.TabIndex = 34;
            this.labelmethod.Text = "其他功能";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 438);
            this.Controls.Add(this.selectBox);
            this.Controls.Add(this.matchResult);
            this.Controls.Add(this.stopAddsbutton);
            this.Controls.Add(this.addItemsbutton);
            this.Controls.Add(this.searchFace);
            this.Controls.Add(this.feaBox);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.insertbutton);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.resolutionBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.idText);
            this.Controls.Add(this.faceBox);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tscbxCameras);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.Photograph);
            this.Controls.Add(this.videoSourcePlayer);
            this.Controls.Add(this.labelmethod);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Face";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            ((System.ComponentModel.ISupportInitialize)(this.searchFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.feaBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Photograph;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox tscbxCameras;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PictureBox searchFace;
        private System.Windows.Forms.PictureBox faceBox;
        private System.Windows.Forms.TextBox idText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox resolutionBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button insertbutton;
        private System.Windows.Forms.ComboBox userBox;
        private System.Windows.Forms.PictureBox feaBox;
        private System.Windows.Forms.Button addItemsbutton;
        private System.Windows.Forms.Button stopAddsbutton;
        private System.Windows.Forms.Label matchResult;
        private System.Windows.Forms.ComboBox selectBox;
        private System.Windows.Forms.Label labelmethod;
    }
}

