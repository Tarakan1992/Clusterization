using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Clusterization
{
    public class Attributes
    {
        public int Square { set; get; }
        public int CenterOfMassX { set; get; }
        public int CenterOfMassY { set; get; }
        public int Perimetr { set; get; }
        public double Compactness { set; get; }
        public double Elongetion { set; get; }
        public double Orientation { set; get; }
        public double m20 { set; get; }
        public double m02 { set; get; }
        public double m11 { set; get; }
    }

    public class AttributeManager
    {
        private Dictionary<int, Attributes> regionsDictionary;
    

        private readonly int[,] _lables;

        public AttributeManager(int[,] lables)
        {
            _lables = lables;
            regionsDictionary = new Dictionary<int, Attributes>();
        }

        public Dictionary<int, Attributes> StartProcessing()
        {
            GetSquareAndPerimeter();
            GetOrientationAndElongetion();

            return regionsDictionary;
        }

        private void GetSquareAndPerimeter()
        {
            for (int i = 0; i < _lables.GetLength(0); i++)
            {
                for (int j = 0; j < _lables.GetLength(1); j++)
                {
                    if (_lables[i, j] != 0)
                    {
                        if (!regionsDictionary.ContainsKey(_lables[i, j]))
                        {
                            regionsDictionary.Add(_lables[i, j], new Attributes());
                        }
                        else
                        {
                            regionsDictionary[_lables[i, j]].Square++;
                            
                            if (_lables[i, j] != _lables[i + 1, j] || _lables[i, j] != _lables[i - 1, j] ||
                                _lables[i, j] != _lables[i, j + 1] || _lables[i, j] != _lables[i, j - 1])
                            {
                                regionsDictionary[_lables[i, j]].Perimetr++;
                            }

                            regionsDictionary[_lables[i, j]].CenterOfMassX += i;
                            regionsDictionary[_lables[i, j]].CenterOfMassY += j;

                        }
                    }
                }
            }

            foreach (var region in regionsDictionary)
            {
                region.Value.CenterOfMassX /= region.Value.Square;
                region.Value.CenterOfMassY /= region.Value.Square;
            }

        }


        private void GetOrientationAndElongetion()
        {
            for (int i = 0; i < _lables.GetLength(0); i++)
            {
                for (int j = 0; j < _lables.GetLength(1); j++)
                {
                    if (_lables[i, j] != 0)
                    {
                        regionsDictionary[_lables[i, j]].m20 +=
                            Math.Pow(i - regionsDictionary[_lables[i, j]].CenterOfMassX, 2.0);

                        regionsDictionary[_lables[i, j]].m02 +=
                            Math.Pow(j - regionsDictionary[_lables[i, j]].CenterOfMassY, 2.0);

                        regionsDictionary[_lables[i, j]].m11 += i - regionsDictionary[_lables[i, j]].CenterOfMassX*
                                                                j - regionsDictionary[_lables[i, j]].CenterOfMassY;
                    }
                }
            }

            
            foreach (var region in regionsDictionary)
            {
                var it = region.Value;

                var sqrt = Math.Sqrt(Math.Pow(it.m20 - it.m02, 2) + 4*Math.Pow(it.m11, 2));

                it.Elongetion = (it.m20 + it.m02 + sqrt)/(it.m20 + it.m02 - sqrt);

                it.Orientation = Math.Atan(2*it.m11/(it.m20 - it.m02))/2;

                it.Compactness = Math.Pow(it.Perimetr, 2)/it.Square;
            }
        }


    }
}
