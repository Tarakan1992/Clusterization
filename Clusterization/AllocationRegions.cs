using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Clusterization
{
    public class AllocationRegions
    {
        private int[,] labels;
       // private Dictionary<int, Color[]> regionsDictionary = new Dictionary<int, Color[]>(); 

        public int[,] Labeling(Bitmap src)
        {
            int l = 1;
            labels = new int[src.Width, src.Height];
            var dst = new Bitmap(src);

            for (int i = 0; i < src.Width; i++)
            {
                for (int j = 0; j < src.Height; j++)
                {
                    Fill(dst, i, j, l++);
                }
            }

            return labels;
        }

        private void Fill(Bitmap image, int x, int y, int L)
        {
            if (labels[x, y] == 0 && image.GetPixel(x, y).ToArgb() == Color.Black.ToArgb())
            {
                image.SetPixel(x, y, Color.FromArgb( 255 * L / (image.Height * image.Width) , 0, 255 *  L / (image.Height * image.Width) ));
                labels[x, y] = L;

                if (x > 0)
                {
                    Fill(image, x - 1, y, L);
                }

                if (x < image.Width - 1)
                {
                    Fill(image, x + 1, y, L);
                }

                if (y > 0)
                {
                    Fill(image, x, y - 1, L);
                }

                if (y < image.Height - 1)
                {
                    Fill(image, x, y + 1, L);
                }
            }
        }
    }
}
