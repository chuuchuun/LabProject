using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Interfaces;
using System.Data;
using LabProject.Domain.Interfaces;
using System;

namespace LabProject.Infrastructure.Repositories
{
    public class AppointmentRepository(IDbConnectionFactory dbFactory) : IRepository<Appointment>
    {
        private readonly IDbConnectionFactory _dbFactory = dbFactory;

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            using var connection = _dbFactory.CreateConnection();
            const string query = "SELECT * FROM appointments.Appointments";

            try
            {
                return await connection.QueryAsync<Appointment>(query);
            }
            catch
            {
                return [];
            }
        }

        public async Task<Appointment?> GetByIdAsync(long id)
        {
            using var connection = _dbFactory.CreateConnection();
            const string query = "SELECT * FROM appointments.Appointments WHERE Id = @Id";

            try
            {
                return await connection.QueryFirstOrDefaultAsync<Appointment>(query, new { Id = id });
            }
            catch
            {
                return null;
            }
        }

        public async Task<long> AddAsync(Appointment entity)
        {
            using var connection = _dbFactory.CreateConnection();
            const string query = @"
                INSERT INTO appointments.Appointments 
                    (ClientId, ProviderId, ServiceId, LocationId, DateTime, Status)
                VALUES 
                    (@ClientId, @ProviderId, @ServiceId, @LocationId, @DateTime, @Status);
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

        public async Task<bool> UpdateAsync(long id, Appointment entity)
        {
            using var connection = _dbFactory.CreateConnection();

            const string checkQuery = "SELECT COUNT(1) FROM appointments.Appointments WHERE Id = @Id";

            try
            {
                var exists = await connection.ExecuteScalarAsync<bool>(checkQuery, new { Id = id });
                if (!exists)
                    return false;

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
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using var connection = _dbFactory.CreateConnection();
            const string query = "DELETE FROM appointments.Appointments WHERE Id = @Id";

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
