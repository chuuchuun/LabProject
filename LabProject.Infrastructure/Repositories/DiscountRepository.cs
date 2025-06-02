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
    public class DiscountRepository(IDbConnectionFactory dbFactory) : IDiscountRepository
    {
        private readonly IDbConnectionFactory _connectionFactory = dbFactory;

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM marketing.Discounts";

            try
            {
                return await connection.QueryAsync<Discount>(sql);
            }
            catch
            {
                return [];
            }
        }

        public async Task<Discount?> GetByIdAsync(long id)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM marketing.Discounts WHERE Id = @Id";

            try
            {
                return await connection.QueryFirstOrDefaultAsync<Discount>(sql, new { Id = id });
            }
            catch
            {
                return null;
            }
        }

        private async Task<long> GetMaxIdAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT ISNULL(MAX(Id), 0) FROM marketing.Discounts";
            return await connection.ExecuteScalarAsync<long>(sql);
        }

        public async Task<long> AddAsync(Discount entity)
        {
            using var connection = _connectionFactory.CreateConnection();

            var newId = await GetMaxIdAsync() + 1;
            entity.Id = newId;

            const string sql = @"
            INSERT INTO marketing.Discounts (Id, ClientId, Title, Value, Description, ValidUntil)
            VALUES (@Id, @ClientId, @Title, @Value, @Description, @ValidUntil);";

            try
            {
                var rows = await connection.ExecuteAsync(sql, entity);
                return rows > 0 ? entity.Id : 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> UpdateAsync(long id, Discount entity)
        {
            using var connection = _connectionFactory.CreateConnection();

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
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "DELETE FROM marketing.Discounts WHERE Id = @Id";

            try
            {
                var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Discount>> GetValidDiscountsForClientAsync(long clientId)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT * FROM marketing.Discounts
                WHERE ClientId = @ClientId
                AND (ValidUntil IS NULL OR ValidUntil >= GETUTCDATE())";

            return await connection.QueryAsync<Discount>(sql, new { ClientId = clientId });
        }
    }
}
