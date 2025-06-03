using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using LabProject.Infrastructure.Interfaces;

namespace LabProject.Infrastructure.Repositories
{
    public class RoleRepository(IDbConnectionFactory connectionFactory) : IRepository<Role>
    {
        private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM auth.Roles";
            try
            {
                return await db.QueryAsync<Role>(sql);
            }
            catch
            {
                return [];
            }
        }

        public async Task<Role?> GetByIdAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM auth.Roles WHERE Id = @Id";
            try
            {
                return await db.QuerySingleOrDefaultAsync<Role>(sql, new { Id = id });
            }
            catch
            {
                return null;
            }
        }
        private async Task<int> GetMaxIdAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT ISNULL(MAX(Id), 0) FROM auth.Roles";
            return await connection.ExecuteScalarAsync<int>(sql);
        }
        public async Task<long> AddAsync(Role entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();

            var newId = await GetMaxIdAsync() + 1;
            entity.Id = newId;

            const string sql = @"
                INSERT INTO auth.Roles (Id, Name)
                VALUES (@Id, @Name);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";
            try
            {
                var rows = await db.ExecuteAsync(sql, entity);
                return rows > 0 ? entity.Id : 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> UpdateAsync(long id, Role entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();

            const string checkSql = "SELECT COUNT(1) FROM auth.Roles WHERE Id = @Id";
            try
            {
                var exists = await db.ExecuteScalarAsync<bool>(checkSql, new { Id = id });
                if (!exists) return false;

                const string updateSql = "UPDATE auth.Roles SET Name = @Name WHERE Id = @Id";
                int rows = await db.ExecuteAsync(updateSql, new { entity.Name, Id = id });

                return rows > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();

            const string checkSql = "SELECT COUNT(1) FROM auth.Roles WHERE Id = @Id";
            try
            {
                var exists = await db.ExecuteScalarAsync<bool>(checkSql, new { Id = id });
                if (!exists) return false;

                const string deleteSql = "DELETE FROM auth.Roles WHERE Id = @Id";
                int rows = await db.ExecuteAsync(deleteSql, new { Id = id });

                return rows > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
