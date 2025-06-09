using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Interfaces;
using System.Data;
using LabProject.Domain.Interfaces;
using System;
using LabProject.Application.Dtos.AppontmentDtos;
using Microsoft.Data.SqlClient;

namespace LabProject.Infrastructure.Repositories
{
    public class AppointmentRepository(IDbConnectionFactory dbFactory) : IAppointmentRepository
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

        private async Task<long> GetMaxIdAsync()
        {
            using var connection = _dbFactory.CreateConnection();
            const string sql = "SELECT ISNULL(MAX(Id), 0) FROM appointments.Appointments";
            return await connection.ExecuteScalarAsync<long>(sql);
        }

        public async Task<long> AddAsync(Appointment entity)
        {
            using var connection = _dbFactory.CreateConnection();

            var newId = await GetMaxIdAsync() + 1;
            entity.Id = newId;

            const string query = @"
            INSERT INTO appointments.Appointments 
                (Id, ClientId, ProviderId, ServiceId, LocationId, DateTime, Status)
            VALUES 
                (@Id, @ClientId, @ProviderId, @ServiceId, @LocationId, @DateTime, @Status);";

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

        public async Task<IEnumerable<Appointment>> GetAppointmentsByClientIdAsync(long clientId)
        {

            using var connection = _dbFactory.CreateConnection();
            const string query = "appointments.GetClientAppointments";

            try
            {
                var appointments = await connection.QueryAsync<Appointment>(
                       query,
                        new { ClientId = clientId },
                        commandType: CommandType.StoredProcedure);

                return [.. appointments];

            }
            catch
            {
                return [];
            }
        }

        public async Task<IEnumerable<Appointment>> GetUpcomingAppointmentsForProviderAsync(long providerId)
        {

            using var connection = _dbFactory.CreateConnection();
            const string query = "appointments.GetUpcomingAppointmentsForProvider";

            try
            {
                var appointments = await connection.QueryAsync<Appointment>(
                       query,
                        new { ProviderId = providerId },
                        commandType: CommandType.StoredProcedure);

                return [.. appointments];

            }
            catch
            {
                return [];
            }
        }
    }
}
