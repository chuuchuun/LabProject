CREATE SCHEMA auth;
GO

CREATE SCHEMA services;
GO

CREATE SCHEMA locations;
GO

CREATE SCHEMA appointments;
GO

CREATE SCHEMA marketing;
GO


CREATE TABLE auth.Roles (
    Id INT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL UNIQUE,
	CreatedAt DATETIME DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100),
	UpdatedAt DATETIME DEFAULT GETUTCDATE(),
	UpdatedBy NVARCHAR(100)
);

CREATE TABLE auth.Users (
    Id BIGINT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    Phone NVARCHAR(20) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    RoleId INT NOT NULL FOREIGN KEY REFERENCES auth.Roles,
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100),
	UpdatedAt DATETIME DEFAULT GETUTCDATE(),
	UpdatedBy NVARCHAR(100)
);

CREATE TABLE services.Specialties (
    Id BIGINT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
	CreatedAt DATETIME DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100),
	UpdatedAt DATETIME DEFAULT GETUTCDATE(),
	UpdatedBy NVARCHAR(100)
);

CREATE TABLE services.ProviderSpecialties (
    ProviderId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users,
    SpecialtyId BIGINT NOT NULL FOREIGN KEY REFERENCES services.Specialties,
    PRIMARY KEY (ProviderId, SpecialtyId),
	CreatedAt DATETIME DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100),
	UpdatedAt DATETIME DEFAULT GETUTCDATE(),
	UpdatedBy NVARCHAR(100)
);

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

CREATE TABLE services.ProviderServices (
    ProviderId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users,
    ServiceId BIGINT NOT NULL FOREIGN KEY REFERENCES services.Services,
    PRIMARY KEY (ProviderId, ServiceId),
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100),
	UpdatedAt DATETIME DEFAULT GETUTCDATE(),
	UpdatedBy NVARCHAR(100)
);

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

CREATE TABLE locations.ProviderLocations (
    ProviderId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users,
    LocationId BIGINT NOT NULL FOREIGN KEY REFERENCES locations.Locations,
    PRIMARY KEY (ProviderId, LocationId),
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100),
	UpdatedAt DATETIME DEFAULT GETUTCDATE(),
	UpdatedBy NVARCHAR(100)
);

CREATE TABLE marketing.Discounts (
    Id BIGINT PRIMARY KEY,
    ClientId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users,
    Title NVARCHAR(100) NOT NULL,
    Value DECIMAL(10,2) NOT NULL,
    Description NVARCHAR(255),
    ValidUntil DATE,
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100),
	UpdatedAt DATETIME DEFAULT GETUTCDATE(),
	UpdatedBy NVARCHAR(100)
);


CREATE TABLE marketing.Favorites (
    ClientId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users,
    ProviderId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users,
	CreatedAt DATETIME DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100),
	UpdatedAt DATETIME DEFAULT GETUTCDATE(),
	UpdatedBy NVARCHAR(100)
    PRIMARY KEY (ClientId, ProviderId)
);

CREATE TABLE appointments.Appointments (
    Id BIGINT PRIMARY KEY,
    ClientId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users,
    ProviderId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users,
    ServiceId BIGINT NOT NULL FOREIGN KEY REFERENCES services.Services,
    LocationId BIGINT NOT NULL FOREIGN KEY REFERENCES locations.Locations,
    DateTime DATETIME,
    Status NVARCHAR(50),
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100),
	UpdatedAt DATETIME DEFAULT GETUTCDATE(),
	UpdatedBy NVARCHAR(100)
);

CREATE TABLE appointments.Reviews (
    Id BIGINT PRIMARY KEY,
    ClientId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.Users,
    AppointmentId BIGINT NOT NULL FOREIGN KEY REFERENCES appointments.Appointments,
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    Comment NVARCHAR(1000),
    DatePosted DATETIME,
	CreatedAt DATETIME DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100),
	UpdatedAt DATETIME DEFAULT GETUTCDATE(),
	UpdatedBy NVARCHAR(100)
);

CREATE TABLE appointments.Payments (
    Id BIGINT PRIMARY KEY,
    AppointmentId BIGINT NOT NULL UNIQUE FOREIGN KEY REFERENCES appointments.Appointments,
    AmountPaid DECIMAL(5,2) NOT NULL,
    PaidAt DATETIME NOT NULL,
	CreatedAt DATETIME DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100),
	UpdatedAt DATETIME DEFAULT GETUTCDATE(),
	UpdatedBy NVARCHAR(100)
);

CREATE TABLE appointments.PaymentsDiscounts (
    DiscountId BIGINT NOT NULL FOREIGN KEY REFERENCES marketing.Discounts,
    PaymentId BIGINT NOT NULL FOREIGN KEY REFERENCES appointments.Payments,
    PayedValue DECIMAL(10,2),
	CreatedAt DATETIME DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100),
	UpdatedAt DATETIME DEFAULT GETUTCDATE(),
	UpdatedBy NVARCHAR(100)
    PRIMARY KEY (DiscountId, PaymentId)
);

CREATE INDEX IndexUsersRoleId ON auth.Users(RoleId);
CREATE INDEX IndexAppointmentsClientId ON appointments.Appointments(ClientId);
CREATE INDEX IndexAppointmentsProviderId ON appointments.Appointments(ProviderId);
CREATE INDEX IndexAppointmentsDateTime ON appointments.Appointments(DateTime);
CREATE INDEX IndexPaymentsAppointmentId ON appointments.Payments(AppointmentId);
CREATE NONCLUSTERED INDEX IndexAppointmentsStatus ON appointments.Appointments(Status);
