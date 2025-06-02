using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Interfaces
{
    public  interface IReadableService<TDto> where TDto : class
    {
        Task<TDto?> GetByIdAsync(long id);
        Task<IEnumerable<TDto>> GetAllAsync();
    }
}
