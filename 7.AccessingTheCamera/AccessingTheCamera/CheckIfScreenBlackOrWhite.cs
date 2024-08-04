using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace AccessingTheCamera
{
    internal class CheckIfScreenBlackOrWhite
    {
        public static bool CheckIfEntirelyWhiteScreenOrBlackScreen(Mat frame)
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
