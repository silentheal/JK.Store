/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--INSERT INTO Addresses(Address1, Address2, City, [State], Zipcode) VALUES
--('22 W Monroe', 'Suite 100', 'Chicago', 'IL', '60601'),
--('1000 N Wells', NULL, 'Chicago', 'IL', '60654'),
--('862 W 55th', 'Apt 2', 'Chicago', 'IL', '60618'),
--('3476 W Irving Park', NULL, 'Chicago', 'IL', '60604'),
--('222 W Ontario', 'Suite 450', 'Chicago', 'IL', '60654')

--INSERT INTO Policies(Number, EffectiveDate, AddressID) VALUES
--(100, GetDate(), (SELECT TOP 1 ID FROM Addresses WHERE Address1 = '22 W Monroe')),
--(200, GetDate(), (SELECT TOP 1 ID FROM Addresses WHERE Address1 = '1000 N Wells')),
--(300, GetDate(), (SELECT TOP 1 ID FROM Addresses WHERE Address1 = '862 W 55th')),
--(400, GetDate(), (SELECT TOP 1 ID FROM Addresses WHERE Address1 = '3476 W Irving Park')),
--(500, GetDate(), (SELECT TOP 1 ID FROM Addresses WHERE Address1 = '222 W Ontario'))

--INSERT INTO Users (FirstName, LastName, PolicyID, AddressID) VALUES
--('Bugs', 'Bunny', (SELECT TOP 1 ID FROM Policies WHERE Number = 100), (SELECT TOP 1 ID FROM Addresses WHERE Address1 = '22 W Monroe')),
--('Daffy', 'Duck', (SELECT TOP 1 ID FROM Policies WHERE Number = 200), (SELECT TOP 1 ID FROM Addresses WHERE Address1 = '1000 N Wells')),
--('Elmer', 'Fudd', (SELECT TOP 1 ID FROM Policies WHERE Number = 300), (SELECT TOP 1 ID FROM Addresses WHERE Address1 = '862 W 55th')),
--('Marvin', 'Martian', (SELECT TOP 1 ID FROM Policies WHERE Number = 400), (SELECT TOP 1 ID FROM Addresses WHERE Address1 = '3476 W Irving Park')),
--('Speedy', 'Gonzalez', (SELECT TOP 1 ID FROM Policies WHERE Number = 500), (SELECT TOP 1 ID FROM Addresses WHERE Address1 = '222 W Ontario'))

SET IDENTITY_INSERT Country ON

INSERT INTO Country(ID, Name, Abbreviation) VALUES
(1, 'United States of America', 'USA'),
(2, 'Canada', 'CAN')

SET IDENTITY_INSERT Country OFF

INSERT INTO State(CountryID, Abbreviation, Name) VALUES

(1, 'AL', 'Alabama'),
(1, 'AK', 'Alaska'),
(1, 'AZ', 'Arizona'),
(1, 'AR', 'Arkansas'),
(1, 'CA', 'California'),
(1, 'CO', 'Colorado'),
(1, 'CT', 'Connecticut'),
(1, 'DE', 'Delaware'),
(1, 'DC', 'District Of Columbia'),
(1, 'FL', 'Florida'),
(1, 'GA', 'Georgia'),
(1, 'HI', 'Hawaii'),
(1, 'ID', 'Idaho'),
(1, 'IL', 'Illinois'),
(1, 'IN', 'Indiana'),
(1, 'IA', 'Iowa'),
(1, 'KS', 'Kansas'),
(1, 'KY', 'Kentucky'),
(1, 'LA', 'Louisiana'),
(1, 'ME', 'Maine'),
(1, 'MD', 'Maryland'),
(1, 'MA', 'Massachusetts'),
(1, 'MI', 'Michigan'),
(1, 'MN', 'Minnesota'),
(1, 'MS', 'Mississippi'),
(1, 'MO', 'Missouri'),
(1, 'MT', 'Montana'),
(1, 'NE', 'Nebraska'),
(1, 'NV', 'Nevada'),
(1, 'NH', 'New Hampshire'),
(1, 'NJ', 'New Jersey'),
(1, 'NM', 'New Mexico'),
(1, 'NY', 'New York'),
(1, 'NC', 'North Carolina'),
(1, 'ND', 'North Dakota'),
(1, 'OH', 'Ohio'),
(1, 'OK', 'Oklahoma'),
(1, 'OR', 'Oregon'),
(1, 'PA', 'Pennsylvania'),
(1, 'RI', 'Rhode Island'),
(1, 'SC', 'South Carolina'),
(1, 'SD', 'South Dakota'),
(1, 'TN', 'Tennessee'),
(1, 'TX', 'Texas'),
(1, 'UT', 'Utah'),
(1, 'VT', 'Vermont'),
(1, 'VA', 'Virginia'),
(1, 'WA', 'Washington'),
(1, 'WV', 'West Virginia'),
(1, 'WI', 'Wisconsin'),
(1, 'WY', 'Wyoming'),
(2, 'AB', 'Alberta'),
(2, 'BC', 'British Columbia'),
(2, 'MB', 'Manitoba'),
(2, 'NB', 'New Brunswick'),
(2, 'NL', 'Newfoundland and Labrador'),
(2, 'NS', 'Nova Scotia'),
(2, 'NT', 'Northwest Territories'),
(2, 'NU', 'Nunavut'),
(2, 'ON', 'Ontario'),
(2, 'PE', 'Prince Edward Island'),
(2, 'QC', 'Quebec'),
(2, 'SK', 'Saskatchewan'),
(2, 'YT', 'Yukon')

INSERT INTO Category(Name, Description) VALUES
('Tabletop', 'games played on top of a normal table'),
('Family', 'Appropriate for most ages and skill levels')

INSERT INTO Product(Name, Price, Description) VALUES
('Clue', 19.99, 'Fun for the whole family'),
('Monopoly', 29.99, 'The game of real-estate empires'),
('Payday', 15.99, 'Pay your bills on time and enjoy retro game artwork')

INSERT INTO ProductImage(Path, ProductID, AltText) VALUES
('/content/clue.jpg', (SELECT TOP 1 ID FROM Product WHERE Name = 'Clue'), 'Clue'),
('/content/monopoly.jpg', (SELECT TOP 1 ID FROM Product WHERE Name = 'Monopoly'), 'Monopoly'),
('/content/payday.jpg', (SELECT TOP 1 ID FROM Product WHERE Name = 'Payday'), 'Payday')

INSERT INTO CategoryProduct(CategoryID, ProductID) VALUES
((SELECT TOP 1 ID FROM Category WHERE Name = 'Tabletop'), (SELECT TOP 1 ID FROM Product WHERE Name = 'Clue')),
((SELECT TOP 1 ID FROM Category WHERE Name = 'Tabletop'), (SELECT TOP 1 ID FROM Product WHERE Name = 'Monopoly')),
((SELECT TOP 1 ID FROM Category WHERE Name = 'Tabletop'), (SELECT TOP 1 ID FROM Product WHERE Name = 'Payday')),
((SELECT TOP 1 ID FROM Category WHERE Name = 'Family'), (SELECT TOP 1 ID FROM Product WHERE Name = 'Clue')),
((SELECT TOP 1 ID FROM Category WHERE Name = 'Family'), (SELECT TOP 1 ID FROM Product WHERE Name = 'Monopoly')),
((SELECT TOP 1 ID FROM Category WHERE Name = 'Family'), (SELECT TOP 1 ID FROM Product WHERE Name = 'Payday'))
