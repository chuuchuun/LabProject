using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace LabProject.Migrations
{
    public class SqlRunner (string connectionString)
    {
        private readonly string _connectionString = connectionString;
        public async Task ExecuteScriptAsync(string sql)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql);
        }
        public async Task ExecuteScriptFromFileAsync(string filePath)
        {
            var script = await File.ReadAllTextAsync(filePath);
            await ExecuteScriptAsync(script);
        }
    }
}
