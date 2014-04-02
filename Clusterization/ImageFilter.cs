using System.Drawing;

namespace Clusterization
{
	using System.Collections.Generic;

	public class ImageFilter
	{
		private const double Treshold = 0.51;
		private int[,] _internalSpaceChecked;

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
					var y = (int)(0.3 * pixel.R + 0.59 * pixel.G + 0.11 * pixel.B);
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
								+ src.GetPixel(i + 1, j + 1).R + src.GetPixel(i - 1, j + 1).R + src.GetPixel(i - 1, j - 1).R + src.GetPixel(i + 1, j - 1).R <= 255 * 3 ? Color.Black : Color.White);
						}
					}
				}
			}


			return dst;
		}

		public Bitmap FillInternalSpace(Bitmap src)
		{
			var dst = new Bitmap(src.Width, src.Height);


			for (int i = 0; i < src.Width; i++)
			{
				for (int j = 0; j < src.Height; j++)
				{
					_internalSpaceChecked = new int[src.Width, src.Height];
					dst.SetPixel(i, j, CheckForIntrnalSpace(src, i, j) ? Color.Black : Color.White);
				}
			}


			return dst;
		}

		public Bitmap SelectClusters(Bitmap src, int[,] labels, Dictionary<int, int> clusterInfo)
		{
			var dst = new Bitmap(src);

			for (int i = 0; i < src.Width; i++)
			{
				for (int j = 0; j < src.Height; j++)
				{
					if (clusterInfo.ContainsKey(labels[i, j]))
					{
						dst.SetPixel(i, j, Color.FromArgb(clusterInfo[labels[i, j]] * 50, 0, clusterInfo[labels[i, j]] * 30));
					}
				}
			}

			return dst;

		}

		private bool CheckForIntrnalSpace(Bitmap src, int x, int y)
		{
			if (x < 0 || x > src.Width - 1 || y < 0 || y > src.Height - 1)
			{
				return false;
			}

			if (src.GetPixel(x, y).ToArgb() == Color.Black.ToArgb() || _internalSpaceChecked[x, y] == 1)
			{
				return true;
			}

			_internalSpaceChecked[x, y] = 1;

			return CheckForIntrnalSpace(src, x - 1, y) && CheckForIntrnalSpace(src, x + 1, y) &&
				   CheckForIntrnalSpace(src, x, y - 1) && CheckForIntrnalSpace(src, x, y + 1);
		}
	}
}