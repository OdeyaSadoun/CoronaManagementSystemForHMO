using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : Shape
    {
        /// <summary>
        /// Function to calculate the perimeter of rectangle
        /// </summary>
        /// <returns>Rectangle's perimeter</returns>
        public override double CalculatePerimeter()
        {
            return Height * 2 + Width * 2;
        }

        /// <summary>
        /// Function to calculate the area of rectangle
        /// </summary>
        /// <returns>Rectangle's area</returns>
        public double CalculateArea()
        {
            return Height * Width;
        }
    }
}
