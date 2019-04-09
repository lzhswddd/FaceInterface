using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperateCamera
{
    class FileTools
    {
        public static List<string> FindFile(string sSourcePath)
        {
            List<String> list = new List<string>();
            //遍历文件夹
            DirectoryInfo theFolder = new DirectoryInfo(sSourcePath);
            FileInfo[] thefileInfo = theFolder.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo NextFile in thefileInfo)  //遍历文件
                list.Add(NextFile.FullName);
            //遍历子文件夹
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                //list.Add(NextFolder.ToString());
                FileInfo[] fileInfo = NextFolder.GetFiles("*.*", SearchOption.AllDirectories);
                foreach (FileInfo NextFile in fileInfo)  //遍历文件              
                    list.Add(NextFile.FullName);
            }
            return list;
        }
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        //如果 使用了 streamreader 在删除前 必须先关闭流 ，否则无法删除 sr.close();
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
                Directory.Delete(srcPath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static string GetImagePath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "图像文件|*.bmp;*.jpg;*.png";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string type = fileDialog.FileName.Substring(fileDialog.FileName.LastIndexOf('.') + 1);
                if (type == "bmp" || type == "jpg" || type == "png")
                {
                    return fileDialog.FileName;
                }
                else
                {
                    MessageBox.Show("请选择图像文件!");
                }
            }
            return null;
        }
        public static string GetVideoPath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "视频文件|*.avi;*.rmvb;*.mp4;*.wmv";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string type = fileDialog.FileName.Substring(fileDialog.FileName.LastIndexOf('.') + 1);
                if (type == "avi" || type == "rmvb" || type == "wmv" || type == "mp4")
                {
                    return fileDialog.FileName;
                }
                else
                {
                    MessageBox.Show("请选择视频文件!");
                }
            }
            return null;
        }
        public static string ShowSaveImageDialog()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "位图(*.bmp)|*.bmp|有损压缩位图(*.jpg)|*.jpg|无损压缩位图(*.png)|*.png";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string type = fileDialog.FileName.Substring(fileDialog.FileName.LastIndexOf('.') + 1);
                if (type == "bmp" || type == "jpg" || type == "png")
                {
                    return fileDialog.FileName;
                }
                else
                {
                    MessageBox.Show("请选择图像文件!");
                }
            }
            return null;
        }      
    }
}
