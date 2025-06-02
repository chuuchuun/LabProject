using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Interfaces
{
    public interface IDeletableService
    {
        Task<bool> DeleteAsync(long id);
    }
}
