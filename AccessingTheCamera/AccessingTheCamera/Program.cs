using System;
using OpenCvSharp;

namespace AccessingTheCamera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var capture = new VideoCapture(0))
            {
                if (!capture.IsOpened())
                {
                    Console.WriteLine("The camera is not open!");
                    return;
                }


                Mat videoCapture = new Mat();
                // The camera is mirrors me, I want it to work correctly so:
                Mat flippedCapture = new Mat();
                Mat anotherAngle = new Mat();
                Mat yetAnotherAngle = new Mat();

                // Set the size for the window
                int height = 300;
                int width = 300;
                Mat resizedWindow = new Mat();

                while (true)
                {
                    capture.Read(videoCapture);

                    if (videoCapture.Empty())
                    {
                        break;
                    }
                    Cv2.Resize(videoCapture, resizedWindow, new Size(width, height));

                    // I use Cv2.Flip
                    Cv2.Flip(videoCapture, flippedCapture, FlipMode.Y);
                    Cv2.Flip(videoCapture, anotherAngle, FlipMode.X);
                    Cv2.Flip(videoCapture, yetAnotherAngle, FlipMode.XY);

                    Cv2.ImShow("resized Window", resizedWindow);
                    Cv2.ImShow("Webcam - Hello World", flippedCapture);
                    Cv2.ImShow("Original Webcam", videoCapture);
                    Cv2.ImShow("Another Angle", anotherAngle);
                    Cv2.ImShow("Yet Another Angle", yetAnotherAngle);

                    if(Cv2.WaitKey(1) == 27)
                    {
                        break;
                    }
                }

                Cv2.DestroyAllWindows();
            }
        }
    }
}
