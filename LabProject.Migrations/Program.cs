using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using LabProject.Migrations;
using Microsoft.Data.SqlClient;
class Program
{
    static async Task Main()
    {
        var connectionString = "Server=localhost;Database=LabProject;Trusted_Connection=True;TrustServerCertificate=True;";
        const string scriptsPath = @"..\..\..\Migrations";
        var scriptsDirectory = Path.GetFullPath(scriptsPath);

        var sqlRunner = new SqlRunner(connectionString);
        var migrationService = new MigrationService(sqlRunner, scriptsDirectory, connectionString);

        await migrationService.ApplyMigrationsAsync();
    }
}
