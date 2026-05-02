using System;

namespace Project.Logic
{
    internal class Vector : IVector
    {
        public double X { get; init; }
        public double Y { get; init; }

        public Vector()
        {
            X = 0;
            Y = 0;
        }

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

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public Vector Normalized()
        {
            return this / Length();
        }

        public double Dot(Vector other)
        {
            return X * other.X + Y * other.Y;
        }

        public Vector Projected(Vector other)
        {
            return other * Dot(other);
        }


        public static Vector operator *(Vector left, double right)
        {
            return new Vector(left.X * right, left.Y * right);
        }

        public static Vector operator /(Vector left, double right)
        {
            return new Vector(left.X / right, left.Y / right);
        }

        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y);
        }
    }
    
}