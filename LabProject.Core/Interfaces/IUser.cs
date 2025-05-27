using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;

namespace LabProject.Domain.Interfaces
{
    public interface IUser
    {
        long Id { get; }
        string Name { get; }
        string Username { get; }
        string Phone { get; }
        string Email { get; }
        string PasswordHash { get; }  
        int RoleId { get; }         
        Role? Role { get; }
    }
}
