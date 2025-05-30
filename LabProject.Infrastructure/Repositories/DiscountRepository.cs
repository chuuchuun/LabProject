using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Interfaces;
using LabProject.Domain.Interfaces;
using System;

namespace LabProject.Infrastructure.Repositories
{
    public class DiscountRepository(IDbConnectionFactory dbFactory) : IRepository<Discount>
    {
        private readonly IDbConnectionFactory _dbFactory = dbFactory;

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            using var connection = _dbFactory.CreateConnection();
            const string query = "SELECT * FROM marketing.Discounts";

            try
            {
                return await connection.QueryAsync<Discount>(query);
            }
            catch
            {
                return [];
            }
        }

        public async Task<Discount?> GetByIdAsync(long id)
        {
            using var connection = _dbFactory.CreateConnection();
            const string query = "SELECT * FROM marketing.Discounts WHERE Id = @Id";

            try
            {
                return await connection.QueryFirstOrDefaultAsync<Discount>(query, new { Id = id });
            }
            catch
            {
                return null;
            }
        }

        public async Task<long> AddAsync(Discount entity)
        {
            using var connection = _dbFactory.CreateConnection();
            const string query = @"
                INSERT INTO marketing.Discounts (ClientId, Title, Value, Description, ValidUntil)
                VALUES (@ClientId, @Title, @Value, @Description, @ValidUntil);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            try
            {
                return await connection.ExecuteScalarAsync<long>(query, entity);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> UpdateAsync(long id, Discount entity)
        {
            using var connection = _dbFactory.CreateConnection();

            const string checkQuery = "SELECT COUNT(1) FROM marketing.Discounts WHERE Id = @Id";

            try
            {
                var exists = await connection.ExecuteScalarAsync<bool>(checkQuery, new { Id = id });
                if (!exists)
                    return false;

                const string updateQuery = @"
                    UPDATE marketing.Discounts
                    SET ClientId = @ClientId,
                        Title = @Title,
                        Value = @Value,
                        Description = @Description,
                        ValidUntil = @ValidUntil
                    WHERE Id = @Id;";

                var rowsAffected = await connection.ExecuteAsync(updateQuery, new
                {
                    Id = id,
                    entity.ClientId,
                    entity.Title,
                    entity.Value,
                    entity.Description,
                    entity.ValidUntil
                });

                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using var connection = _dbFactory.CreateConnection();
            const string query = "DELETE FROM marketing.Discounts WHERE Id = @Id";

            try
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
