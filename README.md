# Exercises Hadasim 3 | Odeya Sadoun

## Overview
The current project contains 2 sets for the 2 homework assignments that were required to be submitted.
First system - Twitter towers, and second system - Corona management system for the HMO.

## Table of Contents
- [System 1: Twitter Towers](#system-1-twitter-towers)
  - [Overview](#overview)
  - [Description](#description)
  - [Installation](#installation)
  - [Project Run](#project-run)
  - [Project Structure](#project-structure)
- [System 2: Corona Management System for HMO](#system-2-corona-management-system-for-hmo)
  - [Overview](#overview-1)
  - [Installation](#installation-1)
  - [Database Setup](#database-setup)
  - [Usage](#usage)
    - [API Access](#api-access)
    - [Client-Side Access](#client-side-access)
  - [Entities](#entities)

# System 1: Twitter Towers

## Overview
The Twitter company is expanding into the field of construction and real estate. This project aims to present different tower options for Twitter and create an interface that allows Twitter to understand the available options.

## Description
The "Twitter Towers" project is written in the C# language. To use the project, you can download it from the Git repository and work with a compatible development environment, with Visual Studio being the recommended choice.

## Installation
To set up the project, follow these steps:
1. Clone the Git repository:  [https://github.com/OdeyaSadoun/Hadasim3HomeEX](https://github.com/OdeyaSadoun/Hadasim3HomeEX)
2. Open the project in Visual Studio or any other compatible C# development environment.
3. Set the TwitterTowers project as the startup project.

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
#option 3

![RectArea](/images/project1/rectArea.png)

#option 4

![RectPerm](/images/project1/rectPerm.png)

<br>


### Triangular Tower

For a triangular tower:
1. After choosing the triangle option from the main menu, an additional menu is presented.
2. The user can choose whether they are interested in the perimeter of the triangle or its printing.
3. If the user selects the perimeter, the program calculates the perimeter and prints its value.
4. Otherwise, the program prints the triangle in a star display based on the project settings.
<br>
#option 3

![TriPerm](/images/project1/triPerm.png)

#option 4

![TriPrint](/images/project1/triPrint.png)

<br>


Throughout the project, input integrity checks are performed to ensure data validity.

## Project Structure
The project is structured as follows:
- There is a dedicated class library project for saving the entities related to shapes.
- The Shape class is an abstract class that represents a shape and is inherited by the Rectangle and Triangle classes.
- Both the Rectangle and Triangle classes implement the CalculatePerimeter function, with each class implementing its unique functionality.

The menu and project run are inside a console application project of a type that contains a reference to shapes so that it can use the models found in the class library.

<br>
<br>

## System 2: Corona Management System for HMO

## Overview
The Corona Management System for HMO is a web-based application designed to manage a corona database for a health fund. The system provides functionality to view and manage members of the health insurance fund and perform data entry for the database records. The database used is MySQL, and the application is built using C# and ASP.NET Core Web API technology.

## Installation

To run the code and set up the project, follow these steps:

1. Clone the Git repository: [https://github.com/OdeyaSadoun/Hadasim3HomeEX](https://github.com/OdeyaSadoun/Hadasim3HomeEX)
2. Ensure that a suitable development environment, such as Visual Studio 2022, is installed on your machine.
3. Install the following libraries from NuGet Package Manager:
   - MySql.Data
   - MySqlConnector
   - Microsoft.AspNetCore.Mvc
3. Set the Covid19ManagementSystem project as the startup project.

## Database Setup

To properly use the code and run the application, you need to have MySQL installed on your computer. After installing MySQL, perform the following steps:

1. Run the SQL files provided in the `SQLFiles` folder. The files are as follows:
   - `createSchem` - Creates the database schema.
   - `createTable` - Creates all the necessary tables.
   - `insertTable` - Fills the tables with initial data.

Once the SQL files are executed, you can start working with the database.

## Usage

### API Access

The system provides an API that exposes various endpoints for performing GET, POST, and PUT operations. The API can be accessed by sending HTTP requests to the appropriate endpoints. Detailed documentation for the API endpoints can be found using Swagger, which allows you to explore and interact with the API without the need for a separate client.

To access Swagger, open a web browser after running the program and uploading the server and enter the address `[localhost:5004](http://localhost:5004/swagger/index.html)`.
This will take you to the Swagger UI, where you can view and test the available API endpoints.
![swaggerMain](/images/project2/swaggerMain.png)
<br>

Example API endpoints:
- `GET /api/patients`: Get all persons.
![getAllVaccinesSwagger](/images/project2/getAllVaccinesSwagger.png)
<br>

- `POST /api/patients`: Create a new Person.
![addPerson1Swagger](/images/project2/addPerson1Swagger.png)
![addPerson2Swagger](/images/project2/addPerson2Swagger.png)
<br>

Receiving specific corona test data for a certain person by id:
![getByIdCoronaTestSwagger](/images/project2/getByIdCoronaTestSwagger.png)
<br>

Getting the statistics for the last month how many patients were every day:
![statisticsSwagger](/images/project2/statisticsSwagger.png)
<br>

Getting the number of people who are not vaccinated at all:
![unvaccinedSwagger](/images/project2/unvaccinedSwagger.png)
<br>

You can see that it is like a UI that allows you to get all the data through their RESTT API.


### Client-Side Access

To use the client-side interface, follow these steps:
1. Run the "CoronaManagementSystem" project in Visual Studio.
2. Open a web browser and navigate to `https://localhost:5004`.

The client-side interface allows you to manage patients, tests, and lab results through a user-friendly web interface.

The home screen offers three main options:
![clientMain](/images/project2/clientMain.png)
<br>

1. **Adding a Person**: Clicking on this option will take you to a form where you can enter details for a new person in the system. After submitting the form, the information will be sent to the appropriate API endpoint via an AJAX request using the jQuery library. The person's details will be stored in the database.
![addPersonClient](/images/project2/addPersonClient.png)
<br>

2. **Displaying People**: This option allows you to view all the people in the system.
![personsListClient](/images/project2/personsListClient.png)
<br>

For each person, you have the following options:
   - Display Details: Provides a full view of the person's details.
   - include his vaccines.
![personDetails](/images/project2/personDetails.png)
<br>

   - Add Positive Corona Test: Opens a page where you can enter the date of exposure to the virus. Submitting the form will add the positive corona test to the appropriate table in the database.
![addPositiveTest](/images/project2/addPositiveTest.png)
<br>

   - Update Recovery Date: Allows you to update the date of recovery from the virus. This option is available only when the date of receiving a positive answer is already entered.
![setRecoveryDate](/images/project2/setRecoveryDate.png)
<br>

   - Insert Vaccine: Opens a window for entering the date and manufacturer's name of the vaccine. Submitting the form will add the vaccine to the appropriate table in the database. Note that only up to four vaccines can be added for each person.
![addVaccine](/images/project2/addVaccine.png)
<br>


3. **Statistics Display**: This option provides two types of statistics:
   - Number of Patients per Day: Displays the number of patients for each day of the last month.
   - Number of Unvaccinated People: Shows the number of people who have not been vaccinated at all.

### Entities

The main entities in the system are:
- Person: Represents a person and contains information such as name, ID, and contact details.
- CoronaTest: Represents a corona test and includes the person's ID, positive test date and the recovery date.
- CoronaVaccine: Represents the corona vaccine and includes the vaccine id, the vaccine date and the manufacturer.

These entities are stored in the MYSQL database and can be managed through the API or client-side interface.



# Remarks:

## Part of the quality strategy is attached as a PDF file.

## Bonuses:
1. Creating a client side that uses the API I created in order to present the data visually.
2. Creating an API for calculating a summary view: one for calculating "how many active patients there were every day in the last month", and the other for calculating the amount of health insurance members who are not vaccinated at all.
3. Creating an API (and implementing it on the client side as well) for updating the date of recovery with a positive corona test result.

## Options for adding development below:
1. Displaying the statistics data in a graph in a visual way.
2. The possibility of uploading an image for each client, preference is given to the Base64 format that converts an image to a string file and when you want to display the image again, you do the reverse conversion. The use of this format makes it possible to save all the images in the database without the need to save locally.
3. Building an architectural specification of the system.


## Conclusion

In conclusion, the exercises in this project demonstrate the implementation of two different systems: "Twitter Towers" and the "Corona Management System for HMO." The "Twitter Towers" project focuses on presenting tower options for Twitter, while the "Corona Management System for HMO" provides a system for managing corona-related data within an HMO.

By following the installation instructions and running the projects, you can explore their functionalities and understand the concepts applied.

## Additional Information

If you have any questions or suggestions regarding the project, please feel free to reach out to me. I would be happy to hear your feedback and engage in discussions.
odeya.sadoun@gmail.com
