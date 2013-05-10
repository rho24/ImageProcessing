using System;

namespace ImageProcessing.Core
{
    public class Vector2
    {
        private readonly Lazy<double> _angle;
        private readonly Lazy<double> _length;
        public int X { get; set; }
        public int Y { get; set; }

        public double Length {
            get { return _length.Value; }
        }

        public double Angle {
            get { return _angle.Value; }
        }

        public static Vector2 Zero {
            get { return new Vector2(0, 0); }
        }

        public Vector2(int x, int y) {
            X = x;
            Y = y;
            _length = new Lazy<double>(() => Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)));
            _angle = new Lazy<double>(() => Math.Atan2(y, x));
        }
    }
}