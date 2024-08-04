using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace AccessingTheCamera
{
    internal class GettingAvailableCameras
    {
        public static List<int> GetAvailableCameras()
        {
            List<int> availableCameras = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                using (var capture = new VideoCapture(i))
                {
                    if (capture.IsOpened())
                    {
                        availableCameras.Add(i);
                    }
                }
            }
            return availableCameras;
        }
    }
}
