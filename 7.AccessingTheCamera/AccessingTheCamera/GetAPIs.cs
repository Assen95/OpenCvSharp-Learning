using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace AccessingTheCamera
{
    internal class GetAPIs
    {
        public static List<VideoCaptureAPIs> GetAPIsForCamera(int cameraIndex)
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

            foreach (var API in APIs)
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
    }
}
