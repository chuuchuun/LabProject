using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Domain.Enums;
using LabProject.Infrastructure.Interfaces;
using System.Data;

public class AppointmentRepository : IRepository<Appointment>
{
    private readonly IDbConnectionFactory _dbFactory;

    public AppointmentRepository(IDbConnectionFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        using var connection = _dbFactory.CreateConnection();
        const string query = "SELECT * FROM appointments.Appointments";
        return await connection.QueryAsync<Appointment>(query);
    }

    public async Task<Appointment?> GetByIdAsync(long id)
    {
        using var connection = _dbFactory.CreateConnection();
        const string query = "SELECT * FROM appointments.Appointments WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Appointment>(query, new { Id = id });
    }

    public async Task<long> AddAsync(Appointment entity)
    {
        using var connection = _dbFactory.CreateConnection();
        const string query = @"
            INSERT INTO appointments.Appointments (ClientId, ProviderId, ServiceId, LocationId, DateTime, Status)
            VALUES (@ClientId, @ProviderId, @ServiceId, @LocationId, @DateTime, @Status);
            SELECT CAST(SCOPE_IDENTITY() as bigint);";

        return await connection.ExecuteScalarAsync<long>(query, entity);
    }

    public async Task<bool> UpdateAsync(long id, Appointment entity)
    {
        using var connection = _dbFactory.CreateConnection();
        const string query = @"
            UPDATE appointments.Appointments
            SET ClientId = @ClientId,
                ProviderId = @ProviderId,
                ServiceId = @ServiceId,
                LocationId = @LocationId,
                DateTime = @DateTime,
                Status = @Status
            WHERE Id = @Id";

        var rowsAffected = await connection.ExecuteAsync(query, new
        {
            Id = id,
            entity.ClientId,
            entity.ProviderId,
            entity.ServiceId,
            entity.LocationId,
            entity.DateTime,
            entity.Status
        });

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        using var connection = _dbFactory.CreateConnection();
        const string query = "DELETE FROM appointments.Appointments WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
        return rowsAffected > 0;
    }
}
