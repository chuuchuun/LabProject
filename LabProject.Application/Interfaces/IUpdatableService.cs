using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Interfaces
{
    public interface IUpdatableService<TUpdateDto> where TUpdateDto : class
    {
        Task<bool> UpdateAsync(long id, TUpdateDto updateDto);
    }
}
