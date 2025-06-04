using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;

namespace LabProject.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetProvidersBySpecialtyAsync(long specialtyId);
        Task<IEnumerable<User>> GetClientFavoritesAsync(long clientId);
    }
}
