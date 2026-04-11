-- =========================
-- __EFMigrationsHistory
-- =========================
IF OBJECT_ID('dbo.__EFMigrationsHistory', 'U') IS NOT NULL DROP TABLE dbo.__EFMigrationsHistory;

CREATE TABLE dbo.__EFMigrationsHistory (
    MigrationId NVARCHAR(150) NOT NULL PRIMARY KEY,
    ProductVersion NVARCHAR(32) NOT NULL
);

-- =========================
-- LoginMasters
-- =========================
IF OBJECT_ID('dbo.LoginMasters', 'U') IS NOT NULL DROP TABLE dbo.LoginMasters;

CREATE TABLE dbo.LoginMasters (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL,
    Password NVARCHAR(200) NOT NULL,
    IsActive BIT NOT NULL,
    createdBy INT NULL,
    createdDate DATETIME2 NULL,
    modifiedBy INT NULL,
    modifiedDate DATETIME2 NULL
);

-- =========================
-- CustomerMasters
-- =========================
IF OBJECT_ID('dbo.CustomerMasters', 'U') IS NOT NULL DROP TABLE dbo.CustomerMasters;

CREATE TABLE dbo.CustomerMasters (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    userId INT NOT NULL,
    Name NVARCHAR(150) NOT NULL,
    Mobile NVARCHAR(15) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    IsActive BIT NOT NULL,
    createdBy INT NULL,
    createdDate DATETIME2 NULL,
    modifiedBy INT NULL,
    modifiedDate DATETIME2 NULL,

    CONSTRAINT FK_Customer_Login FOREIGN KEY (userId)
        REFERENCES dbo.LoginMasters(Id)
        ON DELETE CASCADE
);

CREATE INDEX IX_Customer_userId ON dbo.CustomerMasters(userId);

-- =========================
-- VehicleMasters
-- =========================
IF OBJECT_ID('dbo.VehicleMasters', 'U') IS NOT NULL DROP TABLE dbo.VehicleMasters;

CREATE TABLE dbo.VehicleMasters (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CId INT NOT NULL,
    RegNum NVARCHAR(20) NOT NULL,
    VehicleMake NVARCHAR(100) NOT NULL,
    VehicleModel NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL,
    createdBy INT NULL,
    createdDate DATETIME2 NULL,
    modifiedBy INT NULL,
    modifiedDate DATETIME2 NULL,

    CONSTRAINT FK_Vehicle_Customer FOREIGN KEY (CId)
        REFERENCES dbo.CustomerMasters(Id)
        ON DELETE CASCADE
);

CREATE INDEX IX_Vehicle_CId ON dbo.VehicleMasters(CId);

-- =========================
-- ServiceTypeMasters
-- =========================
IF OBJECT_ID('dbo.ServiceTypeMasters', 'U') IS NOT NULL DROP TABLE dbo.ServiceTypeMasters;

CREATE TABLE dbo.ServiceTypeMasters (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(MAX) NOT NULL,
    IsActive BIT NOT NULL,
    createdBy INT NULL,
    createdDate DATETIME2 NULL,
    modifiedBy INT NULL,
    modifiedDate DATETIME2 NULL
);

-- =========================
-- ServiceMasters
-- =========================
IF OBJECT_ID('dbo.ServiceMasters', 'U') IS NOT NULL DROP TABLE dbo.ServiceMasters;

CREATE TABLE dbo.ServiceMasters (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VId INT NOT NULL,
    Mileage INT NOT NULL,
    TotalBill DECIMAL(10,2) NOT NULL,
    RewardPoints INT NOT NULL,
    IsActive BIT NOT NULL,
    createdBy INT NULL,
    createdDate DATETIME2 NULL,
    modifiedBy INT NULL,
    modifiedDate DATETIME2 NULL,
    FileName NVARCHAR(MAX) NULL,
    ContentType NVARCHAR(MAX) NULL,
    FileData VARBINARY(MAX) NULL,

    CONSTRAINT FK_Service_Vehicle FOREIGN KEY (VId)
        REFERENCES dbo.VehicleMasters(Id)
        ON DELETE CASCADE
);

CREATE INDEX IX_Service_VId ON dbo.ServiceMasters(VId);

-- =========================
-- ServiceTypeMappings
-- =========================
IF OBJECT_ID('dbo.ServiceTypeMappings', 'U') IS NOT NULL DROP TABLE dbo.ServiceTypeMappings;

CREATE TABLE dbo.ServiceTypeMappings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ServiceId INT NOT NULL,
    ServiceTypeId INT NOT NULL,

    CONSTRAINT FK_STM_Service FOREIGN KEY (ServiceId)
        REFERENCES dbo.ServiceMasters(Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_STM_ServiceType FOREIGN KEY (ServiceTypeId)
        REFERENCES dbo.ServiceTypeMasters(Id)
        ON DELETE CASCADE
);

CREATE INDEX IX_STM_ServiceId ON dbo.ServiceTypeMappings(ServiceId);
CREATE INDEX IX_STM_ServiceTypeId ON dbo.ServiceTypeMappings(ServiceTypeId);