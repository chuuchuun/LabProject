using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Interfaces;

namespace LabProject.Infrastructure.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public RoleRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM auth.Roles";
            return await db.QueryAsync<Role>(sql);
        }

        public async Task<Role?> GetByIdAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM auth.Roles WHERE Id = @Id";
            return await db.QuerySingleOrDefaultAsync<Role>(sql, new { Id = id });
        }

        public async Task<long> AddAsync(Role entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = @"
                INSERT INTO auth.Roles (Name)
                VALUES (@Name);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            var id = await db.ExecuteScalarAsync<long>(sql, new { entity.Name });
            return id;
        }

        public async Task<bool> UpdateAsync(long id, Role entity)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = @"
                UPDATE auth.Roles SET Name = @Name WHERE Id = @Id";

            int rows = await db.ExecuteAsync(sql, new { entity.Name, Id = id });
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using IDbConnection db = _connectionFactory.CreateConnection();
            string sql = "DELETE FROM auth.Roles WHERE Id = @Id";
            int rows = await db.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
