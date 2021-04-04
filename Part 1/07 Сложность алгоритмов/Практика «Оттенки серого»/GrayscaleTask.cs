namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
        {
            var grayPixel = new double[original.GetLength(0), original.GetLength(1)];
            var grayPixelX = grayPixel.GetLength(0);
            var grayPixelY = grayPixel.GetLength(1);
            for (int i = 0; i < grayPixelX; i++)
            {
                for (int j = 0; j < grayPixelY; j++)
                {
                    grayPixel[i, j] = (original[i, j].R * 0.299 + original[i, j].G * 0.587 + original[i, j].B * 0.114) / 255;
                }
            }
            return grayPixel;
        }
    }
}