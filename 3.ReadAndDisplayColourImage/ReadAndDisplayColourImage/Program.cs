using System;
using System.IO;
using OpenCvSharp;

namespace ReadAndDisplayColourImage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pathImg = @"C:\temp\coca-cola-logo.png";
            Mat pic = Cv2.ImRead(pathImg);
            Mat grayImage = new Mat();
            Mat blueImage = pic.Clone();
            Mat convertedToRGB = new Mat();

            for (int y = 0; y < blueImage.Rows; y++)
            {
                for (int x = 0; x < blueImage.Cols; x++)
                {
                    Vec3b color = blueImage.At<Vec3b>(y, x);

                    if (color.Item2 > 150 && color.Item0 < 100 && color.Item1 < 100)
                    {
                        color.Item0 = 255;
                        color.Item1 = 0;
                        color.Item2 = 0;
                        blueImage.Set<Vec3b>(y, x, color);
                    }
                }
            }


            Cv2.CvtColor(pic, grayImage, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(pic, convertedToRGB, ColorConversionCodes.BGR2RGB);

            Console.WriteLine("Image size is " + pic.Size());
            Console.WriteLine("Image type is " + pic.Type());

            Cv2.ImShow("coca cola", pic);
            Cv2.ImShow("second image", grayImage);
            Cv2.ImShow("Blue cola", blueImage);
            Cv2.ImShow("RGB CONVERTED", convertedToRGB);
            Cv2.WaitKey(5000);
            Cv2.DestroyAllWindows();

        }
    }
}
