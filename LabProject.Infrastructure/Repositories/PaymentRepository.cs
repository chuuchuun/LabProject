using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Interfaces;

namespace LabProject.Infrastructure.Repositories
{
    public class PaymentRepository : IRepository<Payment>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public PaymentRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM appointments.Payments";
            return await db.QueryAsync<Payment>(sql);
        }

        public async Task<Payment?> GetByIdAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM appointments.Payments WHERE Id = @Id";
            return await db.QuerySingleOrDefaultAsync<Payment>(sql, new { Id = id });
        }

        public async Task<long> AddAsync(Payment entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = @"
                INSERT INTO appointments.Payments (AppointmentId, AmountPaid, PaidAt)
                VALUES (@AppointmentId, @AmountPaid, @PaidAt);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            var id = await db.ExecuteScalarAsync<long>(sql, new
            {
                entity.AppointmentId,
                entity.AmountPaid,
                entity.PaidAt
            });

            return id;
        }

        public async Task<bool> UpdateAsync(long id, Payment entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = @"
                UPDATE appointments.Payments SET
                    AppointmentId = @AppointmentId,
                    AmountPaid = @AmountPaid,
                    PaidAt = @PaidAt
                WHERE Id = @Id";

            int rows = await db.ExecuteAsync(sql, new
            {
                entity.AppointmentId,
                entity.AmountPaid,
                entity.PaidAt,
                Id = id
            });

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "DELETE FROM appointments.Payments WHERE Id = @Id";
            int rows = await db.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
