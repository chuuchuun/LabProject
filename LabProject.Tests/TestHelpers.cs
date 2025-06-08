using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Domain.Entities;

namespace LabProject.Tests
{
    public static class TestHelpers
    {
        public static UserCreateDto BasicUserCreateDto() =>
            new()
            {
                Email = "test@example.com", 
                Name = "Test", 
                Password = "123", 
                Phone = "111-111-1111", 
                Username = "test"
            };
        public static User BasicUser() =>
           new ()
           {
               Id = 1,
               Name = "Test",
               Email = "test@example.com",
               PasswordHash = "123",
               Phone = "111-111-1111",
               Username = "test"
           };
        public static UserCreateDto BasicUserCreateDtoWithNoPassword() =>
            new()
            {
                Email = "test@example.com",
                Name = "Test",
                Password = "",
                Phone = "111-111-1111",
                Username = "test"
            };
        public static UserUpdateDto BasicUserUpdateDto() =>
            new()
            {
                Name = "UpdatedTest",
                Email = "updatedtest@example.com"
            };
    }
}
