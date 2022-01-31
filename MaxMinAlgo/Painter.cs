using System.Drawing;

#pragma warning disable CA1416

namespace MinMaxAlgo
{
    public class Painter
    {
        private const int PenWidth = 2;
        
        public Bitmap MakeImage(MaxMinData data, int bitmapSize)
        {
            Bitmap b = new Bitmap(bitmapSize, bitmapSize);
            using var g = Graphics.FromImage(b);
            g.Clear(Color.White);
            foreach (var vector in data.Vectors)
            {
                g.DrawRectangle(SelectColor(vector.ClassIndex), vector.Point.X,
                    vector.Point.Y, 1, 1);
                g.FillRectangle(SelectColor(vector.ClassIndex).Brush, vector.Point.X,
                    vector.Point.Y, 1, 1);
            }

            foreach (var center in data.Centers)
            { 
                g.DrawRectangle(SelectColor(center.ClassIndex), center.Point.X,
                    center.Point.Y, 10, 10);
                g.FillRectangle(SelectColor(center.ClassIndex).Brush, center.Point.X,
                    center.Point.Y, 10, 10);
            }
            
            return b;
        }

        private static Pen SelectColor(int classIndex)
        {
            var color = classIndex switch
            {
                1 => Color.Red,
                2 => Color.Coral,
                3 => Color.Blue,
                4 => Color.Green,
                5 => Color.Brown,
                6 => Color.Orange,
                7 => Color.HotPink,
                8 => Color.Purple,
                9 => Color.Gold,
                10 => Color.DarkRed,
                _ => Color.Black,
            };

            return new Pen(color) {Width = PenWidth};
        }
    }
}