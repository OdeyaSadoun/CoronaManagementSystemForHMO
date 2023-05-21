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

1. Clone the Git repository: https://github.com/OdeyaSadoun/Hadasim3HomeEX
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

### Client-Side Access

In addition to the API, the system also provides a client-side view accessible through the home screen.
When you enter the address `localhost:5004` in a web browser after running the program and uploading the server, you will be directed to the home screen of the client view.

The home screen offers three main options:

1. **Adding a Person**: Clicking on this option will take you to a form where you can enter details for a new person in the system. After submitting the form, the information will be sent to the appropriate API endpoint via an AJAX request using the jQuery library. The person's details will be stored in the database.

2. **Displaying People**: This option allows you to view all the people in the system. For each person, you have the following options:
   - Display Details: Provides a full view of the person's details.
   - Add Positive Corona Test: Opens a page where you can enter the date of exposure to the virus. Submitting the form will add the positive corona test to the appropriate table in the database.
   - Update Recovery Date: Allows you to update the date of recovery from the virus. This option is available only when the date of receiving a positive answer is already entered.
   - Insert Vaccine: Opens a window for entering the date and manufacturer's name of the vaccine. Submitting the form will add the vaccine to the appropriate table in the database. Note that only up to four vaccines can be added for each person.

3. **Statistics Display**: This option provides two types of statistics:
   - Number of Patients per Day: Displays the number of patients for each day of the last month.
   - Number of Unvaccinated People: Shows the number of people who have not been vaccinated at all.

## Entities

The system manages three types of entities: Person, CoronaVaccine, and CoronaTest.

 Each entity corresponds to a table in the database.

### Person

The `Person` entity represents an person and has the following attributes:

- `PersonId`: Unique identifier for the person.
- `FirstName`: First name of the person.
- `LastName`: Last name of the person.
- `ID`: Identification number of the person.
- `DateOfBirth`: Date of birth of the person.
- `Telephone`: Telephone number of the person.
- `MobilePhone`: Mobile phone number of the person.
- `City`: City where the person resides.
- `Street`: Street where the person resides.
- `NumberStreet`: House number on the street.

### CoronaVaccine

The `CoronaVaccine` entity represents a corona vaccine and has the following attributes:

- `VaccineId`: Unique identifier for the vaccine.
- `PersonId`: ID of the person associated with the vaccine.
- `VaccinationDate`: Date when the vaccine was administered.
- `Manufacturer`: Name of the vaccine manufacturer.

### CoronaTest

The `CoronaTest` entity represents a corona test and has the following attributes:

- `TestId`: Unique identifier for the test.
- `PersonId`: ID of the person associated with the test.
- `PositiveDate`: Date when the positive test result was received.
- `RecoveryDate`: Date when the person recovered from the virus. (Nullable)




Note that the system extends the requirements of the exercise by allowing the updating of the recovery date and implementing additional features beyond the exercise scope.




