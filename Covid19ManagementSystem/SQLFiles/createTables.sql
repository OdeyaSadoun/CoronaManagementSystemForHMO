
use coronadatabase;

CREATE TABLE IF NOT EXISTS Person (
    PersonId INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    ID VARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Telephone VARCHAR(50) NOT NULL,
    MobilePhone VARCHAR(50) NOT NULL,
    City VARCHAR(50) NOT NULL,
    Street VARCHAR(50) NOT NULL,
    NumberStreet VARCHAR(50) NOT NULL,
    PersonImage LONGTEXT
);


CREATE TABLE IF NOT EXISTS CoronaTest (
    TestId INT AUTO_INCREMENT PRIMARY KEY,
    PersonId INT NOT NULL,
    PositiveDate DATE NOT NULL,
    RecoveryDate DATE,
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
);

CREATE TABLE IF NOT EXISTS CoronaVaccine (
    VaccineId INT AUTO_INCREMENT PRIMARY KEY,
    PersonId INT NOT NULL,
    VaccinationDate DATE NOT NULL,
    Manufacturer VARCHAR(50) NOT NULL,
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
);
