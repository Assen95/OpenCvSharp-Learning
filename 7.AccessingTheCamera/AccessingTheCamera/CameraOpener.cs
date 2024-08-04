using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace AccessingTheCamera
{
    internal class CameraOpener
    {
        public static void OpenCamera(int selectedCamera, VideoCaptureAPIs selectedAPI) // Add API option later VideoCapture APIs
        {
            using (var captured = new VideoCapture(selectedCamera, selectedAPI))
            {
                if (!captured.IsOpened())
                {
                    Console.WriteLine("The camera is not open!");
                    return;
                }

                Mat videoCaptured = new Mat();

                while (true)
                {
                    captured.Read(videoCaptured);

                    if (videoCaptured.Empty())
                    {
                        break;
                    }
                    if (CheckIfScreenBlackOrWhite.CheckIfEntirelyWhiteScreenOrBlackScreen(videoCaptured))
                    {
                        Console.WriteLine("Error: the video is entirely white or black!");
                        break;
                    }

                    Cv2.ImShow($"Camera {selectedCamera + 1}; API: {selectedAPI}", videoCaptured);

                    if (Cv2.WaitKey(1) == 27)
                    {
                        break;
                    }
                }

            }
            Cv2.DestroyAllWindows();

        }
    }
}
