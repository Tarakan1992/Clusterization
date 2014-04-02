using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Clusterization
{
    public class ImageFilter
    {
        private const double Treshold = 0.55;
        
        public Bitmap Binarization(Bitmap src)
        {

            var dst = new Bitmap(src.Width, src.Height);

            for (int i = 0; i < src.Width; i++)
            {
                for (int j = 0; j < src.Height; j++)
                {
                    dst.SetPixel(i, j, src.GetPixel(i, j).GetBrightness() < Treshold ? Color.Black : Color.White);
                }
            }

            return dst;
        }

        public Bitmap Grayscale(Bitmap src)
        {
            var dst = new Bitmap(src.Width, src.Height);

            for (int i = 0; i < src.Width; i++)
            {
                for (int j = 0; j < src.Height; j++)
                {
                    var pixel = src.GetPixel(i, j);
                    //var y = (pixel.R + pixel.G + pixel.B)/3;
                    var y = (int)(0.3*pixel.R + 0.59*pixel.G + 0.11*pixel.B);
                    dst.SetPixel(i, j, Color.FromArgb(pixel.A, y, y, y));
                }
            }

            return dst;
        }

        public Bitmap FillSomeSpace(Bitmap src)
        {
            var dst = new Bitmap(src.Width, src.Height);

            for (int k = 0; k < 10; k++)
            {
                for (int i = 0; i < src.Width; i++)
                {
                    for (int j = 0; j < src.Height; j++)
                    {
                        if (i > 0 && j > 0 && i < src.Width - 1 && j < src.Height - 1)
                        {
                            dst.SetPixel(i, j, src.GetPixel(i - 1, j).R + src.GetPixel(i + 1, j).R + src.GetPixel(i, j - 1).R + src.GetPixel(i, j + 1).R
                               /* + src.GetPixel(i - 1, j - 1).R + src.GetPixel(i - 1, j + 1).R + src.GetPixel(i + 1, j - 1).R + src.GetPixel(i + 1, j + 1).R */ <= 255 ? Color.Black : Color.White);
                        }
                    }
                }
            }
            

            return dst;
        }
    }
}