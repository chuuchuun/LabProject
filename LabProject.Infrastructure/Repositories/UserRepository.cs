using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Interfaces;

namespace LabProject.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM auth.Users";
            return await db.QueryAsync<User>(sql);
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM auth.Users WHERE Id = @Id";
            return await db.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<long> AddAsync(User entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = @"
                INSERT INTO auth.Users (Name, Username, Phone, Email, PasswordHash, RoleId)
                VALUES (@Name, @Username, @Phone, @Email, @PasswordHash, @RoleId);
                SELECT CAST(SCOPE_IDENTITY() AS bigint);";

            var id = await db.ExecuteScalarAsync<long>(sql, new
            {
                entity.Name,
                entity.Username,
                entity.Phone,
                entity.Email,
                entity.PasswordHash,
                entity.RoleId
            });

            return id;
        }

        public async Task<bool> UpdateAsync(long id, User entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = @"
                UPDATE auth.Users SET
                    Name = @Name,
                    Username = @Username,
                    Phone = @Phone,
                    Email = @Email,
                    PasswordHash = @PasswordHash,
                    RoleId = @RoleId
                WHERE Id = @Id";

            int rows = await db.ExecuteAsync(sql, new
            {
                entity.Name,
                entity.Username,
                entity.Phone,
                entity.Email,
                entity.PasswordHash,
                entity.RoleId,
                Id = id
            });

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "DELETE FROM auth.Users WHERE Id = @Id";
            int rows = await db.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
