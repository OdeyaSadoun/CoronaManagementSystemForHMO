using System;

namespace Shapes
{
    public abstract class Shape
    {

        public double Height { get; set; }
        public double Width { get; set; }

        public abstract double CalculatePerimeter();
    }
}
