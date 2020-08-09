DROP DATABASE IF EXISTS `Micheal_Hansen`;
CREATE DATABASE `Micheal_Hansen`;

USE `Micheal_Hansen`;

DROP TABLE IF EXISTS `appointments`;
CREATE TABLE `appointments` (
  `AppointmentId` int NOT NULL AUTO_INCREMENT,
  `Time` datetime NOT NULL,
  `StylistId` int DEFAULT '0',
  `ClientId` int DEFAULT '0',
  PRIMARY KEY (`AppointmentId`),
  KEY `StylistId_idx` (`StylistId`),
  KEY `ClientId_idx` (`ClientId`),
  CONSTRAINT `ApptClient` FOREIGN KEY (`ClientId`) REFERENCES `clients` (`ClientID`),
  CONSTRAINT `ApptStylist` FOREIGN KEY (`StylistId`) REFERENCES `stylists` (`StylistId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DROP TABLE IF EXISTS `clients`;
CREATE TABLE `clients` (
  `ClientID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `StylistID` int DEFAULT '0',
  PRIMARY KEY (`ClientID`),
  KEY `StylistID_idx` (`StylistID`),
  CONSTRAINT `StylistID` FOREIGN KEY (`StylistID`) REFERENCES `stylists` (`StylistId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DROP TABLE IF EXISTS `stylists`;
CREATE TABLE `stylists` (
  `StylistId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  PRIMARY KEY (`StylistId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;