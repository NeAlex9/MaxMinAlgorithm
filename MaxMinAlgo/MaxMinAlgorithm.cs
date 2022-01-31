using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MinMaxAlgo;

#pragma warning disable CA1416

namespace MaxMinAlgo
{
    public class MaxMinAlgorithm
    {
        private readonly string Directory = "..\\..\\..\\Iterations";
        public readonly int FormCount;
        private const int TotalAttempts = 20;
        private const int bitmapSize = 1000;

        public MaxMinAlgorithm(int formCount)
        {
            this.FormCount = formCount;
        }

        public void AssignAlgorithm()
        {
            var painter = new Painter();
            var randomizer = new RandomGenerationService(bitmapSize);
            var data = new MaxMinData(this.FormCount);
            randomizer.Generate(ref data);
            ref var farForm = ref GetMaxFarForm(ref data);
            var attempt = 1;
            var distance = 0d;
            var limitValue = 0d;
            do
            {
                data.SetCenter(ref farForm);
                CalculateArea(ref data);
                var image = painter.MakeImage(data, bitmapSize);
                var path = Path.Combine(this.Directory, attempt.ToString() + ".bmp");
                image.Save(path, ImageFormat.Bmp);
                farForm = ref GetMaxFarForm(ref data);
                distance = GetDistance(farForm.Point, data.Centers[farForm.ClassIndex - 1].Point);
                limitValue = CalculateLimitValue(data.Centers.ToArray());
                attempt++;
            } while (TotalAttempts >= attempt && distance > limitValue);
        }

        private static void CalculateArea(ref MaxMinData actualData)
        {
            for (int i = 0; i < actualData.Vectors.Length; i++)
            {
                double minDistance = bitmapSize * bitmapSize;
                var classIndex = actualData.Vectors[i].ClassIndex;
                foreach (var center in actualData.Centers)
                {
                    var dist = GetDistance(actualData.Vectors[i].Point, center.Point);
                    if (minDistance > dist)
                    {
                        minDistance = dist;
                        classIndex = center.ClassIndex;
                    }
                }

                actualData.Vectors[i].ClassIndex = classIndex;
            }
        }

        private static double CalculateLimitValue((Point Point, int ClassIndex)[] centres)
        {
            var sum = 0d;
            for (int i = 0; i < centres.Length - 1; i++)
            {
                for (int j = i + 1; j < centres.Length; j++)
                {
                    sum += GetDistance(centres[i].Point, centres[j].Point);
                }
            }

            return sum / (centres.Length * (centres.Length - 1));
        }

        private static ref (Point Point, int ClassIndex) GetMaxFarForm(ref MaxMinData actualData)
        {
            ref var maxFarForm = ref actualData.Vectors[0];
            var index = 0;
            var maxDistance = 0d;
            for (int i = 0; i < actualData.ClassCount; i++)
            {
                maxFarForm =  ref GetMaxFarFromCenterForm(actualData.Vectors, actualData.Centers[i]);
                var actualDistance = GetDistance(maxFarForm.Point, actualData.Centers[i].Point);
                if (maxDistance < actualDistance)
                {
                    maxDistance = actualDistance;
                }
            }

            return ref maxFarForm;
        }

        private static ref (Point Point, int ClassIndex) GetMaxFarFromCenterForm(
            (Point Point, int ClassIndex)[] vectors,
            (Point Point, int ClassIndex) center)
        {
            var index = 0;
            var maxDistance = 0d;
            for (int i = 0; i < vectors.Length; i++)
            {
                if (vectors[i].ClassIndex == center.ClassIndex)
                {
                    var actualDistance = GetDistance(center.Point, vectors[i].Point);
                    if (maxDistance < actualDistance)
                    {
                        maxDistance = actualDistance;
                        index = i;
                    }
                }
            }

            return ref vectors[index];
        }

        private static double GetDistance(Point a, Point b) =>
            Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    }
}