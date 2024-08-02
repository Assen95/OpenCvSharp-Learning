using System;
using System.Collections.Generic;
using OpenCvSharp;
// Create a function called GetAvailableCamera() it loops from 1 to 10 with VideoCapture and return a List of available cameras
// 

namespace AccessingTheCamera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> availableCameras = GetAvailableCameras();
            if (availableCameras.Count == 0)
            {
                Console.WriteLine("There are currently no available cameras!");
                return;
            }

            Console.WriteLine("Available Cameras:");
            foreach(var camera in availableCameras)
            {
                Console.WriteLine($"Camera {camera + 1}");
            }

            int enteredCameraNumber = 0;

            while (true)
            {
                Console.WriteLine("Please choose a camera from the list, from 0 to 10.");
                if (int.TryParse(Console.ReadLine(), out enteredCameraNumber))
                {
                    enteredCameraNumber--;
                    break;
                }

                Console.WriteLine("Entered number doesn't correlate to an existing camera. Please choose one from the aforementioned options.");
            }

            OpenCamera(availableCameras[enteredCameraNumber]);


        }
        static List<int> GetAvailableCameras()
        {
            List<int> availableCameras = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                using(var capture = new VideoCapture(i))
                {
                    if (capture.IsOpened())
                    {
                        availableCameras.Add(i);
                    }
                }
            }
            return availableCameras;
        }

        static void OpenCamera(int selectedCamera) // Add API option later VideoCapture APIs
        {
            using (var captured = new VideoCapture(selectedCamera))
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

                    Cv2.ImShow("Original Webcam", videoCaptured);

                    if(Cv2.WaitKey(1) == 27)
                    {
                        break;
                    }
                }

            }
                Cv2.DestroyAllWindows();

        }
    }
}
