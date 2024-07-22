using System;
using OpenCvSharp;

namespace ImageAnnotation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\temp\grayImage.jpg";
            Mat grayImage = Cv2.ImRead(path);

            // Drawing a Line

            Mat imageLine = grayImage.Clone();

            Cv2.Line(imageLine, new Point(50, 50), new Point(200, 200), new Scalar(255, 0, 0), 5, LineTypes.Link8);    

            // Drawing a circle

            Mat imageCircle = grayImage.Clone();

            Cv2.Circle(imageCircle, new Point(200, 200), 100, new Scalar(255, 0, 0), 5, LineTypes.Link8);

            // Drawing a rectangle, same principle, no need to make an example

            // Adding Text

            Mat imageText = grayImage.Clone();

            Cv2.PutText(imageText, "I want to suck!", new Point(100, 300), HersheyFonts.HersheyPlain, 2, new Scalar(255, 0, 0), 2, LineTypes.Link8);

            Cv2.ImShow("Gray Image", grayImage);
            Cv2.ImShow("Line in Image", imageLine);
            Cv2.ImShow("Circle in Image", imageCircle);
            Cv2.ImShow("Image with Text", imageText);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
            
        }
    }
}
