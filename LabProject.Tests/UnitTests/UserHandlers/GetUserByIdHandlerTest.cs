using Xunit;
using Moq;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using LabProject.Application.Features.Users.Queries;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using LabProject.Application.Dtos.UserDtos;
using FluentAssertions;

namespace LabProject.Tests.UnitTests.UserHandlers
{
    public class GetUserByIdQueryHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly IMapper _mapper;
        private readonly GetUserByIdQueryHandler _handler;

        public GetUserByIdQueryHandlerTests()
        {
            _userRepoMock = new Mock<IUserRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            });
            _mapper = config.CreateMapper();

            _handler = new GetUserByIdQueryHandler(_userRepoMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnUserDto_WhenUserExists()
        {
            var userId = 1;
            var user = TestHelpers.BasicUserWithId(userId);

            _userRepoMock.Setup(r => r.GetByIdAsync(userId))
                         .ReturnsAsync(user);
            var query = new GetUserByIdQuery(userId);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result!.Id.Should().Be(user.Id);
            result.Email.Should().Be(user.Email);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenUserNotFound()
        {
            var userId = 99;
            _userRepoMock.Setup(r => r.GetByIdAsync(userId))
                         .ReturnsAsync((User?)null);
            var query = new GetUserByIdQuery(userId);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().BeNull();
        }
    }
}
