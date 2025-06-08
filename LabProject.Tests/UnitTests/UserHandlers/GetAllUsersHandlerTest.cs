using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LabProject.Application.Features.Users.Queries;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using FluentAssertions;
using LabProject.Tests;

namespace LabProject.Tests.UnitTests.UserHandlers
{
    public class GetAllUsersHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly IMapper _mapper;
        private readonly GetAllUsersQueryHandler _handler;

        public GetAllUsersHandlerTest()
        {
            _userRepoMock = new Mock<IUserRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            });
            _mapper = config.CreateMapper();

            _handler = new GetAllUsersQueryHandler(_userRepoMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedUserDtos_WhenUsersExist()
        {
            var users = new List<User>
        {
            TestHelpers.BasicUser(),
            TestHelpers.BasicUserWithId(2)
        };
            _userRepoMock.Setup(repo => repo.GetAllAsync())
                         .ReturnsAsync(users);
            var query = new GetAllUsersQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(u => u.Email == "test@example.com");
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            _userRepoMock.Setup(repo => repo.GetAllAsync())
                         .ReturnsAsync(new List<User>());
            var query = new GetAllUsersQuery();


            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
