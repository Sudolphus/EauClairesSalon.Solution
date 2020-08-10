# _Eau Claire's Salon_

#### _An app to keep track of a salon, 7.31.20_

#### By _**Micheal Hansen**_

## Description

_This app is designed to keep track of the employees of a salon, along with their regular customers, using a MySQL database on the backend integrated into a .NET MVC via Entity Framework Core._

## Specifications

| Spec | Input | Output |
| :--- | :---: | ---: |
| User can access the home page| Localhost:5000 | Homepage splash screen|
| User can view a list of stylists| Click "View Stylists" | List of stylists |
| User can view a stylist's details, including a list of their clients | Clicks a stylist's name | Stylist's details |
| User can add new stylists to the database | Clicks Add New | New stylist added to database |
| User can add clients to a stylist | Clicks Add Client | Client added to stylist |

## Setup/Installation Requirements

Software Requirements
1. .NET framework
2. MySQL Server
3. A code editor (Visual Studio Code, Atom, etc.)
4. A SQL database manager (MySql Workbench, etc. Techically optional)

Acquire The Repo:
1. Click the 'Clone or Download Button'
2. Alternately, Clone via Bash/GitBash: `git clone https://github.com/Sudolphus/EauClairesSalon.Solution.git`

Editting the Code Base:
1. Open the project in your code editor; with Bash, this is done by navigating to the project directory, then `code .`

Running the program:
1. First, you'll need to acquire the necessary package by running `dotnet restore` in the HairdSaon directory.
2. To run the program, you'll need to compile the code: `dotnet build`. This will create a compiled application in the bin/ folder.
3. Alternately, you can run the program directly with `dotnet run`.

SQL Database:
1. First, create the database. If you have a SQL Database Manager, you can directly import the schema from the Micheal_Hansen.sql file included in the top level of the project.
2. Alternately, you can create the database manually. In a terminal with MySQL running, enter:
```
DROP DATABASE IF EXISTS `micheal_hansen`;
CREATE DATABASE IF NOT EXISTS `micheal_hansen`;
USE `micheal_hansen`;

DROP TABLE IF EXISTS `stylists`;
CREATE TABLE `stylists` (
  `StylistId` int NOT NULL AUTO_INCREMENT,
  `Name` longtext,
  PRIMARY KEY (`StylistId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DROP TABLE IF EXISTS `clients`;
CREATE TABLE `clients` (
  `ClientId` int NOT NULL AUTO_INCREMENT,
  `Name` longtext,
  `StylistId` int NOT NULL,
  PRIMARY KEY (`ClientId`),
  KEY `IX_Clients_StylistId` (`StylistId`),
  CONSTRAINT `FK_Clients_Stylists_StylistId` FOREIGN KEY (`StylistId`) REFERENCES `stylists` (`StylistId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DROP TABLE IF EXISTS `appointments`;
CREATE TABLE `appointments` (
  `AppointmentId` int NOT NULL AUTO_INCREMENT,
  `DayTime` datetime(6) NOT NULL,
  `StylistId` int NOT NULL,
  `ClientId` int NOT NULL,
  PRIMARY KEY (`AppointmentId`),
  KEY `IX_Appointments_ClientId` (`ClientId`),
  KEY `IX_Appointments_StylistId` (`StylistId`),
  CONSTRAINT `FK_Appointments_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `clients` (`ClientId`) ON DELETE CASCADE,
  CONSTRAINT `FK_Appointments_Stylists_StylistId` FOREIGN KEY (`StylistId`) REFERENCES `stylists` (`StylistId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
```
3. Alternately, alternately, the migrations folder can allow you to import the database directly. To do so, in the HairSalon directory, run `dotnet ef database update`.
4. You'll also need to configure the appsettings.json file, by changing {Your_Password} to your MySql Server password. If your computer uses a different port, if you're not the root user, or you've decided to import the database with a different name, you'll need to update those fields as well.
   
## Known Bugs

_Full CRUD functionality for appointments doesn't exist, and there's no protection against double-scheduling._

## Support and contact details

_Please reach out through my GitHub account._

## Technologies Used

* _VSCode_
* _C# and .NET Core_
* _MySQL Server and MySQL Workbench_
* _Entity Core_
