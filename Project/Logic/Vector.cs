using System;

namespace Project.Logic
{
    internal class Vector : IVector
    {
        public double X {get; init; }
        public double Y {get; init; }

        public Vector(double xComponent, double yComponent)
        {
            X = xComponent;
            Y = yComponent;
        }

        public override bool Equals(object? obj)
        {
            Vector? vector = obj as Vector;
            return vector != null &&
               X == vector.X &&
               Y == vector.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
    
}