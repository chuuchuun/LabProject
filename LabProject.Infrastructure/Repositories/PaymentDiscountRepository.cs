using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using LabProject.Infrastructure.Interfaces;
using System;

namespace LabProject.Infrastructure.Repositories
{
    public class PaymentDiscountRepository(IDbConnectionFactory connectionFactory) : IRepository<PaymentDiscount>
    {
        private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<PaymentDiscount>> GetAllAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM appointments.PaymentsDiscounts";

            try
            {
                return await db.QueryAsync<PaymentDiscount>(sql);
            }
            catch
            {
                return [];
            }
        }

        public async Task<PaymentDiscount?> GetByIdAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM appointments.PaymentsDiscounts WHERE Id = @Id";

            try
            {
                return await db.QuerySingleOrDefaultAsync<PaymentDiscount>(sql, new { Id = id });
            }
            catch
            {
                return null;
            }
        }

        public async Task<long> AddAsync(PaymentDiscount entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = @"
                INSERT INTO appointments.PaymentsDiscounts (DiscountId, PaymentId, PayedValue)
                VALUES (@DiscountId, @PaymentId, @PayedValue);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            try
            {
                return await db.ExecuteScalarAsync<long>(sql, new
                {
                    entity.DiscountId,
                    entity.PaymentId,
                    entity.PayedValue
                });
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> UpdateAsync(long id, PaymentDiscount entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();

            const string checkSql = "SELECT COUNT(1) FROM appointments.PaymentsDiscounts WHERE Id = @Id";

            try
            {
                var exists = await db.ExecuteScalarAsync<bool>(checkSql, new { Id = id });
                if (!exists)
                    return false;

                const string sql = @"
                    UPDATE appointments.PaymentsDiscounts SET
                        DiscountId = @DiscountId,
                        PaymentId = @PaymentId,
                        PayedValue = @PayedValue
                    WHERE Id = @Id";

                int rows = await db.ExecuteAsync(sql, new
                {
                    Id = id,
                    entity.DiscountId,
                    entity.PaymentId,
                    entity.PayedValue
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
            const string sql = "DELETE FROM appointments.PaymentsDiscounts WHERE Id = @Id";

            try
            {
                int rows = await db.ExecuteAsync(sql, new { Id = id });
                return rows > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
