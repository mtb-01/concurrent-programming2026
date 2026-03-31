using System;
using System.Collections.Generic;

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
    }
}