# Exercises Hadasim 3 | Odeya Sadoun

## Overview
The current project contains 2 sets for the 2 homework assignments that were required to be submitted.
First system - Twitter towers, and second system - Corona management system for the HMO.

# System 1: Twitter Towers

## Overview
The Twitter company is expanding into the field of construction and real estate. This project aims to present different tower options for Twitter and create an interface that allows Twitter to understand the available options.

## Description
The "Twitter Towers" project is written in the C# language. To use the project, you can download it from the Git repository and work with a compatible development environment, with Visual Studio being the recommended choice.

## Installation
To set up the project, follow these steps:
1. Clone the Git repository: https://github.com/OdeyaSadoun/Hadasim3HomeEX
2. Open the project in Visual Studio or any other compatible C# development environment.

## Project Run
The project follows a menu-driven approach. Upon starting the program, the user is presented with a menu to choose between building a rectangular or triangular tower.
<br>
<br>
![Menu](/images/project1/mainMenu.png)
<br>

The menu will continue to display until the user selects option 3 to exit the system:
<br>
<br>
![Exit](/images/project1/exit.png)
<br>

### Rectangular Tower

For a rectangular tower:

1. Enter the height and width of the tower.
2. The program performs tests based on the user's input.
3. If the tower is a square or a rectangle with a difference between the lengths of its sides greater than 5, the area of the rectangle will be printed.
4. Otherwise, the perimeter of the rectangle will be printed.

<br>
![rectArea](/images/project1/rectArea.png)
<br>

<br>
![rectPerm](/images/project1/rectPerm.png)
<br>



### Triangular Tower


For a triangular tower:
1. After choosing the triangle option from the main menu, an additional menu is presented.
2. The user can choose whether they are interested in the perimeter of the triangle or its printing.
3. If the user selects the perimeter, the program calculates the perimeter and prints its value.
4. Otherwise, the program prints the triangle in a star display based on the project settings.

Throughout the project, input integrity checks are performed to ensure data validity.

## Project Structure
The project is structured as follows:
- There is a dedicated class library project for saving the entities related to shapes.
- The Shape class is an abstract class that represents a shape and is inherited by the Rectangle and Triangle classes.
- Both the Rectangle and Triangle classes implement the CalculatePerimeter function, with each class implementing its unique functionality.

The menu and project run are inside a console application project of a type that contains a reference to shapes so that it can use the models found in the class library.



## System 2: 
