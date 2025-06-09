using Dapper;
using Microsoft.Data.SqlClient;

namespace LabProject.Migrations
{
    public class MigrationService(SqlRunner sqlRunner, string scriptsDirectory, string connectionString) 
    {
        private readonly SqlRunner _sqlRunner = sqlRunner;
        private readonly string _scriptsDirectory = scriptsDirectory;
        private readonly string _connectionString = connectionString;

        public async Task ApplyMigrationsAsync()
        {
            var scriptFiles = Directory.GetFiles(_scriptsDirectory, "*.sql")
                                       .OrderBy(f => f)
                                       .ToList();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await connection.ExecuteAsync(@"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '__MigrationHistory')
                BEGIN
                    CREATE TABLE dbo.__MigrationHistory (
                        Id INT IDENTITY PRIMARY KEY,
                        ScriptName NVARCHAR(255) NOT NULL UNIQUE,
                        AppliedAt DATETIME NOT NULL DEFAULT GETUTCDATE()
                    )
                END
            ");

            foreach (var filePath in scriptFiles)
            {
                var scriptName = Path.GetFileName(filePath);

                var alreadyApplied = await connection.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM dbo.__MigrationHistory WHERE ScriptName = @ScriptName",
                    new { ScriptName = scriptName });

                if (alreadyApplied == 0)
                {
                    Console.WriteLine($"Applying migration: {scriptName}");
                    var sql = await File.ReadAllTextAsync(filePath);

                    var batches = SplitSqlBatches(sql);

                    foreach (var batch in batches)
                    {
                        if (!string.IsNullOrWhiteSpace(batch))
                        {
                            await connection.ExecuteAsync(batch);
                        }
                    }

                    await connection.ExecuteAsync(
                        "INSERT INTO dbo.__MigrationHistory (ScriptName) VALUES (@ScriptName)",
                        new { ScriptName = scriptName });

                    Console.WriteLine($"Migration applied: {scriptName}");
                }
                else
                {
                    Console.WriteLine($"Skipped (already applied): {scriptName}");
                }
            }

            Console.WriteLine("All migrations checked.");
        }

        private static List<string> SplitSqlBatches(string sql)
        {
            var lines = sql.Split(["\r\n", "\n"], StringSplitOptions.None);
            var batches = new List<string>();
            var currentBatch = new List<string>();

            foreach (var line in lines)
            {
                if (line.Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
                {
                    if (currentBatch.Count > 0)
                    {
                        batches.Add(string.Join(Environment.NewLine, currentBatch));
                        currentBatch.Clear();
                    }
                }
                else
                {
                    currentBatch.Add(line);
                }
            }
            if (currentBatch.Count > 0)
            {
                batches.Add(string.Join(Environment.NewLine, currentBatch));
            }

            return batches;
        }
    }
}
