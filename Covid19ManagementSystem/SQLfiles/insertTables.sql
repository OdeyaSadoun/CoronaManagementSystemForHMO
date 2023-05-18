INSERT INTO Person (PersonId, FirstName, LastName, ID, DateOfBirth, Telephone, MobilePhone, City, Street, NumberStreet)
VALUES
    (1, 'John', 'Doe', '123456789', '1990-01-01', '555-1234', '555-5678', 'New York', 'Main Street', '123'),
    (2, 'Jane', 'Smith', '987654321', '1985-05-10', '555-4321', '555-8765', 'Los Angeles', 'Broadway', '456'),
    (3, 'Michael', 'Johnson', '456123789', '1998-03-15', '555-2468', '555-1357', 'Chicago', 'Oak Street', '789'),
    (4, 'Emily', 'Brown', '321654987', '1992-07-20', '555-6789', '555-9876', 'Houston', 'Elm Street', '246'),
    (5, 'Daniel', 'Taylor', '789456123', '1987-11-30', '555-3698', '555-7412', 'San Francisco', 'Market Street', '135'),
    (6, 'Sophia', 'Davis', '654987321', '1995-09-05', '555-8520', '555-9630', 'Boston', 'Park Avenue', '369'),
    (7, 'Matthew', 'Miller', '159753468', '1991-04-12', '555-7410', '555-9631', 'Seattle', 'Pine Street', '852'),
    (8, 'Olivia', 'Wilson', '753159852', '1988-12-25', '555-3690', '555-8521', 'Miami', 'Ocean Drive', '741'),
    (9, 'James', 'Anderson', '852963741', '1994-06-18', '555-9632', '555-7411', 'Dallas', 'Maple Avenue', '963'),
    (10, 'Emma', 'Thomas', '963852741', '1997-02-14', '555-8522', '555-3691', 'Phoenix', 'Sunset Boulevard', '852');
    
INSERT INTO CoronaTest (TestId, PersonId, PositiveDate, RecoveryDate)
VALUES
    (1, 1, '2022-03-05', NULL),
    (2, 3, '2022-02-15', '2022-03-01'),
    (3, 5, '2022-04-10', NULL),
    (4, 7, '2022-03-20', NULL),
    (5, 9, '2022-01-25', '2022-02-10'),
    (6, 2, '2022-05-05', NULL),
    (7, 4, '2022-04-15', '2022-05-01'),
    (8, 6, '2022-03-25', '2022-04-10'),
    (9, 8, '2022-02-10', NULL),
    (10, 10, '2022-05-15', NULL);

INSERT INTO CoronaVaccine (VaccineId, PersonId, VaccinationDate, Manufacturer)
VALUES
    (1, 1, '2022-04-05', 'Pfizer'),
    (2, 3, '2022-03-01', 'Moderna'),
    (3, 5, '2022-05-10', 'Johnson & Johnson'),
    (4, 7, '2022-04-20', 'Pfizer'),
    (5, 9, '2022-02-25', 'Moderna'),
    (6, 2, '2022-06-05', 'Pfizer'),
    (7, 4, '2022-05-15', 'Johnson & Johnson'),
    (8, 6, '2022-04-25', 'Moderna'),
    (9, 8, '2022-03-10', 'Pfizer'),
    (10, 10, '2022-06-15', 'Moderna');
