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
    public class UserRepository(IDbConnectionFactory connectionFactory) : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM auth.Users";

            try
            {
                return await db.QueryAsync<User>(sql);
            }
            catch
            {
                return [];
            }
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM auth.Users WHERE Id = @Id";

            try
            {
                return await db.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
            }
            catch
            {
                return null;
            }
        }
        public async Task<long> GetMaxIdAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string sql = "SELECT ISNULL(MAX(Id), 0) FROM auth.Users";

            return await db.ExecuteScalarAsync<long>(sql);
        }

        public async Task<long> AddAsync(User entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();

            var newId = await GetMaxIdAsync() + 1;
            entity.Id = newId;

            const string sql = @"
            INSERT INTO auth.Users (Id, Name, Username, Phone, Email, PasswordHash, RoleId)
            VALUES (@Id, @Name, @Username, @Phone, @Email, @PasswordHash, @RoleId);";

            try
            {
                int rows = await db.ExecuteAsync(sql, new
                {
                    entity.Id,
                    entity.Name,
                    entity.Username,
                    entity.Phone,
                    entity.Email,
                    entity.PasswordHash,
                    entity.RoleId
                });

                return rows > 0 ? entity.Id : 0;
            }
            catch(Exception e)
            {
                throw(e);
                return 0;
            }
        }

        public async Task<bool> UpdateAsync(long id, User entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string checkSql = "SELECT COUNT(1) FROM auth.Users WHERE Id = @Id";

            try
            {
                var exists = await db.ExecuteScalarAsync<bool>(checkSql, new { Id = id });
                if (!exists) return false;

                const string updateSql = @"
                    UPDATE auth.Users SET
                        Name = @Name,
                        Username = @Username,
                        Phone = @Phone,
                        Email = @Email,
                        PasswordHash = @PasswordHash,
                        RoleId = @RoleId
                    WHERE Id = @Id";

                int rows = await db.ExecuteAsync(updateSql, new
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
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            const string checkSql = "SELECT COUNT(1) FROM auth.Users WHERE Id = @Id";

            try
            {
                var exists = await db.ExecuteScalarAsync<bool>(checkSql, new { Id = id });
                if (!exists) return false;

                const string deleteSql = "DELETE FROM auth.Users WHERE Id = @Id";
                int rows = await db.ExecuteAsync(deleteSql, new { Id = id });

                return rows > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<User>> GetProvidersBySpecialtyAsync(long specialtyId)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT u.* FROM auth.Users u
                JOIN services.ProviderSpecialties ps ON u.Id = ps.ProviderId
                WHERE ps.SpecialtyId = @SpecialtyId
                AND u.RoleId = 2";

            return await connection.QueryAsync<User>(sql, new { SpecialtyId = specialtyId });
        }


        public async Task<IEnumerable<User>> GetClientFavoritesAsync(long clientId)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT u.* FROM auth.Users u
                JOIN marketing.Favorites f ON u.Id = f.ProviderId
                WHERE f.ClientId = @ClientId";

            return await connection.QueryAsync<User>(sql, new { ClientId = clientId });
        }
    }
}
