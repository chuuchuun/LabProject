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
    public class ServiceRepository(IDbConnectionFactory connectionFactory) : IRepository<Service>
    {
        private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM services.Services";

            try
            {
                return await db.QueryAsync<Service>(sql);
            }
            catch
            {
                return [];
            }
        }

        public async Task<Service?> GetByIdAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM services.Services WHERE Id = @Id";

            try
            {
                return await db.QuerySingleOrDefaultAsync<Service>(sql, new { Id = id });
            }
            catch
            {
                return null;
            }
        }

        public async Task<long> AddAsync(Service entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = @"
                INSERT INTO services.Services (Name, Description, DurationMinutes, Price)
                VALUES (@Name, @Description, @DurationMinutes, @Price);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            try
            {
                return await db.ExecuteScalarAsync<long>(sql, new
                {
                    entity.Name,
                    entity.Description,
                    entity.DurationMinutes,
                    entity.Price
                });
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> UpdateAsync(long id, Service entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string checkSql = "SELECT COUNT(1) FROM services.Services WHERE Id = @Id";

            try
            {
                var exists = await db.ExecuteScalarAsync<bool>(checkSql, new { Id = id });
                if (!exists) return false;

                const string updateSql = @"
                    UPDATE services.Services SET
                        Name = @Name,
                        Description = @Description,
                        DurationMinutes = @DurationMinutes,
                        Price = @Price
                    WHERE Id = @Id";

                int rows = await db.ExecuteAsync(updateSql, new
                {
                    entity.Name,
                    entity.Description,
                    entity.DurationMinutes,
                    entity.Price,
                    Id = id
                });

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
            const string checkSql = "SELECT COUNT(1) FROM services.Services WHERE Id = @Id";

            try
            {
                var exists = await db.ExecuteScalarAsync<bool>(checkSql, new { Id = id });
                if (!exists) return false;

                const string deleteSql = "DELETE FROM services.Services WHERE Id = @Id";
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
