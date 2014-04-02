using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace Clusterization
{
    public class Clusterization
    {
        private readonly Dictionary<int, Attributes> _regions;

        public Clusterization(Dictionary<int, Attributes> regions)
        {
            _regions = regions;
        }

        public Dictionary<int, int> Kmedoits()
        {
            var result = new Dictionary<int, int>();

            var class1List = new List<int>();
            var class2List = new List<int>();

            var currentElementClass1 = _regions.Values.ElementAt(0);
            var currentElementClass2 = _regions.Values.ElementAt(1);
            var previousAverageDistanceClass1 = -1.0;
            var previousAverageDistanceClass2 = -1.0;

            while (true)
            {
                var averageDistanceClass1 = 0.0;
                var averageDistanceClass2 = 0.0;
                
                for (int i = 0; i < _regions.Count; i++)
                {
                    var distanceClass1 = ProcessEvclidDistance(currentElementClass1, _regions.Values.ElementAt(i));
                    var distanceClass2 = ProcessEvclidDistance(currentElementClass2, _regions.Values.ElementAt(i));

                    if (distanceClass1 >= distanceClass2)
                    {
                        class2List.Add(_regions.First(p=> p.Value == _regions.Values.ElementAt(i)).Key);
                        
                        averageDistanceClass2 += distanceClass2;
                    }
                    else
                    {
                        class1List.Add(_regions.First(p=> p.Value == _regions.Values.ElementAt(i)).Key);

                        averageDistanceClass1 += distanceClass1;
                    }
                }

                averageDistanceClass1 /= class1List.Count;
                averageDistanceClass2 /= class2List.Count;
                
                if (previousAverageDistanceClass1 < 0 || previousAverageDistanceClass2 < 0)
                {
                    previousAverageDistanceClass1 = averageDistanceClass1;
                    previousAverageDistanceClass2 = averageDistanceClass2;

                }
                else
                {
                    if (averageDistanceClass1 < previousAverageDistanceClass1 ||
                        averageDistanceClass2 < previousAverageDistanceClass2)
                    {
                        previousAverageDistanceClass1 = averageDistanceClass1;
                        previousAverageDistanceClass2 = averageDistanceClass2;
                    }
                    else
                    {
                        foreach (var i in class1List)
                        {
                            result.Add(i, 1);    
                        }
                        foreach (var i in class2List)
                        {
                            result.Add(i, 2);
                        }

                        break;
                    }
                }

                if (class1List.Count > 1)
                {
                    currentElementClass1 = _regions[class1List.First(p => currentElementClass1 != _regions[p])];

                }
                if (class2List.Count > 1)
                {
                    currentElementClass2 = _regions[class2List.First(p => currentElementClass2 != _regions[p])];
                }

                class1List.Clear();
                class2List.Clear();
            }

            return result;
        }

        private double ProcessEvclidDistance(Attributes item1, Attributes items2)
        {
            var result = 0.0;
            result += Math.Pow(item1.CenterOfMassX - items2.CenterOfMassX, 2);
            result += Math.Pow(item1.CenterOfMassY - items2.CenterOfMassY, 2);
            result += Math.Pow(item1.Compactness - items2.Compactness, 2);
            result += Math.Pow(item1.Elongetion - items2.Elongetion, 2);
            result += Math.Pow(item1.Orientation - items2.Orientation, 2);
            result += Math.Pow(item1.Perimetr - items2.Perimetr, 2);
            result += Math.Pow(item1.Square - items2.Square, 2);

            return Math.Sqrt(result);
        }
    }
}
