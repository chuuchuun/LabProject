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
    public class LocationRepository(IDbConnectionFactory dbFactory) : IRepository<Location>
    {
        private readonly IDbConnectionFactory _dbFactory = dbFactory;

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            using var connection = _dbFactory.CreateConnection();
            const string query = "SELECT * FROM locations.Locations";

            try
            {
                return await connection.QueryAsync<Location>(query);
            }
            catch
            {
                return [];
            }
        }

        public async Task<Location?> GetByIdAsync(long id)
        {
            using var connection = _dbFactory.CreateConnection();
            const string query = "SELECT * FROM locations.Locations WHERE Id = @Id";

            try
            {
                return await connection.QueryFirstOrDefaultAsync<Location>(query, new { Id = id });
            }
            catch
            {
                return null;
            }
        }
        private async Task<long> GetMaxIdAsync()
        {
            using var connection = _dbFactory.CreateConnection();
            const string sql = "SELECT ISNULL(MAX(Id), 0) FROM locations.Locations";
            return await connection.ExecuteScalarAsync<long>(sql);
        }
        public async Task<long> AddAsync(Location entity)
        {
            using var connection = _dbFactory.CreateConnection();

            var newId = await GetMaxIdAsync() + 1;
            entity.Id = newId;

            const string query = @"
                INSERT INTO locations.Locations (Id, Name, Address, City, Phone)
                VALUES (@Id, @Name, @Address, @City, @Phone);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            try
            {
                var rows = await connection.ExecuteAsync(query, entity);
                return rows > 0 ? entity.Id : 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> UpdateAsync(long id, Location entity)
        {
            using var connection = _dbFactory.CreateConnection();

            const string checkQuery = "SELECT COUNT(1) FROM locations.Locations WHERE Id = @Id";

            try
            {
                var exists = await connection.ExecuteScalarAsync<bool>(checkQuery, new { Id = id });

                if (!exists)
                    return false;

                const string updateQuery = @"
                    UPDATE locations.Locations
                    SET Name = @Name,
                        Address = @Address,
                        City = @City,
                        Phone = @Phone
                    WHERE Id = @Id;";

                var rowsAffected = await connection.ExecuteAsync(updateQuery, new
                {
                    Id = id,
                    entity.Name,
                    entity.Address,
                    entity.City,
                    entity.Phone
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
            const string query = "DELETE FROM locations.Locations WHERE Id = @Id";

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
