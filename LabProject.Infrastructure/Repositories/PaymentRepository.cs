using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using LabProject.Infrastructure.Interfaces;
using System;
using System.Linq;

namespace LabProject.Infrastructure.Repositories
{
    public class PaymentRepository(IDbConnectionFactory connectionFactory) : IRepository<Payment>
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

        public async Task<long> AddAsync(Payment entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = @"
                INSERT INTO appointments.Payments (AppointmentId, AmountPaid, PaidAt)
                VALUES (@AppointmentId, @AmountPaid, @PaidAt);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            try
            {
                return await db.ExecuteScalarAsync<long>(sql, new
                {
                    entity.AppointmentId,
                    entity.AmountPaid,
                    entity.PaidAt
                });
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
    }
}
