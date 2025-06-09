IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'auth')
BEGIN
    EXEC('CREATE SCHEMA auth;')
END
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'services')
BEGIN
    EXEC('CREATE SCHEMA services;')
END
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'locations')
BEGIN
    EXEC('CREATE SCHEMA locations;')
END
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'appointments')
BEGIN
    EXEC('CREATE SCHEMA appointments;')
END
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'marketing')
BEGIN
    EXEC('CREATE SCHEMA marketing;')
END
GO


IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'Roles' AND s.name = 'auth')
BEGIN
    CREATE TABLE auth.Roles (
        Id INT PRIMARY KEY,
        Name NVARCHAR(50) NOT NULL UNIQUE,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'Users' AND s.name = 'auth')
BEGIN
    CREATE TABLE auth.Users (
        Id BIGINT PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Username NVARCHAR(100) NOT NULL UNIQUE,
        Phone NVARCHAR(20) NOT NULL,
        Email NVARCHAR(255) NOT NULL,
        PasswordHash NVARCHAR(255) NOT NULL,
        RoleId INT NOT NULL FOREIGN KEY REFERENCES auth.Roles(Id),
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'Specialties' AND s.name = 'services')
BEGIN
    CREATE TABLE services.Specialties (
        Id BIGINT PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'ProviderSpecialties' AND s.name = 'services')
BEGIN
    CREATE TABLE services.ProviderSpecialties (
        ProviderId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users(Id),
        SpecialtyId BIGINT NOT NULL FOREIGN KEY REFERENCES services.Specialties(Id),
        PRIMARY KEY (ProviderId, SpecialtyId),
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'Services' AND s.name = 'services')
BEGIN
    CREATE TABLE services.Services (
        Id BIGINT PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Description NVARCHAR(1000),
        DurationMinutes INT CHECK (DurationMinutes BETWEEN 0 AND 180),
        Price DECIMAL(10,2),
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'ProviderServices' AND s.name = 'services')
BEGIN
    CREATE TABLE services.ProviderServices (
        ProviderId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users(Id),
        ServiceId BIGINT NOT NULL FOREIGN KEY REFERENCES services.Services(Id),
        PRIMARY KEY (ProviderId, ServiceId),
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'Locations' AND s.name = 'locations')
BEGIN
    CREATE TABLE locations.Locations (
        Id BIGINT PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Address NVARCHAR(255) NOT NULL,
        City NVARCHAR(100) NOT NULL,
        Phone NVARCHAR(20) NOT NULL,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'ProviderLocations' AND s.name = 'locations')
BEGIN
    CREATE TABLE locations.ProviderLocations (
        ProviderId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users(Id),
        LocationId BIGINT NOT NULL FOREIGN KEY REFERENCES locations.Locations(Id),
        PRIMARY KEY (ProviderId, LocationId),
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'Discounts' AND s.name = 'marketing')
BEGIN
    CREATE TABLE marketing.Discounts (
        Id BIGINT PRIMARY KEY,
        ClientId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users(Id),
        Title NVARCHAR(100) NOT NULL,
        Value DECIMAL(10,2) NOT NULL,
        Description NVARCHAR(255),
        ValidUntil DATE,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'Favorites' AND s.name = 'marketing')
BEGIN
    CREATE TABLE marketing.Favorites (
        ClientId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users(Id),
        ProviderId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users(Id),
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100),
        PRIMARY KEY (ClientId, ProviderId)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'Appointments' AND s.name = 'appointments')
BEGIN
    CREATE TABLE appointments.Appointments (
        Id BIGINT PRIMARY KEY,
        ClientId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users(Id),
        ProviderId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users(Id),
        ServiceId BIGINT NOT NULL FOREIGN KEY REFERENCES services.Services(Id),
        LocationId BIGINT NOT NULL FOREIGN KEY REFERENCES locations.Locations(Id),
        DateTime DATETIME,
        Status NVARCHAR(50),
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'Reviews' AND s.name = 'appointments')
BEGIN
    CREATE TABLE appointments.Reviews (
        Id BIGINT PRIMARY KEY,
        ClientId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users(Id),
        AppointmentId BIGINT NOT NULL FOREIGN KEY REFERENCES appointments.Appointments(Id),
        Rating INT CHECK (Rating BETWEEN 1 AND 5),
        Comment NVARCHAR(1000),
        DatePosted DATETIME,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'Payments' AND s.name = 'appointments')
BEGIN
    CREATE TABLE appointments.Payments (
        Id BIGINT PRIMARY KEY,
        AppointmentId BIGINT NOT NULL UNIQUE FOREIGN KEY REFERENCES appointments.Appointments(Id),
        AmountPaid DECIMAL(5,2) NOT NULL,
        PaidAt DATETIME NOT NULL,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name = 'PaymentsDiscounts' AND s.name = 'appointments')
BEGIN
    CREATE TABLE appointments.PaymentsDiscounts (
        DiscountId BIGINT NOT NULL FOREIGN KEY REFERENCES marketing.Discounts(Id),
        PaymentId BIGINT NOT NULL FOREIGN KEY REFERENCES appointments.Payments(Id),
        PayedValue DECIMAL(10,2),
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        CreatedBy NVARCHAR(100),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedBy NVARCHAR(100),
        PRIMARY KEY (DiscountId, PaymentId)
    );
END

IF NOT EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'IndexUsersRoleId' AND object_id = OBJECT_ID('auth.Users')
)
BEGIN
    CREATE INDEX IndexUsersRoleId ON auth.Users(RoleId);
END

IF NOT EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'IndexAppointmentsClientId' AND object_id = OBJECT_ID('appointments.Appointments')
)
BEGIN
    CREATE INDEX IndexAppointmentsClientId ON appointments.Appointments(ClientId);
END

IF NOT EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'IndexAppointmentsProviderId' AND object_id = OBJECT_ID('appointments.Appointments')
)
BEGIN
    CREATE INDEX IndexAppointmentsProviderId ON appointments.Appointments(ProviderId);
END

IF NOT EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'IndexAppointmentsDateTime' AND object_id = OBJECT_ID('appointments.Appointments')
)
BEGIN
    CREATE INDEX IndexAppointmentsDateTime ON appointments.Appointments(DateTime);
END

IF NOT EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'IndexPaymentsAppointmentId' AND object_id = OBJECT_ID('appointments.Payments')
)
BEGIN
    CREATE INDEX IndexPaymentsAppointmentId ON appointments.Payments(AppointmentId);
END

IF NOT EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'IndexAppointmentsStatus' AND object_id = OBJECT_ID('appointments.Appointments')
)
BEGIN
    CREATE NONCLUSTERED INDEX IndexAppointmentsStatus ON appointments.Appointments(Status);
END
