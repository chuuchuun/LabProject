using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.LocationDtos;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Domain.Entities;

namespace LabProject.Tests
{
    public static class TestHelpers
    {
        public static UserCreateDto CorrectUserCreateDto() =>
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
        public static UserCreateDto IncorrectUserCreateDtoWithNoPassword() =>
            new()
            {
                Email = "test@example.com",
                Name = "Test",
                Password = "",
                Phone = "111-111-1111",
                Username = "test",
                RoleName = "Client"
            };
        public static UserUpdateDto CorrectUserUpdateDto() =>
            new()
            {
                Name = "UpdatedTest",
                Email = "updatedtest@example.com"
            };

        public static LocationCreateDto CorrectLocationCreateDto() =>
            new()
            {
                Name = "Test Location",
                Address = "123 Test St",
                City = "Test City",
                Phone = "111-111-1111",
            };

        public static Location BasicLocation() =>
            new()
            {
                Id = 1,
                Name = "Test Location",
                Address = "123 Test St",
                City = "Test City",
                Phone = "111-111-1111",
            };

        public static LocationUpdateDto CorrectLocationUpdateDto() =>
            new()
            {
                Name = "Updated Test Location",
                Address = "123 Test St",
                City = "Test City",
                Phone = "111-111-1111",
            };

    }
}
