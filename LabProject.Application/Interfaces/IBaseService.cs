using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Interfaces
{
    public interface IBaseService<TEntity, TDto, TCreateDto, TUpdateDto> : ICreatableService<TCreateDto>, IUpdatableService<TUpdateDto>, IReadableService<TDto>, IDeletableService
        where TEntity : IBaseEntity
        where TDto : BaseDto
        where TCreateDto : class
        where TUpdateDto : class
    {
    }
}
