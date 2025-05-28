using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Interfaces;

namespace LabProject.Infrastructure.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ReviewRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM appointments.Reviews";
            return await db.QueryAsync<Review>(sql);
        }

        public async Task<Review?> GetByIdAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM appointments.Reviews WHERE Id = @Id";
            return await db.QuerySingleOrDefaultAsync<Review>(sql, new { Id = id });
        }

        public async Task<long> AddAsync(Review entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = @"
                INSERT INTO appointments.Reviews (ClientId, AppointmentId, Rating, Comment, DatePosted)
                VALUES (@ClientId, @AppointmentId, @Rating, @Comment, @DatePosted);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            var id = await db.ExecuteScalarAsync<long>(sql, new
            {
                entity.ClientId,
                entity.AppointmentId,
                entity.Rating,
                entity.Comment,
                entity.DatePosted
            });

            return id;
        }

        public async Task<bool> UpdateAsync(long id, Review entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = @"
                UPDATE appointments.Reviews SET
                    ClientId = @ClientId,
                    AppointmentId = @AppointmentId,
                    Rating = @Rating,
                    Comment = @Comment,
                    DatePosted = @DatePosted
                WHERE Id = @Id";

            int rows = await db.ExecuteAsync(sql, new
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

        public async Task<bool> DeleteAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "DELETE FROM appointments.Reviews WHERE Id = @Id";
            int rows = await db.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
