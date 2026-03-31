using System;

namespace Project.Data
{
    internal class Ball : IBall
    {
        public IVector Position { get; set; }
        public IVector Velocity { get; }
        public double Mass { get; init; }
        public double Circumference { get; init; }


        public Ball(IVector initialPosition, IVector initialVelocity, double mass, double circumference)
        {
            Position = initialPosition;
            Velocity = initialVelocity;
            Mass = mass;
            Circumference = circumference;
        }
    }
}
