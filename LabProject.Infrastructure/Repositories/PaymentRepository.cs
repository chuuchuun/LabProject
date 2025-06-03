using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using LabProject.Infrastructure.Interfaces;
using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LabProject.Infrastructure.Repositories
{
    public class PaymentRepository(IDbConnectionFactory connectionFactory) : IPaymentRepository
    {
        private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM appointments.Payments";

            try
            {
                return await db.QueryAsync<Payment>(sql);
            }
            catch
            {
                return [];
            }
        }

        public async Task<Payment?> GetByIdAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM appointments.Payments WHERE Id = @Id";

            try
            {
                return await db.QuerySingleOrDefaultAsync<Payment>(sql, new { Id = id });
            }
            catch
            {
                return null;
            }
        }
        private async Task<long> GetMaxIdAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT ISNULL(MAX(Id), 0) FROM appointments.Payments";
            return await connection.ExecuteScalarAsync<long>(sql);
        }
        public async Task<long> AddAsync(Payment entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            var newId = await GetMaxIdAsync() + 1;
            entity.Id = newId;
            const string sql = @"
                INSERT INTO appointments.Payments (Id, AppointmentId, AmountPaid, PaidAt)
                VALUES (@Id, @AppointmentId, @AmountPaid, @PaidAt);
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

        public async Task<bool> UpdateAsync(long id, Payment entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string checkSql = "SELECT COUNT(1) FROM appointments.Payments WHERE Id = @Id";

            try
            {
                var exists = await db.ExecuteScalarAsync<bool>(checkSql, new { Id = id });
                if (!exists) return false;

                const string sql = @"
                    UPDATE appointments.Payments SET
                        AppointmentId = @AppointmentId,
                        AmountPaid = @AmountPaid,
                        PaidAt = @PaidAt
                    WHERE Id = @Id";

                var rows = await db.ExecuteAsync(sql, new
                {
                    entity.AppointmentId,
                    entity.AmountPaid,
                    entity.PaidAt,
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
            const string sql = "DELETE FROM appointments.Payments WHERE Id = @Id";

            try
            {
                var rows = await db.ExecuteAsync(sql, new { Id = id });
                return rows > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<decimal> GetTotalRevenueByProviderAsync(long providerId, DateTime startDate, DateTime endDate)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT SUM(p.AmountPaid) FROM appointments.Payments p
                JOIN appointments.Appointments a ON p.AppointmentId = a.Id
                WHERE a.ProviderId = @ProviderId
                AND p.PaidAt BETWEEN @StartDate AND @EndDate";

            return await connection.ExecuteScalarAsync<decimal>(sql, new
            {
                ProviderId = providerId,
                StartDate = startDate,
                EndDate = endDate
            });
        }
    }
}
