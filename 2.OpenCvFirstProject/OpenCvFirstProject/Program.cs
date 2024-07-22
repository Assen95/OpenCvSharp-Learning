using System;
using System.IO;
using OpenCvSharp;

namespace OpenCvFirstProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var imagePath = @"C:\temp\lena.jpg";
            Mat src = new Mat(imagePath, ImreadModes.Grayscale);

            Mat dst = new Mat();

            Cv2.Canny(src, dst, 50, 200);

            new OpenCvSharp.Window("src image", src);
            new OpenCvSharp.Window("dst image", dst);

            Cv2.WaitKey();

        }
    }
}
