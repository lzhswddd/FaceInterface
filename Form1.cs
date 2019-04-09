using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AForge.Video.DirectShow;
using System.Threading;
using System.Drawing;

namespace OperateCamera
{
    public partial class Form1 : Form
    {
        private bool flag;
        private bool closing = false;
        private bool addIDAllFlag = false;
        private bool isDetectrunning = false;
        private bool isFeaRunning = false;
        private double threshold = 0.4;
        private string fileDir = "./data";
        private string filePath = "./data/facedata.fd";
        private string imageDir = "./images";
        private List<FaceData> facedata = null;
        private Thread thread = null;
        private Thread addIDThread = null;
        private Thread addIDAllThread = null;
        private Thread matchingThread = null;
        public Thread daemonThread = null;
        private FilterInfoCollection videoDevices;
        delegate void SetBitmapCallback(string result);
        delegate void SetTestback(string test);
        delegate void SetIDback(FeatureData feature, string data, float[] fea, Image img);
        delegate void SetSearchback(Image img);
        delegate void SetFaceback(Image img, string savename);
        delegate void SetMatchback(Image img, string result);
        delegate void CloseCallback(ThreadData threadDatas);
        delegate void CloseFormback();
        public Form1()
        {
            InitializeComponent();
            try
            {
                FaceApi.CreateLoadModels();
            }
            catch
            {
                MessageBox.Show("模型导入失败!");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (searchFace.Visible == true)
                searchFace.Visible = false;
            try
            {
                // 枚举所有视频输入设备
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                foreach (FilterInfo device in videoDevices)
                {
                    tscbxCameras.Items.Add(device.Name);
                    VideoCaptureDevice videoSource = new VideoCaptureDevice(device.MonikerString);
                    foreach (VideoCapabilities videoResolution in videoSource.VideoCapabilities)
                    {
                        string info = videoResolution.FrameSize.Width + "*" + videoResolution.FrameSize.Height;
                        if (resolutionBox.Items.Count == 0 || !resolutionBox.Items.Contains(info))
                            resolutionBox.Items.Add(info);
                    }
                }
                resolutionBox.SelectedIndex = 0;
                tscbxCameras.SelectedIndex = 0;
                userBox.SelectedIndex = 0;
            }
            catch (ApplicationException)
            {
                tscbxCameras.Items.Add("No local capture devices");
                videoDevices = null;
                userBox.SelectedIndex = 1;
            }
            loadFaceData();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult result = MessageBox.Show("你确定要关闭吗！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            //if (result == DialogResult.OK)
            //{
            //    e.Cancel = false;  //点击OK  
            //    closing = true;
            //btnClose_Click(null, null);
            //saveFaceData();
            //DelModel();
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
            if (!closing)
            {
                closing = true;
                CloseThread();
                if (addIDThread != null && addIDThread.ThreadState != ThreadState.Stopped)
                {
                    addIDThread.Abort();
                    addIDThread.Join();
                    addIDThread = null;
                }
                if (addIDAllThread != null && addIDAllThread.ThreadState != ThreadState.Stopped)
                {
                    addIDAllThread.Abort();
                    addIDAllThread.Join();
                    addIDAllThread = null;
                }
                if (matchingThread != null && matchingThread.ThreadState != ThreadState.Stopped)
                {
                    matchingThread.Abort();
                    matchingThread.Join();
                    matchingThread = null;
                }
                if (thread != null && thread.ThreadState != ThreadState.Stopped)
                {
                    thread.Abort();
                    thread.Join();
                    thread = null;
                }
                if (daemonThread != null && daemonThread.ThreadState != ThreadState.Stopped)
                {
                    daemonThread.Join();
                    daemonThread = null;
                }
            }
            CloseCamera();
            //while (daemonThread != null && daemonThread.ThreadState != ThreadState.Stopped) ;
            saveFaceData();
            try
            {
                FaceApi.DelModel();
            }
            catch { }
        }
        public void StopThread(object obj)
        {
            ThreadData td = (ThreadData)obj;
            if (td == null) return;
            if (td.thread != null)
            {
                while (td.thread.ThreadState != ThreadState.Stopped && td.thread.ThreadState != ThreadState.WaitSleepJoin && td.thread.ThreadState != ThreadState.Aborted) ;
            }
            td.flag = true;
            CloseThread(td);
        }
        public void CloseThread(ThreadData td)
        {
            if (InvokeRequired)//调用和创建该控件的线程是不同的线程，必须调用Invoke方法
            {
                if (closing) return;
                //创建该方法的委托实例
                CloseCallback d = new CloseCallback(CloseThread);
                //调用该委托实例，并传递参数，参数为object类型，使用this调用Invoke（this为当前窗体，是创建该窗体控件的线程）
                Invoke(d, new object[] { td });//this指定创建该控件的线程来Invoke（调用）
            }
            else//调用与创建该控件的线程是同一个线程
            {
                td.callBack(td);
            }
        }
        public void CloseThread()
        {
            flag = false;
            addIDAllFlag = false;
            ThreadData td1 = CreateCloseAddID();
            ThreadData td2 = CreateCloseAddIDall();
            ThreadData td3 = CreateCloseMatch();
            ThreadData td4 = CreateCloseFace();
            QueueThreadData queue = new QueueThreadData();
            queue.threadDatas.Add(td1);
            queue.threadDatas.Add(td2);
            queue.threadDatas.Add(td3);
            queue.threadDatas.Add(td4);
            queue.Start(this);
        }
        private ThreadData CreateCloseMatch()
        {
            ThreadData threadData = new ThreadData(matchingThread, matchingThread == null || matchingThread.ThreadState == ThreadState.Stopped);
            threadData.callBack = new ThreadData.CallBack(CloseMatchCallBack);
            return threadData;
        }
        private ThreadData CreateCloseFace()
        {
            ThreadData threadData = new ThreadData(thread, thread == null || thread.ThreadState == ThreadState.Stopped);
            threadData.callBack = new ThreadData.CallBack(CloseFaceCallBack);
            return threadData;
        }
        private ThreadData CreateCloseAddID()
        {
            ThreadData threadData = new ThreadData(addIDThread, addIDThread == null || addIDThread.ThreadState == ThreadState.Stopped);
            threadData.callBack = new ThreadData.CallBack(CloseAddIDCallBack);
            return threadData;
        }
        private ThreadData CreateCloseAddIDall()
        {
            ThreadData threadData = new ThreadData(addIDAllThread, addIDAllThread == null || addIDAllThread.ThreadState == ThreadState.Stopped);
            threadData.callBack = new ThreadData.CallBack(CloseAddIDAllCallBack);
            return threadData;
        }
        private void CloseFaceCallBack(ThreadData td)
        {
            if (td.flag)
            {
                if (faceBox.BackgroundImage != null)
                {
                    faceBox.BackgroundImage.Dispose();
                    faceBox.BackgroundImage = null;
                }
                if (searchFace.Visible == true)
                    searchFace.Visible = false;
            }
            else
            {
                StopThread(td);
            }
        }
        private void CloseCamera()
        {
            videoSourcePlayer.SignalToStop();
            videoSourcePlayer.WaitForStop();
            label2.Text = "摄像头关闭";
        }
        private void CloseAddIDCallBack(ThreadData td)
        {
            if (!td.flag)
            {
                StopThread(td);
            }
        }
        private void CloseAddIDAllCallBack(ThreadData td)
        {
            if (!td.flag)
            {
                StopThread(td);
            }
        }
        private void CloseMatchCallBack(ThreadData td)
        {
            if (!td.flag)
            {
                StopThread(td);
            }
            else
            {
                SetBox("-6");
            }
        }
        private void saveFaceData()
        {
            if (facedata.Count != 0)
            {
                if (Directory.Exists(fileDir))
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            foreach (FaceData fea in facedata)
                            {
                                sw.WriteLine("ID " + fea.name);
                                foreach (float[] vec in fea.feavec)
                                {
                                    foreach (float iter in vec)
                                    {
                                        sw.Write(iter + " ");
                                    }
                                    sw.Write("\n");
                                }
                                sw.WriteLine("end");
                            }
                            //清空缓冲区
                            sw.Flush();
                            //关闭流
                            sw.Close();
                        }
                        fs.Close();
                    }
                }
            }
            else
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
        private void loadFaceData()
        {
            facedata = new List<FaceData>();
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            if (!Directory.Exists(imageDir))
            {
                Directory.CreateDirectory(imageDir);
            }
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
                {
                    String line;
                    FaceData face = new FaceData();
                    while ((line = sr.ReadLine()) != null)
                    {
                        String[] data = line.Split(' ');
                        if (data[0] == "ID")
                        {
                            face.name = data[1];
                        }
                        else if (data[0] == "end")
                        {
                            facedata.Add(face);
                            face = new FaceData();
                        }
                        else
                        {
                            float[] vec = new float[FaceApi.FeatureVecLen()];
                            for (int idx = 0; idx < vec.Length; ++idx)
                            {
                                vec[idx] = Convert.ToSingle(data[idx]);
                            }
                            face.feavec.Add(vec);
                        }
                    }
                }
                if (facedata.Count != 0)
                {
                    foreach (FaceData face in facedata)
                    {
                        TreeNode[] nodes = new TreeNode[face.feavec.Count];
                        int idx = 0;
                        foreach (float[] fea in face.feavec)
                        {
                            float sum = 0;
                            foreach (float single in fea)
                            {
                                sum += single;
                            }
                            nodes[idx] = new TreeNode(sum + "");
                            idx += 1;
                        }
                        TreeNode treeNode = new TreeNode(face.name, nodes);
                        treeView1.Nodes.Add(treeNode);
                    }
                }
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            CameraConn();
        }
        private void CameraConn()
        {
            try
            {
                if (!videoSourcePlayer.IsRunning)
                {
                    VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[tscbxCameras.SelectedIndex].MonikerString);
                    videoSource.VideoResolution = videoSource.VideoCapabilities[resolutionBox.SelectedIndex];
                    videoSourcePlayer.VideoSource = videoSource;
                    videoSourcePlayer.Start();
                    label2.Text = "摄像头连接成功";
                    if (searchFace.Visible == true)
                        searchFace.Visible = false;
                }
                else
                {
                    MessageBox.Show("相机已开启!");
                }
            }
            catch (Exception ex)
            {
                label2.Text = "摄像头连接失败: " + ex.Message;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            flag = false;
            ThreadData td1 = CreateCloseMatch();
            ThreadData td2 = CreateCloseFace();
            QueueThreadData queue = new QueueThreadData();
            queue.threadDatas.Add(td1);
            queue.threadDatas.Add(td2);
            queue.Start(this);
            CloseCamera();
        }
        private void Photograph_Click(object sender, EventArgs e)
        {
            try
            {
                if (videoSourcePlayer.IsRunning)
                {
                    if (thread == null || thread.ThreadState != ThreadState.Running)
                    {
                        while (videoSourcePlayer.GetCurrentVideoFrame() == null) ;
                        if (searchFace.Visible == false)
                            searchFace.Visible = true;
                        thread = new Thread(new ThreadStart(play_video));
                        thread.Start();
                    }
                    else
                    {
                        MessageBox.Show("正在识别!");
                    }
                }
                else
                {
                    label2.Text = "摄像头未运行!";
                }
            }
            catch (Exception ex)
            {
                label2.Text = "摄像头异常：" + ex.Message;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            flag = false;
            label2.Text = "关闭人脸检测";
            ThreadData td1 = CreateCloseMatch();
            ThreadData td2 = CreateCloseFace();
            QueueThreadData queue = new QueueThreadData();
            queue.threadDatas.Add(td1);
            queue.threadDatas.Add(td2);
            queue.Start(this);
        }
        public void play_video()
        {
            flag = true;
            SetTestBox("正在进行人脸检测");
            while (flag)
            {
                try
                {
                    using (Bitmap bitmapSource = videoSourcePlayer.GetCurrentVideoFrame())
                    {
                        if (bitmapSource != null)
                        {
                            if (!isDetectrunning)
                            {
                                BitmapData bitmapData = bitmapSource.LockBits(new Rectangle(0, 0, bitmapSource.Width, bitmapSource.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                                int[] landmark = new int[FaceApi.LandMarkLen()];
                                isDetectrunning = true;
                                bool success = FaceApi.FindFacePtr_(bitmapData.Scan0, bitmapData.Width, bitmapData.Height, bitmapData.Stride, 3, out int x1, out int y1, out int x2, out int y2, landmark);
                                isDetectrunning = false;
                                bitmapSource.UnlockBits(bitmapData);
                                if (success)
                                {
                                    if (!closing)
                                    {
                                        if (facedata.Count != 0)
                                        {
                                            if (matchingThread == null || matchingThread.ThreadState != ThreadState.Running)
                                            {
                                                for (int idx = 0; idx < landmark.Length / 2; ++idx)
                                                {
                                                    landmark[idx * 2] -= x1;
                                                    landmark[idx * 2 + 1] -= y1;
                                                }
                                                MatchData matchData = new MatchData(bitmapSource.Clone(new Rectangle(x1, y1, x2 - x1, y2 - y1), PixelFormat.Format24bppRgb), landmark);
                                                matchingThread = new Thread(new ParameterizedThreadStart(Matching));
                                                matchingThread.Start(matchData);
                                            }
                                        }
                                        SetSearchBox(bitmapSource.Clone(new Rectangle(x1, y1, x2 - x1, y2 - y1), PixelFormat.Format24bppRgb));
                                        SetBox("0");
                                    }
                                }
                                else
                                {
                                    if (!closing)
                                    {
                                        SetBox("-6");
                                    }
                                }
                            }
                            else
                            {
                                if (!closing)
                                    SetBox("-6");
                            }
                        }
                        else
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (!closing)
                        SetBox("摄像头异常：" + ex.Message);
                }
            }
            if (!closing)
            {
                SetBox("连接摄像头");
                SetTestBox("已停止人脸检测");
            }
        }
        private void SetTestBox(string result)
        {
            //InvokeRequired属性需要比较调用线程和创建线程的线程ID，如果俩线程ID不同，则返回true
            if (label5.InvokeRequired)//调用和创建该控件的线程是不同的线程，必须调用Invoke方法
            {
                if (!closing)
                {
                    //创建该方法的委托实例
                    SetTestback d = new SetTestback(SetTestBox);
                    //调用该委托实例，并传递参数，参数为object类型，使用this调用Invoke（this为当前窗体，是创建该窗体控件的线程）
                    Invoke(d, new object[] { result });//this指定创建该控件的线程来Invoke（调用）
                }
            }
            else//调用与创建该控件的线程是同一个线程
            {
                if (!closing)
                {
                    label5.Text = result;
                }
            }
        }
        private void SetBox(string result)
        {
            //InvokeRequired属性需要比较调用线程和创建线程的线程ID，如果俩线程ID不同，则返回true
            if (searchFace.InvokeRequired)//调用和创建该控件的线程是不同的线程，必须调用Invoke方法
            {
                if (!closing)
                {
                    //创建该方法的委托实例
                    SetBitmapCallback d = new SetBitmapCallback(SetBox);
                    //调用该委托实例，并传递参数，参数为object类型，使用this调用Invoke（this为当前窗体，是创建该窗体控件的线程）
                    Invoke(d, new object[] { result });//this指定创建该控件的线程来Invoke（调用）
                }
            }
            else//调用与创建该控件的线程是同一个线程
            {
                if (closing) return;
                if (result != "0")
                {
                    if (searchFace.BackgroundImage != null)
                    {
                        searchFace.BackgroundImage.Dispose();
                        searchFace.BackgroundImage = null;
                    }
                    if (faceBox.BackgroundImage != null)
                    {
                        faceBox.BackgroundImage.Dispose();
                        faceBox.BackgroundImage = null;
                    }
                }
                switch (result)
                {
                    case "0": result = "检测到人脸！"; break;
                    case "-1": result = "没有找到人脸检测模型！"; break;
                    case "-2": result = "图像读取失败！"; break;
                    case "-3": result = "检测程序运行失败！"; break;
                    case "-4": result = "绘制图像失败！"; break;
                    case "-5": result = "保存图像失败！"; break;
                    case "-6": result = "没有检测到人脸！"; break;
                }
                label2.Text = result;
            }
        }
        private void SetFaceBox(Image img, string savename)
        {
            //InvokeRequired属性需要比较调用线程和创建线程的线程ID，如果俩线程ID不同，则返回true
            if (feaBox.InvokeRequired)//调用和创建该控件的线程是不同的线程，必须调用Invoke方法
            {
                if (!closing)
                {
                    //创建该方法的委托实例
                    SetFaceback d = new SetFaceback(SetFaceBox);
                    //调用该委托实例，并传递参数，参数为object类型，使用this调用Invoke（this为当前窗体，是创建该窗体控件的线程）
                    Invoke(d, new object[] { img, savename });//this指定创建该控件的线程来Invoke（调用）
                }
            }
            else//调用与创建该控件的线程是同一个线程
            {
                if (closing) return;
                if (img != null)
                {
                    if (feaBox.BackgroundImage != null)
                    {
                        feaBox.BackgroundImage.Dispose();
                        feaBox.BackgroundImage = null;
                    }
                    feaBox.BackgroundImage = img;
                    using (Bitmap temp = new Bitmap(img))
                    {
                        temp.Save(savename + ".jpg");
                    }
                }
            }
        }
        private void SetSearchBox(Image img)
        {
            //InvokeRequired属性需要比较调用线程和创建线程的线程ID，如果俩线程ID不同，则返回true
            if (searchFace.InvokeRequired)//调用和创建该控件的线程是不同的线程，必须调用Invoke方法
            {
                if (!closing)
                {
                    //创建该方法的委托实例
                    SetSearchback d = new SetSearchback(SetSearchBox);
                    //调用该委托实例，并传递参数，参数为object类型，使用this调用Invoke（this为当前窗体，是创建该窗体控件的线程）
                    Invoke(d, new object[] { img });//this指定创建该控件的线程来Invoke（调用）
                }
            }
            else//调用与创建该控件的线程是同一个线程
            {
                if (closing) return;
                if (searchFace.BackgroundImage != null)
                {
                    searchFace.BackgroundImage.Dispose();
                    searchFace.BackgroundImage = null;
                }
                try
                {
                    searchFace.BackgroundImage = img;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void SetMatchBox(Image img, string result)
        {
            //InvokeRequired属性需要比较调用线程和创建线程的线程ID，如果俩线程ID不同，则返回true
            if (faceBox.InvokeRequired)//调用和创建该控件的线程是不同的线程，必须调用Invoke方法
            {
                if (!closing)
                {
                    if (matchingThread.ThreadState != ThreadState.Running) return;
                    //创建该方法的委托实例
                    SetMatchback d = new SetMatchback(SetMatchBox);
                    //调用该委托实例，并传递参数，参数为object类型，使用this调用Invoke（this为当前窗体，是创建该窗体控件的线程）
                    Invoke(d, new object[] { img, result });//this指定创建该控件的线程来Invoke（调用）
                }
            }
            else//调用与创建该控件的线程是同一个线程
            {
                if (closing) return;
                if (thread.ThreadState == ThreadState.Running)
                {
                    if (faceBox.BackgroundImage != null)
                    {
                        faceBox.BackgroundImage.Dispose();
                        faceBox.BackgroundImage = null;
                    }
                    if (img != null)
                    {
                        faceBox.BackgroundImage = img;
                    }
                    matchResult.Text = result;
                }
            }
        }
        private void SetIDBox(FeatureData feature, string data, float[] fea, Image img)
        {
            //InvokeRequired属性需要比较调用线程和创建线程的线程ID，如果俩线程ID不同，则返回true
            if (treeView1.InvokeRequired)//调用和创建该控件的线程是不同的线程，必须调用Invoke方法
            {
                if (!closing)
                {
                    //创建该方法的委托实例
                    SetIDback d = new SetIDback(SetIDBox);
                    //调用该委托实例，并传递参数，参数为object类型，使用this调用Invoke（this为当前窗体，是创建该窗体控件的线程）
                    Invoke(d, new object[] { feature, data, fea, img });//this指定创建该控件的线程来Invoke（调用）
                }
            }
            else//调用与创建该控件的线程是同一个线程
            {
                if (closing) return;
                if (!string.IsNullOrEmpty(data))
                {
                    foreach (TreeNode node in treeView1.Nodes)
                    {
                        if (node.GetNodeCount(true) != 0)
                        {
                            if (feature.itemID == node.Text)
                            {
                                TreeNode treeNode = node;
                                treeView1.SelectedNode = treeNode;
                                treeView1.SelectedNode.Nodes.Add(data);
                                facedata[node.Index].feavec.Add(fea);
                                SetFaceBox(img, imageDir + "/" + facedata[node.Index].name + "/" + facedata[node.Index].feavec.Count);
                                return;
                            }
                        }
                    }

                    TreeNode[] Data = new TreeNode[1];
                    Data[0] = new TreeNode(data);
                    TreeNode ID = new TreeNode(feature.itemID, Data);
                    treeView1.Nodes.Add(ID);
                    treeView1.SelectedNode = ID;
                    FaceData face = new FaceData(fea, feature.itemID);
                    facedata.Add(face);
                    if (!Directory.Exists(imageDir + "/" + feature.itemID))
                    {
                        Directory.CreateDirectory(imageDir + "/" + feature.itemID);
                    }
                    SetFaceBox(img, imageDir + "/" + feature.itemID + "/" + facedata[facedata.Count - 1].feavec.Count);

                }
            }
        }
        public void GetFeatureVec(object obj)
        {
            FeatureData feature = (FeatureData)obj;
            using (Bitmap bitmapSource = feature.localRead ? new Bitmap(feature.localFileName) : videoSourcePlayer.GetCurrentVideoFrame())
            {
                if (bitmapSource != null)
                {
                    //Bitmap bitmapSource = BitmapHelper.ScaleToSize(bitmapVideo, 256, 256);
                    float[] vec = new float[FaceApi.FeatureVecLen()];
                    int[] landmark = new int[FaceApi.LandMarkLen()];
                    BitmapData bitmapData = bitmapSource.LockBits(new Rectangle(0, 0, bitmapSource.Width, bitmapSource.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                    while (isDetectrunning) { Console.WriteLine("waitting Detect"); };
                    isDetectrunning = true;
                    bool success = FaceApi.FindFacePtr(bitmapData.Scan0, bitmapData.Width, bitmapData.Height, bitmapData.Stride, 3, out int x1, out int y1, out int width, out int height, landmark);
                    isDetectrunning = false;
                    bitmapSource.UnlockBits(bitmapData);
                    if (success)
                    {
                        bitmapData = bitmapSource.LockBits(new Rectangle(x1, y1, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                        for (int idx = 0; idx < landmark.Length / 2; ++idx)
                        {
                            landmark[idx * 2] -= x1;
                            landmark[idx * 2 + 1] -= y1;
                        }
                        while (isFeaRunning) { Console.WriteLine("waitting FeaRunning"); };
                        isFeaRunning = true;
                        success = FaceApi.FeatureVecPtr(bitmapData.Scan0, bitmapData.Width, bitmapData.Height, bitmapData.Stride, 3, landmark, vec);
                        isFeaRunning = false;
                        bitmapSource.UnlockBits(bitmapData);
                    }
                    if (success)
                    {
                        float sum = 0;
                        for (int idx = 0; idx < vec.Length; ++idx)
                            sum += vec[idx];
                        SetIDBox(feature, sum + "", vec, bitmapSource.Clone(new Rectangle(x1, y1, width, height), PixelFormat.Format24bppRgb));
                    }
                    else
                    {
                        MessageBox.Show("添加数据失败!");
                    }
                }
            }
        }
        public void Matching(object obj)
        {
            MatchData matchData = (MatchData)obj;
            if (matchData.bitmapSource != null)
            {
                float[] vec = new float[FaceApi.FeatureVecLen()];
                BitmapData bitmapData = matchData.bitmapSource.LockBits(new Rectangle(0, 0, matchData.bitmapSource.Width, matchData.bitmapSource.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                while (isFeaRunning) { Console.WriteLine("Matching is Waitting"); }
                isFeaRunning = true;
                bool success = FaceApi.FeatureVecPtr(bitmapData.Scan0, bitmapData.Width, bitmapData.Height, bitmapData.Stride, 3, matchData.landmark, vec);
                isFeaRunning = false;
                matchData.bitmapSource.UnlockBits(bitmapData);
                double maxdata = 0;
                int maxidx1 = 0;
                int maxidx2 = 0;
                int index = 0;
                foreach (FaceData face in facedata)
                {
                    for (int idx = 0; idx < face.feavec.Count; ++idx)
                    {
                        double result = FaceApi.Distance(vec, face.feavec[idx]);
                        if (result > maxdata)
                        {
                            maxdata = result;
                            maxidx2 = idx;
                            maxidx1 = index;
                        }
                        Console.WriteLine(result + "");
                    }
                    index += 1;
                }
                if (maxdata > threshold)
                {
                    if (!closing)
                    {
                        string path = imageDir + "/" + facedata[maxidx1].name + "/" + (maxidx2 + 1) + ".jpg";
                        SetMatchBox(BitmapHelper.ReadImageFile(path), "匹配ID：" + facedata[maxidx1].name/*maxdata + ""*/);
                    }
                }
                else
                {
                    if (!closing)
                    {
                        SetMatchBox(null, "");
                    }
                }
            }
            matchData.Dispose();
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            if (feaBox.BackgroundImage != null)
            {
                feaBox.BackgroundImage.Dispose();
                feaBox.BackgroundImage = null;
            }
            if (faceBox.BackgroundImage != null)
            {
                faceBox.BackgroundImage.Dispose();
                faceBox.BackgroundImage = null;
            }
        }
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode != null)
                {
                    if (treeView1.SelectedNode.GetNodeCount(true) == 0)
                    {
                        string path = imageDir + "/" + facedata[treeView1.SelectedNode.Parent.Index].name + "/" + (treeView1.SelectedNode.Index + 1) + ".jpg";
                        feaBox.BackgroundImage = BitmapHelper.ReadImageFile(path);
                    }
                    else
                    {
                        ResetFeaBox();
                    }
                }
                else
                {
                    ResetFeaBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ReSet_Click(object sender, EventArgs e)
        {
            if (faceBox.BackgroundImage != null)
            {
                faceBox.BackgroundImage.Dispose();
                faceBox.BackgroundImage = null;
            }
            ResetFeaBox();
            HideMethod(idText.Text);
        }
        private void RemoveItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (feaBox.BackgroundImage != null)
                {
                    feaBox.BackgroundImage.Dispose();
                    feaBox.BackgroundImage = null;
                }
                string path;
                if (treeView1.SelectedNode.GetNodeCount(true) != 0)
                {
                    path = imageDir + "/" + facedata[treeView1.SelectedNode.Index].name;
                    if (Directory.Exists(path))
                    {
                        FileTools.DelectDir(path);
                    }
                    facedata.RemoveAt(treeView1.SelectedNode.Index);
                }
                else
                {
                    path = imageDir + "/" + facedata[treeView1.SelectedNode.Parent.Index].name;
                    if (Directory.Exists(path))
                    {
                        string file = imageDir + "/" + facedata[treeView1.SelectedNode.Parent.Index].name + "/" + (treeView1.SelectedNode.Index + 1) + ".jpg";
                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }
                    }
                    facedata[treeView1.SelectedNode.Parent.Index].feavec.RemoveAt(treeView1.SelectedNode.Index);
                    if (treeView1.SelectedNode.Parent.GetNodeCount(true) == 1)
                    {
                        FileTools.DelectDir(path);
                        facedata.RemoveAt(treeView1.SelectedNode.Parent.Index);
                        treeView1.SelectedNode.Parent.Remove();
                        return;
                    }
                }
                treeView1.SelectedNode.Remove();
            }
            else
                MessageBox.Show("请选择要删除的节点!");
        }
        private void AddItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(idText.Text.Trim()))
            {
                string id = idText.Text.Trim();
                if (facedata.Count != 0)
                {
                    foreach (FaceData fd in facedata)
                    {
                        if (fd.name == id)
                        {
                            MessageBox.Show("加入人脸数据失败: 重复的ID");
                            return;
                        }
                    }
                }
                if (addIDThread == null || addIDThread.ThreadState == ThreadState.Stopped)
                {
                    if (userBox.SelectedIndex == 0)
                    {
                        if (videoSourcePlayer.IsRunning)
                        {
                            FeatureData feature = new FeatureData(id);
                            addIDThread = new Thread(new ParameterizedThreadStart(GetFeatureVec));
                            addIDThread.Start(feature);
                            idText.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("摄像头未运行!");
                        }
                    }
                    else if (userBox.SelectedIndex == 1)
                    {
                        string fileName = FileTools.GetImagePath();
                        if (fileName != null)
                        {
                            FeatureData feature = new FeatureData(id, fileName);
                            addIDThread = new Thread(new ParameterizedThreadStart(GetFeatureVec));
                            addIDThread.Start(feature);
                            idText.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("正在添加数据中, 请等待!");
                }
            }
            else
            {
                MessageBox.Show("error: ID 为空!");
            }
        }
        private void InsertItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode != null)
                {
                    if (treeView1.SelectedNode.GetNodeCount(true) != 0)
                    {
                        if (addIDThread == null || addIDThread.ThreadState == ThreadState.Stopped)
                        {
                            if (userBox.SelectedIndex == 0)
                            {
                                if (videoSourcePlayer.IsRunning)
                                {
                                    FeatureData feature = new FeatureData(treeView1.SelectedNode.Text);
                                    addIDThread = new Thread(new ParameterizedThreadStart(GetFeatureVec));
                                    addIDThread.Start(feature);
                                    idText.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("摄像头未运行!");
                                }
                            }
                            else if (userBox.SelectedIndex == 1)
                            {
                                string fileName = FileTools.GetImagePath();
                                if (fileName != null)
                                {
                                    FeatureData feature = new FeatureData(treeView1.SelectedNode.Text, fileName);
                                    addIDThread = new Thread(new ParameterizedThreadStart(GetFeatureVec));
                                    addIDThread.Start(feature);
                                    idText.Text = "";
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("正在添加数据中, 请等待!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("error: 请选择父节点!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void addItemsbutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dirDialog = new FolderBrowserDialog();
            dirDialog.Description = "请选择文件路径";
            DialogResult result = dirDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    if(addIDAllThread == null || addIDAllThread.ThreadState == ThreadState.Stopped)
                    {
                        List<string> files = FileTools.FindFile(dirDialog.SelectedPath);
                        addIDAllThread = new Thread(new ParameterizedThreadStart(AddItemAll));
                        addIDAllThread.Start(files);
                    }
                    else
                    {
                        MessageBox.Show("正在添加数据中, 请等待!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void AddItemAll(object obj)
        {
            addIDAllFlag = true;
            List<string> files = (List<string>)obj;
            string itemid = "";
            foreach (string file in files)
            {
                if (!addIDAllFlag) break;
                string[] end = file.Split('.');
                if (end[end.Length - 1] == "jpg" || end[end.Length - 1] == "bmp" || end[end.Length - 1] == "png")
                {
                    string[] substr = file.Split('\\');
                    FeatureData feature = new FeatureData(itemid, file);
                    itemid = substr[substr.Length - 2];
                    feature.itemID = itemid;
                    GetFeatureVec(feature);
                }
            }
        }
        private void ResetFeaBox()
        {
            if (feaBox.BackgroundImage != null)
            {
                feaBox.BackgroundImage.Dispose();
                feaBox.BackgroundImage = null;
            }
        }       
        private void stopAddsbutton_Click(object sender, EventArgs e)
        {
            addIDAllFlag = false;
            ThreadData td1 = CreateCloseAddIDall();
            QueueThreadData queue = new QueueThreadData();
            queue.threadDatas.Add(td1);
            queue.Start(this);
        }
        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if(MouseButtons.Right == e.Button)
            {
                ResetFeaBox();
            }
        }
        private void idText_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == 13)
            {
                HideMethod(idText.Text);
            }
        }
        private void HideMethod(string cmd)
        {
            if (cmd == "distance")
            {
                Hide();
                CloseThread();
                DistFrom testFrom = new DistFrom();
                testFrom.ShowDialog();
                Show();
            }
            if (cmd == "drawface")
            {
                Hide();
                CloseThread();
                DrawFaceForm faceForm = new DrawFaceForm();
                faceForm.ShowDialog();
                Show();
            }
            if (cmd == "video")
            {
                Hide();
                CloseThread();
                VideoForm videoForm = new VideoForm();
                videoForm.ShowDialog();
                Show();
            }
            if (cmd == "screen")
            {
                Hide();
                CloseThread();
                ScreenForm videoForm = new ScreenForm();
                videoForm.ShowDialog();
                Show();
            }
        }
        private void selectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (selectBox.SelectedIndex)
            {
                case 0: HideMethod("video"); break;
                case 1: HideMethod("drawface"); break;
                case 2: HideMethod("screen"); break;
                case 3: HideMethod("distance"); break;
                default: break;
            }
            selectBox.SelectedIndex = -1;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Environment.Exit(0);
        }
    }
    public class ThreadData
    {
        public Thread thread = null;
        public Thread daemonThread = null;
        public bool flag = false;
        public CallBack callBack = null;
        public delegate void CallBack(ThreadData td);
        public ThreadData(Thread thread, bool flag)
        {
            this.thread = thread;
            this.flag = flag;
        }
    }
    public class QueueThreadData
    {
        public List<ThreadData> threadDatas = null;
        public QueueThreadData()
        {
            threadDatas = new List<ThreadData>();
        }
        public QueueThreadData(ThreadData threadData)
        {
            threadDatas = new List<ThreadData>();
            threadDatas.Add(threadData);
        }
        public void Start(Form1 form)
        {
            List<ThreadData> remove = new List<ThreadData>();
            foreach (ThreadData threadData in threadDatas)
            {
                if (threadData.flag)
                    remove.Add(threadData);
            }
            foreach (ThreadData threadData in remove)
            {
                threadDatas.Remove(threadData);
            }
            if (form.daemonThread == null || form.daemonThread.ThreadState == ThreadState.Stopped)
            {
                form.daemonThread = new Thread(new ParameterizedThreadStart(CloseThread));
                form.daemonThread.Start(form);
            }
        }

        public void CloseThread(object obj)
        {
            Thread thread = null;
            foreach(ThreadData threadData in threadDatas)
            {
                thread = new Thread(new ParameterizedThreadStart((obj as Form1).StopThread));
                thread.Start(threadData);
                thread.Join();
                thread = null;
            }
        }
    }
    public class FaceData
    {
        public string name;
        public List<float[]> feavec = null;
        public FaceData()
        {
            name = "";
            feavec = new List<float[]>();
        }
        public FaceData(float[] fea, string id)
        {
            name = id;
            feavec = new List<float[]>();
            feavec.Add(fea);
        }
    }
    public class MatchData
    {
        public Bitmap bitmapSource = null;
        public int[] landmark = null;
        public MatchData(Image bitmap, int[] landmark)
        {
            bitmapSource = new Bitmap(bitmap);
            this.landmark = (int[])landmark.Clone();
        }
        public void Dispose()
        {
            bitmapSource.Dispose();
            bitmapSource = null;
            landmark = null;
        }
    }
    public class FeatureData
    {
        public bool localRead = false;
        public string itemID = null;
        public string localFileName = "";
        public FeatureData(string itemID)
        {
            this.itemID = itemID;
        }
        public FeatureData(string itemID, string localFileName)
        {
            this.itemID = itemID;
            this.localRead = true;
            this.localFileName = localFileName;
        }
    }
}
