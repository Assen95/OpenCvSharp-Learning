using System;
using System.Collections.Generic;
using OpenCvSharp;
// 1. Cleanup
// 2. Create a function called GetAvailableCamera() it loops from 1 to 10 with VideoCapture and return a List of available cameras
// 3. Create a function which returns a list of APIs, use same logic for camera
// 4. Modify OpenCamera to include GetAPIs() function
// 5. Create a function, which returns a bool, it checks a frame of the camera, gets the max and min values with Cv2.MinMaxLoc() and checks if the frame's values match with one or the other
// 6. Make it more readable, separate the functions into separate files

namespace AccessingTheCamera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Getting Available Cameras
            List<int> availableCameras = GettingAvailableCameras.GetAvailableCameras();
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
            var availableAPIs = GetAPIs.GetAPIsForCamera(availableCameras[enteredCameraNumber]);

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



            CameraOpener.OpenCamera(availableCameras[enteredCameraNumber], availableAPIs[selectedAPINumber]);


        }
    }
}
