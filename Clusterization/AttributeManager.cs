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
                        }
                    }
                }
            }
        }

    }
}
