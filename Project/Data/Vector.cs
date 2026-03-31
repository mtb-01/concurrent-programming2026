namespace Project.Data
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
    }
}
