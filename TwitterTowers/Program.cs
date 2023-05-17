using Shapes;
using System;

namespace TwitterTowers
{
    class Program
    {
        static Shape shape;

        static void Main(string[] args)
        {
            int choice; //to save the user choice
            do
            {
                DisplayMenu(); 
                choice = GetChoice(); 

                switch (choice)
                {
                    case 1:
                        CreateRectangleTower();
                        break;
                    case 2:
                        CreateTriangleTower();
                        break;
                    case 3:
                        Console.WriteLine("Exiting the program...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }

                Console.WriteLine();
            }
            while (choice != 3);
        }

        /// <summary>
        /// Function that print the menu of options to the user
        /// </summary>
        static void DisplayMenu()
        {
            Console.WriteLine("Tower Builder Program");
            Console.WriteLine("---------------------");
            Console.WriteLine("Enter your choice:");
            Console.WriteLine("1. Create a Rectangle Tower");
            Console.WriteLine("2. Create a Triangle Tower");
            Console.WriteLine("3. Exit");
        }

        /// <summary>
        /// Function to get the user's choice in the menu
        /// </summary>
        /// <returns>
        /// A number that the user enter
        /// </returns>
        static int GetChoice()
        {
            Console.Write("Enter your choice: ");
            int userChoice;
            int.TryParse(Console.ReadLine(), out userChoice);
            return userChoice;
        }

        /// <summary>
        /// Function to get the tower's height from the user
        /// </summary>
        /// <returns>
        /// The tower's height
        /// </returns>
        static double InputHeightTower()
        {
            Console.Write("Enter the height of the tower: ");
            double height;
            double.TryParse(Console.ReadLine(), out height);
            /*The following condition is not necessary
            since it is given in the exercise that a correct input is guaranteed for the height of a tower.*/
            if (height < 2)
                throw new ArgumentException("Height must be more or equal to 2");
            return height;
        }

        /// <summary>
        /// Function to get the tower's width from the user
        /// </summary>
        /// <returns>
        /// The tower's width
        /// </returns>
        static double InputWidthTower()
        {
            Console.Write("Enter the width of the tower: ");
            double width;
            double.TryParse(Console.ReadLine(), out width);
            return width;
        }

        static void CreateRectangleTower()
        {
            Console.WriteLine("Creating a Rectangle Tower");
            Console.WriteLine("---------------------------");
            shape = new Rectangle();
            shape.Height = InputHeightTower();
            shape.Width = InputWidthTower();

            /*
             I understood from the exercise that 2 things must be checked for area printing:
             1. Is the rectangle a square - the width is equal to the height.
             2. Is the difference of the sides of the rectangle greater than 5.
             For any other case, meaning that it is a rectangle whose side difference is less than 5 but greater than 0
             (because 0 will be when the rectangle is a square) the perimeter will be printed.
             */
            if (Math.Abs(shape.Height - shape.Width) > 5 || shape.Height == shape.Width)
            {
                if (shape is Rectangle)
                {
                    double area = ((Rectangle)shape).CalculateArea();
                    Console.WriteLine($"Rectangle tower's area is: {area}");
                }
                else
                {
                    throw new ArgumentException("For using CalculateArea the shape must be Rectangle type");
                }
            }
            else
            {
                double perimeter = shape.CalculatePerimeter();
                Console.WriteLine($"Rectangle tower's perimeter is: {perimeter}");
            }
        }

        static void CreateTriangleTower()
        {
            Console.WriteLine("Creating a Triangle Tower");
            Console.WriteLine("--------------------------");
            shape = new Triangle();
            shape.Height = InputHeightTower();
            shape.Width = InputWidthTower();

            // Perform actions specific to creating a triangular tower
            Console.WriteLine($"Triangle Tower created with height: {shape.Height} and base width: {shape.Width}");
        }
    }

}

