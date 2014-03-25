using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clusterization
{
    public partial class Form1 : Form
    {
        private readonly ImageFilter _imageFilter;

        public Form1()
        {
            _imageFilter = new ImageFilter();
            InitializeComponent();
        }

        private void buttonLoadImage_Click(object sender, EventArgs e)
        {
            var result = loadImageDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                pictureBoxImage.Image = new Bitmap(loadImageDialog.FileName); 
            }
        }

        private void buttonClustering_Click(object sender, EventArgs e)
        {
            pictureBoxImage.Image = _imageFilter.Binarization(new Bitmap(pictureBoxImage.Image));
        }
    }
}
