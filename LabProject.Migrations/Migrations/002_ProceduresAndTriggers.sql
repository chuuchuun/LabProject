-- ================================================
-- STORED PROCEDURES
-- ================================================

IF NOT EXISTS (
    SELECT * FROM sys.objects 
    WHERE type = 'P' 
      AND name = 'GetClientAppointments' 
      AND SCHEMA_NAME(schema_id) = 'appointments'
)
BEGIN
    EXEC('
    CREATE PROCEDURE appointments.GetClientAppointments
        @ClientId BIGINT
    AS
    BEGIN
        SELECT a.Id, a.DateTime, a.Status,
               s.Name AS ServiceName,
               u.Name AS ProviderName,
               l.Name AS LocationName
        FROM appointments.Appointments a
        JOIN services.Services s ON s.Id = a.ServiceId
        JOIN auth.Users u ON u.Id = a.ProviderId
        JOIN locations.Locations l ON l.Id = a.LocationId
        WHERE a.ClientId = @ClientId
        ORDER BY a.DateTime DESC;
    END;
    ');
END;
GO

IF NOT EXISTS (
    SELECT * FROM sys.objects 
    WHERE type = 'P' 
      AND name = 'GetUpcomingAppointmentsForProvider' 
      AND SCHEMA_NAME(schema_id) = 'appointments'
)
BEGIN
    EXEC('
    CREATE PROCEDURE appointments.GetUpcomingAppointmentsForProvider
        @ProviderId BIGINT
    AS
    BEGIN
        SELECT a.Id, a.DateTime, a.Status,
               u.Name AS ClientName,
               s.Name AS ServiceName
        FROM appointments.Appointments a
        JOIN auth.Users u ON u.Id = a.ClientId
        JOIN services.Services s ON s.Id = a.ServiceId
        WHERE a.ProviderId = @ProviderId AND a.DateTime >= GETUTCDATE()
        ORDER BY a.DateTime;
    END;
    ');
END;
GO

IF NOT EXISTS (
    SELECT * FROM sys.objects 
    WHERE type = 'P' 
      AND name = 'GetClientTotalPayments' 
      AND SCHEMA_NAME(schema_id) = 'appointments'
)
BEGIN
    EXEC('
    CREATE PROCEDURE appointments.GetClientTotalPayments
        @ClientId BIGINT
    AS
    BEGIN
        SELECT SUM(p.AmountPaid) AS TotalPaid
        FROM appointments.Appointments a
        JOIN appointments.Payments p ON p.AppointmentId = a.Id
        WHERE a.ClientId = @ClientId;
    END;
    ');
END;
GO

-- ================================================
-- TRIGGERS
-- ================================================

IF NOT EXISTS (
    SELECT * FROM sys.tables 
    WHERE name = 'CancelledAppointmentsLog' 
      AND schema_id = SCHEMA_ID('appointments')
)
BEGIN
    CREATE TABLE appointments.CancelledAppointmentsLog (
        AppointmentId BIGINT,
        CancelledAt DATETIME DEFAULT GETUTCDATE(),
        StatusBefore INT
    );
END
GO

CREATE OR ALTER TRIGGER appointments.trg_LogCancelledAppointments
ON appointments.Appointments
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO appointments.CancelledAppointmentsLog (AppointmentId, StatusBefore)
    SELECT d.Id, d.Status
    FROM deleted d
    JOIN inserted i ON d.Id = i.Id
    WHERE i.Status = 1 AND d.Status <> 1;
END;
