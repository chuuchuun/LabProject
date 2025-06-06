using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;

namespace LabProject.Domain.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Task<Role?> GetRoleByNameAsync(string roleName);
    }
}
