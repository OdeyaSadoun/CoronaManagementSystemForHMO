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
                        try
                        {
                            CreateRectangleTower();
                        }
                        catch (ArgumentException ae)
                        {
                            Console.WriteLine(ae);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 2:
                        try
                        {
                            CreateTriangleTower();
                        }
                        catch (ArgumentException ae)
                        {
                            Console.WriteLine(ae);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
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
            Console.WriteLine("Twitter Towers Program");
            Console.WriteLine("---------------------");
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
            Console.WriteLine("");
            Console.Write("Enter your choice: ");
            int userChoice;
            int.TryParse(Console.ReadLine(), out userChoice);
            Console.WriteLine("");
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

        /// <summary>
        /// Function to create rectangle tower
        /// The user enter height and width and after it the function print or the area or the perimeter.
        /// </summary>
        static void CreateRectangleTower()
        {
            Console.WriteLine("Creating a Rectangle Tower");
            Console.WriteLine("---------------------------");
            shape = new Rectangle();
            shape.Height = InputHeightTower();
            shape.Width = InputWidthTower();
            Console.WriteLine(' ');
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

        /// <summary>
        /// Function to create triangle tower
        /// The user enter height and width and after it the function print or the perimeter or the triangle in * format.
        /// </summary>
        static void CreateTriangleTower()
        {
            Console.WriteLine("Creating a Triangle Tower");
            Console.WriteLine("--------------------------");
            shape = new Triangle();
            shape.Height = InputHeightTower();
            shape.Width = InputWidthTower();
            Console.WriteLine(' ');

            //triangle internal menu:
            Console.WriteLine("Triangle choices:");
            Console.WriteLine("---------------------");
            Console.WriteLine("1. Calculate the triangle's perimeter");
            Console.WriteLine("2. Print the triangle");

            int triangleChoice = GetChoice();

            switch (triangleChoice)
            {
                case 1:
                    double perimeter = shape.CalculatePerimeter();
                    Console.WriteLine($"Triangle tower's perimeter is: {perimeter}");
                    break;
                case 2:
                    /*
                     From the exercise I understood that printing the triangle is only possible if
                     the width is shorter than twice the height of the triangle and the width of the triangle is also odd.
                     Therefore, to the condition of checking whether the printing of the triangle is possible,
                     I added a check of whether the width is exactly equal to 2 times the height,
                     because according to my understanding, even with such an option, the triangle cannot be printed.
                     *
                     * 
                     The options where the triangle cannot be printed:
                     1. The width of the triangle is an even number.
                     2. Its width is longer OR equal than twice its height.
                     */
                    if (shape.Width % 2 == 0 || shape.Width >= shape.Height * 2)
                    {
                        Console.WriteLine("The triangle cannot be printed");
                    }
                    else
                    {
                        if (shape is Triangle)
                        {
                            ((Triangle)shape).PrintRectangle();
                        }
                        else
                        {
                            throw new ArgumentException("For using PrintRectangle the shape must be Triangle type");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
            Console.WriteLine();
        }
    }
}

