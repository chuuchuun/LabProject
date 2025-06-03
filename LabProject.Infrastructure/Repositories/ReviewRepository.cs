using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using LabProject.Infrastructure.Interfaces;

namespace LabProject.Infrastructure.Repositories
{
    public class ReviewRepository(IDbConnectionFactory connectionFactory) : IReviewRepository
    {
        private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM appointments.Reviews";
            try
            {
                return await db.QueryAsync<Review>(sql);
            }
            catch
            {
                return [];
            }
        }

        public async Task<Review?> GetByIdAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM appointments.Reviews WHERE Id = @Id";
            try
            {
                return await db.QuerySingleOrDefaultAsync<Review>(sql, new { Id = id });
            }
            catch
            {
                return null;
            }
        }
        private async Task<long> GetMaxIdAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT ISNULL(MAX(Id), 0) FROM appointments.Reviews";
            return await connection.ExecuteScalarAsync<long>(sql);
        }
        public async Task<long> AddAsync(Review entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();

            var newId = await GetMaxIdAsync() + 1;
            entity.Id = newId;

            const string sql = @"
                INSERT INTO appointments.Reviews 
                    (Id, ClientId, AppointmentId, Rating, Comment, DatePosted)
                VALUES 
                    (@Id, @ClientId, @AppointmentId, @Rating, @Comment, @DatePosted);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

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

        public async Task<bool> UpdateAsync(long id, Review entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string checkSql = "SELECT COUNT(1) FROM appointments.Reviews WHERE Id = @Id";

            try
            {
                var exists = await db.ExecuteScalarAsync<bool>(checkSql, new { Id = id });
                if (!exists) return false;

                const string sql = @"
                    UPDATE appointments.Reviews SET
                        ClientId = @ClientId,
                        AppointmentId = @AppointmentId,
                        Rating = @Rating,
                        Comment = @Comment,
                        DatePosted = @DatePosted
                    WHERE Id = @Id";

                var rows = await db.ExecuteAsync(sql, new
                {
                    entity.ClientId,
                    entity.AppointmentId,
                    entity.Rating,
                    entity.Comment,
                    entity.DatePosted,
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
            const string sql = "DELETE FROM appointments.Reviews WHERE Id = @Id";

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

        public async Task<double> GetAverageRatingForProviderAsync(long providerId)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT AVG(CAST(Rating AS FLOAT)) FROM appointments.Reviews r
                JOIN appointments.Appointments a ON r.AppointmentId = a.Id
                WHERE a.ProviderId = @ProviderId";

            return await connection.ExecuteScalarAsync<double>(sql, new { ProviderId = providerId });
        }
    }
}
