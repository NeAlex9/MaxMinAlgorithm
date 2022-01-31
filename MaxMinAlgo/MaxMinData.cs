using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MinMaxAlgo
{
    public struct MaxMinData : IEquatable<MaxMinData>
    {
        public int ClassCount { get; set; }
        public readonly int FormCount;

        public MaxMinData(int formCount)
        {
            this.ClassCount = 1;
            this.FormCount = formCount;
            this.Vectors = new (Point, int)[this.FormCount];
            this.Centers = new List<(Point Point, int ClassIndex)>();
        }

        public (Point Point, int ClassIndex)[] Vectors { get; private set; }
        public List<(Point Point, int ClassIndex)> Centers { get; private set; }

        public void SetCenter(ref (Point Point, int ClassIndex) vector)
        {
            this.ClassCount++;
            vector.ClassIndex = this.ClassCount;
            this.Centers.Add(vector);
        }
        
        public MaxMinData Clone()
        {
            var vectors = new (Point, int)[this.FormCount];
            var centers = new (Point point, int classIndex)[this.ClassCount];
            Array.Copy(this.Vectors, vectors, this.FormCount);
            Array.Copy(this.Centers.ToArray(), centers, this.ClassCount);
            return new MaxMinData(this.FormCount) {Vectors = vectors, Centers = centers.ToList()};
        }

        public bool Equals(MaxMinData other) =>
            this.FormCount == other.FormCount
            && this.ClassCount == other.ClassCount
            && other.Centers.SequenceEqual(this.Centers);
    }
}