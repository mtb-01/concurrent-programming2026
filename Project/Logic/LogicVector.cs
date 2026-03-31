using System;
using System.Collections.Generic;

namespace Project.Logic
{
    internal class Vector : IVector
    {
        public double x {get; init; }
        public double y {get; init; }

        public Vector(double xComponent, double yComponent)
        {
            x = xComponent;
            y = yComponent;
        }       
    }
}