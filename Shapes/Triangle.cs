using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Triangle : Shape
    {
        /// <summary>
        /// Function to calculate the perimeter of triangle
        /// </summary>
        /// <returns>Triangle's perimeter</returns>
        public override double CalculatePerimeter()
        {
            //A triangle will always be isosceles.

            /*
            To calculate the perimeter of the triangle we will have to calculate the side of the leg once
            (since we were told that the triangle is equilateral).
            Since we are also given the height of the triangle -
            in an isosceles triangle it is also the middle of the base - that is, it divides the base into 2 equal parts.
            In this way we will get 2 overlapping right triangles.
            We will take one for the purpose of calculating the side which is the "excess" of the triangle.
            The calculation will be carried out as follows:
            square root of: half the width of the triangle squared + the height squared.
             */
            double width = (double)Width;

            double side = Math.Sqrt(((width / 2) * (width / 2)) + (Height * Height));

            return 2 * side + Width;
        }

        /// <summary>
        /// Function to print the triangle
        /// </summary>
        public void PrintRectangle()
        {
            int width = (int)Width;

            // Print the first row:
            Console.Write(new string(' ', width / 2));
            Console.Write('*');
            Console.WriteLine(new string(' ', width / 2));

            /*In order to know how many numbers are odd in our range,
            we will take the width - which is the largest odd number,
            subtract 2 from it and divide by 2, and thus we will get the amount of odd numbers in the range.
            */
            int numOdds = (width - 2) / 2;
            /*
            Calculate the number of rows of stars for each odd number by subtracting 2 from the height of the triangle.
            (one for the top row, and the other for the bottom row).
            after it divid by the number of the odd numbers.
             */
            int numOfRows = ((int)Height - 2) / numOdds;
            int divisionRemainer = ((int)Height - 2) % numOdds;
            /*
            Now we will loop through all the odd numbers
            and for each one we will print the number of rows of asterisks it should receive.
             */
            int numSpaces;
            for (int i = 3; i < width; i+=2)
            {
                numSpaces = (width - i) / 2;
                for (int j = 0; j < numOfRows; j++)
                {
                    Console.Write(new string(' ', numSpaces));
                    Console.Write(new string('*', i));
                    Console.WriteLine(new string(' ', numSpaces));
                }

                //If there is a remainder, add these rows in the first block that contains 3 stars.
                if (i == 3 && divisionRemainer > 0)
                {
                    for (int k = 0; k < divisionRemainer; k++)
                    {
                        Console.Write(new string(' ', numSpaces));
                        Console.Write(new string('*', i));
                        Console.WriteLine(new string(' ', numSpaces));
                    }
                }

            }



            // Print the bottom row:
            Console.WriteLine(new string('*', width));
        }
    }
}
