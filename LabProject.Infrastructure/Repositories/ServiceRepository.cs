using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Interfaces;

namespace LabProject.Infrastructure.Repositories
{
    public class ServiceRepository : IRepository<Service>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ServiceRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM services.Services";
            return await db.QueryAsync<Service>(sql);
        }

        public async Task<Service?> GetByIdAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM services.Services WHERE Id = @Id";
            return await db.QuerySingleOrDefaultAsync<Service>(sql, new { Id = id });
        }

        public async Task<long> AddAsync(Service entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = @"
                INSERT INTO services.Services (Name, Description, DurationMinutes, Price)
                VALUES (@Name, @Description, @DurationMinutes, @Price);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            var id = await db.ExecuteScalarAsync<long>(sql, new
            {
                entity.Name,
                entity.Description,
                entity.DurationMinutes,
                entity.Price
            });

            return id;
        }

        public async Task<bool> UpdateAsync(long id, Service entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = @"
                UPDATE services.Services SET
                    Name = @Name,
                    Description = @Description,
                    DurationMinutes = @DurationMinutes,
                    Price = @Price
                WHERE Id = @Id";

            int rows = await db.ExecuteAsync(sql, new
            {
                entity.Name,
                entity.Description,
                entity.DurationMinutes,
                entity.Price,
                Id = id
            });

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "DELETE FROM services.Services WHERE Id = @Id";
            int rows = await db.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
