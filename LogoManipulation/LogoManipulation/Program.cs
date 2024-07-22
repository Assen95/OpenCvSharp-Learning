using System;
using System.Numerics;
using OpenCvSharp;

namespace LogoManipulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cocaColaImgPath = @"C:\temp\coca-cola-logo.png";
            var backgroundImgPath = @"C:\temp\checkeredImage.jpg";

            Mat cocaColaLogo = Cv2.ImRead(cocaColaImgPath);
            // Take the image's height and width, so that we can resize the background to its size
            int cocaHeight = cocaColaLogo.Height;
            int cocaWidth = cocaColaLogo.Width;

            Mat backgroundImage = Cv2.ImRead(backgroundImgPath);
            Mat resizedBackgroundImage = new Mat();
            Cv2.Resize(backgroundImage, resizedBackgroundImage, new Size(cocaWidth, cocaHeight));

            // Create a Mask for Original Image
            Mat maskCocaCola = new Mat();
            Cv2.CvtColor(cocaColaLogo, maskCocaCola, ColorConversionCodes.RGB2GRAY);
            
            Mat adaptedCocaColaMask = new Mat();
            Cv2.Threshold(maskCocaCola, adaptedCocaColaMask, 127, 255, ThresholdTypes.Binary);

            // Inverse the mask

            Mat inversedAdapatedCocaColaMask = new Mat();
            Cv2.Threshold(adaptedCocaColaMask, inversedAdapatedCocaColaMask, 127, 255, ThresholdTypes.BinaryInv);

            // Apply background on the Mask

            Mat backgroundImageMask = new Mat();
            Cv2.BitwiseAnd(resizedBackgroundImage, resizedBackgroundImage, backgroundImageMask, mask: adaptedCocaColaMask);

            // Isolate Foreground from Image
            Mat foregroundImage = new Mat();
            Cv2.BitwiseAnd(cocaColaLogo, cocaColaLogo, foregroundImage, mask: inversedAdapatedCocaColaMask);

            // Result: Merge Foreground and Background
            Mat mergedImageResult = new Mat();
            Cv2.Add(backgroundImageMask, foregroundImage, mergedImageResult);

            // Save the Image
            bool result = Cv2.ImWrite(@"C:\temp\final_ResultLogo.jpg", mergedImageResult);


            Cv2.ImShow("CocaColaLogo", cocaColaLogo);
            // Cv2.ImShow("Background Image", backgroundImage);
            Cv2.ImShow("Resized Image", resizedBackgroundImage);
            Cv2.ImShow("Mask Coca Cola", adaptedCocaColaMask);
            Cv2.ImShow("Inversed Mask Coca Cola", inversedAdapatedCocaColaMask);
            Cv2.ImShow("Adapted Mask", backgroundImageMask);
            Cv2.ImShow("Isolated Foreground Image", foregroundImage);
            Cv2.ImShow("Final Result", mergedImageResult);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
