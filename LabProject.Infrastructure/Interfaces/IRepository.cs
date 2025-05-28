using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;
using LabProject.Domain.Enums;

namespace LabProject.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(long id);
        Task<long> AddAsync(T entity);
        Task<bool> UpdateAsync(long id, T entity);
        Task<bool> DeleteAsync(long id);
    }
}
