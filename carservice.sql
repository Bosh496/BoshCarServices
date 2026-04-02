/*
 Navicat Premium Dump SQL

 Source Server         : local_machine
 Source Server Type    : MySQL
 Source Server Version : 80044 (8.0.44)
 Source Host           : localhost:3306
 Source Schema         : carservice

 Target Server Type    : MySQL
 Target Server Version : 80044 (8.0.44)
 File Encoding         : 65001

 Date: 01/04/2026 21:03:51
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for __efmigrationshistory
-- ----------------------------
DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE `__efmigrationshistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for customermasters
-- ----------------------------
DROP TABLE IF EXISTS `customermasters`;
CREATE TABLE `customermasters`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `userId` int NOT NULL,
  `Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Mobile` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `createdBy` int NULL DEFAULT NULL,
  `createdDate` datetime(6) NULL DEFAULT NULL,
  `modifiedBy` int NULL DEFAULT NULL,
  `modifiedDate` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_CustomerMasters_userId`(`userId` ASC) USING BTREE,
  CONSTRAINT `FK_CustomerMasters_LoginMasters_userId` FOREIGN KEY (`userId`) REFERENCES `loginmasters` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for loginmasters
-- ----------------------------
DROP TABLE IF EXISTS `loginmasters`;
CREATE TABLE `loginmasters`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `createdBy` int NULL DEFAULT NULL,
  `createdDate` datetime(6) NULL DEFAULT NULL,
  `modifiedBy` int NULL DEFAULT NULL,
  `modifiedDate` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for servicemasters
-- ----------------------------
DROP TABLE IF EXISTS `servicemasters`;
CREATE TABLE `servicemasters`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `VId` int NOT NULL,
  `Mileage` int NOT NULL,
  `TotalBill` decimal(10, 2) NOT NULL,
  `RewardPoints` int NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `createdBy` int NULL DEFAULT NULL,
  `createdDate` datetime(6) NULL DEFAULT NULL,
  `modifiedBy` int NULL DEFAULT NULL,
  `modifiedDate` datetime(6) NULL DEFAULT NULL,
  `FileName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ContentType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `FileData` longblob NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_ServiceMasters_VId`(`VId` ASC) USING BTREE,
  CONSTRAINT `FK_ServiceMasters_VehicleMasters_VId` FOREIGN KEY (`VId`) REFERENCES `vehiclemasters` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 19 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for servicetypemappings
-- ----------------------------
DROP TABLE IF EXISTS `servicetypemappings`;
CREATE TABLE `servicetypemappings`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ServiceId` int NOT NULL,
  `ServiceTypeId` int NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_ServiceTypeMappings_ServiceId`(`ServiceId` ASC) USING BTREE,
  INDEX `IX_ServiceTypeMappings_ServiceTypeId`(`ServiceTypeId` ASC) USING BTREE,
  CONSTRAINT `FK_ServiceTypeMappings_ServiceMasters_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `servicemasters` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_ServiceTypeMappings_ServiceTypeMasters_ServiceTypeId` FOREIGN KEY (`ServiceTypeId`) REFERENCES `servicetypemasters` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 24 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for servicetypemasters
-- ----------------------------
DROP TABLE IF EXISTS `servicetypemasters`;
CREATE TABLE `servicetypemasters`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `createdBy` int NULL DEFAULT NULL,
  `createdDate` datetime(6) NULL DEFAULT NULL,
  `modifiedBy` int NULL DEFAULT NULL,
  `modifiedDate` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 23 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for vehiclemasters
-- ----------------------------
DROP TABLE IF EXISTS `vehiclemasters`;
CREATE TABLE `vehiclemasters`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CId` int NOT NULL,
  `RegNum` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `VehicleMake` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `VehicleModel` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `createdBy` int NULL DEFAULT NULL,
  `createdDate` datetime(6) NULL DEFAULT NULL,
  `modifiedBy` int NULL DEFAULT NULL,
  `modifiedDate` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_VehicleMasters_CId`(`CId` ASC) USING BTREE,
  CONSTRAINT `FK_VehicleMasters_CustomerMasters_CId` FOREIGN KEY (`CId`) REFERENCES `customermasters` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 14 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
