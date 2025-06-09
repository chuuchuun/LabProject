IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '__MigrationHistory' AND schema_id = SCHEMA_ID('dbo'))
CREATE TABLE dbo.__MigrationHistory (
    Id INT IDENTITY PRIMARY KEY,
    ScriptName NVARCHAR(255) NOT NULL UNIQUE,
    AppliedAt DATETIME NOT NULL DEFAULT GETUTCDATE()
);
