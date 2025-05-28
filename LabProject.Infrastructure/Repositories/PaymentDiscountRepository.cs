using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Interfaces;

namespace LabProject.Infrastructure.Repositories
{
    public class PaymentDiscountRepository : IRepository<PaymentDiscount>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public PaymentDiscountRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<PaymentDiscount>> GetAllAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM appointments.PaymentsDiscounts";
            return await db.QueryAsync<PaymentDiscount>(sql);
        }

        public async Task<PaymentDiscount?> GetByIdAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM appointments.PaymentsDiscounts WHERE Id = @Id";
            return await db.QuerySingleOrDefaultAsync<PaymentDiscount>(sql, new { Id = id });
        }

        public async Task<long> AddAsync(PaymentDiscount entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = @"
                INSERT INTO appointments.PaymentsDiscounts (DiscountId, PaymentId, PayedValue)
                VALUES (@DiscountId, @PaymentId, @PayedValue);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            var id = await db.ExecuteScalarAsync<long>(sql, new
            {
                entity.DiscountId,
                entity.PaymentId,
                entity.PayedValue
            });

            return id;
        }

        public async Task<bool> UpdateAsync(long id, PaymentDiscount entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = @"
                UPDATE appointments.PaymentsDiscounts SET
                    DiscountId = @DiscountId,
                    PaymentId = @PaymentId,
                    PayedValue = @PayedValue
                WHERE Id = @Id";

            int rows = await db.ExecuteAsync(sql, new
            {
                entity.DiscountId,
                entity.PaymentId,
                entity.PayedValue,
                Id = id
            });

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "DELETE FROM appointments.PaymentsDiscounts WHERE Id = @Id";
            int rows = await db.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
