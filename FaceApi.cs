using System;
using System.Runtime.InteropServices;

namespace OperateCamera
{
    class FaceApi
    {
        [DllImport("FaceRecognization.dll", EntryPoint = "create_load_models", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static void CreateLoadModels(string model_path = "./models");
        [DllImport("FaceRecognization.dll", EntryPoint = "del_model", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static void DelModel();
        [DllImport("FaceRecognization.dll", EntryPoint = "findFaceP", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static bool FindFacePtr(IntPtr image, int width, int height, int step, int channels,
            out int xLeft, out int yLeft, out int rectWitdh, out int rectHeight, int[] landMark, int minarea = 0);
        [DllImport("FaceRecognization.dll", EntryPoint = "findFaceP_", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static bool FindFacePtr_(IntPtr image, int width, int height, int step, int channels,
            out int xLeft, out int yLeft, out int xRight, out int yRight, int[] landMark, int minarea = 0);
        [DllImport("FaceRecognization.dll", EntryPoint = "landMarkLen", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static int LandMarkLen();
        [DllImport("FaceRecognization.dll", EntryPoint = "featureVecLen", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static int FeatureVecLen();
        [DllImport("FaceRecognization.dll", EntryPoint = "featureVecP", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static bool FeatureVecPtr(IntPtr image, int width, int height, int step, int channels, int[] landMark, float[] fea);
        [DllImport("FaceRecognization.dll", EntryPoint = "distance", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static double Distance(float[] fea_1, float[] fea_2);
        [DllImport("FaceRecognization.dll", EntryPoint = "drawFaces", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static int DrawFaces(IntPtr image, int width, int height, int step, int channels, int minarea = 0, bool drawmark = true, bool rand_color = true, int r = 255, int g = 0, int b = 0);
        [DllImport("FaceRecognization.dll", EntryPoint = "FaceRecognition", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static double FaceRecognition(string img1, string img2);

        [DllImport("FaceRecognization.dll", EntryPoint = "videoFrame", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static void VideoFrame(string video_path, videoDllcallBack frame, bool isResize = true, int width = 800, int height = 600);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void videoDllcallBack(IntPtr scan0, int width, int height, int step, out bool stop);
        [DllImport("FaceRecognization.dll", EntryPoint = "getVideoFrame", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static bool GetVideoFrame(ImageDllcallBack frame, bool isResize = true, int width = 800, int height = 600);
        [DllImport("FaceRecognization.dll", EntryPoint = "openVideo", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static bool OpenVideo(string path);
        [DllImport("FaceRecognization.dll", EntryPoint = "closeVideo", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static void CloseVideo();
        [DllImport("FaceRecognization.dll", EntryPoint = "catchScreen", SetLastError = true, CharSet = CharSet.Ansi)]
        public extern static void CatchFrame(ImageDllcallBack frame, bool isResize = true, int width = 800, int height = 600);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ImageDllcallBack(IntPtr scan0, int width, int height, int step);
    }
}
