using System;
using System.IO;
using OpenCvSharp;

namespace ImageHandling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var imgPath = @"C:\temp\germanExample.jpg";
            Mat source = Cv2.ImRead(imgPath);

            Console.WriteLine(source);

            Console.WriteLine("The image size is " + source.Size());
            Console.WriteLine("The image type is " + source.Type());
            Cv2.ImShow("Sexy Woman", source);

            Cv2.WaitKey(0);
                
            Cv2.DestroyAllWindows();
        }
    }
}
