namespace Project.Data
{
    internal class Ball : IBall
    {
        public IVector Position { get; set; }
        public IVector Velocity { get; }
        public double Mass { get; init; }
        public double Diameter { get; init; }


        public Ball(IVector initialPosition, IVector initialVelocity, double mass, double diameter)
        {
            Position = initialPosition;
            Velocity = initialVelocity;
            Mass = mass;
            Diameter = diameter;
        }
    }
}
