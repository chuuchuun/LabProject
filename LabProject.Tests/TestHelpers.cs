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
                Username = "test",
                RoleName = "Client"
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
        public static User BasicUserWithId(long id) =>
         new()
         {
             Id = id,
             Name = $"Test{id}",
             Email = $"test{id}@example.com",
             PasswordHash = $"{id}23",
             Phone = $"{id}{id}2-222-2222",
             Username = $"test{id}"
         };
        public static UserCreateDto BasicUserCreateDtoWithNoPassword() =>
            new()
            {
                Email = "test@example.com",
                Name = "Test",
                Password = "",
                Phone = "111-111-1111",
                Username = "test",
                RoleName = "Client"
            };
        public static UserUpdateDto BasicUserUpdateDto() =>
            new()
            {
                Name = "UpdatedTest",
                Email = "updatedtest@example.com"
            };
    }
}
