using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Interfaces
{
    public interface IBaseService<T>
    where T : IBaseEntity
    {
        public Task<long> AddAsync(T entityModel);

        public Task<T?> GetByIdAsync(long id);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<bool> UpdateAsync(long id, T entityModel);

        public Task<bool> DeleteAsync(long id);
    }
}
