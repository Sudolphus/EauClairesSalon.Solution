DROP DATABASE IF EXISTS `micheal_hansen`;
CREATE DATABASE IF NOT EXISTS `micheal_hansen`;
USE `micheal_hansen`;
DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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