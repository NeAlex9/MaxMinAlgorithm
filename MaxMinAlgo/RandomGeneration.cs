using System;
using System.Drawing;
using MinMaxAlgo;

namespace MinMaxAlgo
{
    public class RandomGenerationService
    {
        private readonly int maxValueVector;
        private static readonly Random RandomGen = new Random();

        public RandomGenerationService(int maxVectorValue)
        {
            this.maxValueVector = maxVectorValue;
        }

        public void Generate(ref MaxMinData data)
        {
            this.GenerateRandomForms(ref data);
            this.GenerateRandomCentres(ref data);
        }

        private void GenerateRandomForms(ref MaxMinData data)
        {
            for (int i = 0; i < data.FormCount; i++)
            {
                data.Vectors[i].Point = new Point(RandomGen.Next(0, maxValueVector), RandomGen.Next(0, maxValueVector));
                data.Vectors[i].ClassIndex = 1;
            }
        }

        private void GenerateRandomCentres(ref MaxMinData data)
        {
            var elementsIndex = RandomGen.Next(0, data.FormCount);
            data.Vectors[elementsIndex].ClassIndex = 1;
            data.Centers.Add(data.Vectors[elementsIndex]);
        }
    }
}