using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clusterization
{
    public partial class Form1 : Form
    {
        private readonly ImageFilter _imageFilter;
        private readonly AllocationRegions _allocationRegions;

        public Form1()
        {
            _imageFilter = new ImageFilter();
            _allocationRegions = new AllocationRegions();
            InitializeComponent();
        }

        private void buttonLoadImage_Click(object sender, EventArgs e)
        {
            var result = loadImageDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                pictureBoxImage.Image = new Bitmap(loadImageDialog.FileName); 
            }
        }

        private void buttonClustering_Click(object sender, EventArgs e)
        {
            pictureBoxImage.Image = _imageFilter.Grayscale(new Bitmap(pictureBoxImage.Image));
            pictureBoxImage.Image = _imageFilter.Binarization(new Bitmap(pictureBoxImage.Image));
            pictureBoxImage.Image = _imageFilter.FillSomeSpace(new Bitmap(pictureBoxImage.Image));
			pictureBoxImage.Image = _imageFilter.FillInternalSpace(new Bitmap(pictureBoxImage.Image));

            var manager = new AttributeManager(_allocationRegions.Labeling(new Bitmap(pictureBoxImage.Image)));
            var result = manager.StartProcessing();
            Console.WriteLine("log");
        }
    }
}
