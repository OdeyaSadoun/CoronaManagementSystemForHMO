using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : Shape
    {
        public override double CalculatePerimeter()
        {
            return Height * 2 + Width * 2;
        }
    }
}
