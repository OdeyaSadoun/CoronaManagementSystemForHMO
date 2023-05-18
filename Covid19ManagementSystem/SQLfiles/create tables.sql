use coronadatabase;

CREATE TABLE Person (
    PersonId INT PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    ID VARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Telephone VARCHAR(50) NOT NULL,
    MobilePhone VARCHAR(50) NOT NULL,
	City VARCHAR(50) NOT NULL,
    Street VARCHAR(50) NOT NULL,
    NumberStreet VARCHAR(50) NOT NULL

);

CREATE TABLE CoronaTest (
    TestId INT PRIMARY KEY,
    PersonId INT NOT NULL,
    PositiveDate DATE,
    RecoveryDate DATE,
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
);

CREATE TABLE CoronaVaccine (
    VaccineId INT PRIMARY KEY,
    PersonId INT NOT NULL,
    VaccinationDate DATE NOT NULL,
    Manufacturer VARCHAR(50) NOT NULL,
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
);
