using System;
using OpenCvSharp;
namespace ImageEnchancement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var imgPath = @"C:\temp\mountains.jpg";

            var anotherImgPath = @"C:\temp\sheet_music.jpg";

            Mat sheetMusic = Cv2.ImRead(anotherImgPath, ImreadModes.Grayscale);

            Mat binaryThresholdImage = new Mat(); // uses global threshold value
            Mat adaptiveThreshold = new Mat(); // uses an adaptable treshold value, it is perfect for images, like a photo, which could have varying degrees of light


            Cv2.Threshold(sheetMusic, binaryThresholdImage, 127, 255, ThresholdTypes.Binary);
            Cv2.AdaptiveThreshold(sheetMusic, 
                adaptiveThreshold, 
                maxValue: 255, 
                adaptiveMethod: AdaptiveThresholdTypes.MeanC,
                ThresholdTypes.Binary,
                blockSize: 11,
                c: 2
                );

            Mat mountains = Cv2.ImRead(imgPath, ImreadModes.Grayscale);
            

            // Increases brightness
            Mat brighterImage = new Mat();
            mountains.ConvertTo(brighterImage, MatType.CV_8UC3, alpha: 1, beta: 50);

            // Decreases brightness
            Mat darkerImage = new Mat();
            mountains.ConvertTo(darkerImage, MatType.CV_8UC3, alpha: 1, beta: -50);

            // Alpha increaases or decreases contrast

            // Bitwise : AND, OR, XOR, NOT -> are useful in various image processing tasks such as masking, combining images, and logical operations on pixels.


            Cv2.ImShow("Mountains", mountains);
            //Cv2.ImShow("Brihgter", brighterImage);
            //Cv2.ImShow("Darker", darkerImage);
            Cv2.ImShow("Music Sheet", sheetMusic);
            Cv2.ImShow("Threshold Image", binaryThresholdImage);
            Cv2.ImShow("Adaptive Threshold", adaptiveThreshold);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();

        }
    }
}
