using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Interfaces;

public class LocationRepository : IRepository<Location>
{
    private readonly IDbConnectionFactory _dbFactory;

    public LocationRepository(IDbConnectionFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        using var connection = _dbFactory.CreateConnection();
        const string query = "SELECT * FROM locations.Locations";
        return await connection.QueryAsync<Location>(query);
    }

    public async Task<Location?> GetByIdAsync(long id)
    {
        using var connection = _dbFactory.CreateConnection();
        const string query = "SELECT * FROM locations.Locations WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Location>(query, new { Id = id });
    }

    public async Task<long> AddAsync(Location entity)
    {
        using var connection = _dbFactory.CreateConnection();
        const string query = @"
            INSERT INTO locations.Locations (Name, Address, City, Phone)
            VALUES (@Name, @Address, @City, @Phone);
            SELECT CAST(SCOPE_IDENTITY() as bigint);";

        return await connection.ExecuteScalarAsync<long>(query, entity);
    }

    public async Task<bool> UpdateAsync(long id, Location entity)
    {
        using var connection = _dbFactory.CreateConnection();
        const string query = @"
            UPDATE locations.Locations
            SET Name = @Name,
                Address = @Address,
                City = @City,
                Phone = @Phone
            WHERE Id = @Id;";

        var rowsAffected = await connection.ExecuteAsync(query, new
        {
            Id = id,
            entity.Name,
            entity.Address,
            entity.City,
            entity.Phone
        });

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        using var connection = _dbFactory.CreateConnection();
        const string query = "DELETE FROM locations.Locations WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
        return rowsAffected > 0;
    }
}
