using System;
using System.Collections.Generic;
using OpenCvSharp;
// 1. Cleanup
// 2. Create a function called GetAvailableCamera() it loops from 1 to 10 with VideoCapture and return a List of available cameras
// 3. Create a function which returns a list of APIs, use same logic for camera
// 4. Modify OpenCamera to include GetAPIs() function
// 5. Create a function, which returns a bool, it checks a frame of the camera, gets the max and min values with Cv2.MinMaxLoc() and checks if the frame's values match with one or the other

namespace AccessingTheCamera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Getting Available Cameras
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

            // Camera selection
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

            // Getting available APIs after camera selection
            var availableAPIs = GetAPIsForCamera(availableCameras[enteredCameraNumber]);

            Console.WriteLine($"Your selected camera is Camera: {enteredCameraNumber + 1}");
            Console.WriteLine("API options for the camera:");
            foreach(var currentAPI in availableAPIs)
            {
                Console.WriteLine($"API: {currentAPI} - {availableAPIs.IndexOf(currentAPI) + 1}");
            }

            // API Selection
            int selectedAPINumber = 0;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out selectedAPINumber))
                {
                    selectedAPINumber--;
                    break;
                }

                Console.WriteLine("The selection does not correlate to an available API for the camera! Please enter of the listed options.");
            }



            OpenCamera(availableCameras[enteredCameraNumber], availableAPIs[selectedAPINumber]);


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

        static List<VideoCaptureAPIs> GetAPIsForCamera(int cameraIndex)
        {
            List<VideoCaptureAPIs> availableAPIs = new List<VideoCaptureAPIs>();

            var APIs = new List<VideoCaptureAPIs>
            {
                VideoCaptureAPIs.ANY, // Automatic Selection - When you don't have a preference OpenCV chooses the best option
                VideoCaptureAPIs.DSHOW, // DirectShow - Good performance and compability for video capture hardware on Windows
                VideoCaptureAPIs.MSMF, // Microsoft Media Foundation - Support a wide range of video formats and modern hardware
                VideoCaptureAPIs.FFMPEG, // FFMpeg backend - popular choice for handling video files
                VideoCaptureAPIs.IMAGES // OpenCv Image Sequence - For processing a series of still images in a video-like manner
            };

            foreach(var API in APIs)
            {
                using (var caputed = new VideoCapture(cameraIndex, API))
                {
                    if (caputed.IsOpened())
                    {
                        availableAPIs.Add(API);
                    }
                }
            }

            return availableAPIs;
        }
        static void OpenCamera(int selectedCamera, VideoCaptureAPIs selectedAPI) // Add API option later VideoCapture APIs
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
                    if (CheckIfEntirelyWhiteScreenOrBlackScreen(videoCaptured))
                    {
                        Console.WriteLine("Error: the video is entirely white or black!");
                        break;
                    }

                    Cv2.ImShow($"Camera {selectedCamera + 1}; API: {selectedAPI}", videoCaptured);

                    if(Cv2.WaitKey(1) == 27)
                    {
                        break;
                    }
                }

            }
                Cv2.DestroyAllWindows();

        }
        static bool CheckIfEntirelyWhiteScreenOrBlackScreen(Mat frame)
        {
            Cv2.MinMaxLoc(frame, out double minVal, out double maxVal);

            if ((minVal == 0 && maxVal == 0) || (minVal == 255 & maxVal == 255))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
