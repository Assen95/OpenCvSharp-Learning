using System;
using System.IO;
using OpenCvSharp;


namespace BasicImageManipulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var image = @"C:\temp\germanExample.jpg";

            Mat hotPic = Cv2.ImRead(image);
            Mat resized = Cv2.ImRead(image);
            Mat flippedBabe = Cv2.ImRead(image);

            Size newSize = new Size(200, 300);

            Cv2.Resize(hotPic, resized, newSize);
            Cv2.Flip(hotPic, flippedBabe, FlipMode.Y); // X, Y, XY (horizontal, vertical, horizontal and vertical)

            Rect roi = new Rect(300, 600, 200, 200);

            Mat croppedImage = new Mat(hotPic, roi);
            Console.WriteLine("Image size " + croppedImage.Size());

            Cv2.ImShow("Hot German", hotPic);
            Cv2.ImShow("Cropped", croppedImage);
            Cv2.ImShow("Resized Bithch", resized);
            Cv2.ImShow("Flipped Bitch", flippedBabe);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();

            //Cropping an image

        }
    }
}
