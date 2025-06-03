using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using LabProject.Infrastructure.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LabProject.Infrastructure.Repositories
{
    public class ServiceRepository(IDbConnectionFactory connectionFactory) : IServiceRepository
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
        private async Task<long> GetMaxIdAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT ISNULL(MAX(Id), 0) FROM services.Services";
            return await connection.ExecuteScalarAsync<long>(sql);
        }
       
        public async Task<long> AddAsync(Service entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            var newId = await GetMaxIdAsync() + 1;
            entity.Id = newId;

            const string sql = @"
                INSERT INTO services.Services (Id, Name, Description, DurationMinutes, Price)
                VALUES (@Id, @Name, @Description, @DurationMinutes, @Price);
                SELECT CAST(SCOPE_IDENTITY() as bigint);"
            ;

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

        public async Task<IEnumerable<Service>> GetServicesByProviderAsync(long providerId)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT s.* FROM services.Services s
                JOIN services.ProviderServices ps ON s.Id = ps.ServiceId
                WHERE ps.ProviderId = @ProviderId";

            return await connection.QueryAsync<Service>(sql, new { ProviderId = providerId });
        }
    }
}
